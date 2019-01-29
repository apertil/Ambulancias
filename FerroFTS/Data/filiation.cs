using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FerroFTS.Data
{
    [DataContract()]
    public class filiation
    {        [DataMember()]
        [BsonIgnoreIfDefault()]
        public string dni;
        [DataMember()] [BsonIgnoreIfDefault()]
        public string gender;// As String
        [DataMember()] [BsonIgnoreIfDefault()]
        public string name;// As String
        [DataMember()] [BsonIgnoreIfDefault()]
        public string surnames;// As String
        [DataMember()] [BsonIgnoreIfDefault()]
        public string address;// As String
    [DataMember()][BsonIgnoreIfDefault()]
        public DateTime birthdate;// As System.DateTime
    }
}
