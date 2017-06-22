using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace AuthService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AuthService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select AuthService.svc or AuthService.svc.cs at the Solution Explorer and start debugging.
    public class AuthService : IAuthService
    {
        private SqlConnection connection;
        public AuthService()
        {
            this.connection = new SqlConnection("Data Source=DESKTOP-EQTGMTD;Initial Catalog=projet_gen;Integrated Security=True");
        }
        public bool AuthSerivce(string username, string password)
        {
            return CheckUser(username, password);
        }

        public bool CheckUser(string username, string password)
        {
            User matchingUser = null;
            using(this.connection)
            {
                string request = "Select * from users where username=@username and password=@password";
                SqlCommand cmd = new SqlCommand(request, connection);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        matchingUser = new User();
                        matchingUser.UserName = reader["username"].ToString();
                        matchingUser.Password = reader["password"].ToString();
                    }
                    connection.Close();
                }
            }
            return matchingUser != null ? true : false;
        }
    }
}
