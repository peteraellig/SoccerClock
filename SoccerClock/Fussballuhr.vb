Public Class Fussballuhr

    'variables for vMix
    Public IP As String = "localhost"
    Public PORT As String = "8088"
    Public TcpPort As Integer = 8099
    Public sendstring As String = ""

    'vMix status and error variables
    Private connected As Boolean = 0
    ' Startet False (statt True) - genau wie Tennis26_Scorer.isVmixConnected. Der erste echte
    ' Check (CheckVmixConnection() in Form1_Load) muss VOR den ersten VTX()-Aufrufen laufen,
    ' sonst würde jeder der rund ein Dutzend Setup-Sends (Reset_scorebug/Reset_Clock/Set_Club/
    ' Set_Labels) versuchen zu senden, bevor überhaupt bekannt ist, ob vMix erreichbar ist.
    Private ConnectedState As Boolean = False

    Private Timer As New Timer
    ' Eigener, durchgehend laufender Timer für die vMix-Erreichbarkeitsprüfung - getrennt vom
    ' Spieluhr-Timer oben, der nur läuft, während die Uhr tatsächlich mitzählt (Start/Stop-
    ' Buttons). Analog zu Tennis26_Scorer.Timer1_Tick, das CheckVmixConnection() 1x/Sekunde
    ' durchgehend ab dem Laden aufruft: schlägt ein einzelner Check-Versuch (z.B. direkt nach
    ' dem Umschalten HTTP<->TCP) mal fehl, holt der nächste Tick das automatisch nach, statt
    ' dass ConnectedState dauerhaft False bleibt, bis jemand manuell auf den Status klickt.
    Private ReadOnly vmixConnectionTimer As New Timer
    Private TotalTime As TimeSpan = TimeSpan.Zero

    'variable for filename of the initial settings (ip, color, channel)
    Private Shared filename As String = "C:\vMix\soccerclock\soccerclock.xml"
    Private directory As String = "C:\vMix\soccerclock"
    Private directorytitles As String = "C:\vMix\soccerclock\titles\"
    Private directorylogos As String = "C:\vMix\soccerclock\logos\"

    Private overlayState As Boolean = False
    Private scorebugState As Boolean = False
    Private titleState As Boolean = False
    Private Pause1State As Boolean = False
    Private Pause2State As Boolean = False
    Private Pause3State As Boolean = False
    Private gameoverState As Boolean = False
    Private largeresultState As Boolean = False
    Private clockstate As Boolean = False
    Private clockrunning As Boolean = False

    Private scoreHome As Integer
    Private scoreAway As Integer
    Public duration_halftime As Integer
    Public duration_overtime As Integer

    Public Home_Color1 As String = "00000000" ' Default color is black transparent
    Public Home_Color2 As String = "00000000"
    Public Away_Color1 As String = "00000000"
    Public Away_Color2 As String = "00000000"

    ' Zeigt an, ob Sponsor1/Sponsor2 (btn_werbung1/btn_werbung2, gemeinsamer Werbung-Overlay-
    ' Kanal settings.ComboBox4) gerade sichtbar ist - analog zu Tennis26_Scorer's
    ' sponsor1ToggleStatus/sponsor2ToggleStatus.
    Private sponsor1ToggleStatus As Boolean = False
    Private sponsor2ToggleStatus As Boolean = False

    ' Live-JSON-Export (Settings-CheckBox8) - analog zu Tennis26_Scorer/TennisJsonExporter.vb,
    ' aber ohne eingebetteten Server: SoccerClock hat wenig Zustandsänderungen, ein simpler
    ' periodischer Datei-Schreibvorgang reicht (siehe LiveJsonExporter/JsonObjectBuilder in
    ' SoccerClockJsonExporter.vb). lastLiveJsonWriteAt+HEARTBEAT sorgen dafür, dass
    ' "updatedAt" auch ohne Zustandsänderung (z.B. lange Pause) regelmässig weiterläuft, damit
    ' ein externer Konsument eine eingefrorene/abgestürzte App von einer echten Pause
    ' unterscheiden kann.
    Private ReadOnly jsonExporter As New LiveJsonExporter()
    Private lastLiveJsonWriteAt As DateTime = DateTime.MinValue
    Private Const LIVE_JSON_HEARTBEAT_SECONDS As Integer = 5

    Private Sub Fussballuhr_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ' TCP-Verbindung zu vMix sauber trennen, falls die TCP-API gerade aktiv war.
        ' Analog zu Tennis26_Scorer_FormClosing.
        tcpVmixSender.Dispose()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer.Interval = 1000 ' setzt den Timer auf 1 Sekunde
        AddHandler Timer.Tick, AddressOf Timer_Tick
        Timer_message.Interval = 4000
        RadioButton1.Checked = True
        settings.LoadXML()

        ' Verbindungscheck VOR Reset_scorebug()/Reset_Clock()/Set_Club()/Set_Labels(), die
        ' zusammen schon rund ein Dutzend VTX()-Aufrufe auslösen (siehe ConnectedState-Default
        ' oben) - sonst versucht jeder einzelne davon einen echten, blockierenden Sendeversuch,
        ' bevor überhaupt bekannt ist, ob vMix erreichbar ist. Analog zu Tennis26_Scorer_Load.
        CheckVmixConnection()
        Connect_to_vMix()

        Reset_scorebug()
        Reset_Score()
        Reset_Clock()
        Set_Club()
        Set_duration()
        Set_Labels()
        Update_vMixColors()
        vmixConnectionTimer.Interval = 1000
        AddHandler vmixConnectionTimer.Tick, AddressOf VmixConnectionTimer_Tick
        vmixConnectionTimer.Start()
        Me.Text = My.Application.Info.Title & " " & My.Application.Info.Version.ToString & " | " & My.Application.Info.CompanyName + " - " & My.Application.Info.Copyright
    End Sub

    Public Sub Set_Labels()
        If settings.CheckBox1.Checked = False Then
            ToolStripStatusLabel3.Text = "overtime split"
        Else
            ToolStripStatusLabel3.Text = "NO overtime split"
        End If

        If settings.CheckBox2.Checked = True Then
            ToolStripStatusLabel7.Text = "PublicDisplay ON"
            Dim scorebugtitle As String = Getgtzip(settings.TextBox19.Text)
            Dim clocktextfield As String = Getgtzip(settings.TextBox9.Text)
            sendstring = BuildVmixSetCommand("SetText", scorebugtitle, clocktextfield, "00:00")
            VTX(sendstring)
        Else
            ToolStripStatusLabel7.Text = "PublicDisplay OFF"
            Dim scorebugtitle As String = Getgtzip(settings.TextBox19.Text)
            Dim clocktextfield As String = Getgtzip(settings.TextBox9.Text)
            sendstring = BuildVmixSetCommand("SetText", scorebugtitle, clocktextfield, "PublicDisplay OFF")
            VTX(sendstring)
        End If

        'If settings.CheckBox2.Checked = False Then
        '    ToolStripStatusLabel4.Text = "show overtime to play"
        'Else
        '    ToolStripStatusLabel4.Text = "NO overtime to play"
        'End If

        If settings.CheckBox5.Checked = True Then
            ToolStripStatusLabel9.Text = "autostop clock ON"
        Else
            ToolStripStatusLabel9.Text = "autostop clock OFF"
        End If

        ' Beschriftung der beiden Werbung-Buttons kommt aus den Settings (TextBox45/46) -
        ' die Buttons selbst blenden immer fix sponsor1.gtzip/sponsor2.gtzip ein, siehe
        ' btn_werbung1_Click/btn_werbung2_Click.
        btn_werbung1.Text = settings.TextBox45.Text
        btn_werbung2.Text = settings.TextBox46.Text
    End Sub


    Private Sub PictureBox_Color_Click(sender As Object, e As EventArgs) Handles homecolor1.Click, homecolor2.Click, awaycolor1.Click, awaycolor2.Click
        ' Display the ColorDialog to choose a color
        If ColorDialog1.ShowDialog() = DialogResult.OK Then
            ' Get the selected color
            Dim selectedColor As Color = ColorDialog1.Color
            Dim nametemplate As String = Getgtzip(settings.TextBox8.Text)
            Dim sendstring As String = ""

            ' Identify which PictureBox was clicked and set the corresponding color
            If sender Is homecolor1 Then
                homecolor1.BackColor = selectedColor
                Home_Color1 = selectedColor.ToArgb().ToString("X6").Substring(2)
                sendstring = BuildVmixSetCommand("SetColor", nametemplate, "home_color1.Fill.Color", "#" + Home_Color1)
            ElseIf sender Is homecolor2 Then
                homecolor2.BackColor = selectedColor
                Home_Color2 = selectedColor.ToArgb().ToString("X6").Substring(2)
                sendstring = BuildVmixSetCommand("SetColor", nametemplate, "home_color2.Fill.Color", "#" + Home_Color2)
            ElseIf sender Is awaycolor1 Then
                awaycolor1.BackColor = selectedColor
                Away_Color1 = selectedColor.ToArgb().ToString("X6").Substring(2)
                sendstring = BuildVmixSetCommand("SetColor", nametemplate, "away_color1.Fill.Color", "#" + Away_Color1)
            ElseIf sender Is awaycolor2 Then
                awaycolor2.BackColor = selectedColor
                Away_Color2 = selectedColor.ToArgb().ToString("X6").Substring(2)
                sendstring = BuildVmixSetCommand("SetColor", nametemplate, "away_color2.Fill.Color", "#" + Away_Color2)
            End If
            settings.SaveXML()

            ' Send the string to the VTX function
            If Not String.IsNullOrEmpty(sendstring) Then
                VTX(sendstring)
            End If
        Else
            ' No color was chosen, handle the error here
            MessageBox.Show("No color was chosen.", "Error22", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Public Sub Update_vMixColors()
        Dim nametemplate As String = Getgtzip(settings.TextBox8.Text)
        sendstring = BuildVmixSetCommand("SetColor", nametemplate, "home_color1.Fill.Color", "#" + Home_Color1)
        VTX(sendstring)
        sendstring = BuildVmixSetCommand("SetColor", nametemplate, "home_color2.Fill.Color", "#" + Home_Color2)
        VTX(sendstring)
        sendstring = BuildVmixSetCommand("SetColor", nametemplate, "away_color1.Fill.Color", "#" + Away_Color1)
        VTX(sendstring)
        sendstring = BuildVmixSetCommand("SetColor", nametemplate, "away_color2.Fill.Color", "#" + Away_Color2)
        VTX(sendstring)
    End Sub

    Public Sub RESET_btncolors()
        Dim nametemplate As String = Getgtzip(settings.TextBox8.Text)

        sendstring = BuildVmixSetCommand("SetColor", nametemplate, "home_color1.Fill.Color", "#FFFFFF00")
        VTX(sendstring)
        sendstring = BuildVmixSetCommand("SetColor", nametemplate, "home_color2.Fill.Color", "#FFFFFF00")
        VTX(sendstring)
        sendstring = BuildVmixSetCommand("SetColor", nametemplate, "away_color1.Fill.Color", "#FFFFFF00")
        VTX(sendstring)
        sendstring = BuildVmixSetCommand("SetColor", nametemplate, "away_color2.Fill.Color", "#FFFFFF00")
        VTX(sendstring)

        Home_Color1 = "00000000"
        Home_Color2 = "00000000"
        Away_Color1 = "00000000"
        Away_Color2 = "00000000"

        homecolor1.BackColor = Color.LightGray
        homecolor2.BackColor = Color.LightGray
        awaycolor1.BackColor = Color.LightGray
        awaycolor2.BackColor = Color.LightGray
        settings.SaveXML()
    End Sub

    Private Sub Timer_Tick(sender As Object, e As EventArgs)
        TotalTime = TotalTime.Add(TimeSpan.FromSeconds(1))
        UpdateTimeLabels()
    End Sub

    ' True, sobald das Overtime-Overlay wegen "no overtime split" ausgeblendet wurde - verhindert,
    ' dass UpdateTimeLabels() (1x/Sekunde über Timer_Tick) Overtime_Visible_OFF()/
    ' Overtime_to_play_Visible_OFF() bei jedem Tick erneut auslöst, obwohl sich am
    ' Sichtbarkeitsstatus nichts ändert - das sendet sonst 4 unnötige vMix-Befehle pro Sekunde.
    Private noOvertimeSplitOverlayHidden As Boolean = False

    Public Sub UpdateTimeLabels()
        'formats time, either as normal or with overtime split
        If settings.CheckBox1.Checked Then
            ' NICHT CType(TotalTime.TotalMinutes, Integer) - das rundet (Banker's Rounding)
            ' statt abzuschneiden, wodurch die angezeigte Minute schon ab Sekunde 30 auf die
            ' nächste Minute umspringt (und bei exakten .5-Werten je nach gerade/ungerade
            ' sogar zurückspringen kann). TotalTime.Minutes + TotalTime.Hours * 60 sind
            ' ganzzahlige TimeSpan-Komponenten ohne Rundungsfehler - analog zum Else-Zweig
            ' unten (times.DisplayTime.Minutes + times.DisplayTime.Hours * 60).
            Label_time.Text = String.Format("{0:D2}:{1:D2}", TotalTime.Minutes + TotalTime.Hours * 60, TotalTime.Seconds)
            Label_overtime.Font = New Font("Arial", 10, FontStyle.Bold)
            Label_overtime.Text = "no overtime split"
            If Not noOvertimeSplitOverlayHidden Then
                Overtime_Visible_OFF()
                Overtime_to_play_Visible_OFF()
                noOvertimeSplitOverlayHidden = True
            End If
        Else
            noOvertimeSplitOverlayHidden = False
            Dim times = GetDisplayTimeAndOvertime()
            Label_time.Text = String.Format("{0:D2}:{1:D2}", times.DisplayTime.Minutes + times.DisplayTime.Hours * 60, times.DisplayTime.Seconds)
            Label_overtime.Text = String.Format("{0:D2}:{1:D2}", times.Overtime.Minutes + times.Overtime.Hours * 60, times.Overtime.Seconds)
            Label_overtime.Font = New Font("Arial", 12, FontStyle.Regular)
        End If
    End Sub

    Private Function GetDisplayTimeAndOvertime() As (DisplayTime As TimeSpan, Overtime As TimeSpan)
        Dim thresholdTime As TimeSpan
        Set_duration()
        If RadioButton1.Checked Then
            thresholdTime = TimeSpan.FromMinutes(duration_halftime)
        ElseIf RadioButton2.Checked Then
            thresholdTime = TimeSpan.FromMinutes(2 * duration_halftime)
        ElseIf RadioButton3.Checked Then
            thresholdTime = TimeSpan.FromMinutes((2 * duration_halftime) + duration_overtime)
        ElseIf RadioButton4.Checked Then
            thresholdTime = TimeSpan.FromMinutes((2 * duration_halftime) + (2 * duration_overtime))
        End If
        If TotalTime > thresholdTime Then
            Return (thresholdTime, TotalTime - thresholdTime)
        Else
            Return (TotalTime, TimeSpan.Zero)
        End If
    End Function

    Private Sub Button_clockstart_Click(sender As Object, e As EventArgs) Handles Button_clockstart.Click
        Timer.Start()
        RadioButton1.Enabled = True
        Clock_start()
    End Sub
    Private Sub Clock_start()
        Button_clockstart.BackColor = SystemColors.Control
        Button_clockstart.Text = "Clock running"
        Button_clockstop.BackColor = Color.Red
        clockrunning = True
        WriteLiveJsonFile()
    End Sub
    Private Sub Clock_stop()
        Button_clockstart.BackColor = Color.Green
        Button_clockstart.Text = "Clock start"
        Button_clockstop.BackColor = SystemColors.Control
        TextBox_overtime.Text = ""
        clockrunning = False
        ' Blendet beim Stoppen der Uhr NUR den Scorebug aus, nicht mehr den Titel-Kanal
        ' (auf Peters Wunsch) - eine offene Pause-/Titelkarte soll stehen bleiben, bis sie
        ' über ihren eigenen Button manuell wieder ausgeblendet wird.
        sendstring = "Function=OverlayInput" + settings.ComboBox2.Text + "Out"
        VTX(sendstring)
        Reset_Buttons()
        Reset_State()
        WriteLiveJsonFile()
    End Sub

    Private Sub Button_clockstop_Click(sender As Object, e As EventArgs) Handles Button_clockstop.Click
        Clock_stop()
        Threading.Thread.Sleep(500)
        Overtime_Visible_OFF()
        Overtime_to_play_Visible_OFF()
        Set_duration()
        Timer.Stop()
        If RadioButton1.Checked Then
            RadioButton2.Checked = True
            TotalTime = TimeSpan.FromMinutes(duration_halftime)
        ElseIf RadioButton2.Checked Then
            RadioButton3.Checked = True
            TotalTime = TimeSpan.FromMinutes(2 * duration_halftime)
        ElseIf RadioButton3.Checked Then
            RadioButton4.Checked = True
            TotalTime = TimeSpan.FromMinutes((2 * duration_halftime) + duration_overtime)
        End If
        UpdateTimeLabels()
        ' LabelOverTimesmall.Visible = False

    End Sub

    Private Sub Label1_DoubleClick(sender As Object, e As EventArgs) Handles Label_time.DoubleClick
        Reset_Clock()
        Reset_scorebug()
    End Sub

    Private Sub btn_reset_clock_Click(sender As Object, e As EventArgs) Handles btn_reset_clock.Click
        Reset_Clock()
        Reset_scorebug()
    End Sub

    Private Sub Reset_scorebug()
        Overtime_Visible_OFF()
        Overtime_to_play_Visible_OFF()
    End Sub

    Private Sub btn_reset_score_Click(sender As Object, e As EventArgs) Handles btn_reset_score.Click
        Reset_Score()
    End Sub
    Private Sub Reset_Score()
        scoreHome = 0
        lbl_TorHome.Text = scoreHome
        scoreAway = 0
        lbl_TorAway.Text = scoreAway
    End Sub
    Private Sub Reset_Clock()
        Timer.Stop()
        TotalTime = TimeSpan.Zero
        RadioButton1.Checked = True
        Label_time.Text = "00:00"
        Clock_stop()
        UpdateTimeLabels()
        Dim scorebugtitle As String = Getgtzip(settings.TextBox8.Text)
        Dim clocktextfield As String = Getgtzip(settings.TextBox9.Text)
        TextBox_overtime.Text = ""
        sendstring = BuildVmixSetCommand("SetText", scorebugtitle, clocktextfield, "00:00")
        VTX(sendstring)
        Clock_ON()
        Overtime_to_play_Visible_OFF()
        Overtime_Visible_OFF()
    End Sub
    Public Sub Set_duration()
        duration_halftime = CInt(settings.TextBox1.Text)
        duration_overtime = CInt(settings.TextBox2.Text)
        ToolStripStatusLabel1.Text = "ht=" + duration_halftime.ToString + "min"
        ToolStripStatusLabel2.Text = "ot=" + duration_overtime.ToString + "min"
    End Sub

    Public Sub Set_Club()
        NameHome.Text = settings.TextBox21.Text
        NameAway.Text = settings.TextBox22.Text
        Dim scorebugtitle As String = Getgtzip(settings.TextBox8.Text)
        Dim title As String = Getgtzip(settings.TextBox32.Text)
        Dim public_displaytitle As String = Getgtzip(settings.TextBox19.Text)
        Dim homeclubfield As String = Getgtzip(settings.TextBox14.Text)
        Dim awayclubfield As String = Getgtzip(settings.TextBox15.Text)
        Dim homeclubfieldlong As String = Getgtzip(settings.TextBox26.Text)
        Dim awayclubfieldlong As String = Getgtzip(settings.TextBox27.Text)
        Dim text2 As String = Getgtzip(settings.TextBox35.Text)
        sendstring = BuildVmixSetCommand("SetText", scorebugtitle, homeclubfield, settings.TextBox23.Text)
        VTX(sendstring)
        sendstring = BuildVmixSetCommand("SetText", scorebugtitle, awayclubfield, settings.TextBox24.Text)
        VTX(sendstring)
        sendstring = BuildVmixSetCommand("SetText", scorebugtitle, homeclubfieldlong, settings.TextBox21.Text)
        VTX(sendstring)
        sendstring = BuildVmixSetCommand("SetText", scorebugtitle, awayclubfieldlong, settings.TextBox22.Text)
        VTX(sendstring)
        sendstring = BuildVmixSetCommand("SetText", public_displaytitle, text2, settings.TextBox39.Text)
        VTX(sendstring)
        Draw_Logos(public_displaytitle)
        Draw_Logos(title)
    End Sub

    Private Sub Overtime_Visible_ON()
        Dim scorebugtitle As String = Getgtzip(settings.TextBox8.Text)
        Dim overlayText As String = settings.TextBox10.Text
        Dim overlayimage As String = settings.TextBox11.Text
        Dim overlayToPlayText As String = settings.TextBox12.Text
        Dim overlayToPlayimage As String = settings.TextBox13.Text
        sendstring = "Function=SetTextVisibleOn&Input=" + scorebugtitle + "&SelectedName=" + overlayText
        VTX(sendstring)
        sendstring = "Function=SetImageVisibleOn&Input=" + scorebugtitle + "&SelectedName=" + overlayimage
        VTX(sendstring)
    End Sub

    Private Sub Overtime_to_play_Visible_ON()
        Dim scorebugtitle As String = Getgtzip(settings.TextBox8.Text)
        Dim overlayText As String = settings.TextBox10.Text
        Dim overlayimage As String = settings.TextBox11.Text
        Dim overlayToPlayText As String = settings.TextBox12.Text
        Dim overlayToPlayimage As String = settings.TextBox13.Text
        sendstring = "Function=SetTextVisibleOn&Input=" + scorebugtitle + "&SelectedName=" + overlayToPlayText
        VTX(sendstring)
        sendstring = "Function=SetImageVisibleOn&Input=" + scorebugtitle + "&SelectedName=" + overlayToPlayimage
        VTX(sendstring)
    End Sub

    Private Sub Overtime_Visible_OFF()
        Dim scorebugtitle As String = Getgtzip(settings.TextBox8.Text)
        Dim overlayText As String = settings.TextBox10.Text
        Dim overlayimage As String = settings.TextBox11.Text
        Dim overlayToPlayText As String = settings.TextBox12.Text
        Dim overlayToPlayimage As String = settings.TextBox13.Text
        sendstring = "Function=SetTextVisibleOff&Input=" + scorebugtitle + "&SelectedName=" + overlayText
        VTX(sendstring)
        sendstring = "Function=SetImageVisibleOff&Input=" + scorebugtitle + "&SelectedName=" + overlayimage
        VTX(sendstring)
    End Sub

    Private Sub Overtime_to_play_Visible_OFF()
        Dim scorebugtitle As String = Getgtzip(settings.TextBox8.Text)
        Dim overlayText As String = settings.TextBox10.Text
        Dim overlayimage As String = settings.TextBox11.Text
        Dim overlayToPlayText As String = settings.TextBox12.Text
        Dim overlayToPlayimage As String = settings.TextBox13.Text
        sendstring = "Function=SetTextVisibleOff&Input=" + scorebugtitle + "&SelectedName=" + overlayToPlayText
        VTX(sendstring)
        sendstring = "Function=SetImageVisibleOff&Input=" + scorebugtitle + "&SelectedName=" + overlayToPlayimage
        VTX(sendstring)
    End Sub
    Private Sub Clock_OFF()
        Dim scorebugtitle As String = Getgtzip(settings.TextBox8.Text)
        Dim public_displaytitle As String = Getgtzip(settings.TextBox19.Text)

        sendstring = "Function=SetTextVisibleOff&Input=" + scorebugtitle + "&SelectedName=" + settings.TextBox9.Text.Trim
        VTX(sendstring)
        sendstring = "Function=SetImageVisibleOff&Input=" + scorebugtitle + "&SelectedName=" + settings.TextBox20.Text
        VTX(sendstring)
        sendstring = "Function=SetImageVisibleOff&Input=" + public_displaytitle + "&SelectedName=" + settings.TextBox9.Text
        VTX(sendstring)
        Overtime_to_play_Visible_OFF()
        Overtime_Visible_OFF()
    End Sub

    Private Sub Clock_ON()
        Dim scorebugtitle As String = Getgtzip(settings.TextBox8.Text)
        Dim public_displaytitle As String = Getgtzip(settings.TextBox19.Text)

        sendstring = "Function=SetTextVisibleOn&Input=" + scorebugtitle + "&SelectedName=" + settings.TextBox9.Text
        VTX(sendstring)
        sendstring = "Function=SetImageVisibleOn&Input=" + scorebugtitle + "&SelectedName=" + settings.TextBox20.Text
        VTX(sendstring)
        sendstring = "Function=SetImageVisibleOn&Input=" + public_displaytitle + "&SelectedName=" + settings.TextBox9.Text
        VTX(sendstring)
    End Sub

    Private Sub RadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged, RadioButton2.CheckedChanged, RadioButton3.CheckedChanged, RadioButton4.CheckedChanged
        Timer.Stop()
        If RadioButton1.Checked Then
            TotalTime = TimeSpan.FromMinutes(0)
        ElseIf RadioButton2.Checked Then
            TotalTime = TimeSpan.FromMinutes(duration_halftime)
        ElseIf RadioButton3.Checked Then
            TotalTime = TimeSpan.FromMinutes(2 * duration_halftime)
        ElseIf RadioButton4.Checked Then
            TotalTime = TimeSpan.FromMinutes((2 * duration_halftime) + duration_overtime)
        End If
        UpdateTimeLabels()
    End Sub

    Private Sub Button_clockminusmin_Click(sender As Object, e As EventArgs) Handles Button_clockminusmin.Click
        TotalTime = TotalTime.Subtract(TimeSpan.FromMinutes(1))
        If TotalTime.TotalSeconds < 0 Then TotalTime = TimeSpan.Zero
        UpdateTimeLabels()
    End Sub

    Private Sub Button_clockplusmin_Click(sender As Object, e As EventArgs) Handles Button_clockplusmin.Click
        TotalTime = TotalTime.Add(TimeSpan.FromMinutes(1))
        UpdateTimeLabels()
    End Sub

    Private Sub Button_clockplussec_Click(sender As Object, e As EventArgs) Handles Button_clockplussec.Click
        TotalTime = TotalTime.Add(TimeSpan.FromSeconds(1))
        UpdateTimeLabels()
    End Sub

    Private Sub Button_clockminussec_Click(sender As Object, e As EventArgs) Handles Button_clockminussec.Click
        TotalTime = TotalTime.Subtract(TimeSpan.FromSeconds(1))
        If TotalTime.TotalSeconds < 0 Then TotalTime = TimeSpan.Zero
        UpdateTimeLabels()
    End Sub

    Private Sub Label_time_TextChanged(sender As Object, e As EventArgs) Handles Label_time.TextChanged
        Dim scorebugtitle As String = Getgtzip(settings.TextBox8.Text)
        Dim clocktextfield As String = Getgtzip(settings.TextBox9.Text)
        sendstring = BuildVmixSetCommand("SetText", scorebugtitle, clocktextfield, Label_time.Text)
        VTX(sendstring)
    End Sub

    Private Sub Label_overtime_TextChanged(sender As Object, e As EventArgs) Handles Label_overtime.TextChanged
        Dim scorebugtitle As String = Getgtzip(settings.TextBox8.Text)
        Dim overtimetextfield As String = Getgtzip(settings.TextBox10.Text)
        If clockstate = False Then
            If Label_overtime.Text <> "00:00" Then Overtime_Visible_ON()
            sendstring = BuildVmixSetCommand("SetText", scorebugtitle, overtimetextfield, Label_overtime.Text)
            VTX(sendstring)
        End If
    End Sub

    Private Sub btn_setup_Click(sender As Object, e As EventArgs) Handles btn_setup.Click
        settings.Show()
        Me.Hide()
    End Sub

    Private Sub btn_exit_Click(sender As Object, e As EventArgs) Handles btn_exit.Click
        Try
            If MessageBox.Show("Do you really want to end the program?", "vMix soccerclock", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                Application.Exit()
            End If
        Catch ex As Exception
            MessageBox.Show($"Error exiting application: {ex.Message}", "Error10", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ToolStripStatusLabel5_TextChanged(sender As Object, e As EventArgs) Handles ToolStripStatusLabel5.TextChanged
        Timer_message.Start()
    End Sub

    Private Sub Timer_message_Tick(sender As Object, e As EventArgs) Handles Timer_message.Tick
        'clears error messages in status bar after 4 seconds (Interval wird in Form1_Load
        'gesetzt - vorher stand hier keine Designer-Vorgabe, d.h. die allererste Meldung
        'verschwand schon nach dem WinForms-Default von 100ms statt nach 4 Sekunden)
        ToolStripStatusLabel5.Text = ""
        Timer_message.Stop()
    End Sub

    Private Sub btn_scorebug_Click(sender As Object, e As EventArgs) Handles btn_scorebug.Click
        'homeTeamSets
        'AwayTeamSets
        Clock_ON()
        clockstate = False
        btn_uhrweg.BackColor = SystemColors.ControlDark
        Dim scorebugtitle As String = Getgtzip(settings.TextBox8.Text)
        Reset_Buttons()
        If scorebugState = False Then
            sendstring = "Function=OverlayInput" + settings.ComboBox2.Text + "In&Input=" + scorebugtitle
            VTX(sendstring)
            scorebugState = True
            btn_scorebug.BackColor = Color.Red
        Else
            sendstring = "Function=OverlayInput" + settings.ComboBox2.Text + "Out"
            VTX(sendstring)
            Reset_Buttons()
            Reset_State()
        End If
        WriteLiveJsonFile()
    End Sub

    ' Blendet sponsor1.gtzip/sponsor2.gtzip auf dem Werbung-Overlay-Kanal (ComboBox4) ein
    ' bzw. aus - analog zu Tennis26_Scorer.Btn_sponsor1_Click/Btn_sponsor2_Click. Beide
    ' Buttons teilen sich einen Kanal, daher schaltet das Einblenden des einen den anderen
    ' automatisch "aus" (vMix ersetzt den Inhalt des Kanals bei OverlayInputXIn), die beiden
    ' ToggleStatus-Felder halten nur den UI-Zustand (Button-Farbe) synchron dazu.
    Private Sub btn_werbung1_Click(sender As Object, e As EventArgs) Handles btn_werbung1.Click
        Dim nametemplate As String = "sponsor1.gtzip"
        sponsor1ToggleStatus = Not sponsor1ToggleStatus

        If sponsor1ToggleStatus Then
            sendstring = "Function=OverlayInput" + settings.ComboBox4.Text + "In&Input=" + nametemplate + "&Mix=0"
            btn_werbung1.BackColor = Color.Red
            btn_werbung2.BackColor = SystemColors.ControlDark
            sponsor2ToggleStatus = False
        Else
            sendstring = "Function=OverlayInput" + settings.ComboBox4.Text + "Out&Input=" + nametemplate + "&Mix=0"
            btn_werbung1.BackColor = SystemColors.ControlDark
            sponsor2ToggleStatus = False
        End If

        VTX(sendstring)
        WriteLiveJsonFile()
    End Sub

    Private Sub btn_werbung2_Click(sender As Object, e As EventArgs) Handles btn_werbung2.Click
        Dim nametemplate As String = "sponsor2.gtzip"
        sponsor2ToggleStatus = Not sponsor2ToggleStatus

        If sponsor2ToggleStatus Then
            sendstring = "Function=OverlayInput" + settings.ComboBox4.Text + "In&Input=" + nametemplate + "&Mix=0"
            btn_werbung2.BackColor = Color.Red
            btn_werbung1.BackColor = SystemColors.ControlDark
            sponsor1ToggleStatus = False
        Else
            sendstring = "Function=OverlayInput" + settings.ComboBox4.Text + "Out&Input=" + nametemplate + "&Mix=0"
            btn_werbung2.BackColor = SystemColors.ControlDark
            sponsor1ToggleStatus = False
        End If

        VTX(sendstring)
        WriteLiveJsonFile()
    End Sub

    Private Shared Function Getgtzip(filePath As String) As String
        ' Find the last backslash in the path
        Dim lastBackslashIndex As Integer = filePath.LastIndexOf("\")
        ' Return the substring that starts right after the last backslash
        Return filePath.Substring(lastBackslashIndex + 1)
    End Function

    Private Sub Btn_TorPlus_Home_Click(sender As Object, e As EventArgs) Handles Btn_TorPlus_Home.Click
        scoreHome += 1
        lbl_TorHome.Text = scoreHome
    End Sub

    Private Sub Btn_TorMinus_Home_Click(sender As Object, e As EventArgs) Handles Btn_TorMinus_Home.Click
        If scoreHome > 0 Then scoreHome -= 1
        lbl_TorHome.Text = scoreHome
    End Sub

    Private Sub Btn_TorPlus_Away_Click(sender As Object, e As EventArgs) Handles Btn_TorPlus_Away.Click
        scoreAway += 1
        lbl_TorAway.Text = scoreAway
    End Sub

    Private Sub Btn_TorMinus_Away_Click(sender As Object, e As EventArgs) Handles Btn_TorMinus_Away.Click
        If scoreAway > 0 Then scoreAway -= 1
        lbl_TorAway.Text = scoreAway
    End Sub

    Private Sub lbl_TorHome_TextChanged(sender As Object, e As EventArgs) Handles lbl_TorHome.TextChanged, lbl_TorAway.TextChanged
        Update_result()
    End Sub

    Public Sub Update_result()
        Dim scorebugtitle As String = Getgtzip(settings.TextBox8.Text)
        Dim homescorefield As String = Getgtzip(settings.TextBox16.Text)
        Dim Awayscorefield As String = Getgtzip(settings.TextBox17.Text)
        Dim divider As String = Getgtzip(settings.TextBox18.Text)

        sendstring = BuildVmixSetCommand("SetText", scorebugtitle, homescorefield, lbl_TorHome.Text)
        VTX(sendstring)
        sendstring = BuildVmixSetCommand("SetText", scorebugtitle, Awayscorefield, lbl_TorAway.Text)
        VTX(sendstring)
        sendstring = BuildVmixSetCommand("SetText", scorebugtitle, divider, settings.TextBox7.Text)
        VTX(sendstring)
        lbl_divider.Text = settings.TextBox7.Text
        WriteLiveJsonFile()
    End Sub

    Private Sub btn_reset_all_Click(sender As Object, e As EventArgs) Handles btn_reset_all.Click
        Reset_scorebug()
        Reset_Score()
        Reset_Clock()
        Set_Club()
        Clock_ON()
        clockstate = False
        btn_uhrweg.BackColor = SystemColors.ControlDark
    End Sub

    Private Sub TextBox_overtime_TextChanged(sender As Object, e As EventArgs) Handles TextBox_overtime.TextChanged
        Dim scorebugtitle As String = Getgtzip(settings.TextBox8.Text)
        Dim overlayToPlayText As String = settings.TextBox12.Text
        Dim overlayToPlayimage As String = settings.TextBox13.Text
        Dim overtime As String
        If String.IsNullOrEmpty(TextBox_overtime.Text.Trim) Then
            overtime = String.Empty
            Overtime_to_play_Visible_OFF()
        Else
            Overtime_to_play_Visible_ON()
            ' Wenn die TextBox nicht leer ist, versuche den Inhalt in eine Zahl umzuwandeln.
            ' Rohes "+" statt vorkodiertem "%2B" - BuildVmixSetCommand() kodiert den Value
            ' jetzt selbst (EncodeVmixValue), ein bereits kodiertes "%2B" hier würde sonst
            ' zu "%252B" doppelkodiert und als Literal "%2B3" statt "+3" angezeigt.
            overtime = "+" + TextBox_overtime.Text.Trim
        End If
        sendstring = BuildVmixSetCommand("SetText", scorebugtitle, overlayToPlayText, overtime)
        VTX(sendstring)
    End Sub

    Private Sub TextBox_overtime_DoubleClick(sender As Object, e As EventArgs) Handles TextBox_overtime.DoubleClick
        TextBox_overtime.Text = ""
    End Sub

    Private Sub textbox_overtime_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox_overtime.KeyPress
        ' Überprüfe, ob das eingegebene Zeichen eine Zahl, die Backspace-Taste oder die Delete-Taste ist
        If Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> ChrW(Keys.Back) AndAlso e.KeyChar <> ChrW(Keys.Delete) Then
            ' Wenn nicht, unterdrücke die Eingabe
            e.Handled = True
            ToolStripStatusLabel5.Text = "only digits allowed"
        End If
    End Sub

    Private Sub ListBox5_Click(sender As Object, e As EventArgs) Handles ListBox5.Click
        ListBox5.Items.Clear()
    End Sub

    Private Sub ToolStripStatusLabel4_Click(sender As Object, e As EventArgs) Handles ToolStripStatusLabel4.Click, ListBox5.Click
        If ListBox5.Visible = False Then
            ListBox5.Visible = True
            ListBox5.Items.Clear()
        Else
            ListBox5.Visible = False
        End If
    End Sub

    Public Sub VTX(HTML_URL As String)
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        Dim original As String = Getgtzip(settings.TextBox8.Text)
        Dim replacement As String = Getgtzip(settings.TextBox19.Text)

        If ConnectedState = True Then
            ' Erste URL ohne Änderung senden
            SendHTMLRequest(HTML_URL)

            ' Überprüfen, ob die Checkbox2 aktiviert ist
            If settings.CheckBox2.Checked Then
                ' Überprüfen, ob die URL den Text "Function=OverlayInput" enthält
                If Not HTML_URL.Contains("Function=OverlayInput") Then
                    ' Den Teil des Strings ersetzen
                    Dim modifiedHTML_URL As String = HTML_URL.Replace(original, replacement)
                    ' neue URL mit geändertem String senden
                    SendHTMLRequest(modifiedHTML_URL)
                Else
                    ' Optional: Nachricht hinzufügen, dass die URL ausgeschlossen wurde
                    ListBox5.Items.Add("Function=OverlayInput was excluded.")
                End If
            End If
        End If

        stopwatch.Stop()
        Dim elapsedTime As Double = stopwatch.Elapsed.TotalMilliseconds
        ListBox5.Items.Add($"command took {elapsedTime:F3} ms")
    End Sub


    ' Baut einen "SetText"/"SetImage"/"SetColor"-vMix-Befehl und kodiert den Wert dabei
    ' konsequent URL-sicher. Vorher wurden Teamnamen/Freitexte meist unkodiert eingefügt,
    ' sodass "&" oder "#" die vMix-Request verfälschen bzw. abschneiden konnten.
    '
    ' WICHTIG: Uri.EscapeDataString statt WebUtility.UrlEncode - letzteres kodiert
    ' Leerzeichen als "+" (alte Formular-Kodierung), was in einer vMix-URL als literales
    ' Plus-Zeichen ankommt ("FC St.+Gallen"). Uri.EscapeDataString erzeugt "%20". Analog zu
    ' Tennis26_Scorer.EncodeVmixValue/BuildVmixSetCommand.
    Public Function EncodeVmixValue(value As String) As String
        Return Uri.EscapeDataString(If(value, ""))
    End Function

    Public Function BuildVmixSetCommand(func As String, input As String, selectedName As String, value As String) As String
        Return "Function=" + func + "&Input=" + input + "&SelectedName=" + selectedName + "&Value=" + EncodeVmixValue(value)
    End Function

    ' Wahl zwischen HTTP- und TCP-API (Settings-CheckBox6) wird bei jedem Aufruf frisch
    ' gelesen statt einmalig gecacht - so wirkt eine Settings-Änderung sofort, ohne das
    ' Programm neu zu starten. Die eigentliche Übersetzung des "Function=X&Param=Y&..."-
    ' Strings ins jeweilige Protokoll steckt in VmixHttpSender/VmixTcpSender (siehe IVmixSender).
    Private ReadOnly httpVmixSender As New VmixHttpSender()
    Private ReadOnly tcpVmixSender As New VmixTcpSender()

    Public Sub SendHTMLRequest(ByVal HTML_URL As String)
        If ConnectedState = True Then
            Dim useHttp As Boolean = settings.CheckBox6.Checked
            Dim sender As IVmixSender = If(useHttp, CType(httpVmixSender, IVmixSender), tcpVmixSender)

            Dim result As String = sender.Send(HTML_URL)
            Label2.Text = sender.LastCommand

            If result.StartsWith("Exception Error in VTX") Then
                ConnectedState = False
                ToolStripStatusLabel5.Text = result
            Else
                ToolStripStatusLabel5.Text = If(useHttp, "HTTP command sent", "TCP command sent")
            End If
        End If
    End Sub

    Private Sub btn_nocolor_home_Click(sender As Object, e As EventArgs) Handles btn_nocolor_home.Click
        RESET_btncolors()
    End Sub

    ' Prüft die vMix-Erreichbarkeit über das aktuell gewählte Protokoll (Settings-CheckBox6)
    ' statt hart über einen TCP-Verbindungsversuch. Vorher blieb ConnectedState - und damit
    ' das Senden über SendHTMLRequest/VTX - auch im HTTP-Modus von der TCP-Erreichbarkeit
    ' abhängig: lief vMix' TCP-API nicht, wurden nie Befehle gesendet, selbst wenn HTTP
    ' funktioniert hätte. Analog zu Tennis26_Scorer.CheckVmixConnection: sender.Send("")
    ' und Prüfung auf den "Exception Error in VTX"-Präfix.
    Sub CheckVmixConnection()
        Dim useHttp As Boolean = settings.CheckBox6.Checked
        Dim sender As IVmixSender = If(useHttp, CType(httpVmixSender, IVmixSender), tcpVmixSender)
        Dim result As String = sender.Send("")
        ConnectedState = Not result.StartsWith("Exception Error in VTX")

        ' Dieselbe Status-Anzeige wie Tennis26_Scorer.CheckVmixConnection (Label14 dort) -
        ' ToolStripStatusLabel8 war zuvor nur die "ol1/ol2/ol3/ol4"-Anzeige der alten,
        ' inzwischen entfernten Overlay-RadioButtons und stand seither frei.
        Dim protocolLabel As String = If(useHttp, "HTTP", "TCP")
        ToolStripStatusLabel8.Text = If(ConnectedState, $"vMix found - {protocolLabel} connected", "vMix not found - please start vMix")
        ToolStripStatusLabel8.ForeColor = If(ConnectedState, Color.Green, Color.Red)
    End Sub

    ' 1x/Sekunde durchgehender Re-Check (siehe vmixConnectionTimer oben) - hält
    ' ConnectedState/ToolStripStatusLabel6 aktuell, ohne dass jemand manuell auf den Status
    ' klicken oder die Settings öffnen muss, analog zu Tennis26_Scorer.Timer1_Tick.
    ' notifyOnDisconnect:=False, damit die MsgBox in Connect_to_vMix() nicht jede Sekunde
    ' erneut aufpoppt, solange vMix nicht erreichbar ist.
    Private Sub VmixConnectionTimer_Tick(sender As Object, e As EventArgs)
        CheckVmixConnection()
        Connect_to_vMix(notifyOnDisconnect:=False)

        ' Live-JSON-Heartbeat (siehe LIVE_JSON_HEARTBEAT_SECONDS oben) - hält "updatedAt"
        ' auch ohne Zustandsänderung aktuell. WriteLiveJsonFile() prüft selbst, ob CheckBox8
        ' aktiv ist.
        If DateTime.UtcNow.Subtract(lastLiveJsonWriteAt).TotalSeconds >= LIVE_JSON_HEARTBEAT_SECONDS Then
            WriteLiveJsonFile()
        End If
    End Sub

    ' Baut den aktuellen Spielstand als JSON - unabhängig von vMix, für beliebige externe
    ' Software geeignet, die die Live-JSON-Datei ausliest. Analog zu
    ' Tennis26_Scorer.BuildLiveStateJson.
    Private Function BuildLiveStateJson() As String
        Dim homeObj As New JsonObjectBuilder()
        homeObj.AddString("name", settings.TextBox21.Text) _
               .AddString("shortName", settings.TextBox23.Text) _
               .AddInt("score", scoreHome) _
               .AddString("color1", "#" & Home_Color1) _
               .AddString("color2", "#" & Home_Color2)

        Dim awayObj As New JsonObjectBuilder()
        awayObj.AddString("name", settings.TextBox22.Text) _
               .AddString("shortName", settings.TextBox24.Text) _
               .AddInt("score", scoreAway) _
               .AddString("color1", "#" & Away_Color1) _
               .AddString("color2", "#" & Away_Color2)

        ' Welche Halbzeit/Verlängerung gerade läuft - dieselben Schwellen wie
        ' GetDisplayTimeAndOvertime()/Button_clockstop_Click.
        Dim half As String = "1st half"
        If RadioButton2.Checked Then half = "2nd half"
        If RadioButton3.Checked Then half = "1st overtime"
        If RadioButton4.Checked Then half = "2nd overtime"

        Dim root As New JsonObjectBuilder()
        root.AddRaw("home", homeObj.ToString()) _
            .AddRaw("away", awayObj.ToString()) _
            .AddString("clockTime", Label_time.Text) _
            .AddInt("clockSeconds", CInt(TotalTime.TotalSeconds)) _
            .AddBool("clockRunning", clockrunning) _
            .AddString("half", half) _
            .AddString("overtimeText", Label_overtime.Text) _
            .AddBool("scorebugVisible", scorebugState) _
            .AddBool("titleVisible", titleState) _
            .AddBool("pause1Visible", Pause1State) _
            .AddBool("pause2Visible", Pause2State) _
            .AddBool("pause3Visible", Pause3State) _
            .AddBool("gameOverVisible", gameoverState) _
            .AddBool("sponsor1Visible", sponsor1ToggleStatus) _
            .AddBool("sponsor2Visible", sponsor2ToggleStatus) _
            .AddString("updatedAt", DateTime.UtcNow.ToString("o")) _
            .AddInt("heartbeatIntervalSeconds", LIVE_JSON_HEARTBEAT_SECONDS)

        ' vMix erwartet bei einer JSON Data Source zwingend ein Array von Objekten (auch bei
        ' nur einer "Zeile") - siehe Tennis26_Scorer.BuildLiveStateJson für die Begründung.
        Return "[" & root.ToString() & "]"
    End Function

    ' Zentraler Schreibpunkt für die Live-JSON-Datei - vom Heartbeat (VmixConnectionTimer_Tick)
    ' UND von jeder Zustandsänderung aufgerufen (Score, Uhr, Overlays), damit die Datei nicht
    ' bis zu 5 Sekunden hinter dem tatsächlichen Zustand hinterherhinkt.
    Private Sub WriteLiveJsonFile()
        If Not settings.CheckBox8.Checked Then Return

        If jsonExporter.WriteToFile(settings.TextBox47.Text, BuildLiveStateJson()) Then
            lastLiveJsonWriteAt = DateTime.UtcNow
            ToolStripStatusLabel10.Text = "JSON written"
            ToolStripStatusLabel10.ForeColor = Color.Green
        Else
            ToolStripStatusLabel10.Text = "JSON write failed: " & jsonExporter.LastError
            ToolStripStatusLabel10.ForeColor = Color.Red
        End If
    End Sub

    Sub Connect_to_vMix(Optional notifyOnDisconnect As Boolean = True)
        If settings.CheckBox3.Checked = True Then
            ToolStripStatusLabel6.Visible = True
            If ConnectedState = True Then
                ToolStripStatusLabel6.Text = "vMix connected"
                ToolStripStatusLabel6.BackColor = Color.LightGreen
                ToolStripStatusLabel6.ForeColor = Color.Black
                connected = 1
            Else
                If notifyOnDisconnect Then
                    MsgBox("start vMix. When you start vMix after this program, don't forget to press RESET ALL to reset the displays in vMix")
                End If
                ToolStripStatusLabel6.Text = "click here to try to connect"
                ToolStripStatusLabel6.BackColor = Color.Red
                ToolStripStatusLabel6.ForeColor = Color.White
            End If
        Else
            ToolStripStatusLabel6.Visible = False
        End If
    End Sub

    Private Sub ToolStripStatusLabel6_Click(sender As Object, e As EventArgs) Handles ToolStripStatusLabel6.Click
        If settings.CheckBox3.Checked = True Then
            CheckVmixConnection()
            Connect_to_vMix()
        End If
    End Sub

    Private Sub btn_titel_Click(sender As Object, e As EventArgs) Handles btn_titel.Click
        If clockrunning = True Then
            MsgBox("Stop Clock First")
            Exit Sub
        End If
        Dim template As String = Getgtzip(settings.TextBox32.Text)
        Reset_Buttons()
        sendstring = BuildVmixSetCommand("SetText", template, settings.TextBox34.Text.Trim, settings.TextBox38.Text.Trim)
        VTX(sendstring)
        sendstring = BuildVmixSetCommand("SetText", template, settings.TextBox35.Text.Trim, settings.TextBox39.Text.Trim)
        VTX(sendstring)
        sendstring = BuildVmixSetCommand("SetText", template, settings.TextBox36.Text.Trim, " ")
        VTX(sendstring)
        sendstring = BuildVmixSetCommand("SetText", template, settings.TextBox37.Text.Trim, " ")
        VTX(sendstring)
        Draw_Logos(template)
        If titleState = False Then
            sendstring = "Function=OverlayInput" + settings.ComboBox3.Text + "In&Input=" + template
            VTX(sendstring)
            titleState = True
            btn_titel.BackColor = Color.Red
            If settings.CheckBox4.Checked Then
                Pauseinfo_Publicdisplay("titel")
            End If
        Else
            sendstring = "Function=OverlayInput" + settings.ComboBox3.Text + "Out"
            VTX(sendstring)
            Reset_Buttons()
            Reset_State()
        End If
        WriteLiveJsonFile()
    End Sub

    Sub Pauseinfo_Publicdisplay(titel As String)
        If settings.CheckBox2.Checked Then
            Dim template As String = Getgtzip(settings.TextBox19.Text)
            Dim clockfield As String = settings.TextBox9.Text
            Dim textfield As String = ""
            Select Case titel
                Case "titel"
                    textfield = ""
                Case "p1"
                    textfield = settings.TextBox3.Text
                Case "p2"
                    textfield = settings.TextBox4.Text
                Case "p3"
                    textfield = settings.TextBox5.Text
                Case "end"
                    textfield = settings.TextBox6.Text
            End Select
            sendstring = BuildVmixSetCommand("SetText", template, clockfield, textfield)
            VTX(sendstring)
        End If
    End Sub

    Private Sub btn_1pause_Click(sender As Object, e As EventArgs) Handles btn_1pause.Click
        HandlePauseOrEndButton(btn_1pause, Pause1State, settings.TextBox3, "p1", setUhrwegRedOnActivate:=False)
    End Sub

    Private Sub btn_2pause_Click(sender As Object, e As EventArgs) Handles btn_2pause.Click
        HandlePauseOrEndButton(btn_2pause, Pause2State, settings.TextBox4, "p2", setUhrwegRedOnActivate:=False)
    End Sub

    Private Sub btn_3pause_Click(sender As Object, e As EventArgs) Handles btn_3pause.Click
        HandlePauseOrEndButton(btn_3pause, Pause3State, settings.TextBox5, "p3", setUhrwegRedOnActivate:=False)
    End Sub

    Private Sub btn_spielende_Click(sender As Object, e As EventArgs) Handles btn_spielende.Click
        HandlePauseOrEndButton(btn_spielende, gameoverState, settings.TextBox6, "end", setUhrwegRedOnActivate:=True)
    End Sub

    ' Gemeinsame Logik für die vier fast identischen Pause/Spielende-Buttons (vorher ~35 quasi
    ' duplizierte Zeilen je Button) - togglet ein Titel-Overlay mit Infotext-Feldern
    ' (TextBox34/35/36/37 <- TextBox38/39/infoTextBox/Ergebnis) ein bzw. aus. stateFlag ByRef,
    ' damit der Helper direkt Pause1State/Pause2State/Pause3State/gameoverState des Aufrufers
    ' aktualisiert, ohne vier separate Wrapper-Methoden.
    '
    ' Bugfix beim Zusammenführen: btn_2pause/btn_3pause hatten (anders als btn_1pause und
    ' btn_spielende) KEINE "Stop Clock First"-Warnung, wenn die Uhr läuft und Autostop
    ' (CheckBox5) deaktiviert ist - man konnte die Uhr dadurch inkonsistent per Pause 2/3
    ' weiterlaufen lassen, während Pause 1/Spielende das verhinderten. Jetzt für alle vier
    ' einheitlich (die sicherere Variante).
    Private Sub HandlePauseOrEndButton(button As Button, ByRef stateFlag As Boolean, infoTextBox As TextBox, publicDisplayCase As String, setUhrwegRedOnActivate As Boolean)
        If clockrunning = True And settings.CheckBox5.Checked = True Then
            Button_clockstop.PerformClick()
        ElseIf clockrunning = True And settings.CheckBox5.Checked = False Then
            MsgBox("Stop Clock First")
            Exit Sub
        End If

        Dim template As String = Getgtzip(settings.TextBox32.Text)
        Dim result As String = lbl_TorHome.Text + " " + lbl_divider.Text + " " + lbl_TorAway.Text
        Reset_Buttons()
        sendstring = BuildVmixSetCommand("SetText", template, settings.TextBox34.Text.Trim, settings.TextBox38.Text.Trim)
        VTX(sendstring)
        sendstring = BuildVmixSetCommand("SetText", template, settings.TextBox35.Text.Trim, settings.TextBox39.Text.Trim)
        VTX(sendstring)
        sendstring = BuildVmixSetCommand("SetText", template, settings.TextBox36.Text.Trim, infoTextBox.Text.Trim)
        VTX(sendstring)
        sendstring = BuildVmixSetCommand("SetText", template, settings.TextBox37.Text.Trim, result)
        VTX(sendstring)
        Draw_Logos(template)

        If stateFlag = False Then
            sendstring = "Function=OverlayInput" + settings.ComboBox3.Text + "In&Input=" + template
            VTX(sendstring)
            stateFlag = True
            button.BackColor = Color.Red
            If settings.CheckBox4.Checked Then
                Pauseinfo_Publicdisplay(publicDisplayCase)
            End If
            Clock_OFF()
            clockstate = True
            If setUhrwegRedOnActivate Then btn_uhrweg.BackColor = Color.Red
        Else
            sendstring = "Function=OverlayInput" + settings.ComboBox3.Text + "Out"
            VTX(sendstring)
            Reset_Buttons()
            Reset_State()
            stateFlag = False
        End If
        WriteLiveJsonFile()
    End Sub

    Private Sub btn_resgross_Click(sender As Object, e As EventArgs) Handles btn_resgross.Click

    End Sub
    Private Sub Reset_Buttons()
        'buttons
        btn_scorebug.BackColor = SystemColors.ControlDark
        btn_titel.BackColor = SystemColors.ControlDark
        btn_1pause.BackColor = SystemColors.ControlDark
        btn_2pause.BackColor = SystemColors.ControlDark
        btn_3pause.BackColor = SystemColors.ControlDark
        btn_spielende.BackColor = SystemColors.ControlDark
        btn_resgross.BackColor = SystemColors.ControlDark
    End Sub

    Private Sub Reset_State()
        'state
        scorebugState = False
        titleState = False
        Pause1State = False
        Pause2State = False
        Pause3State = False
        gameoverState = False
        largeresultState = False
        Clear_TitleText()
        'sets time display in publick display back to time
        Dim template As String = Getgtzip(settings.TextBox19.Text)
        Dim clockfield As String = settings.TextBox9.Text
        sendstring = BuildVmixSetCommand("SetText", template, clockfield, Label_time.Text)
        VTX(sendstring)
        Clock_ON()
        clockstate = False
        btn_uhrweg.BackColor = SystemColors.ControlDark
    End Sub

    Public Sub Draw_Logos(nametemplate)
        Dim clublogo_home As String = directorylogos + settings.TextBox23.Text.Trim + "-" + settings.TextBox21.Text.Trim + ".png"
        Dim clublogo_away As String = directorylogos + settings.TextBox24.Text.Trim + "-" + settings.TextBox22.Text.Trim + ".png"

        ' Kein manuelles Replace(" ", "%20") mehr nötig - BuildVmixSetCommand() kodiert den
        ' kompletten (rohen) Pfad selbst; ein hier bereits vorkodiertes "%20" würde sonst zu
        ' "%2520" doppelkodiert und vMix findet die Logo-Datei nicht mehr.
        sendstring = BuildVmixSetCommand("SetImage", nametemplate, "clublogo_home.Source", clublogo_home)
        VTX(sendstring)
        sendstring = BuildVmixSetCommand("SetImage", nametemplate, "clublogo_away.Source", clublogo_away)
        VTX(sendstring)
        sendstring = BuildVmixSetCommand("SetText", nametemplate, "home_long.Text", settings.TextBox21.Text.Trim)
        VTX(sendstring)
        sendstring = BuildVmixSetCommand("SetText", nametemplate, "away_long.Text", settings.TextBox22.Text.Trim)
        VTX(sendstring)
    End Sub

    Private Sub Clear_TitleText()
        Dim template As String = Getgtzip(settings.TextBox32.Text)
        sendstring = BuildVmixSetCommand("SetText", template, "text_upper.Text", " ")
        VTX(sendstring)
        sendstring = BuildVmixSetCommand("SetText", template, "text_lower.Text", " ")
        VTX(sendstring)
    End Sub

    Private Sub btn_uhrweg_Click(sender As Object, e As EventArgs) Handles btn_uhrweg.Click
        If clockstate = False Then
            Clock_OFF()
            clockstate = True
            btn_uhrweg.BackColor = Color.Red
        Else
            Clock_ON()
            clockstate = False
            btn_uhrweg.BackColor = SystemColors.ControlDark
        End If
    End Sub



    'Private Sub VTX(HTML_URL)
    '    Dim stopwatch As Stopwatch = Stopwatch.StartNew()
    '    If ConnectedState = True Then
    '        'version aus frauenfussball client
    '        'HTML communication with VMIX 
    '        HTML_URL = "http://" + IP + ":8088/API/?" + HTML_URL
    '        Dim responseData As String = ""
    '        'html send function
    '        Try
    '            Dim cookieJar As New Net.CookieContainer()
    '            Dim hwrequest As Net.HttpWebRequest = Net.WebRequest.Create(HTML_URL)
    '            hwrequest.CookieContainer = cookieJar
    '            hwrequest.Accept = "*/*"
    '            hwrequest.AllowAutoRedirect = True
    '            hwrequest.UserAgent = "http_requester/0.1"
    '            hwrequest.Method = "GET"
    '            hwrequest.Timeout = 30
    '            Dim hwresponse As Net.HttpWebResponse = hwrequest.GetResponse()
    '            If hwresponse.StatusCode = Net.HttpStatusCode.OK Then
    '                Dim responseStream As IO.StreamReader =
    '              New IO.StreamReader(hwresponse.GetResponseStream())
    '                responseData = responseStream.ReadToEnd()
    '                ToolStripStatusLabel5.Text = responseData
    '            End If
    '            hwresponse.Close()
    '        Catch ex As Exception
    '            ToolStripStatusLabel5.Text = ("Exception Error in VTX (vMix running?): " & ex.Message)
    '        End Try
    '    End If
    '    stopwatch.Stop()
    '    Dim elapsedTicks As Long = stopwatch.ElapsedTicks
    '    Dim elapsedTime As Double = stopwatch.Elapsed.TotalMilliseconds
    '    ListBox5.Items.Add($"command took {elapsedTime:F3} ms ")
    'End Sub



End Class
