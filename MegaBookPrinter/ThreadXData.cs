using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace MegaBookPrinter
{
    /// <summary>
    /// Any data shared by thread with other
    /// threads.  
    /// </summary>
    /// <remarks>All data is exposed through
    /// accessors.  This allows proper synchronization
    /// to be enforced.
    /// </remarks>
    public class ThreadXData : SharedData
    {


        public int Complete
        {
            get
            {
                int iPer = 0;
                iPer = LockedCopy(ref nPercent);
                if(iPer > 100) iPer = 100;
                return iPer;
            }
            set
            {
                LockedSet(ref nPercent, ref value);
            }
        }

        /// <summary>
        /// Current worker status 
        /// </summary>
        public string Message
        {
            get
            {
                string sRet;
                ReadLock();
                sRet = sMsg;
                ReadRelease();
                return sRet;
            }
            set
            {
                WriteLock();
                sMsg = value;
                WriteRelease();
                return;
            }
        }
        public string FolderPath
        {
            get
            {
                string sRet;
                ReadLock();
                sRet = folderPath;
                ReadRelease();
                return sRet;
            }
            set
            {
                WriteLock();
                folderPath = value;
                WriteRelease();
                return;
            }
        }
        public string FileName
        {
            get
            {
                string sRet;
                ReadLock();
                sRet = fileName;
                ReadRelease();
                return sRet;
            }
            set
            {
                WriteLock();
                fileName = value;
                WriteRelease();
                return;
            }
        }
        public string NewFileName
        {
            get
            {
                string sRet;
                ReadLock();
                sRet = newFileName;
                ReadRelease();
                return sRet;
            }
            set
            {
                WriteLock();
                newFileName = value;
                WriteRelease();
                return;
            }
        }
        

        public string Report
        {
            get
            {
                string sRet;
                ReadLock();
                sRet = sbReport.ToString();
                ReadRelease();
                return sRet;
            }
        }

        public void ClearReport()
        {
            WriteLock();
            sbReport = new StringBuilder();
            WriteRelease();
            return;
        }

        public void AddReport(string sText)
        {
            WriteLock();
            sbReport.Append(sText);
            WriteRelease();
            return;
        }
        public string State
        {
            get
            {
                string sRet;
                ReadLock();
                sRet = sState;
                ReadRelease();
                return sRet;
            }
            set
            {
                WriteLock();
                sState = value;
                WriteRelease();
                return;
            }
        }
        /*public string StartsWith
        {
            get
            {
                string sRet;
                ReadLock();
                sRet = sStartsWith;
                ReadRelease();
                return sRet;
            }
            set
            {
                WriteLock();
                sStartsWith = value;
                WriteRelease();
                return;
            }
        }*/
        public string From
        {
            get
            {
                string sRet;
                ReadLock();
                sRet = sFrom;
                ReadRelease();
                return sRet;
            }
            set
            {
                WriteLock();
                sFrom = value;
                WriteRelease();
                return;
            }
        }
        
        public string Areacode
        {
            get
            {
                string sRet;
                ReadLock();
                sRet = sAreacode;
                ReadRelease();
                return sRet;
            }
            set
            {
                
                WriteLock();
                string sRet = value;
                if (sRet.Length > 0)
                {
                    sAreacode = sRet;
                }
                else
                {
                    sAreacode = "";
                }
                WriteRelease();
                return;
            }
        }
        public string To
        {
            get
            {
                string sRet;
                ReadLock();
                sRet = sTo;
                ReadRelease();
                return sRet;
            }
            set
            {
                WriteLock();
                sTo = value;
                WriteRelease();
                return;
            }
        }
        public int Like
        {
            get
            {
                return LockedCopy(ref iLike);
            }
            set
            {
                LockedSet(ref iLike, ref value);
            }
        }
        public string LTV
        {
            get
            {
                
                string sRet;
                ReadLock();
                sRet = sLTV;
                ReadRelease();
                return sRet;
                
            }
            set
            {
                WriteLock();
                sLTV = value;
                WriteRelease();
                return;
            }
        }
        //public long Total
        //{
            //get
            //{
                //return LockedCopy(ref iTotal);
            //}
            //set
            //{
                //LockedSet(ref iTotal, ref value);
            //}
        //}
        public int MaxPages
        {
            get
            {
                return LockedCopy(ref iMaxPages);
            }
            set
            {
                LockedSet(ref iMaxPages, ref value);
            }
        }
        public string LookingTo
        {
            get
            {
                string sRet;
                ReadLock();
                sRet = sLookingTo;
                ReadRelease();
                return sRet;
            }
            set
            {
                WriteLock();
                sLookingTo = value;
                WriteRelease();
                return;
            }
        }
        public string Zip
        {
            get
            {
                string sRet;
                ReadLock();
                sRet = sZip;
                ReadRelease();
                return sRet;
            }
            set
            {
                WriteLock();
                sZip = value;
                WriteRelease();
                return;
            }
        }
        public string City
        {
            get
            {
                string sRet;
                ReadLock();
                sRet = sCity;
                ReadRelease();
                return sRet;
            }
            set
            {
                WriteLock();
                sCity = value;
                WriteRelease();
                return;
            }
        }
        //protected long iTotal = 0;
        protected int nPercent = 0;
        protected string sMsg = "";
        protected string folderPath = "";
        protected string fileName = "";
        protected string newFileName = "";
        
        protected StringBuilder sbReport = new StringBuilder();

        protected string sState = "";
        //protected string sStartsWith = "";
        protected string sFrom = "";
        protected string sTo = "";
        protected int iLike = 1;
        protected string sAreacode = "";
        protected int iMaxPages = 0;
        protected string sLookingTo = "";
        
        protected string sZip = "";
        protected string sCity = "";
        protected string sLTV = "";
    }

}
