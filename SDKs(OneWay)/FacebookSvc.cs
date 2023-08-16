#if SDKBUNDLE_ENABLED
using Facebook.Unity;
#endif
using System.Collections.Generic;
using UnityEngine;

public class FacebookSvc
{
#if SDKBUNDLE_ENABLED
    public void Init()
    {
        if (!FB.IsInitialized)
        {
            // Initialize the Facebook SDK
            FB.Init(InitCallback, OnHideUnity);
        }
        else
        {
            // Already initialized, signal an app activation App Event
            FB.ActivateApp();
        }
    }

    private void InitCallback()
    {
        if (FB.IsInitialized)
        {
            // Signal an app activation App Event
            FB.ActivateApp();
            // Continue with Facebook SDK
            // ...
        }
        else
        {
            Debug.Log("Failed to Initialize the Facebook SDK");
        }
    }

    private void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            // Pause the game - we will need to hide
            Time.timeScale = 0;
        }
        else
        {
            // Resume the game - we're getting focus again
            Time.timeScale = 1;
        }
    }

    /// <summary>
    /// 自定义事件
    /// </summary>
    /// <param name="_name"></param>
    /// <param name="_parameters"></param>
    public void CustomEvent(string _name, Dictionary<string, object> _parameters)
    {
        FB.LogAppEvent(_name, null, _parameters);
    }
#endif
}
