using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class MD5Manager : MonoBehavioer
{
#if UNITY_EDITOR || UNITY_IOS

#elif UNITY_ANDROID
    public static string MD5 = GetSignatureMD5Hash();
#endif
    /// <summary>
    /// 获取Android APK的MD5签名指纹
    /// </summary>
    /// <returns>The signature M d5 hash.</returns>
    public string GetSignatureMD5Hash()
    {
        //Debug.Log ("GetSignatureMD5Hash");
        var player = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        var activity = player.GetStatic<AndroidJavaObject>("currentActivity");
        var PackageManager = new AndroidJavaClass("android.content.pm.PackageManager");

        var packageName = activity.Call<string>("getPackageName");

        var GET_SIGNATURES = PackageManager.GetStatic<int>("GET_SIGNATURES");
        var packageManager = activity.Call<AndroidJavaObject>("getPackageManager");
        var packageInfo = packageManager.Call<AndroidJavaObject>("getPackageInfo", packageName, GET_SIGNATURES);
        var signatures = packageInfo.Get<AndroidJavaObject[]>("signatures");
        if(signatures != null && signatures.Length > 0)
        {
            byte[] bytes = signatures[0].Call<byte[]>("toByteArray");

            var md5String = GetMD5Hash(bytes);
            md5String = md5String.ToUpper ();

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < md5String.Length; ++i) {
                if(i>0 && i %2 ==0)
                {
                    sb.Append(':');
                }
                sb.Append (md5String[i]);
            }

            return sb.ToString ();

        }

        return null;
    }

    private string GetMD5Hash(byte[] bytedata)
    {
        //Debug.Log ("GetMD5Hash");
        try
        { 
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] retVal = md5.ComputeHash(bytedata);



            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < retVal.Length; i++)
            {
                sb.Append(retVal[i].ToString("x2"));
            }
            return sb.ToString();
        }
        catch (Exception ex)
        {
            throw new Exception("GetMD5Hash() fail,error:" + ex.Message);
        }
    }
}
