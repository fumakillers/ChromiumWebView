using CefSharp;

namespace ChromiumWebView {
    /// <summary>
    /// javascript window.openの発火抑制
    /// </summary>
    public class DisableLifeSpanHandler : ILifeSpanHandler {
        bool ILifeSpanHandler.DoClose(IWebBrowser chromiumWebBrowser, IBrowser browser) {
            return true;
        }

        void ILifeSpanHandler.OnAfterCreated(IWebBrowser chromiumWebBrowser, IBrowser browser) {
            // override only
        }

        void ILifeSpanHandler.OnBeforeClose(IWebBrowser chromiumWebBrowser, IBrowser browser) {
            // override only
        }

        bool ILifeSpanHandler.OnBeforePopup(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, string targetUrl, string targetFrameName, WindowOpenDisposition targetDisposition, bool userGesture, IPopupFeatures popupFeatures, IWindowInfo windowInfo, IBrowserSettings browserSettings, ref bool noJavascriptAccess, out IWebBrowser newBrowser) {
            newBrowser = null;
            return true;
        }
    }
}
