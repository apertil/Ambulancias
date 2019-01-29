Imports MongoDB.Bson
Imports MongoDB.Driver
Imports FerroData
Imports FerroData.Mongo

Namespace Mongo
    ' Interfaz genérico de entidad de la que hereda MongoEntity
    ' Debe implementar el ObjectId
    Public Interface IDocument
        Property Id As MongoDB.Bson.ObjectId
    End Interface
End Namespace

