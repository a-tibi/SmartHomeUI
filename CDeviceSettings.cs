using System;

using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace Ground_Floor
{
    class CDevices
    {
        public CDevices()
        {
            lstPrm = new List<string>();
        }

        public int iTag;
        public int iType;
        public bool bVisible;
        public string sDescription;
        public int iValue;
        public List<string> lstPrm;
        public CTwinCatInterface TCInterface;
    }

    class CZones
    {
        public int iZoneTag;
        public List<string> lstServerZones;
        public int iFloor;
        public string sZoneDescription;
        public List<CDevices> lstDevices;
        public List<string> lstAcPrm;
        public CTwinCatInterface ACTCinterface;
        public int iAVZoneID;

        public CZones()
        {
            lstDevices = new List<CDevices>();
            lstAcPrm = new List<string>();
            lstServerZones = new List<string>();
            iAVZoneID = -1;
        }
    }

    class CDeviceSettings
    {
        private bool _bFileMissing;
        private string _sFileName;

        public List<string> lstSources;
        public List<int> lstSourceID;

        public int iDefaultFloorNo;
        public int iHeight;
        public int iSlideWidth;
        public int iToggleWidth;
        public int iSceneWidth;
        public int iBlindWidth;
        public int iMarginText;
        public int iMarginSymbol;
        public int iInitialMarginTopText;
        public int iInitialMarginTopSymbol;
        public int iMarginColumn;
        public int iFontSize;
        public int iFontColor;
        public int iTextWidth;

        // Motion Sensor Default Settings :: Begin

        public int iMsensorDefaultDelay;
        public int iMsensorDefaultFromHour;
        public int iMsensorDefaultFromMinute;
        public int iMsensorDefaultToHour;
        public int iMsensorDefaultToMinute;

        // Motion Sensor Default Settings :: End

        public List<CZones> lstZones;
        public List<string> lstControllerPrm;
        public string sServerAddress;
        public int iServerPort;

        // Global :: Begin
        public string sGlobalLightsDescription;
        public string sGlobalBlindsDescription;
        public string sGlobalACDescription;
        public string sGlobalFAHUDescription;
        public string sGlobalMSensorDescription;
        // Global :: End

        public CDeviceSettings(string sFileName)
        {
            lstZones = new List<CZones>();
            lstControllerPrm = new List<string>();

            lstSources = new List<string>();
            lstSourceID = new List<int>();

            _sFileName = sFileName;
            try
            {
                if (File.Exists(_sFileName))
                {
                    _bFileMissing = false;
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("XML settings file is missing.");
                    _bFileMissing = true;
                }

            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Exception caught while creating instance of XML reader. Check presence of xml settings file.");
                _bFileMissing = true;
            }
        }

        public bool GetDeviceSettings()
        {
            bool bRet = false;

            if (!_bFileMissing)
            {
                XmlTextReader XMLReader;
                string sValue;
                string sElemtName = "";
                CZones locZone = new CZones();
                CDevices locDevice = new CDevices();
                string[] myLstPrm;
                int iCptString;

                XMLReader = new XmlTextReader(_sFileName);

                while (XMLReader.Read())
                {
                    switch (XMLReader.NodeType)
                    {
                        case XmlNodeType.Element:
                            sElemtName = XMLReader.Name;
                            break;
                        case XmlNodeType.Text:
                            sValue = XMLReader.Value;
                            if (sElemtName == "CONTROLLER_PRM")
                                lstControllerPrm.Add(sValue);
                            if (sElemtName == "DEFAULT_FLOOR")
                                iDefaultFloorNo = Convert.ToInt32(sValue);
                            if (sElemtName == "HEIGHT")
                                iHeight = Convert.ToInt32(sValue);
                            if (sElemtName == "SLIDE_WIDTH")
                                iSlideWidth = Convert.ToInt32(sValue);
                            if (sElemtName == "TOGGLE_WIDTH")
                                iToggleWidth = Convert.ToInt32(sValue);
                            if (sElemtName == "SCENE_WIDTH")
                                iSceneWidth = Convert.ToInt32(sValue);
                            if (sElemtName == "BLIND_WIDTH")
                                iBlindWidth = Convert.ToInt32(sValue);
                            if (sElemtName == "MARGIN_TEXT")
                                iMarginText = Convert.ToInt32(sValue);
                            if (sElemtName == "MARGIN_SYMBOL")
                                iMarginSymbol = Convert.ToInt32(sValue);
                            if (sElemtName == "INITIAL_MARGIN_TOP_TEXT")
                                iInitialMarginTopText = Convert.ToInt32(sValue);
                            if (sElemtName == "INITIAL_MARGIN_TOP_SYMBOL")
                                iInitialMarginTopSymbol = Convert.ToInt32(sValue);
                            if (sElemtName == "MARGIN_COLUMN")
                                iMarginColumn = Convert.ToInt32(sValue);
                            if (sElemtName == "FONT_SIZE")
                                iFontSize = Convert.ToInt32(sValue);
                            if (sElemtName == "FONT_COLOR")
                                iFontColor = Convert.ToInt32(sValue);
                            if (sElemtName == "TEXT_WIDTH")
                                iTextWidth = Convert.ToInt32(sValue);
                            // Motion SensorDefault Settings :: Begin
                            if (sElemtName == "MSENSOR_DEFAULT_DELAY")
                                iMsensorDefaultDelay = Convert.ToInt32(sValue);
                            if (sElemtName == "MSENSOR_DEFAULT_FROM_HOUR")
                                iMsensorDefaultFromHour = Convert.ToInt32(sValue);
                            if (sElemtName == "MSENSOR_DEFAULT_FROM_MINUTE")
                                iMsensorDefaultFromMinute = Convert.ToInt32(sValue);
                            if (sElemtName == "MSENSOR_DEFAULT_TO_HOUR")
                                iMsensorDefaultToHour = Convert.ToInt32(sValue);
                            if (sElemtName == "MSENSOR_DEFAULT_TO_MINUTE")
                                iMsensorDefaultToMinute = Convert.ToInt32(sValue);
                            // Motion SensorDefault Settings :: End
                            if (sElemtName == "AV_SOURCE_ID")
                            {
                                myLstPrm = sValue.Split(';');
                                lstSources.Add(myLstPrm[1]);

                                lstSourceID.Add(Convert.ToInt32(myLstPrm[0]));

                            }
                            if (sElemtName == "LCS_SERVER")
                            {
                                myLstPrm = sValue.Split(';');
                                this.sServerAddress = myLstPrm[0];
                                this.iServerPort = Convert.ToInt32(myLstPrm[1]);

                            }

                            // Global :: Begin
                            if (sElemtName == "GLOBAL_LIGHTS_DESCRIPTION")
                                sGlobalLightsDescription = sValue;
                            if (sElemtName == "GLOBAL_BLINDS_DESCRIPTION")
                                sGlobalBlindsDescription = sValue;
                            if (sElemtName == "GLOBAL_AC_DESCRIPTION")
                                sGlobalACDescription = sValue;
                            if (sElemtName == "GLOBAL_FAHU_DESCRIPTION")
                                sGlobalFAHUDescription = sValue;
                            if (sElemtName == "GLOBAL_MSENSOR_DESCRIPTION")
                                sGlobalMSensorDescription = sValue;
                            // Global :: End

                            if (sElemtName == "ZONE_TAG")
                            {
                                locZone = new CZones();
                                locZone.iZoneTag = Convert.ToInt32(sValue);
                            }
                            if (sElemtName == "ZONE_SERVER")
                            {
                                myLstPrm = sValue.Split(';');
                                for (iCptString = 0; iCptString < myLstPrm.Length; iCptString++)
                                    locZone.lstServerZones.Add(myLstPrm[iCptString]);
                            }
                            if (sElemtName == "ZONE_AC_PRM")
                            {

                                myLstPrm = sValue.Split(';');

                                for (iCptString = 0; iCptString < myLstPrm.Length; iCptString++)
                                    locZone.lstAcPrm.Add(myLstPrm[iCptString]);
                                locZone.ACTCinterface = new CTwinCatInterface(locZone.lstAcPrm[0], Convert.ToInt32(locZone.lstAcPrm[1]));

                            }
                            if (sElemtName == "FLOOR")
                            {
                                locZone.iFloor = Convert.ToInt32(sValue);
                            }
                            if (sElemtName == "AUDIO_ZONE_ID")
                            {
                                locZone.iAVZoneID = Convert.ToInt32(sValue);
                            }
                            if (sElemtName == "TAG")
                            {
                                locDevice = new CDevices();

                                locDevice.iTag = Convert.ToInt32(sValue);
                            }
                            if (sElemtName == "PARAMETERS")
                            {

                                myLstPrm = sValue.Split(';');

                                locDevice.lstPrm.Clear();
                                for (iCptString = 0; iCptString < myLstPrm.Length; iCptString++)
                                    locDevice.lstPrm.Add(myLstPrm[iCptString]);

                                int iControllerIdx = Convert.ToInt32(locDevice.lstPrm[0]);
                                myLstPrm = lstControllerPrm[iControllerIdx].Split(';');
                                locDevice.TCInterface = new CTwinCatInterface(myLstPrm[0], Convert.ToInt32(myLstPrm[1]));
                            }
                            if (sElemtName == "TYPE")
                            {
                                locDevice.iType = Convert.ToInt32(sValue);
                            }
                            if (sElemtName == "VISIBLE")
                            {
                                locDevice.bVisible = Convert.ToBoolean(sValue);
                            }
                            if (sElemtName == "DESCRIPTION")
                            {
                                locDevice.sDescription = sValue;
                            }
                            if (sElemtName == "VALUE")
                            {
                                locDevice.iValue = Convert.ToInt32(sValue);
                                locZone.lstDevices.Add(locDevice);
                            }
                            if (sElemtName == "ZONE_DESCRIPTION")
                            {
                                locZone.sZoneDescription = sValue;
                                lstZones.Add(locZone);
                                bRet = true;
                            }
                            break;
                    }
                }
            }

            return bRet;
        }
    }
}
