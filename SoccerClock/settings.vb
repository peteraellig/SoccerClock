Imports System.IO
Imports System.Text.RegularExpressions

Public Class settings

    'variable for filename of the initial settings (ip, color, channel)
    Private Shared filename As String = "C:\vMix\soccerclock\soccerclock.xml"
    Private directory As String = "C:\vMix\soccerclock"
    Private selection As String
    Private Sub settings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FillComboBoxWithClubNames()
        publicdisplay_checked()
        Me.Text = My.Application.Info.Title & " " & My.Application.Info.Version.ToString & " settings | " & My.Application.Info.CompanyName + " - " & My.Application.Info.Copyright
    End Sub

    Private Sub Label_Click(sender As Object, e As EventArgs) Handles Label8.Click, Label19.Click, Label59.Click, Label61.Click
        'sub for opening a filedialog, when clicked a label
        Dim label As Label = CType(sender, Label)
        Dim labelName As String = label.Name


        ' extract the label number
        Dim regex As New Regex("\d+")
        Dim match As Match = regex.Match(labelName)

        If match.Success Then
            Dim labelNumber As Integer = CInt(match.Value)

            ' find the corresponding textbox
            Dim textboxName As String = "textbox" & labelNumber.ToString()
            Dim textbox As TextBox = CType(Me.Controls(textboxName), TextBox)

            ' open the file dialog
            Using openFileDialog As New OpenFileDialog
                openFileDialog.InitialDirectory = "C:\vmix\soccerclock\titles"
                If openFileDialog.ShowDialog() = DialogResult.OK Then
                    textbox.Text = openFileDialog.FileName
                Else
                    If textbox IsNot Nothing Then
                        textbox.Text = "not set"
                    End If
                End If
            End Using
        Else
            MessageBox.Show("Label number conversion failed for: " & labelName, "Error22", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
    Public Sub LoadXML()
        Try
            'check if file exists
            If Not System.IO.File.Exists(filename) Then
                MessageBox.Show("Settings-file not found, load sample data, create the directory and save the data in " & filename, "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Set_sampledata()
                Set_sampledata_files()
                SaveXML()
                Exit Sub
            End If

            'initialize XML 
            Dim xmlDoc As New Xml.XmlDocument()
            xmlDoc.Load(filename)

            'loop through TextBox1 - TextBox40
            For i As Integer = 1 To 40
                Dim textBox As TextBox = CType(Me.Controls("TextBox" & i), TextBox)
                Dim node As Xml.XmlNode = xmlDoc.SelectSingleNode("/Settings/TextBoxes/TextBox" & i)
                If node IsNot Nothing Then
                    textBox.Text = node.InnerText
                End If
            Next

            ''load checkBox1 state
            'Dim checkBox1Node As Xml.XmlNode = xmlDoc.SelectSingleNode("/Settings/CheckBoxes/CheckBox1")
            'If checkBox1Node IsNot Nothing Then
            '    CheckBox1.Checked = Boolean.Parse(checkBox1Node.InnerText)
            'End If

            ''load checkBox2 state
            'Dim checkBox2Node As Xml.XmlNode = xmlDoc.SelectSingleNode("/Settings/CheckBoxes/CheckBox2")
            'If checkBox2Node IsNot Nothing Then
            '    CheckBox2.Checked = Boolean.Parse(checkBox2Node.InnerText)
            'End If

            ''load checkBox3 state
            'Dim checkBox3Node As Xml.XmlNode = xmlDoc.SelectSingleNode("/Settings/CheckBoxes/CheckBox3")
            'If checkBox3Node IsNot Nothing Then
            '    CheckBox3.Checked = Boolean.Parse(checkBox3Node.InnerText)
            'End If

            ''load checkBox4 state
            'Dim checkBox4Node As Xml.XmlNode = xmlDoc.SelectSingleNode("/Settings/CheckBoxes/CheckBox4")
            'If checkBox4Node IsNot Nothing Then
            '    CheckBox4.Checked = Boolean.Parse(checkBox4Node.InnerText)
            'End If

            ''load checkBox5 state
            'Dim checkBox5Node As Xml.XmlNode = xmlDoc.SelectSingleNode("/Settings/CheckBoxes/CheckBox5")
            'If checkBox5Node IsNot Nothing Then
            '    CheckBox5.Checked = Boolean.Parse(checkBox5Node.InnerText)
            'End If

            ' Liste der CheckBoxen erstellen - Nummer explizit neben jeder Checkbox, statt sie
            ' aus der Array-Position abzuleiten: CheckBox7 existiert nicht (Nummerierungslücke),
            ' ein rein positionsbasiertes "CheckBox" & (Index+1) würde CheckBox8 sonst fälschlich
            ' unter dem Knoten "CheckBox7" lesen/schreiben.
            Dim checkBoxes As CheckBox() = {CheckBox1, CheckBox2, CheckBox3, CheckBox4, CheckBox5, CheckBox6, CheckBox8}
            Dim checkBoxNumbers As Integer() = {1, 2, 3, 4, 5, 6, 8}

            ' Schleife über die CheckBoxen
            For idx As Integer = 0 To checkBoxes.Length - 1
                ' XML-Knoten für jede CheckBox laden
                Dim checkBoxNode As Xml.XmlNode = xmlDoc.SelectSingleNode("/Settings/CheckBoxes/CheckBox" & checkBoxNumbers(idx).ToString())
                ' CheckBox setzen, falls der Knoten existiert
                If checkBoxNode IsNot Nothing Then
                    checkBoxes(idx).Checked = Boolean.Parse(checkBoxNode.InnerText)
                End If
            Next

            ' load colors
            Dim homeColor1Node As Xml.XmlNode = xmlDoc.SelectSingleNode("/Settings/Colors/HomeColor1")
            Dim homeColor2Node As Xml.XmlNode = xmlDoc.SelectSingleNode("/Settings/Colors/HomeColor2")
            Dim awayColor1Node As Xml.XmlNode = xmlDoc.SelectSingleNode("/Settings/Colors/AwayColor1")
            Dim awayColor2Node As Xml.XmlNode = xmlDoc.SelectSingleNode("/Settings/Colors/AwayColor2")

            If homeColor1Node IsNot Nothing Then
                Fussballuhr.Home_Color1 = homeColor1Node.InnerText
                Fussballuhr.homecolor1.BackColor = ColorTranslator.FromHtml("#" & Fussballuhr.Home_Color1)
            End If

            If homeColor2Node IsNot Nothing Then
                Fussballuhr.Home_Color2 = homeColor2Node.InnerText
                Fussballuhr.homecolor2.BackColor = ColorTranslator.FromHtml("#" & Fussballuhr.Home_Color2)
            End If

            If awayColor1Node IsNot Nothing Then
                Fussballuhr.Away_Color1 = awayColor1Node.InnerText
                Fussballuhr.awaycolor1.BackColor = ColorTranslator.FromHtml("#" & Fussballuhr.Away_Color1)
            End If

            If awayColor2Node IsNot Nothing Then
                Fussballuhr.Away_Color2 = awayColor2Node.InnerText
                Fussballuhr.awaycolor2.BackColor = ColorTranslator.FromHtml("#" & Fussballuhr.Away_Color2)
            End If

            ' load TCP/HTTP ports (vMix-Versand per HTTP statt TCP, siehe CheckBox6) - eigene
            ' Knoten statt Teil der TextBox1-40-Schleife, analog zu den Colors weiter oben.
            Dim tcpPortNode As Xml.XmlNode = xmlDoc.SelectSingleNode("/Settings/Network/TcpPort")
            If tcpPortNode IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(tcpPortNode.InnerText) Then
                TextBox43.Text = tcpPortNode.InnerText
            End If
            Dim httpPortNode As Xml.XmlNode = xmlDoc.SelectSingleNode("/Settings/Network/HttpPort")
            If httpPortNode IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(httpPortNode.InnerText) Then
                TextBox44.Text = httpPortNode.InnerText
            End If

            ' load per-content Overlay-Kanäle (Scorebug/Titel/Werbung), analog zu Tennis26s
            ' ComboBoxValues(1-4) - jeder Buttontyp toggelt seinen eigenen vMix-Overlay-Kanal,
            ' statt sich (wie vorher über Fussballuhr.Overlay) einen einzigen zu teilen.
            Dim scorebugOverlayNode As Xml.XmlNode = xmlDoc.SelectSingleNode("/Settings/Overlays/ScorebugOverlay")
            If scorebugOverlayNode IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(scorebugOverlayNode.InnerText) Then
                ComboBox2.Text = scorebugOverlayNode.InnerText
            End If
            Dim titelOverlayNode As Xml.XmlNode = xmlDoc.SelectSingleNode("/Settings/Overlays/TitelOverlay")
            If titelOverlayNode IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(titelOverlayNode.InnerText) Then
                ComboBox3.Text = titelOverlayNode.InnerText
            End If
            Dim werbungOverlayNode As Xml.XmlNode = xmlDoc.SelectSingleNode("/Settings/Overlays/WerbungOverlay")
            If werbungOverlayNode IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(werbungOverlayNode.InnerText) Then
                ComboBox4.Text = werbungOverlayNode.InnerText
            End If
            Dim werbungLabel1Node As Xml.XmlNode = xmlDoc.SelectSingleNode("/Settings/Overlays/WerbungLabel1")
            If werbungLabel1Node IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(werbungLabel1Node.InnerText) Then
                TextBox45.Text = werbungLabel1Node.InnerText
            End If
            Dim werbungLabel2Node As Xml.XmlNode = xmlDoc.SelectSingleNode("/Settings/Overlays/WerbungLabel2")
            If werbungLabel2Node IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(werbungLabel2Node.InnerText) Then
                TextBox46.Text = werbungLabel2Node.InnerText
            End If

            ' load Live-JSON-Dateipfad (CheckBox8 schaltet den Export selbst ein/aus,
            ' siehe Fussballuhr.WriteLiveJsonFile)
            Dim jsonPathNode As Xml.XmlNode = xmlDoc.SelectSingleNode("/Settings/LiveJson/JsonPath")
            If jsonPathNode IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(jsonPathNode.InnerText) Then
                TextBox47.Text = jsonPathNode.InnerText
            End If

        Catch ex As Exception
            MessageBox.Show("An error occurred while loading the data from XML: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        If TextBox15.Text.Trim = "" Then TextBox25.Text = "localhost"
        Fussballuhr.IP = TextBox25.Text.Trim
        If TextBox43.Text.Trim = "" Then TextBox43.Text = "8099"
        Dim parsedTcpPort As Integer
        If Integer.TryParse(TextBox43.Text.Trim, parsedTcpPort) Then Fussballuhr.TcpPort = parsedTcpPort
        If TextBox44.Text.Trim = "" Then TextBox44.Text = "8088"
        ' Wie TextBox43/TcpPort behandeln - getrimmt und als gültige Portnummer validiert,
        ' statt den rohen (potenziell mit Whitespace behafteten) TextBox-Inhalt direkt in die
        ' HTTP-URL zu übernehmen. Fussballuhr.PORT bleibt ein String (für die URL-Konkatenation
        ' in VmixHttpSender), aber der Roundtrip durch Integer.TryParse normalisiert ihn.
        Dim parsedHttpPort As Integer
        If Integer.TryParse(TextBox44.Text.Trim, parsedHttpPort) Then
            Fussballuhr.PORT = parsedHttpPort.ToString()
        Else
            Fussballuhr.PORT = TextBox44.Text.Trim
        End If

        ' Overlay-ComboBoxen sind nicht auf DropDownList beschränkt (frei editierbar) - falls
        ' der gespeicherte oder eingegebene Wert keine gültige Kanalnummer 1-8 ist, auf einen
        ' Default zurückfallen statt eine ungültige "OverlayInputXIn"-URL an vMix zu senden.
        NormalizeOverlayComboBox(ComboBox2, "1")
        NormalizeOverlayComboBox(ComboBox3, "2")
        NormalizeOverlayComboBox(ComboBox4, "3")
        If TextBox45.Text.Trim = "" Then TextBox45.Text = "Sponsor 1"
        If TextBox46.Text.Trim = "" Then TextBox46.Text = "Sponsor 2"
        If TextBox47.Text.Trim = "" Then TextBox47.Text = "C:\vmix\soccerclock\data\soccerclock_live.json"
    End Sub

    Private Sub NormalizeOverlayComboBox(combo As ComboBox, defaultValue As String)
        Dim channel As Integer
        If Not Integer.TryParse(combo.Text.Trim, channel) OrElse channel < 1 OrElse channel > 8 Then
            combo.Text = defaultValue
        End If
    End Sub

    Public Sub SaveXML()
        Try
            'check if directory exists
            If Not System.IO.Directory.Exists(directory) Then
                System.IO.Directory.CreateDirectory(directory)
            End If

            'initialize XML writer with formatting enabled
            Using xmlWriter As New Xml.XmlTextWriter(filename, System.Text.Encoding.UTF8)
                xmlWriter.Formatting = Xml.Formatting.Indented
                xmlWriter.Indentation = 4 'set the indentation level (spaces)

                xmlWriter.WriteStartDocument()
                xmlWriter.WriteStartElement("Settings")

                'save textBox values
                xmlWriter.WriteStartElement("TextBoxes")
                For i As Integer = 1 To 40
                    Dim textBox As TextBox = CType(Me.Controls("TextBox" & i), TextBox)
                    xmlWriter.WriteStartElement("TextBox" & i)
                    xmlWriter.WriteString(textBox.Text)
                    xmlWriter.WriteEndElement()
                Next
                xmlWriter.WriteEndElement()

                'save checkBox2 value
                xmlWriter.WriteStartElement("CheckBoxes")
                xmlWriter.WriteStartElement("CheckBox1")
                xmlWriter.WriteString(CheckBox1.Checked.ToString())
                xmlWriter.WriteEndElement()
                xmlWriter.WriteStartElement("CheckBox2")
                xmlWriter.WriteString(CheckBox2.Checked.ToString())
                xmlWriter.WriteEndElement()
                xmlWriter.WriteStartElement("CheckBox3")
                xmlWriter.WriteString(CheckBox3.Checked.ToString())
                xmlWriter.WriteEndElement()
                xmlWriter.WriteStartElement("CheckBox4")
                xmlWriter.WriteString(CheckBox4.Checked.ToString())
                xmlWriter.WriteEndElement()
                xmlWriter.WriteStartElement("CheckBox5")
                xmlWriter.WriteString(CheckBox5.Checked.ToString())
                xmlWriter.WriteEndElement()
                xmlWriter.WriteStartElement("CheckBox6")
                xmlWriter.WriteString(CheckBox6.Checked.ToString())
                xmlWriter.WriteEndElement()
                xmlWriter.WriteStartElement("CheckBox8")
                xmlWriter.WriteString(CheckBox8.Checked.ToString())
                xmlWriter.WriteEndElement()
                xmlWriter.WriteEndElement()

                ' save TCP/HTTP ports (vMix-Versand per HTTP statt TCP, siehe CheckBox6)
                xmlWriter.WriteStartElement("Network")
                xmlWriter.WriteStartElement("TcpPort")
                xmlWriter.WriteString(TextBox43.Text)
                xmlWriter.WriteEndElement()
                xmlWriter.WriteStartElement("HttpPort")
                xmlWriter.WriteString(TextBox44.Text)
                xmlWriter.WriteEndElement()
                xmlWriter.WriteEndElement()

                ' save per-content Overlay-Kanäle (Scorebug/Titel/Werbung) + Werbung-Beschriftungen
                xmlWriter.WriteStartElement("Overlays")
                xmlWriter.WriteStartElement("ScorebugOverlay")
                xmlWriter.WriteString(ComboBox2.Text)
                xmlWriter.WriteEndElement()
                xmlWriter.WriteStartElement("TitelOverlay")
                xmlWriter.WriteString(ComboBox3.Text)
                xmlWriter.WriteEndElement()
                xmlWriter.WriteStartElement("WerbungOverlay")
                xmlWriter.WriteString(ComboBox4.Text)
                xmlWriter.WriteEndElement()
                xmlWriter.WriteStartElement("WerbungLabel1")
                xmlWriter.WriteString(TextBox45.Text)
                xmlWriter.WriteEndElement()
                xmlWriter.WriteStartElement("WerbungLabel2")
                xmlWriter.WriteString(TextBox46.Text)
                xmlWriter.WriteEndElement()
                xmlWriter.WriteEndElement()

                ' save Live-JSON-Dateipfad
                xmlWriter.WriteStartElement("LiveJson")
                xmlWriter.WriteStartElement("JsonPath")
                xmlWriter.WriteString(TextBox47.Text)
                xmlWriter.WriteEndElement()
                xmlWriter.WriteEndElement()

                ' save colors
                xmlWriter.WriteStartElement("Colors")
                xmlWriter.WriteStartElement("HomeColor1")
                xmlWriter.WriteString(Fussballuhr.Home_Color1)
                xmlWriter.WriteEndElement()
                xmlWriter.WriteStartElement("HomeColor2")
                xmlWriter.WriteString(Fussballuhr.Home_Color2)
                xmlWriter.WriteEndElement()
                xmlWriter.WriteStartElement("AwayColor1")
                xmlWriter.WriteString(Fussballuhr.Away_Color1)
                xmlWriter.WriteEndElement()
                xmlWriter.WriteStartElement("AwayColor2")
                xmlWriter.WriteString(Fussballuhr.Away_Color2)
                xmlWriter.WriteEndElement()
                xmlWriter.WriteEndElement()

                xmlWriter.WriteEndElement() ' Settings
                xmlWriter.WriteEndDocument()
            End Using
        Catch ex As Exception
            MessageBox.Show("An error occurred while saving the data to XML: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btn_exit_Click(sender As Object, e As EventArgs) Handles btn_exit.Click
        Fussballuhr.Show()
        Me.Hide()
    End Sub

    Private Sub btn_savesettings_Click(sender As Object, e As EventArgs) Handles btn_savesettings.Click
        Dim title As String = Getgtzip(TextBox32.Text)
        Dim public_displaytitle As String = Getgtzip(TextBox19.Text)
        SaveXML()
        Fussballuhr.Set_Club()
        Fussballuhr.Draw_Logos(public_displaytitle)
        Fussballuhr.Draw_Logos(title)
        Fussballuhr.Set_duration()
        Fussballuhr.Update_result()
        Fussballuhr.Set_Labels()
        TextBox21.Enabled = False
        TextBox22.Enabled = False
        TextBox23.Enabled = False
        TextBox24.Enabled = False
        MsgBox("settings saved to " & filename)
        LoadXML()
    End Sub

    Private Function Getgtzip(filePath As String) As String
        ' Find the last backslash in the path
        Dim lastBackslashIndex As Integer = filePath.LastIndexOf("\")
        ' Return the substring that starts right after the last backslash
        Return filePath.Substring(lastBackslashIndex + 1)
    End Function

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        Fussballuhr.UpdateTimeLabels()
    End Sub

    Private Sub btn_sampledata_Click(sender As Object, e As EventArgs) Handles btn_sampledata.Click
        Set_sampledata()
    End Sub

    Public Sub Set_sampledata()
        TextBox1.Text = "45"
        TextBox2.Text = "15"
        TextBox3.Text = "pause 1st half"
        TextBox4.Text = "pause 2nd half"
        TextBox5.Text = "pause 3rd half"
        TextBox6.Text = "end of match"
        TextBox7.Text = "-"
        TextBox8.Text = "C:\VMIX\soccerclock\titles\scorebug.gtzip"
        TextBox9.Text = "time.Text"
        TextBox10.Text = "overtime.Text"
        TextBox11.Text = "bg_ot.Source"
        TextBox12.Text = "overtime_toplay.Text"
        TextBox13.Text = "bg_ot_toplay.Source"
        TextBox14.Text = "home_short.Text"
        TextBox15.Text = "away_short.Text"
        TextBox16.Text = "home_score.Text"
        TextBox17.Text = "away_score.Text"
        TextBox18.Text = "divider.Text"
        TextBox19.Text = "C:\VMIX\soccerclock\titles\public_display.gtzip"
        TextBox20.Text = "bg_clock.Source"
        TextBox21.Text = "FC St.Gallen"
        TextBox22.Text = "BSC Young Boys"
        TextBox23.Text = "FCSG"
        TextBox24.Text = "YB"
        TextBox25.Text = "localhost"
        TextBox26.Text = "home_long.Text"
        TextBox27.Text = "away_long.Text"
        TextBox28.Text = "home_color1.Fill.Color"
        TextBox29.Text = "home_color2.Fill.Color"
        TextBox30.Text = "away_color1.Fill.Color"
        TextBox31.Text = "away_color2.Fill.Color"
        TextBox32.Text = "C:\VMIX\soccerclock\titles\title.gtzip"
        TextBox33.Text = ""
        TextBox34.Text = "text_1.Text"
        TextBox35.Text = "text_2.Text"
        TextBox36.Text = "text_3.Text"
        TextBox37.Text = "result.Text"
        TextBox38.Text = "Your League"
        TextBox39.Text = "University Stadium"
        TextBox40.Text = "Text 3"
        TextBox41.Text = "bg_result.Source"
        TextBox43.Text = "8099"
        TextBox44.Text = "8088"
        ComboBox2.Text = "1" ' Scorebug-Overlay
        ComboBox3.Text = "2" ' Titel-Overlay (Titel/Pausen/Spielende)
        ComboBox4.Text = "3" ' Werbung-Overlay (sponsor1.gtzip/sponsor2.gtzip)
        TextBox45.Text = "Sponsor 1"
        TextBox46.Text = "Sponsor 2"
        TextBox47.Text = "C:\vmix\soccerclock\data\soccerclock_live.json"

        CheckBox1.Checked = False ' Default value for CheckBox1
        CheckBox2.Checked = False ' Default value for CheckBox2
        CheckBox3.Checked = False ' Default value for CheckBox3
        CheckBox4.Checked = False ' Default value for CheckBox4
        CheckBox5.Checked = True ' Default value for CheckBox5
        CheckBox6.Checked = False ' Default value for CheckBox6 (TCP, not HTTP)
        CheckBox8.Checked = False ' Default value for CheckBox8 (Live-JSON-Export aus)
    End Sub

    Public Sub Set_sampledata_files()
        'saves internal ressources to harddisk

        'checks for logo direcotry
        Dim logosdirectory As String = "C:\vMix\soccerclock\logos"
        'check if directory exists
        If Not System.IO.Directory.Exists(logosdirectory) Then
            System.IO.Directory.CreateDirectory(logosdirectory)
        End If

        'save all logos
        Dim b As Bitmap = My.Resources.FCA_FC_Aarau
        b.Save("C:\vMix\soccerclock\logos\FCA-FC Aarau.png")
        b = My.Resources.FCB_FC_Basel
        b.Save("C:\vMix\soccerclock\logos\FCB-FC Basel.png")
        b = My.Resources.FCL_FC_Luzern
        b.Save("C:\vMix\soccerclock\logos\FCL-FC Luzern.png")
        b = My.Resources.FCSG_FC_St_Gallen
        b.Save("C:\vMix\soccerclock\logos\FCSG-FC St.Gallen.png")
        b = My.Resources.FCZ_FC_Zürich
        b.Save("C:\vMix\soccerclock\logos\FCZ-FC Zürich.png")
        b = My.Resources.GC_Grasshoppers_Club_Zürich
        b.Save("C:\vMix\soccerclock\logos\GC-Grasshoppers Club Zürich.png")
        b = My.Resources.LUG_FC_Lugano
        b.Save("C:\vMix\soccerclock\logos\LUG-FC Lugano.png")
        b = My.Resources.LUZ_FC_Luzern
        b.Save("C:\vMix\soccerclock\logos\LUZ-FC Luzern.png")
        b = My.Resources.SFC_Servette_SC
        b.Save("C:\vMix\soccerclock\logos\SFC-Servette SC.png")
        b = My.Resources.transparent
        b.Save("C:\vMix\soccerclock\logos\transparent.png")
        b = My.Resources.YB_Young_Boys_Bern
        b.Save("C:\vMix\soccerclock\logos\YB-Young Boys Bern.png")
        b = My.Resources.YSF_Yverdon_Sport_FC
        b.Save("C:\vMix\soccerclock\logos\YSF-Yverdon Sport FC.png")

        ' checks for fonts directory
        Dim fontsdirectory As String = "C:\vMix\soccerclock\fonts"
        'check if directory exists
        If Not System.IO.Directory.Exists(fontsdirectory) Then
            System.IO.Directory.CreateDirectory(fontsdirectory)
        End If

        'save all fonts
        Dim c() As Byte
        c = My.Resources.open_sans
        File.WriteAllBytes("C:\vMix\soccerclock\fonts\open-sans.zip", c)
        c = My.Resources.Roboto
        File.WriteAllBytes("C:\vMix\soccerclock\fonts\Roboto.zip", c)


        ' checks for advertising directory
        Dim advertisingdirectory As String = "C:\vMix\soccerclock\advertising"
        'check if directory exists
        If Not System.IO.Directory.Exists(advertisingdirectory) Then
            System.IO.Directory.CreateDirectory(advertisingdirectory)
        End If

        'save all advertising-logos
        Dim a As Bitmap = My.Resources.garage
        a.Save("C:\vMix\soccerclock\advertising\garage.png")
        a = My.Resources.insurance
        a.Save("C:\vMix\soccerclock\advertising\insurance.png")
        a = My.Resources.metzgerei
        a.Save("C:\vMix\soccerclock\advertising\metzgerei.png")


        ' checks for title directory
        Dim titledirectory As String = "C:\vMix\soccerclock\titles"
        'check if directory exists
        If Not System.IO.Directory.Exists(titledirectory) Then
            System.IO.Directory.CreateDirectory(titledirectory)
        End If

        'save all titles
        Dim t() As Byte
        t = My.Resources.public_display
        File.WriteAllBytes("C:\vMix\soccerclock\titles\public_display.gtzip", t)
        t = My.Resources.scorebug
        File.WriteAllBytes("C:\vMix\soccerclock\titles\scorebug.gtzip", t)
        t = My.Resources.title
        File.WriteAllBytes("C:\vMix\soccerclock\titles\title.gtzip", t)

        '' checks PDF directory
        'Dim pdfdirectory As String = "C:\PiPo\manual"
        'If (Not directory.Exists(pdfdirectory)) Then
        '    directory.CreateDirectory(pdfdirectory)
        'End If

        ''save all manuals
        'Dim p() As Byte
        'p = My.Resources.pdf
        'File.WriteAllBytes("c:\PiPo\manual\PiPo Manual.pdf", p)
    End Sub


    Private Sub btn_installtitles_in_vmix_Click(sender As Object, e As EventArgs) Handles btn_installtitles_in_vmix.Click
        Dim sendstring As String = ""

        If TextBox8.Text.Trim > "" Then
            sendstring = "Function=Addinput&Value=Title|" + Me.Controls("TextBox8").Text
            Fussballuhr.SendHTMLRequest(sendstring)
        End If
        If TextBox19.Text.Trim > "" Then
            sendstring = "Function=Addinput&Value=Title|" + TextBox19.Text
            Fussballuhr.SendHTMLRequest(sendstring)
        End If
        If TextBox32.Text.Trim > "" Then
            sendstring = "Function=Addinput&Value=Title|" + TextBox32.Text
            Fussballuhr.SendHTMLRequest(sendstring)
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = True Then
            Fussballuhr.ToolStripStatusLabel7.Text = "PublicDisplay ON"
        Else
            Fussballuhr.ToolStripStatusLabel7.Text = "PublicDisplay OFF"
        End If
        publicdisplay_checked()
    End Sub

    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.Checked = True Then
            Fussballuhr.ToolStripStatusLabel6.Visible = True
            Fussballuhr.CheckVmixConnection()
            Fussballuhr.Connect_to_vMix()
        Else
            Fussballuhr.ToolStripStatusLabel6.Visible = False
        End If
    End Sub

    ' Protokoll gewechselt (HTTP<->TCP) - Erreichbarkeit sofort für das neu gewählte
    ' Protokoll neu prüfen. Ohne das blieb ConnectedState vom vorherigen Protokoll stehen:
    ' schlug der erste Send-Versuch im neuen Protokoll fehl, wurde ConnectedState False und
    ' SendHTMLRequest/VTX blockierten danach JEDES Protokoll, auch ein Zurückwechseln auf das
    ' vorher funktionierende - ohne manuellen Klick auf ToolStripStatusLabel6 ging dann gar
    ' nichts mehr.
    Private Sub CheckBox6_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox6.CheckedChanged
        Fussballuhr.CheckVmixConnection()
        Fussballuhr.Connect_to_vMix()
    End Sub

    Private Sub publicdisplay_checked()
        If CheckBox2.Checked Then
            Label81.Visible = True
            Label82.Visible = True
            PictureBox11.Visible = True
            CheckBox4.Visible = True
            setcolors_publicdisplay()
        Else
            Label81.Visible = False
            Label82.Visible = False
            PictureBox11.Visible = False
            CheckBox4.Visible = False
        End If
    End Sub

    Private Sub setcolors_publicdisplay()
        Dim h1color As String = Fussballuhr.homecolor1.BackColor.ToArgb().ToString("X6").Substring(2)
        Dim h2color As String = Fussballuhr.homecolor2.BackColor.ToArgb().ToString("X6").Substring(2)
        Dim a1color As String = Fussballuhr.awaycolor1.BackColor.ToArgb().ToString("X6").Substring(2)
        Dim a2color As String = Fussballuhr.awaycolor2.BackColor.ToArgb().ToString("X6").Substring(2)
        Dim nametemplate As String = Getgtzip(TextBox19.Text)
        Dim sendstring As String = ""
        sendstring = Fussballuhr.BuildVmixSetCommand("SetColor", nametemplate, "home_color1.Fill.Color", "#" + h1color)
        Fussballuhr.VTX(sendstring)
        sendstring = Fussballuhr.BuildVmixSetCommand("SetColor", nametemplate, "home_color2.Fill.Color", "#" + h2color)
        Fussballuhr.VTX(sendstring)
        sendstring = Fussballuhr.BuildVmixSetCommand("SetColor", nametemplate, "away_color1.Fill.Color", "#" + a1color)
        Fussballuhr.VTX(sendstring)
        sendstring = Fussballuhr.BuildVmixSetCommand("SetColor", nametemplate, "away_color2.Fill.Color", "#" + a2color)
        Fussballuhr.VTX(sendstring)
    End Sub

    Private Sub FillComboBoxWithClubNames()
        ' Pfad zum Verzeichnis mit den Logos
        Dim directoryPath As String = "C:\vmix\soccerclock\logos"

        'check if directory exists
        If Not System.IO.Directory.Exists(directory) Then
            System.IO.Directory.CreateDirectory(directory)
        End If

        ' Überprüfen, ob das Verzeichnis existiert
        If System.IO.Directory.Exists(directoryPath) Then
            ' Alle Dateien im Verzeichnis abrufen
            Dim files As String() = System.IO.Directory.GetFiles(directoryPath)

            ' Die ComboBox leeren
            ComboBox1.Items.Clear()

            ' Durch alle Dateien im Verzeichnis iterieren
            For Each file As String In files
                ' Den Dateinamen ohne Pfad und Erweiterung erhalten
                Dim fileName As String = Path.GetFileNameWithoutExtension(file)

                ' Den Clubnamen (kurze Form und lange Form) extrahieren
                If fileName.Contains("-") Then
                    Dim clubNames As String() = fileName.Split("-"c)
                    Dim clubNameShort As String = clubNames(0)
                    Dim clubNameLong As String = clubNames(1)

                    ' Den extrahierten Clubnamen (kurz und lang) als Dictionary-Eintrag hinzufügen
                    ' ComboBox1.Items.Add(New With {.ShortName = clubNameShort, .LongName = clubNameLong})
                    ComboBox1.Items.Add(fileName)
                End If
            Next

            ' Legt fest, welches Property in der ComboBox angezeigt wird
            ComboBox1.DisplayMember = "ShortName"
        Else
            MessageBox.Show("Verzeichnis existiert nicht: " & directoryPath)
        End If
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        FillComboBoxWithClubNames()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        ' Überprüfen, ob ein Eintrag ausgewählt wurde
        If ComboBox1.SelectedItem IsNot Nothing Then
            ' Überprüfen, ob ein Eintrag ausgewählt wurde
            If ComboBox1.SelectedItem IsNot Nothing Then
                ' Den ausgewählten Clubnamen als vollständigen Namen abrufen
                Dim selectedClub As String = ComboBox1.SelectedItem.ToString()

                ' Kurz- und Langname anhand des Bindestrichs extrahieren
                Dim clubNames As String() = selectedClub.Split("-"c)
                If clubNames.Length = 2 Then
                    If selection = "home" Then
                        TextBox23.Text = clubNames(0) ' Kurzname in TextBox23
                        TextBox21.Text = clubNames(1) ' Langname in TextBox21
                    Else
                        TextBox24.Text = clubNames(0) ' Kurzname in TextBox23
                        TextBox22.Text = clubNames(1) ' Langname in TextBox21
                    End If
                End If
            End If
            End If
        ComboBox1.Visible = False
        btn_select_home.Visible = True
        btn_select_away.Visible = True
    End Sub

    Private Sub btn_select_home_Click(sender As Object, e As EventArgs) Handles btn_select_home.Click, btn_select_away.Click
        ' Casting the sender to a RadioButton
        Dim selectedButton As Button = CType(sender, Button)
        ComboBox1.Text = ""
        Select Case selectedButton.Name
            Case "btn_select_home"
                selection = "home"
                ComboBox1.Visible = True
                btn_select_away.Visible = False
            Case "btn_select_away"
                selection = "away"
                ComboBox1.Visible = True
                btn_select_home.Visible = False
        End Select
    End Sub

    Private Sub textbox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress, TextBox2.KeyPress
        ' Überprüfe, ob das eingegebene Zeichen eine Zahl, die Backspace-Taste oder die Delete-Taste ist
        If Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> ChrW(Keys.Back) AndAlso e.KeyChar <> ChrW(Keys.Delete) Then
            ' Wenn nicht, unterdrücke die Eingabe
            e.Handled = True
            MsgBox("only digits allowed")
        End If
    End Sub

    Private Sub btn_LogoDirectory_Click(sender As Object, e As EventArgs) Handles btn_LogoDirectory.Click
        ' Define the directory path
        Dim directoryPath As String = "C:\VMIX\soccerclock\logos"

        ' Check if the directory exists
        If System.IO.Directory.Exists(directoryPath) Then
            ' Open the directory in Windows Explorer
            Process.Start("explorer.exe", directoryPath)
        Else
            ' Display an error message if the directory doesn't exist
            Console.WriteLine("Directory not found: " & directoryPath)
        End If
    End Sub

    Private Sub btn_Help_teamnames_Click(sender As Object, e As EventArgs) Handles btn_Help_teamnames.Click
        If TextBox42.Visible = False Then
            TextBox42.Visible = True
            btn_LogoDirectory.Visible = True
            TextBox42.BringToFront()
            btn_LogoDirectory.BringToFront()
        Else
            TextBox42.Visible = False
            btn_LogoDirectory.Visible = False
        End If
    End Sub

    Private Sub TextBox42_TextChanged(sender As Object, e As EventArgs) Handles TextBox42.Click
        If TextBox42.Visible = False Then
            TextBox42.Visible = True
            btn_LogoDirectory.Visible = True
            TextBox42.BringToFront()
            btn_LogoDirectory.BringToFront()
        Else
            TextBox42.Visible = False
            btn_LogoDirectory.Visible = False
        End If
    End Sub

    Private Sub CheckBox5_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox5.CheckedChanged
        If CheckBox5.Checked = True Then Fussballuhr.ToolStripStatusLabel9.Text = "autostop clock ON"
        If CheckBox5.Checked = False Then Fussballuhr.ToolStripStatusLabel9.Text = "autostop clock OFF"
    End Sub

    Private Sub Label21_Click(sender As Object, e As EventArgs) Handles Label21.Click, Label22.Click, Label23.Click, Label24.Click
        If TextBox21.Enabled = False Then
            TextBox21.Enabled = True
            TextBox22.Enabled = True
            TextBox23.Enabled = True
            TextBox24.Enabled = True
        Else
            TextBox21.Enabled = False
            TextBox22.Enabled = False
            TextBox23.Enabled = False
            TextBox24.Enabled = False
        End If
    End Sub


End Class
