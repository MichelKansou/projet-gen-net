using Middleware.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Middleware
{
    [DataContract]
    public struct Message
    {
        [DataMember]
        private string operation;
        [DataMember]
        private Application application;
        [DataMember]
        private string userToken;
        [DataMember]
        private object item;

        public string Operation { get => operation; set => operation = value; }
        public Application Application { get => application; set => application = value; }
        public string UserToken { get => userToken; set => userToken = value; }
        public object Item { get => item; set => item = value; }
    }
}
