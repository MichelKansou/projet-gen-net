using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Middleware
{
    [ServiceContract]
    interface IAuthService
    {
        [OperationContract]
        bool Authenticate(string username, string password);

        [OperationContract]
        bool CheckUser(string username, string password);
    }
}
