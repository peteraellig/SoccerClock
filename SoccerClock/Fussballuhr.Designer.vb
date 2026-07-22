<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Fussballuhr
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Fussballuhr))
        Me.Label_time = New System.Windows.Forms.Label()
        Me.Button_clockstart = New System.Windows.Forms.Button()
        Me.Label_overtime = New System.Windows.Forms.Label()
        Me.Button_clockstop = New System.Windows.Forms.Button()
        Me.Button_clockplusmin = New System.Windows.Forms.Button()
        Me.Button_clockminusmin = New System.Windows.Forms.Button()
        Me.RadioButton1 = New System.Windows.Forms.RadioButton()
        Me.RadioButton2 = New System.Windows.Forms.RadioButton()
        Me.RadioButton3 = New System.Windows.Forms.RadioButton()
        Me.RadioButton4 = New System.Windows.Forms.RadioButton()
        Me.Button_clockplussec = New System.Windows.Forms.Button()
        Me.Button_clockminussec = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Btn_TorMinus_Home = New System.Windows.Forms.Button()
        Me.Btn_TorPlus_Away = New System.Windows.Forms.Button()
        Me.Btn_TorMinus_Away = New System.Windows.Forms.Button()
        Me.Btn_TorPlus_Home = New System.Windows.Forms.Button()
        Me.awaycolor2 = New System.Windows.Forms.Button()
        Me.awaycolor1 = New System.Windows.Forms.Button()
        Me.homecolor2 = New System.Windows.Forms.Button()
        Me.homecolor1 = New System.Windows.Forms.Button()
        Me.lbl_divider = New System.Windows.Forms.Label()
        Me.btn_nocolor_home = New System.Windows.Forms.Button()
        Me.btn_reset_clock = New System.Windows.Forms.Button()
        Me.btn_setup = New System.Windows.Forms.Button()
        Me.TextBox_overtime = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btn_scorebug = New System.Windows.Forms.Button()
        Me.btn_reset_score = New System.Windows.Forms.Button()
        Me.btn_reset_all = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btn_uhrweg = New System.Windows.Forms.Button()
        Me.btn_exit = New System.Windows.Forms.Button()
        Me.Timer_message = New System.Windows.Forms.Timer(Me.components)
        Me.ListBox5 = New System.Windows.Forms.ListBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.lbl_TorAway = New System.Windows.Forms.Label()
        Me.lbl_TorHome = New System.Windows.Forms.Label()
        Me.NameAway = New System.Windows.Forms.Label()
        Me.NameHome = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabel6 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel2 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel3 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel7 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel9 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel8 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel4 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel5 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ColorDialog1 = New System.Windows.Forms.ColorDialog()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.PictureBox4 = New System.Windows.Forms.PictureBox()
        Me.btn_titel = New System.Windows.Forms.Button()
        Me.btn_1pause = New System.Windows.Forms.Button()
        Me.btn_2pause = New System.Windows.Forms.Button()
        Me.btn_3pause = New System.Windows.Forms.Button()
        Me.btn_spielende = New System.Windows.Forms.Button()
        Me.btn_resgross = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btn_werbung1 = New System.Windows.Forms.Button()
        Me.btn_werbung2 = New System.Windows.Forms.Button()
        Me.ToolStripStatusLabel10 = New System.Windows.Forms.ToolStripStatusLabel()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusStrip1.SuspendLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label_time
        '
        Me.Label_time.Font = New System.Drawing.Font("Segoe UI", 36.0!)
        Me.Label_time.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.Label_time.Location = New System.Drawing.Point(786, 28)
        Me.Label_time.Name = "Label_time"
        Me.Label_time.Size = New System.Drawing.Size(176, 65)
        Me.Label_time.TabIndex = 0
        Me.Label_time.Text = "00:00"
        Me.Label_time.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.ToolTip1.SetToolTip(Me.Label_time, "doubleclick on time resets the clock")
        '
        'Button_clockstart
        '
        Me.Button_clockstart.BackColor = System.Drawing.SystemColors.Control
        Me.Button_clockstart.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button_clockstart.Font = New System.Drawing.Font("Segoe UI", 15.75!)
        Me.Button_clockstart.Location = New System.Drawing.Point(605, 41)
        Me.Button_clockstart.Name = "Button_clockstart"
        Me.Button_clockstart.Size = New System.Drawing.Size(95, 77)
        Me.Button_clockstart.TabIndex = 1
        Me.Button_clockstart.Text = "Clock Start"
        Me.ToolTip1.SetToolTip(Me.Button_clockstart, "starts clock")
        Me.Button_clockstart.UseVisualStyleBackColor = False
        '
        'Label_overtime
        '
        Me.Label_overtime.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.Label_overtime.Location = New System.Drawing.Point(786, 95)
        Me.Label_overtime.Name = "Label_overtime"
        Me.Label_overtime.Size = New System.Drawing.Size(176, 25)
        Me.Label_overtime.TabIndex = 2
        Me.Label_overtime.Text = "00:00"
        Me.Label_overtime.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Button_clockstop
        '
        Me.Button_clockstop.BackColor = System.Drawing.SystemColors.Control
        Me.Button_clockstop.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button_clockstop.Font = New System.Drawing.Font("Segoe UI", 15.75!)
        Me.Button_clockstop.Location = New System.Drawing.Point(1049, 41)
        Me.Button_clockstop.Name = "Button_clockstop"
        Me.Button_clockstop.Size = New System.Drawing.Size(93, 77)
        Me.Button_clockstop.TabIndex = 3
        Me.Button_clockstop.Text = "Clock Stop"
        Me.ToolTip1.SetToolTip(Me.Button_clockstop, "stops the clock and automatically switches to the next time period")
        Me.Button_clockstop.UseVisualStyleBackColor = False
        '
        'Button_clockplusmin
        '
        Me.Button_clockplusmin.BackColor = System.Drawing.SystemColors.Control
        Me.Button_clockplusmin.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button_clockplusmin.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.Button_clockplusmin.Location = New System.Drawing.Point(706, 41)
        Me.Button_clockplusmin.Name = "Button_clockplusmin"
        Me.Button_clockplusmin.Size = New System.Drawing.Size(75, 33)
        Me.Button_clockplusmin.TabIndex = 4
        Me.Button_clockplusmin.Text = "Min. +"
        Me.ToolTip1.SetToolTip(Me.Button_clockplusmin, "Time correction, +1 minute ")
        Me.Button_clockplusmin.UseVisualStyleBackColor = False
        '
        'Button_clockminusmin
        '
        Me.Button_clockminusmin.BackColor = System.Drawing.SystemColors.Control
        Me.Button_clockminusmin.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button_clockminusmin.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.Button_clockminusmin.Location = New System.Drawing.Point(968, 41)
        Me.Button_clockminusmin.Name = "Button_clockminusmin"
        Me.Button_clockminusmin.Size = New System.Drawing.Size(76, 33)
        Me.Button_clockminusmin.TabIndex = 5
        Me.Button_clockminusmin.Text = "Min. -"
        Me.ToolTip1.SetToolTip(Me.Button_clockminusmin, "Time correction, -1 minute")
        Me.Button_clockminusmin.UseVisualStyleBackColor = False
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.BackColor = System.Drawing.Color.IndianRed
        Me.RadioButton1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadioButton1.ForeColor = System.Drawing.Color.White
        Me.RadioButton1.Location = New System.Drawing.Point(623, 8)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(63, 17)
        Me.RadioButton1.TabIndex = 6
        Me.RadioButton1.TabStop = True
        Me.RadioButton1.Text = "1st half"
        Me.RadioButton1.UseVisualStyleBackColor = False
        '
        'RadioButton2
        '
        Me.RadioButton2.AutoSize = True
        Me.RadioButton2.BackColor = System.Drawing.Color.IndianRed
        Me.RadioButton2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadioButton2.ForeColor = System.Drawing.Color.White
        Me.RadioButton2.Location = New System.Drawing.Point(756, 8)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(68, 17)
        Me.RadioButton2.TabIndex = 7
        Me.RadioButton2.TabStop = True
        Me.RadioButton2.Text = "2nd half"
        Me.RadioButton2.UseVisualStyleBackColor = False
        '
        'RadioButton3
        '
        Me.RadioButton3.AutoSize = True
        Me.RadioButton3.BackColor = System.Drawing.Color.IndianRed
        Me.RadioButton3.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadioButton3.ForeColor = System.Drawing.Color.White
        Me.RadioButton3.Location = New System.Drawing.Point(882, 8)
        Me.RadioButton3.Name = "RadioButton3"
        Me.RadioButton3.Size = New System.Drawing.Size(87, 17)
        Me.RadioButton3.TabIndex = 8
        Me.RadioButton3.TabStop = True
        Me.RadioButton3.Text = "1st overtime"
        Me.RadioButton3.UseVisualStyleBackColor = False
        '
        'RadioButton4
        '
        Me.RadioButton4.AutoSize = True
        Me.RadioButton4.BackColor = System.Drawing.Color.IndianRed
        Me.RadioButton4.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadioButton4.ForeColor = System.Drawing.Color.White
        Me.RadioButton4.Location = New System.Drawing.Point(1020, 8)
        Me.RadioButton4.Name = "RadioButton4"
        Me.RadioButton4.Size = New System.Drawing.Size(92, 17)
        Me.RadioButton4.TabIndex = 9
        Me.RadioButton4.TabStop = True
        Me.RadioButton4.Text = "2nd overtime"
        Me.RadioButton4.UseVisualStyleBackColor = False
        '
        'Button_clockplussec
        '
        Me.Button_clockplussec.BackColor = System.Drawing.SystemColors.Control
        Me.Button_clockplussec.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button_clockplussec.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.Button_clockplussec.Location = New System.Drawing.Point(706, 85)
        Me.Button_clockplussec.Name = "Button_clockplussec"
        Me.Button_clockplussec.Size = New System.Drawing.Size(75, 33)
        Me.Button_clockplussec.TabIndex = 11
        Me.Button_clockplussec.Text = "Sek. +"
        Me.ToolTip1.SetToolTip(Me.Button_clockplussec, "Time correction, +1 second")
        Me.Button_clockplussec.UseVisualStyleBackColor = False
        '
        'Button_clockminussec
        '
        Me.Button_clockminussec.BackColor = System.Drawing.SystemColors.Control
        Me.Button_clockminussec.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button_clockminussec.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.Button_clockminussec.Location = New System.Drawing.Point(968, 85)
        Me.Button_clockminussec.Name = "Button_clockminussec"
        Me.Button_clockminussec.Size = New System.Drawing.Size(76, 33)
        Me.Button_clockminussec.TabIndex = 12
        Me.Button_clockminussec.Text = "Sek.  -"
        Me.ToolTip1.SetToolTip(Me.Button_clockminussec, "Time correction, -1 second")
        Me.Button_clockminussec.UseVisualStyleBackColor = False
        '
        'Btn_TorMinus_Home
        '
        Me.Btn_TorMinus_Home.BackColor = System.Drawing.Color.Gainsboro
        Me.Btn_TorMinus_Home.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.Btn_TorMinus_Home.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Btn_TorMinus_Home.Font = New System.Drawing.Font("Microsoft Sans Serif", 48.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_TorMinus_Home.ForeColor = System.Drawing.Color.White
        Me.Btn_TorMinus_Home.Location = New System.Drawing.Point(134, 63)
        Me.Btn_TorMinus_Home.Name = "Btn_TorMinus_Home"
        Me.Btn_TorMinus_Home.Size = New System.Drawing.Size(45, 50)
        Me.Btn_TorMinus_Home.TabIndex = 988
        Me.Btn_TorMinus_Home.Text = "-"
        Me.ToolTip1.SetToolTip(Me.Btn_TorMinus_Home, "Goal MINUS for home team")
        Me.Btn_TorMinus_Home.UseCompatibleTextRendering = True
        Me.Btn_TorMinus_Home.UseVisualStyleBackColor = False
        '
        'Btn_TorPlus_Away
        '
        Me.Btn_TorPlus_Away.BackColor = System.Drawing.Color.IndianRed
        Me.Btn_TorPlus_Away.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Btn_TorPlus_Away.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.Btn_TorPlus_Away.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Btn_TorPlus_Away.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_TorPlus_Away.ForeColor = System.Drawing.Color.White
        Me.Btn_TorPlus_Away.Location = New System.Drawing.Point(477, 36)
        Me.Btn_TorPlus_Away.Name = "Btn_TorPlus_Away"
        Me.Btn_TorPlus_Away.Size = New System.Drawing.Size(80, 80)
        Me.Btn_TorPlus_Away.TabIndex = 985
        Me.Btn_TorPlus_Away.Text = "+"
        Me.ToolTip1.SetToolTip(Me.Btn_TorPlus_Away, "Goal for the away team")
        Me.Btn_TorPlus_Away.UseVisualStyleBackColor = False
        '
        'Btn_TorMinus_Away
        '
        Me.Btn_TorMinus_Away.BackColor = System.Drawing.Color.Gainsboro
        Me.Btn_TorMinus_Away.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.Btn_TorMinus_Away.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Btn_TorMinus_Away.Font = New System.Drawing.Font("Microsoft Sans Serif", 48.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_TorMinus_Away.ForeColor = System.Drawing.Color.White
        Me.Btn_TorMinus_Away.Location = New System.Drawing.Point(379, 63)
        Me.Btn_TorMinus_Away.Name = "Btn_TorMinus_Away"
        Me.Btn_TorMinus_Away.Size = New System.Drawing.Size(50, 50)
        Me.Btn_TorMinus_Away.TabIndex = 986
        Me.Btn_TorMinus_Away.Text = "-"
        Me.ToolTip1.SetToolTip(Me.Btn_TorMinus_Away, "Goal MINUS for away team")
        Me.Btn_TorMinus_Away.UseCompatibleTextRendering = True
        Me.Btn_TorMinus_Away.UseVisualStyleBackColor = False
        '
        'Btn_TorPlus_Home
        '
        Me.Btn_TorPlus_Home.BackColor = System.Drawing.Color.IndianRed
        Me.Btn_TorPlus_Home.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Btn_TorPlus_Home.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.Btn_TorPlus_Home.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Btn_TorPlus_Home.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_TorPlus_Home.ForeColor = System.Drawing.Color.White
        Me.Btn_TorPlus_Home.Location = New System.Drawing.Point(18, 36)
        Me.Btn_TorPlus_Home.Name = "Btn_TorPlus_Home"
        Me.Btn_TorPlus_Home.Size = New System.Drawing.Size(80, 80)
        Me.Btn_TorPlus_Home.TabIndex = 987
        Me.Btn_TorPlus_Home.Text = "+"
        Me.ToolTip1.SetToolTip(Me.Btn_TorPlus_Home, "Goal for the home team")
        Me.Btn_TorPlus_Home.UseVisualStyleBackColor = False
        '
        'awaycolor2
        '
        Me.awaycolor2.FlatAppearance.BorderSize = 0
        Me.awaycolor2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.awaycolor2.Location = New System.Drawing.Point(413, 145)
        Me.awaycolor2.Name = "awaycolor2"
        Me.awaycolor2.Size = New System.Drawing.Size(150, 15)
        Me.awaycolor2.TabIndex = 1159
        Me.ToolTip1.SetToolTip(Me.awaycolor2, "Changes colour 2 away team, also during the match")
        Me.awaycolor2.UseVisualStyleBackColor = True
        '
        'awaycolor1
        '
        Me.awaycolor1.FlatAppearance.BorderSize = 0
        Me.awaycolor1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.awaycolor1.Location = New System.Drawing.Point(413, 129)
        Me.awaycolor1.Name = "awaycolor1"
        Me.awaycolor1.Size = New System.Drawing.Size(150, 15)
        Me.awaycolor1.TabIndex = 1158
        Me.ToolTip1.SetToolTip(Me.awaycolor1, "Changes colour 1 away team, also during the match")
        Me.awaycolor1.UseVisualStyleBackColor = True
        '
        'homecolor2
        '
        Me.homecolor2.FlatAppearance.BorderSize = 0
        Me.homecolor2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.homecolor2.Location = New System.Drawing.Point(12, 145)
        Me.homecolor2.Name = "homecolor2"
        Me.homecolor2.Size = New System.Drawing.Size(150, 15)
        Me.homecolor2.TabIndex = 1157
        Me.ToolTip1.SetToolTip(Me.homecolor2, "Changes colour 2 home team, also during the match")
        Me.homecolor2.UseVisualStyleBackColor = True
        '
        'homecolor1
        '
        Me.homecolor1.FlatAppearance.BorderSize = 0
        Me.homecolor1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.homecolor1.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.homecolor1.Location = New System.Drawing.Point(12, 129)
        Me.homecolor1.Name = "homecolor1"
        Me.homecolor1.Size = New System.Drawing.Size(150, 15)
        Me.homecolor1.TabIndex = 1156
        Me.ToolTip1.SetToolTip(Me.homecolor1, "Changes colour 1 home team, also during the match")
        Me.homecolor1.UseVisualStyleBackColor = True
        '
        'lbl_divider
        '
        Me.lbl_divider.AutoSize = True
        Me.lbl_divider.BackColor = System.Drawing.Color.DimGray
        Me.lbl_divider.Font = New System.Drawing.Font("Microsoft Sans Serif", 27.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_divider.ForeColor = System.Drawing.Color.White
        Me.lbl_divider.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lbl_divider.Location = New System.Drawing.Point(261, 74)
        Me.lbl_divider.Name = "lbl_divider"
        Me.lbl_divider.Size = New System.Drawing.Size(28, 42)
        Me.lbl_divider.TabIndex = 1163
        Me.lbl_divider.Text = ":"
        Me.lbl_divider.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolTip1.SetToolTip(Me.lbl_divider, "doubleclick resets result")
        '
        'btn_nocolor_home
        '
        Me.btn_nocolor_home.BackColor = System.Drawing.Color.Gainsboro
        Me.btn_nocolor_home.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.btn_nocolor_home.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_nocolor_home.Font = New System.Drawing.Font("Segoe UI", 6.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_nocolor_home.ForeColor = System.Drawing.Color.Black
        Me.btn_nocolor_home.Location = New System.Drawing.Point(360, 131)
        Me.btn_nocolor_home.Name = "btn_nocolor_home"
        Me.btn_nocolor_home.Size = New System.Drawing.Size(46, 24)
        Me.btn_nocolor_home.TabIndex = 1166
        Me.btn_nocolor_home.Text = "no color"
        Me.ToolTip1.SetToolTip(Me.btn_nocolor_home, "no team color")
        Me.btn_nocolor_home.UseCompatibleTextRendering = True
        Me.btn_nocolor_home.UseVisualStyleBackColor = False
        '
        'btn_reset_clock
        '
        Me.btn_reset_clock.BackColor = System.Drawing.SystemColors.Control
        Me.btn_reset_clock.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_reset_clock.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_reset_clock.Location = New System.Drawing.Point(782, 131)
        Me.btn_reset_clock.Name = "btn_reset_clock"
        Me.btn_reset_clock.Size = New System.Drawing.Size(180, 29)
        Me.btn_reset_clock.TabIndex = 18
        Me.btn_reset_clock.Text = "RESET CLOCK"
        Me.btn_reset_clock.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.ToolTip1.SetToolTip(Me.btn_reset_clock, "Resets clock")
        Me.btn_reset_clock.UseVisualStyleBackColor = False
        '
        'btn_setup
        '
        Me.btn_setup.BackColor = System.Drawing.Color.LightGreen
        Me.btn_setup.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.btn_setup.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_setup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_setup.Location = New System.Drawing.Point(1049, 270)
        Me.btn_setup.Name = "btn_setup"
        Me.btn_setup.Size = New System.Drawing.Size(103, 55)
        Me.btn_setup.TabIndex = 19
        Me.btn_setup.Text = "settings"
        Me.ToolTip1.SetToolTip(Me.btn_setup, "Various settings for programme and title")
        Me.btn_setup.UseVisualStyleBackColor = False
        '
        'TextBox_overtime
        '
        Me.TextBox_overtime.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox_overtime.Location = New System.Drawing.Point(1072, 216)
        Me.TextBox_overtime.Name = "TextBox_overtime"
        Me.TextBox_overtime.Size = New System.Drawing.Size(55, 35)
        Me.TextBox_overtime.TabIndex = 20
        Me.TextBox_overtime.Text = "5"
        Me.TextBox_overtime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ToolTip1.SetToolTip(Me.TextBox_overtime, "extra time indicated by the referee")
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.Label1.Location = New System.Drawing.Point(1024, 164)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(128, 31)
        Me.Label1.TabIndex = 21
        Me.Label1.Text = "overtime to play"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.ToolTip1.SetToolTip(Me.Label1, "extra time indicated by the referee")
        '
        'btn_scorebug
        '
        Me.btn_scorebug.BackColor = System.Drawing.SystemColors.ControlDark
        Me.btn_scorebug.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.btn_scorebug.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_scorebug.Font = New System.Drawing.Font("Segoe UI", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_scorebug.Location = New System.Drawing.Point(8, 177)
        Me.btn_scorebug.Name = "btn_scorebug"
        Me.btn_scorebug.Size = New System.Drawing.Size(180, 80)
        Me.btn_scorebug.TabIndex = 24
        Me.btn_scorebug.Text = "scorebug "
        Me.ToolTip1.SetToolTip(Me.btn_scorebug, "small result during the match")
        Me.btn_scorebug.UseVisualStyleBackColor = False
        '
        'btn_reset_score
        '
        Me.btn_reset_score.BackColor = System.Drawing.SystemColors.Control
        Me.btn_reset_score.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_reset_score.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_reset_score.Location = New System.Drawing.Point(205, 129)
        Me.btn_reset_score.Name = "btn_reset_score"
        Me.btn_reset_score.Size = New System.Drawing.Size(139, 29)
        Me.btn_reset_score.TabIndex = 1161
        Me.btn_reset_score.Text = "RESET SCORE"
        Me.btn_reset_score.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.ToolTip1.SetToolTip(Me.btn_reset_score, "Resets result")
        Me.btn_reset_score.UseVisualStyleBackColor = False
        '
        'btn_reset_all
        '
        Me.btn_reset_all.BackColor = System.Drawing.Color.LightYellow
        Me.btn_reset_all.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.btn_reset_all.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_reset_all.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_reset_all.ForeColor = System.Drawing.Color.Red
        Me.btn_reset_all.Location = New System.Drawing.Point(488, 177)
        Me.btn_reset_all.Name = "btn_reset_all"
        Me.btn_reset_all.Size = New System.Drawing.Size(180, 80)
        Me.btn_reset_all.TabIndex = 1162
        Me.btn_reset_all.Text = "RESET ALL"
        Me.ToolTip1.SetToolTip(Me.btn_reset_all, "Resets result and clock")
        Me.btn_reset_all.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(1046, 194)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(102, 19)
        Me.Label3.TabIndex = 1164
        Me.Label3.Text = "only digits (no +)"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.ToolTip1.SetToolTip(Me.Label3, "extra time indicated by the referee")
        '
        'btn_uhrweg
        '
        Me.btn_uhrweg.BackColor = System.Drawing.SystemColors.ControlDark
        Me.btn_uhrweg.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_uhrweg.Location = New System.Drawing.Point(221, 177)
        Me.btn_uhrweg.Name = "btn_uhrweg"
        Me.btn_uhrweg.Size = New System.Drawing.Size(80, 80)
        Me.btn_uhrweg.TabIndex = 1174
        Me.btn_uhrweg.Text = "no clock"
        Me.ToolTip1.SetToolTip(Me.btn_uhrweg, "Hides the clock display in the inserts")
        Me.btn_uhrweg.UseVisualStyleBackColor = False
        '
        'btn_exit
        '
        Me.btn_exit.BackColor = System.Drawing.Color.IndianRed
        Me.btn_exit.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.btn_exit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_exit.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_exit.ForeColor = System.Drawing.Color.White
        Me.btn_exit.Location = New System.Drawing.Point(1049, 331)
        Me.btn_exit.Name = "btn_exit"
        Me.btn_exit.Size = New System.Drawing.Size(103, 53)
        Me.btn_exit.TabIndex = 22
        Me.btn_exit.Text = "exit"
        Me.btn_exit.UseVisualStyleBackColor = False
        '
        'Timer_message
        '
        '
        'ListBox5
        '
        Me.ListBox5.FormattingEnabled = True
        Me.ListBox5.Location = New System.Drawing.Point(841, 177)
        Me.ListBox5.Name = "ListBox5"
        Me.ListBox5.Size = New System.Drawing.Size(187, 212)
        Me.ListBox5.TabIndex = 25
        '
        'PictureBox2
        '
        Me.PictureBox2.BackColor = System.Drawing.Color.IndianRed
        Me.PictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBox2.Location = New System.Drawing.Point(592, 5)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(560, 158)
        Me.PictureBox2.TabIndex = 29
        Me.PictureBox2.TabStop = False
        '
        'lbl_TorAway
        '
        Me.lbl_TorAway.BackColor = System.Drawing.Color.DimGray
        Me.lbl_TorAway.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TorAway.ForeColor = System.Drawing.Color.White
        Me.lbl_TorAway.Location = New System.Drawing.Point(281, 66)
        Me.lbl_TorAway.Name = "lbl_TorAway"
        Me.lbl_TorAway.Size = New System.Drawing.Size(99, 57)
        Me.lbl_TorAway.TabIndex = 984
        Me.lbl_TorAway.Text = "88"
        Me.lbl_TorAway.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl_TorHome
        '
        Me.lbl_TorHome.BackColor = System.Drawing.Color.DimGray
        Me.lbl_TorHome.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TorHome.ForeColor = System.Drawing.Color.White
        Me.lbl_TorHome.Location = New System.Drawing.Point(175, 66)
        Me.lbl_TorHome.Name = "lbl_TorHome"
        Me.lbl_TorHome.Size = New System.Drawing.Size(94, 57)
        Me.lbl_TorHome.TabIndex = 983
        Me.lbl_TorHome.Text = "88"
        Me.lbl_TorHome.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'NameAway
        '
        Me.NameAway.BackColor = System.Drawing.Color.DimGray
        Me.NameAway.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NameAway.ForeColor = System.Drawing.Color.White
        Me.NameAway.Location = New System.Drawing.Point(303, 4)
        Me.NameAway.Name = "NameAway"
        Me.NameAway.Size = New System.Drawing.Size(263, 29)
        Me.NameAway.TabIndex = 1155
        Me.NameAway.Text = "longlonglong awayname AAA"
        Me.NameAway.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'NameHome
        '
        Me.NameHome.BackColor = System.Drawing.Color.DimGray
        Me.NameHome.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NameHome.ForeColor = System.Drawing.Color.White
        Me.NameHome.Location = New System.Drawing.Point(9, 4)
        Me.NameHome.Name = "NameHome"
        Me.NameHome.Size = New System.Drawing.Size(261, 29)
        Me.NameHome.TabIndex = 1154
        Me.NameHome.Text = "longlonglong homename A"
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.DimGray
        Me.PictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBox1.Location = New System.Drawing.Point(8, 4)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(560, 158)
        Me.PictureBox1.TabIndex = 1160
        Me.PictureBox1.TabStop = False
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel6, Me.ToolStripStatusLabel1, Me.ToolStripStatusLabel2, Me.ToolStripStatusLabel3, Me.ToolStripStatusLabel7, Me.ToolStripStatusLabel9, Me.ToolStripStatusLabel8, Me.ToolStripStatusLabel4, Me.ToolStripStatusLabel10, Me.ToolStripStatusLabel5})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 392)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(1160, 22)
        Me.StatusStrip1.TabIndex = 1165
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripStatusLabel6
        '
        Me.ToolStripStatusLabel6.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.ToolStripStatusLabel6.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me.ToolStripStatusLabel6.Name = "ToolStripStatusLabel6"
        Me.ToolStripStatusLabel6.Size = New System.Drawing.Size(121, 17)
        Me.ToolStripStatusLabel6.Text = "ToolStripStatusLabel6"
        Me.ToolStripStatusLabel6.ToolTipText = "click to reconnect"
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.ToolStripStatusLabel1.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me.ToolStripStatusLabel1.Margin = New System.Windows.Forms.Padding(0, 3, 5, 2)
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(121, 17)
        Me.ToolStripStatusLabel1.Text = "ToolStripStatusLabel1"
        '
        'ToolStripStatusLabel2
        '
        Me.ToolStripStatusLabel2.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.ToolStripStatusLabel2.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me.ToolStripStatusLabel2.Margin = New System.Windows.Forms.Padding(0, 3, 5, 2)
        Me.ToolStripStatusLabel2.Name = "ToolStripStatusLabel2"
        Me.ToolStripStatusLabel2.Size = New System.Drawing.Size(121, 17)
        Me.ToolStripStatusLabel2.Text = "ToolStripStatusLabel2"
        '
        'ToolStripStatusLabel3
        '
        Me.ToolStripStatusLabel3.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.ToolStripStatusLabel3.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me.ToolStripStatusLabel3.Margin = New System.Windows.Forms.Padding(0, 3, 5, 2)
        Me.ToolStripStatusLabel3.Name = "ToolStripStatusLabel3"
        Me.ToolStripStatusLabel3.Size = New System.Drawing.Size(121, 17)
        Me.ToolStripStatusLabel3.Text = "ToolStripStatusLabel3"
        '
        'ToolStripStatusLabel7
        '
        Me.ToolStripStatusLabel7.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.ToolStripStatusLabel7.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me.ToolStripStatusLabel7.Margin = New System.Windows.Forms.Padding(0, 3, 5, 2)
        Me.ToolStripStatusLabel7.Name = "ToolStripStatusLabel7"
        Me.ToolStripStatusLabel7.Size = New System.Drawing.Size(121, 17)
        Me.ToolStripStatusLabel7.Text = "ToolStripStatusLabel7"
        '
        'ToolStripStatusLabel9
        '
        Me.ToolStripStatusLabel9.AutoSize = False
        Me.ToolStripStatusLabel9.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.ToolStripStatusLabel9.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner
        Me.ToolStripStatusLabel9.Margin = New System.Windows.Forms.Padding(0, 3, 5, 2)
        Me.ToolStripStatusLabel9.Name = "ToolStripStatusLabel9"
        Me.ToolStripStatusLabel9.Size = New System.Drawing.Size(122, 17)
        Me.ToolStripStatusLabel9.Text = "ToolStripStatusLabel9"
        '
        'ToolStripStatusLabel8
        '
        Me.ToolStripStatusLabel8.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.ToolStripStatusLabel8.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me.ToolStripStatusLabel8.Name = "ToolStripStatusLabel8"
        Me.ToolStripStatusLabel8.Size = New System.Drawing.Size(121, 17)
        Me.ToolStripStatusLabel8.Text = "ToolStripStatusLabel8"
        '
        'ToolStripStatusLabel4
        '
        Me.ToolStripStatusLabel4.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.ToolStripStatusLabel4.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me.ToolStripStatusLabel4.Margin = New System.Windows.Forms.Padding(0, 3, 5, 2)
        Me.ToolStripStatusLabel4.Name = "ToolStripStatusLabel4"
        Me.ToolStripStatusLabel4.Size = New System.Drawing.Size(69, 17)
        Me.ToolStripStatusLabel4.Text = "vmix timing"
        '
        'ToolStripStatusLabel5
        '
        Me.ToolStripStatusLabel5.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.ToolStripStatusLabel5.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me.ToolStripStatusLabel5.Name = "ToolStripStatusLabel5"
        Me.ToolStripStatusLabel5.Size = New System.Drawing.Size(121, 17)
        Me.ToolStripStatusLabel5.Text = "ToolStripStatusLabel5"
        '
        'PictureBox3
        '
        Me.PictureBox3.BackColor = System.Drawing.Color.IndianRed
        Me.PictureBox3.Location = New System.Drawing.Point(766, 28)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(237, 12)
        Me.PictureBox3.TabIndex = 1167
        Me.PictureBox3.TabStop = False
        '
        'PictureBox4
        '
        Me.PictureBox4.BackColor = System.Drawing.Color.IndianRed
        Me.PictureBox4.Location = New System.Drawing.Point(782, 85)
        Me.PictureBox4.Name = "PictureBox4"
        Me.PictureBox4.Size = New System.Drawing.Size(180, 12)
        Me.PictureBox4.TabIndex = 1168
        Me.PictureBox4.TabStop = False
        '
        'btn_titel
        '
        Me.btn_titel.BackColor = System.Drawing.SystemColors.ControlDark
        Me.btn_titel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_titel.Location = New System.Drawing.Point(8, 304)
        Me.btn_titel.Name = "btn_titel"
        Me.btn_titel.Size = New System.Drawing.Size(80, 80)
        Me.btn_titel.TabIndex = 1169
        Me.btn_titel.Text = "Opening Title"
        Me.btn_titel.UseVisualStyleBackColor = False
        '
        'btn_1pause
        '
        Me.btn_1pause.BackColor = System.Drawing.SystemColors.ControlDark
        Me.btn_1pause.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_1pause.Location = New System.Drawing.Point(119, 304)
        Me.btn_1pause.Name = "btn_1pause"
        Me.btn_1pause.Size = New System.Drawing.Size(80, 80)
        Me.btn_1pause.TabIndex = 1170
        Me.btn_1pause.Text = "1st pause"
        Me.btn_1pause.UseVisualStyleBackColor = False
        '
        'btn_2pause
        '
        Me.btn_2pause.BackColor = System.Drawing.SystemColors.ControlDark
        Me.btn_2pause.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_2pause.Location = New System.Drawing.Point(205, 304)
        Me.btn_2pause.Name = "btn_2pause"
        Me.btn_2pause.Size = New System.Drawing.Size(80, 80)
        Me.btn_2pause.TabIndex = 1171
        Me.btn_2pause.Text = "2nd pause"
        Me.btn_2pause.UseVisualStyleBackColor = False
        '
        'btn_3pause
        '
        Me.btn_3pause.BackColor = System.Drawing.SystemColors.ControlDark
        Me.btn_3pause.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_3pause.Location = New System.Drawing.Point(290, 304)
        Me.btn_3pause.Name = "btn_3pause"
        Me.btn_3pause.Size = New System.Drawing.Size(80, 80)
        Me.btn_3pause.TabIndex = 1172
        Me.btn_3pause.Text = "3rd pause"
        Me.btn_3pause.UseVisualStyleBackColor = False
        '
        'btn_spielende
        '
        Me.btn_spielende.BackColor = System.Drawing.SystemColors.ControlDark
        Me.btn_spielende.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_spielende.Location = New System.Drawing.Point(406, 304)
        Me.btn_spielende.Name = "btn_spielende"
        Me.btn_spielende.Size = New System.Drawing.Size(80, 80)
        Me.btn_spielende.TabIndex = 1173
        Me.btn_spielende.Text = "game over"
        Me.btn_spielende.UseVisualStyleBackColor = False
        '
        'btn_resgross
        '
        Me.btn_resgross.BackColor = System.Drawing.SystemColors.ControlDark
        Me.btn_resgross.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_resgross.Location = New System.Drawing.Point(326, 177)
        Me.btn_resgross.Name = "btn_resgross"
        Me.btn_resgross.Size = New System.Drawing.Size(80, 80)
        Me.btn_resgross.TabIndex = 1175
        Me.btn_resgross.Text = "large result"
        Me.btn_resgross.UseVisualStyleBackColor = False
        Me.btn_resgross.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(15, 270)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(39, 13)
        Me.Label2.TabIndex = 1176
        Me.Label2.Text = "Label2"
        Me.Label2.Visible = False
        '
        'btn_werbung1
        '
        Me.btn_werbung1.BackColor = System.Drawing.SystemColors.ControlDark
        Me.btn_werbung1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_werbung1.Location = New System.Drawing.Point(588, 304)
        Me.btn_werbung1.Name = "btn_werbung1"
        Me.btn_werbung1.Size = New System.Drawing.Size(80, 80)
        Me.btn_werbung1.TabIndex = 1177
        Me.btn_werbung1.Text = "Sponsor1"
        Me.btn_werbung1.UseVisualStyleBackColor = False
        '
        'btn_werbung2
        '
        Me.btn_werbung2.BackColor = System.Drawing.SystemColors.ControlDark
        Me.btn_werbung2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_werbung2.Location = New System.Drawing.Point(674, 304)
        Me.btn_werbung2.Name = "btn_werbung2"
        Me.btn_werbung2.Size = New System.Drawing.Size(80, 80)
        Me.btn_werbung2.TabIndex = 1178
        Me.btn_werbung2.Text = "Sponsor2"
        Me.btn_werbung2.UseVisualStyleBackColor = False
        '
        'ToolStripStatusLabel10
        '
        Me.ToolStripStatusLabel10.Name = "ToolStripStatusLabel10"
        Me.ToolStripStatusLabel10.Size = New System.Drawing.Size(123, 17)
        Me.ToolStripStatusLabel10.Text = "ToolStripStatusLabel10"
        '
        'Fussballuhr
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightGray
        Me.ClientSize = New System.Drawing.Size(1160, 414)
        Me.Controls.Add(Me.btn_werbung2)
        Me.Controls.Add(Me.btn_werbung1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btn_resgross)
        Me.Controls.Add(Me.btn_uhrweg)
        Me.Controls.Add(Me.btn_spielende)
        Me.Controls.Add(Me.btn_3pause)
        Me.Controls.Add(Me.btn_2pause)
        Me.Controls.Add(Me.btn_1pause)
        Me.Controls.Add(Me.btn_titel)
        Me.Controls.Add(Me.Label_overtime)
        Me.Controls.Add(Me.btn_reset_clock)
        Me.Controls.Add(Me.Button_clockminussec)
        Me.Controls.Add(Me.Button_clockplussec)
        Me.Controls.Add(Me.Button_clockminusmin)
        Me.Controls.Add(Me.Button_clockplusmin)
        Me.Controls.Add(Me.Button_clockstop)
        Me.Controls.Add(Me.Button_clockstart)
        Me.Controls.Add(Me.PictureBox4)
        Me.Controls.Add(Me.PictureBox3)
        Me.Controls.Add(Me.btn_nocolor_home)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBox_overtime)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Btn_TorMinus_Away)
        Me.Controls.Add(Me.Btn_TorMinus_Home)
        Me.Controls.Add(Me.lbl_TorHome)
        Me.Controls.Add(Me.lbl_TorAway)
        Me.Controls.Add(Me.lbl_divider)
        Me.Controls.Add(Me.btn_reset_all)
        Me.Controls.Add(Me.btn_reset_score)
        Me.Controls.Add(Me.awaycolor2)
        Me.Controls.Add(Me.awaycolor1)
        Me.Controls.Add(Me.homecolor2)
        Me.Controls.Add(Me.homecolor1)
        Me.Controls.Add(Me.NameAway)
        Me.Controls.Add(Me.NameHome)
        Me.Controls.Add(Me.Btn_TorPlus_Away)
        Me.Controls.Add(Me.Btn_TorPlus_Home)
        Me.Controls.Add(Me.ListBox5)
        Me.Controls.Add(Me.btn_scorebug)
        Me.Controls.Add(Me.btn_exit)
        Me.Controls.Add(Me.btn_setup)
        Me.Controls.Add(Me.RadioButton4)
        Me.Controls.Add(Me.RadioButton3)
        Me.Controls.Add(Me.RadioButton2)
        Me.Controls.Add(Me.RadioButton1)
        Me.Controls.Add(Me.Label_time)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.PictureBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "Fussballuhr"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Soccer Clock"
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label_time As Label
    Friend WithEvents Button_clockstart As Button
    Friend WithEvents Label_overtime As Label
    Friend WithEvents Button_clockstop As Button
    Friend WithEvents Button_clockplusmin As Button
    Friend WithEvents Button_clockminusmin As Button
    Friend WithEvents RadioButton1 As RadioButton
    Friend WithEvents RadioButton2 As RadioButton
    Friend WithEvents RadioButton3 As RadioButton
    Friend WithEvents RadioButton4 As RadioButton
    Friend WithEvents Button_clockplussec As Button
    Friend WithEvents Button_clockminussec As Button
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents btn_reset_clock As Button
    Friend WithEvents btn_setup As Button
    Friend WithEvents TextBox_overtime As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents btn_exit As Button
    Friend WithEvents Timer_message As Timer
    Friend WithEvents btn_scorebug As Button
    Friend WithEvents ListBox5 As ListBox
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents Btn_TorMinus_Home As Button
    Friend WithEvents Btn_TorPlus_Away As Button
    Friend WithEvents Btn_TorMinus_Away As Button
    Friend WithEvents lbl_TorAway As Label
    Friend WithEvents Btn_TorPlus_Home As Button
    Friend WithEvents lbl_TorHome As Label
    Friend WithEvents awaycolor2 As Button
    Friend WithEvents awaycolor1 As Button
    Friend WithEvents homecolor2 As Button
    Friend WithEvents homecolor1 As Button
    Friend WithEvents NameAway As Label
    Friend WithEvents NameHome As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents btn_reset_score As Button
    Friend WithEvents btn_reset_all As Button
    Friend WithEvents lbl_divider As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents ToolStripStatusLabel1 As ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel2 As ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel3 As ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel4 As ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel5 As ToolStripStatusLabel
    Friend WithEvents ColorDialog1 As ColorDialog
    Friend WithEvents btn_nocolor_home As Button
    Friend WithEvents PictureBox3 As PictureBox
    Friend WithEvents PictureBox4 As PictureBox
    Friend WithEvents ToolStripStatusLabel6 As ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel7 As ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel8 As ToolStripStatusLabel
    Friend WithEvents btn_titel As Button
    Friend WithEvents btn_1pause As Button
    Friend WithEvents btn_2pause As Button
    Friend WithEvents btn_3pause As Button
    Friend WithEvents btn_spielende As Button
    Friend WithEvents btn_uhrweg As Button
    Friend WithEvents btn_resgross As Button
    Friend WithEvents ToolStripStatusLabel9 As ToolStripStatusLabel
    Friend WithEvents Label2 As Label
    Friend WithEvents btn_werbung1 As Button
    Friend WithEvents btn_werbung2 As Button
    Friend WithEvents ToolStripStatusLabel10 As ToolStripStatusLabel
End Class
