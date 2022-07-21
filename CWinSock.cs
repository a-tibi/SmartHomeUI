using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Ground_Floor
{
    class CWinSock
    {
        public bool bDisconnected;

        public CWinSock(string sServer, int iPort)
        {
            byte[] iPAddr = new byte[4];
            string[] tabstring = sServer.Split('.');
            for (int iCpt = 0; iCpt <= 3; iCpt++)
                iPAddr[iCpt] = Convert.ToByte(tabstring[iCpt]);

            _myIp = new IPAddress(iPAddr);
            _iPort = iPort;

            _ipEnd = new IPEndPoint(_myIp, _iPort);

            bDisconnected = false;


        }

        public void setPort(int iPort)
        {
            _iPort = iPort;
            _ipEnd = new IPEndPoint(_myIp, _iPort);
        }

        public void Disconnect()
        {
            _myServer.Close();
        }

        public void DisConnect()
        {
            IAsyncResult res = null;
            _myServer.EndConnect(res);
            _myServer = null;
        }

        public bool Connect()
        {
            bool bReturn = false;

            try
            {
                _myServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                _myServer.Connect(_ipEnd);
                Console.WriteLine("Connect to socket IpEnd to address " + _ipEnd.Address.ToString() + " port " + _ipEnd.Port.ToString() + ".");
                bReturn = true;
                bDisconnected = false;
            }
            catch
            {
                Console.WriteLine("Failed to Connect to socket IpEnd to address " + _ipEnd.Address.ToString() + " port " + _ipEnd.Port.ToString() + ".");
            }
                
            

            return bReturn;
        }


        public void WaitConnection()
        {
            try
            {
                _myServer.Listen(10);

                Console.WriteLine("Server waiting for connection on port " + _iPort);
                

                _myClient = _myServer.Accept();
                _myClient.Blocking = true;
                _bIsConnected = true;
                
                Console.WriteLine("Client Connected on port " + _iPort);
            }
            catch
            {
                Console.WriteLine("Exception caught while waiting for client connection.");
            }
        }

        


        public byte[] ReceiveByte()
        {
            int iReceived;
            byte[] bBuffer = new byte[124];
            byte[] returnBuffer = null;

            try
            {

                iReceived = _myServer.Receive(bBuffer);
                if (iReceived > 0)
                {
                    returnBuffer = new byte[iReceived];
                    Array.Copy(bBuffer, returnBuffer, iReceived);

                    
                    Console.WriteLine(iReceived.ToString() + " Byte received from client on port " + _iPort.ToString());

                }
                else
                {
                    _bIsConnected = false;
                }
                Thread.Sleep(50);    
                
                
            }
            catch
            {
                Console.WriteLine("Exception caught while waiting to receive data. Client might be disconnected.");
            }


            return returnBuffer;


        }


        public void SendAnswer(string sAnswer)
        {
            byte[] bBuffer;
            char[] aChar;
            int iCptChar;

            aChar = new char[sAnswer.Length];
            bBuffer = new byte[sAnswer.Length];

            aChar = sAnswer.ToCharArray();
            for (iCptChar = 0; iCptChar < sAnswer.Length; iCptChar++)
                bBuffer[iCptChar] = (byte)aChar[iCptChar];

            try
            {
                _myClient.Send(bBuffer);
            }
            catch
            {
                Console.WriteLine("Exception caught while sending data. Client might be disconnected.");
            }    
        }


        public void SendByte(byte[] cAnswer)
        {
            try
            {
                _myServer.Send(cAnswer);
            }
            catch
            {
                Console.WriteLine("Exception caught while sending data buffer. Client might be disconnected. Attempt to reconnect.");
                bDisconnected = true;
                Connect();
            }
        }

        public bool IsClientConnected()
        {
            if (_myClient == null)
            {
                return false;
            }
            else
            {
                
                //return _myClient.Connected;
                return _bIsConnected;
            }
        }


       


        private IPAddress _myIp;
        private IPEndPoint _ipEnd;
        private Socket _myServer;
        private Socket _myClient;
        private int _iPort;
        private bool _bIsConnected;
    }
}
