using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Middleware.Models
{
    [KnownType(typeof(User))]
    [DataContract(IsReference = true)]
    public class User
    {
        [DataMember]
        private string username;
        [DataMember]
        private string password;
        [DataMember]
        private string token;
        [DataMember]
        DateTime lastConnection;
        [DataMember]
        DateTime tokenExpiration;

        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public string Token { get => token; set => token = value; }
        public DateTime LastConnection { get => lastConnection; set => lastConnection = value; }
        public DateTime TokenExpiration { get => tokenExpiration; set => tokenExpiration = value; }
    }
}
