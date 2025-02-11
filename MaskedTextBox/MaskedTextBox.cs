using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace MaskedTextBox
{
    public partial class MaskedTextBox : System.Windows.Forms.TextBox
    {
        private bool m_dateOnly;
        private bool m_phoneOnly;
        private bool m_IPAddrOnly;
        private bool m_ssn;
        private bool m_decimalOnly;
        private bool m_digitOnly;
        private int m_AreacodeOnly;
        private int digitPos = 0;
        private bool m_digitCSVOnly;

        private int DelimitNumber = 0;
        //private System.Windows.Forms.ErrorProvider errorProvider1;

        //private System.ComponentModel.Container components = null;
    
        public MaskedTextBox()
        {
            InitializeComponent();
        }

        public bool DateOnly
        {
            get { return m_dateOnly; }
            set { m_dateOnly = value; }      
        }
        public bool PhoneOnly
        {
            get { return m_phoneOnly; }
            set { m_phoneOnly = value; }
        }
        public bool IPAddrOnly
        {
            get { return m_IPAddrOnly; }
            set { m_IPAddrOnly = value; }
        }
        public bool SSNOnly
        {
            get { return m_ssn; }
            set { m_ssn = value; }
        }
        public bool DecimalOnly
        {
            get { return m_decimalOnly; }
            set { m_decimalOnly = value; }
        }
        public bool DigitOnly
        {
            get { return m_digitOnly; }
            set { m_digitOnly = value; }
        }
        public bool DigitCSVOnly
        {
            get { return m_digitCSVOnly; }
            set { m_digitCSVOnly = value; }
        }

        public int AreacodeOnly
        {
            get { return m_AreacodeOnly; }
            set { m_AreacodeOnly = value; }
        }

        private void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            MaskedTextBox sd = (MaskedTextBox)sender;
            if (sd.m_IPAddrOnly)
                sd.MaskIpAddr(e);
            if (sd.m_digitOnly)
                sd.MaskDigit(e);
            if (sd.m_ssn)
                sd.MaskPhoneSSN(e, 3, 2);
            if (sd.m_phoneOnly)
                sd.MaskPhoneSSN(e, 3, 3);
            if (sd.m_dateOnly)
                sd.MaskDate(e);
            if (sd.m_decimalOnly)
                sd.MaskDecimal(e);
            if (sd.m_digitCSVOnly)
                sd.MaskCSVDigits(e);
            
        }

        
        private void MaskDigit(KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == 8)
            {
                errorProvider1.SetError(this, "");
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
                errorProvider1.SetError(this, "Only valid for Digit");
            }
        }
        private void MaskDecimal(KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == '.' || e.KeyChar == 8)
            {
                e.Handled = false;
                errorProvider1.SetError(this, "");
            }
            else
            {
                e.Handled = true;
                errorProvider1.SetError(this, "Only valid for Digit and dot");
            }
        }
        private void MaskCSVDigits(KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == ',' || e.KeyChar == 8)
            {
                e.Handled = false;
                errorProvider1.SetError(this, "");
            }
            else
            {
                e.Handled = true;
                errorProvider1.SetError(this, "Only valid for Digit and comma");
            }
        }
        private void MaskDate(KeyPressEventArgs e)
        {
            int len = this.Text.Length;
            int indx = this.Text.LastIndexOf("/");
            // if test is highlighted reset vars
            if (this.SelectedText == this.Text)
            {
                indx = -1;
                digitPos = 0;
                DelimitNumber = 0;
                this.Text = null;
            }
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == '/' || e.KeyChar == 8)
            {
                string tmp = this.Text;
                if (e.KeyChar != 8)
                {
                    if (e.KeyChar != '/')
                    {
                        if (indx > 0)
                            digitPos = len - indx;
                        else
                            digitPos++;
                        if (digitPos == 3 && DelimitNumber < 2)
                        {
                            if (e.KeyChar != '/')
                            {
                                DelimitNumber++;
                                this.AppendText("/");
                            }
                        }

                        errorProvider1.SetError(this, "");
                        if ((digitPos == 2 || (Int32.Parse(e.KeyChar.ToString()) > 1 && DelimitNumber == 0)))
                        {
                            string tmp2;
                            if (indx == -1)
                                tmp2 = e.KeyChar.ToString();
                            else
                                tmp2 = this.Text.Substring(indx + 1) + e.KeyChar.ToString();

                            if (DelimitNumber < 2)
                            {
                                if (digitPos == 1) this.AppendText("0");
                                this.AppendText(e.KeyChar.ToString());
                                if (indx < 0)
                                {
                                    if (Int32.Parse(this.Text) > 12) // check validation
                                    {
                                        string str;
                                        str = this.Text.Insert(0, "0");
                                        if (Int32.Parse(this.Text) > 13)
                                        {
                                            this.Text = str.Insert(2, "/0");
                                            DelimitNumber++;
                                            this.AppendText("/");
                                        }
                                        else
                                        {
                                            this.Text = str.Insert(2, "/");
                                            this.AppendText("");
                                        }
                                        DelimitNumber++;
                                    }
                                    else
                                    {
                                        this.AppendText("/");
                                        DelimitNumber++;
                                    }
                                    e.Handled = true;
                                }
                                else
                                {
                                    if (DelimitNumber == 1)
                                    {
                                        int m = Int32.Parse(this.Text.Substring(0, indx));
                                        if (!CheckDayOfMonth(m, Int32.Parse(tmp2)))
                                            errorProvider1.SetError(this, "Make sure this month have the day");
                                        else
                                        {
                                            this.AppendText("/");
                                            DelimitNumber++;
                                            e.Handled = true;
                                        }
                                    }
                                }
                            }
                        }
                        else if (digitPos == 1 && Int32.Parse(e.KeyChar.ToString()) > 3 && DelimitNumber < 2)
                        {
                            if (digitPos == 1) this.AppendText("0");
                            this.AppendText(e.KeyChar.ToString());
                            this.AppendText("/");
                            DelimitNumber++;
                            e.Handled = true;
                        }
                        else
                        {
                            if (digitPos == 1 && DelimitNumber == 2 && e.KeyChar > '2')
                                errorProvider1.SetError(this, "The year should start with 1 or 2");
                        }
                    }
                    else
                    {
                        DelimitNumber++;
                        string tmp3;
                        if (indx == -1)
                            tmp3 = this.Text.Substring(indx + 1);
                        else
                            tmp3 = this.Text;
                        if (digitPos == 1)
                        {
                            this.Text = tmp3.Insert(indx + 1, "0"); ;
                            this.AppendText("/");
                            e.Handled = true;
                        }
                    }
                }
                else
                {
                    e.Handled = false;
                    if ((len - indx) == 1)
                    {
                        DelimitNumber--;
                        if (indx > -1)
                            digitPos = 2;
                        else
                            digitPos--;
                    }
                    else
                    {
                        if (indx > -1)
                            digitPos = len - indx - 1;
                        else
                            digitPos = len - 1;
                    }
                }
            }
            else
            {
                e.Handled = true;
                errorProvider1.SetError(this, "Only valid for Digit and /");
            }
        }
        private void MaskPhoneSSN(KeyPressEventArgs e, int pos, int pos2)
        {
            int len = this.Text.Length;
            int indx = this.Text.LastIndexOf("-");
            // if test is highlighted reset vars
            if (this.SelectedText == this.Text)
            {
                indx = -1;
                digitPos = 0;
                DelimitNumber = 0;
            }
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == '-' || e.KeyChar == 8)
            { // only digit, Backspace and - are accepted
                string tmp = this.Text;
                if (e.KeyChar != 8)
                {
                    errorProvider1.SetError(this, "");
                    if (e.KeyChar != '-')
                    {
                        if (indx > 0)
                            digitPos = len - indx;
                        else
                            digitPos++;
                    }
                    if (indx > -1 && digitPos == pos2 && DelimitNumber == 1)
                    {
                        if (e.KeyChar != '-')
                        {
                            this.AppendText(e.KeyChar.ToString());
                            this.AppendText("-");
                            e.Handled = true;
                            DelimitNumber++;
                        }
                    }
                    else if (digitPos == pos && DelimitNumber == 0)
                    {
                        if (e.KeyChar != '-')
                        {
                            this.AppendText(e.KeyChar.ToString());
                            this.AppendText("-");
                            e.Handled = true;
                            DelimitNumber++;
                        }
                    }
                }
                else
                {
                    e.Handled = false;
                    if ((len - indx) == 1)
                    {
                        DelimitNumber--;
                        if ((indx) > -1)
                            digitPos = len - indx;
                        else
                            digitPos--;
                    }
                    else
                    {
                        if (indx > -1)
                            digitPos = len - indx - 1;
                        else
                            digitPos = len - 1;
                    }
                }
            }
            else
            {
                e.Handled = true;
                errorProvider1.SetError(this, "Only valid for Digit and -");
            }
        }
        private void MaskIpAddr(KeyPressEventArgs e)
        {
            int len = this.Text.Length;
            int indx = this.Text.LastIndexOf(".");
            // if test is highlighted reset vars
            if (this.SelectedText == this.Text)
            {
                indx = -1;
                digitPos = 0;
                DelimitNumber = 0;
            }
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == '.' || e.KeyChar == 8)
            { // only digit, Backspace and dot are accepted
                string tmp = this.Text;
                errorProvider1.SetError(this, "");
                if (e.KeyChar != 8)
                {
                    if (e.KeyChar != '.')
                    {
                        if (indx > 0)
                            digitPos = len - indx;
                        else
                            digitPos++;
                    }
                    if (digitPos == 3 && e.KeyChar != '.')
                    {
                        string tmp2 = this.Text.Substring(indx + 1) + e.KeyChar;
                        if (Int32.Parse(tmp2) > 255) // check validation
                            errorProvider1.SetError(this, "The number can't be bigger than 255");
                        else
                        {
                            if (DelimitNumber < 3)
                            {
                                this.AppendText(e.KeyChar.ToString());
                                this.AppendText(".");
                                DelimitNumber++;
                                e.Handled = true;
                            }
                        }
                    }
                    else if (digitPos == 4 && DelimitNumber < 3)
                        this.AppendText(".");
                }
                else
                {
                    e.Handled = false;
                    if ((len - indx) == 1)
                    {
                        DelimitNumber--;
                        if (indx > -1)
                        {
                            digitPos = len - indx;
                        }
                        else
                            digitPos--;
                    }
                    else
                    {
                        if (indx > -1)
                            digitPos = len - indx - 1;
                        else
                            digitPos = len - 1;
                    }
                }
            }
            else
            {
                e.Handled = true;
                errorProvider1.SetError(this, "Only valid for Digit abd dot");
            }
        }
        private bool CheckDayOfMonth(int mon, int day)
        {
            bool ret = true;
            if (day == 0) ret = false;
            switch (mon)
            {
                case 1:
                    if (day > 31)
                        ret = false;
                    break;
                case 2:
                    if (day > 28)
                        ret = false;
                    break;
                case 3:
                    if (day > 31)
                        ret = false;
                    break;
                case 4:
                    if (day > 30)
                        ret = false;
                    break;
                case 5:
                    if (day > 31)
                        ret = false;
                    break;
                case 6:
                    if (day > 30)
                        ret = false;
                    break;
                case 7:
                    if (day > 31)
                        ret = false;
                    break;
                case 8:
                    if (day > 31)
                        ret = false;
                    break;
                case 9:
                    if (day > 30)
                        ret = false;
                    break;
                case 10:
                    if (day > 31)
                        ret = false;
                    break;
                case 11:
                    if (day > 30)
                        ret = false;
                    break;
                case 12:
                    if (day > 31)
                        ret = false;
                    break;
                default:
                    ret = false;
                    break;
            }
            return ret;
        }
    }
}