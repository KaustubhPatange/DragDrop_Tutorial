' NOTE: SET BUTTON AllowDrop PROPERTY TO TRUE
Imports System.Runtime
Imports System.IO

Public Class Form1
    Private MyFile As FileInfo

    Private Sub Button1_DragEnter(sender As Object, e As System.Windows.Forms.DragEventArgs) Handles Button1.DragEnter
        ' DISPLAY BEHAVIOR [MOUSE ICON] FOR FILE DRAG
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        End If
    End Sub

    Private Sub Button1_DragDrop(sender As Object, e As System.Windows.Forms.DragEventArgs) Handles Button1.$safeprojectname$
        ' GET [SINGLE] FILE PATH FROM DROPPED FILE
        Dim file As String = e.Data.GetData(DataFormats.FileDrop)(0)

        ' GET PATH & FILEINFO
        Dim path As String = file
        MyFile = New FileInfo(path)

        ' ABORT IF NOT A VALID FILE
        If String.IsNullOrWhiteSpace(MyFile.Extension) Then Exit Sub

        ' SHOW FILE DETAILS
        ShowFileInfo()

        ' UPDATE FORM CONTROLS
        UpdateControls()
    End Sub

    Private Sub ShowFileInfo()
        ' DISPLAY FILE ATTRIBUTES
        TextBox1.Text = MyFile.FullName

        lblFileName.Text = "File Name: " & MyFile.Name
        lblFileType.Text = "File Type: " & MyFile.Extension
        lblFileSize.Text = "Size: " & Math.Round(MyFile.Length / 1024) & "KB"
        lblCreated.Text = "Created: " & MyFile.CreationTime
        lblModified.Text = "Modified: " & MyFile.LastWriteTime

        pbIcon.Image = Icon.ExtractAssociatedIcon(MyFile.FullName).ToBitmap
    End Sub

    Private Sub UpdateControls()
        'GET FILE NAME FROM PATH & RESIZE BUTTON TO FIT TEXT
        Button1.Text = MyFile.Name ' Set button text to file name
        Button1.Width = GetButtonSize(MyFile.Name) ' Use function to calulate new size

        ' MOVE "DROP HERE" LABEL WITH RESIZED BUTTON
        Label1.Left = Button1.Left + Button1.Width + 20
    End Sub

    Private Function GetButtonSize(text As String) As Integer
        ' MEASURE SIZE OF NEW TEXT & ADD BUTTON MARGINS
        Return Button1.CreateGraphics.MeasureString(text, Button1.Font).Width() + Button1.Margin.Left + Button1.Margin.Right + 5
    End Function

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Try
            ' EXECUTE OR OPEN FILE WITH DEFAULT PROGRAM
            Process.Start(TextBox1.Text)
        Catch ex As Exception
        End Try
    End Sub
End Class
