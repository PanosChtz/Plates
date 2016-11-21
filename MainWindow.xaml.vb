Imports System.Data.SqlClient

Class MainWindow
    Dim mastercon As New appInfo
    Dim mycon As String = mastercon.sqlCon
    Dim db As String = mastercon.database
    Dim con As New SqlConnection
    Public Sub getInsertPlate()
        Dim insPlate As New insertPlateNum
        topGrid.Children.Add(insPlate)
    End Sub
    Public Sub getInsertPloio()
        Dim insPloio As New insertPloio
        topGrid.Children.Add(insPloio)
    End Sub
    Public Sub getSearchPloio()
        Dim searchPloio As New searchPloio
        topGrid.Children.Add(searchPloio)
    End Sub
End Class
