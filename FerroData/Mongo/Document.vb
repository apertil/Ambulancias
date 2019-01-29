Imports MongoDB.Bson.Serialization.Attributes
Imports MongoDB.Bson
Imports MongoDB.Driver
Imports FerroData
Imports FerroData.Mongo
Imports System.Runtime.Serialization

Namespace Mongo
    ' Clase genérica de entidad de la que heredan las demás entidades
    ' Debe implementar el ObjectId
    <DataContract>
    Public MustInherit Class Document
        Implements FerroData.Mongo.IDocument

        Private _oId As MongoDB.Bson.ObjectId

        <DataMember> <BsonId>
        Public Property Id As MongoDB.Bson.ObjectId Implements IDocument.Id
            Get
                Return Me._oId
            End Get
            Set(value As MongoDB.Bson.ObjectId)
                Me._oId = value
            End Set
        End Property
    End Class
End Namespace

