Imports System.Data.SqlClient
Imports System.Data

Public Class dataControl
    Dim mastercon As New appInfo
    Dim mycon As String = mastercon.sqlCon
    Dim db As String = mastercon.database
    Dim con As New SqlConnection
    Public Sub startApp()


    End Sub
    Public Sub showDataPloio(ByVal dataT As DataTable)
        For Each dataTrow As DataRow In dataT.Rows
            Dim ddate As Date = dataTrow("dateDB")
            dataTrow("dateDB") = ddate.ToString("dd-MM-yyyy")
        Next
        dataT.Columns("idPloio").ColumnName = "ID ΠΛΟΙΟΥ"
        dataT.Columns("onomaPloioy").ColumnName = "ΟΝΟΜΑ ΠΛΟΙΟΥ"
        dataT.Columns("pleyriko").ColumnName = "ΠΛΕΥΡΙΚΟ ΠΛΟΙΟΥ"
        dataT.Columns("eidosAkinisias").ColumnName = "ΕΙΔΟΣ ΑΚΙΝΗΣΙΑΣ"
        dataT.Columns("etosAkinisias").ColumnName = "ΕΤΟΣ"
        dataT.Columns("dateDB").ColumnName = "ΗΜΕΡ ΕΙΣΑΓΩΓΗΣ"

        resultDataGrid.ItemsSource = dataT.DefaultView
    End Sub
    Public Sub showDataPloioPlates(ByVal dataT As DataTable)

        dataT.Columns("idPlateNum").ColumnName = "ID PLATENUM"
        dataT.Columns("idPloio").ColumnName = "ID ΠΛΟΙΟΥ"
        dataT.Columns("arithDee").ColumnName = "ΑΡΙΘΜΟΣ ΔΕΕ"
        dataT.Columns("anagomAntikata").ColumnName = "ΕΡΓΑΣΙΑ"
        dataT.Columns("ylikoElasm").ColumnName = "ΥΛΙΚΟ ΕΛΑΣΜΑΤΟΣ"
        dataT.Columns("telikoEmbado").ColumnName = "ΤΕΛΙΚΟ ΕΜΒΑΔΟ"
        dataT.Columns("eidosAkinisias").ColumnName = "ΕΙΔΟΣ ΑΚΙΝΗΣΙΑΣ"
        dataT.Columns("etosAkinisias").ColumnName = "ΕΤΟΣ ΑΚΙΝΗΣΙΑΣ"

        resultDataGrid.ItemsSource = dataT.DefaultView
    End Sub
    Public Sub mouse2Click()
        Dim rownum As String = resultDataGrid.SelectedIndex
        Dim idBlock As TextBlock = resultDataGrid.Columns(0).GetCellContent(resultDataGrid.Items(rownum))
        If idBlock.Text Like "#####-####-*" Then
            Dim eidosAkinisiasBlock As TextBlock = resultDataGrid.Columns(6).GetCellContent(resultDataGrid.Items(rownum))
            Dim etosAkinisiasBlock As TextBlock = resultDataGrid.Columns(7).GetCellContent(resultDataGrid.Items(rownum))
            Dim fetchPloio As New insertPlateNum
            Dim idPloioBlock As TextBlock = resultDataGrid.Columns(1).GetCellContent(resultDataGrid.Items(rownum))
            fetchPloio.idPloio = idPloioBlock.Text
            fetchPloio.eidosAkin = eidosAkinisiasBlock.Text
            fetchPloio.etosAkin = etosAkinisiasBlock.Text
            fetchPloio.showPloio()
            fetchPloio.insertPalioPlateTab(idBlock.Text)
            Dim mm As MainWindow = Window.GetWindow(Me)
            mm.topGrid.Children.Clear()
            mm.topGrid.Children.Add(fetchPloio)
            Exit Sub
        ElseIf idBlock.Text Like "#####" Then
            Dim eidosAkinisiasBlock As TextBlock = resultDataGrid.Columns(3).GetCellContent(resultDataGrid.Items(rownum))
            Dim etosAkinisiasBlock As TextBlock = resultDataGrid.Columns(4).GetCellContent(resultDataGrid.Items(rownum))
            Dim fetchPloio As New insertPlateNum
            fetchPloio.idPloio = idBlock.Text
            fetchPloio.eidosAkin = eidosAkinisiasBlock.Text
            fetchPloio.etosAkin = etosAkinisiasBlock.Text
            fetchPloio.showPloio()
            Dim mm As MainWindow = Window.GetWindow(Me)
            mm.topGrid.Children.Clear()
            mm.topGrid.Children.Add(fetchPloio)
        End If
        
    End Sub
End Class
