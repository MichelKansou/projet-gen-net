using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Middleware
{
    public class AuthService : IAuthService
    {
        private SqlConnection connection;

        public AuthService()
        {
            connection = new SqlConnection("Data Source=DESKTOP-EQTGMTD;Initial Catalog=projet_gen;Integrated Security=True");
            Trace.WriteLine("AuthService initialize");
        }

        public bool Authenticate(string username, string password)
        {
            return CheckUser(username, password);
        }

        public bool CheckUser(string username, string password)
        {
            this.connection = new SqlConnection("Data Source=DESKTOP-EQTGMTD;Initial Catalog=projet_gen;Integrated Security=True");
            User matchingUser = null;
            using (connection)
            {
                string request = "Select * from users where username=@username and password=@password";
                SqlCommand cmd = new SqlCommand(request, connection);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        matchingUser = new User()
                        {
                            Username = reader["username"].ToString(),
                            Password = reader["password"].ToString()
                        };
                    }
                    connection.Close();
                }
            }
            return matchingUser != null ? true : false;
        }
    }
}
