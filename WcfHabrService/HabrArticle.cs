using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using MongoDB.Bson;

namespace WcfHabrService
{
    [DataContract]
    public class HabrArticle
    {
        [DataMember]
        public ObjectId Id { get; set; }

        [DataMember]
        public int HabrId { get; set; }

        [DataMember]
        public string Theme { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public List<string> Tags { get; set; }

        [DataMember]
        public string PublicationDate { get; set; }
    }
}