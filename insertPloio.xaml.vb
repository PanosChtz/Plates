Imports System.Data.SqlClient

Public Class insertPloio
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
    Public Sub autoCompl()
        ploiaList.Items.Clear()
        ploiaList.Visibility = Windows.Visibility.Visible
        Try
            Dim reader As SqlDataReader
            con.ConnectionString = mycon
            Dim sqlCMD As New SqlCommand("select onomaPloioy from " & db & ".ploio where onomaPloioy like '%" & onomaploioyText.Text.ToUpper & "%' order by onomaPloioy desc", con)
            con.Open()
            ' ***EKTELESH QUERY ARXH
            reader = sqlCMD.ExecuteReader()
            While reader.Read
                ploiaList.Items.Add(reader.Item("onomaPloioy"))
            End While

            reader.Close()
            con.Close()
        Catch ex As Exception
            con.Close()
            MsgBox("fillcombo--insertPloio-1-" & ex.Message)
        Finally
            con.Close()
        End Try
    End Sub
    Public Sub onomaSelected()
        onomaploioyText.Text = String.Empty
        onomaploioyText.Text = ploiaList.SelectedItem
        'ploiaList.Visibility = Windows.Visibility.Hidden
        Try
            Dim reader As SqlDataReader
            con.ConnectionString = mycon
            Dim sqlCMD As New SqlCommand("select * from " & db & ".ploio where onomaPloioy='" & onomaploioyText.Text.ToUpper & "'", con)
            con.Open()
            ' ***EKTELESH QUERY ARXH
            reader = sqlCMD.ExecuteReader()
            While reader.Read
                tiposLab.Content = reader.Item("tiposPloioy")
                pleyrikoLab.Content = reader.Item("pleyrikoPloioy")
                ypiresiaLab.Content = reader.Item("ypiresiaPloioy")
            End While

            reader.Close()
            con.Close()
        Catch ex As Exception
            con.Close()
            MsgBox("fillcombo--insertPloio-1-" & ex.Message)
        Finally
            con.Close()
        End Try
    End Sub
    Public Sub insertPloioSub()
        Dim idiosTiposAkinisias As Boolean = False
        'TSEK EAN YPARXEI IDIOS TIPOS AKINHSIAS MESA STO ETOS
        Try
            Dim reader As SqlDataReader
            con.ConnectionString = mycon
            Dim sqlCMD As New SqlCommand("select * from " & db & ".ploio4plate where eidosAkinisias='" & eidosAkinisiaCombo.SelectedItem & "' and etosAkinisias='" & etosCombo.SelectedItem & "'", con)
            con.Open()
            ' ***EKTELESH QUERY ARXH
            reader = sqlCMD.ExecuteReader()
            If reader.Read Then
                idiosTiposAkinisias = True
            End If

            reader.Close()
            con.Close()
        Catch ex As Exception
            con.Close()
            MsgBox("insertPloio--insertPloioSub-1-" & ex.Message)
        Finally
            con.Close()
        End Try
        '-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*
        If idiosTiposAkinisias = True Then
            MsgBox("ΥΠΑΡΧΕΙ ΤΟ ΕΙΔΟΣ ΤΗΣ ΑΚΙΝΗΣΙΑΣ ΣΤΟ ΤΡΕΧΟΝ ΕΤΟΣ", MsgBoxStyle.Critical)
            Exit Sub
        End If
        Try
            Dim ddate As Date = datePick.Text
            Dim idPloio4DB As New Fetch
            Dim reader As SqlDataReader
            con.ConnectionString = mycon
            Dim sqlCMD As New SqlCommand("insert into " & db & ".ploio4plate (idPloio,onomaPloioy,pleyriko,eidosAkinisias,etosAkinisias,dateDB)" _
                                              & " values ('" & idPloio4DB.getIdPloio(onomaploioyText.Text.ToUpper) & "','" & onomaploioyText.Text.ToUpper & "'," _
                                              & "'" & pleyrikoLab.Content & "','" & eidosAkinisiaCombo.SelectedItem.ToString & "','" & etosCombo.SelectedItem.ToString & "'," _
                                              & "'" & ddate.ToString("yyyy-MM-dd") & "')", con)
            con.Open()
            ' ***EKTELESH QUERY ARXH
            reader = sqlCMD.ExecuteReader()
            reader.Close()
            con.Close()
        Catch ex As Exception
            con.Close()
            MsgBox("insertPloio--insertPloioSub-2-" & ex.Message)
        Finally
            con.Close()
        End Try
    End Sub
End Class
