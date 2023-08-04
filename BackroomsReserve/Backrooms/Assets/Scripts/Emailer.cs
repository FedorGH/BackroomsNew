using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net.Security;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using Unity.Services.Authentication;
using Unity.Services.Core;

public class Emailer : MonoBehaviour
{  
    public static void NewUser()
    {
        MailMessage NewUser = new MailMessage();
        NewUser.From = new MailAddress("ttbauthenticationmanager@gmail.com");
        NewUser.To.Add("BackroomsNew2023@gmail.com");

        SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
        smtpServer.Port = 587;//GIVE CORRECT PORT HERE
        NewUser.Subject = "NewUser!";
        NewUser.Body = $"New Player! PlayerID: {AuthenticationService.Instance.PlayerId} _______ AcessToken: {AuthenticationService.Instance.AccessToken}";
        smtpServer.Credentials = new System.Net.NetworkCredential("ttbauthenticationmanager@gmail.com", "vhewqdqodwisgdai") as ICredentialsByHost;
        smtpServer.EnableSsl = true;
        ServicePointManager.ServerCertificateValidationCallback =
        delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        { return true; };
        smtpServer.Send(NewUser);
        //smtpServer.SendAsync(mail)
        Debug.Log("NewUserDetected");
    }

    public static void SignInFailed(RequestFailedException err)
    {
        MailMessage Fail = new MailMessage();
        Fail.From = new MailAddress("ttbauthenticationmanager@gmail.com");
        Fail.To.Add("BackroomsNew2023@gmail.com");

        SmtpClient smtpServerA = new SmtpClient("smtp.gmail.com");
        smtpServerA.Port = 587;//GIVE CORRECT PORT HERE
        Fail.Subject = "SignInFailed";
        Fail.Body = $"SignedFailed! PlayerID: Failed, ErrorReason:";
        smtpServerA.Credentials = new System.Net.NetworkCredential("ttbauthenticationmanager@gmail.com", "vhewqdqodwisgdai") as ICredentialsByHost;
        smtpServerA.EnableSsl = true;
        ServicePointManager.ServerCertificateValidationCallback =
        delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        { return true; };
        smtpServerA.Send(Fail);
        //smtpServer.SendAsync(mail)
        Debug.Log("ErrorDetected");
    }

    public static void SignedOut()
    {
        MailMessage SignOut = new MailMessage();
        SignOut.From = new MailAddress("ttbauthenticationmanager@gmail.com");
        SignOut.To.Add("BackroomsNew2023@gmail.com");

        SmtpClient smtpServerB = new SmtpClient("smtp.gmail.com");
        smtpServerB.Port = 587;//GIVE CORRECT PORT HERE
        SignOut.Subject = "SignedOut";
        SignOut.Body = $"SignedOut! PlayerID: {AuthenticationService.Instance.PlayerId}";
        smtpServerB.Credentials = new System.Net.NetworkCredential("ttbauthenticationmanager@gmail.com", "vhewqdqodwisgdai") as ICredentialsByHost;
        smtpServerB.EnableSsl = true;
        ServicePointManager.ServerCertificateValidationCallback =
        delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        { return true; };
        smtpServerB.Send(SignOut);
        //smtpServer.SendAsync(mail)
        Debug.Log("Outed");
    }

    public static void TimeOut()
    {
        MailMessage TimedOut = new MailMessage();
        TimedOut.From = new MailAddress("ttbauthenticationmanager@gmail.com");
        TimedOut.To.Add("BackroomsNew2023@gmail.com");

        SmtpClient smtpServerD = new SmtpClient("smtp.gmail.com");
        smtpServerD.Port = 587;//GIVE CORRECT PORT HERE
        TimedOut.Subject = "TimeOut";
        TimedOut.Body = $"Somebody TimedOut PlayerID: {AuthenticationService.Instance.PlayerId}";
        smtpServerD.Credentials = new System.Net.NetworkCredential("ttbauthenticationmanager@gmail.com", "vhewqdqodwisgdai") as ICredentialsByHost;
        smtpServerD.EnableSsl = true;
        ServicePointManager.ServerCertificateValidationCallback =
        delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        { return true; };
        smtpServerD.Send(TimedOut);
        //smtpServer.SendAsync(mail)
        Debug.Log("TimeOut");
    }
}
