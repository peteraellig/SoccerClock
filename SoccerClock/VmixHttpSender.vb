' Versendet vMix-Befehle per HTTP-GET an die vMix-Web-API (http://IP:Port/API/?Function=...).
' Analog zu Tennis26/VmixHttpSender.vb - IP/Port kommen hier aus Fussballuhr.IP/Fussballuhr.PORT
' statt aus Tennis26_Settings.TextBoxValues.
Public Class VmixHttpSender
    Implements IVmixSender

    Private lastCommandValue As String = ""

    Public ReadOnly Property LastCommand As String Implements IVmixSender.LastCommand
        Get
            Return lastCommandValue
        End Get
    End Property

    Public Function Send(command As String) As String Implements IVmixSender.Send
        Dim url As String = "http://" + Fussballuhr.IP + ":" + Fussballuhr.PORT + "/API/?" + command
        lastCommandValue = url

        Try
            Dim cookieJar As New Net.CookieContainer()
            Dim hwrequest As Net.HttpWebRequest = Net.WebRequest.Create(url)
            hwrequest.CookieContainer = cookieJar
            hwrequest.Accept = "*/*"
            hwrequest.AllowAutoRedirect = True
            hwrequest.UserAgent = "http_requester/0.1"
            hwrequest.Method = "GET"
            ' 30ms (statt 3000) wäre ein Tippfehler-Erbe aus Tennis26/VmixHttpSender.vb - eine
            ' derart kurze Frist lässt so gut wie jeden echten Request (selbst auf localhost)
            ' als Timeout fehlschlagen, bevor vMix überhaupt antworten kann.
            hwrequest.Timeout = 3000

            Dim hwresponse As Net.HttpWebResponse = hwrequest.GetResponse()
            Dim responseData As String = ""
            If hwresponse.StatusCode = Net.HttpStatusCode.OK Then
                Dim responseStream As New IO.StreamReader(hwresponse.GetResponseStream())
                responseData = responseStream.ReadToEnd()
            End If
            hwresponse.Close()
            Return responseData
        Catch ex As Exception
            Return "Exception Error in VTX (vMix running?): " & ex.Message
        End Try
    End Function

End Class
