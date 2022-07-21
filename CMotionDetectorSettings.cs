using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Ground_Floor
{
    class CMotionDetectorSettings
    {
        // Const
        private const int MILISECOND_MINUTES_RATE = 60000;

        //Settings        
        private const int MSENSOR_INITIAL_MARGIN_TOP            = 0;           // it was 9 // Top Margin for Enable & Conifg (ComboBox)
        private const int MSENSOR_CONFIG_INITIAL_MARGIN_TOP     = 0;    // Top Margin for Label
        private const int MSENSOR_SYMBOL_ENABLE_WIDTH           = 170;
        private const int MSENSOR_SYMBOL_BUTTON_HEIGHT          = 66;
        private const int MSENSOR_SYMBOL_PANEL_HEIGHT           = 96;
        private const int MSENSOR_CONFIG_PANEL_HEIGHT           = 164;
        private const string MSENSOR_CONFIG_DELAY               = "1;3;5;10;15;30";
        private const string MSENSOR_CONFIG_HOUR                = "0;1;2;3;4;5;6;7;8;9;10;11;12;13;14;15;16;17;18;19;20;21;22;23";
        private const string MSENSOR_CONFIG_MINUTE              = "0;10;15;20;30;40;45;50";
        private const int MSENSOR_CONFIG_LIST_WIDTH             = 96;
        private const int MSENSOR_CONFIG_LIST_COLOR             = 2;
        private const int MSENSOR_CONFIG_FONT_SIZE              = 14;
        private const string MSENSOR_CONFIG_DELAY_TEXT          = "Delay";
        private const string MSENSOR_CONFIG_FROM_TEXT           = "From";
        private const string MSENSOR_CONFIG_TO_TEXT             = "To";
        private const string MSENSOR_CONFIG_DOT_TEXT            = ":";
        private const string MSENSOR_CONFIG_SAVE                = "Save";
        private const int MSENSOR_CONFIG_SAVE_WIDTH             = 91;
        private const int MSENSOR_CONFIG_SAVE_HEIGHT            = 76;
        private const int MSENSOR_CONFIG_SAVE_FONT_SIZE         = 20;
        private const int MSENSOR_CONFIG_COLUMN1_LEFT           = 0;      //it was 9
        private const int MSENSOR_CONFIG_COLUMN2_LEFT           = 64;     //it was 72
        private const int MSENSOR_CONFIG_COLUMN3_LEFT           = 171;    //it was 176
        private const int MSENSOR_CONFIG_COLUMN4_LEFT           = 192;    //it was 198
        private const int MSENSOR_CONFIG_COLUMN5_LEFT           = 310;    //it was 308
        private const int MSENSOR_CONFIG_COLUMN6_LEFT           = 380;    //it was ?
        private const int MSENSOR_CONFIG_ROWS_OFFSET            = 54;


        private CToggleButton _cEnableDisableButton;
        private CPushButton _cEnableButton;
        private CPushButton _cDisableButton;
        private CToggleButton _cConfigButton;
        private Panel _pnlEnable;
        private Panel _pnlConfig;
        private Label _lblDelay;
        private Label _lblFrom;
        private Label _lblTo;
        private Label _lblFromDot;
        private Label _lblToDot;
        private ComboBox _cmbDelay;
        private ComboBox _cmbFromHour;
        private ComboBox _cmbFromMinute;
        private ComboBox _cmbToHour;
        private ComboBox _cmbToMinute;
        private Button _btnSave;

        private double _dSelectedDelay;
        private _strTIMESTRUCT _strTimeFrom, _strTimeTo;

        private int _iTag;
        private int _iValue;
        private int _iConfigStatus;

        public CDevices LinkedDevice;

        public CMotionDetectorSettings(Image imgEnable, Image imgDisable, Image imgConfig, Image imgDeConfig, Panel pnl, int tag, int iDefaultStatus, int iDefaultConfigStatus, double dDefaultDelay, UInt16 iDefaultFromHour, UInt16 iDefaultFromMinute, UInt16 iDefaultToHour, UInt16 iDefaultToMinute, List<Panel> lstPnlConfig, List<CToggleButton> lstTglConfig, CDevices deviceToBeLinked)
        {
            LinkedDevice = deviceToBeLinked;

            //Tag & Value
            _iTag = tag;
            _iValue = iDefaultStatus;
            _iConfigStatus = iDefaultConfigStatus;

            // Define Panels
            _pnlEnable = new Panel();
            _pnlEnable.Height = MSENSOR_SYMBOL_PANEL_HEIGHT;
            _pnlEnable.Dock = DockStyle.Top;
            _pnlEnable.BackColor = pnl.BackColor;

            _pnlConfig = new Panel();
            _pnlConfig.Tag = _iTag;
            _pnlConfig.Height = MSENSOR_CONFIG_PANEL_HEIGHT;
            _pnlConfig.Dock = DockStyle.Top;
            _pnlConfig.BackColor = pnl.BackColor;
            _pnlConfig.Visible = _getBooleanValue(_iConfigStatus);
            lstPnlConfig.Add(_pnlConfig);

            // Define Enable & Config Buttons
            if (_iTag == CGlobal.GLOBAL_ZONE)
            {
                //_cEnableButton  = new CPushButton(MSENSOR_SYMBOL_ENABLE_WIDTH, MSENSOR_SYMBOL_BUTTON_HEIGHT, MSENSOR_CONFIG_COLUMN1_LEFT, MSENSOR_INITIAL_MARGIN_TOP, imgEnable, imgEnable, _pnlEnable, CGlobal.GLOBAL_ZONE, 1, CGlobal.CATEGORY_MS);
                //_cDisableButton = new CPushButton(MSENSOR_SYMBOL_ENABLE_WIDTH, MSENSOR_SYMBOL_BUTTON_HEIGHT, MSENSOR_CONFIG_COLUMN4_LEFT, MSENSOR_INITIAL_MARGIN_TOP, imgDisable, imgDisable, _pnlEnable, CGlobal.GLOBAL_ZONE, 0, CGlobal.CATEGORY_MS);
            }
            else
            {
                _cEnableDisableButton = new CToggleButton(MSENSOR_SYMBOL_ENABLE_WIDTH, MSENSOR_SYMBOL_BUTTON_HEIGHT, MSENSOR_CONFIG_COLUMN1_LEFT, MSENSOR_INITIAL_MARGIN_TOP, imgEnable, imgDisable, _pnlEnable, _iTag, _iValue, LinkedDevice, CGlobal.CATEGORY_MS);
            }

            if (_iTag == CGlobal.GLOBAL_ZONE)
            {
                _cConfigButton = new CToggleButton(MSENSOR_SYMBOL_BUTTON_HEIGHT, MSENSOR_SYMBOL_BUTTON_HEIGHT, MSENSOR_CONFIG_COLUMN6_LEFT, MSENSOR_INITIAL_MARGIN_TOP, imgConfig, imgDeConfig, _pnlEnable, _iTag, _iConfigStatus, lstPnlConfig, lstTglConfig);
            }
            else
            {
                _cConfigButton = new CToggleButton(MSENSOR_SYMBOL_BUTTON_HEIGHT, MSENSOR_SYMBOL_BUTTON_HEIGHT, MSENSOR_CONFIG_COLUMN4_LEFT, MSENSOR_INITIAL_MARGIN_TOP, imgConfig, imgDeConfig, _pnlEnable, _iTag, _iConfigStatus, lstPnlConfig, lstTglConfig);
            }
            lstTglConfig.Add(_cConfigButton);

            // Define Config Components
            int iConfigLabelRow1    = MSENSOR_CONFIG_INITIAL_MARGIN_TOP;
            int iConfigLabelRow2    = MSENSOR_CONFIG_INITIAL_MARGIN_TOP + MSENSOR_CONFIG_ROWS_OFFSET;
            int iConfigLabelRow3    = MSENSOR_CONFIG_INITIAL_MARGIN_TOP + (2 * MSENSOR_CONFIG_ROWS_OFFSET);

            int iConfigComboBoxRow1 = MSENSOR_INITIAL_MARGIN_TOP;
            int iConfigComboBoxRow2 = MSENSOR_INITIAL_MARGIN_TOP + MSENSOR_CONFIG_ROWS_OFFSET;
            int iConfigComboBoxRow3 = MSENSOR_INITIAL_MARGIN_TOP + (2 * MSENSOR_CONFIG_ROWS_OFFSET);

            Font LabelFont = new Font("Tahoma", MSENSOR_CONFIG_FONT_SIZE, FontStyle.Regular);
            Color clrLabelColor = Color.White;

            _lblDelay = new Label();
            _lblDelay.Text = MSENSOR_CONFIG_DELAY_TEXT;
            _lblDelay.Font = LabelFont;
            _lblDelay.ForeColor = clrLabelColor;
            _lblDelay.Top = iConfigLabelRow1;
            _lblDelay.Left = MSENSOR_CONFIG_COLUMN1_LEFT;

            _lblFrom = new Label();
            _lblFrom.Text = MSENSOR_CONFIG_FROM_TEXT;
            _lblFrom.Font = LabelFont;
            _lblFrom.ForeColor = clrLabelColor;
            _lblFrom.Top = iConfigLabelRow2;
            _lblFrom.Left = MSENSOR_CONFIG_COLUMN1_LEFT;

            _lblTo = new Label();
            _lblTo.Text = MSENSOR_CONFIG_TO_TEXT;
            _lblTo.Font = LabelFont;
            _lblTo.ForeColor = clrLabelColor;
            _lblTo.Top = iConfigLabelRow3;
            _lblTo.Left = MSENSOR_CONFIG_COLUMN1_LEFT;

            _lblFromDot = new Label();
            _lblFromDot.Text = MSENSOR_CONFIG_DOT_TEXT;
            _lblFromDot.Font = LabelFont;
            _lblFromDot.ForeColor = clrLabelColor;
            _lblFromDot.Top = iConfigLabelRow2;
            _lblFromDot.Left = MSENSOR_CONFIG_COLUMN3_LEFT;

            _lblToDot = new Label();
            _lblToDot.Text = MSENSOR_CONFIG_DOT_TEXT;
            _lblToDot.Font = LabelFont;
            _lblToDot.ForeColor = clrLabelColor;
            _lblToDot.Top = iConfigLabelRow3;
            _lblToDot.Left = MSENSOR_CONFIG_COLUMN3_LEFT;

            if (_iTag == CGlobal.GLOBAL_ZONE)
            {
                _getDefaultValues(dDefaultDelay, iDefaultFromHour, iDefaultFromMinute, iDefaultToHour, iDefaultToMinute); // Save the default selected index from Default values
            }
            else
            {
                _getPLCValues(); // Save the default selected index from PLC values
            }

            _cmbDelay = new ComboBox();
            _cmbDelay.Top = iConfigComboBoxRow1;
            _cmbDelay.Left = MSENSOR_CONFIG_COLUMN2_LEFT;
            _cmbDelay.Width = MSENSOR_CONFIG_LIST_WIDTH;
            _fillComboBox(_cmbDelay, MSENSOR_CONFIG_DELAY, ';');
            _cmbDelay.SelectedIndex = _getSelectedIndexComboBox(_cmbDelay, _dSelectedDelay.ToString());

            _cmbFromHour = new ComboBox();
            _cmbFromHour.Top = iConfigComboBoxRow2;
            _cmbFromHour.Left = MSENSOR_CONFIG_COLUMN2_LEFT;
            _cmbFromHour.Width = MSENSOR_CONFIG_LIST_WIDTH;
            _fillComboBox(_cmbFromHour, MSENSOR_CONFIG_HOUR, ';');
            _cmbFromHour.SelectedIndex = _getSelectedIndexComboBox(_cmbFromHour, _strTimeFrom.wHour.ToString());

            _cmbFromMinute = new ComboBox();
            _cmbFromMinute.Top = iConfigComboBoxRow2;
            _cmbFromMinute.Left = MSENSOR_CONFIG_COLUMN4_LEFT;
            _cmbFromMinute.Width = MSENSOR_CONFIG_LIST_WIDTH;
            _fillComboBox(_cmbFromMinute, MSENSOR_CONFIG_MINUTE, ';');
            _cmbFromMinute.SelectedIndex = _getSelectedIndexComboBox(_cmbFromMinute, _strTimeFrom.wMinute.ToString());

            _cmbToHour = new ComboBox();
            _cmbToHour.Top = iConfigComboBoxRow3;
            _cmbToHour.Left = MSENSOR_CONFIG_COLUMN2_LEFT;
            _cmbToHour.Width = MSENSOR_CONFIG_LIST_WIDTH;
            _fillComboBox(_cmbToHour, MSENSOR_CONFIG_HOUR, ';');
            _cmbToHour.SelectedIndex = _getSelectedIndexComboBox(_cmbToHour, _strTimeTo.wHour.ToString());

            _cmbToMinute = new ComboBox();
            _cmbToMinute.Top = iConfigComboBoxRow3;
            _cmbToMinute.Left = MSENSOR_CONFIG_COLUMN4_LEFT;
            _cmbToMinute.Width = MSENSOR_CONFIG_LIST_WIDTH;
            _fillComboBox(_cmbToMinute, MSENSOR_CONFIG_MINUTE, ';');
            _cmbToMinute.SelectedIndex = _getSelectedIndexComboBox(_cmbToMinute, _strTimeTo.wMinute.ToString());

            Font SaveFont = new Font("Tahoma", MSENSOR_CONFIG_SAVE_FONT_SIZE, FontStyle.Regular);

            _btnSave = new Button();
            _btnSave.Text = MSENSOR_CONFIG_SAVE;
            _btnSave.Width = MSENSOR_CONFIG_SAVE_WIDTH;
            _btnSave.Height = MSENSOR_CONFIG_SAVE_HEIGHT;
            _btnSave.BackColor = Color.White;
            _btnSave.ForeColor = Color.FromArgb(9, 69, 123);
            _btnSave.Font = SaveFont;
            _btnSave.Left = MSENSOR_CONFIG_COLUMN5_LEFT;
            _btnSave.Top = iConfigComboBoxRow2;
            _btnSave.Click += new EventHandler(_btnSave_Click);

            // Add the comfiguration componenets to the Config Panel                        
            _pnlConfig.Controls.Add(_cmbDelay);
            _pnlConfig.Controls.Add(_cmbFromHour);
            _pnlConfig.Controls.Add(_cmbFromMinute);
            _pnlConfig.Controls.Add(_cmbToHour);
            _pnlConfig.Controls.Add(_cmbToMinute);
            _pnlConfig.Controls.Add(_btnSave);
            _pnlConfig.Controls.Add(_lblDelay);
            _pnlConfig.Controls.Add(_lblFrom);
            _pnlConfig.Controls.Add(_lblTo);
            _pnlConfig.Controls.Add(_lblFromDot);
            _pnlConfig.Controls.Add(_lblToDot);

            // Add the componenets to the Panel             
            pnl.Controls.Add(_pnlConfig);
            pnl.Controls.Add(_pnlEnable);
        }

        private void _btnSave_Click(object sender, EventArgs e)
        {
            // Appearance
            _cConfigButton.setConfigStatus(false);

            // Save values 
            _dSelectedDelay = Convert.ToDouble(_cmbDelay.SelectedItem) * MILISECOND_MINUTES_RATE;
            UInt32 uiPIRDelay = Convert.ToUInt32(_dSelectedDelay);

            _strTimeFrom.wHour = Convert.ToUInt16(_cmbFromHour.SelectedItem);
            _strTimeFrom.wMinute = Convert.ToUInt16(_cmbFromMinute.SelectedItem);
            _strTimeTo.wHour = Convert.ToUInt16(_cmbToHour.SelectedItem);
            _strTimeTo.wMinute = Convert.ToUInt16(_cmbToMinute.SelectedItem);

            // Save them into Twin Cat ...
            LinkedDevice.TCInterface.SetDWord(LinkedDevice.lstPrm[1] + ".bPIROnTime", uiPIRDelay);

            //  Save the bValue for all the OutputDevices which have been controlled via InputDevice (PIR)
            if (_iTag == CGlobal.GLOBAL_ZONE)
            {
                // ...
            }
            else
            {
                LinkedDevice.TCInterface.SetDWord(LinkedDevice.lstPrm[1] + ".bPIROnTime", uiPIRDelay);

                //  Save the bValue for all the OutputDevices which have been controlled via InputDevice (PIR)
                for (int i = 2; i < LinkedDevice.lstPrm.Count; i++)
                {
                    LinkedDevice.TCInterface.SetDateTime(LinkedDevice.lstPrm[i] + ".TimeScheduleForPIR.TimeON", _strTimeFrom);
                    LinkedDevice.TCInterface.SetDateTime(LinkedDevice.lstPrm[i] + ".TimeScheduleForPIR.TimeOFF", _strTimeTo);
                }
            }
        }



        private void _fillComboBox(ComboBox cmb, string strItems, char chrSpliter)
        {
            string[] saItems;
            saItems = strItems.Split(chrSpliter);

            foreach (string strItem in saItems)
            {
                cmb.Items.Add(strItem);
            }
        }

        private bool _getBooleanValue(int iValue)
        {
            bool bRet = iValue == 1 ? true : false;
            return bRet;
        }

        private void _getPLCValues()
        {
            UInt32 uiPIRDelay = LinkedDevice.TCInterface.GetDWord(LinkedDevice.lstPrm[1] + ".bPIROnTime");

            _dSelectedDelay = Convert.ToDouble(uiPIRDelay / MILISECOND_MINUTES_RATE); // Convert to minutes             

            //  Read the OutputDevice.bEnablePIR from the first Output Device in case there are more than one Output Device (because initialy they are all same),
            _strTimeFrom = LinkedDevice.TCInterface.GetDateTime(LinkedDevice.lstPrm[2] + ".TimeScheduleForPIR.TimeON");
            _strTimeTo = LinkedDevice.TCInterface.GetDateTime(LinkedDevice.lstPrm[2] + ".TimeScheduleForPIR.TimeOFF");

        }

        private void _getDefaultValues(double dDelay, UInt16 iFromHour, UInt16 iFromMinute, UInt16 iToHour, UInt16 iToMinute)
        {
            _dSelectedDelay = dDelay;

            _strTimeFrom.wHour = iFromHour;
            _strTimeFrom.wMinute = iFromMinute;
            _strTimeTo.wHour = iToHour;
            _strTimeTo.wMinute = iToMinute;
        }

        private int _getSelectedIndexComboBox(ComboBox cmb, string strSelectedItem)
        {
            int iSelectedIndex = 0;
            for (int i = 0; i < cmb.Items.Count; i++)
            {
                if (strSelectedItem == cmb.Items[i].ToString())
                {
                    iSelectedIndex = i;
                }
            }
            return iSelectedIndex;
        }
    }
}
