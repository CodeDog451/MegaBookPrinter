using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace MegaBookPrinter
{
    public partial class FormMain : Form, IMessageFilter
    {
        const int WM_KEYDOWN = 0x100;
        const int WM_KEYUP = 0x101;

        public bool PreFilterMessage(ref Message m)
        {
            CheckKeyState();

            return false;
        }
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            try
            {
                // TODO: This line of code loads data into the 'telemarketSQLDataSetStates.tblSelect' table. You can move, or remove it, as needed.
                this.tblSelectTableAdapter.Fill(this.telemarketSQLDataSetStates.tblSelect);
                // TODO: This line of code loads data into the 'telemarketSQLDataSetStates.LeadCount' table. You can move, or remove it, as needed.

                // TODO: This line of code loads data into the 'telemarketSQLDataSetStates.states' table. You can move, or remove it, as needed.
               
                // TODO: This line of code loads data into the 'telemarketSQLDataSet.telemarketLeads' table. You can move, or remove it, as needed.

                //this adds the custom message filter I use to trap the cap lock, num lock and scroll lock keys
                Application.AddMessageFilter(this);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error:Form1_Load " + ex.Message);
            }

        }

        

        private void dataGridViewStates_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (((DataGridView)sender).SelectedRows.Count > 0)
                {
                    //textBoxState.Text = ((DataGridView)sender).SelectedRows[0].Cells[0].Value.ToString().Replace("\"", "");
                    //this.leadCountTableAdapter.Fill(this.telemarketSQLDataSetStates.LeadCount, textBoxState.Text);
                    //SetFiltersGrid();

                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error:dataGridViewStates_SelectionChanged" + ex.Message);
            }
        }

        private void dataGridViewLookingto_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (((DataGridView)sender).SelectedRows.Count > 0)
                {
                    textBoxLookingTo.Text = ((DataGridView)sender).SelectedRows[0].Cells[0].Value.ToString();
                    textBoxCount.Text = ((DataGridView)sender).SelectedRows[0].Cells[1].Value.ToString();
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error:dataGridViewLookingto_SelectionChanged" + ex.Message);
            }
        }

        

        private void checkBoxDates_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                panelDates.Enabled = ((CheckBox)sender).Checked;
                SetFiltersGrid();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error:checkBoxDates_CheckedChanged" + ex.Message);
            }
        }

        private void checkBoxStates_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                panelStates.Enabled = ((CheckBox)sender).Checked;
                dataGridViewStates.Enabled = ((CheckBox)sender).Checked;

                if (((CheckBox)sender).Checked)
                {
                    this.statesTableAdapter.Fill(this.telemarketSQLDataSetStates.states);
                    dataGridViewStates.DataSource = statesBindingSource;
                }
                else
                {
                    dataGridViewStates.ClearSelection();
                    dataGridViewStates.DataSource = null;

                }
                SetFiltersGrid();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error:checkBoxStates_CheckedChanged" + ex.Message);
            }
        }
        private void ThreadSetFiltersGrid()
        {
            
        }
        private void SetFiltersGrid()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                
                
                string sFrom = "";
                string sTo = "";
                string sState = "";
                string sCity = "";
                string sZip = "";
                string sAreacode = "";
                string sLTV = "";
                panelLookingto.Enabled = checkBoxLookingto.Checked;
                
                if (checkBoxDates.Checked)
                {
                    
                    DateTime dtFrom, dtTo;
                    
                    dtFrom = new DateTime(dateTimePickerFrom.Value.Year, dateTimePickerFrom.Value.Month, dateTimePickerFrom.Value.Day, 0, 0, 0);
                    dtTo = new DateTime(dateTimePickerTo.Value.Year, dateTimePickerTo.Value.Month, dateTimePickerTo.Value.Day, 0, 0, 0);

                    sFrom = dtFrom.ToShortDateString();
                    sTo = dtTo.ToShortDateString();
                    
                }
                if (checkBoxStates.Checked)
                {
                    sState = textBoxState.Text;
                }

                if (checkBoxCity.Checked)
                {
                    sCity = textBoxCity.Text;
                }
                if (checkBoxAreacode.Checked)
                {
                    sAreacode = maskedTextBoxAreacode.Text;
                }
                if (checkBoxZip.Checked)
                {
                    sZip = textBoxZip.Text;
                }
                
                if (checkBoxCity.Checked)
                {
                    this.citysTableAdapter.Fill(this.telemarketSQLDataSetStates.Citys, sState, sAreacode, sZip, sFrom, sTo);
                    this.dataGridViewCitys.DataSource = citysBindingSource;
                }
                else
                {
                    dataGridViewCitys.DataSource = null;
                }
                if (checkBoxAreacode.Checked)
                {
                    this.areacodesTableAdapter.Fill(this.telemarketSQLDataSetStates.Areacodes, sState, sCity, sZip, sFrom, sTo);
                    dataGridViewAreacodes.DataSource = areacodesBindingSource;
                }
                else
                {
                    dataGridViewAreacodes.DataSource = null;
                }
                if (checkBoxZip.Checked)
                {
                    this.zipsTableAdapter.Fill(this.telemarketSQLDataSetStates.Zips, sState, sAreacode, sCity, sFrom, sTo);
                    this.dataGridViewZips.DataSource = zipsBindingSource;
                }
                else
                {
                    this.dataGridViewZips.DataSource = null;
                }
                if (this.textBoxLTV.Text.Length > 0)
                {
                    sLTV = textBoxLTV.Text;
                }
                if (checkBoxLookingto.Checked)
                {

                    this.lookingToTypes1TableAdapter.Fill(this.telemarketSQLDataSetStates.LookingToTypes1, sState, sAreacode, sCity, sZip, sFrom, sTo, sLTV);
                    dataGridViewLookingto.DataSource = lookingToTypesBindingSource;                    
                }
                else
                {
                    dataGridViewLookingto.DataSource = null;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error:SetFiltersGrid" + ex.Message);
            }
            this.Cursor = Cursors.Arrow;
        }
        private void checkBoxLookingto_CheckedChanged(object sender, EventArgs e)
        {
            SetFiltersGrid();
        }

        private void buttonPreview_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                string sFrom = "";
                string sTo = "";
                string sState = "";
                string sCity = "";
                string sZip = "";
                string sAreacode = "";

                int iLike = 1;
                string sLookingTo = "";
                string sLTV = "";

                if (checkBoxDates.Checked)
                {

                    DateTime dtFrom, dtTo;

                    dtFrom = new DateTime(dateTimePickerFrom.Value.Year, dateTimePickerFrom.Value.Month, dateTimePickerFrom.Value.Day, 0, 0, 0);
                    dtTo = new DateTime(dateTimePickerTo.Value.Year, dateTimePickerTo.Value.Month, dateTimePickerTo.Value.Day, 0, 0, 0);

                    sFrom = dtFrom.ToShortDateString();
                    sTo = dtTo.ToShortDateString();

                }
                if (checkBoxStates.Checked)
                {
                    sState = textBoxState.Text;
                }

                if (checkBoxCity.Checked)
                {
                    sCity = textBoxCity.Text;
                }
                if (checkBoxAreacode.Checked)
                {
                    sAreacode = maskedTextBoxAreacode.Text;
                }
                if (checkBoxZip.Checked)
                {
                    sZip = textBoxZip.Text;
                }
                if (checkBoxLookingto.Checked)
                {
                    sLookingTo = textBoxLookingTo.Text;
                    iLike = int.Parse(comboBoxLike.SelectedValue.ToString());
                }
                if (textBoxLTV.Text.Length > 0)
                {
                    sLTV = textBoxLTV.Text;
                }
                pagedLeadsListTableAdapter.Fill(this.telemarketSQLDataSetStates.PagedLeadsList, sState, sLookingTo, sFrom, sTo, 0, 24, iLike, sAreacode, sZip, sCity, sLTV);
                /*if (checkBoxStates.Checked)
                {
                    sState = textBoxState.Text = dataGridViewStates.SelectedRows[0].Cells[0].Value.ToString().Replace("\"", "");
                }
                if (checkBoxLookingto.Checked)
                {
                    if (dataGridViewLookingto.SelectedRows.Count > 0)
                    {
                        //sStartsWith = textBoxStartsWith.Text = dataGridViewLookingto.SelectedRows[0].Cells[0].Value.ToString();
                        iLike = int.Parse(comboBoxLike.SelectedValue.ToString());
                    }
                }
                if (checkBoxDates.Checked)
                {
                    //DateTime dtFrom = this.dateTimePickerFrom.Value;
                    //DateTime dtTo = this.dateTimePickerTo.Value;
                    DateTime dtFrom = new DateTime(dateTimePickerFrom.Value.Year, dateTimePickerFrom.Value.Month, dateTimePickerFrom.Value.Day, 0, 0, 0);
                    DateTime dtTo = new DateTime(dateTimePickerTo.Value.Year, dateTimePickerTo.Value.Month, dateTimePickerTo.Value.Day, 0, 0, 0);
                    if (checkBoxStates.Checked)
                    {
                        //pagedLeadsListTableAdapter.Fill(this.telemarketSQLDataSetStates.PagedLeadsList, sState, sStartsWith, dtFrom, dtTo, 0, 24, iLike, maskedTextBoxAreacode.Text);
                    }
                    else
                    {
                        //all states
                        //pagedLeadsListTableAdapter.FillBy(this.telemarketSQLDataSetStates.PagedLeadsList, sStartsWith, dtFrom, dtTo, 0, 24, iLike, maskedTextBoxAreacode.Text);
                    }
                }
                else
                {
                    if (checkBoxStates.Checked)
                    {
                        //pagedLeadsListTableAdapter.Fill(this.telemarketSQLDataSetStates.PagedLeadsList, sState, sStartsWith, null, null, 0, 24, iLike, maskedTextBoxAreacode.Text);
                    }
                    else
                    {
                        //all states
                        //pagedLeadsListTableAdapter.FillBy(this.telemarketSQLDataSetStates.PagedLeadsList, sStartsWith, null, null, 0, 24, iLike, maskedTextBoxAreacode.Text);
                    }
                }*/
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error:buttonPreview_Click" + ex.Message);
            }
            this.Cursor = Cursors.Arrow;
            
        }
        protected ThreadXData threadData = null;
        protected ControllerSingleSink threadController = null;
        private void buttonRun_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtNewFileLocation.Text.Length == 0)
                {
                    GetOutputLocation();
                }
                else
                {
                    if (this.txtNewFileName.Text.Length == 0)
                    {
                        MessageBox.Show("Please enter an Output filename", "MegaBook");

                        txtNewFileName.Focus();
                    }
                    else
                    {
                        string newFileName = txtNewFileLocation.Text + @"\" + txtNewFileName.Text;

                        if (newFileName.IndexOf(".") > 0)
                        {
                            if (Path.GetExtension(newFileName) != ".htm")
                            {
                                newFileName = newFileName.Replace(Path.GetExtension(newFileName), ".htm");
                            }
                        }
                        else
                        {
                            newFileName += ".htm";
                        }
                        if (null == threadController)
                        {
                            threadData = new ThreadXData();

                            /*long iTotal = 0;
                            int iLike = 1;
                            string sState = "";
                            string sStartsWith = "";
                            if (checkBoxStates.Checked)
                            {
                                sState = textBoxState.Text = dataGridViewStates.SelectedRows[0].Cells[0].Value.ToString().Replace("\"", "");
                            }
                            if (checkBoxLookingto.Checked)
                            {
                                if (dataGridViewLookingto.SelectedRows.Count > 0)
                                {
                                    sStartsWith = textBoxLookingTo.Text = dataGridViewLookingto.SelectedRows[0].Cells[0].Value.ToString();
                                    iLike = int.Parse(comboBoxLike.SelectedValue.ToString());
                                    if (iLike == 0)
                                    {
                                        this.leadCountTableAdapter.Fill(this.telemarketSQLDataSetStates.LeadCount, textBoxState.Text);
                                        long iStateTotal = long.Parse(dataGridViewCount.Rows[0].Cells[0].Value.ToString());
                                        long iDontWant = long.Parse(textBoxCount.Text);
                                        iTotal = iStateTotal - iDontWant;

                                    }
                                    else
                                    {

                                        iTotal = long.Parse(textBoxCount.Text);
                                    }
                                }

                            }
                            else
                            {
                                this.leadCountTableAdapter.Fill(this.telemarketSQLDataSetStates.LeadCount, textBoxState.Text);
                                if (dataGridViewStates.SelectedRows.Count > 0)
                                {
                                    iTotal = long.Parse(dataGridViewCount.Rows[0].Cells[0].Value.ToString());

                                }
                            }
                            threadData.State = sState;
                            threadData.StartsWith = sStartsWith;
                            threadData.Like = iLike;
                            if (checkBoxDates.Checked)
                            {
                                DateTime dtFrom = this.dateTimePickerFrom.Value;
                                DateTime dtTo = this.dateTimePickerTo.Value;
                                
                                threadData.From = dtFrom.ToString("d");
                                threadData.To = dtTo.ToString("d");
                            }
                            else
                            {
                                threadData.From = "";
                                threadData.To = "";
                            }
                            
                            threadData.Areacode = maskedTextBoxAreacode.Text;
                            threadData.Total = iTotal;*/
                            threadData.NewFileName = newFileName;
                            string sFrom = "";
                            string sTo = "";
                            string sState = "";
                            string sCity = "";
                            string sZip = "";
                            string sAreacode = "";

                            int iLike = 1;
                            string sLookingTo = "";

                            if (checkBoxDates.Checked)
                            {

                                DateTime dtFrom, dtTo;

                                dtFrom = new DateTime(dateTimePickerFrom.Value.Year, dateTimePickerFrom.Value.Month, dateTimePickerFrom.Value.Day, 0, 0, 0);
                                dtTo = new DateTime(dateTimePickerTo.Value.Year, dateTimePickerTo.Value.Month, dateTimePickerTo.Value.Day, 0, 0, 0);

                                sFrom = dtFrom.ToShortDateString();
                                sTo = dtTo.ToShortDateString();

                            }
                            if (checkBoxStates.Checked)
                            {
                                sState = textBoxState.Text;
                            }

                            if (checkBoxCity.Checked)
                            {
                                sCity = textBoxCity.Text;
                            }
                            if (checkBoxAreacode.Checked)
                            {
                                sAreacode = maskedTextBoxAreacode.Text;
                            }
                            if (checkBoxZip.Checked)
                            {
                                sZip = textBoxZip.Text;
                            }
                            if (checkBoxLookingto.Checked)
                            {
                                sLookingTo = textBoxLookingTo.Text;
                                iLike = int.Parse(comboBoxLike.SelectedValue.ToString());
                            }
                            if (maskedTextBoxMaxPages.Text.Length > 0)
                            {
                                threadData.MaxPages = int.Parse(maskedTextBoxMaxPages.Text);
                            }
                            else
                            {
                                threadData.MaxPages = 0;
                            }
                            threadData.State = sState;
                            threadData.Areacode = sAreacode;
                            threadData.Zip = sZip;
                            threadData.City = sCity;
                            threadData.From = sFrom;
                            threadData.To = sTo;
                            threadData.LookingTo = sLookingTo;
                            threadData.Like = iLike;

                            threadController = new ControllerSingleSink(new ThreadX(threadData));
                            threadController.Start();

                            /* start timer ticks */
                            timerGetData.Enabled = true;

                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error:buttonRun_Click " + ex.Message);
            }
        }
        private void CheckKeyState()
        {
            try
            {
                if (Control.IsKeyLocked(Keys.Scroll))
                {
                    toolStripStatusLabelScrollLock.Text = "SCRLK";
                }
                else
                {
                    toolStripStatusLabelScrollLock.Text = "       ";
                }
                if (System.Console.CapsLock)
                {
                    toolStripStatusLabelCapLock.Text = "CAP";
                }
                else
                {
                    toolStripStatusLabelCapLock.Text = "     ";
                }
                if (System.Console.NumberLock)
                {
                    toolStripStatusLabelNumLock.Text = "NUM";
                }
                else
                {
                    toolStripStatusLabelNumLock.Text = "     ";
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error:CheckKeyState() " + ex.Message);
            }
        }
        private void timerGetData_Tick(object sender, EventArgs e)
        {
            //try
            //{
                if (null != threadData)
                {
                    textStatus.Text = threadData.Message;
                    /* If you don't want to keep the reference
                    * to the thread class, you can also retrieve and downcast:
                    textStatus.Text = (ThreadX)(threadConroller.GetThread()).data.Message;
                    */


                    toolStripProgressBarProcess.Value = threadData.Complete;
                    
                    

                    textOutput.Text = threadData.Report;

                    if (StatusTickArgs.States.Running != threadController.LastStatus.State)
                    {
                        threadController = null;
                        threadData = null;
                        
                    }
                }
            //}
            //catch (System.Exception ex)
            //{
                //MessageBox.Show("Timer failed:" + ex.Message);
            //}
            //return;       
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            try
            {
                if (null != threadController)
                {

                    timerGetData.Enabled = false;

                    /* Stop thread */
                    if (threadController.Terminate(1000))
                    {
                        textOutput.Text = threadData.Report;
                    }

                    /* Note that the references get thrown away here 
                     * even if the thread refused to die.  If it rejected
                     * all attempts to stop, we will just abandon the thread.
                     * If this happens only because the wait time was not long
                     * enough and the thread will eventually stop, this may
                     * be acceptable behavior.  On the other hand, if the thread actually is
                     * still alive and kicking, then your application may require different
                     * behavior here.
                     */
                    threadController = null;

                    /* Because the owner constructed and kept a reference
                    to the data, it can still use the results if needed. 
                    In this case, we're done with it. */
                    threadData = null;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Stop failed:" + ex.Message);
            }
            
        }

        private void FormMain_Activated(object sender, EventArgs e)
        {
            CheckKeyState();
        }

        private void dateTimePickerBetween_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                //dateTimePickerFrom
                //SetFiltersGrid();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error:dateTimePickerBetween_ValueChanged" + ex.Message);
            }
        }

        private void dateTimePickerBetween_CloseUp(object sender, EventArgs e)
        {
            try
            {

                SetFiltersGrid();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error:dateTimePickerBetween_CloseUp" + ex.Message);
            }
        }
        private void GetOutputLocation()
        {
            try
            {
                System.Windows.Forms.FolderBrowserDialog dlg = new FolderBrowserDialog();
                dlg.ShowNewFolderButton = true;

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    txtNewFileLocation.Text = dlg.SelectedPath;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error:btnBrowseFolder_Click() " + ex.Message);
            }
        }
        private void btnBrowseFolder_Click(object sender, EventArgs e)
        {
            GetOutputLocation();
        }

        private void maskedTextBoxAreacode_TextChanged(object sender, EventArgs e)
        {
            //if (((TextBox)sender).Text.Length == 3)
            //{
                //SetFiltersGrid();
                
            //}
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            this.lookingToTypesByDateTableAdapter.Fill(this.telemarketSQLDataSetStates.LookingToTypesByDate, textBoxState.Text, dateTimePickerFrom.Value, dateTimePickerTo.Value, maskedTextBoxAreacode.Text);
        }

        private void dataGridViewStates_DoubleClick(object sender, EventArgs e)
        {
            dataGridViewFilters_DoubleClick(dataGridViewStates, textBoxState);
            
        }

        private void buttonClearStates_Click(object sender, EventArgs e)
        {
            textBoxState.Text = "";
        }

        private void checkBoxAreacode_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                panelAreacodes.Enabled = ((CheckBox)sender).Checked;
                dataGridViewAreacodes.Enabled = ((CheckBox)sender).Checked;

                if (((CheckBox)sender).Checked)
                {
                    dataGridViewAreacodes.DataSource = areacodesBindingSource;
                }
                else
                {
                    dataGridViewAreacodes.ClearSelection();
                    dataGridViewAreacodes.DataSource = null;

                }
                SetFiltersGrid();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error:checkBoxAreacodes_CheckedChanged" + ex.Message);
            }
        }
        private void dataGridViewFilters_DoubleClick(DataGridView aView, TextBox aTBox)
        {
            if (aView.SelectedRows.Count > 0)
            {
                string sItem = aView.SelectedRows[0].Cells[0].Value.ToString().Trim();
                string sItems = aTBox.Text;
                if (!sItems.Contains(sItem))
                {
                    if (sItems.Length > 0)
                    {
                        sItems = sItems + ",";
                    }
                    sItems = sItems + sItem;
                    aTBox.Text = sItems;
                }
            }
        }
        private void dataGridViewAreacodes_DoubleClick(object sender, EventArgs e)
        {
            dataGridViewFilters_DoubleClick(dataGridViewAreacodes, maskedTextBoxAreacode);
            
        }

        private void buttonClearAreacodes_Click(object sender, EventArgs e)
        {
            maskedTextBoxAreacode.Text = "";
        }

        private void dataGridViewCitys_DoubleClick(object sender, EventArgs e)
        {
            dataGridViewFilters_DoubleClick(dataGridViewCitys, textBoxCity);
        }

        private void checkBoxCity_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                panelCitys.Enabled = ((CheckBox)sender).Checked;
                dataGridViewCitys.Enabled = ((CheckBox)sender).Checked;

                if (((CheckBox)sender).Checked)
                {
                    dataGridViewCitys.DataSource = citysBindingSource;
                }
                else
                {
                    dataGridViewCitys.ClearSelection();
                    dataGridViewCitys.DataSource = null;

                }
                SetFiltersGrid();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error:checkBoxAreacodes_CheckedChanged" + ex.Message);
            }
        }

        private void checkBoxZip_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                panelZip.Enabled = ((CheckBox)sender).Checked;
                dataGridViewZips.Enabled = ((CheckBox)sender).Checked;

                if (((CheckBox)sender).Checked)
                {
                    dataGridViewZips.DataSource = zipsBindingSource;
                }
                else
                {
                    dataGridViewZips.ClearSelection();
                    dataGridViewZips.DataSource = null;

                }
                SetFiltersGrid();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error:checkBoxAreacodes_CheckedChanged" + ex.Message);
            }
        }

        private void dataGridViewZips_DoubleClick(object sender, EventArgs e)
        {
            dataGridViewFilters_DoubleClick(dataGridViewZips, textBoxZip);
        }

        private void dataGridViewLookingto_DoubleClick(object sender, EventArgs e)
        {
            dataGridViewFilters_DoubleClick(dataGridViewLookingto, textBoxLookingTo);
        }

        private void buttonClearCity_Click(object sender, EventArgs e)
        {
            textBoxCity.Text = "";
        }

        private void buttonClearZip_Click(object sender, EventArgs e)
        {
            textBoxZip.Text = "";
        }

        private void buttonClearLookingto_Click(object sender, EventArgs e)
        {
            textBoxLookingTo.Text = "";
        }

        

        

        

        
    }
}