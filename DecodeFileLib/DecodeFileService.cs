using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public static Dictionary<String, DecodeResponse> responses;

        public DecodeFileService(String urlListener, String queueListener, String urlProducer, String queueProducer)
        {
            jmsListner = new JmsListener(urlListener, queueListener);
            jmsProducer = new JmsProducer(urlProducer, queueProducer);
            responses = new Dictionary<String, DecodeResponse>();
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
                if (responses.ContainsKey(decodeFile.FileName))
                {
                    Trace.WriteLine("send resp");
                    return checkDecodeFile(decodeFile);
                }
                else
                {
                    String decodedContent = Cesar(decodeFile.Content, i);
                    jmsProducer.Send(decodedContent, decodeFile.FileName, i.ToString(), decodeFile.Md5, 26);
                }
            }
            while (!(responses.ContainsKey(decodeFile.FileName))){}

            Trace.WriteLine("Finished");

            if (responses.ContainsKey(decodeFile.FileName))
            {
                return checkDecodeFile(decodeFile);
            }
            else
            {
                return null;
            }
        }

        private DecodeFileOut checkDecodeFile(DecodeFileIn decodeFile)
        {
            // return 
            DecodeFileOut response = new DecodeFileOut();
            response.FileName = decodeFile.FileName;
            response.Key = responses[decodeFile.FileName].Key;
            response.Secret = responses[decodeFile.FileName].Secret;

            Trace.WriteLine("clear");
            // clear the dictionary
            responses.Remove(decodeFile.FileName);

            return response;

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

        public static void AddResponses(String fileName, DecodeResponse response)
        {
            responses.Add(fileName, response);
        }
    }
}
