using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace MegaBookPrinter
{
    /// <summary>
    /// Base class for all threads.
    /// </summary>
    /// <remarks>This base class allows threads to be used with
    /// the controllers.  Similar to Java Threads, it only requires
    /// the derived class to implement the Run() method.
    /// </remarks>
    public abstract class ThreadBase
    {

        /// <summary>
        /// Event fired whenever thread has a status change.
        /// </summary>
        /// <remarks>
        /// Within this library, only a ControllerBase
        /// attaches to this event, not Forms.  The controller
        /// forwards the events to the forms.
        /// In non-form based programs, this event would be 
        /// used directly in the typical manner.
        /// </remarks>
        public event StatusTickHandler eventStatusTick;

        public ThreadBaseData basedata = new ThreadBaseData();


        /// <summary>
        /// The method used for ThreadStart() delegates
        /// </summary>
        /// <remarks>
        /// This method provides the standard behavior and
        /// exception handling to wrap the Run() method.
        /// Mainly, it ensures that the LastStatus is manipulated
        /// in the method expected by the Controller's.
        /// </remarks>
        public virtual void RunOuter()
        {
            try
            {

                /* go */
                basedata.LastStatus = new StatusTickArgs
                    (StatusTickArgs.States.Running);

                int nCode = Run();

                basedata.LastStatus = new StatusTickArgs
                    (StatusTickArgs.States.Stopped, nCode);

            }
            catch (StopRequestException /*x*/ )
            {
                basedata.LastStatus = new StatusTickArgs
                    (StatusTickArgs.States.Stopped, StatusTickArgs.StopExit);
            }
            catch (ThreadAbortException /*x*/ )
            {
                basedata.LastStatus = new StatusTickArgs
                    (StatusTickArgs.States.Stopped, StatusTickArgs.AbortExit);
            }
            finally
            {
                /* This is always set here just before the exit.
                Even if no request event was signaled, because these are
                manual reset events, it will stay signaled indefinitely.
                If the owner at some later time attempts to Stop this
                thread, the call will succeed. */
                basedata.signalStopAck.Set();
            }

            return;
        }


        /*
         * A typical implementation might be:
         * 
         override public void Run() {

            do something that changes data ...
            FireStatusTick( StatusTickArgs.States.Running );
        
            try {
              for( int i=0; i<100; i++ ) {
                Wait( 5000 );
                ... something
              }
            }
            catch( StopRequestException ) {
              throw;
            }
            ...
            
            return StatusTickArgs.NormalExit;
        }
        */
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// When implementing this method, do not
        /// catch ThreadAbortException as the outer method
        /// already handles this.
        /// </remarks>
        abstract public int Run();

        /// <summary>
        /// A delay funciton to use instead of Thread.Sleep()
        /// </summary>
        /// <remarks>The Run() method should use this method
        /// to pause for the specified period.  If the owner
        /// requests a stop during this period, a StopRequestException
        /// will be thrown.  The code in the Run() method can use 
        /// For long operations that do not contain any need to sleep
        /// Wait(0) calls should be interspersed to allow the owner
        /// to stop the thread at reasonable intervals.
        /// Note: if your program is like this
        /// for( int i=0; i<1000000; i++ ) {
        ///   /* do something */
        /// }
        /// Do not:
        /// for( int i=0; i<1000000; i++ ) {
        ///   Wait(0);
        ///   /* do something */
        /// }
        /// This will waste far too much time in the Wait.
        /// Instead do this:
        /// const int chunk = 1000;
        /// int i, j;
        /// i = 0;
        /// while( i < 1000000 ) {
        ///   Wait(0);
        ///   for( j=0; j<chunk; j++ ) {
        ///     /* do something */
        ///     i++;
        ///   }
        /// }
        /// 
        /// </remarks>
        /// <param name="nMs">Time to wait.  Can be 0.</param>
        protected void Wait(int nMs)
        {
            if (basedata.signalStopRequest.WaitOne(nMs, false))
            {
                throw new StopRequestException();
            }
            return;
        }

        /// <summary>
        /// Fire event to delegates.
        /// </summary>
        /// <remarks>
        /// Worker threads can call this method whenever there is new
        /// state information.  During thread operation, this can used
        /// to indicate progress.  At completion, an exit code can be
        /// set.
        /// </remarks>
        /// <param name="nState"></param>
        protected void FireStatusTick(StatusTickArgs.States nState)
        {
            StatusTickArgs tick = new StatusTickArgs(nState);
            FireStatusTick(tick);
            return;
        }
        protected void FireStatusTick(StatusTickArgs.States nState, int nCode)
        {
            StatusTickArgs tick = new StatusTickArgs(nState, nCode);
            FireStatusTick(tick);
            return;
        }
        protected void FireStatusTick(StatusTickArgs tick)
        {
            /* store away status for controllers use */
            basedata.LastStatus = tick;

            /* fire event to any sinks 
             * Note the use of standard event code here.
             * The delegate for this event is in
             * ControllerBase and contains the
             * code that Invoke()'s the event
             * to the listeners.  */
            if (null != eventStatusTick)
            {
                eventStatusTick(tick);
            }
            return;
        }

    }
}
