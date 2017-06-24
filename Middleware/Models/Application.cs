using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Middleware.Models
{
    public class Application
    {
        private String name;
        private String version;
        private String token;

        public String Name { get => name; set => name = value; }
        public String Version { get => version; set => version = value; }
        public String Token { get => token; set => token = value; }
    }
}
