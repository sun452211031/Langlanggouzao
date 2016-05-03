using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.IO;
using UnityEngine.UI;
public class Photograph : MonoBehaviour, IPointerClickHandler
{
    private bool isSucceed = true;
    public manager findmanager;
    private string path;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (isSucceed)
        {
            StartCoroutine("GetCapture");
        }
    }
    private void GetCapture()//屏幕截图
    {
        isSucceed = false;
        try
        {
            int width = (int)Mathf.Round(Screen.height * 1.37f);
            int height = Screen.height;
            Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);
            tex.ReadPixels(new Rect(0, 0, width, height), 0, 0, true);
            byte[] imagebytes = tex.EncodeToJPG();
            tex.Compress(false);
            tex.Apply();
            findmanager.GetCapturetexture[Num.GetCapturetextureNum] = tex;

            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            {
                path = Application.persistentDataPath + Num.GetCapturetextureNum + ".jpg";
                string origin = path;

                string destination = "/mnt/sdcard/DCIM";
                if (!Directory.Exists(destination))
                {
                    Directory.CreateDirectory(destination);
                }

                destination = destination + "/" + "langlanggouzaoImage" + Num.GetCapturetextureNum + ".jpg";

                if (System.IO.File.Exists(origin))
                {
                    System.IO.File.Move(origin, destination);
                }
                path = destination;
            }
            else if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                path = Application.dataPath;
            }
            File.WriteAllBytes(path, imagebytes);
            findmanager.GetCapturetexture[Num.GetCapturetextureNum] = tex;
            findmanager.photosSet();

            if (Num.GetCapturetextureNum < 8)
            {
                Num.GetCapturetextureNum += 1;
            }
            else
            {
                Num.GetCapturetextureNum = 0;
            }
        }
        catch (System.Exception e)
        {
            Debug.Log("ScreenCaptrueError:" + e);
        }
        isSucceed = true;
    }
    //private void GetCapture()//屏幕截图
    //{
    //    isSucceed = false;
    //    try
    //    {
    //        int width = Screen.width;
    //        int height = Screen.height;
    //        Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);
    //        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0, true);
    //        byte[] imagebytes = tex.EncodeToJPG();
    //        tex.Compress(false);
    //        tex.Apply();
    //        findmanager.GetCapturetexture[Num.GetCapturetextureNum] = tex;
    //        findmanager.photosSet();
    //        path = Application.persistentDataPath + "/" + Num.GetCapturetextureNum+".jpg";
    //        File.WriteAllBytes(path, imagebytes);
    //        if (Num.GetCapturetextureNum < 8)
    //        {
    //            Num.GetCapturetextureNum += 1;
    //        }
    //        else
    //        {
    //            Num.GetCapturetextureNum = 0;
    //        }
    //    }
    //    catch (System.Exception e)
    //    {
    //        Debug.Log("ScreenCaptrueError:" + e);
    //    }
    //    isSucceed = true;
    //}
}
