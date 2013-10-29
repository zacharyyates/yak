using System;

namespace Yak {
    public class ExceptionEventArgs : EventArgs {
        public Exception Exception { get; set; }

        public static implicit operator ExceptionEventArgs(Exception ex) {
            return new ExceptionEventArgs { Exception = ex };
        }
    }
}