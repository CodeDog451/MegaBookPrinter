using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace MegaBookPrinter
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// Use this controller when there is only one listener or sink
    /// of status events.  A single sink is the common model, where
    /// a single form creates and listens to the worker thread.  
    /// </remarks>
    public class ControllerSingleSink : ControllerBase
    {

        public class MyData : SharedData
        {
            public Control formOwner = null;
            public StatusTickHandler dgateOwner = null;
        }

        protected MyData data = new MyData();

        public ControllerSingleSink(ThreadBase oBaseThread)
            : base(oBaseThread)
        {
            return;
        }

        /// <summary>Attach the control to the worker thread.</summary>
        /// <remarks>
        /// Sets the control and delegate to receive events from
        /// the worker thread.  Note that the Delegate <b>must</b> be
        /// created in the same thread as the Control.
        /// Controls are free to attach and detach from the worker at
        /// any time.
        /// </remarks>
        /// <param name="form"></param>
        /// <param name="dgate"></param>
        /// <exception cref="System.ApplicationException">
        /// Thrown if the data
        /// cannot be set because the worker thread has locked it.  This
        /// could only happen if the thread was stalled or crashed 
        /// without releasing the lock.
        /// </exception>
        public void setListener(Control form, StatusTickHandler dgate)
        {
            data.WriteLock();
            data.formOwner = form;
            data.dgateOwner = dgate;
            data.WriteRelease();
            return;
        }


        /// <summary>Detach the listener from the worker thread.</summary>
        /// <exception cref="System.ApplicationException">
        /// Thrown if the data
        /// cannot be changed because the worker thread has locked it.  This
        /// could only happen if the thread was stalled or crashed 
        /// without releasing the lock.
        /// </exception>
        public void clearListener()
        {
            data.WriteLock();
            data.formOwner = null;
            data.dgateOwner = null;
            data.WriteRelease();
            return;
        }

        /// <summary>Fire status event to any listeners.</summary>
        /// <remarks>Note: it is safe to call this regardless of whether any listeners have
        /// been attached.</remarks>
        /// <param name="args"></param>
        override public void FireStatusTick(StatusTickArgs args)
        {
            try
            {
                data.ReadLock();
                if ((null != data.formOwner) && (null != data.dgateOwner))
                {
                    DoEvent(data.formOwner, data.dgateOwner, args);
                }
                data.ReadRelease();

                /* This yields to other threads.  Because messages
                where just posted into the listeners queues, they will
                be available to run.  If you set breakpoints, you may
                see that the Delegates are executed immediately
                when BeginInvoke is called.  This is reasonable because
                the worker has been getting all the time slices
                lately.  If the Delegates haven't executed yet,
                this call will make it likely that this 
                thread will block here and the listeners will run. */
                Thread.Sleep(0);
            }
            catch (ApplicationException)
            {
            }
            return;
        }

    }
}
