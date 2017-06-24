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
        public User user;

        public AuthService()
        {
            user = new User();
            connection = new SqlConnection("Data Source=DESKTOP-EQTGMTD;Initial Catalog=projet_gen;Integrated Security=True");
            Trace.WriteLine("AuthService initialize");
        }

        public bool Authenticate(string username, string password)
        {

            return CheckUser(username, password) ? CheckToken() : false;
        }

        public bool CheckToken()
        {
            if (user.Token != null)
            {
                // TODO : check if token is not expired
                using (connection)
                {
                    string request = "Select * from users where user_token=@userToken";
                    SqlCommand cmd = new SqlCommand(request, connection);
                    cmd.Parameters.AddWithValue("@userToken", user.Token);
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        DateTime currentTime = DateTime.Now;
                        while (reader.Read())
                        {
                            int result = DateTime.Compare((DateTime)reader["token_expiration"], currentTime);
                            if (result < 0)
                            {
                                GenerateToken();
                            }
                        }
                        connection.Close();
                    }
                }
            }
            else
            {
                GenerateToken();
            }
            return true;   
        }


        private void GenerateToken()
        {
            Guid g = Guid.NewGuid();
            string generatedToken = Convert.ToBase64String(g.ToByteArray());
            generatedToken = generatedToken.Replace("=", "");
            generatedToken = generatedToken.Replace("+", "");

            using (connection)
            {
                string request = "INSERT into users (user_token, last_connection, token_expiration) VALUES (@userToken, @lastConnection, @tokenExpiration) where username=@username and password=@password";
                SqlCommand cmd = new SqlCommand(request, connection);
                cmd.Parameters.AddWithValue("@userToken", generatedToken);
                cmd.Parameters.AddWithValue("@lastConnection", DateTime.Now);
                cmd.Parameters.AddWithValue("@tokenExpiration", DateTime.Now.AddMinutes(30));
                cmd.Parameters.AddWithValue("@username", user.Username);
                cmd.Parameters.AddWithValue("@password", user.Password);
                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException sqlEx)
                {
                    Trace.WriteLine("SQL query : " + sqlEx.ToString());
                }
                finally
                {
                    connection.Close();
                }
            }
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
                            Password = reader["password"].ToString(),
                            Token = reader["user_token"].ToString(),
                            LastConnection = (DateTime)reader["last_connection"],
                            TokenExpiration = (DateTime)reader["token_expiration"]
                        };
                    }
                    connection.Close();
                }
            }
            this.user = matchingUser;
            return matchingUser != null ? true : false;
        }
    }
}
