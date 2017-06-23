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
            Trace.WriteLine("app token : " + msg.App_token);
            Trace.WriteLine("username : " + msg.Username);
            Trace.WriteLine("password : " + msg.User_password);
            Trace.WriteLine(msg.App_token);

            // check the if the app token is valid
            if (! "zEAxsZ3iNwCfWWn46c".Equals(msg.App_token))
            {
                Trace.WriteLine("Someone tried to connect and is not allowed to use our server \n");
                response.Status = "BAD_APP_TOKENS";
                response.Description = "Your application is not allowed to use our server";
                return response;
            }
            
            Trace.Write("App token validated \n");
            if ("authentication".Equals(msg.Op_infos))
            {
                // TODO: check if i's ok
                // if not, return an erre
                response.Items = new object[1] { auth.Authenticate(msg.Username, msg.User_password) };
                response.Status = "SUCCESS";
            }
            else
            {
                // check if the token is valid
                // TODO : chek if token is valid
                if (true)
                {
                    // execute the matching operation
                    switch (msg.Op_infos)
                    {
                        case "decode":
                            // decode the file
                            DecodeFileOut result = decodeFileService.DecodeFile((DecodeFileIn) msg.Data[0]);
                            if (result == null)
                            {
                                response.Description = "Impossible to decode the file.";
                                response.Status = "DECODE_IMPOSSIBLE";
                            }
                            else
                            {
                                response.Items = new object[1] { result };
                                response.Status = "SUCCESS";
                            }
                            break;
                        default:
                            response.Description = "Operation " + msg.Op_name + " doesn't exist";
                            response.Status = "NO_MATCHING_OPERATION";
                            Trace.WriteLine(response.Description);
                            break;
                    }
                }
                else
                {
                    response.Description = "The user token " + msg.User_token + " is not valid.";
                    response.Status = "INVALID_USER_TOKEN";
                }
            }

            return response;
        }
    }
}
