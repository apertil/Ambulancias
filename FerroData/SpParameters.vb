Public Class SpParameters
    Inherits System.Collections.CollectionBase

#Region "*** Variables Locales ***"
    Private mcKeys As New System.Collections.ArrayList
    Private mbDisposed As Boolean = False
#End Region

#Region "*** Métodos Públicos ***"

    Public Function Add(ByVal sName As String, _
                        ByVal oType As Object, _
                        ByVal oParameterDirection As System.Data.ParameterDirection, _
                        ByVal oValue As Object) _
                        As FerroData.SpParameter
        Dim oTemp As FerroData.SpParameter
        Try
            oTemp = Add(sName, oType, oParameterDirection, oValue, "")
            Return oTemp
        Catch ex As Exception
            Throw New Exception(ex.StackTrace.Substring(ex.StackTrace.LastIndexOf(vbCrLf) + 4) + vbCrLf + ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function Add(ByVal sName As String, _
                        ByVal oType As Object, _
                        ByVal oParameterDirection As System.Data.ParameterDirection, _
                        ByVal oValue As Object, _
                        ByVal sUdtTypeName As String) _
                        As FerroData.SpParameter
        Dim oTemp As New FerroData.SpParameter
        Try
            oTemp.Name = sName
            oTemp.Type = oType
            oTemp.ParameterDirection = oParameterDirection
            oTemp.UdtTypeName = sUdtTypeName
            oTemp.Value = oValue

            List.Add(oTemp)
            mcKeys.Clear()
            Return oTemp
        Catch ex As Exception
            Throw New Exception(ex.StackTrace.Substring(ex.StackTrace.LastIndexOf(vbCrLf) + 4) + vbCrLf + ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function Add(ByVal oSpParameter As FerroData.SpParameter, _
                        ByVal oKey As Object) _
                        As FerroData.SpParameter
        Dim oTemp As FerroData.SpParameter
        Try
            oTemp = oSpParameter.Clone
            List.Add(oTemp)
            mcKeys.Add("Key" + CType(oKey, String))
            Return oTemp
        Catch ex As Exception
            Throw New Exception(ex.StackTrace.Substring(ex.StackTrace.LastIndexOf(vbCrLf) + 4) + vbCrLf + ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function Add(ByVal oSpParameter As FerroData.SpParameter) As FerroData.SpParameter
        Dim oTemp As FerroData.SpParameter
        Try
            oTemp = oSpParameter
            List.Add(oTemp)
            mcKeys.Clear()
            Return oTemp
        Catch ex As Exception
            Throw New Exception(ex.StackTrace.Substring(ex.StackTrace.LastIndexOf(vbCrLf) + 4) + vbCrLf + ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function RemoveBy(ByVal iIndex As Integer) As Boolean
        Try
            If (iIndex >= 0) And (iIndex < List.Count) Then
                List.RemoveAt(iIndex)
                If mcKeys.Count > 0 Then
                    mcKeys.RemoveAt(iIndex)
                End If
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw New Exception(ex.StackTrace.Substring(ex.StackTrace.LastIndexOf(vbCrLf) + 4) + vbCrLf + ex.Message)
            Return False
        End Try
    End Function

    Public Function Remove(ByVal oKey As Object) As Boolean
        Dim iIndex As Integer
        Try
            If (mcKeys.Count > 0) Then
                iIndex = CType(mcKeys.IndexOf("Key" & CType(oKey, String)), Integer)
                If (iIndex >= 0) And (iIndex < List.Count) Then
                    List.RemoveAt(iIndex)
                    mcKeys.RemoveAt(iIndex)
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            Throw New Exception(ex.StackTrace.Substring(ex.StackTrace.LastIndexOf(vbCrLf) + 4) + vbCrLf + ex.Message)
            Return False
        End Try
    End Function

    Public Function Exist(ByVal oKey As Object) As Boolean
        Try
            Return mcKeys.Contains("Key" & CType(oKey, String))
        Catch ex As Exception
            Throw New Exception(ex.StackTrace.Substring(ex.StackTrace.LastIndexOf(vbCrLf) + 4) + vbCrLf + ex.Message)
            Return False
        End Try
    End Function

    Public Function Item(ByVal oKey As Object) As FerroData.SpParameter
        Dim iIndex As Integer
        Try
            If mcKeys.Count > 0 Then
                iIndex = CType(mcKeys.IndexOf("Key" + CType(oKey, String)), Integer)
                Return CType(List.Item(iIndex), FerroData.SpParameter)
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Throw New Exception(ex.StackTrace.Substring(ex.StackTrace.LastIndexOf(vbCrLf) + 4) + vbCrLf + ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function ItemBy(ByVal Index As Integer) As FerroData.SpParameter
        Try
            Return CType(List.Item(Index), FerroData.SpParameter)
        Catch ex As Exception
            Throw New Exception(ex.StackTrace.Substring(ex.StackTrace.LastIndexOf(vbCrLf) + 4) + vbCrLf + ex.Message)
            Return Nothing
        End Try
    End Function

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

#Region "*** Métodos Privados ***"

    Protected Overrides Sub OnClear()
        Try
            mcKeys.Clear()
        Catch ex As Exception
            Throw New Exception(ex.StackTrace.Substring(ex.StackTrace.LastIndexOf(vbCrLf) + 4) + vbCrLf + ex.Message)
        End Try
    End Sub

    Protected Overrides Sub Finalize()
        Try
            If Not mbDisposed Then
                List.Clear()
                MyBase.Finalize()
                mbDisposed = True
            End If
        Catch ex As Exception
            Throw New Exception(ex.StackTrace.Substring(ex.StackTrace.LastIndexOf(vbCrLf) + 4) + vbCrLf + ex.Message)
        End Try
    End Sub

#End Region

End Class
