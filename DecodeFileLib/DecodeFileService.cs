using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DecodeFileLib
{
    public class DecodeFileService : IDecodeFileService
    {
        private JmsListener jmsListner;
        private JmsProducer jmsProducer;

        public DecodeFileService(String urlListener, String queueListener, String urlProducer, String queueProducer)
        {
            jmsListner = new JmsListener(urlListener, queueListener);
            jmsProducer = new JmsProducer(urlProducer, queueProducer);
        }

        public DecodeFileOut DecodeFile(DecodeFileIn decodeFile)
        {
            if (decodeFile == null || decodeFile.Content == null || 
                decodeFile.FileName == null || decodeFile.Md5 == null )
            {
                return null;
            }

            for (int i = 0; i < 26; i++)
            {
                if (jmsListner.Responses.ContainsKey(decodeFile.FileName))
                {
                    // clear the dictionary
                    jmsListner.Responses.Remove(decodeFile.FileName);

                    // return 
                    DecodeFileOut response = new DecodeFileOut();
                    response.FileName = decodeFile.FileName;
                    response.Key = jmsListner.Responses[decodeFile.FileName].Key;
                    response.Secret = jmsListner.Responses[decodeFile.FileName].Secret;
                    return response;
                }
                else
                {
                    String decodedContent = Cesar(decodeFile.Content, i);
                    jmsProducer.Send(decodedContent, decodeFile.FileName, i.ToString(), decodeFile.Md5);
                }
            }
            return null;
        }

        private String Cesar(String text, int key)
        {
            char[] chars = text.ToCharArray();
            for (int i = 0; i < text.Length; i++)
            {
                if ('a' <= chars[i] && chars[i] <= 'z')
                {
                    chars[i] = (char)(int)(((chars[i] - 'a') + key) + 'a');
                    if (chars[i] > 'z')
                        chars[i] = (char)(int)((chars[i] - 'z') + ('a' - 1));
                }
                else
                {
                    if ('A' <= chars[i] && chars[i] <= 'Z')
                    {
                        chars[i] = (char)(int)(((chars[i] - 'A') + key) + 'A');
                        if (chars[i] > 'Z')
                            chars[i] = (char)(int)((chars[i] - 'Z') + ('A' - 1));
                    }
                    else if ('0' <= chars[i] && chars[i] <= '9')
                    {

                        chars[i] = (char)(int)(((chars[i] - '0') + key) + '0');
                        if (chars[i] > '9')
                            chars[i] = (char)(int)((chars[i] - '9') + ('0' - 1));
                    }

                }
            }
            return new String(chars);
        }
    }
}
