using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Ground_Floor
{
    static class CMessageConstants
    {
        public const int ThreadNoNA = 255;
        public const int EntranceThread = 0;

        public const int CMD_TYPE_REPLY = 0;
        public const int CMD_TYPE_LIGHTING = 1;
        public const int CMD_TYPE_HVAC = 2;
        public const int CMD_TYPE_AV = 3;
        public const int CMD_TYPE_IR = 4;
        public const int CMD_TYPE_MOTOR = 5;
        public const int CMD_TYPE_TEXT = 10;


        public const int CMD_TYPE_COMMUNICATION = 254;
        public const int CMD_TYPE_NONE = 255;

        //CMD_TYPE_LIGHTING
        public const int CMD_LIGHT_SWITCHON = 1;
        public const int CMD_LIGHT_SWITCHOFF = 2;
        public const int CMD_LIGHT_SWITCHTOGGLE = 3;
        public const int CMD_LIGHT_DIMTO = 4;
        public const int CMD_LIGHT_REQUEST_DEV_VALUE = 5;
        public const int CMD_LIGHT_GIVE_DEV_VALUE = 6;

        //CMD_TYPE_AV
        public const int CMD_AV_ALL_ZONE_OFF = 1;
        public const int CMD_AV_ZONE_ON = 2;
        public const int CMD_AV_ZONE_OFF = 3;
        public const int CMD_AV_VOLUME_UP = 4;
        public const int CMD_AV_VOLUME_DOWN = 5;
        public const int CMD_AV_SEND_COMMAND_TO_SOURCE = 14;


        //CMD_TYPE_COMMUNICATION        
        public const int CMD_COMM_CONNECTION_GRANTED = 1;
        public const int CMD_COMM_CONNECTION_REFUSED = 2;
        public const int CMD_COMM_GET_CONFIGURATION = 3;
        public const int CMD_COMM_GIVE_CONF_QTY = 4;
        public const int CMD_COMM_GET_MASTER = 5;
        public const int CMD_COMM_SET_SCENE = 6;
        public const int CMD_COMM_RECALL_SCENE = 7;
        public const int CMD_COMM_GET_REMOTE_CONF = 8;
        public const int CMD_COMM_GIVE_REMOTE_CONF = 9;
        public const int CMD_COMM_SET_AWAY_MODE = 14;
        public const int CMD_COMM_GET_AWAY_MODE = 15;

        //CMD_TYPE_IR
        public const int CMD_IR_SEND_SIGNAL = 1;

        //CMD_TYPE_TEXT
        public const int CMD_TXT_ASK_FOR_CONNECTION = 0;
        public const int CMD_TXT_GIVE_ZONE_PRM = 1;
        public const int CMD_TXT_GIVE_DEVICE_PRM = 2;

        //CMD_TYPE_MOTOR
        public const int CMD_MOTOR_OPEN = 1;
        public const int CMD_MOTOR_CLOSE = 2;
        public const int CMD_MOTOR_STOP = 3;

        // CMD_GLOBAL, add by Ahmed
        public const int CMD_GLOBAL_ALL_LIGHTS_SWITCHON = 1;
        public const int CMD_GLOBAL_ALL_LIGHTS_SWITCHOFF = 2;
        public const int CMD_GLOBAL_ALL_AC_SETTEMP = 3;
        public const int CMD_GLOBAL_ALL_AC_SWITCHOFF = 4;
        public const int CMD_GLOBAL_ALL_AC_AUTO = 5;
        public const int CMD_GLOBAL_FAHU_SWITCHOFF = 6;
        public const int CMD_GLOBAL_FAHU_AUTO = 7;

    }


    class CMessage
    {
        private char si_CMD_ID;
	    private char si_CMD_TYPE;
	    private byte b_NB_INT;
	    private byte b_NB_DBL;
	    private int[] ai_PRM_INT;
	    private float[] ad_PRM_DBL;
	    private char[] cMessage;
	    private byte cChkSm;
	    private bool bAck;
	    private int iCalcChksm;
	    private int iIdxAvailableDblPrm;
	    private int iIdxAvailableIntPrm;
        private int iFromThreadNo;

        //public
        public void AcknowledgeMessage()
        {
            bAck = true;
        }

        public bool IsAck()
        {
            return bAck;
        }

        

        public int GetSourceThreadNo()
        {
            return iFromThreadNo;
        }

        public CMessage()
        {
            ai_PRM_INT = new int[16];
            ad_PRM_DBL = new float[16];
            cMessage = new char[120];
            iIdxAvailableDblPrm = 0;
            iIdxAvailableIntPrm = 0;
        }

        public int CheckSum()
        {
	        return (int)cChkSm;
        }

        public int Command_Id()
        {
	        return (int)si_CMD_ID;
        }


        public int Command_Type()
        {
	        return (int)si_CMD_TYPE;
        }
				 
        public int NbPrmInt()
        {
	        return	(int)b_NB_INT;
        }
				 
        public int NbPrmDbl()
        {
	        return	(int)b_NB_DBL;
        }
				 
        public int GetPrmInt(int idx)
        {
	
	        if (idx<=15) {
		        return ai_PRM_INT[idx];
	        }
	        else {
		        return	0;
	        }
        }
				 
        public double GetPrmDouble(int idx)
        {
					 
	        if (idx<=15) {
		        return ad_PRM_DBL[idx];
	        }
	        else {
			        return	0.0;
	        }
        }


        public bool IsMessageValid()
        {
	        CalculateChkSm();
	        int iRcvdChekSm = (int)cChkSm;
		
	        if (iRcvdChekSm==iCalcChksm) {
		        return true;
	        }
	        else {
		        return false;
	        }
	
        }


        public void CopyMessage(CMessage MsgToCopy)
        {

            //to review, find a way to do a C like memcpy
            si_CMD_ID = MsgToCopy.si_CMD_ID;
            si_CMD_TYPE = MsgToCopy.si_CMD_TYPE;
            b_NB_INT = MsgToCopy.b_NB_INT;
            b_NB_DBL = MsgToCopy.b_NB_DBL;

            for (int iCpt = 0; iCpt < 16; iCpt++) ai_PRM_INT[iCpt] = MsgToCopy.ai_PRM_INT[iCpt];
            for (int iCpt = 0; iCpt < 16; iCpt++) ad_PRM_DBL[iCpt] = MsgToCopy.ad_PRM_DBL[iCpt];
            for (int iCpt = 0; iCpt < 120; iCpt++) cMessage[iCpt] = MsgToCopy.cMessage[iCpt];
                        
            cChkSm = MsgToCopy.cChkSm;

            bAck = MsgToCopy.IsAck();

            iFromThreadNo = MsgToCopy.GetSourceThreadNo();

        }


        public bool AddDblPrm(float dblPrm)
        {
	        if (iIdxAvailableDblPrm<=15) {
		
		        ad_PRM_DBL[iIdxAvailableDblPrm]=dblPrm;
		        iIdxAvailableDblPrm++;
		        b_NB_DBL=(byte)iIdxAvailableDblPrm;
		
		        CalculateChkSm();
		        cChkSm = (byte)iCalcChksm;
		        return true;
		
	        }
	        else {
		        return false;
	        }

        }

        public bool AddIntPrm(int intPrm)
        {
	        if (iIdxAvailableIntPrm<=15) {
		
		        ai_PRM_INT[iIdxAvailableIntPrm]=intPrm;
		        iIdxAvailableIntPrm++;
		        b_NB_INT=(byte)iIdxAvailableIntPrm;
		        CalculateChkSm();
		        cChkSm = (byte)iCalcChksm;
		        return true;
		
	        }
	        else {
		        return false;
	        }
	
        }

        public void SetMessage(int Cmd_Id,int Cmd_Type,int ThreadNumber)
        {
	        iIdxAvailableDblPrm = 0;
	        iIdxAvailableIntPrm = 0;
	        si_CMD_ID=(char)Cmd_Id;
	        si_CMD_TYPE=(char)Cmd_Type;
	        b_NB_DBL=0;
	        b_NB_INT=0;
	        cChkSm=(byte)CalculateChkSm();
            iFromThreadNo = ThreadNumber;
	
        }

        public bool SetMessageFromBuffer(byte[] cReceivedFrame,int ThreadNumber)
        {

            int iCptPrm;
            byte[] TabByte = new byte[4];

            if (cReceivedFrame!=null)
            {
                if (cReceivedFrame.Length == 124)
                {

                    si_CMD_TYPE = (char)(cReceivedFrame[0]);
                    si_CMD_ID = (char)(cReceivedFrame[1]);
                    b_NB_DBL = (byte)((((int)(cReceivedFrame[2])) >> 4) & 15);
                    b_NB_INT = (byte)(((int)(cReceivedFrame[2])) & 15);
                    if ((si_CMD_TYPE == CMessageConstants.CMD_TYPE_TEXT))
                    {
                        for (iCptPrm = 0; iCptPrm < 120; iCptPrm++) cMessage[iCptPrm] = (char)cReceivedFrame[3 + iCptPrm];

                    }
                    else
                    {
                        for (iCptPrm = 0; iCptPrm < (int)b_NB_INT; iCptPrm++)
                        {
                            //Buffer.BlockCopy(cReceivedFrame,3+(iCptPrm*4),ai_PRM_INT,iCptPrm,4);
                            //memcpy(&(ai_PRM_INT[iCptPrm]),&(cReceivedFrame[3+(iCptPrm*4)]),sizeof(int));

                            TabByte[0] = (byte)cReceivedFrame[3 + (iCptPrm * 4)];
                            TabByte[1] = (byte)cReceivedFrame[4 + (iCptPrm * 4)];
                            TabByte[2] = (byte)cReceivedFrame[5 + (iCptPrm * 4)];
                            TabByte[3] = (byte)cReceivedFrame[6 + (iCptPrm * 4)];
                            ai_PRM_INT[iCptPrm] = BitConverter.ToInt32(TabByte, 0);
                        }
                        for (iCptPrm = 0; iCptPrm < (int)b_NB_DBL; iCptPrm++)
                        {
                            //Buffer.BlockCopy(cReceivedFrame,63+(iCptPrm*4),ad_PRM_DBL,iCptPrm,4);
                            //memcpy(&(ad_PRM_DBL[iCptPrm]),&(cReceivedFrame[63+(iCptPrm*4)]),sizeof(float));
                            TabByte[0] = (byte)cReceivedFrame[63 + (iCptPrm * 4)];
                            TabByte[1] = (byte)cReceivedFrame[64 + (iCptPrm * 4)];
                            TabByte[2] = (byte)cReceivedFrame[65 + (iCptPrm * 4)];
                            TabByte[3] = (byte)cReceivedFrame[66 + (iCptPrm * 4)];
                            ad_PRM_DBL[iCptPrm] = BitConverter.ToSingle(TabByte, 0);
                        }
                    }


                    cChkSm = (Byte)cReceivedFrame[123];

                    iFromThreadNo = ThreadNumber;

                    return IsMessageValid();
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
	       

        }

        

        public byte[] CreateFrame()
        {

            byte[] cToSend = new byte[124];
	        int iCptPrm;
            byte[] TabByte = new byte[4];

	        cToSend[0] = (byte)si_CMD_TYPE;
            cToSend[1] = (byte)si_CMD_ID;
            cToSend[2] = (byte)((b_NB_DBL << 4) + (b_NB_INT));
            if ((si_CMD_TYPE == CMessageConstants.CMD_TYPE_TEXT))
            {
               
		        for (iCptPrm=0; iCptPrm<120; iCptPrm++) {
                    cToSend[3+iCptPrm] = (byte)cMessage[iCptPrm];
			        //memcpy(&(cToSend[3+iCptPrm]),&(cMessage[iCptPrm]),sizeof(char));
		        }
	        }else {	
		        for (iCptPrm=0; iCptPrm<(int)b_NB_INT; iCptPrm++) {
                    //Buffer.BlockCopy(ai_PRM_INT, iCptPrm * 4, cToSend, 3 + (iCptPrm * 4), 4);                    
                    TabByte = BitConverter.GetBytes(ai_PRM_INT[iCptPrm]);
                    cToSend[3 + (iCptPrm * 4)] = TabByte[0];
                    cToSend[4 + (iCptPrm * 4)] = TabByte[1];
                    cToSend[5 + (iCptPrm * 4)] = TabByte[2];
                    cToSend[6 + (iCptPrm * 4)] = TabByte[3];
			        //memcpy(&(cToSend[3+(iCptPrm*4)]),&(ai_PRM_INT[iCptPrm]),sizeof(int));
		        }
		        for (iCptPrm=0; iCptPrm<(int)b_NB_DBL; iCptPrm++) {
                    //Buffer.BlockCopy(ad_PRM_DBL, iCptPrm, cToSend, 63 + (iCptPrm * 4), 4);
                    TabByte = BitConverter.GetBytes(ad_PRM_DBL[iCptPrm]);
                    cToSend[63 + (iCptPrm * 4)] = TabByte[0];
                    cToSend[64 + (iCptPrm * 4)] = TabByte[1];
                    cToSend[65 + (iCptPrm * 4)] = TabByte[2];
                    cToSend[66 + (iCptPrm * 4)] = TabByte[3];
			        //memcpy(&(cToSend[63+(iCptPrm*4)]),&(ad_PRM_DBL[iCptPrm]),sizeof(float));
		        }
	        }
	        cToSend[123]=cChkSm;

            return cToSend;
	
        }

        public void SetTextMessage(int Cmd_Id,char[] cMsg,int ThreadNumber)
        {
	        iIdxAvailableDblPrm = 0;
	        iIdxAvailableIntPrm = 0;
	        si_CMD_ID=(char)Cmd_Id;
	        si_CMD_TYPE=(char)CMessageConstants.CMD_TYPE_TEXT;
	        b_NB_DBL=0;
	        b_NB_INT=0;
	        /*if (StrLength(cMsg)<120) {
		        strcpy(cMessage,cMsg);
	        }*/
            Array.Copy(cMsg, cMessage, cMsg.Length);
           
	        cChkSm=(byte)CalculateChkSm();
            iFromThreadNo = ThreadNumber;
        }

        public string GetMessage()
        {
            string sReturn = new string(cMessage);

            return sReturn;
        }


        //private
        private int CalculateChkSm()
        {
	        int iSumChar = 0;
	        int iCptPrm;
	        int iTotPrm;
	
	        iSumChar = (int)si_CMD_ID;
	        iSumChar+= (int)si_CMD_TYPE;
	        iSumChar+= (int)b_NB_INT;
	        iSumChar+= (int)b_NB_DBL;

            if ((si_CMD_TYPE == CMessageConstants.CMD_TYPE_TEXT))
            {
		        iTotPrm = 120;
		        for (iCptPrm=0; iCptPrm<iTotPrm; iCptPrm++) {
			        iSumChar+=(int)cMessage[iCptPrm];
		        }		
	        }
	        else {
		        iTotPrm = (int)b_NB_INT;
		        for (iCptPrm=0; iCptPrm<iTotPrm; iCptPrm++) {
			        iSumChar+=(int)ai_PRM_INT[iCptPrm];
		        }
		        iTotPrm = (int)b_NB_DBL;
		        for (iCptPrm=0; iCptPrm<iTotPrm; iCptPrm++) {
			        iSumChar+=(int)ad_PRM_DBL[iCptPrm];
		        }
	        }
	
	
	
	
	        iCalcChksm=(iSumChar & 255);
	
	        return iCalcChksm;
        }




        
    }
}
