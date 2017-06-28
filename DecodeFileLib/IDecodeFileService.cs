using System;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace DecodeFileLib
{
    [ServiceContract]
    public interface IDecodeFileService
    {
        [OperationContract]
        DecodeFileOut DecodeFile(DecodeFileIn decodeFile);
    }

    [DataContract]
    public class DecodeFileIn
    {
        string fileName;
        string content;
        string md5;

        [DataMember]
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        [DataMember]
        public string Content
        {
            get { return content; }
            set { content = value; }
        }

        [DataMember]
        public string Md5
        {
            get { return md5; }
            set { md5 = value; }
        }
    }

    [DataContract]
    public class DecodeFileOut
    {
        string fileName;
        string key;
        string secret;
        string text;
        float ratio;
        
        [DataMember]
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        [DataMember]
        public string Key
        {
            get { return key; }
            set { key = value; }
        }

        [DataMember]
        public string Secret
        {
            get { return secret; }
            set { secret = value; }
        }

        [DataMember]
        public string Text
        {
            get { return text; }
            set { text = value; }
        }
        [DataMember]
        public float Ratio
        {
            get { return ratio; }
            set { ratio = value; }
        }
    }
}
