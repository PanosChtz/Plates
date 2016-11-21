Imports System.Data.SqlClient
Imports System.Data

Public Class insertPlateNum
    Dim mastercon As New appInfo
    Dim mycon As String = mastercon.sqlCon
    Dim db As String = mastercon.database
    Dim con As New SqlConnection

    Public idPloio As String
    Public eidosAkin As String
    Public etosAkin As String
    Public Sub showPloio()
        Try
            Dim reader As SqlDataReader
            con.ConnectionString = mycon
            Dim userlog_cmd As New SqlCommand("select * from " & db & ".ploio4plate where idPloio='" & idPloio & "' and eidosAkinisias='" & eidosAkin & "' and etosAkinisias='" & etosAkin & "'", con)
            con.Open()
            ' ***EKTELESH QUERY ARXH
            reader = userlog_cmd.ExecuteReader()
            If reader.Read Then
                ploioTab.Header = reader.Item("onomaPloioy")
                idPloioLab.Content = idPloio
                onomaPloioLab.Content = reader.Item("onomaPloioy")
                eidosAkinisiasLab.Content = eidosAkin
                etosAkinisiasLab.Content = etosAkin
            End If
            reader.Close()
            con.Close()
        Catch ex As Exception
            con.Close()
            MsgBox("insertPlateNum--showPloio-1-" & ex.Message)
        Finally
            con.Close()
        End Try

    End Sub
    Public Sub insertNewPlateTab()
        Try
            Dim palteOnoma As String = "PlateNum"
            Dim id As New PlateID
            Dim idPlateSub As String = id.getPlateId(idPloio, etosAkin)

            Dim idsplit() As String
            idsplit = idPlateSub.Split("-")

            Dim plateNew As New PlateNum
            plateNew.idPloio = idPloio
            plateNew.idPlate = idPlateSub
            plateNew.etosAkin = etosAkinisiasLab.Content
            plateNew.eidosAkinisiasLab.Content = eidosAkinisiasLab.Content
            Dim neoTab As New TabItem
            With neoTab
                .Header = "PlateNum" & idsplit(2)
                .Name = "PlateNum" & idsplit(2)
            End With
            Dim neoTabGrid As New Grid
            neoTabGrid.Children.Add(plateNew)
            neoTab.Content = neoTabGrid
            plateControlTabs.Items.Add(neoTab)
        Catch ex As Exception
            MsgBox("neoPlate---" & ex.Message)
        End Try
    End Sub
    Public Sub insertPalioPlateTab(ByVal idplateBy As String)
        Try
            Dim palteOnoma As String = "PlateNum"
            Dim id As New PlateID
            Dim idPlateSub As String = idplateBy

            Dim idsplit() As String
            idsplit = idPlateSub.Split("-")

            Dim plateNew As New PlateNum
            plateNew.idPloio = idPloio
            plateNew.idPlate = idPlateSub
            plateNew.etosAkin = etosAkinisiasLab.Content
            plateNew.eidosAkinisiasLab.Content = eidosAkinisiasLab.Content
            Dim neoTab As New TabItem
            With neoTab
                .Header = "PlateNum" & idsplit(2)
                .Name = "PlateNum" & idsplit(2)
            End With
            Try
                Dim reader As SqlDataReader
                con.ConnectionString = mycon
                Dim userlog_cmd As New SqlCommand("select * from " & db & ".platesNum where idPloio='" & idPloio & "' and idPlateNum='" & idPlateSub & "' and eidosAkinisias='" & eidosAkin & "' and etosAkinisias='" & etosAkin & "'", con)
                con.Open()
                ' ***EKTELESH QUERY ARXH
                reader = userlog_cmd.ExecuteReader()
                If reader.Read Then
                    plateNew.paxosBox.Text = reader.Item("paxosElasmMM")
                End If
                reader.Close()
                con.Close()
            Catch ex As Exception
                con.Close()
                MsgBox("insertPalioPlateTab--1-" & ex.Message)
            Finally
                con.Close()
            End Try
            Dim neoTabGrid As New Grid
            neoTabGrid.Children.Add(plateNew)
            neoTab.Content = neoTabGrid
            plateControlTabs.Items.Add(neoTab)
            neoTab.IsSelected = True
        Catch ex As Exception
            MsgBox("neoPlate---" & ex.Message)
        End Try
    End Sub
    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        insertNewPlateTab()
    End Sub
    Public Sub searchDataPlate()
        Try
            Dim ploioDS As New DataSet
            Dim ploioDA As New SqlDataAdapter
            con.ConnectionString = mycon
            Dim ploioCMD As New SqlCommand
            ploioCMD.Connection = con
            ploioCMD.CommandText = "select idPlateNum,idPloio,arithDee,anagomAntikata,ylikoElasm,telikoEmbado,eidosAkinisias,etosAkinisias from " & db & ".platesNum where idPloio='" & idPloio & "'"
            ploioCMD.CommandType = CommandType.Text
            ploioDA.SelectCommand = ploioCMD
            ploioDA.Fill(ploioDS, "ploioRes")
            con.Close()
            Dim mmain As MainWindow = Window.GetWindow(Me)
            mmain.bottomGrid.Children.Clear()
            Dim getdata As New dataControl
            getdata.showDataPloioPlates(ploioDS.Tables(0))
            mmain.bottomGrid.Children.Add(getdata)
            'For Each dataPloio As DataRow In ploioDS.Tables(0).Rows
            '    MsgBox(dataPloio("idPloio"))
            'Next
        Catch ex As Exception
            MsgBox("searchData---" & ex.Message)
        End Try

    End Sub
End Class
