Imports System.Data.SqlClient

Public Class PlateNum
    Dim mastercon As New appInfo
    Dim mycon As String = mastercon.sqlCon
    Dim db As String = mastercon.database
    Dim con As New SqlConnection
    Public idPloio As String
    Public etosAkin As String
    Public idPlate As String
    Dim runOnce As Boolean = False
    Public Sub appStart()
        idPlateLab.Content = idPlate
        If runOnce = False Then
            fillCombo()
        End If
    End Sub
    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        insertPlateNum()
    End Sub
    Public Sub fillCombo()
        Try
            runOnce = True
            Dim reader As SqlDataReader
            con.ConnectionString = mycon
            Dim userlog_cmd As New SqlCommand("select paxosTimi from " & db & ".timesCombo", con)
            con.Open()
            ' ***EKTELESH QUERY ARXH
            reader = userlog_cmd.ExecuteReader()
            While reader.Read
                If reader.Item("paxosTimi").ToString.Length > 0 Then
                    paxosCombo.Items.Add(reader.Item("paxosTimi"))
                Else
                    Exit While
                End If
            End While
            reader.Close()
            con.Close()
        Catch ex As Exception
            con.Close()
            MsgBox("fillCombo-1-" & ex.Message)
        Finally
            con.Close()
        End Try
        Try
            runOnce = True
            Dim reader As SqlDataReader
            con.ConnectionString = mycon
            Dim userlog_cmd As New SqlCommand("select embadoTimi from " & db & ".timesCombo", con)
            con.Open()
            ' ***EKTELESH QUERY ARXH
            reader = userlog_cmd.ExecuteReader()
            While reader.Read
                If reader.Item("embadoTimi").ToString.Length > 0 Then
                    embadoCombo.Items.Add(reader.Item("embadoTimi"))
                Else
                    Exit While
                End If
            End While
            reader.Close()
            con.Close()
        Catch ex As Exception
            con.Close()
            MsgBox("fillCombo-2-" & ex.Message)
        Finally
            con.Close()
        End Try
        Try
            runOnce = True
            Dim reader As SqlDataReader
            con.ConnectionString = mycon
            Dim userlog_cmd As New SqlCommand("select eidosElasmatos from " & db & ".timesCombo", con)
            con.Open()
            ' ***EKTELESH QUERY ARXH
            reader = userlog_cmd.ExecuteReader()
            While reader.Read
                If reader.Item("eidosElasmatos").ToString.Length > 0 Then
                    eidosCombo.Items.Add(reader.Item("eidosElasmatos"))
                Else
                    Exit While
                End If
            End While
            reader.Close()
            con.Close()
        Catch ex As Exception
            con.Close()
            MsgBox("fillCombo-3-" & ex.Message)
        Finally
            con.Close()
        End Try
        ylikoCombo.Items.Add("")
        Try
            runOnce = True
            Dim reader As SqlDataReader
            con.ConnectionString = mycon
            Dim userlog_cmd As New SqlCommand("select * from " & db & ".ylikaElasm", con)
            con.Open()
            ' ***EKTELESH QUERY ARXH
            reader = userlog_cmd.ExecuteReader()
            While reader.Read
                ylikoCombo.Items.Add(reader.Item("yliko"))
            End While
            reader.Close()
            con.Close()
        Catch ex As Exception
            con.Close()
            MsgBox("fillCombo-4-" & ex.Message)
        Finally
            con.Close()
        End Try
    End Sub
    Public Sub eidikoChange()
        Try
            runOnce = True
            Dim reader As SqlDataReader
            con.ConnectionString = mycon
            Dim userlog_cmd As New SqlCommand("select * from " & db & ".ylikaElasm where yliko='" & ylikoCombo.SelectedItem & "'", con)
            con.Open()
            ' ***EKTELESH QUERY ARXH
            reader = userlog_cmd.ExecuteReader()
            While reader.Read
                eidikoLab.Content = reader.Item("eidikoBaros")
            End While
            reader.Close()
            con.Close()
        Catch ex As Exception
            con.Close()
            MsgBox("fillCombo-4-" & ex.Message)
        Finally
            con.Close()
        End Try
    End Sub
    Public Sub insertPlateNum()
        Try
            Dim paxosFinal As New Decimal
            Dim embadoFinal As New Decimal
            If paxosBox.Text.Length < 1 Or paxosCombo.SelectedIndex < 0 Then
                MsgBox("ΠΑΡΑΚΑΛΩ ΕΛΕΞΤΕ ΤΟ ΠΑΧΟΣ", MsgBoxStyle.Critical)
            Else
                If paxosCombo.SelectedItem.ToString.Equals("cm") Then
                    paxosFinal = Convert.ToDecimal(paxosBox.Text)
                    Dim conv As New Converter
                    paxosFinal = conv.cm2mm(paxosFinal)
                ElseIf paxosCombo.SelectedItem.ToString.Equals("mm") Then
                    paxosFinal = Convert.ToDecimal(paxosBox.Text)
                End If
            End If
            If embadoBox.Text.Length < 1 Or embadoCombo.SelectedIndex < 0 Then
                MsgBox("ΠΑΡΑΚΑΛΩ ΕΛΕΞΤΕ ΤΟ ΕΜΒΑΔΟ", MsgBoxStyle.Critical)
            Else
                If embadoCombo.SelectedItem.ToString.Equals("cm²") Then
                    embadoFinal = Convert.ToDecimal(embadoBox.Text)
                    Dim conv As New Converter
                    embadoFinal = conv.cm2mm(embadoFinal)
                ElseIf embadoCombo.SelectedItem.ToString.Equals("m²") Then
                    embadoFinal = Convert.ToDecimal(embadoBox.Text)
                    Dim conv As New Converter
                    embadoFinal = conv.meter2mm(embadoFinal)
                End If
            End If
            Dim anagAntikata As String = String.Empty
            If anagwCheck.IsChecked = False And antikataCheck.IsChecked = False Then
                MsgBox("ΠΑΡΑΚΑΛΩ ΕΛΕΞΤΕ ΤΑ ΚΟΥΤΑΚΙΑ ΑΝΑΓΟΜΩΣΗ/ΑΝΤΙΚΑΤΑΣΤΑΣΗ", MsgBoxStyle.Critical)
            Else
                If anagwCheck.IsChecked = True Then
                    anagAntikata = "ΑΝΑΓΟΜΩΣΗ"
                End If
                If antikataCheck.IsChecked = True Then
                    anagAntikata = "ΑΝΤΙΚΑΤΑΣΤΑΣΗ"
                End If
            End If
            'MsgBox(paxosFinal)
            'MsgBox(embadoFinal)
            Dim reader As SqlDataReader
            con.ConnectionString = mycon
            Dim userlog_cmd As New SqlCommand("insert into " & db & ".platesNum (idPloio,idPlateNum,eidosAkinisias,etosAkinisias,paxosElasmMM" _
                                              & ",embadoElasmMM,eidikoBarosElasm,eidosElasm,arithDee,anagomAntikata,ylikoElasm,epektasiEmbadoyMM,telikoEmbado) values " _
                                              & "('" & idPloio & "','" & idPlate & "','" & eidosAkinisiasLab.Content & "','" & etosAkin & "','" & paxosFinal.ToString.Replace(",", ".") & "','" & embadoFinal.ToString.Replace(",", ".") & "'," _
                                              & "'" & Convert.ToDecimal(eidikoLab.Content).ToString.Replace(",", ".") & "','" & eidosCombo.SelectedItem & "'," _
                                              & "'" & deeBox.Text.ToUpper & "','" & anagAntikata & "','" & ylikoCombo.SelectedItem & "','0','0.00')", con)
            con.Open()
            ' ***EKTELESH QUERY ARXH
            reader = userlog_cmd.ExecuteReader()
            While reader.Read
                If reader.Item("paxosTimi").ToString.Length > 0 Then
                    paxosCombo.Items.Add(reader.Item("paxosTimi"))
                Else
                    Exit While
                End If
            End While
            reader.Close()
            con.Close()
        Catch ex As Exception
            con.Close()
            MsgBox("insertPlateNum-1-" & ex.Message)
        Finally
            con.Close()
        End Try
    End Sub
End Class
