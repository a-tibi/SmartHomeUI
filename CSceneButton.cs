using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Ground_Floor
{
    class CSceneButton
    {
        private PictureBox _SceneButton;
        private List<Image> _lstImages;
        private int _iTag;
        private int _iValue;
        private int _iDefaultValue;
        private Panel _pnl;
        public CZones LinkedZone;

        private CConnectionManager _cmServer;

        public CSceneButton(int width, int height, int x, int y, Image imgScene_u, Image imgScene1, Image imgScene2, Image imgScene3, Image imgScene4, Panel pnl, int tag, int iDefaultValue, CZones zoneToBeLinked, CConnectionManager cmServer)
        {
            //Server 
            _cmServer = cmServer;

            LinkedZone = zoneToBeLinked;

            // Tag & Value
            _iValue         = iDefaultValue;
            _iDefaultValue  = iDefaultValue;
            _iTag           = tag;

            _pnl            = pnl;

            Size buttonSize = new Size(width, height);

            _lstImages = new List<Image>();
            _lstImages.Add(imgScene_u);
            _lstImages.Add(imgScene1);
            _lstImages.Add(imgScene2);
            _lstImages.Add(imgScene3);
            _lstImages.Add(imgScene4);

            _SceneButton = new PictureBox();
            _SceneButton.Size = buttonSize;
            _SceneButton.Location = new Point(x, y);
            _SceneButton.Tag = _iTag;
            _SceneButton.MouseDown += new MouseEventHandler(_SceneButton_MouseDown);
            _SceneButton.MouseUp += new MouseEventHandler(_SceneButton_MouseUp);
            changeStatus(iDefaultValue);

            // Add the componenets to the Panel
            pnl.Controls.Add(_SceneButton);
        }

        private void _SceneButton_MouseDown(object sender, MouseEventArgs e)
        {
            int[] aryMinRanges = new int[4] { 0 , 80 , 157, 235 };
            int[] aryMaxRanges = new int[4] { 66, 146, 223, 301 };
            int iMousePosition = Form.MousePosition.X - (_SceneButton.Left + _pnl.Left + _pnl.Parent.Left);

            for (int iStatus = 0; iStatus < 4; iStatus++)
            {
                if ((iMousePosition > aryMinRanges[iStatus]) && (iMousePosition < aryMaxRanges[iStatus]))
                {
                    changeStatus(++iStatus);      //  0->Scene1 , 1->Scene2, 2->Scene3, 3->Scene4 
                }
            }
        }

        private void changeStatus(int iStatus)
        {
            _iValue = iStatus;
            _SceneButton.Image = _lstImages[iStatus];
        }

        private int getValue()
        {
            return _iValue;
        }

        private void _SceneButton_MouseUp(object sender, MouseEventArgs e)
        {
            //  Value
            _iValue = getValue();

            for (int i = 0; i < LinkedZone.lstServerZones.Count; i++)
            {
                CMessage msgToSend = new CMessage();
                msgToSend.SetMessage(CMessageConstants.CMD_COMM_RECALL_SCENE, CMessageConstants.CMD_TYPE_COMMUNICATION, 0);

                msgToSend.AddIntPrm(Convert.ToInt32(LinkedZone.lstServerZones[i]));    //  Append Zone No related to the specific Zone 

                msgToSend.AddIntPrm(_iValue);   //  Append Scene No.                                   

                _cmServer.SendMessage(msgToSend);
            }

            //  Appearance :: Return to the defaults 
            changeStatus(_iDefaultValue);
        }
    }
}
