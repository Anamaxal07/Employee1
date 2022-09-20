Imports System.Data
Imports Npgsql
Public Class Form1

    Dim lv As ListViewItem
    Dim Selected As String

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PopListview()
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

    End Sub

End Class
