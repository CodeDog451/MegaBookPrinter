using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace MegaBookPrinter
{
    public class ThreadBaseData : SharedData
    {

        public StatusTickArgs LastStatus
        {
            get
            {
                StatusTickArgs copy;
                ReadLock();
                copy = laststat;
                ReadRelease();
                return copy;
            }
            set
            {
                WriteLock();
                laststat = value;
                WriteRelease();
                return;
            }
        }

        /// <summary>
        /// Signal to request the thread to stop running.
        /// </summary>
        public ManualResetEvent signalStopRequest = new ManualResetEvent(false);

        /// <summary>
        /// Signal from thread that acknowledges the thread has stopped.
        /// </summary>
        /// <remarks>
        /// It is a contract that the worker thread only signals this 
        /// event as one of the last things it does before exiting.
        /// Strictly, you cannot correctly say "I'm dead" while you are alive.
        /// When the worker thread Set()'s this it should be very close to
        /// the return; in Run()
        /// </remarks>
        public ManualResetEvent signalStopAck = new ManualResetEvent(false);

        protected StatusTickArgs laststat = new StatusTickArgs(StatusTickArgs.States.Unknown);

    }
}
