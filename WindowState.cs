using System.Runtime.Serialization;

namespace ChromiumWebView {
    /// <summary>
    /// application起動/終了時のwindow状態の保持
    /// </summary>
    [DataContract]
    public class WindowState {
        [DataMember]
        public int Width { get; set; } = 1024;
        [DataMember]
        public int Height { get; set; } = 796;
        [DataMember]
        public int LocationX { get; set; } = 0;
        [DataMember]
        public int LocationY { get; set; } = 0;
    }
}