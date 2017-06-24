using Middleware.Models;
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
        User Authenticate(string username, string password);

        [OperationContract]
        bool CheckUser(string username, string password);

        [OperationContract]
        bool CheckToken();

        [OperationContract]
        User getUser();
    }
}
