using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Text;
using CefSharp.WinForms;
using System.Runtime.Serialization.Json;
using System.Reflection;
using System.Media;
using PCSC;
using PCSC.Iso7816;
using PCSC.Exceptions;
using PCSC.Monitoring;

namespace ChromiumWebView {
    /// <summary>
    /// chromium web browser - webview
    /// </summary>
    public partial class Form1 : Form {
        private ChromiumWebBrowser chromium;
        private readonly Cursor defaultCousor;
        private readonly string appPath;
        private string readerName;
        private ISCardMonitor monitor;
        private readonly SoundPlayer sound;

        #region form init
        public Form1() {
            InitializeComponent();
            appPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            defaultCousor = Cursor.Current;
            InitializeChromium();
            sound = new SoundPlayer(appPath + @"\contents\beep-06.wav");
        }

        // cefsharpはInitializeComponent後に生成
        private void InitializeChromium() {
            var settings = new CefSettings {
                Locale = System.Globalization.CultureInfo.CurrentCulture.Parent.ToString(),
                AcceptLanguageList = System.Globalization.CultureInfo.CurrentCulture.Name,
                UserDataPath = appPath,
                CachePath = appPath + Settings.CacheDirectoryName,
                LogSeverity = CefSharp.LogSeverity.Disable
            };
            CefSharp.CefSharpSettings.ShutdownOnExit = false;
            CefSharp.Cef.Initialize(settings, performDependencyCheck: false, browserProcessHandler: null);
            chromium = new ChromiumWebBrowser(Settings.Domain);
            backGroundPanel.Controls.Add(chromium);
            chromium.Dock = DockStyle.Fill;
            chromium.LoadingStateChanged += Chromium_LoadingStateChanged;
            chromium.LoadError += Chromium_LoadError;

            chromium.MenuHandler = new DisableMenuHandler();         // 右クリックでcontextmenuを描画しない
            chromium.LifeSpanHandler = new DisableLifeSpanHandler(); // javascript:window.popup発火抑制
            /* -------------------- 実験的 -------------------- */
            //chromium.FrameLoadEnd += Chromium_FrameLoadEnd;        // html置換
        }
        #endregion

        #region Chromium event
        private void Chromium_LoadError(object sender, CefSharp.LoadErrorEventArgs e) {
            chromium.Load(appPath + @"\contents\load-error.html");
        }

        private void Chromium_LoadingStateChanged(object sender, CefSharp.LoadingStateChangedEventArgs e) {
            Invoke(new Action(() => {
                void buttonEnabled(bool flag) {
                    homeToolStripButton.Enabled = flag;
                    backendToolStripButton.Enabled = flag;
                    reloadToolStripButton.Enabled = flag;
                    backToolStripButton.Enabled = flag;
                }
                if (chromium.IsLoading) {
                    buttonEnabled(false);
                    statusMessageLabel.Text = "now loading...";
                    Cursor.Current = Cursors.WaitCursor;
                    loadingToolStripButton.Visible = true;
                } else {
                    buttonEnabled(true);
                    statusMessageLabel.Text = e.Browser.MainFrame.Url;
                    Cursor.Current = defaultCousor;
                    loadingToolStripButton.Visible = false;
                    if (e.CanGoBack) {
                        backToolStripButton.Enabled = true;
                    } else {
                        backToolStripButton.Enabled = false;
                    }
                }
            }));
            //System.Diagnostics.Debug.WriteLine("state change");
        }

        private void Chromium_FrameLoadEnd(object sender, CefSharp.FrameLoadEndEventArgs e) {
            // LoadStringForUrlが走るとここのChromium_FrameLoadEndも走って無限ループ
            // LoadStringForUrlの場合、HttpStatusCode=0なのでそれ以外で下記を実行
            /*
            if (e.HttpStatusCode != 0) {
                async void asyncAction() {
                    var source = "";
                    await chromium.GetBrowser().MainFrame.GetSourceAsync().ContinueWith(
                        task => {
                            source = task.Result.ToString();
                        }
                    );
                    source = source.Replace("//affiliate.url.com/", "//127.0.0.1/");
                    //chromium.GetBrowser().MainFrame.LoadStringForUrl(source, e.Url);
                    
                }
                asyncAction();
            }*/
        }
        #endregion

        #region PCSC event
        private void Monitor_CardInserted(object sender, CardStatusEventArgs e) {
            if (!chromium.IsLoading) {
                Invoke(new Action(() => {
                    NfcRead();
                }));
            }
        }

        private void NfcRead() {
            try {
                using var context = ContextFactory.Instance.Establish(SCardScope.System);
                using var reader = context.ConnectReader(readerName, SCardShareMode.Shared, SCardProtocol.Any);
                var apdu = new CommandApdu(IsoCase.Case2Short, reader.Protocol) {
                    CLA = 0xff,
                    Instruction = InstructionCode.GetData,
                    P1 = 0x00,
                    P2 = 0x00,
                    Le = 0
                };
                using (reader.Transaction(SCardReaderDisposition.Leave)) {
                    var sendPci = SCardPCI.GetPci(reader.Protocol);
                    var receivePci = new SCardPCI();
                    var receiveBuffer = new byte[256];
                    var command = apdu.ToArray();

                    var bytesReceived = reader.Transmit(
                        sendPci,
                        command,
                        command.Length,
                        receivePci,
                        receiveBuffer,
                        receiveBuffer.Length);

                    var responseApdu = new ResponseApdu(receiveBuffer, bytesReceived, IsoCase.Case2Short, reader.Protocol);

                    if (responseApdu.HasData) {
                        var id = new StringBuilder(BitConverter.ToString(responseApdu.GetData()));
                        //var js = "document.getElementById('nfc_uid').value='" + id.ToString() + "';";
                        var js = $"nfcUidPost('{id}');" ;
                        chromium.GetBrowser().MainFrame.ExecuteJavaScriptAsync(js);
                        sound.Play();
                    } else {
                        MessageBox.Show("IDを取得できません", "error");
                    }
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "error");
            }
        }
        #endregion

        #region form/button event
        private void Form1_Shown(object sender, EventArgs e) {
            var majorVersion = Environment.OSVersion.Version.Major;
            // defaultではWindows10でもメジャーバージョンに6を返す仕様。app.manifestファイルを参照。
            if (majorVersion < 10 ) {
                var warningMessage = "Windows" + majorVersion.ToString() + "未満のバージョンをサポートしていません。";
                MessageBox.Show(warningMessage, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Application.Exit();
            }
            try {
                // check connect reader
                using var context = ContextFactory.Instance.Establish(SCardScope.System);
                var readerNames = context.GetReaders();
                if (readerNames == null || readerNames.Length < 1) {
                    throw new NoServiceException(SCardError.NoReadersAvailable);
                }
                readerName = readerNames[0];
                // create monitor instance
                monitor = MonitorFactory.Instance.Create(SCardScope.System);
                monitor.CardInserted += Monitor_CardInserted;
                monitor.Start(readerName);
            } catch (NoServiceException ex) {
                MessageBox.Show(ex.Message, "error");
                Application.Exit();
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "error");
                Application.Exit();
            }
            // windowの状態読み込み
            LoadWindowState();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e) {
            CefSharp.Cef.Shutdown();
            if(monitor != null) {
                monitor.Dispose();
            }
            if(sound != null) {
                sound.Dispose();
            }
            // wimndow状態の保存
            SaveWindowState();
        }

        private void HomeToolStripButton_Click(object sender, EventArgs e) {
            if (!chromium.IsLoading) {
                chromium.Load(Settings.Domain);
            }
        }

        private void BackendToolStripButton_Click(object sender, EventArgs e) {
            if (!chromium.IsLoading) {
                chromium.Load(Settings.Domain + Settings.BackendUrl);
            }
        }

        private void ReloadToolStripButton_Click(object sender, EventArgs e) {
            if (!chromium.IsLoading) {
                var js = "location.reload();";
                chromium.GetBrowser().MainFrame.ExecuteJavaScriptAsync(js);
            }
        }

        private void BackToolStripButton_Click(object sender, EventArgs e) {
            if (!chromium.IsLoading) {
                var js = "history.back();";
                chromium.GetBrowser().MainFrame.ExecuteJavaScriptAsync(js);
            }
        }

        private void ToolStripButton1_Click(object sender, EventArgs e) {
            if (!chromium.IsLoading) {
                var js = "document.getElementById('user_id').value='hogefuga';";
                js += "document.getElementById('password').value='PASSWORD'";
                chromium.GetBrowser().MainFrame.ExecuteJavaScriptAsync(js);
            }
        }

        private async void ToolStripButton2_Click(object sender, EventArgs e) {
            if (!chromium.IsLoading) {
                //var js = "document.getElementsByClassName('chromium-test');"; // NG
                //var js = "document.getElementsByClassName('chromium-test')[0];"; // NG
                var js = "document.getElementsByClassName('chromium-test')[0].innerText;"; // OK
                //var js = "document.getElementById('user_id').value;"; // OK
                var test = string.Empty;
                await chromium.GetBrowser().MainFrame.EvaluateScriptAsync(js).ContinueWith(
                    task => {
                        var response = task.Result;
                        if (response.Success && response.Result != null) {
                            test = response.Result.ToString();
                        }
                    }
                );
                statusMessageLabel.Text = test;
            }
        }
        #endregion

        #region private method
        private void LoadWindowState() {
            if(!File.Exists(appPath + Settings.WindowStateFileName)) {
                CreateWindowState();
            }
            using var fileStream = new FileStream(appPath + Settings.WindowStateFileName, FileMode.Open);
            var serializer = new DataContractJsonSerializer(typeof(WindowState));
            var windowState = (WindowState)serializer.ReadObject(fileStream);
            this.Width = windowState.Width;
            this.Height = windowState.Height;
            this.Location = new Point(windowState.LocationX, windowState.LocationY);
        }

        private void SaveWindowState() {
            if (!File.Exists(appPath + Settings.WindowStateFileName)) {
                CreateWindowState();
            }
            var windowState = new WindowState {
                Width = this.Width,
                Height = this.Height,
                LocationX = this.Location.X,
                LocationY = this.Location.Y
            };
            using var fileStream = new FileStream(appPath + Settings.WindowStateFileName, FileMode.Open);
            var serializer = new DataContractJsonSerializer(typeof(WindowState));
            serializer.WriteObject(fileStream, windowState);
        }

        private void CreateWindowState() {
            var windowState = new WindowState();
            using var fileStream = new FileStream(appPath + Settings.WindowStateFileName, FileMode.Create);
            var serializer = new DataContractJsonSerializer(typeof(WindowState));
            serializer.WriteObject(fileStream, windowState);
        }

        #endregion


    }
}