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

        private const int KEY_LENGTH = 6;
        private const string CARACTERES = "abcdefghijklmnopqrstuvwxyz";

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

            XORDecryption xorDecryption = new XORDecryption(CARACTERES, KEY_LENGTH);
            double maxIteration = Math.Pow(CARACTERES.Length, KEY_LENGTH);
            for (int i = 0; i < maxIteration; i++)
            {
                if (responses.ContainsKey(decodeFile.FileName))
                {
                    Trace.WriteLine("send resp");
                    return checkDecodeFile(decodeFile);
                }
                else
                {
                    string key = "ety67";//xorDecryption.FindKey();
                    if (key == null)
                    {
                        jmsProducer.Send("", decodeFile.FileName, "", decodeFile.Md5, 0);
                        break;
                    }
                    else
                    {
                        //key += "67";
                        String decodedContent = xorDecryption.Decrypt(decodeFile.Content, key);
                        jmsProducer.Send(decodedContent, decodeFile.FileName, key, decodeFile.Md5, (int)maxIteration);
                    }
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
            response.Text = responses[decodeFile.FileName].Text;
            response.Ratio = responses[decodeFile.FileName].Ratio;

            Trace.WriteLine("clear");
            // clear the dictionary
            responses.Remove(decodeFile.FileName);

            return response;

        }

        // Do not use this function
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
