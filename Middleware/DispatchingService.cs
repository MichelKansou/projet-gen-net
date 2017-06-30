using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DecodeFileLib;
using Middleware.Models;

namespace Middleware
{
    public class DispatchingService : IDispatchingService
    {
        private const String URL_LISTENER = "tcp://172.20.10.14:61616";
        private const String URL_PRODUCER = "tcp://172.20.10.14:61616?jms.useAsyncSend=true";
        private const String QUERY_LISTENER = "decodeResponse";
        private const String QUERY_PRODUCER = "decodeAsk";

        private AuthService auth;
        private User user;
        private DecodeFileService decodeFileService;

        // Initialize Dispatching Service
        public DispatchingService()
        {
            // init authService and DecodeFileService
            auth = new AuthService();
            user = new User();
            decodeFileService = new DecodeFileService(URL_LISTENER, QUERY_LISTENER, URL_PRODUCER, QUERY_PRODUCER);
            Trace.WriteLine("Dispatching initialized");
        }

        // Main WCF dispatcher
        public Response Dispatcher(Message msg)
        {
            Response response = new Response();

            // TODO: remove 
            Trace.WriteLine("Dispatcher called");
            Trace.WriteLine("Operation : " + msg.Operation);
            Trace.WriteLine("app token : " + msg.Application.Name);
            Trace.WriteLine("app token : " + msg.Application.Version);
            Trace.WriteLine("app token : " + msg.Application.Token);
            Trace.WriteLine(msg.Application.Token);

            // check the if the app token is valid
            if (! "zEAxsZ3iNwCfWWn46c".Equals(msg.Application.Token))
            {
                Trace.WriteLine("Someone tried to connect and is not allowed to use our server");
                response.Status = "BAD_APP_TOKENS";
                response.Description = "Your application is not allowed to use our server";
                return response;
            }
            
            Trace.WriteLine("App token validated");
            if ("authentication".Equals(msg.Operation))
            {
                User user = msg.User;
                response.User = auth.Authenticate(user.Username, user.Password);
                if (response.User != null)
                {
                    response.Status = "SUCCESS";
                }
                else
                {
                    response.Status = "INVALID_USER";
                    response.Description = "The username or the password are invalid.";
                }
            }
            else
            {
                // check if the token is valid
                if (auth.CheckToken(msg.UserToken) != null)
                {
                    // execute the matching operation
                    switch (msg.Operation)
                    {
                        case "decode":
                            // decode the file
                            response.DecodeFileOut = decodeFileService.DecodeFile(msg.DecodeFileIn);
                            if (response.DecodeFileOut == null || response.DecodeFileOut.Key == null || response.DecodeFileOut.Key == "")
                            {
                                response.Description = "Impossible to decode the file.";
                                response.Status = "DECODE_IMPOSSIBLE";
                            }
                            else
                            {
                                response.Status = "SUCCESS";
                            }
                            break;
                        default:
                            response.Description = "Operation " + msg.Operation + " doesn't exist";
                            response.Status = "NO_MATCHING_OPERATION";
                            Trace.WriteLine(response.Description);
                            break;
                    }
                }
                else
                {
                    response.Description = "The user token " + msg.UserToken + " is not valid.";
                    response.Status = "INVALID_USER_TOKEN";
                }
            }

            Trace.WriteLine("Return response with status " + response.Status);

            return response;
        }
    }
}
