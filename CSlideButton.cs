using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Ground_Floor
{
    class CSlideButton
    {
        //private Size      _Size;
        //private Point     _Location;      
        //private Point     _SliderLocation;       
        private PictureBox _OffImage;
        private PictureBox _OnImage;
        private PictureBox _Background;
        private PictureBox _Slider;
        private int _iTag;
        private int _iCurX;
        bool _bDrag;
        private int _iMaxPos;
        private int _iMinPos;
        private int _iValue;

        public CDevices LinkedDevice;

        public CSlideButton(int width, int height, int buttonWidth, int x, int y, Image imgBackground, Image imgSlide, Image imgOn, Image imgOff , Panel pnl, int tag, int iDefaultPositionPerCent,CDevices deviceToBeLinked)
        {
            LinkedDevice = deviceToBeLinked;
            _Background = new PictureBox();
            _Background.Image = imgBackground;
            _Background.Size = new Size(width, height);            
            _Background.Location = new Point(x + buttonWidth, y);

            Size buttonSize = new Size(buttonWidth, height);

            _iValue = GetPLCStatus();

            _Slider = new PictureBox();
            _Slider.Image = imgSlide;
            _Slider.Size = buttonSize;
            int iPosition = _getXPosition(_iValue);
            _Slider.Location = new Point(x + buttonWidth + iPosition, y);
            _Slider.MouseMove += new MouseEventHandler(_Slider_MouseMove);
            _Slider.MouseUp += new MouseEventHandler(_Slider_MouseUp);
            _Slider.MouseDown += new MouseEventHandler(_Slider_MouseDown);            
            
            _OnImage = new PictureBox();
            _OnImage.Image = imgOn;
            _OnImage.Size = buttonSize;
            _OnImage.Location = new Point(x + width + buttonWidth, y);            
            _OnImage.MouseDown += new MouseEventHandler(_OnImage_MouseDown);
            _OnImage.MouseUp +=new MouseEventHandler(_OnImage_MouseUp);
            
            _OffImage = new PictureBox();
            _OffImage.Image = imgOff;
            _OffImage.Size = buttonSize;
            _OffImage.Location = new Point(x, y);    
            _OffImage.MouseDown += new MouseEventHandler(_OffImage_MouseDown);  
            _OffImage.MouseUp +=new MouseEventHandler(_OffImage_MouseUp);         

            //Initial the max & min positions
            _iMaxPos = _Background.Left + _Background.Width - _Slider.Width;
            _iMinPos = _Background.Left;

            // Tag & Value
            _iTag = tag;
            

            // Add the componenets to the Panel
            pnl.Controls.Add(_Slider);
            pnl.Controls.Add(_OffImage);
            pnl.Controls.Add(_OnImage);
            pnl.Controls.Add(_Background);
        }

        private int _getXPosition(int iPercentValue)
        {
            int iXPosition = (iPercentValue * (_Background.Width - _Slider.Width)) / 100;
            return iXPosition;
        }

        private int _getPercentValue(int iXPosition)
        {
            int iPercentValue = ((iXPosition - _Background.Left) * 100) / (_Background.Width - _Slider.Width);
            return iPercentValue;
        }

        private void _OnImage_MouseDown(object sender, MouseEventArgs e)
        {
            _Slider.Left = _iMaxPos;
        }

        private void _OffImage_MouseDown(object sender, MouseEventArgs e)
        {
            _Slider.Left = _iMinPos;
        }

        private void _Slider_MouseDown(object sender, MouseEventArgs e)
        {            
            _iCurX = Form.MousePosition.X - _Slider.Left;
            _bDrag = true;
        }

        private void _Slider_MouseMove(object sender, MouseEventArgs e)
        {            
            int iPosition;            
            if (_bDrag)
            {                
                iPosition = Form.MousePosition.X - _iCurX;                
                if ((iPosition >= _iMinPos) && (iPosition <= _iMaxPos))
                {
                    _Slider.Left = iPosition;                    
                }
            }            
        }

        private void _Slider_MouseUp(object sender, MouseEventArgs e)
        {            
            _bDrag = false;
            _iValue = _getPercentValue(_Slider.Left);
            UpdatePLC(_iValue);
        }

        private void _OnImage_MouseUp(object sender, MouseEventArgs e)
        {
            _iValue = _getPercentValue(_Slider.Left);
            UpdatePLC(_iValue);
        }

        private void _OffImage_MouseUp(object sender, MouseEventArgs e)
        {
            _iValue = _getPercentValue(_Slider.Left);
            UpdatePLC(_iValue);

        }

        private void UpdatePLC(int iValue)
        {
            LinkedDevice.TCInterface.SetShort(LinkedDevice.lstPrm[1], Convert.ToInt16(iValue*200));
            
        }

        private int GetPLCStatus()
        {
            return Convert.ToInt32(Convert.ToSingle(LinkedDevice.TCInterface.GetShort(LinkedDevice.lstPrm[2] + ".iValue"))/200);
        }
    }
}
