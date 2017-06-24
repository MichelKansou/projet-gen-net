using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using Middleware.Models;

namespace Middleware
{
    [ServiceContract]
    public interface IDispatchingService
    {
        [OperationContract]
        Response Dispatcher(Message msg);
    }
}
