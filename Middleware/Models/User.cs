using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Middleware
{
    [DataContract]
    public class User
    {
        [DataMember]
        private string username;
        [DataMember]
        private string password;
        [DataMember]
        private string user_token;
        [DataMember]
        DateTime last_connection;
        [DataMember]
        DateTime token_expiration;

        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public string User_token { get => user_token; set => user_token = value; }
        public DateTime Last_connection { get => last_connection; set => last_connection = value; }
        public DateTime Token_expiration { get => token_expiration; set => token_expiration = value; }
    }
}
