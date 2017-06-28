using System;
using Apache.NMS;
using Apache.NMS.Util;
using Apache.NMS.ActiveMQ;
using Apache.NMS.ActiveMQ.Commands;


namespace DecodeFileLib
{
    class JmsProducer
    {
        private IConnection connection;
        private ISession session;
        private IDestination dest;
        //private IMessageProducer producer;

        public JmsProducer(String uri, String queue)
        {
            IConnectionFactory factory = new ConnectionFactory(uri);
            connection = factory.CreateConnection();
            connection.Start();
            session = connection.CreateSession();
            dest = session.GetQueue(queue);
           // producer = session.CreateProducer(dest);
            //producer.DeliveryMode()
        }

        public void Send(String message, String fileName, String key, String md5, int maxLoop)
        {
            using (IMessageProducer producer = session.CreateProducer(dest))
            {
                var textMessage = producer.CreateTextMessage(message);
                textMessage.Properties.SetString("fileName", fileName);
                textMessage.Properties.SetString("key", key);
                textMessage.Properties.SetString("md5", md5);
                textMessage.Properties.SetInt("maxLoop", maxLoop);
                producer.Send(textMessage);
            }
        }

        public void ShutDown()
        {
            session.Close();
            connection.Close();
        }
    }
}
