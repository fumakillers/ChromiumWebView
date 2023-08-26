using System;
using System.Windows.Forms;
using System.Threading;

namespace ChromiumWebView {
    static class Program {
        // Application起動中にGCが葬ってしまう可能性があるのでLocal変数ではなくフィールドにする
        private static Mutex mutex;
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main() {
            // MutexはIDisposableを継承してるのでusingで破棄保証
            using (mutex = new Mutex(false, Settings.MutexName)) {
                if (mutex.WaitOne(0, false) == false) {
                    return;
                }
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
        }
    }
}