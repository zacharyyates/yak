using System;

namespace Yak {
    public class DynamicEventArgs : EventArgs {
        public dynamic Data { get; set; }
    }
}