using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Middleware
{
    [DataContract]
    public class Message
    {
        [DataMember]
        private bool op_statut;
        [DataMember]
        private string op_name;
        [DataMember]
        private string op_infos;
        [DataMember]
        private string app_name;
        [DataMember]
        private string app_version;
        [DataMember]
        private string app_token;
        [DataMember]
        private string username;
        [DataMember]
        private string user_password;
        [DataMember]
        private string user_token;
        [DataMember]
        private object[] data;

        public bool Op_statut { get => op_statut; set => op_statut = value; }
        public string Op_name { get => op_name; set => op_name = value; }
        public string Op_infos { get => op_infos; set => op_infos = value; }
        public string App_name { get => app_name; set => app_name = value; }
        public string App_version { get => app_version; set => app_version = value; }
        public string App_token { get => app_token; set => app_token = value; }
        public string Username { get => username; set => username = value; }
        public string User_password { get => user_password; set => user_password = value; }
        public string User_token { get => user_token; set => user_token = value; }
        public object[] Data { get => data; set => data = value; }
    }
}
