Imports System.Data.SqlClient
Imports System.Data

Public Class searchPloio
    Dim mastercon As New appInfo
    Dim mycon As String = mastercon.sqlCon
    Dim db As String = mastercon.database
    Dim con As New SqlConnection
    Public Sub startApp()
        fillCombo()
    End Sub
    Public Sub fillCombo()
        Try
            Dim reader As SqlDataReader
            con.ConnectionString = mycon
            Dim sqlCMD As New SqlCommand("select * from " & db & ".eidosAkinisias", con)
            con.Open()
            ' ***EKTELESH QUERY ARXH
            reader = sqlCMD.ExecuteReader()
            While reader.Read
                eidosAkinisiaCombo.Items.Add(reader.Item("eidosAkinisias"))
            End While

            reader.Close()
            con.Close()
        Catch ex As Exception
            con.Close()
            MsgBox("fillcombo--insertPloio-1-" & ex.Message)
        Finally
            con.Close()
        End Try

        Try
            Dim reader As SqlDataReader
            con.ConnectionString = mycon
            Dim sqlCMD As New SqlCommand("select * from " & db & ".etosT", con)
            con.Open()
            ' ***EKTELESH QUERY ARXH
            reader = sqlCMD.ExecuteReader()
            While reader.Read
                etosCombo.Items.Add(reader.Item("eetos"))
            End While

            reader.Close()
            con.Close()
        Catch ex As Exception
            con.Close()
            MsgBox("fillcombo--insertPloio-2-" & ex.Message)
        Finally
            con.Close()
        End Try
    End Sub
    Public Sub searchData()
        Try
            Dim apoDate As New Date
            Dim mexriDate As Date
            If apodatePick.Text.Length > 0 Then
                apoDate = apodatePick.Text
            Else
                apoDate = "1900-01-01"
            End If
            If mexridatePick.Text.Length > 0 Then
                mexriDate = mexridatePick.Text
            Else
                mexriDate = "2100-01-01"
            End If

            Dim ploioDS As New DataSet
            Dim ploioDA As New SqlDataAdapter
            con.ConnectionString = mycon
            Dim ploioCMD As New SqlCommand
            ploioCMD.Connection = con
            ploioCMD.CommandText = "select * from " & db & ".ploio4plate where onomaPloioy like '%" _
                & onomaploioyText.Text.ToUpper & "%' and eidosAkinisias like '%" & eidosAkinisiaCombo.SelectedItem & "%' and etosAkinisias like '%" & etosCombo.SelectedItem & "%' and dateDB between '" _
                & apoDate.ToString("yyyy-MM-dd") & "' and '" & mexriDate.ToString("yyyy-MM-dd") & "'"
            ploioCMD.CommandType = CommandType.Text
            ploioDA.SelectCommand = ploioCMD
            ploioDA.Fill(ploioDS, "ploioRes")
            con.Close()
            Dim mm As MainWindow = Window.GetWindow(Me)
            mm.bottomGrid.Children.Clear()
            Dim getdata As New dataControl
            getdata.showDataPloio(ploioDS.Tables(0))
            mm.bottomGrid.Children.Add(getdata)
            'For Each dataPloio As DataRow In ploioDS.Tables(0).Rows
            '    MsgBox(dataPloio("idPloio"))
            'Next
        Catch ex As Exception
            MsgBox("searchData---" & ex.Message)
        End Try
        
    End Sub
End Class
