using System;

using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Ground_Floor
{

    static class CConnectionConstants
    {
        public const int Error_None = 0;
        public const int Error_ServerNotAvailable = 1;
        public const int Error_ConnectionRefused = 2;
        public const int Error_ConnectionTimeout = 3;
        public const int Error_NotConnected = 3;
    }

    class CConnectionManager
    {
        private CWinSock _Socket;
        private CMessage _Message;
        private bool _bConnected;
        private string sServerAddress;
        private int iEntrancePort;

        public CConnectionManager(string ServerAddress, int EntrancePort)
        {
            sServerAddress = ServerAddress;
            iEntrancePort = EntrancePort;
        }

        public int ConnectToServer()
        {
            int iReturn;
            byte[] bRcvd = new byte[124];
            bool bConnectionMessageReceived = false;
            bool bConnectionGranted = false;
            bool bTimeOutElapsed = false;
            int StartTime;

            _bConnected = false;
            _Socket = new CWinSock(sServerAddress,iEntrancePort);

            iReturn = 0;
            if (_Socket.Connect())
            {
                //_Message = new CMessage();

                //_Message.SetTextMessage(CMessageConstants.CMD_TXT_ASK_FOR_CONNECTION, sDeviceID.ToCharArray(), 0);
                //_Socket.SendByte(_Message.CreateFrame());
                //Thread.Sleep(250);
                //StartTime = DateTime.Now.Millisecond;

                //while ((!bConnectionMessageReceived) && (!bTimeOutElapsed))
                //{
                //    bRcvd = _Socket.ReceiveByte();
                //    if (_Message.SetMessageFromBuffer(bRcvd, 0))
                //    {
                //        Console.WriteLine("Message received : " + _Message.Command_Type().ToString());
                //        if (_Message.Command_Type() == CMessageConstants.CMD_TYPE_COMMUNICATION)
                //        {
                //            bConnectionMessageReceived = true;

                //            if (_Message.Command_Id() == CMessageConstants.CMD_COMM_CONNECTION_GRANTED)
                //            {
                //                Console.WriteLine("Connection Cranted.");
                //                _Socket.Disconnect();
                //                _Socket.setPort(_Message.GetPrmInt(0));
                //                _Socket.Connect();
                //                bConnectionGranted = true;
                //                iReturn = 0;
                //            }
                //            else
                //            {
                //                bConnectionGranted = false;
                //            }
                //        }
                //        Thread.Sleep(250);
                //    }

                //    if ((DateTime.Now.Millisecond - StartTime) > 5000) bTimeOutElapsed = true;
                //}

                //if ((!bConnectionGranted) && (!bTimeOutElapsed)) 
                //    iReturn = CConnectionConstants.Error_ConnectionRefused;
                

                //if (bTimeOutElapsed)
                //    iReturn = CConnectionConstants.Error_ConnectionTimeout;

            }
            else
            {
                iReturn = CConnectionConstants.Error_ServerNotAvailable;
            }

            if (iReturn == CConnectionConstants.Error_None) _bConnected = true;

            return iReturn;
        }

        public bool IsConnected()
        {
            return _bConnected;
        }

        public int SendMessage(CMessage MsgToSend)
        {
            int iReturn = CConnectionConstants.Error_None;

            iReturn = this.ConnectToServer();

            if (!_bConnected)
            {
                iReturn = CConnectionConstants.Error_NotConnected;
            }
            else
            {
                _Socket.SendByte(MsgToSend.CreateFrame());
            }

            _Socket.Disconnect();

            return iReturn;
        }

        public CMessage ReceiveMessage()
        {
            CMessage MessageReturn = null;
            byte[] bRcvd = new byte[124];

            if (_bConnected)
            {
                MessageReturn = new CMessage();
                bRcvd = _Socket.ReceiveByte();
                if (!MessageReturn.SetMessageFromBuffer(bRcvd, 0)) MessageReturn = null;
            }            

            return MessageReturn;
        }

    }
}
