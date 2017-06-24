using DecodeFileLib;
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
        private User user;
        [DataMember]
        private DecodeFileOut decodeFileout;

        public String Status { get => status; set => status = value; }
        public String Description { get => description; set => description = value; }
        public User User { get => user; set => user = value; }
        public DecodeFileOut DecodeFileOut { get => decodeFileout; set => decodeFileout = value; }
    }
}
