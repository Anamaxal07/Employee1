Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports Npgsql
Public Class Form1

    Dim lv As ListViewItem
    Dim Selected As String

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PoplistView()
    End Sub

    Private Sub PoplistView()
        ListView1.Clear()
        With ListView1
            .View = View.Details
            .GridLines = True
            .Columns.Add("ID", 40)
            .Columns.Add("Last Name", 110)
            .Columns.Add("First Name", 110)
            .Columns.Add("Position", 110)
        End With

        openCon()
        sql = "Select * from tbl_empinfo"
        cmd = New NpgsqlCommand(sql, cn)
        dr = cmd.ExecuteReader()

        Do While dr.Read() = True
            lv = New ListViewItem(dr("empno").ToString)
            lv.SubItems.Add(dr("emplastname"))
            lv.SubItems.Add(dr("empfirstname"))
            lv.SubItems.Add(dr("empposition"))
            ListView1.Items.Add(lv)
        Loop
        cn.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If txtempno.Text = "" Then
            MessageBox.Show("No Id or Invalid Format", "No entry")
        Else txtlastname.Text = ""
            MessageBox.Show("No Id or Invalid Format", "No entry")
        End If


        If MsgBox("Are you sure to save this record", vbQuestion + vbYesNo) = vbYes Then
            openCon()
            sql = "INSERT INTO tbl_empinfo (empno, emplastname, empfirstname, empmidinitial, empaddress, empgender, empcontact, empposition)" _
            & "Values ('" & (Me.txtempno.Text) & "','" & Me.txtlastname.Text & "','" & Me.txtfirstname.Text & "','" & Me.txtmi.Text & "','" & Me.txtaddress.Text & "','" & Me.cmbgender.Text & "','" & Me.txtcontact.Text & "','" & Me.cmbposition.Text & "')"
            cmd = New NpgsqlCommand(sql, cn)
            cmd.ExecuteNonQuery()
            cn.Close()

        End If
        PoplistView()
        txtempno.Clear()
        txtlastname.Clear()
        txtfirstname.Clear()
        txtmi.Clear()
        txtaddress.Clear()
        txtcontact.Clear()
        cmbposition.ResetText()
        cmbgender.ResetText()
    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged
        Dim i As Integer
        For i = 0 To ListView1.SelectedItems.Count - 1

            Selected = ListView1.SelectedItems(i).Text
            openCon()
            sql = "Select * from tbl_empinfo where empno = '" & Selected & "'"
            cmd = New NpgsqlCommand(sql, cn)
            dr = cmd.ExecuteReader

            dr.Read()
            Me.txtempno.Text = dr("empno")
            Me.txtlastname.Text = dr("emplastname")
            Me.txtfirstname.Text = dr("empfirstname")
            Me.txtmi.Text = dr("empmidinitial")
            Me.txtaddress.Text = dr("empaddress")
            Me.cmbgender.Text = dr("empgender")
            Me.txtcontact.Text = dr("empcontact")
            Me.cmbposition.Text = dr("empposition")
            cn.Close()

        Next

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If MsgBox("Are you sure to delete this record?", vbQuestion + vbYesNo) = vbYes Then
            openCon()
            sql = "DELETE FROM tbl_empinfo where empno = '" & Selected & "'"
            cmd = New NpgsqlCommand(sql, cn)
            cmd.ExecuteNonQuery()
            cn.Close()


        End If
        PoplistView()
        txtempno.Clear()
        txtlastname.Clear()
        txtfirstname.Clear()
        txtmi.Clear()
        txtaddress.Clear()
        txtcontact.Clear()
        cmbposition.ResetText()
        cmbgender.ResetText()


    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If MsgBox("Are you sure to update this record?", vbQuestion + vbYesNo) = vbYes Then
            openCon()
            sql = "UPDATE tbl_empinfo SET empno='" & Me.txtempno.Text & "', emplastname='" & Me.txtlastname.Text & "', empfirstname='" & Me.txtfirstname.Text & "', empmidinitial='" & Me.txtmi.Text & "', empaddress='" & Me.txtaddress.Text & "', empgender='" & Me.cmbgender.Text & "', empcontact='" & Me.txtcontact.Text & "', empposition='" & Me.cmbposition.Text & "' Where empno = '" & Selected & "'"
            cmd = New NpgsqlCommand(sql, cn)
            cmd.ExecuteNonQuery()
            cn.Close()

            PoplistView()
            txtempno.Clear()
            txtlastname.Clear()
            txtfirstname.Clear()
            txtmi.Clear()
            txtaddress.Clear()
            txtcontact.Clear()
            cmbposition.ResetText()
            cmbgender.ResetText()



        End If
    End Sub

    Private Sub txtempno_TextChanged(sender As Object, e As EventArgs) Handles txtempno.TextChanged

    End Sub
End Class
