using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FerroFTS.Data
{
    [DataContract()]
    public class sensor
    {
        [DataMember()]
        public string name;
        [DataMember()]
        public string value;
        [DataMember()] [BsonIgnoreIfNull()]
        public string units;
    }
}
