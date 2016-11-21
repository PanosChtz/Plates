Imports System.Data.SqlClient

Public Class Fetch
    Dim mastercon As New appInfo
    Dim mycon As String = mastercon.sqlCon
    Dim db As String = mastercon.database
    Dim con As New SqlConnection
    Public Function getIdPloio(ByVal onomaPloio As String) As String
        Try
            Dim reader As SqlDataReader
            con.ConnectionString = mycon
            Dim userlog_cmd As New SqlCommand("select * from " & db & ".ploio where onomaPloioy='" & onomaPloio & "'", con)
            con.Open()
            ' ***EKTELESH QUERY ARXH
            reader = userlog_cmd.ExecuteReader()
            If reader.Read Then
                Return reader.Item("idPloio")
                Exit Function
            End If
            reader.Close()
            con.Close()
        Catch ex As Exception
            con.Close()
            MsgBox("fetch--getIdPloio-1-" & ex.Message)
        Finally
            con.Close()
        End Try
        Return "NON"
    End Function
End Class
