Imports MongoDB.Bson
Imports MongoDB.Driver
Imports FerroData
Imports FerroData.Mongo

Namespace Mongo
    Public MustInherit Class DocumentCollection(Of T As FerroData.Mongo.IDocument)
        Implements IDisposable

#Region "*** Variables Locales ***"
        Private moMongoCollection As MongoDB.Driver.IMongoCollection(Of T)
        Private moSession As FerroData.SessionMongo
#End Region
        Public WriteOnly Property Session As FerroData.SessionMongo
            Set(value As FerroData.SessionMongo)
                Try
                    Me.moSession = value
                    Me.SetMongoCollection()
                Catch ex As Exception
                    Throw New Exception(ex.StackTrace.Substring(ex.StackTrace.LastIndexOf(vbCrLf) + 4) + vbCrLf + ex.Message)
                End Try
            End Set
        End Property

        Public ReadOnly Property MongoCollection() As IMongoCollection(Of T)
            Get
                Return Me.moMongoCollection
            End Get
        End Property


        Private Sub SetMongoCollection()
            Dim oClient As MongoDB.Driver.MongoClient
            Dim oDb As MongoDB.Driver.MongoDatabaseBase
            Try
                oClient = New MongoDB.Driver.MongoClient(Me.moSession.ConnectionString)
                oDb = CType(oClient.GetDatabase(Me.moSession.Database), MongoDB.Driver.MongoDatabaseBase)

                Me.moMongoCollection = oDb.GetCollection(Of T)(GetType(T).Name)
            Catch ex As Exception
                Throw New Exception(ex.StackTrace.Substring(ex.StackTrace.LastIndexOf(vbCrLf) + 4) + vbCrLf + ex.Message)
            End Try
        End Sub


#Region "IDisposable Support"
        Private disposedValue As Boolean ' Para detectar llamadas redundantes

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not disposedValue Then
                If disposing Then
                    ' TODO: elimine el estado administrado (objetos administrados).
                    If Not Me.moSession Is Nothing Then
                        Me.moSession.Dispose()
                    End If
                End If

                ' TODO: libere los recursos no administrados (objetos no administrados) y reemplace Finalize() a continuación.
                ' TODO: configure los campos grandes en nulos.
            End If
            disposedValue = True
        End Sub

        ' TODO: reemplace Finalize() solo si el anterior Dispose(disposing As Boolean) tiene código para liberar recursos no administrados.
        Protected Overrides Sub Finalize()
            ' No cambie este código. Coloque el código de limpieza en el anterior Dispose(disposing As Boolean).
            Dispose(False)
            MyBase.Finalize()
        End Sub

        ' Visual Basic agrega este código para implementar correctamente el patrón descartable.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' No cambie este código. Coloque el código de limpieza en el anterior Dispose(disposing As Boolean).
            Dispose(True)
            ' TODO: quite la marca de comentario de la siguiente línea si Finalize() se ha reemplazado antes.
            ' GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class
End Namespace

