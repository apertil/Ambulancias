Imports System.Runtime.Serialization

<DataContract>
Public Class MultiLine
    <DataMember>
    Public Property type As String = String.Empty

    <DataMember>
    Public Shadows Property coordinates As List(Of List(Of Double())) = Nothing

End Class
