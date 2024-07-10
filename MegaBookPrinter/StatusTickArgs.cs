using System;
using System.Collections.Generic;
using System.Text;

namespace MegaBookPrinter
{
    /// <summary>
    /// Arguments for common thread status tick event.
    /// </summary>
    public class StatusTickArgs : EventArgs
    {

        public StatusTickArgs(States state)
        {
            State = state;
            ExitCode = None;
            return;
        }

        public StatusTickArgs(States state, int code)
        {
            State = state;
            ExitCode = code;
            return;
        }

        /// <summary>
        /// Available states for worker thread.
        /// </summary>
        public enum States { Unknown, Running, Stopped };

        public States State
        {
            get
            {
                return nState;
            }
            set
            {
                nState = value;
            }
        }

        static public readonly int None = -1;
        static public readonly int NormalExit = 0;
        static public readonly int StopExit = 1000;
        static public readonly int AbortExit = 1001;

        public int ExitCode
        {
            get
            {
                return nExitCode;
            }
            set
            {
                nExitCode = value;
            }
        }

        public bool IsRunning()
        {
            return (States.Running != nState);
        }

        protected States nState;

        protected int nExitCode;
    }
    /// <summary>
    /// Delegate for common thread status tick events.
    /// </summary>
    public delegate void StatusTickHandler(StatusTickArgs args);
}
