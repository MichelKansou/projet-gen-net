using Middleware.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Middleware.Dao
{
    class UserDao
    {
        private SqlConnection connection;

        public UserDao()
        {
            this.connection = new SqlConnection("Data Source=DESKTOP-EQTGMTD;Initial Catalog=projet_gen;Integrated Security=True");
        }
    
        // find a user by username and password
        public User FindByUsernameAndPassword(String username, String password)
        {
            String request = "Select * from users where username=@username and password=@password";
            User user = null;

            // check if connectionString is not null;
            CheckDataSource();

            using (this.connection)
            {
                SqlCommand cmd = new SqlCommand(request, connection);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                connection.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        user = new User()
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

            return user;
        }

        // find a user by token
        public User FindByToken(String token)
        {
            String request = "Select * from users where user_token=@token";
            User user = null;

            // check if connectionString is not null;
            CheckDataSource();

            using (this.connection)
            {
                SqlCommand cmd = new SqlCommand(request, connection);
                cmd.Parameters.AddWithValue("@token", token);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        user = new User()
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

            return user;
        }
       
        // update the token, token expiration date and last connexion
        public void UpdateUser(User user)
        {
            // check if connectionString is not null;
            CheckDataSource();

            using (connection)
            {
                String request = "UPDATE users " +
                    "SET user_token=@userToken, last_connection=@lastConnection, token_expiration=@tokenExpiration " +
                    "WHERE username=@username and password=@password";
                SqlCommand cmd = new SqlCommand(request, connection);
                cmd.Parameters.AddWithValue("@userToken", user.Token);
                cmd.Parameters.AddWithValue("@lastConnection", user.LastConnection);
                cmd.Parameters.AddWithValue("@tokenExpiration", user.TokenExpiration);
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

        // update the token expiration date
        public void UpdateExpirationDate(User user)
        {
            // check if connectionString is not null;
            CheckDataSource();

            using (connection)
            {
                string request = "UPDATE users SET token_expiration=@tokenExpiration where user_token=@token";
                SqlCommand cmd = new SqlCommand(request, connection);
                cmd.Parameters.AddWithValue("@token", user.Token);
                cmd.Parameters.AddWithValue("@tokenExpiration", user.TokenExpiration);
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

        private void CheckDataSource()
        {
            if ("".Equals(this.connection.ConnectionString))
            {
                this.connection.ConnectionString = "Data Source=DESKTOP-EQTGMTD;Initial Catalog=projet_gen;Integrated Security=True";
            }
        }
    }
}
