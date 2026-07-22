Imports System.Text

' Schreibt den aktuellen Spielstand als JSON-Datei auf die Festplatte - kein eingebetteter
' Server nötig (SoccerClock hat wenig Zustandsänderungen, ein einfacher periodischer
' Datei-Schreibvorgang reicht). Analog zu Tennis26/TennisJsonExporter.vb, 1:1 übernommen
' (die Klasse selbst ist bereits generisch, nichts Tennis-Spezifisches drin).
Public Class LiveJsonExporter

    Public Property LastError As String = ""

    ' Schreibt "json" atomar nach "filePath": zuerst in eine .tmp-Datei im selben Ordner,
    ' danach per Move ersetzt (auf demselben Volume ein atomarer Rename) - damit ein
    ' lesender Prozess (z.B. vMix' JSON Data Source) nie eine nur halb geschriebene Datei sieht.
    Public Function WriteToFile(filePath As String, json As String) As Boolean
        Try
            Dim directoryPath = IO.Path.GetDirectoryName(filePath)
            If Not IO.Directory.Exists(directoryPath) Then IO.Directory.CreateDirectory(directoryPath)

            Dim tempPath = filePath & ".tmp"
            IO.File.WriteAllText(tempPath, json, New UTF8Encoding(False))

            If IO.File.Exists(filePath) Then IO.File.Delete(filePath)
            IO.File.Move(tempPath, filePath)

            LastError = ""
            Return True
        Catch ex As Exception
            LastError = ex.Message
            Return False
        End Try
    End Function

End Class

' Baut ein JSON-Objekt aus einzelnen Feldern zusammen, ohne Kommas/Klammern von Hand zählen
' zu müssen. 1:1 aus Tennis26/TennisJsonExporter.vb übernommen.
Public Class JsonObjectBuilder
    Private ReadOnly parts As New List(Of String)

    Public Function AddString(key As String, value As String) As JsonObjectBuilder
        parts.Add($"""{JsonObjectBuilder.Escape(key)}"":""{JsonObjectBuilder.Escape(value)}""")
        Return Me
    End Function

    Public Function AddInt(key As String, value As Integer) As JsonObjectBuilder
        parts.Add($"""{JsonObjectBuilder.Escape(key)}"":{value}")
        Return Me
    End Function

    Public Function AddBool(key As String, value As Boolean) As JsonObjectBuilder
        parts.Add($"""{JsonObjectBuilder.Escape(key)}"":{If(value, "true", "false")}")
        Return Me
    End Function

    ' Für bereits fertige JSON-Fragmente (z.B. ein verschachteltes Objekt aus einem
    ' zweiten JsonObjectBuilder.ToString()).
    Public Function AddRaw(key As String, rawJson As String) As JsonObjectBuilder
        parts.Add($"""{JsonObjectBuilder.Escape(key)}"":{rawJson}")
        Return Me
    End Function

    Public Overrides Function ToString() As String
        Return "{" & String.Join(",", parts) & "}"
    End Function

    Public Shared Function Escape(value As String) As String
        If value Is Nothing Then Return ""
        Dim sb As New StringBuilder()
        For Each c As Char In value
            Select Case c
                Case """"c : sb.Append("\""")
                Case "\"c : sb.Append("\\")
                Case Chr(8) : sb.Append("\b")
                Case Chr(9) : sb.Append("\t")
                Case Chr(10) : sb.Append("\n")
                Case Chr(12) : sb.Append("\f")
                Case Chr(13) : sb.Append("\r")
                Case Else
                    If AscW(c) < &H20 Then
                        sb.Append("\u" & Convert.ToInt32(c).ToString("x4"))
                    Else
                        sb.Append(c)
                    End If
            End Select
        Next
        Return sb.ToString()
    End Function
End Class
