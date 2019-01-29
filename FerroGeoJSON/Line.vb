Imports System.Runtime.Serialization

<DataContract>
Public Class Line
    <DataMember>
    Public Property type As String = String.Empty

    <DataMember>
    Public Shadows Property coordinates As List(Of Double()) = Nothing

End Class
