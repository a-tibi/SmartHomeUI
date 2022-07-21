using System;
using System.Collections.Generic;
using System.Text;
using TwinCAT.Ads;

namespace Ground_Floor
{
    public struct _strTIMESTRUCT
    {
        public UInt16 wYear;
        public UInt16 wMonth;
        public UInt16 wDayOfWeek;
        public UInt16 wDay;
        public UInt16 wHour;
        public UInt16 wMinute;
        public UInt16 wSecond;
        public UInt16 wMilliseconds;
    }

    class CDigitalInputForServer
    {
        private bool[] _abState;
        private bool[] _abPreviousState;
        private int[] _aiDeviceID;
        private bool[] _abVolumeUp;
        private long _lIdxGroup;
        private long _lInitIdxOffset;
        private int _iCount;

        public CDigitalInputForServer(int iNbInput, long IdxGroup, long InitIdxOffset)
        {
            _abState = new bool[iNbInput];
            _abPreviousState = new bool[iNbInput];
            _abVolumeUp = new bool[iNbInput];
            _aiDeviceID = new int[iNbInput];
            _lIdxGroup = IdxGroup;
            _lInitIdxOffset = InitIdxOffset;
            _iCount = iNbInput;
        }

        public void SetDeviceID(int iIDx, int iDeviceID)
        {
            _aiDeviceID[iIDx] = iDeviceID;
        }

        public void SetFunction(int iIdx, bool bVolumeUp)
        {
            _abVolumeUp[iIdx] = bVolumeUp;
        }

        public bool IsForVolumeUp(int iIdx)
        {
            return _abVolumeUp[iIdx];
        }

        public int GetDeviceID(int iIDx)
        {
            return _aiDeviceID[iIDx];
        }

        public int GetCount()
        {
            return _iCount;
        }

        public long GetIdxGroup()
        {
            return _lIdxGroup;
        }

        public long GetIdxOffset(int iIdx)
        {
            return (_lInitIdxOffset + iIdx);
        }

        public void SetValue(int iIdx, bool bValue)
        {
            _abPreviousState[iIdx] = _abState[iIdx];
            _abState[iIdx] = bValue;
        }

        public bool GetValue(int iIdx)
        {
            return _abState[iIdx];
        }

        public bool HasChanged(int iIdx)
        {
            return (_abPreviousState[iIdx] != _abState[iIdx]);
        }
    }

    class CTwinCatInterface
    {
        //public members

        //Constructor
        public CTwinCatInterface(string sNetID, int iPortNo)
        {
            _sNetID = sNetID;
            _iSvrPort = iPortNo;

            _cTcAds = new TcAdsClient();
        }

        public _strTIMESTRUCT GetDateTime(string sVarName)
        {
            int iHandle = 0;
            _strTIMESTRUCT strValue = new _strTIMESTRUCT();
            Connect();

            try
            {
                if (_cTcAds.IsConnected)
                {
                    iHandle = _cTcAds.CreateVariableHandle(sVarName);

                    strValue = (_strTIMESTRUCT)_cTcAds.ReadAny(iHandle, typeof(_strTIMESTRUCT));

                    _cTcAds.DeleteVariableHandle(iHandle);


                }
                else
                {
                    Console.WriteLine("Attempting to read var name: " + sVarName + " to an unconnected server.");
                }
            }
            catch
            {
                Console.WriteLine("Exception caught while attempting to read var name: " + sVarName + ".");
            }

            DisConnect();

            return strValue;
        }

        public void SetDateTime(string sVarName, _strTIMESTRUCT strDTValue)
        {
            int iHandle = 0;
            Connect();

            try
            {

                if (_cTcAds.IsConnected)
                {
                    iHandle = _cTcAds.CreateVariableHandle(sVarName);

                    _cTcAds.WriteAny(iHandle, strDTValue);

                    _cTcAds.DeleteVariableHandle(iHandle);

                }
                else
                {
                    Console.WriteLine("Attempting to write var name: " + sVarName + " to an unconnected server.");
                }
            }
            catch
            {
                Console.WriteLine("Exception caught while attempting to write var name: " + sVarName + ".");
            }
            DisConnect();
        }

        public void SetBool(string sVarName, bool bValue)
        {
            int iHandle = 0;
            Connect();

            try
            {

                if (_cTcAds.IsConnected)
                {
                    iHandle = _cTcAds.CreateVariableHandle(sVarName);

                    _cTcAds.WriteAny(iHandle, bValue);

                    _cTcAds.DeleteVariableHandle(iHandle);

                }
                else
                {
                    Console.WriteLine("Attempting to write var name: " + sVarName + " to an unconnected server.");
                }
            }
            catch
            {
                Console.WriteLine("Exception caught while attempting to write var name: " + sVarName + ".");
            }

            DisConnect();

        }

        public bool GetBool(string sVarName)
        {
            int iHandle = 0;
            bool bValue = false;

            Connect();

            try
            {
                if (_cTcAds.IsConnected)
                {
                    iHandle = _cTcAds.CreateVariableHandle(sVarName);

                    bValue = (bool)_cTcAds.ReadAny(iHandle, typeof(bool));

                    _cTcAds.DeleteVariableHandle(iHandle);


                }
                else
                {
                    Console.WriteLine("Attempting to read var name: " + sVarName + " to an unconnected server.");
                }
            }
            catch
            {
                Console.WriteLine("Exception caught while attempting to read var name: " + sVarName + ".");
            }

            DisConnect();
            return bValue;

        }

        public void SetDWord(string sVarName, UInt32 uiValue)
        {
            int iHandle = 0;
            Connect();

            try
            {

                if (_cTcAds.IsConnected)
                {
                    iHandle = _cTcAds.CreateVariableHandle(sVarName);

                    _cTcAds.WriteAny(iHandle, uiValue);

                    _cTcAds.DeleteVariableHandle(iHandle);

                }
                else
                {
                    Console.WriteLine("Attempting to write var name: " + sVarName + " to an unconnected server.");
                }
            }
            catch
            {
                Console.WriteLine("Exception caught while attempting to write var name: " + sVarName + ".");
            }
            DisConnect();
        }

        public UInt32 GetDWord(string sVarName)
        {
            uint iRet = 0;
            int iHandle = 0;
            Connect();

            try
            {

                if (_cTcAds.IsConnected)
                {
                    iHandle = _cTcAds.CreateVariableHandle(sVarName);
                    iRet = (uint)_cTcAds.ReadAny(iHandle, typeof(uint));
                    _cTcAds.DeleteVariableHandle(iHandle);


                }
                else
                {
                    Console.WriteLine("Attempting to write var name: " + sVarName + " to an unconnected server.");
                }
            }
            catch
            {
                Console.WriteLine("Exception caught while attempting to write var name: " + sVarName + ".");
            }
            DisConnect();

            return (UInt32)iRet;

        }

        public void SetWord(string sVarName, UInt16 uiValue)
        {
            int iHandle = 0;
            Connect();

            try
            {

                if (_cTcAds.IsConnected)
                {
                    iHandle = _cTcAds.CreateVariableHandle(sVarName);

                    _cTcAds.WriteAny(iHandle, uiValue);

                    _cTcAds.DeleteVariableHandle(iHandle);


                }
                else
                {
                    Console.WriteLine("Attempting to write var name: " + sVarName + " to an unconnected server.");
                }
            }
            catch
            {
                Console.WriteLine("Exception caught while attempting to write var name: " + sVarName + ".");
            }
            DisConnect();
        }

        public UInt16 GetWord(string sVarName)
        {
            ushort iRet = 0;
            int iHandle = 0;
            Connect();

            try
            {

                if (_cTcAds.IsConnected)
                {
                    iHandle = _cTcAds.CreateVariableHandle(sVarName);
                    iRet = (ushort)_cTcAds.ReadAny(iHandle, typeof(ushort));
                    _cTcAds.DeleteVariableHandle(iHandle);


                }
                else
                {
                    Console.WriteLine("Attempting to write var name: " + sVarName + " to an unconnected server.");
                }
            }
            catch
            {
                Console.WriteLine("Exception caught while attempting to write var name: " + sVarName + ".");
            }
            DisConnect();

            return (UInt16)iRet;

        }

        public void SetByte(string sVarName, byte bValue)
        {
            int iHandle = 0;
            Connect();

            try
            {
                if (_cTcAds.IsConnected)
                {
                    iHandle = _cTcAds.CreateVariableHandle(sVarName);

                    _cTcAds.WriteAny(iHandle, bValue);

                    _cTcAds.DeleteVariableHandle(iHandle);


                }
                else
                {
                    Console.WriteLine("Attempting to write var name: " + sVarName + " to an unconnected server.");
                }
            }
            catch
            {
                Console.WriteLine("Exception caught while attempting to write var name: " + sVarName + ".");
            }
            DisConnect();
        }

        public byte GetByte(string sVarName)
        {
            int iHandle = 0;
            byte bValue = 0;

            Connect();

            try
            {
                if (_cTcAds.IsConnected)
                {
                    iHandle = _cTcAds.CreateVariableHandle(sVarName);

                    bValue = (byte)_cTcAds.ReadAny(iHandle, typeof(bool));

                    _cTcAds.DeleteVariableHandle(iHandle);


                }
                else
                {
                    Console.WriteLine("Attempting to read var name: " + sVarName + " to an unconnected server.");
                }
            }
            catch
            {
                Console.WriteLine("Exception caught while attempting to read var name: " + sVarName + ".");
            }
            DisConnect();
            return bValue;

        }

        public short GetCircuitByIdx(int iCircuit)
        {
            Connect();
            short iRet;
            long idxOffset;

            idxOffset = 20 + ((iCircuit - 1) * 2);

            iRet = (short)_cTcAds.ReadAny(61488, idxOffset, typeof(short));

            DisConnect();
            return iRet;
        }

        public void SetCircuitByIdx(int iCircuit, short iCircuitValue)
        {
            Connect();

            long idxOffset;

            idxOffset = 20 + ((iCircuit - 1) * 2);

            _cTcAds.WriteAny(61488, idxOffset, iCircuitValue);

            DisConnect();
        }

        public void SwitchAllGroupOff()
        {
            Connect();

            long idxOffset;

            idxOffset = 256;

            _cTcAds.WriteAny(61488, idxOffset, true);

            DisConnect();


        }

        public void SwitchOFFGroupByIdx(int iGroupNo)
        {
            Connect();

            long idxOffset;

            idxOffset = 236 + (iGroupNo - 1);

            _cTcAds.WriteAny(61488, idxOffset, true);

            DisConnect();
        }

        public bool GetContactorByIndex(int iCtcNo)
        {
            Connect();
            bool bRet;
            long idxOffset;

            idxOffset = iCtcNo - 1;

            bRet = (bool)_cTcAds.ReadAny(61488, idxOffset, typeof(bool));

            DisConnect();
            return bRet;
        }

        public bool GetBoolByIndex(long iIdxGroup, long iIdxOffset)
        {
            Connect();
            bool bRet;

            bRet = (bool)_cTcAds.ReadAny(iIdxGroup, iIdxOffset, typeof(bool));

            DisConnect();
            return bRet;
        }

        public void SetContactorByIndex(int iCtcNo, bool bValue)
        {
            Connect();

            long idxOffset;

            if (bValue)
            {
                idxOffset = 80 + (iCtcNo - 1);
            }
            else
            {
                idxOffset = 100 + (iCtcNo - 1);
            }

            _cTcAds.WriteAny(61488, idxOffset, true);

            DisConnect();
        }

        public void SetSceneValueByIdx(int iSceneNo, int iCircuitNo, UInt16 iValue)
        {
            Connect();

            long idxOffset;

            idxOffset = (80 + ((iSceneNo - 1) * 34)) + ((iCircuitNo) * 2);

            _cTcAds.WriteAny(61488, idxOffset, iValue);

            DisConnect();

        }

        public void RecallSceneByIdx(int iSceneNo)
        {
            Connect();

            long idxOffset;

            idxOffset = 216 + (iSceneNo - 1);

            _cTcAds.WriteAny(61488, idxOffset, true);

            DisConnect();

        }

        public void SetSceneNbCircuitByIdx(int iSceneNo, byte iValue)
        {
            Connect();

            long idxOffset;

            idxOffset = 112 + ((iSceneNo - 1) * 34);

            _cTcAds.WriteAny(61488, idxOffset, iValue);

            DisConnect();
        }

        public void SetScene(int iSceneIndex, int iCircuitIndex, int iDimmingValue)
        {
            //this.SetWord(".aScenes[" + iSceneIndex.ToString() + "].aCommandOutput[" + iCircuitIndex.ToString() + "]", (UInt16)iDimmingValue);
            this.SetSceneValueByIdx(iSceneIndex, iCircuitIndex, (UInt16)iDimmingValue);
        }

        public short GetShort(string sVarName)
        {
            int iHandle = 0;
            short iValue = 0;

            Connect();

            try
            {
                if (_cTcAds.IsConnected)
                {
                    iHandle = _cTcAds.CreateVariableHandle(sVarName);

                    iValue = (short)_cTcAds.ReadAny(iHandle, typeof(short));

                    _cTcAds.DeleteVariableHandle(iHandle);


                }
                else
                {
                    Console.WriteLine("Attempting to read var name: " + sVarName + " to an unconnected server.");
                }
            }
            catch
            {
                Console.WriteLine("Exception caught while attempting to read var name: " + sVarName + ".");
            }
            DisConnect();
            return iValue;

        }

        public void SetShort(string sVarName, short iValue)
        {
            int iHandle = 0;
            Connect();

            try
            {
                if (_cTcAds.IsConnected)
                {
                    iHandle = _cTcAds.CreateVariableHandle(sVarName);

                    _cTcAds.WriteAny(iHandle, iValue);

                    _cTcAds.DeleteVariableHandle(iHandle);


                }
                else
                {
                    Console.WriteLine("Attempting to write var name: " + sVarName + " to an unconnected server.");
                }
            }
            catch
            {
                Console.WriteLine("Exception caught while attempting to write var name: " + sVarName + ".");
            }
            DisConnect();
        }

        //private members
        private TcAdsClient _cTcAds;
        private int _iSvrPort;
        private string _sNetID;

        private void Connect()
        {

            try
            {

                _cTcAds.Connect(_sNetID, _iSvrPort);
                if (_cTcAds.IsConnected)
                {
                    //Console.WriteLine("Connected to " + _sNetID + ", to port " + _iSvrPort + ".");
                }
                else
                {
                    Console.WriteLine("Error while attempting to connect to " + _sNetID + ", to port " + _iSvrPort + ".");
                }
            }
            catch
            {
                Console.WriteLine("Exception caught while attempting to connect to " + _sNetID + ", to port " + _iSvrPort + ".");
            }
        }

        private void DisConnect()
        {
            _cTcAds.Dispose();
        }

    }
}
