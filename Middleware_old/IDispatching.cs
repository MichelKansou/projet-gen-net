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
        private bool op_statut;
        private string op_name;
        private string op_infos;
        private string app_name;
        private string app_version;
        private string app_token;
        private string username;
        private string user_password;
        private string user_token;
        private object[] data;

        [DataMember(IsRequired = false)]
        public bool Op_statut { get => op_statut; set => op_statut = value; }

        [DataMember(IsRequired = true)]
        public string Op_name { get => op_name; set => op_name = value; }

        [DataMember(IsRequired = true)]
        public string Op_infos { get => op_infos; set => op_infos = value; }

        [DataMember(IsRequired = true)]
        public string App_name { get => app_name; set => app_name = value; }

        [DataMember(IsRequired = true)]
        public string App_version { get => app_version; set => app_version = value; }

        [DataMember(IsRequired = true)]
        public string App_token { get => app_token; set => app_token = value; }

        [DataMember(IsRequired = false)]
        public string Username { get => username; set => username = value; }

        [DataMember(IsRequired = false)]
        public string User_password { get => user_password; set => user_password = value; }

        [DataMember(IsRequired = false)]
        public string User_token { get => user_token; set => user_token = value; }

        [DataMember(IsRequired = false)]
        public object[] Data { get => data; set => data = value; }
    }
}
