using System;
using System.Collections.Generic;
using Apache.NMS;
using Apache.NMS.ActiveMQ;
using System.Diagnostics;

namespace DecodeFileLib
{
    class JmsListener
    {
        private IConnection connection;
        private ISession session;
        private IDestination dest;
        private IMessageConsumer consumer;
        private Dictionary<String, DecodeResponse> responses;

        public Dictionary<String, DecodeResponse> Responses
        {
            get {
                Trace.WriteLine("count : " + this.responses.Count);
                return this.responses; }
            set { this.responses = value; }
        }

        // Initialize JMS Listner 
        public JmsListener(String uri, String queue)
        {
            IConnectionFactory factory = new ConnectionFactory(uri);
            connection = factory.CreateConnection();
            connection.Start();
            session = connection.CreateSession();
            dest = session.GetQueue(queue);
            consumer = session.CreateConsumer(dest);
            consumer.Listener += new MessageListener(OnMessage);
            responses = new Dictionary<string, DecodeResponse>();
        }

        // Get Message from Queue   
        private void OnMessage(IMessage receivedMsg)
        {
            ITextMessage textMessage = receivedMsg as ITextMessage;
            DecodeResponse response = new DecodeResponse();
            response.FileName = textMessage.Properties.GetString("fileName");
            response.Key = textMessage.Properties.GetString("key");
            response.Secret = textMessage.Properties.GetString("secret");
            response.Text = textMessage.Text;
            response.Ratio = textMessage.Properties.GetFloat("ratio");

            Trace.WriteLine("Received key : " + response.Key);
            Trace.WriteLine("Received Filename : " + response.FileName);

           // response = "".Equals(response.Key) ? null : response;

            if ("".Equals(response.Key))
            {
                Trace.WriteLine("Key is null");
            }

            DecodeFileService.AddResponses(response.FileName, response);
        }
    }
}
