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
        private const String URL_LISTENER = "tcp://192.168.48.1:61616";
        private const String URL_PRODUCER = "tcp://192.168.48.1:61616";
        private const String QUERY_LISTENER = "decodeResponse";
        private const String QUERY_PRODUCER = "decodeAsk";

        private AuthService auth;
        private DecodeFileService decodeFileService;

        public DispatchingService()
        {
            // init authService and DecodeFileService
            auth = new AuthService();
            decodeFileService = new DecodeFileService(URL_LISTENER, QUERY_LISTENER, URL_PRODUCER, QUERY_PRODUCER);
            Trace.WriteLine("Dispatching initialized");
        }

        public Response Dispatcher(Message msg)
        {
            Response response = new Response();

            // TODO: remove 
            Trace.WriteLine("Dispatcher called \n");
            Trace.WriteLine("app token : " + msg.Application.Name);
            Trace.WriteLine(msg.Application.Token);

            // check the if the app token is valid
            if (! "zEAxsZ3iNwCfWWn46c".Equals(msg.Application.Token))
            {
                Trace.WriteLine("Someone tried to connect and is not allowed to use our server \n");
                response.Status = "BAD_APP_TOKENS";
                response.Description = "Your application is not allowed to use our server";
                return response;
            }
            
            Trace.Write("App token validated \n");
            if ("authentication".Equals(msg.Operation))
            {
                User user = (User) msg.Item;
                response.Item = auth.Authenticate(user.Username, user.Password);
                if (response.Item == null)
                {
                    response.Status = "SUCCESS";
                }
                else
                {
                    response.Status = "INVALID_USER";
                    response.Status = "The username or the password are invalid.";
                }
            }
            else
            {
                // check if the token is valid
                // TODO : chek if token is valid
                if (auth.CheckToken())
                {
                    // execute the matching operation
                    switch (msg.Operation)
                    {
                        case "decode":
                            // decode the file
                            response.Item = decodeFileService.DecodeFile((DecodeFileIn) msg.Item);
                            if (response.Item == null)
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

            return response;
        }
    }
}
