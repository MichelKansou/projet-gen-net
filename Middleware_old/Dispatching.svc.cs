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
            Console.WriteLine("app token : " + msg.App_token);
            Console.WriteLine("username : " + msg.Username);
            Console.WriteLine("password : " + msg.User_password);

            if ("zEAxsZ3iNwCfWWn46c".Equals(msg.App_token))
            {
                Console.Write("App token validated \n");
                if(msg.User_token != null)
                {
                    //TODO: check token validation
                }
                if ("authentication".Equals(msg.Op_name))
                {
                    Console.Write("Checking User \n");
                    if (auth.AuthSerivce(msg.Username, msg.User_password))
                    {
                        Console.Write("Authetication succeded");
                    } else
                    {
                        Console.Write("Authetication failed \n");
                    }
                    this.msg.Data = new object[1] { (object) auth.AuthSerivce(msg.Username, msg.User_password) };
                }
                this.msg.Op_infos = "Done";
                this.msg.Op_statut = true;

            } else
            {
                Console.Write("Someone tried to connect and is not allowed to use our server \n");
                this.msg.Op_infos = "Your application is not allowed to use our server";
                this.msg.Op_statut = false;
            }
            this.msg.App_name = null;
            this.msg.App_token = null;
            this.msg.App_version = null;
            this.msg.Username = null;
            this.msg.User_password = null;
            this.msg.User_token = null;
            return this.msg;
        }
    }
}
