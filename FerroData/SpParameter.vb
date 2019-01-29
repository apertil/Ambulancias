Public Class SpParameter

#Region "*** Variables ***"

    Private msName As String
    Private moType As Object
    Private moValue As Object
    Private moParameterDirection As System.Data.ParameterDirection
    Private msUdtTypeName As String = ""
    Private mbDisposed As Boolean = False

#End Region

#Region "*** Propiedades ***"

    Public Property Name() As String
        Get
            Return msName
        End Get
        Set(ByVal Value As String)
            msName = Value
        End Set
    End Property

    Public Property Type() As Object
        Get
            Return moType
        End Get
        Set(ByVal Value As Object)
            moType = Value
        End Set
    End Property

    Public Property Value() As Object
        Get
            Return moValue
        End Get
        Set(ByVal Value As Object)
            moValue = Value
        End Set
    End Property

    Public Property ParameterDirection() As System.Data.ParameterDirection
        Get
            Return moParameterDirection
        End Get
        Set(ByVal Value As System.Data.ParameterDirection)
            moParameterDirection = Value
        End Set
    End Property

    Public Property UdtTypeName() As String
        Get
            Return msUdtTypeName
        End Get
        Set(ByVal Value As String)
            msUdtTypeName = Value
        End Set
    End Property

    Public Sub Dispose()
        Try
            If Not mbDisposed Then
                Finalize() 'Libero memoria
                mbDisposed = True 'El objeto se ha desechado
                GC.SuppressFinalize(Me) 'Finalize no necesita ser llamado
            End If
        Catch ex As Exception
            Throw New Exception(ex.StackTrace.Substring(ex.StackTrace.LastIndexOf(vbCrLf) + 4) + vbCrLf + ex.Message)
        End Try
    End Sub

#End Region

#Region "*** Métodos públicos ***"

    Public Function Clone() As FerroData.SpParameter
        Dim oClone As New FerroData.SpParameter
        Try
            oClone.Name = msName
            oClone.Type = moType
            oClone.Value = moValue
            oClone.ParameterDirection = moParameterDirection
            oClone.UdtTypeName = msUdtTypeName
            Return oClone
        Catch ex As Exception
            Throw New Exception(ex.StackTrace.Substring(ex.StackTrace.LastIndexOf(vbCrLf) + 4) + vbCrLf + ex.Message)
        End Try
    End Function

    Protected Overrides Sub Finalize()
        Try
            If Not mbDisposed Then
                mbDisposed = True 'El objeto se ha desechado
                MyBase.Finalize()
            End If
        Catch ex As Exception
            Throw New Exception(ex.StackTrace.Substring(ex.StackTrace.LastIndexOf(vbCrLf) + 4) + vbCrLf + ex.Message)
        End Try
    End Sub

#End Region

End Class

