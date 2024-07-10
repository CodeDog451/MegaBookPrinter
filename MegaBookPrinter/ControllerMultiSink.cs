using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Threading;

namespace MegaBookPrinter
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// Use this controller when there multible listeners or sinks
    /// of status events. 
    /// </remarks>
    public class ControllerMultiSink : ControllerBase
    {

        protected struct Listener
        {
            public Listener(Control f, StatusTickHandler d)
            {
                form = f;
                dgate = d;
            }

            public Control form;
            public StatusTickHandler dgate;

            /* matching rule 
             * form must be equal
             * null dgate matches any
             */
            public override bool Equals(object other)
            {
                bool bMatch = false;
                if (form.Equals(((Listener)other).form))
                {
                    if ((dgate != null) && (((Listener)other).dgate != null))
                    {
                        if (((Listener)other).dgate == dgate)
                        {
                            bMatch = true;
                        }
                    }
                    else
                    {
                        bMatch = true;
                    }
                }
                return bMatch;
            }

            /// <summary>
            /// Compiler desires this override
            /// </summary>
            /// <returns></returns>
            public override int GetHashCode()
            {
                return base.GetHashCode();
            }
        }

        public class MyData : SharedData
        {
            public ArrayList listSinks;

            public MyData()
            {
                listSinks = new ArrayList();
                return;
            }

        }

        protected MyData data = new MyData();

        public ControllerMultiSink(ThreadBase oBaseThread)
            : base(oBaseThread)
        {
            return;
        }

        public void addListener(Control form, StatusTickHandler dgate)
        {
            try
            {
                data.WriteLock();
                data.listSinks.Add(new Listener(form, dgate));
                data.WriteRelease();
            }
            catch (ApplicationException)
            {
            }
            return;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="form">Form that wants to detach</param>
        /// <param name="dgate">If null, matches all delegates</param>
        public void removeListener(Control form, StatusTickHandler dgate)
        {
            try
            {
                data.WriteLock();
                /* search for all matches and remove
                Because the null delegate acts as a wildcard, more
                than one match may occur */
                Listener match = new Listener(form, dgate);
                int nIndex;
                do
                {
                    nIndex = data.listSinks.IndexOf(match);
                    if (-1 != nIndex)
                    {
                        data.listSinks.RemoveAt(nIndex);
                    }
                } while (-1 != nIndex);

                data.WriteRelease();
            }
            catch (ApplicationException)
            {
            }
            return;
        }

        /// <summary>
        /// Fire events into listener threads.
        /// </summary>
        /// <remarks>
        /// This method translates the execution of the event
        /// handler into the listener threads.  The same delegates
        /// are executed, but in the context of the listener thread.
        /// In C++, this is similar to a PostMessage from a worker thread
        /// in that a message is added to the windows message queue. 
        /// </remarks>
        /// <param name="args"></param>
        override public void FireStatusTick(StatusTickArgs args)
        {
            Listener l;
            try
            {
                data.ReadLock();
                System.Collections.IEnumerator it = data.listSinks.GetEnumerator();
                while (it.MoveNext())
                {
                    l = (Listener)it.Current;
                    DoEvent(l.form, l.dgate, args);
                }
                data.ReadRelease();

                /* This yields to other threads.  Because messages
                where just posted into the listeners queues, they will
                be available to run.  Thereby, this call will make
                it likely that this thread will block here and
                the listeners will run. */
                Thread.Sleep(0);
            }
            catch (ApplicationException)
            {
            }

            return;
        }

    }
}
