﻿using Middleware.Dao;
using Middleware.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;

namespace Middleware
{
    public class AuthService : IAuthService
    {
        private UserDao daoUser;

        // Initialize AuthService
        public AuthService()
        {
            this.daoUser = new UserDao();
        }

        // Authenticate user with valide info
        public User Authenticate(String username, String password)
        {
            password = generateHash(password);

            // check if the username and password are valid
            User user = this.daoUser.FindByUsernameAndPassword(username, password);
            if (user == null) return null;
            if (user.Token == null || user.TokenExpiration < DateTime.Now)
            {
                // generate a new token
                user = GenerateToken(user);
            }
            else
            {
                // increase token expiration
                user.TokenExpiration = DateTime.Now.AddMinutes(30);
                this.daoUser.UpdateExpirationDate(user);
            }
            
            return user;
        }

        // Check if connected user has a valid token
        public User CheckToken(String token)
        {
            Trace.WriteLine("Token from checkToken : " + token);
            if (token == null) return null;
            User user = this.daoUser.FindByToken(token);

            int result = DateTime.Compare(user.TokenExpiration, DateTime.Now);

            if (user == null || result < 0)
            {
                return null;
            }
            else
            {
                // increase token expiration
                user.TokenExpiration = DateTime.Now.AddMinutes(30);
                this.daoUser.UpdateExpirationDate(user);
            }
            return user;
        }

        // Generate Hash for user password
        private String generateHash(String text)
        {
            // given, a password in a string
            string password = @text;

            // byte array representation of that string
            byte[] encodedPassword = new UTF8Encoding().GetBytes(password);

            // need MD5 to calculate the hash
            byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);

            // string representation (similar to UNIX format)
            return BitConverter.ToString(hash)
               // without dashes
               .Replace("-", string.Empty)
               // make lowercase
               .ToLower();
        }

        // Generate User Token
        private User GenerateToken(User user)
        {
            Guid g = Guid.NewGuid();
            String generatedToken = Convert.ToBase64String(g.ToByteArray());
            generatedToken = generatedToken.Replace("=", "");
            generatedToken = generatedToken.Replace("+", "");
            generatedToken = generatedToken.Replace("/", "");

            // map the user with new token information
            user.Token = generatedToken;
            user.TokenExpiration = DateTime.Now.AddMinutes(30);
            user.LastConnection = DateTime.Now;

            // update the user
            this.daoUser.UpdateUser(user);

            return user;
        }
    }
}
