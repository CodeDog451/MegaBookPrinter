using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MegaBookPrinter
{
    /// <summary>
    /// Summary description for ControllerBase.
    /// </summary>
    public abstract class ControllerBase
    {

        protected ThreadBase oBaseThread;
        protected ThreadStart dgateRun;
        protected Thread thd;

        /// <summary>
        /// Status data from the thread base
        /// </summary>
        public StatusTickArgs LastStatus
        {
            get
            {
                return oBaseThread.basedata.LastStatus;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dgate">May be null, but not much will happen.</param>
        public ControllerBase(ThreadBase oBaseThread)
        {
            this.oBaseThread = oBaseThread;

            /* hook up the controller to handle the events fired from the thread.
             * Note that the controller will receive the events the normal way, i.e.
             * the method will execute in the thread context of the caller, which is
             * the worker thread.
             */
            oBaseThread.eventStatusTick += new StatusTickHandler(this.FireStatusTick);

            return;
        }

        /// <summary>
        /// Thread class accessor.
        /// </summary>
        /// <returns>The thread class provided when the controller
        /// was created.</returns>
        public ThreadBase GetThread()
        {
            return oBaseThread;
        }


        /// <summary>
        /// Starts the thread.
        /// </summary>
        public void Start()
        {
            dgateRun = new ThreadStart(oBaseThread.RunOuter);
            if (null != dgateRun)
            {
                thd = new Thread(dgateRun);
                thd.Start();
            }
            return;
        }

        /// <summary>
        /// Stop the running thread via the polite mechanism.
        /// </summary>
        /// <param name="nTimeoutMs"></param>
        /// <returns>true if the thread did stop within the
        /// specified interval.</returns>
        public bool Stop(int nTimeoutMs)
        {
            bool bThreadStopped = false;
            if (null != oBaseThread)
            {

                /* set signal to request the thread to stop */
                oBaseThread.basedata.signalStopRequest.Set();

                /* block until the thread acknowledges that it
                will stop normally.  Strictly, the thread is
                still running at this exact moment.  It is a 
                design contract that the thread only signals the
                acknowledge event as its last action. */
                if (oBaseThread.basedata.signalStopAck.WaitOne(nTimeoutMs, false))
                {
                    bThreadStopped = true;
                }
            }
            return bThreadStopped;
        }

        /// <summary>
        /// Abort the running thread.
        /// </summary>
        /// <remarks>This method terminates the thread with malice.
        /// This method will block up to the timeout interval waiting
        /// for the thread to exit.
        /// </remarks>
        /// <param name="nTimeoutMs">Maximum time in ms to wait</param>
        public bool Abort(long nTimeoutMs)
        {
            bool bThreadAborted = false;
            if (null != thd)
            {

                /* try to abort the thread */
                thd.Abort();

                /* poll looking for a clean exit code 
                Threads can struggle to stay alive, so the
                timeout here is necessary to prevent the
                owner from being blocked by a misbehaved
                thread. 
                Note that the exit code is not checked here.
                This method does not care why the thread has stopped
                only that it is stopped.
                */
                const int nPeriod = 100;
                long nStart, nNow;
                nStart = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                do
                {
                    Thread.Sleep(nPeriod);
                    bThreadAborted = (oBaseThread.basedata.LastStatus.State == StatusTickArgs.States.Stopped);
                    nNow = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                }
                while ((!bThreadAborted) && ((nNow - nStart) < nTimeoutMs));
            }
            return bThreadAborted;
        }

        /// <summary>
        /// Terminate thread by all means available.
        /// </summary>
        /// <remarks>Tries to Stop the thread then Abort.  Use
        /// this method when the caller does not care how the 
        /// thread was stopped, just that it is stopped.
        /// </remarks>
        /// <param name="nTimeoutMs"></param>
        /// <returns></returns>
        public bool Terminate(int nTimeoutMs)
        {
            bool bDead = false;

            if (Stop(nTimeoutMs))
            {
                bDead = true;
            }
            else if (Abort(nTimeoutMs))
            {
                bDead = true;
            }

            return bDead;
        }


        abstract public void FireStatusTick(StatusTickArgs args);


        static protected void DoEvent(Control form, Delegate dgate, StatusTickArgs args)
        {
            try
            {
                form.BeginInvoke(dgate, new object[] { args });
                /* You can change this to:
                form.Invoke( dgate, new object[]{ args } );
                to cause the delegate to execute synchronously.  The
                thread context will be correctly manipulated by the framework
                to cause the delegate to execute in the main threads context.
                Largely because the destination is a Form, I consider this bad
                practice.  There is no reason to stop the worker thread
                until the UI processes the event.  The events are all
                one-directional as they should be.  The model (worker thread)
                should not care about the view (Form).
                */
                echoThread();
            }
            catch (Exception)
            {
            }
            return;
        }

        [System.Diagnostics.Conditional("DEBUG")]
        static protected void echoThread()
        {
            string sWork;
            sWork = "fire [" + Thread.CurrentThread.Name + "]";
            System.Diagnostics.Debug.WriteLine(sWork);
            return;
        }

    }
}
