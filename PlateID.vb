Imports System.Data.SqlClient

Public Class PlateID
    Dim mastercon As New appInfo
    Dim mycon As String = mastercon.sqlCon
    Dim db As String = mastercon.database
    Dim con As New SqlConnection
    Public Function getPlateId(ByVal idPloio As String, ByVal etosAkin As String)
        Try
            Dim idCheck As Boolean = False
            Dim rnd As New Random
            Dim idPlate As String = String.Empty
            While idCheck = False
                idPlate = idPloio & "-" & etosAkin & "-" & rnd.Next(9999, 99999)
                idCheck = checkID(idPlate)
            End While
            Return idPlate
        Catch ex As Exception
            MsgBox("PlateID---getPlateId---" & ex.Message)
        Finally

        End Try
    End Function
    Private Function checkID(ByVal id2Check As String) As Boolean
        Try
            Dim check As Boolean = True
            Dim reader As SqlDataReader
            con.ConnectionString = mycon
            Dim userlog_cmd As New SqlCommand("select iidd from " & db & ".idTable where iidd='" & id2Check & "'", con)
            con.Open()
            ' ***EKTELESH QUERY ARXH
            reader = userlog_cmd.ExecuteReader()
            If reader.Read Then
                check = False
            Else
                check = True
                Return check
            End If
            reader.Close()
            con.Close()
        Catch ex As Exception
            con.Close()
            'MsgBox("insertPlateNum--showPloio-1-" & ex.Message)
        Finally
            con.Close()
        End Try
    End Function

End Class
