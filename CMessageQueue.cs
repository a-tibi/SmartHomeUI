using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Ground_Floor
{
    class CMessageQueue
    {
        private CMessage ReplyMessage;
        private CMessage MessageToStore;
        private Mutex LocRcvdMutex;
        private Mutex LocSentMutex;
        private List<CMessage> SentMessageQueue;
        private List<CMessage> RcvdMessageQueue;
        private bool _isQueueForMessagesToSend;

        public CMessageQueue(bool isQueueForMessagesToSend)
        {

            _isQueueForMessagesToSend = isQueueForMessagesToSend;
	        RcvdMessageQueue = new List<CMessage>();
	
	        SentMessageQueue = new List<CMessage>();


            ReplyMessage = new CMessage();

            LocRcvdMutex = new Mutex();
            LocSentMutex = new Mutex();
	
        }
	


        public CMessage PushMessage(CMessage IncommingMessage,bool ToSendBuffer)
        {



            

	        List<CMessage> MessageQueue;
            Mutex locMutex;
	
	        if (ToSendBuffer) {
		        MessageQueue = SentMessageQueue;
                locMutex = LocSentMutex;
	        }
	        else {
		        MessageQueue = RcvdMessageQueue;
                locMutex = LocRcvdMutex;
	        }


            if (IncommingMessage.Command_Type() == CMessageConstants.CMD_TYPE_REPLY)
            {
		        AckMessage(IncommingMessage);
                ReplyMessage.SetMessage(0, CMessageConstants.CMD_TYPE_NONE, IncommingMessage.GetSourceThreadNo());
	        }
	        else {
		        MessageToStore = new CMessage();

                
		        MessageToStore.CopyMessage(IncommingMessage);

                locMutex.WaitOne();
		        MessageQueue.Add(MessageToStore);
                locMutex.ReleaseMutex();

                ReplyMessage.SetMessage(0, CMessageConstants.CMD_TYPE_REPLY,IncommingMessage.GetSourceThreadNo());
                ReplyMessage.AddIntPrm(IncommingMessage.Command_Id());
		        ReplyMessage.AddIntPrm(IncommingMessage.CheckSum());

		        
	        }
            

            return ReplyMessage;
        }

        public void AckMessage(CMessage IncommingMessage)
        {
	
	        int Cmd_Id;
	        int ChkSm;
	        CMessage Message;
	
	        for (int iCpt=0;iCpt<SentMessageQueue.Count;iCpt++){
		        Message = SentMessageQueue[iCpt];
		        Cmd_Id = Message.Command_Id();
		        ChkSm = Message.CheckSum();
                if ((Cmd_Id == IncommingMessage.GetPrmInt(0)) && (ChkSm == IncommingMessage.GetPrmInt(1)))
                {
                    LocSentMutex.WaitOne();
                    SentMessageQueue.RemoveAt(iCpt);
                    LocSentMutex.ReleaseMutex();
		        }
	        }
	
        }



    public CMessage PullMessage()
    {
	
	    CMessage ReturnMessage = new CMessage();
	    


	    if (RcvdMessageQueue.Count>0) {
            
		    ReturnMessage.CopyMessage(RcvdMessageQueue[0]);

            LocRcvdMutex.WaitOne();
		    RcvdMessageQueue.RemoveAt(0);
            LocRcvdMutex.ReleaseMutex();

            return ReturnMessage;
	    }
	    else {
		    return null;
	    }

    }
	

    





    }
}
