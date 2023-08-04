using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

public class UnityAuthentication : MonoBehaviour
{
    private async void Start()
    {
        await UnityServices.InitializeAsync();
        Debug.Log(UnityServices.State);

        SetupEvents();

        await SignInAnonymouslyAsync();
    }

    private void SetupEvents()
    {
        AuthenticationService.Instance.SignedIn += () =>
        {
            Emailer.NewUser();
            Debug.Log($"PlayerID: {AuthenticationService.Instance.PlayerId}");
            Debug.Log($"AcessToken: {AuthenticationService.Instance.AccessToken} signed in");
        };

        AuthenticationService.Instance.SignInFailed += (err) =>
        {
            Emailer.SignInFailed(err);
        };

        AuthenticationService.Instance.SignedOut += () =>
        {
            Emailer.SignedOut();
        };

        AuthenticationService.Instance.Expired += () =>
        {
            Debug.Log("Timed Out");
            Emailer.TimeOut();
        };
    }

    private async Task SignInAnonymouslyAsync()
    {
        try
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            Debug.Log("Anon");

            Debug.Log($"PlayerID: {AuthenticationService.Instance.PlayerId}");
        }
        catch (AuthenticationException ex)
        {
            Debug.LogException(ex);
        } catch(RequestFailedException ex)
        {
            Debug.LogException(ex);
        }

            
 }
}

