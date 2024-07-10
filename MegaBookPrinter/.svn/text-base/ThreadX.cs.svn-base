using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using System.Windows.Forms;
using System.Threading;
using System.IO; 
namespace MegaBookPrinter
{
    public class ThreadX : ThreadBase
    {

        public ThreadXData data;
        private MegaBookPrinter.telemarketSQLDataSetStatesTableAdapters.PagedLeadsListTableAdapter pagedLeadsListTableAdapter;
        private MegaBookPrinter.telemarketSQLDataSetStatesTableAdapters.PagedLeadsListCountTableAdapter pagedLeadsListCountTableAdapter;
        private telemarketSQLDataSetStates telemarketSQLDataSetStates;

        public ThreadX()
        {
            data = new ThreadXData();


            return;
        }

        public ThreadX(ThreadXData data)
        {
            this.data = data;
            return;
        }

        private void PrintOut(string str)
        {
            data.AddReport(str + "\r\n");
        }
        private void PrintOut(DataRow aRow)
        {
            try
            {
                string line = "";

                foreach (object element in aRow.ItemArray)
                {
                    line = line + element.ToString() + ", ";
                }
                if (line.Length > 2)
                {
                    int index = line.LastIndexOf(", ");
                    line = line.Substring(0, index + 1);
                    PrintOut(line);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "PrintOut(DataRow aRow)");
            }
        }
        private void PrintOut(DataTable aTable)
        {
            try
            {
                foreach (DataRow aRow in aTable.Rows)
                {
                    PrintOut(aRow);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "PrintOut(DataTable aTable)");
            }
        }
        protected string PhoneFormater(string sTarget)
        {
            try
            {
                string sResult = "";
                foreach (char c in sTarget)
                {
                    int temp;
                    string s = "";
                    s = s + c;
                    if ((int.TryParse(s, out temp)) || (s == "."))
                    {
                        sResult = sResult + c;
                    }
                }
                if (sResult.Length > 0)
                {
                    if (sResult.Length > 10) sResult = sResult.Substring(0, 10);
                    double d = double.Parse(sResult);
                    sResult = String.Format("{0:(###) ###-####}", d);
                }
                return sResult;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "PhoneFormater(string sTarget)");
                return "";
            }
        }
        protected string RowFill(string sTarget, int iIndex)
        {
            try
            {
                string sResult = "";
                string sFirstname = "";
                const int iMax = 7;
                if (!telemarketSQLDataSetStates.PagedLeadsList[iIndex].IsFirst_NameNull())
                {
                    sFirstname = telemarketSQLDataSetStates.PagedLeadsList[iIndex].First_Name;
                    if (sFirstname.Length > iMax) sFirstname = sFirstname.Substring(0, iMax);
                }
                sResult = sTarget.Replace("(firstname)", sFirstname);
                string sLastname = "";
                if (!telemarketSQLDataSetStates.PagedLeadsList[iIndex].IsLast_NameNull())
                {
                    sLastname = telemarketSQLDataSetStates.PagedLeadsList[iIndex].Last_Name;
                    if (sLastname.Length > iMax) sLastname = sLastname.Substring(0, iMax);
                }
                sResult = sResult.Replace("(lastname)", sLastname);
                string sCoApp = "";
                if (!telemarketSQLDataSetStates.PagedLeadsList[iIndex].IsCo_AppNull())
                {
                    sCoApp = telemarketSQLDataSetStates.PagedLeadsList[iIndex].Co_App;
                }
                sResult = sResult.Replace("(coapp)", sCoApp);
                string sCity = "";
                if (!telemarketSQLDataSetStates.PagedLeadsList[iIndex].IsCityNull())
                {
                    sCity = telemarketSQLDataSetStates.PagedLeadsList[iIndex].City;
                }
                sResult = sResult.Replace("(city)", sCity);
                string sState = "";
                if (!telemarketSQLDataSetStates.PagedLeadsList[iIndex].IsStateNull())
                {
                    sState = telemarketSQLDataSetStates.PagedLeadsList[iIndex].State;
                }
                sResult = sResult.Replace("(state)", sState);
                string sAddress = "";
                if (!telemarketSQLDataSetStates.PagedLeadsList[iIndex].IsAddressNull())
                {
                    sAddress = telemarketSQLDataSetStates.PagedLeadsList[iIndex].Address;
                    if (sAddress.Length > 15) sAddress = sAddress.Substring(0, 15);
                }
                sResult = sResult.Replace("(address)", sAddress);
                string sZip = "";
                if (!telemarketSQLDataSetStates.PagedLeadsList[iIndex].IsZipNull())
                {
                    sZip = telemarketSQLDataSetStates.PagedLeadsList[iIndex].Zip;
                }
                sResult = sResult.Replace("(zip)", sZip);
                string sHomephone = "";
                if (!telemarketSQLDataSetStates.PagedLeadsList[iIndex].IsHome_PhoneNull())
                {

                    sHomephone = PhoneFormater(telemarketSQLDataSetStates.PagedLeadsList[iIndex].Home_Phone);
                }
                sResult = sResult.Replace("(homephone1)", sHomephone);
                string sWorkphone = "";
                if (!telemarketSQLDataSetStates.PagedLeadsList[iIndex].IsWork_PhoneNull())
                {
                    sWorkphone = PhoneFormater(telemarketSQLDataSetStates.PagedLeadsList[iIndex].Work_Phone);
                }
                sResult = sResult.Replace("(homephone2)", sWorkphone);
                string sHousetype = "";
                if (!telemarketSQLDataSetStates.PagedLeadsList[iIndex].IsHouse_TypeNull())
                {
                    sHousetype = telemarketSQLDataSetStates.PagedLeadsList[iIndex].House_Type;
                }
                sResult = sResult.Replace("(housetype)", sHousetype);
                string sDesiredLoan = "";
                if (!telemarketSQLDataSetStates.PagedLeadsList[iIndex].IsDesired_LoanNull())
                {
                    sDesiredLoan = telemarketSQLDataSetStates.PagedLeadsList[iIndex].Desired_Loan.ToString();
                    double d = double.Parse(sDesiredLoan);
                    sDesiredLoan = String.Format("{0:$#,##0.00;($#,##0.00);Zero}", d);
                }
                sResult = sResult.Replace("(desiredloan)", sDesiredLoan);
                string sCredit = "";
                if (!telemarketSQLDataSetStates.PagedLeadsList[iIndex].IsCreditNull())
                {
                    sCredit = telemarketSQLDataSetStates.PagedLeadsList[iIndex].Credit;
                }
                sResult = sResult.Replace("(credit)", sCredit);
                string sWantsto = "";
                if (!telemarketSQLDataSetStates.PagedLeadsList[iIndex].IsWant_toNull())
                {
                    sWantsto = telemarketSQLDataSetStates.PagedLeadsList[iIndex].Want_to;
                    if (sWantsto.Length > 10) sWantsto = sWantsto.Substring(0, 10);
                }
                sResult = sResult.Replace("(wantsto)", sWantsto);
                sResult = sResult.Replace("(rownum)", telemarketSQLDataSetStates.PagedLeadsList[iIndex].RowNum.ToString());
                return sResult;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "RowFill(string sTarget, int iIndex)");
                return "";
            }
        }
        override public int Run()
        {
            try
            {
                //MessageBox.Show(data.MaxPages.ToString());
                //MessageBox.Show(data.State);
                //MessageBox.Show(data.Areacode);
                //MessageBox.Show(data.Zip);
                //MessageBox.Show(data.City);
                //return StatusTickArgs.NormalExit;
                this.pagedLeadsListTableAdapter = new MegaBookPrinter.telemarketSQLDataSetStatesTableAdapters.PagedLeadsListTableAdapter();
                this.pagedLeadsListCountTableAdapter = new MegaBookPrinter.telemarketSQLDataSetStatesTableAdapters.PagedLeadsListCountTableAdapter();
                this.telemarketSQLDataSetStates = new MegaBookPrinter.telemarketSQLDataSetStates();


                string sHeader = "<head><title>Call Book</title><STYLE TYPE=\"text/css\">P.breakhere {page-break-before: always}</STYLE></head><body>";

                string sTableStart = "<div><table cellspacing=\"0\" cellpadding=\"4\" rules=\"all\" border=\"1\" style=\"border-color:Black;border-width:1px;border-style:Solid;font-family:Arial;border-collapse:collapse;\" >";

                string sHeaderRow = "<tr style=\"color:Black;background-color:LightGrey;\"><th scope=\"col\" style=\"border-color:Black;font-size:X-Small;white-space:nowrap;\">First Name</th><th scope=\"col\" style=\"border-color:Black;font-size:X-Small;white-space:nowrap;\">Last Name</th><th scope=\"col\" style=\"border-color:Black;font-size:XX-Small;white-space:nowrap;\">Co App/Cell</th><th scope=\"col\" style=\"border-color:Black;font-size:XX-Small;white-space:nowrap;\">City</th><th scope=\"col\" style=\"border-color:Black;font-size:XX-Small;white-space:nowrap;\">State</th><th scope=\"col\" style=\"border-color:Black;font-size:XX-Small;white-space:nowrap;\">Address</th><th scope=\"col\" style=\"border-color:Black;font-size:XX-Small;white-space:nowrap;\">Zip</th><th scope=\"col\" style=\"border-color:Black;font-size:Small;width:200px;white-space:nowrap;\">&nbsp;Home&nbsp;Phone&nbsp;1&nbsp;</th><th align=\"center\" scope=\"col\" style=\"border-color:Black;font-size:Small;width:200px;white-space:nowrap;\">&nbsp;Home&nbsp;Phone&nbsp;2&nbsp;</th><th scope=\"col\" style=\"border-color:Black;font-size:XX-Small;\">House Type</th><th scope=\"col\" style=\"border-color:Black;font-size:XX-Small;white-space:nowrap;\">Desired Loan</th><th scope=\"col\" style=\"border-color:Black;font-size:XX-Small;white-space:nowrap;\">Credit</th><th scope=\"col\" style=\"border-color:Black;font-size:XX-Small;white-space:nowrap;\">Want to</th><th scope=\"col\" style=\"border-color:Black;font-size:XX-Small;white-space:nowrap;\">Call 1</th><th scope=\"col\" style=\"border-color:Black;font-size:XX-Small;white-space:nowrap;\">Call 2</th><th scope=\"col\" style=\"border-color:Black;font-size:XX-Small;white-space:nowrap;\">Call 3</th><th scope=\"col\" style=\"border-color:Black;font-size:XX-Small;white-space:nowrap;\">Call 4</th><th scope=\"col\" style=\"border-color:Black;font-size:XX-Small;white-space:nowrap;\">Call 5</th></tr>";
                string sDataRow = "<tr>";
                sDataRow = sDataRow + "<td style=\"border-color:Black;border-width:1px;border-style:Solid;font-size:X-Small;font-weight:bold;width:125px;white-space:nowrap;\">";

                //First Name 
                sDataRow = sDataRow + "(firstname)";

                sDataRow = sDataRow + "</td>";
                sDataRow = sDataRow + "<td style=\"border-color:Black;border-width:1px;border-style:Solid;font-size:X-Small;font-weight:bold;width:125px;white-space:nowrap;\">";

                //Last Name  
                sDataRow = sDataRow + "(lastname)";

                sDataRow = sDataRow + "</td>";
                sDataRow = sDataRow + "<td align=\"center\" style=\"border-color:Black;border-width:1px;border-style:Solid;font-size:XX-Small;width:150px;white-space:nowrap;\">";

                //Co App/Cell
                sDataRow = sDataRow + "(coapp)";

                sDataRow = sDataRow + "</td>";
                sDataRow = sDataRow + "<td style=\"border-color:Black;border-width:1px;border-style:Solid;font-size:XX-Small;width:130px;white-space:nowrap;\">";

                //City
                sDataRow = sDataRow + "(city)";

                sDataRow = sDataRow + "</td>";
                sDataRow = sDataRow + "<td align=\"center\" style=\"border-color:Black;border-width:1px;border-style:Solid;font-size:Small;width:10px;white-space:nowrap;\">";

                //State
                sDataRow = sDataRow + "(state)";

                sDataRow = sDataRow + "</td>";
                sDataRow = sDataRow + "<td style=\"border-color:Black;border-width:1px;border-style:Solid;font-size:XX-Small;width:200px;white-space:nowrap;\">";

                //Address
                sDataRow = sDataRow + "(address)";

                sDataRow = sDataRow + "</td>";
                sDataRow = sDataRow + "<td style=\"border-color:Black;border-width:1px;border-style:Solid;font-size:XX-Small;width:10px;white-space:nowrap;\">";

                //Zip code
                sDataRow = sDataRow + "(zip)";

                sDataRow = sDataRow + "</td>";
                sDataRow = sDataRow + "<td align=\"center\" style=\"border-color:Black;border-width:1px;border-style:Solid;font-size:Medium;font-weight:bold;width:150px;white-space:nowrap;\">";

                //Home Phone 1
                sDataRow = sDataRow + "(homephone1)";

                sDataRow = sDataRow + "</td>";
                sDataRow = sDataRow + "<td align=\"center\" style=\"border-color:Black;border-width:1px;border-style:Solid;font-size:Medium;font-weight:bold;width:150px;white-space:nowrap;\">";


                //Home Phone 2
                sDataRow = sDataRow + "(homephone2)";

                sDataRow = sDataRow + "</td>";
                sDataRow = sDataRow + "<td align=\"center\" valign=\"middle\" style=\"border-color:Black;border-width:1px;border-style:Solid;font-size:XX-Small;width:10px;\">";

                //House Type
                sDataRow = sDataRow + "(housetype)";

                sDataRow = sDataRow + "</td>";
                sDataRow = sDataRow + "<td align=\"center\" style=\"border-color:Black;border-width:1px;border-style:Solid;font-size:XX-Small;width:20px;white-space:nowrap;\">";

                //Desired Loan
                sDataRow = sDataRow + "(desiredloan)";

                sDataRow = sDataRow + "</td>";
                sDataRow = sDataRow + "<td align=\"center\" style=\"border-color:Black;border-width:1px;border-style:Solid;font-size:XX-Small;width:10px;white-space:nowrap;\">";

                //Credit
                sDataRow = sDataRow + "(credit)";

                sDataRow = sDataRow + "</td>";
                sDataRow = sDataRow + "<td align=\"center\" style=\"border-color:Black;border-width:1px;border-style:Solid;font-family:Arial Narrow;font-size:XX-Small;width:5px;white-space:nowrap;\">";

                //Wants to
                sDataRow = sDataRow + "(wantsto)";

                sDataRow = sDataRow + "</td>";
                sDataRow = sDataRow + "<td style=\"border-color:Black;border-width:1px;border-style:Solid;width:1px;\">";

#if DEBUG
                sDataRow = sDataRow + "(rownum)";
#endif
                sDataRow = sDataRow + " ";
                sDataRow = sDataRow + "</td>";
                sDataRow = sDataRow + "<td style=\"border-color:Black;border-width:1px;border-style:Solid;width:1px;\">";
                sDataRow = sDataRow + " ";
                sDataRow = sDataRow + "</td>";
                sDataRow = sDataRow + "<td style=\"border-color:Black;border-width:1px;border-style:Solid;width:1px;\">";
                sDataRow = sDataRow + " ";
                sDataRow = sDataRow + "</td>";
                sDataRow = sDataRow + "<td style=\"border-color:Black;border-width:1px;border-style:Solid;width:1px;\">";
                sDataRow = sDataRow + " ";
                sDataRow = sDataRow + "</td>";
                sDataRow = sDataRow + "<td style=\"border-color:Black;border-width:1px;border-style:Solid;width:1px;white-space:nowrap;\">";
                sDataRow = sDataRow + " ";
                sDataRow = sDataRow + "</td>";
                sDataRow = sDataRow + "</tr>";

                string sTableEnd = "</table></div><P CLASS='breakhere'>";


                string sEnd = "</body></html>";



                data.AddReport("Process Started\r\n");

                data.AddReport("State: " + data.State + "\r\n");
                data.AddReport("Areacode: " + data.Areacode + "\r\n");
                data.AddReport("City: " + data.City + "\r\n");
                data.AddReport("Zip: " + data.Zip + "\r\n");
                data.AddReport("From: " + data.From + "\r\n");
                data.AddReport("To: " + data.To + "\r\n");
                data.AddReport("Like: " + data.Like.ToString() + "\r\n");
                data.AddReport("Looking To: " + data.LookingTo + "\r\n");
                data.AddReport("Max Pages: " + data.MaxPages.ToString() + "\r\n");
                const int iPageLength = 21;
               /* if ((data.From.Length > 0) && (data.To.Length > 0))
                {
                    if (data.State.Length > 0)
                    {
                        //pagedLeadsListTableAdapter.Fill(this.telemarketSQLDataSetStates.PagedLeadsList, data.State, data.StartsWith, DateTime.Parse(data.From), DateTime.Parse(data.To), 0, iPageLength, data.Like, data.Areacode);
                    }
                    else
                    {
                        //all states
                        //pagedLeadsListTableAdapter.FillBy(this.telemarketSQLDataSetStates.PagedLeadsList, data.StartsWith, DateTime.Parse(data.From), DateTime.Parse(data.To), 0, iPageLength, data.Like, data.Areacode);
                    }
                }
                else
                {
                    if (data.State.Length > 0)
                    {
                        //pagedLeadsListTableAdapter.Fill(this.telemarketSQLDataSetStates.PagedLeadsList, data.State, data.StartsWith, null, null, 0, iPageLength, data.Like, data.Areacode);
                    }
                    else
                    {
                        //all states
                        //pagedLeadsListTableAdapter.FillBy(this.telemarketSQLDataSetStates.PagedLeadsList, data.StartsWith, null, null, 0, iPageLength, data.Like, data.Areacode);
                    }
                }*/

                // Create an instance of StreamWriter to write text to a file.
                // The using statement also closes the StreamWriter.
                int iFile = 0;
                //using (StreamWriter sw = new StreamWriter(data.NewFileName))
                //{
                StreamWriter sw = new StreamWriter(data.NewFileName);
                sw.WriteLine(sHeader);

                long iRowNum = 0;
                this.pagedLeadsListCountTableAdapter.Fill(telemarketSQLDataSetStates.PagedLeadsListCount, data.State, data.LookingTo, data.From, data.To, data.Like, data.Areacode, data.Zip, data.City, data.LTV);
                long iTotal = 100;
                if (telemarketSQLDataSetStates.PagedLeadsListCount.Rows.Count > 0)
                {
                    iTotal = telemarketSQLDataSetStates.PagedLeadsListCount[0].Total;
                }
                data.AddReport("fill dataset" + "\r\n");
                data.Message = "Counting Records";
                pagedLeadsListTableAdapter.Fill(this.telemarketSQLDataSetStates.PagedLeadsList, data.State, data.LookingTo, data.From, data.To, 0, iPageLength, data.Like, data.Areacode, data.Zip, data.City, data.LTV);
                data.AddReport("dataset: " + telemarketSQLDataSetStates.PagedLeadsList.Rows.Count + "\r\n");
                int iPage = 0;
                while (telemarketSQLDataSetStates.PagedLeadsList.Rows.Count > 0)
                {
                    sw.WriteLine(sTableStart);
                    sw.WriteLine(sHeaderRow);
                    string sState = telemarketSQLDataSetStates.PagedLeadsList[0].State.ToUpper().Trim();
                    for (int z = 0; z < telemarketSQLDataSetStates.PagedLeadsList.Rows.Count; z++)
                    {
                        if (sState != telemarketSQLDataSetStates.PagedLeadsList[z].State.ToUpper().Trim())
                        {
                            //sw.WriteLine(sTableEnd);
                            //sw.WriteLine(sTableStart);
                            //sw.WriteLine(sHeaderRow);
                            //sState = telemarketSQLDataSetStates.PagedLeadsList[z].State.ToUpper().Trim();
                            break;// break printout on state change
                        }
                        sw.WriteLine(RowFill(sDataRow, z));
                        iRowNum = telemarketSQLDataSetStates.PagedLeadsList[z].RowNum;
                        data.Message = iRowNum.ToString();
                        if (iTotal > 0)
                            data.Complete = Convert.ToInt32((((double)iRowNum / (double)iTotal) * (double)100));

                    }
                    //double temp = iRowNum / iTotal * 100;

                    sw.WriteLine(sTableEnd);
                    iPage++;
                    if (data.MaxPages > 0)
                    {
                        if (iPage >= data.MaxPages)
                        {
                            iFile++;
                            sw.WriteLine(sEnd);
                            sw.Close();
                            string sName = data.NewFileName;
                            sName = sName.Replace(Path.GetExtension(sName), iFile.ToString() + ".htm");
                            sw = new StreamWriter(sName);
                            sw.WriteLine(sHeader);

                            iPage = 0;
                        }
                    }
                    pagedLeadsListTableAdapter.Fill(this.telemarketSQLDataSetStates.PagedLeadsList, data.State, data.LookingTo, data.From, data.To, iRowNum + 1, iPageLength, data.Like, data.Areacode, data.Zip, data.City, data.LTV);

                    /*if ((data.From.Length > 0) && (data.To.Length > 0))
                    {
                        if (data.State.Length > 0)
                        {
                            //pagedLeadsListTableAdapter.Fill(this.telemarketSQLDataSetStates.PagedLeadsList, data.State, data.StartsWith, DateTime.Parse(data.From), DateTime.Parse(data.To), iRowNum + 1, iPageLength, data.Like, data.Areacode);
                        }
                        else
                        {
                            //all states
                            //pagedLeadsListTableAdapter.FillBy(this.telemarketSQLDataSetStates.PagedLeadsList, data.StartsWith, DateTime.Parse(data.From), DateTime.Parse(data.To), iRowNum + 1, iPageLength, data.Like, data.Areacode);
                        }
                    }
                    else
                    {
                        if (data.State.Length > 0)
                        {
                            //pagedLeadsListTableAdapter.Fill(this.telemarketSQLDataSetStates.PagedLeadsList, data.State, data.StartsWith, null, null, iRowNum + 1, iPageLength, data.Like, data.Areacode);
                        }
                        else
                        {
                            //all states
                            //pagedLeadsListTableAdapter.FillBy(this.telemarketSQLDataSetStates.PagedLeadsList, data.StartsWith, null, null, iRowNum + 1, iPageLength, data.Like, data.Areacode);
                        }
                    }*/
                }

                sw.WriteLine(sEnd);
                sw.Close();
                //}

                data.Message = iRowNum.ToString() +": done";
                data.AddReport("done\r\n");
                //data.Message = "done";
                data.Complete = 100;
                //System.Diagnostics.Process.Start(data.NewFileName);
                FireStatusTick(StatusTickArgs.States.Stopped, StatusTickArgs.NormalExit);
                //return StatusTickArgs.NormalExit;
            }
            catch (StopRequestException /*x*/ )
            {

                data.AddReport("splat - incomplete\r\n");
                /* Don't fire events here. Because the events
                are placed into queues, doing so would likely produce 
                an undesired effect.  Stop would be requested, and
                acknowledged, then one more message would be received. 
                Only set final data and cleanup. */
                throw;
                return StatusTickArgs.NormalExit;
            }
            catch (System.Exception ex)
            {

                if (ex.Message != "Thread was being aborted.")
                {
                    MessageBox.Show(ex.Message);
                }

            }
            
            return StatusTickArgs.NormalExit;

        }

    }
}
