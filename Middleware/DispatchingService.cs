using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Middleware
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class DispatchingService : IDispatchingService
    {
        private Message msg;
        private AuthService auth;

        public DispatchingService()
        {
            this.msg = new Message();
            this.auth = new AuthService();
            Trace.WriteLine("Dispatching initialized");
        }
        public Message Dispatcher(Message msg)
        {
            Trace.WriteLine("Dispatcher called \n");
            Trace.WriteLine("app token : " + msg.App_token);
            Trace.WriteLine("username : " + msg.Username);
            Trace.WriteLine("password : " + msg.User_password);
            Trace.WriteLine(msg.App_token);
            if ("zEAxsZ3iNwCfWWn46c".Equals(msg.App_token))
            {
                Trace.Write("App token validated \n");
                if (msg.User_token != null)
                {
                    //TODO: check token validation
                }
                if ("authentication".Equals(msg.Op_name))
                {
                    Trace.Write("Checking User \n");
                    if (auth.Authenticate(msg.Username, msg.User_password))
                    {
                        Trace.WriteLine("Authetication succeded");
                    }
                    else
                    {
                        Trace.WriteLine("Authetication failed \n");
                    }
                    this.msg.Data = new object[1] { (object)auth.Authenticate(msg.Username, msg.User_password) };
                }
                this.msg.Op_infos = "Done";
                this.msg.Op_statut = true;

            }
            else
            {
                Trace.WriteLine("Someone tried to connect and is not allowed to use our server \n");
                this.msg.Op_infos = "Your application is not allowed to use our server";
                this.msg.Op_statut = false;
            }
            EmptyMessage();
            return this.msg;
        }

        private void EmptyMessage()
        {
            this.msg.App_name = null;
            this.msg.App_token = null;
            this.msg.App_version = null;
            this.msg.Username = null;
            this.msg.User_password = null;
            this.msg.User_token = null;
        }
    }
}
