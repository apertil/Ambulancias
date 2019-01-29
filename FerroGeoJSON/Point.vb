Imports System.Runtime.Serialization

<DataContract>
Public Class Point
    <DataMember>
    Public Property type As String = String.Empty

    <DataMember>
    Public Shadows Property coordinates As Double()

End Class
