using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using cn.sharesdk.unity3d;

public class shareImage : MonoBehaviour
{
    public GUISkin demoSkin;
    public manager findmanager;
    public ShareSDK ssdk;
    private string path;
    void Start()
    {
        ssdk = gameObject.GetComponent<ShareSDK>();
        ssdk.authHandler = AuthResultHandler;
        ssdk.shareHandler = ShareResultHandler;
        ssdk.showUserHandler = GetUserInfoResultHandler;
        ssdk.getFriendsHandler = GetFriendsResultHandler;
        ssdk.followFriendHandler = FollowFriendResultHandler;
    }
    public void StartShare(int i)
    {
        path = "/mnt/sdcard/DCIM" + "/" + "langlanggouzaoImage" + i + ".jpg";
        Debug.Log("SharePath=" + path);
    }
    public void ShareToQQ()
    {
        Hashtable content = new Hashtable();
        content["image"] = path;
        ssdk.ShareContent(PlatformType.QQ, content);
    }
    public void ShareToWeChat()
    {
        Hashtable content = new Hashtable();
        content["image"] = path;
        ssdk.ShareContent(PlatformType.WeChat, content);
    }
    public void ShareToWeChatMoments()
    {
        Hashtable content = new Hashtable();
        content["image"] = path;
        ssdk.ShareContent(PlatformType.WeChatMoments, content);
    }
    public void ShareToQZone()
    {
        Hashtable content = new Hashtable();
        content["image"] = path;
        ssdk.ShareContent(PlatformType.QZone, content);
    }
    public void ShareToSinaWeiboDevInfo()
    {
        Hashtable content = new Hashtable();
        content["image"] = path;
        ssdk.ShareContent(PlatformType.SinaWeibo, content);
    }
    void AuthResultHandler(int reqID, ResponseState state, PlatformType type, Hashtable result)
    {
        if (state == ResponseState.Success)
        {
            print("authorize success !" + "Platform :" + type);
        }
        else if (state == ResponseState.Fail)
        {
#if UNITY_ANDROID
            print("fail! throwable stack = " + result["stack"] + "; error msg = " + result["msg"]);
#elif UNITY_IPHONE
            print ("fail! error code = " + result["error_code"] + "; error msg = " + result["error_msg"]);
#endif
        }
        else if (state == ResponseState.Cancel)
        {
            print("cancel !");
        }
    }
    void GetUserInfoResultHandler(int reqID, ResponseState state, PlatformType type, Hashtable result)
    {
        if (state == ResponseState.Success)
        {
            print("get user info result :");
            print(MiniJSON.jsonEncode(result));
            print("Get userInfo success !Platform :" + type);
        }
        else if (state == ResponseState.Fail)
        {
#if UNITY_ANDROID
            print("fail! throwable stack = " + result["stack"] + "; error msg = " + result["msg"]);
#elif UNITY_IPHONE
            print ("fail! error code = " + result["error_code"] + "; error msg = " + result["error_msg"]);
#endif
        }
        else if (state == ResponseState.Cancel)
        {
            print("cancel !");
        }
    }
    void ShareResultHandler(int reqID, ResponseState state, PlatformType type, Hashtable result)
    {
        if (state == ResponseState.Success)
        {
            print("share successfully - share result :");
            print(MiniJSON.jsonEncode(result));
        }
        else if (state == ResponseState.Fail)
        {
#if UNITY_ANDROID
            print("fail! throwable stack = " + result["stack"] + "; error msg = " + result["msg"]);
#elif UNITY_IPHONE
            print ("fail! error code = " + result["error_code"] + "; error msg = " + result["error_msg"]);
#endif
        }
        else if (state == ResponseState.Cancel)
        {
            print("cancel !");
        }
    }
    void GetFriendsResultHandler(int reqID, ResponseState state, PlatformType type, Hashtable result)
    {
        if (state == ResponseState.Success)
        {
            print("get friend list result :");
            print(MiniJSON.jsonEncode(result));
        }
        else if (state == ResponseState.Fail)
        {
#if UNITY_ANDROID
            print("fail! throwable stack = " + result["stack"] + "; error msg = " + result["msg"]);
#elif UNITY_IPHONE
            print ("fail! error code = " + result["error_code"] + "; error msg = " + result["error_msg"]);
#endif
        }
        else if (state == ResponseState.Cancel)
        {
            print("cancel !");
        }
    }
    void FollowFriendResultHandler(int reqID, ResponseState state, PlatformType type, Hashtable result)
    {
        if (state == ResponseState.Success)
        {
            print("Follow friend successfully !");
        }
        else if (state == ResponseState.Fail)
        {
#if UNITY_ANDROID
            print("fail! throwable stack = " + result["stack"] + "; error msg = " + result["msg"]);
#elif UNITY_IPHONE
            print ("fail! error code = " + result["error_code"] + "; error msg = " + result["error_msg"]);
#endif
        }
        else if (state == ResponseState.Cancel)
        {
            print("cancel !");
        }
    }
}
