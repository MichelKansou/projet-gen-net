using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Middleware
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Dispatching" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Dispatching.svc or Dispatching.svc.cs at the Solution Explorer and start debugging.
    public class Dispatching : IDispatching
    {
        private MSG msg;
        private AuthService auth;
        public Dispatching()
        {
            Console.Write("Dispatching initialized");
            this.msg = new MSG();
            this.auth = new AuthService();
        }

        public MSG dispatcher(MSG msg)
        {
            Console.Write("Dispatcher called \n");
            Console.Write(msg.app_token);
            if ("zEAxsZ3iNwCfWWn46c".Equals(msg.app_token))
            {
                Console.Write("App token validated \n");
                if(msg.user_token != null)
                {
                    //TODO: check token validation
                }
                if ("authentication".Equals(msg.op_name))
                {
                    Console.Write("Checking User \n");
                    if (auth.AuthSerivce(msg.username, msg.user_password))
                    {
                        Console.Write("Authetication succeded");
                    } else
                    {
                        Console.Write("Authetication failed \n");
                    }
                    this.msg.data = new object[1] { (object) auth.AuthSerivce(msg.username, msg.user_password) };
                }
                this.msg.op_infos = "Done";
                this.msg.op_statut = true;

            } else
            {
                Console.Write("Someone tried to connect and is not allowed to use our server \n");
                this.msg.op_infos = "Your application is not allowed to use our server";
                this.msg.op_statut = false;
            }
            this.msg.app_name = null;
            this.msg.app_token = null;
            this.msg.app_version = null;
            this.msg.username = null;
            this.msg.user_password = null;
            this.msg.user_token = null;
            return this.msg;
        }
    }
}
