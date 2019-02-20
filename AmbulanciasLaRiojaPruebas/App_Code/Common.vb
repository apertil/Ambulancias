Public Class Common
    Friend Shared Function GetPropValue(oProps As List(Of FerroFTS.custom_prop), sName As String) As String
        Dim oConsulta As IEnumerable(Of FerroFTS.custom_prop)
        Dim sValue As String = String.Empty
        Try
            oConsulta = From oProp In oProps
                        Where oProp.name.ToUpper.Trim = sName.ToUpper.Trim
                        Select oProp
            If oConsulta.Count > 0 Then
                sValue = oConsulta(0).value
            End If

            Return sValue

        Catch ex As Exception
            Throw New Exception(ex.StackTrace.Substring(ex.StackTrace.LastIndexOf(vbCrLf) + 4) + vbCrLf + ex.Message)
        End Try
    End Function

    Friend Shared Sub UpdatePropValue(oProps As List(Of FerroFTS.custom_prop), sName As String, sValue As String)
        Dim oProp As FerroFTS.custom_prop
        Try
            For Each oProp In oProps
                If oProp.name = sName Then
                    oProp.value = sValue
                    Exit For
                End If
            Next
        Catch ex As Exception
            Throw New Exception(ex.StackTrace.Substring(ex.StackTrace.LastIndexOf(vbCrLf) + 4) + vbCrLf + ex.Message)
        End Try
    End Sub

    Public Shared Function UpdateServiceReadProp(oService As FerroFTS.service, bValue As Boolean) As FerroFTS.service
        Dim sReadValue As String
        Dim oCusProp As FerroFTS.custom_prop
        Dim sValue As String

        Try
            If bValue Then
                sValue = "true"
            Else
                sValue = "false"
            End If

            If oService.client_properties Is Nothing Then oService.client_properties = New List(Of FerroFTS.custom_prop)
            sReadValue = FerroFTS.Common.GetPropValue(oService.client_properties, "read")

            If sReadValue = String.Empty Then
                oCusProp = New FerroFTS.custom_prop With {.name = "read", .value = sValue}
                oService.client_properties.Add(oCusProp)
            Else
                FerroFTS.Common.UpdatePropValue(oService.client_properties, "read", sValue)
            End If

            Return oService

        Catch ex As Exception
            Throw New Exception(ex.StackTrace.Substring(ex.StackTrace.LastIndexOf(vbCrLf) + 4) + vbCrLf + ex.Message)
        End Try
    End Function

    Public Shared Function TrimStringProperties(oService As FerroFTS.service) As FerroFTS.service

        Try
            For Each oProp As custom_prop In oService.client_properties
                oProp.name = oProp.name.Trim
                oProp.value = oProp.value.Trim
            Next

            If oService.filiation IsNot Nothing Then
                oService.filiation.address = oService.filiation.address.Trim
                oService.filiation.dni = oService.filiation.dni.Trim
                oService.filiation.gender = oService.filiation.gender.Trim
                oService.filiation.name = oService.filiation.name.Trim
                oService.filiation.surnames = oService.filiation.surnames.Trim
            End If

            oService.id_service = oService.id_service.Trim
            oService.indicative = oService.indicative.Trim
            oService.hospital = oService.hospital.Trim
            oService.intervention_address = oService.intervention_address.Trim
            oService.ou = oService.ou.Trim
            oService.ou_service = oService.ou_service.Trim
            oService.remarks = oService.remarks.Trim
            oService.service_type = oService.service_type.Trim
            oService.termination_reason = oService.termination_reason.Trim
            oService.vehicle = oService.vehicle.Trim
            oService.vehicle_type = oService.vehicle_type.Trim

            Return oService

        Catch ex As Exception
            Throw New Exception(ex.StackTrace.Substring(ex.StackTrace.LastIndexOf(vbCrLf) + 4) + vbCrLf + ex.Message)
        End Try
    End Function


    Friend Shared Function ParseUTC(dDateTime As System.DateTime) As System.DateTime
        Dim dDateTimeUTC As System.DateTime
        Try
            Select Case dDateTime.Kind
                Case DateTimeKind.Local
                    dDateTimeUTC = dDateTime.ToUniversalTime
                Case DateTimeKind.Utc
                    dDateTimeUTC = dDateTime
                Case Else
                    dDateTime = System.DateTime.SpecifyKind(dDateTime, DateTimeKind.Local)
                    dDateTimeUTC = dDateTime.ToUniversalTime
            End Select

            Return dDateTimeUTC
        Catch ex As Exception
            Throw New Exception(ex.StackTrace.Substring(ex.StackTrace.LastIndexOf(vbCrLf) + 4) + vbCrLf + ex.Message)
        End Try
    End Function

End Class
