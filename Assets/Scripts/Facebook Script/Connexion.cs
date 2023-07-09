using UnityEngine;
using Facebook.Unity;

public class Connexion : MonoBehaviour
{
    private void Awake()
    {
        if (!FB.IsInitialized)
        {
            FB.Init(InitCallback);
        }
        else
        {
            FB.ActivateApp();
        }
    }

    private void InitCallback()
    {
        if (FB.IsInitialized)
        {
            FB.ActivateApp();
        }
        else
        {
            Debug.LogError("Failed to initialize the Facebook SDK");
        }
    }

    public void FacebookLogin()
    {
        if (!FB.IsLoggedIn)
        {
            FB.LogInWithReadPermissions(callback: OnLoginCallback);
        }
        else
        {
            Debug.Log("User is already logged in to Facebook");
        }
    }

    private void OnLoginCallback(ILoginResult result)
    {
        if (result.Error != null)
        {
            Debug.LogError("Facebook login failed: " + result.Error);
        }
        else if (result.Cancelled)
        {
            Debug.Log("Facebook login cancelled");
        }
        else
        {
            Debug.Log("Facebook login successful");
            // Insérez ici le code à exécuter après la connexion réussie à Facebook
        }
    }
}