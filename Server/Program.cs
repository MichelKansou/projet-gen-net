using Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        private static string uri;
        private static ServiceHost host;
        private static bool open;

        static void Main(string[] args)
        {
            open = false;
            affichage();
            if (ini_serv()) Console.WriteLine("Serveur en écoute"); ;
            Console.Read();
        }

        static bool ini_serv()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Tentative d'initialisation du serveur WCF");
            try
            {
                ServiceHost host = new ServiceHost(typeof(Dispatching));

                host.Open();

                Console.WriteLine("Paramétrage ok");
            } catch (Exception x)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Echec");
                Console.WriteLine(x.ToString());
                open = false;
            }
            return open;
        }
        static void affichage()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("*************** eXia WCF Server ***************\n");
        }
    }
}
