using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Ground_Floor
{
    public partial class Form1 : Form
    {
        //Interface to Server
        CConnectionManager Server;

        // Device Settings (Lightings & Curtains)
        CDeviceSettings cDeviceSettings;

        // PROPERTIES ...
        // Modes
        CToggleButton cMode1;

        // Slider       
        CSlideButton cSlider;

        // Toggle        
        CToggleButton cToggle;

        // Push Button
        CPushButton cPushButton;

        // Blinds        
        CBlindButton cBlind;

        // Motion Sensor 
        CMotionDetectorSettings cMsensor;

        // Scenes
        CSceneButton cScene;

        int[]       ACPackage                   = new int[4] { 9, 10, 11, 18 };
        string[]    ACSetTemp                   = new string[2] { CGlobal.INCREASE, CGlobal.DECREASE };
        int iBedroom23ZoneNo = 26;
        int iBedroom2ZoneNo = 22;
        int iStaircaseZoneNo = 27;
        int iCorridorZoneNo = 18;

        int[]       LightDevices                = new int[3] { CGlobal.ONOFF_DEVICE, CGlobal.DIMMING_DEVICE, CGlobal.SCENE_DEVICE };
        int[]       BlindDevices                = new int[1] { CGlobal.BLIND_DEVICE };
        int[]       MSensorDevices              = new int[1] { CGlobal.MSENSOR_DEVICE };

        int         _iFloorNo;
        int         _iZoneNo;
        int         _iCategoryNo;
        int         _iSettingsNo;

        List<PictureBox> lstPctFloorDown;

        int DefaultCategoryNo = CGlobal.CATEGORY_LIGHTING;                // by default the first category is for Lighting
        List<PictureBox> lstPctCategory;   
        List<PictureBox> lstPctCategoryDown;

        List<Panel> lstPnlFloor;        

        int[] DefaultZoneNo = new int[3] { 0, 8, 17 };   // by default the first zone is for the family room   
        List<PictureBox> lstPctListZone;
        List<PictureBox> lstPctListZoneDown;

        List<Button> lstBtnAcFan;   //  FCU Fan Speed

        // Global Settings
        List<PictureBox> lstPctSettings;
        List<PictureBox> lstPctSettingsDown;

        // CONSTRUCTURE 
        public Form1()
        {
            InitializeComponent();
        }

        // METHODS
        private void Form1_Load(object sender, EventArgs e)
        {                        
            // Load            

            // Load Device Settings ...
            cDeviceSettings = new CDeviceSettings("\\Hard Disk\\ftp\\XMLDevices.xml");
            cDeviceSettings.GetDeviceSettings();
            int DefaultFloorNo = cDeviceSettings.iDefaultFloorNo;   // by default the first panel is for First Floor (BF:0, GF:1, FF:2)

            // Categories
            lstPctCategory = new List<PictureBox>();
            lstPctCategory.Add(pctCat1);
            lstPctCategory.Add(pctCat2);
            lstPctCategory.Add(pctCat3);
            lstPctCategory.Add(pctCat4);
            lstPctCategory.Add(pctCat5);            

            lstPctCategoryDown = new List<PictureBox>();
            lstPctCategoryDown.Add(pctCat1_d);
            lstPctCategoryDown.Add(pctCat2_d);
            lstPctCategoryDown.Add(pctCat3_d);
            lstPctCategoryDown.Add(pctCat4_d);
            lstPctCategoryDown.Add(pctCat5_d);            

            // Floors           
            lstPctFloorDown = new List<PictureBox>();
            lstPctFloorDown.Add(pctBF_d);
            lstPctFloorDown.Add(pctGF_d);
            lstPctFloorDown.Add(pctFF_d);

            // Panels
            lstPnlFloor = new List<Panel>();
            lstPnlFloor.Add(pnlFloor0);
            lstPnlFloor.Add(pnlFloor1);
            lstPnlFloor.Add(pnlFloor2);

            //List2 Zones
            lstPctListZone = new List<PictureBox>();
            lstPctListZone.Add(pctList0Zone1);
            lstPctListZone.Add(pctList0Zone2);
            lstPctListZone.Add(pctList0Zone3);
            lstPctListZone.Add(pctList0Zone4);
            lstPctListZone.Add(pctList0Zone5);
            lstPctListZone.Add(pctList0Zone6);
            lstPctListZone.Add(pctList0Zone7);
            lstPctListZone.Add(pctList0Zone8);            
            lstPctListZone.Add(pctList1Zone1);
            lstPctListZone.Add(pctList1Zone2);
            lstPctListZone.Add(pctList1Zone3);
            lstPctListZone.Add(pctList1Zone4);
            lstPctListZone.Add(pctList1Zone5);
            lstPctListZone.Add(pctList1Zone6);
            lstPctListZone.Add(pctList1Zone7);
            lstPctListZone.Add(pctList1Zone8);
            lstPctListZone.Add(pctList1Zone9);
            lstPctListZone.Add(pctList2Zone1);
            lstPctListZone.Add(pctList2Zone2);
            lstPctListZone.Add(pctList2Zone3);
            lstPctListZone.Add(pctList2Zone4);
            lstPctListZone.Add(pctList2Zone5);
            lstPctListZone.Add(pctList2Zone6);
            lstPctListZone.Add(pctList2Zone7);
            lstPctListZone.Add(pctList2Zone8);
            lstPctListZone.Add(pctList2Zone9);
            lstPctListZone.Add(pctList2Zone67);
            lstPctListZone.Add(pctList2Zone10);
            lstPctListZone.Add(pctGlobal);

            lstPctListZoneDown = new List<PictureBox>();           
            lstPctListZoneDown.Add(pctList0Zone1_d);
            lstPctListZoneDown.Add(pctList0Zone2_d);
            lstPctListZoneDown.Add(pctList0Zone3_d);
            lstPctListZoneDown.Add(pctList0Zone4_d);
            lstPctListZoneDown.Add(pctList0Zone5_d);
            lstPctListZoneDown.Add(pctList0Zone6_d);
            lstPctListZoneDown.Add(pctList0Zone7_d);
            lstPctListZoneDown.Add(pctList0Zone8_d);            
            lstPctListZoneDown.Add(pctList1Zone1_d);
            lstPctListZoneDown.Add(pctList1Zone2_d);
            lstPctListZoneDown.Add(pctList1Zone3_d);
            lstPctListZoneDown.Add(pctList1Zone4_d);
            lstPctListZoneDown.Add(pctList1Zone5_d);
            lstPctListZoneDown.Add(pctList1Zone6_d);
            lstPctListZoneDown.Add(pctList1Zone7_d);
            lstPctListZoneDown.Add(pctList1Zone8_d);
            lstPctListZoneDown.Add(pctList1Zone9_d); 
            lstPctListZoneDown.Add(pctList2Zone1_d);
            lstPctListZoneDown.Add(pctList2Zone2_d);
            lstPctListZoneDown.Add(pctList2Zone3_d);
            lstPctListZoneDown.Add(pctList2Zone4_d);
            lstPctListZoneDown.Add(pctList2Zone5_d);
            lstPctListZoneDown.Add(pctList2Zone6_d);
            lstPctListZoneDown.Add(pctList2Zone7_d);
            lstPctListZoneDown.Add(pctList2Zone8_d);
            lstPctListZoneDown.Add(pctList2Zone9_d);
            lstPctListZoneDown.Add(pctList2Zone67_d);
            lstPctListZoneDown.Add(pctList2Zone10_d);
            lstPctListZoneDown.Add(pctGlobal_d);

            lstBtnAcFan = new List<Button>();
            lstBtnAcFan.Insert(0, null);
            lstBtnAcFan.Insert(1, btnLow);
            lstBtnAcFan.Insert(2, btnMedium);
            lstBtnAcFan.Insert(3, btnHigh);

            // List of Settings
            lstPctSettings = new List<PictureBox>();
            lstPctSettings.Add(pctModes);
            lstPctSettings.Add(pctLocalSettings);

            lstPctSettingsDown = new List<PictureBox>();
            lstPctSettingsDown.Add(pctModes_d);
            lstPctSettingsDown.Add(pctLocalSettings_d);

            foreach (string SrceName in cDeviceSettings.lstSources)
                cmbSource.Items.Add(SrceName);

            cmbSource.SelectedIndex = 0;

            //Serve initialization
            Server = new CConnectionManager(cDeviceSettings.sServerAddress, cDeviceSettings.iServerPort);
                                             


            // Invoke the functions for the default values
            changeFloor(DefaultFloorNo);
            changeCategory(DefaultCategoryNo);
            changeZone(DefaultZoneNo[DefaultFloorNo]);
            changeDevices(DefaultZoneNo[DefaultFloorNo]);

            // Modes
            const int iModeWidth = 326;
            const int iModeHeight = 66;
            const int iModeInitialY = 80;
            const int iModeOffsetY = 112;
            const int iModeX = 166;


            cMode1 = new CToggleButton(iModeWidth, iModeHeight, iModeX, iModeInitialY, pctMode1On.Image, pctMode1Off.Image, pnlModes, 1, CGlobal.MODE, Server);
            // Next Mode -> Y = iModeInitialY + iModeOffsetY
        }

        private void pctFlr_MouseDown(object sender, MouseEventArgs e)
        {
            int[] aryMinRanges = new int[3] {  22, 120, 210 };
            int[] aryMaxRanges = new int[3] { 120, 210, 296 };
            int iMousePosition = Form.MousePosition.X;

            for (int iFloorNo = 0; iFloorNo < 3; iFloorNo++)
            {
                if ((iMousePosition > aryMinRanges[iFloorNo]) && (iMousePosition < aryMaxRanges[iFloorNo]))
                {
                    changeFloor(iFloorNo);
                    changeCategory(DefaultCategoryNo);
                    changeZone(DefaultZoneNo[iFloorNo]);
                    changeDevices(DefaultZoneNo[iFloorNo]);
                }
            }
        }

        private void pctCat_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox locPctCat;
            locPctCat = (PictureBox)sender;
            int iCurrentTag = Convert.ToInt32(locPctCat.Tag);            

            changeCategory(iCurrentTag);
            changeDevices(_iZoneNo);
        }        

        private void pctListZone_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox locPctZone;
            locPctZone = (PictureBox)sender;
            int iCurrentTag = Convert.ToInt32(locPctZone.Tag);            

            changeZone(iCurrentTag);
            changeDevices(iCurrentTag);         
        }              

        private void changeZone(int iZoneNo)
        {
            int iLastTag = Convert.ToInt32(pctListZone_u.Tag);
            if (iLastTag != -1)
            {
                lstPctListZone[iLastTag].Image = pctListZone_u.Image;
            }

            pctListZone_u.Tag = iZoneNo;
            pctListZone_u.Image = lstPctListZone[iZoneNo].Image;
            lstPctListZone[iZoneNo].Image = lstPctListZoneDown[iZoneNo].Image;
            _iZoneNo = iZoneNo;            
        }

        private void changeCategory(int iCatNo)
        {
            if (iCatNo == CGlobal.CATEGORY_AC)
            {
                if (_iZoneNo == iBedroom2ZoneNo)
                {
                    changeZone(iBedroom23ZoneNo);
                }
                else if (_iZoneNo == iCorridorZoneNo)
                {
                    changeZone(iStaircaseZoneNo);
                }
                pnlFloor2_2.Visible = false;
                pnlFloor2_AC1.Visible = true;
                pnlFloor2_AC2.Visible = true;
            }
            else
            {
                if (_iZoneNo == iBedroom23ZoneNo)
                {
                    changeZone(iBedroom2ZoneNo);
                }
                else if (_iZoneNo == iStaircaseZoneNo)
                {
                    changeZone(iCorridorZoneNo);
                }

                pnlFloor2_2.Visible = true;
                pnlFloor2_AC1.Visible = false;
                pnlFloor2_AC2.Visible = false;
            }

            int iLastTag = Convert.ToInt32(pctCat_u.Tag);
            if (iLastTag != -1)
            {
                lstPctCategory[iLastTag].Image = pctCat_u.Image;
            }

            pctCat_u.Tag = iCatNo;
            pctCat_u.Image = lstPctCategory[iCatNo].Image;
            lstPctCategory[iCatNo].Image = lstPctCategoryDown[iCatNo].Image;
            _iCategoryNo = iCatNo;
        }

        private void changeFloor(int iFloorNo)
        {
            pctFlr.Image = lstPctFloorDown[iFloorNo].Image;

            foreach (Panel locPanel in lstPnlFloor)
            {
                locPanel.Visible = false;
            }

            lstPnlFloor[iFloorNo].Visible = true;
            _iFloorNo = iFloorNo;
        }

        private void UpdateACInfo()
        {
            if (_iZoneNo == CGlobal.GLOBAL_ZONE)
            {
                // Global ...
            }
            else
            {
                if (IsAcPackageUnit(_iZoneNo))
                {
                    lblCurrentTempPackage.Text = string.Format("{0:0.0}", GetACActualTemp());
                    lblSetTempPackage.Text = string.Format("{0:0.0}", GetACSetTemp());
                    if (GetACMode())
                    {
                        btnModePackage.Text = "Auto";
                    }
                    else
                    {
                        btnModePackage.Text = "Manual";
                    }
                    if (GetCompressorState())
                    {
                        btnRunPackage.Text = "ON";                        
                    }
                    else
                    {
                        btnRunPackage.Text = "OFF";                        
                    }

                    int iFanState = GetFanState();
                    switch (iFanState)
                    {
                        case 0:
                            btnRunPackageFan.Text = "Fan OFF";
                            break;
                        case 1:
                            btnRunPackageFan.Text = "Fan ON";
                            break;
                    }
                }
                else
                {
                    lblCurrentTemp.Text = string.Format("{0:0.0}", GetACActualTemp());
                    lblSetTemp.Text = string.Format("{0:0.0}", GetACSetTemp());
                    if (GetACMode())
                    {
                        btnMode.Text = "Auto";
                    }
                    else
                    {
                        btnMode.Text = "Manual";
                    }

                    if (GetCompressorState())
                    {
                        btnRun.Text = "ON";
                    }
                    else
                    {
                        btnRun.Text = "OFF";
                    }

                    int iFanState = GetFanState();

                    Font BldFont = new Font("Tahoma", 13, FontStyle.Bold);
                    Font RegFont = new Font("Tahoma", 13, FontStyle.Regular);

                    btnRun.Tag = iFanState; //  Save the Fan State
                    for (int i = 1; i < lstBtnAcFan.Count; i++)
                    {
                        lstBtnAcFan[i].Font = RegFont;
                    }

                    if (iFanState > 0)
                    {
                        lstBtnAcFan[iFanState].Font = BldFont;
                    }
                }
            }
        }

        private void changeDevices(int iZoneNo)
        {
            pnlDevicesACFPackage.Visible    = false;
            pnlDevicesACFCU.Visible         = false;
            pnlGlobalAC.Visible             = false;
            pnlDevices.Visible              = false;
            panelAV.Visible                 = false; 

            switch (_iCategoryNo)
            {
                case CGlobal.CATEGORY_AC:

                    if (_iZoneNo == CGlobal.GLOBAL_ZONE)
                    {
                        pnlGlobalAC.Visible = true;

                        UpdateACInfo();
                    }
                    else if (cDeviceSettings.lstZones[_iZoneNo].lstAcPrm.Count > 0)
                    {
                        if (IsAcPackageUnit(_iZoneNo))
                        {
                            pnlDevicesACFPackage.Visible = true;

                        }
                        else
                        {
                            pnlDevicesACFCU.Visible = true;
                        }

                        UpdateACInfo();
                    }

                    break;
                case CGlobal.CATEGORY_AV:
                    if (_iZoneNo == CGlobal.GLOBAL_ZONE)
                    {
                        // Global ..
                    }
                    else
                    {
                        if (cDeviceSettings.lstZones[iZoneNo].iAVZoneID >= 0)
                        {
                            panelAV.Visible = true;
                            btnZoneONOFF.Text = "Zone ON";
                        }
                    }
                    break;
                case CGlobal.CATEGORY_LIGHTING:
                    pnlDevices.Visible = true;
                    while (pnlDevicesLabel.Controls.Count > 0)
                    {
                        pnlDevicesLabel.Controls[0].Dispose();
                    }

                    while (pnlDevicesIcon.Controls.Count > 0)
                    {
                        pnlDevicesIcon.Controls[0].Dispose();
                    }

                    /*
                    lstCToggle = new List<CToggleButton>();
                    lstCSlider = new List<CSlideButton>();
                    lstCCurtain = new List<CCurtainButton>();            
                    */

                    int iYOffset = cDeviceSettings.iMarginColumn;
                    int iYInitialLabel = cDeviceSettings.iInitialMarginTopText;
                    int iYInitialSymbol = cDeviceSettings.iInitialMarginTopSymbol;
                    int iXOffsetLabel = cDeviceSettings.iMarginText;
                    int iXOffsetSymbol = cDeviceSettings.iMarginSymbol;
                    int iYNextLabel = iYInitialLabel;
                    int iYNextSymbol = iYInitialSymbol;
                    int iFontSize = cDeviceSettings.iFontSize;
                    int iLabelWidth = cDeviceSettings.iTextWidth;
                    int iXOffset = 144;

                    if (_iZoneNo == CGlobal.GLOBAL_ZONE)
                    {
                        cPushButton = new CPushButton(cDeviceSettings.iToggleWidth, cDeviceSettings.iHeight, iXOffsetSymbol, iYNextSymbol, this.pctToggleOn.Image, this.pctToggleOn.Image, this.pnlDevicesIcon, CGlobal.GLOBAL_ZONE, 1, CGlobal.CATEGORY_LIGHTING, Server);
                        cPushButton = new CPushButton(cDeviceSettings.iToggleWidth, cDeviceSettings.iHeight, iXOffsetSymbol + iXOffset, iYNextSymbol, this.pctToggleOff.Image, this.pctToggleOff.Image, this.pnlDevicesIcon, CGlobal.GLOBAL_ZONE, 0, CGlobal.CATEGORY_LIGHTING, Server);

                        // Label
                        Label lblDevice = new Label();
                        lblDevice.Text = cDeviceSettings.sGlobalLightsDescription;
                        lblDevice.Left = iXOffsetLabel;
                        lblDevice.Top = iYNextLabel;
                        lblDevice.Font = new Font(Font.Name, iFontSize, FontStyle.Bold);
                        lblDevice.ForeColor = Color.White;
                        lblDevice.Width = iLabelWidth;
                        this.pnlDevicesLabel.Controls.Add(lblDevice);
                    }
                    else
                    {
                        foreach (CZones zone in cDeviceSettings.lstZones)
                        {
                            if (zone.iZoneTag == iZoneNo)
                            {
                                /*
                                int iCount = Convert.ToInt32(zone.lstDevices.Count);
                                CToggleButton[] cAToggle = new CToggleButton[iCount];
                                CSlideButton[] cASlide = new CSlideButton[iCount];
                                CCurtainButton[] cACurtain = new CCurtainButton[iCount];
                                int i = 0;                     
                                */
                                foreach (CDevices device in zone.lstDevices)
                                {
                                    if (device.bVisible && checkDeviceType(device.iType, LightDevices))
                                    {

                                        // Symbol
                                        if (device.iType == CGlobal.ONOFF_DEVICE)
                                        {
                                            cToggle = new CToggleButton(cDeviceSettings.iToggleWidth, cDeviceSettings.iHeight, iXOffsetSymbol, iYNextSymbol, this.pctToggleOn.Image, this.pctToggleOff.Image, this.pnlDevicesIcon, device.iTag, device.iValue, device, CGlobal.CATEGORY_LIGHTING);
                                            //cAToggle[i] = new CToggleButton(cDeviceSettings.iToggleWidth, cDeviceSettings.iHeight, cDeviceSettings.iLeft, iY, this.pctToggleOn.Image, this.pctToggleOff.Image, this.pnlDevices, device.iTag, device.iValue);
                                            //lstCToggle.Add(cToggle);
                                        }
                                        else if (device.iType == CGlobal.DIMMING_DEVICE)
                                        {
                                            cSlider = new CSlideButton(cDeviceSettings.iSlideWidth, cDeviceSettings.iHeight, cDeviceSettings.iHeight, iXOffsetSymbol, iYNextSymbol, this.pctSliderLane.Image, this.pctSilderSlide.Image, this.pctSliderOn.Image, this.pctSliderOff.Image, this.pnlDevicesIcon, device.iTag, device.iValue, device);
                                            //cASlide[i] = new CSlideButton(cDeviceSettings.iSlideWidth, cDeviceSettings.iHeight, cDeviceSettings.iHeight, cDeviceSettings.iLeft, iY, this.pctSliderLane.Image, this.pctSilderSlide.Image, this.pctSliderOn.Image, this.pctSliderOff.Image, this.pnlDevices, device.iTag, device.iValue);
                                            //lstCSlider.Add(cSlider);
                                        }
                                        else if (device.iType == CGlobal.SCENE_DEVICE)
                                        {
                                            iYNextLabel += (iYOffset / 2);
                                            iYNextSymbol += (iYOffset / 2);
                                            cScene = new CSceneButton(cDeviceSettings.iSceneWidth, cDeviceSettings.iHeight, iXOffsetSymbol, iYNextSymbol, this.pctScene_u.Image, this.pctScene1.Image, this.pctScene2.Image, this.pctScene3.Image, this.pctScene4.Image, this.pnlDevicesIcon, device.iTag, device.iValue, zone, Server);
                                        }

                                        // Label
                                        Label lblDevice = new Label();
                                        lblDevice.Text = device.sDescription;
                                        lblDevice.Left = iXOffsetLabel;
                                        lblDevice.Top = iYNextLabel;
                                        lblDevice.Font = new Font(Font.Name, iFontSize, FontStyle.Bold);
                                        lblDevice.ForeColor = Color.White;
                                        lblDevice.Width = iLabelWidth;
                                        this.pnlDevicesLabel.Controls.Add(lblDevice);

                                        // Prepare Y Offset for the next Label & Symbol
                                        iYNextLabel += iYOffset;
                                        iYNextSymbol += iYOffset;
                                    }
                                }
                            }
                        }
                    }
                    break;
                case CGlobal.CATEGORY_BLIND:
                    pnlDevices.Visible = true;
                    while (pnlDevicesLabel.Controls.Count > 0)
                    {
                        pnlDevicesLabel.Controls[0].Dispose();
                    }

                    while (pnlDevicesIcon.Controls.Count > 0)
                    {
                        pnlDevicesIcon.Controls[0].Dispose();
                    }

                    /*
                    lstCToggle = new List<CToggleButton>();
                    lstCSlider = new List<CSlideButton>();
                    lstCCurtain = new List<CCurtainButton>();            
                    */

                    iYOffset = cDeviceSettings.iMarginColumn;
                    iYInitialLabel = cDeviceSettings.iInitialMarginTopText;
                    iYInitialSymbol = cDeviceSettings.iInitialMarginTopSymbol;
                    iXOffsetLabel = cDeviceSettings.iMarginText;
                    iXOffsetSymbol = cDeviceSettings.iMarginSymbol;
                    iYNextLabel = iYInitialLabel;
                    iYNextSymbol = iYInitialSymbol;
                    iFontSize = cDeviceSettings.iFontSize;
                    iLabelWidth = cDeviceSettings.iTextWidth;

                    if (_iZoneNo == CGlobal.GLOBAL_ZONE)
                    {
                        // Label
                        Label lblDevice = new Label();
                        lblDevice.Text = cDeviceSettings.sGlobalBlindsDescription;
                        lblDevice.Left = iXOffsetLabel;
                        lblDevice.Top = iYNextLabel;
                        lblDevice.Font = new Font(Font.Name, iFontSize, FontStyle.Bold);
                        lblDevice.ForeColor = Color.White;
                        lblDevice.Width = iLabelWidth;
                        this.pnlDevicesLabel.Controls.Add(lblDevice);

                        // Symbol
                        cBlind = new CBlindButton(cDeviceSettings.iBlindWidth, cDeviceSettings.iHeight, iXOffsetSymbol, iYNextSymbol, this.pctBlindOpen.Image, this.pctBlindClose.Image, this.pctBlindStop.Image, this.pnlDevicesIcon, CGlobal.GLOBAL_ZONE, CGlobal.BLIND_DEFAULT_VALUE);
                    }
                    else
                    {
                        foreach (CZones zone in cDeviceSettings.lstZones)
                        {
                            if (zone.iZoneTag == iZoneNo)
                            {
                                /*
                                int iCount = Convert.ToInt32(zone.lstDevices.Count);
                                CToggleButton[] cAToggle = new CToggleButton[iCount];
                                CSlideButton[] cASlide = new CSlideButton[iCount];
                                CCurtainButton[] cACurtain = new CCurtainButton[iCount];
                                int i = 0;                     
                                */
                                foreach (CDevices device in zone.lstDevices)
                                {
                                    if (device.bVisible && checkDeviceType(device.iType, BlindDevices))
                                    {
                                        // Label
                                        Label lblDevice = new Label();
                                        lblDevice.Text = device.sDescription;
                                        lblDevice.Left = iXOffsetLabel;
                                        lblDevice.Top = iYNextLabel;
                                        lblDevice.Font = new Font(Font.Name, iFontSize, FontStyle.Bold);
                                        lblDevice.ForeColor = Color.White;
                                        lblDevice.Width = iLabelWidth;
                                        this.pnlDevicesLabel.Controls.Add(lblDevice);

                                        // Symbol
                                        cBlind = new CBlindButton(cDeviceSettings.iBlindWidth, cDeviceSettings.iHeight, iXOffsetSymbol, iYNextSymbol, this.pctBlindOpen.Image, this.pctBlindClose.Image, this.pctBlindStop.Image, this.pnlDevicesIcon, device.iTag, device.iValue, device);

                                        iYNextLabel += iYOffset;
                                        iYNextSymbol += iYOffset;
                                    }
                                }
                            }
                        }
                    }

                    break;
                case CGlobal.CATEGORY_MS:
                    pnlDevices.Visible = true;
                    while (pnlDevicesLabel.Controls.Count > 0)
                    {
                        pnlDevicesLabel.Controls[0].Dispose();
                    }

                    while (pnlDevicesIcon.Controls.Count > 0)
                    {
                        pnlDevicesIcon.Controls[0].Dispose();
                    }

                    iXOffsetLabel = cDeviceSettings.iMarginText;
                    iFontSize = cDeviceSettings.iFontSize;
                    iLabelWidth = cDeviceSettings.iTextWidth;
                    const int iYOffsetLabel = 12; // it was 20                
                    const int iLabelPanelHeight = 60;
                    const int iConfigOffsetPanelHeight = 103;

                    // List ...
                    List<Panel> lstPnl;
                    List<CToggleButton> lstTgl;
                    lstPnl = new List<Panel>();
                    lstTgl = new List<CToggleButton>();

                    if (_iZoneNo == CGlobal.GLOBAL_ZONE)
                    {
                        // Label
                        Panel pnlLabel = new Panel();
                        pnlLabel.Height = iLabelPanelHeight;
                        pnlLabel.Dock = DockStyle.Top;
                        pnlLabel.BackColor = pnlDevicesLabel.BackColor;

                        Panel pnlOffset = new Panel();
                        pnlOffset.Height = iConfigOffsetPanelHeight;
                        pnlOffset.Dock = DockStyle.Top;
                        pnlOffset.Visible = false;
                        pnlOffset.Tag = CGlobal.GLOBAL_ZONE;
                        pnlOffset.BackColor = pnlDevicesLabel.BackColor;

                        Label lblDevice = new Label();
                        lblDevice.Text = cDeviceSettings.sGlobalMSensorDescription;
                        lblDevice.Left = iXOffsetLabel;
                        lblDevice.Top = iYOffsetLabel;
                        lblDevice.Font = new Font(Font.Name, iFontSize, FontStyle.Bold);
                        lblDevice.ForeColor = Color.White;
                        lblDevice.Width = iLabelWidth;
                        pnlLabel.Controls.Add(lblDevice);
                        this.pnlDevicesLabel.Controls.Add(pnlOffset);
                        this.pnlDevicesLabel.Controls.Add(pnlLabel);
                        lstPnl.Add(pnlOffset);

                        // Symbol                                                                                                                                
                        cMsensor = new CMotionDetectorSettings(pctMSensorEnable.Image, pctMSensorDisable.Image, pctMSensorConfig.Image, pctMSensorDeConfig.Image, this.pnlDevicesIcon, CGlobal.GLOBAL_ZONE, 0, 0, 5, 6, 30, 18, 30, lstPnl, lstTgl, null);
                    }
                    else
                    {
                        foreach (CZones zone in cDeviceSettings.lstZones)
                        {
                            if (zone.iZoneTag == iZoneNo)
                            {
                                foreach (CDevices device in zone.lstDevices)
                                {
                                    if (device.bVisible && checkDeviceType(device.iType, MSensorDevices))
                                    {
                                        // Label
                                        Panel pnlLabel = new Panel();
                                        pnlLabel.Height = iLabelPanelHeight;
                                        pnlLabel.Dock = DockStyle.Top;
                                        pnlLabel.BackColor = pnlDevicesLabel.BackColor;

                                        Panel pnlOffset = new Panel();
                                        pnlOffset.Height = iConfigOffsetPanelHeight;
                                        pnlOffset.Dock = DockStyle.Top;
                                        pnlOffset.Visible = false;
                                        pnlOffset.Tag = device.iTag;
                                        pnlOffset.BackColor = pnlDevicesLabel.BackColor;

                                        Label lblDevice = new Label();
                                        lblDevice.Text = device.sDescription;
                                        lblDevice.Left = iXOffsetLabel;
                                        lblDevice.Top = iYOffsetLabel;
                                        lblDevice.Font = new Font(Font.Name, iFontSize, FontStyle.Bold);
                                        lblDevice.ForeColor = Color.White;
                                        lblDevice.Width = iLabelWidth;
                                        pnlLabel.Controls.Add(lblDevice);
                                        this.pnlDevicesLabel.Controls.Add(pnlOffset);
                                        this.pnlDevicesLabel.Controls.Add(pnlLabel);
                                        lstPnl.Add(pnlOffset);

                                        // Symbol                                                                                                                                
                                        cMsensor = new CMotionDetectorSettings(pctMSensorEnable.Image, pctMSensorDisable.Image, pctMSensorConfig.Image, pctMSensorDeConfig.Image, this.pnlDevicesIcon, device.iTag, device.iValue, 0, 4, 6, 4, 18, 4, lstPnl, lstTgl, device);
                                    }
                                }
                            }
                        }
                    }
                    break;
            }        
        }

        private void pctACSetTempPackage_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox locPctZone;
            locPctZone = (PictureBox)sender;
            int iCurrentTag = Convert.ToInt32(locPctZone.Tag);

            changeSetTemp(lblSetTempPackage, ACSetTemp[iCurrentTag]);
        }

        private void pctACSetTemp_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox locPctZone;
            locPctZone = (PictureBox)sender;
            int iCurrentTag = Convert.ToInt32(locPctZone.Tag); 

            changeSetTemp(lblSetTemp, ACSetTemp[iCurrentTag]);
        }        

        private void changeSetTemp(Label lblInput, string sOperation)
        {
            float fSetTemp = Convert.ToSingle(lblInput.Text);
            switch (sOperation)
            {
                case CGlobal.INCREASE:
                    fSetTemp += 0.5F;
                    if (_iZoneNo == CGlobal.GLOBAL_ZONE)
                    {
                        GlobalSetTemp(fSetTemp);
                    }
                    else
                    {
                        IncreaseSetTemp();
                    }
                    break;
                case CGlobal.DECREASE:
                    fSetTemp -= 0.5F;
                    if (_iZoneNo == CGlobal.GLOBAL_ZONE)
                    {
                        GlobalSetTemp(fSetTemp);
                    }
                    else
                    {
                        DecreaseSetTemp();
                    }
                    break;
            }
            lblInput.Text = string.Format("{0:0.0}", fSetTemp);
        }

        private bool IsAcPackageUnit(int iZoneNo)
        {
            bool bRet = false;
            foreach (int i in ACPackage)
            {
                if (i == iZoneNo)
                {
                    bRet = true;
                    break;
                }
            }
            return bRet;
        }

        private bool checkDeviceType(int iDeviceType, int[] aDevcieTypes)
        {
            bool bRet = false;
            foreach (int i in aDevcieTypes)
            {
                if (i == iDeviceType)
                {
                    bRet = true;
                    break;
                }
            }
            return bRet;
        }

        private void tmrUpdateTime_Tick(object sender, EventArgs e)
        {
            lblDateTime.Text = System.DateTime.Now.ToString();
        }

        private float GetACActualTemp()
        {
            float fRet = 0;

            short ActualTemp = cDeviceSettings.lstZones[_iZoneNo].ACTCinterface.GetShort(cDeviceSettings.lstZones[_iZoneNo].lstAcPrm[2] + ".wTempValue");
            fRet = Convert.ToSingle(ActualTemp) / 10;

            return fRet;
        }

        private float GetACSetTemp()
        {
            float fRet = 0;

            short ActualTemp = cDeviceSettings.lstZones[_iZoneNo].ACTCinterface.GetShort(cDeviceSettings.lstZones[_iZoneNo].lstAcPrm[2] + ".wSetTemp");
            fRet = Convert.ToSingle(ActualTemp) / 10;

            return fRet;
        }

        private void GlobalSetTemp(float fTemp)
        {
            CMessage msgToSend = new CMessage();
            msgToSend.SetMessage(CMessageConstants.CMD_GLOBAL_ALL_AC_SETTEMP, CMessageConstants.CMD_TYPE_COMMUNICATION, 0);
            msgToSend.AddIntPrm(CGlobal.GLOBAL_ZONE);
            // Convert to INT !
            msgToSend.AddIntPrm(Convert.ToInt32(fTemp));

            Server.SendMessage(msgToSend);
        }

        private void IncreaseSetTemp()
        {   
            cDeviceSettings.lstZones[_iZoneNo].ACTCinterface.SetBool(cDeviceSettings.lstZones[_iZoneNo].lstAcPrm[3],true);
        }

        private void DecreaseSetTemp()
        {
            cDeviceSettings.lstZones[_iZoneNo].ACTCinterface.SetBool(cDeviceSettings.lstZones[_iZoneNo].lstAcPrm[4], true);
        }

        private void SetAutoMode(bool bIsAuto)
        {

            if (bIsAuto)
            {
                cDeviceSettings.lstZones[_iZoneNo].ACTCinterface.SetBool(cDeviceSettings.lstZones[_iZoneNo].lstAcPrm[2] + ".bACModeIsAuto", true);
            }
            else
            {
                cDeviceSettings.lstZones[_iZoneNo].ACTCinterface.SetBool(cDeviceSettings.lstZones[_iZoneNo].lstAcPrm[2] + ".bACModeIsAuto", false);
            }
        }

        private void SetFanSpeed(int iFanSpeed)
        {
            cDeviceSettings.lstZones[_iZoneNo].ACTCinterface.SetShort(cDeviceSettings.lstZones[_iZoneNo].lstAcPrm[2] + ".iFanState", Convert.ToInt16(iFanSpeed));
        }

        private void SetACCompressorState(bool bCompressorState)
        {
            if (bCompressorState)
            {
                cDeviceSettings.lstZones[_iZoneNo].ACTCinterface.SetBool(cDeviceSettings.lstZones[_iZoneNo].lstAcPrm[2] + ".bCompState", true);
            }
            else
            {
                cDeviceSettings.lstZones[_iZoneNo].ACTCinterface.SetBool(cDeviceSettings.lstZones[_iZoneNo].lstAcPrm[2] + ".bCompState", false);
            }
        }

        private bool GetCompressorState()
        {
            bool bRet = cDeviceSettings.lstZones[_iZoneNo].ACTCinterface.GetBool(cDeviceSettings.lstZones[_iZoneNo].lstAcPrm[2] + ".bCompState");
            return !bRet;
        }

        private bool GetACMode()
        {
            bool bRet = cDeviceSettings.lstZones[_iZoneNo].ACTCinterface.GetBool(cDeviceSettings.lstZones[_iZoneNo].lstAcPrm[2] + ".bACModeIsAuto");
            return bRet;
        }

        private int GetFanState()
        {
            int iRet = Convert.ToInt32(cDeviceSettings.lstZones[_iZoneNo].ACTCinterface.GetShort(cDeviceSettings.lstZones[_iZoneNo].lstAcPrm[2] + ".iFanState"));
            return iRet;
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            if (btnMode.Text == "Manual")
            {
                if (btnRun.Text == "ON")
                {
                    SetACCompressorState(true);
                    btnRun.Text = "OFF";
                }
                else
                {
                    SetACCompressorState(false);
                    btnRun.Text = "ON";

                    int iCurrentFanState = Convert.ToInt32(btnRun.Tag);
                    if (iCurrentFanState == 0)
                    {
                        Font BldFont = new Font("Tahoma", 13, FontStyle.Bold);
                        lstBtnAcFan[1].Font = BldFont; // In Twincat if the Compressor = ON -> the Fan = Low
                    }
                }
            }
        }

        private void btnFan_Click(object sender, EventArgs e)
        {
            Button locBtnACFan;
            locBtnACFan = (Button)sender;
            int iCurrentTag = Convert.ToInt32(locBtnACFan.Tag);

            changeACFanSpeed(iCurrentTag); 
        }

        private void changeACFanSpeed(int iFanState)
        {
            Font BldFont = new Font("Tahoma", 13, FontStyle.Bold);
            Font RegFont = new Font("Tahoma", 13, FontStyle.Regular);

            int iOldFanState = Convert.ToInt32(btnRun.Tag);

            if (btnMode.Text == "Manual")
            {
                if (iFanState == iOldFanState)
                {
                    if (btnRun.Text == "OFF")
                    {
                        lstBtnAcFan[iFanState].Font = RegFont;
                        SetFanSpeed(0);

                        btnRun.Tag = 0;
                    }
                }
                else
                {
                    for (int i = 1; i < lstBtnAcFan.Count; i++)
                    {
                        lstBtnAcFan[i].Font = RegFont;
                    }

                    lstBtnAcFan[iFanState].Font = BldFont;
                    SetFanSpeed(iFanState);

                    btnRun.Tag = iFanState;
                }
            }
        }              

        private void btnMode_Click(object sender, EventArgs e)
        {
            if (btnMode.Text == "Manual")
            {
                SetAutoMode(true);
                btnMode.Text = "Auto";
            }
            else
            {
                SetAutoMode(false);
                btnMode.Text = "Manual";
            }
        }

        private void btnRunPackage_Click(object sender, EventArgs e)
        {
            if (btnModePackage.Text == "Manual")
            {
                if (btnRunPackage.Text == "ON")
                {
                    SetACCompressorState(true);
                    btnRunPackage.Text = "OFF";
                }
                else
                {
                    SetACCompressorState(false);
                    btnRunPackage.Text = "ON";
                    btnRunPackageFan.Text = "Fan ON";
                }
            }
        }

        private void btnModePackage_Click(object sender, EventArgs e)
        {
            if (btnModePackage.Text == "Manual")
            {
                SetAutoMode(true);
                btnModePackage.Text = "Auto";
            }
            else
            {
                SetAutoMode(false);
                btnModePackage.Text = "Manual";
            }
        }

        private void pctIALogo_DoubleClick(object sender, EventArgs e)
        {
            FrmAbout frmbout = new FrmAbout();
            frmbout.Show();
        }

        
       

        private void btnVolUp_Click(object sender, EventArgs e)
        {
            CMessage msgToSend = new CMessage();
            msgToSend.SetMessage(CMessageConstants.CMD_AV_VOLUME_UP, CMessageConstants.CMD_TYPE_AV, 0);
            msgToSend.AddIntPrm(cDeviceSettings.lstZones[_iZoneNo].iAVZoneID);
            msgToSend.AddIntPrm(cDeviceSettings.lstSourceID[cmbSource.SelectedIndex]);

            Server.SendMessage(msgToSend);
        }

        private void btnVolDown_Click(object sender, EventArgs e)
        {
            CMessage msgToSend = new CMessage();
            msgToSend.SetMessage(CMessageConstants.CMD_AV_VOLUME_DOWN, CMessageConstants.CMD_TYPE_AV, 0);
            msgToSend.AddIntPrm(cDeviceSettings.lstZones[_iZoneNo].iAVZoneID);
            msgToSend.AddIntPrm(cDeviceSettings.lstSourceID[cmbSource.SelectedIndex]);

            Server.SendMessage(msgToSend);
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            CMessage msgToSend = new CMessage();
            msgToSend.SetMessage(CMessageConstants.CMD_AV_SEND_COMMAND_TO_SOURCE, CMessageConstants.CMD_TYPE_AV, 0);
            msgToSend.AddIntPrm(cDeviceSettings.lstSourceID[cmbSource.SelectedIndex]);
            msgToSend.AddIntPrm(10);

            Server.SendMessage(msgToSend);
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            CMessage msgToSend = new CMessage();
            msgToSend.SetMessage(CMessageConstants.CMD_AV_SEND_COMMAND_TO_SOURCE, CMessageConstants.CMD_TYPE_AV, 0);
            msgToSend.AddIntPrm(cDeviceSettings.lstSourceID[cmbSource.SelectedIndex]);
            msgToSend.AddIntPrm(31);

            Server.SendMessage(msgToSend);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            CMessage msgToSend = new CMessage();
            msgToSend.SetMessage(CMessageConstants.CMD_AV_SEND_COMMAND_TO_SOURCE, CMessageConstants.CMD_TYPE_AV, 0);
            msgToSend.AddIntPrm(cDeviceSettings.lstSourceID[cmbSource.SelectedIndex]);
            msgToSend.AddIntPrm(12);

            Server.SendMessage(msgToSend);
        }

        private void btnfbw_Click(object sender, EventArgs e)
        {
            CMessage msgToSend = new CMessage();
            msgToSend.SetMessage(CMessageConstants.CMD_AV_SEND_COMMAND_TO_SOURCE, CMessageConstants.CMD_TYPE_AV, 0);
            msgToSend.AddIntPrm(cDeviceSettings.lstSourceID[cmbSource.SelectedIndex]);
            msgToSend.AddIntPrm(21);

            Server.SendMessage(msgToSend);
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            CMessage msgToSend = new CMessage();
            msgToSend.SetMessage(CMessageConstants.CMD_AV_SEND_COMMAND_TO_SOURCE, CMessageConstants.CMD_TYPE_AV, 0);
            msgToSend.AddIntPrm(cDeviceSettings.lstSourceID[cmbSource.SelectedIndex]);
            msgToSend.AddIntPrm(19);

            Server.SendMessage(msgToSend);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            CMessage msgToSend = new CMessage();
            msgToSend.SetMessage(CMessageConstants.CMD_AV_SEND_COMMAND_TO_SOURCE, CMessageConstants.CMD_TYPE_AV, 0);
            msgToSend.AddIntPrm(cDeviceSettings.lstSourceID[cmbSource.SelectedIndex]);
            msgToSend.AddIntPrm(18);

            Server.SendMessage(msgToSend);
        }

        private void btnffw_Click(object sender, EventArgs e)
        {
            CMessage msgToSend = new CMessage();
            msgToSend.SetMessage(CMessageConstants.CMD_AV_SEND_COMMAND_TO_SOURCE, CMessageConstants.CMD_TYPE_AV, 0);
            msgToSend.AddIntPrm(cDeviceSettings.lstSourceID[cmbSource.SelectedIndex]);
            msgToSend.AddIntPrm(20);

            Server.SendMessage(msgToSend);
        }

        private void pnlDevicesACFCU_GotFocus(object sender, EventArgs e)
        {

        }

        private void btnZoneONOFF_Click(object sender, EventArgs e)
        {
            CMessage msgToSend = new CMessage();
            if (btnZoneONOFF.Text == "Zone ON")
            {

                msgToSend.SetMessage(CMessageConstants.CMD_AV_ZONE_ON, CMessageConstants.CMD_TYPE_AV, 0);
                msgToSend.AddIntPrm(cDeviceSettings.lstZones[_iZoneNo].iAVZoneID);
                msgToSend.AddIntPrm(cDeviceSettings.lstSourceID[cmbSource.SelectedIndex]);


                btnZoneONOFF.Text = "Zone OFF";
            }
            else
            {
                msgToSend.SetMessage(CMessageConstants.CMD_AV_ZONE_OFF, CMessageConstants.CMD_TYPE_AV, 0);
                msgToSend.AddIntPrm(cDeviceSettings.lstZones[_iZoneNo].iAVZoneID);
                btnZoneONOFF.Text = "Zone ON";
            }

            Server.SendMessage(msgToSend);
        }

        private void cmbSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            /*CMessage msgToSend = new CMessage();
            msgToSend.SetMessage(CMessageConstants.CMD_AV_ZONE_ON, CMessageConstants.CMD_TYPE_AV, 0);
            msgToSend.AddIntPrm(cDeviceSettings.lstZones[_iZoneNo].iAVZoneID);
            msgToSend.AddIntPrm(cDeviceSettings.lstSourceID[cmbSource.SelectedIndex]);

            Server.SendMessage(msgToSend);
            btnZoneONOFF.Text = "Zone OFF";*/
        }

        private void pctLocalSettings_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox locPctSettings;
            locPctSettings = (PictureBox)sender;
            int iCurrentTag = Convert.ToInt32(locPctSettings.Tag);

            changeSettings(iCurrentTag);
        }

        private void changeSettings(int iSetNo)
        {
            int iLastTag = Convert.ToInt32(pctSettings_u.Tag);
            if (iLastTag != -1)
            {
                lstPctSettings[iLastTag].Image = pctSettings_u.Image;
            }

            pctSettings_u.Tag = iSetNo;
            pctSettings_u.Image = lstPctSettings[iSetNo].Image;
            lstPctSettings[iSetNo].Image = lstPctSettingsDown[iSetNo].Image;
            _iSettingsNo = iSetNo;
        }

        private void pctLocalSettings_MouseUp(object sender, MouseEventArgs e)
        {
            pnlSettings.Visible = !pnlSettings.Visible;
        }

        private void pctGlobalACSetTemp_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox locPctZone;
            locPctZone = (PictureBox)sender;
            int iCurrentTag = Convert.ToInt32(locPctZone.Tag);

            changeSetTemp(lblGlobalACSetTemp, ACSetTemp[iCurrentTag]);
        }

        private void btnGlobalACOff_Click(object sender, EventArgs e)
        {
            CMessage msgToSend = new CMessage();
            msgToSend.SetMessage(CMessageConstants.CMD_GLOBAL_ALL_AC_SWITCHOFF, CMessageConstants.CMD_TYPE_COMMUNICATION, 0);
            msgToSend.AddIntPrm(CGlobal.GLOBAL_ZONE);
            msgToSend.AddIntPrm(0);

            Server.SendMessage(msgToSend);
        }

        private void btnGlobalACAuto_Click(object sender, EventArgs e)
        {
            CMessage msgToSend = new CMessage();
            msgToSend.SetMessage(CMessageConstants.CMD_GLOBAL_ALL_AC_AUTO, CMessageConstants.CMD_TYPE_COMMUNICATION, 0);
            msgToSend.AddIntPrm(CGlobal.GLOBAL_ZONE);
            msgToSend.AddIntPrm(1);

            Server.SendMessage(msgToSend);
        }

        private void btnFAHUOff_Click(object sender, EventArgs e)
        {
            CMessage msgToSend = new CMessage();
            msgToSend.SetMessage(CMessageConstants.CMD_GLOBAL_FAHU_SWITCHOFF, CMessageConstants.CMD_TYPE_COMMUNICATION, 0);
            msgToSend.AddIntPrm(CGlobal.GLOBAL_ZONE);
            msgToSend.AddIntPrm(0);

            Server.SendMessage(msgToSend);
        }

        private void btnFAHUAuto_Click(object sender, EventArgs e)
        {
            CMessage msgToSend = new CMessage();
            msgToSend.SetMessage(CMessageConstants.CMD_GLOBAL_FAHU_AUTO, CMessageConstants.CMD_TYPE_COMMUNICATION, 0);
            msgToSend.AddIntPrm(CGlobal.GLOBAL_ZONE);
            msgToSend.AddIntPrm(0);

            Server.SendMessage(msgToSend);
        }

        private void btnRunPackageFan_Click(object sender, EventArgs e)
        {
            if ((btnModePackage.Text == "Manual") && (btnRunPackage.Text == "OFF"))
            {
                if (btnRunPackageFan.Text == "Fan ON")
                {
                    SetFanSpeed(0);
                    btnRunPackageFan.Text = "Fan OFF";
                }
                else
                {
                    SetFanSpeed(1);
                    btnRunPackageFan.Text = "Fan ON";
                }
            }
        }                           
    }
}