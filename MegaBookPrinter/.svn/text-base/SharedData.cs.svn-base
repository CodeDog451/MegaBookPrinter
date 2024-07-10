using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace MegaBookPrinter
{
    /// <summary>
    /// SharedData base class for providing synchronized
    /// data access.
    /// </summary>
    public class SharedData
    {

        public SharedData()
        {
        }

        public void WriteLock()
        {

            /* This collision is only caused by a coding
             * failure.  It can be removed in release builds */
#if DEBUG
            if (rwl.IsReaderLockHeld)
            {
                throw new ApplicationException("read write lock collision");
            }
#endif

            rwl.AcquireWriterLock(nMsTimeout);

            return;
        }

        public void WriteRelease()
        {
            rwl.ReleaseWriterLock();
            return;
        }


        public void ReadLock()
        {
            rwl.AcquireReaderLock(nMsTimeout);
            return;
        }


        public void ReadRelease()
        {
            rwl.ReleaseReaderLock();
            return;
        }

        /// <summary>
        /// lock used to synchronize data access
        /// </summary>
        protected ReaderWriterLock rwl = new ReaderWriterLock();

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>The amount of time the lock is held should 
        /// always be miniscule.  All that is done between lock/release
        /// pairs is an assignment or copy operation.  This value should 
        /// be the maximum amout of time a thread can be prevented from
        /// executing these simple operations before something is 
        /// considered broken.</remarks>
        protected int nMsTimeout = 1000;

        /// <summary>
        /// Safe accessor for getting an int.
        /// </summary>
        /// <remarks>These helper accessors simplify writing properties
        /// at the expense of an additional function call.  The Copy
        /// functions ensure atomic operation.  Note that for the
        /// int type, the runtime environment may likely allow only
        /// atomic operations.  Not withstanding, there is no reason
        /// to rely on such details of the runtimes implementation.
        /// This pattern is the same required for all other types.
        /// Templates would be nice here...
        /// </remarks>
        /// <param name="n">reference to variable to return</param>
        /// <returns>A copy accessed safely.</returns>
        protected int LockedCopy(ref int n)
        {
            int xRet;
            ReadLock();
            xRet = n;
            ReadRelease();
            return xRet;
        }
        protected long LockedCopy(ref long n)
        {
            long xRet;
            ReadLock();
            xRet = n;
            ReadRelease();
            return xRet;
        }
        protected string LockedCopy(ref string n)
        {
            string xRet;
            ReadLock();
            xRet = n;
            ReadRelease();
            return xRet;
        }

        /// <summary>
        /// Safe accessor for setting an int.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="var">variable to change referece</param>
        /// <param name="val">new value reference</param>
        protected void LockedSet(ref int var, ref int val)
        {
            WriteLock();
            var = val;
            WriteRelease();
            return;
        }
        protected void LockedSet(ref long var, ref long val)
        {
            WriteLock();
            var = val;
            WriteRelease();
            return;
        }
        protected void LockedSet(ref string var, ref string val)
        {
            WriteLock();
            var = val;
            WriteRelease();
            return;
        }


    }
}
