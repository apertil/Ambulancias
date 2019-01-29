Imports MongoDB.Bson
Imports MongoDB.Driver
Imports FerroData
Imports FerroData.Mongo

Namespace Mongo
    Public Class DataMongo(Of T As FerroData.Mongo.IDocument)
        Implements IDisposable

#Region "*** Variables Locales ***"
        Private moSession As FerroData.SessionMongo
#End Region

#Region "*** Propiedades ***"
        Public WriteOnly Property Session() As FerroData.SessionMongo
            Set(ByVal Value As FerroData.SessionMongo)
                moSession = Value
            End Set
        End Property

#End Region

#Region "*** Métodos públicos ***"
        Public Sub InsertManyAsync(oListDocuments As System.Collections.Generic.List(Of FerroData.Mongo.Document))
            Dim oClient As MongoDB.Driver.MongoClient
            Dim oDb As MongoDB.Driver.MongoDatabaseBase
            Dim oMongoCol As MongoDB.Driver.IMongoCollection(Of FerroData.Mongo.Document)
            Dim oTask As System.Threading.Tasks.Task

            Try
                oClient = New MongoDB.Driver.MongoClient(Me.moSession.ConnectionString)
                oDb = CType(oClient.GetDatabase(Me.moSession.Database), MongoDB.Driver.MongoDatabaseBase)
                oMongoCol = oDb.GetCollection(Of FerroData.Mongo.Document)(GetType(T).Name)

                oTask = oMongoCol.InsertManyAsync(oListDocuments)
                oTask.Wait() 'Para operaciones sin respuesta

                If oTask.Exception IsNot Nothing Then
                    Throw New Exception(oTask.Exception.StackTrace.Substring(oTask.Exception.StackTrace.LastIndexOf(vbCrLf) + 4) + vbCrLf + oTask.Exception.Message)
                End If

                oTask.Dispose()

            Catch ex As Exception
                Throw New Exception(ex.StackTrace.Substring(ex.StackTrace.LastIndexOf(vbCrLf) + 4) + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub InsertMany(oListDocuments As System.Collections.Generic.List(Of FerroData.Mongo.Document))
            Dim oClient As MongoDB.Driver.MongoClient
            Dim oDb As MongoDB.Driver.MongoDatabaseBase
            Dim oMongoCol As MongoDB.Driver.IMongoCollection(Of FerroData.Mongo.Document)

            Try
                oClient = New MongoDB.Driver.MongoClient(Me.moSession.ConnectionString)
                oDb = CType(oClient.GetDatabase(Me.moSession.Database), MongoDB.Driver.MongoDatabaseBase)
                oMongoCol = oDb.GetCollection(Of FerroData.Mongo.Document)(GetType(T).Name)

                oMongoCol.InsertMany(oListDocuments)

            Catch ex As Exception
                Throw New Exception(ex.StackTrace.Substring(ex.StackTrace.LastIndexOf(vbCrLf) + 4) + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub InsertOneAsync(oDocument As FerroData.Mongo.Document)
            Dim oClient As MongoDB.Driver.MongoClient
            Dim oDb As MongoDB.Driver.MongoDatabaseBase
            Dim oMongoCol As MongoDB.Driver.IMongoCollection(Of FerroData.Mongo.Document)
            Dim oTask As System.Threading.Tasks.Task

            Try
                oClient = New MongoDB.Driver.MongoClient(Me.moSession.ConnectionString)
                oDb = CType(oClient.GetDatabase(Me.moSession.Database), MongoDB.Driver.MongoDatabaseBase)
                oMongoCol = oDb.GetCollection(Of FerroData.Mongo.Document)(GetType(T).Name)

                oTask = oMongoCol.InsertOneAsync(oDocument)
                oTask.Wait()

                If oTask.Exception IsNot Nothing Then
                    Throw New Exception(oTask.Exception.StackTrace.Substring(oTask.Exception.StackTrace.LastIndexOf(vbCrLf) + 4) + vbCrLf + oTask.Exception.Message)
                End If

                oTask.Dispose()

            Catch ex As Exception
                Throw New Exception(ex.StackTrace.Substring(ex.StackTrace.LastIndexOf(vbCrLf) + 4) + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub ReplaceOneAsync(oDocument As T,
                                   oFilter As MongoDB.Driver.FilterDefinition(Of T),
                                   oOptions As MongoDB.Driver.UpdateOptions)
            Dim oClient As MongoDB.Driver.MongoClient
            Dim oDb As MongoDB.Driver.MongoDatabaseBase
            Dim oMongoCol As MongoDB.Driver.IMongoCollection(Of T)
            Dim oTask As System.Threading.Tasks.Task

            Try
                oClient = New MongoDB.Driver.MongoClient(Me.moSession.ConnectionString)
                oDb = CType(oClient.GetDatabase(Me.moSession.Database), MongoDB.Driver.MongoDatabaseBase)
                oMongoCol = oDb.GetCollection(Of T)(GetType(T).Name)

                oTask = oMongoCol.ReplaceOneAsync(oFilter, oDocument, oOptions)
                oTask.Wait()

                If oTask.Exception IsNot Nothing Then
                    Throw New Exception(oTask.Exception.StackTrace.Substring(oTask.Exception.StackTrace.LastIndexOf(vbCrLf) + 4) + vbCrLf + oTask.Exception.Message)
                End If

                oTask.Dispose()

            Catch ex As Exception
                Throw New Exception(ex.StackTrace.Substring(ex.StackTrace.LastIndexOf(vbCrLf) + 4) + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub InsertOne(oDocument As FerroData.Mongo.Document)
            Dim oClient As MongoDB.Driver.MongoClient
            Dim oDb As MongoDB.Driver.MongoDatabaseBase
            Dim oMongoCol As MongoDB.Driver.IMongoCollection(Of FerroData.Mongo.Document)

            Try
                oClient = New MongoDB.Driver.MongoClient(Me.moSession.ConnectionString)
                oDb = CType(oClient.GetDatabase(Me.moSession.Database), MongoDB.Driver.MongoDatabaseBase)
                oMongoCol = oDb.GetCollection(Of FerroData.Mongo.Document)(GetType(T).Name)

                oMongoCol.InsertOne(oDocument)

            Catch ex As Exception
                Throw New Exception(ex.StackTrace.Substring(ex.StackTrace.LastIndexOf(vbCrLf) + 4) + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Function Distinct(sField As String) As List(Of System.Object)
            Dim oClient As MongoDB.Driver.MongoClient
            Dim oDb As MongoDB.Driver.MongoDatabaseBase
            Dim oMongoCol As MongoDB.Driver.IMongoCollection(Of T)

            Dim oBuilder = MongoDB.Driver.Builders(Of T).Filter
            Dim oList As List(Of System.Object)

            Try
                oClient = New MongoDB.Driver.MongoClient(Me.moSession.ConnectionString)
                oDb = CType(oClient.GetDatabase(Me.moSession.Database), MongoDB.Driver.MongoDatabaseBase)
                oMongoCol = oDb.GetCollection(Of T)(GetType(T).Name)

                oList = oMongoCol.Distinct(Of System.Object)(sField, oBuilder.Empty).ToList

                Return oList

            Catch ex As Exception
                Throw New Exception(ex.StackTrace.Substring(ex.StackTrace.LastIndexOf(vbCrLf) + 4) + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function Find(oFilter As MongoDB.Driver.FilterDefinition(Of T),
                             oOptions As MongoDB.Driver.FindOptions(Of T)) _
                             As System.Collections.Generic.List(Of T)
            Dim oClient As MongoDB.Driver.MongoClient
            Dim oDb As MongoDB.Driver.MongoDatabaseBase
            Dim oMongoCol As MongoDB.Driver.IMongoCollection(Of T)
            Dim oList As New System.Collections.Generic.List(Of T)

            Try
                oClient = New MongoDB.Driver.MongoClient(Me.moSession.ConnectionString)
                oDb = CType(oClient.GetDatabase(Me.moSession.Database), MongoDB.Driver.MongoDatabaseBase)
                oMongoCol = oDb.GetCollection(Of T)(GetType(T).Name)

                oList = oMongoCol.FindSync(oFilter, oOptions).ToList

                Return oList
            Catch ex As Exception
                Throw New Exception(ex.StackTrace.Substring(ex.StackTrace.LastIndexOf(vbCrLf) + 4) + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function FindAsync(oFilter As MongoDB.Driver.FilterDefinition(Of T),
                                  oSort As MongoDB.Driver.SortDefinition(Of T)) _
                                  As System.Collections.Generic.List(Of T)
            Dim oClient As MongoDB.Driver.MongoClient
            Dim oDb As MongoDB.Driver.MongoDatabaseBase
            Dim oMongoCol As MongoDB.Driver.IMongoCollection(Of T)
            Dim oOptions As New MongoDB.Driver.FindOptions(Of T)
            Dim oTask As System.Threading.Tasks.Task(Of MongoDB.Driver.IAsyncCursor(Of T))
            Dim oList As New System.Collections.Generic.List(Of T)

            Try
                oClient = New MongoDB.Driver.MongoClient(Me.moSession.ConnectionString)
                oDb = CType(oClient.GetDatabase(Me.moSession.Database), MongoDB.Driver.MongoDatabaseBase)
                oMongoCol = oDb.GetCollection(Of T)(GetType(T).Name)

                oOptions.Sort = oSort
                oTask = oMongoCol.FindAsync(oFilter, oOptions)

                If oTask.Exception IsNot Nothing Then
                    Throw New Exception(oTask.Exception.StackTrace.Substring(oTask.Exception.StackTrace.LastIndexOf(vbCrLf) + 4) + vbCrLf + oTask.Exception.Message)
                Else
                    oList = oTask.Result.ToList
                    oTask.Result.Dispose()
                End If

                oTask.Dispose()

                Return oList
            Catch ex As Exception
                Throw New Exception(ex.StackTrace.Substring(ex.StackTrace.LastIndexOf(vbCrLf) + 4) + vbCrLf + ex.Message)
            End Try
        End Function


        Public Sub FindOneAndUpdate(oFilter As MongoDB.Driver.FilterDefinition(Of T),
                                    oUpdate As MongoDB.Driver.UpdateDefinition(Of T),
                                    oOptions As MongoDB.Driver.FindOneAndUpdateOptions(Of T))
            Dim oClient As MongoDB.Driver.MongoClient
            Dim oDb As MongoDB.Driver.MongoDatabaseBase
            Dim oMongoCol As MongoDB.Driver.IMongoCollection(Of T)
            Dim oList As New System.Collections.Generic.List(Of T)
            Dim oResult As T

            Try
                oClient = New MongoDB.Driver.MongoClient(Me.moSession.ConnectionString)
                oDb = CType(oClient.GetDatabase(Me.moSession.Database), MongoDB.Driver.MongoDatabaseBase)
                oMongoCol = oDb.GetCollection(Of T)(GetType(T).Name)

                oResult = oMongoCol.FindOneAndUpdate(oFilter, oUpdate, oOptions)

            Catch ex As Exception
                Throw New Exception(ex.StackTrace.Substring(ex.StackTrace.LastIndexOf(vbCrLf) + 4) + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub FindOneAndReplace(oFilter As MongoDB.Driver.FilterDefinition(Of T),
                                     oReplacement As T,
                                     oOptions As MongoDB.Driver.FindOneAndReplaceOptions(Of T))
            Dim oClient As MongoDB.Driver.MongoClient
            Dim oDb As MongoDB.Driver.MongoDatabaseBase
            Dim oMongoCol As MongoDB.Driver.IMongoCollection(Of T)
            Dim oList As New System.Collections.Generic.List(Of T)
            Dim oResult As T

            Try
                oClient = New MongoDB.Driver.MongoClient(Me.moSession.ConnectionString)
                oDb = CType(oClient.GetDatabase(Me.moSession.Database), MongoDB.Driver.MongoDatabaseBase)
                oMongoCol = oDb.GetCollection(Of T)(GetType(T).Name)

                'El documento devuelto es el original que se ha reemplazado
                'Si el filtro devuelve más de un documento se reemplaza el primero (influye sort de FindOneAndReplaceOptions)
                oResult = oMongoCol.FindOneAndReplace(oFilter, oReplacement, oOptions)

            Catch ex As Exception
                Throw New Exception(ex.StackTrace.Substring(ex.StackTrace.LastIndexOf(vbCrLf) + 4) + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub Delete(oFilter As MongoDB.Driver.FilterDefinition(Of T))
            Dim oClient As MongoDB.Driver.MongoClient
            Dim oDb As MongoDB.Driver.MongoDatabaseBase
            Dim oMongoCol As MongoDB.Driver.IMongoCollection(Of T)

            Try
                oClient = New MongoDB.Driver.MongoClient(Me.moSession.ConnectionString)
                oDb = CType(oClient.GetDatabase(Me.moSession.Database), MongoDB.Driver.MongoDatabaseBase)
                oMongoCol = oDb.GetCollection(Of T)(GetType(T).Name)

                oMongoCol.DeleteMany(oFilter)

            Catch ex As Exception
                Throw New Exception(ex.StackTrace.Substring(ex.StackTrace.LastIndexOf(vbCrLf) + 4) + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub Execute(ByVal sSql As String)
            Try

            Catch ex As Exception
                Throw New Exception(ex.StackTrace.Substring(ex.StackTrace.LastIndexOf(vbCrLf) + 4) + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Function Exist(ByVal sSql As String) As Boolean
            Dim bRet As Boolean = False
            Try


                Return bRet
            Catch ex As Exception
                Throw New Exception(ex.StackTrace.Substring(ex.StackTrace.LastIndexOf(vbCrLf) + 4) + vbCrLf + ex.Message)
            End Try
        End Function

#Region "IDisposable Support"
        Private disposedValue As Boolean ' Para detectar llamadas redundantes

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not disposedValue Then
                If disposing Then
                    ' TODO: elimine el estado administrado (objetos administrados).
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

#End Region

#Region "*** Métodos Privados ***"


#End Region

    End Class
End Namespace

