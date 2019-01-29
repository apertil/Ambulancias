using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FerroFTS.Data
{
    [DataContract()]
    public class location : FerroData.Mongo.Document
    {
         [DataMember()]
        public string ou;// As String
        [DataMember()]
        public string ou_service;// As String
        [DataMember()]
        public DateTime utc_datetime; // As System.DateTime
        [DataMember()] [BsonIgnoreIfNull()]
        public string vehicle;// As String
        [DataMember()] [BsonIgnoreIfNull()]
        public string indicative;
        [DataMember()] [BsonIgnoreIfDefault()]
        public int heading; // As Integer
        [DataMember()] [BsonIgnoreIfDefault()]
        public double speed;// As Double
        [DataMember()] [BsonIgnoreIfDefault()]
        public double pdop;  //As Double

    [DataMember()] [BsonIgnoreIfNull()]
        public List<FerroFTS.Data.sensor> sensors = new List<FerroFTS.Data.sensor>();

        [DataMember()] [BsonIgnoreIfNull()]
        public List<custom_prop> events = new List<custom_prop>();

    [DataMember()] [BsonIgnoreIfNull()]
     public List<custom_prop> provider_properties = new List<custom_prop>();


        [DataMember()]
        [BsonIgnoreIfDefault()]
        public FerroGeoJSON.Point p_location;
        





    }
}
