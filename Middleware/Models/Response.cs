using System;
using System.Runtime.Serialization;

namespace Middleware.Models
{
    [DataContract]
    public class Response
    {
        [DataMember]
        private String status;
        [DataMember]
        private String description;
        [DataMember]
        private object[] items;

        public String Status { get => status; set => status = value; }
        public String Description { get => description; set => description = value; }
        public object[] Items { get => items; set => items = value; }
    }
}
