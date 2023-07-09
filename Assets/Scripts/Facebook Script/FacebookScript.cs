using UnityEngine;
using UnityEngine.UI;
using Facebook.Unity;
using System.Collections.Generic;

public class FacebookScript : MonoBehaviour
{
    public Text FriendsText;

    private void Awake()
    {
        if (!FB.IsInitialized)
        {
            FB.Init(InitCallback, OnHideUnity);
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

    private void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    #region Login / Logout
    public void FacebookLogin()
    {
        var permissions = new string[] { "public_profile", "email", "user_friends" };
        FB.LogInWithReadPermissions(permissions, LoginCallback);
    }

    private void LoginCallback(ILoginResult result)
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
        }
    }

    public void FacebookLogout()
    {
        FB.LogOut();
    }
    #endregion

    public void FacebookShare()
    {
        if (FB.IsLoggedIn)
        {
            FB.ShareLink(new System.Uri("https://resocoder.com"), "Check it out!",
                "Je suis un test",
                new System.Uri("https://resocoder.com/wp-content/uploads/2017/01/logoRound512.png"));
        }
        else
        {
            Debug.Log("User is not logged in to Facebook");
        }
    }

    #region Inviting
    public void FacebookGameRequest()
    {
        if (FB.IsLoggedIn)
        {
            FB.AppRequest("Hey! Come and play this awesome game!", title: "Reso Coder Tutorial");
        }
        else
        {
            Debug.Log("User is not logged in to Facebook");
        }
    }
    #endregion

    public void GetFriendsPlayingThisGame()
    {
        if (FB.IsLoggedIn)
        {
            string query = "/me/friends";
            FB.API(query, HttpMethod.GET, result =>
            {
                var dictionary = (Dictionary<string, object>)Facebook.MiniJSON.Json.Deserialize(result.RawResult);
                var friendsList = (List<object>)dictionary["data"];
                FriendsText.text = string.Empty;
                foreach (var dict in friendsList)
                {
                    FriendsText.text += ((Dictionary<string, object>)dict)["name"];
                }
            });
        }
        else
        {
            Debug.Log("User is not logged in to Facebook");
        }
    }
}
