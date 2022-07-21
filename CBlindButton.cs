using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Ground_Floor
{
    class CBlindButton
    {
        private PictureBox _BlindButton;
        private List<Image> _lstImages;
        private int _iTag;
        private int _iValue;
        private Panel _pnl;
        public CDevices LinkedDevice;

        public CBlindButton(int width, int height, int x, int y, Image imgOpen, Image imgClose, Image imgStop, Panel pnl, int tag, int iDefaultValue,CDevices devToBeLinked)
        {
            LinkedDevice = devToBeLinked;

            // Tag & Value
            _iValue = iDefaultValue;            
            _iTag = tag;
            _pnl = pnl;

            Size buttonSize = new Size(width, height);
             
            _lstImages = new List<Image>();
            _lstImages.Add(imgStop);
            _lstImages.Add(imgClose);
            _lstImages.Add(imgOpen);

            _BlindButton = new PictureBox();
            _BlindButton.Size = buttonSize;
            _BlindButton.Location = new Point(x, y);
            _BlindButton.Tag = _iTag;
            _BlindButton.MouseDown +=new MouseEventHandler(_BlindButton_MouseDown);
            _BlindButton.MouseUp +=new MouseEventHandler(_BlindButton_MouseUp);
            changeStatus(iDefaultValue);

            // Add the componenets to the Panel
            pnl.Controls.Add(_BlindButton);
        }

        // Global
        public CBlindButton(int width, int height, int x, int y, Image imgOpen, Image imgClose, Image imgStop, Panel pnl, int tag, int iDefaultValue)
        {

            // Tag & Value
            _iValue = iDefaultValue;
            _iTag = tag;
            _pnl = pnl;

            Size buttonSize = new Size(width, height);

            _lstImages = new List<Image>();
            _lstImages.Add(imgStop);
            _lstImages.Add(imgClose);
            _lstImages.Add(imgOpen);

            _BlindButton = new PictureBox();
            _BlindButton.Size = buttonSize;
            _BlindButton.Location = new Point(x, y);
            _BlindButton.Tag = _iTag;
            _BlindButton.MouseDown += new MouseEventHandler(_BlindButton_MouseDown);
            _BlindButton.MouseUp += new MouseEventHandler(_GlobalButton_MouseUp);
            changeStatus(iDefaultValue);

            // Add the componenets to the Panel
            pnl.Controls.Add(_BlindButton);
        }

        private void _BlindButton_MouseDown(object sender, MouseEventArgs e)
        {
            int[] aryMinRanges = new int[3] {  89,  0, 176 };
            int[] aryMaxRanges = new int[3] { 155, 66, 241 };
            int iMousePosition = Form.MousePosition.X - (_BlindButton.Left + _pnl.Left + _pnl.Parent.Left);            

            for (int iStatus = 0; iStatus < 3; iStatus++)
            {
                if ((iMousePosition > aryMinRanges[iStatus]) && (iMousePosition < aryMaxRanges[iStatus]))
                {                    
                    changeStatus(iStatus);      //  0->Stop , 1->Open, 2->Close 
                }
            }
        }

        private void changeStatus(int iStatus)
        {
            _iValue = iStatus;
            _BlindButton.Image = _lstImages[iStatus];
        }

        private int getValue()
        {
            return _iValue;
        }

        private void _BlindButton_MouseUp(object sender, MouseEventArgs e)
        {
            _iValue = getValue();
            switch (_iValue)
            {
                case 0:     //  Stop
                    LinkedDevice.TCInterface.SetBool(LinkedDevice.lstPrm[3], true);
                    break;
                case 1:     //  Close
                    LinkedDevice.TCInterface.SetBool(LinkedDevice.lstPrm[2], true);
                    break;
                case 2:     //  Open
                    LinkedDevice.TCInterface.SetBool(LinkedDevice.lstPrm[1], true);
                    break;
            }

        }

        private void _GlobalButton_MouseUp(object sender, MouseEventArgs e)
        {
            _iValue = getValue();
            switch (_iValue)
            {
                case 0:     //  Stop
                    //
                    break;
                case 1:     //  Close
                    //
                    break;
                case 2:     //  Open
                    //
                    break;
            }

        }
    }
}
