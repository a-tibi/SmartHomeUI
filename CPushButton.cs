using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Ground_Floor
{
    class CPushButton
    {
        private Image _DownImage;
        private Image _UpImage;
        private PictureBox _PushButton;
        private int _iCategory;
        private int _iTag;
        private int _iValue;
        private bool _bStatus;

        private CConnectionManager _cmServer;

        public CPushButton(int width, int height, int x, int y, Image imgUp, Image imgDown, Panel pnl, int tag, int iValue, int iCategory, CConnectionManager cmServer)
        {
            //Server 
            _cmServer = cmServer;

            // Category
            _iCategory = iCategory;

            // Tag & Value
            _iValue = iValue;
            _bStatus = _getBooleanValue(_iValue);
            _iTag = tag;

            _DownImage = imgDown;
            _UpImage = imgUp;
            _PushButton = new PictureBox();
            _PushButton.Size = new Size(width, height);
            _PushButton.Location = new Point(x, y);
            _PushButton.Image = _UpImage;
            _PushButton.Tag = tag;
            _PushButton.MouseDown += new MouseEventHandler(_PushButton_MouseDown);
            _PushButton.MouseUp += new MouseEventHandler(_PushButton_MouseUp);

            // Add the componenets to the Panel
            pnl.Controls.Add(_PushButton);
        }

        private void _PushButton_MouseDown(object sender, MouseEventArgs e)
        {
            _PushButton.Image = _DownImage;
        }

        private void _PushButton_MouseUp(object sender, MouseEventArgs e)
        {
            _PushButton.Image = _UpImage;
            switch (_iValue)
            {
                case CGlobal.CATEGORY_LIGHTING:
                    // _bStatus = true
                    CMessage msgToSend = new CMessage();
                    msgToSend.SetMessage(_bStatus ? CMessageConstants.CMD_GLOBAL_ALL_LIGHTS_SWITCHON : CMessageConstants.CMD_GLOBAL_ALL_LIGHTS_SWITCHOFF, CMessageConstants.CMD_TYPE_COMMUNICATION, 0);
                    msgToSend.AddIntPrm(CGlobal.GLOBAL_ZONE);
                    msgToSend.AddIntPrm(_iValue);

                    _cmServer.SendMessage(msgToSend);

                    break;
                case CGlobal.CATEGORY_MS:
                    if (_bStatus)
                    {
                        // _bStatus = true
                    }
                    else
                    {
                        // _bStatus = false
                    }
                    break;
            }
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
    }
}
