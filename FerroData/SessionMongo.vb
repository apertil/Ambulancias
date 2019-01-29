Imports MongoDB.Bson
Imports MongoDB.Driver

Public Class SessionMongo
    Implements IDisposable


#Region "*** Variables Locales ***"
    Private msDatabase As String
#End Region

#Region "*** Propiedades ***"
    Public Property Database() As String
        Get
            Try
                If Me.msDatabase = String.Empty Then
                    Me.msDatabase = Me.LoadDatabaseFromURI()
                End If
                Return Me.msDatabase
            Catch ex As Exception
                Throw New Exception(ex.StackTrace.Substring(ex.StackTrace.LastIndexOf(vbCrLf) + 4) + vbCrLf + ex.Message)
            End Try
        End Get
        Set(value As String)
            Me.msDatabase = value
        End Set
    End Property

    ''' <summary>
    ''' mongodb://[username:password@]host1[:port1][,host2[:port2],...[,hostN[:portN]]][/[database][?options]]
    ''' </summary>
    ''' <returns></returns>
    Public Property ConnectionString As String = String.Empty

#End Region

#Region "*** Métodos públicos ***"

    Public Sub New()

    End Sub

    Public Sub New(sConnectionString As String)
        Try
            Me.ConnectionString = sConnectionString
        Catch ex As Exception
            Throw New Exception(ex.StackTrace.Substring(ex.StackTrace.LastIndexOf(vbCrLf) + 4) + vbCrLf + ex.Message)
        End Try
    End Sub

    Public Sub New(sConnectionString As String, sDatabase As String)
        Try
            Me.ConnectionString = sConnectionString
            Me.Database = sDatabase
        Catch ex As Exception
            Throw New Exception(ex.StackTrace.Substring(ex.StackTrace.LastIndexOf(vbCrLf) + 4) + vbCrLf + ex.Message)
        End Try
    End Sub
#End Region

#Region "*** Métodos Privados ***"

    Private Function LoadDatabaseFromURI() As String
        Dim sText1() As String
        Dim sDatabase1 As String
        Dim sText2() As String
        Dim sDatabase2 As String = String.Empty
        Try
            'p.ej:
            'mongodb://admintest:<PASSWORD>@clusteronvia60test01-shard-00-00-bnkhu.mongodb.net:27017,clusteronvia60test01-shard-00-01-bnkhu.mongodb.net:27017,clusteronvia60test01-shard-00-02-bnkhu.mongodb.net:27017/test?ssl=true&replicaSet=ClusterOnvia60Test01-shard-0&authSource=admin

            sText1 = Me.ConnectionString.Split("/".ToCharArray)
            sDatabase1 = sText1(sText1.Length - 1) 'test?ssl=true&replicaSet=ClusterOnvia60Test01-shard-0&authSource=admin
            sText2 = sDatabase1.Split("?".ToCharArray)
            sDatabase2 = sText2(0) 'test

            Return sDatabase2
        Catch ex As Exception
            Throw New Exception(ex.StackTrace.Substring(ex.StackTrace.LastIndexOf(vbCrLf) + 4) + vbCrLf + ex.Message)
        End Try
    End Function

#End Region

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

End Class

