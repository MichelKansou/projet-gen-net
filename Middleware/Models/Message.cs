using DecodeFileLib;
using Middleware.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Middleware.Models
{
    [DataContract]
    public class Message
    {
        [DataMember]
        private string operation;
        [DataMember]
        private Application application;
        [DataMember]
        private string userToken;
        [DataMember]
        private User user;
        [DataMember]
        private DecodeFileIn decodeFileIn;

        public string Operation { get => operation; set => operation = value; }
        public Application Application { get => application; set => application = value; }
        public string UserToken { get => userToken; set => userToken = value; }
        public User User { get => user; set => user = value; }
        public DecodeFileIn DecodeFileIn { get => decodeFileIn; set => decodeFileIn = value; }
    }
}
