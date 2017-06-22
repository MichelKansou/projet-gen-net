using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace AuthService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAuthService" in both code and config file together.
    [ServiceContract]
    public interface IAuthService
    {

        [OperationContract]
        bool AuthSerivce(string username, string password);

        [OperationContract]
        bool CheckUser(string username, string password);

    }

    [DataContract]
    public class User
    {
        string username = string.Empty;
        string password = string.Empty;
        string user_token = string.Empty;
        DateTime last_connection;
        DateTime token_expiration;

        [DataMember]
        public string UserName
        {
            get { return username; }
            set { username = value; }
        }

        [DataMember]
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        [DataMember]
        public string UserToken
        {
            get { return user_token; }
            set { user_token = value; }
        }

        [DataMember]
        public DateTime LastConnection
        {
            get { return last_connection; }
            set { last_connection = value; }
        }

        [DataMember]
        public DateTime TokenExpiration
        {
            get { return token_expiration; }
            set { token_expiration = value; }
        }
    }

}
