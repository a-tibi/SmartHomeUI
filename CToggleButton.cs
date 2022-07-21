using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Ground_Floor
{
    class CToggleButton
    {
        private Image _OffImage;
        private Image _OnImage;
        private PictureBox _ToggleButton;
        private int _iTag;
        private int _iValue;
        private bool _bStatus;
        public CDevices LinkedDevice;

        private int _iCategory;
        private int _iZone;
        private CConnectionManager _cmServer;

        private List<Panel> _lstPnlConfig;
        private List<CToggleButton> _lstTglConfig;

        // For On/OFF Switch & MSensor (Enalbe/Disable)
        public CToggleButton(int width, int height, int x, int y, Image imgOn, Image imgOff, Panel pnl, int tag, int iDefaultStatus, CDevices deviceToBeLinked, int iCategory)
        {
            // Category
            _iCategory = iCategory;

            LinkedDevice = deviceToBeLinked;

            // Tag & Value
            _iValue = _getIntegerValue(_getPLCStatus());
            _bStatus = _getBooleanValue(_iValue);
            _iTag = tag;

            _OffImage = imgOff;
            _OnImage = imgOn;
            _ToggleButton = new PictureBox();
            _ToggleButton.Size = new Size(width, height);
            _ToggleButton.Location = new Point(x, y);
            _ToggleButton.Image = _bStatus ? imgOn : imgOff;
            _ToggleButton.Tag = tag;
            _ToggleButton.MouseDown += new MouseEventHandler(_ToggleButton_MouseDown);
            _ToggleButton.MouseUp += new MouseEventHandler(_ToggleButton_MouseUp);

            // Add the componenets to the Panel
            pnl.Controls.Add(_ToggleButton);
        }

        // For MSensor (Configure)
        public CToggleButton(int width, int height, int x, int y, Image imgOn, Image imgOff, Panel pnl, int tag, int iDefaultStatus, List<Panel> lstPnlConfig, List<CToggleButton> lstTglConfig)
        {
            // Tag & Value
            _iValue = iDefaultStatus;
            _bStatus = _getBooleanValue(_iValue);
            _iTag = tag;

            // List
            _lstPnlConfig = lstPnlConfig;
            _lstTglConfig = lstTglConfig;

            _OffImage = imgOff;
            _OnImage = imgOn;
            _ToggleButton = new PictureBox();
            _ToggleButton.Size = new Size(width, height);
            _ToggleButton.Location = new Point(x, y);
            _ToggleButton.Image = _bStatus ? imgOn : imgOff;
            _ToggleButton.Tag = tag;
            _ToggleButton.MouseDown += new MouseEventHandler(_ConfigButton_MouseDown);
            _ToggleButton.MouseUp += new MouseEventHandler(_ConfigButton_MouseUp);

            // Add the componenets to the Panel
            pnl.Controls.Add(_ToggleButton);
        }

        // For Modes
        public CToggleButton(int width, int height, int x, int y, Image imgOn, Image imgOff, Panel pnl, int tag, int iZone, CConnectionManager cmServer)
        {
            //Server 
            _cmServer = cmServer;

            // Category
            _iZone = iZone;

            // Tag & Value
            _iTag = tag;
            switch (_iTag)
            {
                case CGlobal.MODE_AWAY:
                    _iValue = _getIntegerValue(GetAwayMode());
                    break;
                default:
                    _iValue = _getIntegerValue(false);
                    break;
            }
            _bStatus = _getBooleanValue(_iValue);

            _OffImage = imgOff;
            _OnImage = imgOn;
            _ToggleButton = new PictureBox();
            _ToggleButton.Size = new Size(width, height);
            _ToggleButton.Location = new Point(x, y);
            _ToggleButton.Image = _bStatus ? imgOn : imgOff;
            _ToggleButton.Tag = tag;
            _ToggleButton.MouseDown += new MouseEventHandler(_ToggleButton_MouseDown);
            _ToggleButton.MouseUp += new MouseEventHandler(_ModeButton_MouseUp);

            // Add the componenets to the Panel
            pnl.Controls.Add(_ToggleButton);
        }

        // For Global
        public CToggleButton(int width, int height, int x, int y, Image imgOn, Image imgOff, Panel pnl, int iCategory)
        {
            // Category
            _iCategory = iCategory;

            // Tag & Value
            _iValue = _getIntegerValue(true);
            _bStatus = _getBooleanValue(_iValue);
            //_iTag = tag;            

            _OffImage = imgOff;
            _OnImage = imgOn;
            _ToggleButton = new PictureBox();
            _ToggleButton.Size = new Size(width, height);
            _ToggleButton.Location = new Point(x, y);
            _ToggleButton.Image = _bStatus ? imgOn : imgOff;
            //_ToggleButton.Tag = tag;
            _ToggleButton.MouseDown += new MouseEventHandler(_ToggleButton_MouseDown);
            _ToggleButton.MouseUp += new MouseEventHandler(_GlobalButton_MouseUp);

            // Add the componenets to the Panel
            pnl.Controls.Add(_ToggleButton);
        }

        private void _ToggleButton_MouseDown(object sender, MouseEventArgs e)
        {
            _setStatus(!_bStatus);
        }

        private void _ConfigButton_MouseDown(object sender, MouseEventArgs e)
        {
            setConfigStatus(!_bStatus);
        }

        private void _ToggleButton_MouseUp(object sender, MouseEventArgs e)
        {
            switch (_iCategory)
            {
                case CGlobal.CATEGORY_LIGHTING:
                    _iValue = _getIntegerValue(_bStatus);
                    if (_bStatus)
                    {
                        LinkedDevice.TCInterface.SetBool(LinkedDevice.lstPrm[1], true);
                    }
                    else
                    {
                        LinkedDevice.TCInterface.SetBool(LinkedDevice.lstPrm[2], true);
                    }
                    break;

                case CGlobal.CATEGORY_MS:
                    // Twincat manipulating ...
                    //_iValue = _getIntegerValue(_bStatus);                    

                    //  Save the bValue for all the OutputDevices which have been controlled via InputDevice (PIR)
                    for (int i = 2; i < LinkedDevice.lstPrm.Count; i++)
                    {
                        LinkedDevice.TCInterface.SetBool(LinkedDevice.lstPrm[i] + ".bEnablePIR", _bStatus);
                    }

                    break;
            }
        }        

        private bool GetAwayMode()
        {
            bool bRet = false;

            CMessage msgToSend = new CMessage();
            msgToSend.SetMessage(CMessageConstants.CMD_COMM_GET_AWAY_MODE, CMessageConstants.CMD_TYPE_COMMUNICATION, 0);
            _cmServer.SendMessage(msgToSend);

            CMessage MsgReply = _cmServer.ReceiveMessage();

            if ((MsgReply != null) && (MsgReply.Command_Type() == CMessageConstants.CMD_TYPE_COMMUNICATION) && (MsgReply.Command_Id() == CMessageConstants.CMD_COMM_GET_AWAY_MODE))
            {
                if (MsgReply.GetPrmInt(0) == 1) bRet = true;
            }
            else
            {
                Console.WriteLine("Invalide response received.");
            }

            return bRet;
        }

        private void _ModeButton_MouseUp(object sender, MouseEventArgs e)
        {
            _iValue = _getIntegerValue(_bStatus);
            switch (_iTag)
            {
                case CGlobal.MODE_AWAY:
                    CMessage msgToSend = new CMessage();
                    if (_bStatus)
                    {
                        // Active Mode
                        msgToSend.SetMessage(CMessageConstants.CMD_COMM_SET_AWAY_MODE, CMessageConstants.CMD_TYPE_COMMUNICATION, 0);
                        msgToSend.AddIntPrm(1);//If away mode is enabled

                        _cmServer.SendMessage(msgToSend);
                    }
                    else
                    {
                        // Deactive Mode
                        // Active Mode
                        msgToSend.SetMessage(CMessageConstants.CMD_COMM_SET_AWAY_MODE, CMessageConstants.CMD_TYPE_COMMUNICATION, 0);
                        msgToSend.AddIntPrm(0);//If away mode is disabled

                        _cmServer.SendMessage(msgToSend);
                    }
                    break;
            }
        }
        private void _GlobalButton_MouseUp(object sender, MouseEventArgs e)
        {
            switch (_iCategory)
            {
                case CGlobal.CATEGORY_LIGHTING:
                    _iValue = _getIntegerValue(_bStatus);
                    if (_bStatus)
                    {
                        // Global ON
                    }
                    else
                    {
                        // Global OFF
                    }
                    break;
                case CGlobal.CATEGORY_MS:
                    _iValue = _getIntegerValue(_bStatus);
                    if (_bStatus)
                    {
                        // Global Enable
                    }
                    else
                    {
                        // Global Disable
                    }
                    break;
            }
        }

        private void _ConfigButton_MouseUp(object sender, MouseEventArgs e)
        {
            // ...

        }

        private bool _getPLCStatus()
        {
            bool bRet = false;

            if (_iCategory == CGlobal.CATEGORY_LIGHTING)
            {
                if (LinkedDevice.TCInterface.GetShort(LinkedDevice.lstPrm[3] + ".iValue") > 0)
                    bRet = true;
            }
            else
            {
                //  Read the OutputDevice.bEnablePIR from the first Output Device in case there are more than one Output Device (because initialy they are all same),
                bRet = LinkedDevice.TCInterface.GetBool(LinkedDevice.lstPrm[2] + ".bEnablePIR");
            }

            return bRet;
        }

        private int _getIntegerValue(bool bValue)
        {
            int iRet = bValue ? 1 : 0;
            return iRet;
        }

        private bool _getBooleanValue(int iValue)
        {
            bool bRet = iValue == 1 ? true : false;
            return bRet;
        }

        private void _setStatus(bool bStatus)
        {
            _bStatus = bStatus;
            _ToggleButton.Image = _bStatus ? _OnImage : _OffImage;
        }

        private int getTag()
        {
            return _iTag;
        }

        public void setConfigStatus(bool bStatus)
        {
            // Symbol
            foreach (CToggleButton locToggle in _lstTglConfig)
            {
                if (locToggle.getTag() == _iTag)
                {
                    locToggle._setStatus(!_bStatus);
                }
                else
                {
                    locToggle._setStatus(false);
                }
            }

            // Panel
            foreach (Panel locPanel in _lstPnlConfig)
            {
                if (Convert.ToInt32(locPanel.Tag) == _iTag)
                {
                    locPanel.Visible = _bStatus;
                }
                else
                {
                    locPanel.Visible = false;
                }
            }
        }
    }
}
