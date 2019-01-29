using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FerroFTS.Data
{   [DataContract()]
    public class service : FerroData.Mongo.Document
    {
    [DataMember()]
    public string id_service;
    [DataMember()]
    public string ou;
    [DataMember()]
    public string ou_service;
    [DataMember()]
    [BsonIgnoreIfNull()]
    public int priority;

    [DataMember()] [BsonIgnoreIfNull()]
    public string indicative;
    [DataMember()] [BsonIgnoreIfNull()]
     public string vehicle;
    [DataMember()] [BsonIgnoreIfNull()]
     public string service_type;
    [DataMember()] [BsonIgnoreIfNull()]
     public string vehicle_type;
    [DataMember()] [BsonIgnoreIfNull()]
     public string termination_reason;
    [DataMember()] [BsonIgnoreIfNull()]
     public string hospital;
    [DataMember()] [BsonIgnoreIfNull()]
     public string intervention_address;
    [DataMember()] [BsonIgnoreIfNull()]
    public int intervention_quality;
    [DataMember()] [BsonIgnoreIfNull()]
     public string remarks;

     [DataMember()]
     public DateTime start_date; //Tiene que haber siempre start_date
     [DataMember()] [BsonIgnoreIfDefault()]
     public DateTime transmission_date; // As System.DateTime
     [DataMember()] [BsonIgnoreIfDefault()]
     public DateTime movilization_date;// As System.DateTime
     [DataMember()] [BsonIgnoreIfDefault()]
     public DateTime intervention_date;
     [DataMember()] [BsonIgnoreIfDefault()]
     public DateTime departure_date;
     [DataMember()] [BsonIgnoreIfDefault()]
     public DateTime transfer_date;
     [DataMember()] [BsonIgnoreIfDefault()]
     public DateTime end_date;

     [DataMember()] [BsonIgnoreIfNull()]
     public FerroFTS.Data.filiation filiation;

     [DataMember()] [BsonIgnoreIfNull()]
     public List<FerroFTS.Data.custom_prop> client_properties = new List<FerroFTS.Data.custom_prop>();

     [DataMember()] [BsonIgnoreIfDefault()]
     public FerroGeoJSON.Point p_intervention;
     [DataMember()]
     [BsonIgnoreIfDefault()]
     public FerroGeoJSON.Point p_hospital;

    }
}
