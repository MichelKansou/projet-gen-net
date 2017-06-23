using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Middleware
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IDispatching" in both code and config file together.
    [ServiceContract]
    public interface IDispatching
    {
        [OperationContract]
        MSG dispatcher(MSG msg);
    }

    [DataContract]
    public struct MSG
    {
        [DataMember]
        public bool op_statut;
        [DataMember]
        public string op_name;
        [DataMember]
        public string op_infos;
        [DataMember]
        public string app_name;
        [DataMember]
        public string app_version;
        [DataMember]
        public string app_token;
        [DataMember]
        public string username;
        [DataMember]
        public string user_password;
        [DataMember]
        public string user_token;
        [DataMember]
        public object[] data;
    }
}
