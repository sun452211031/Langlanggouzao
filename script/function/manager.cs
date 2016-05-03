using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using Vuforia;
public class manager : MonoBehaviour
{
    public static int PosIsAllRender = 0;
    public Texture[] GetCapturetexture = new Texture[9];
    public RawImage[] Photos = new RawImage[9];
    public GameObject Progress;
    public Slider thisSlider;
    public Text showText;
    public GameObject getUVtexture;
    IEnumerator Start()
    {
        if (Num.GetCapturetextureIsTrue == false)
        {
            //CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
            yield return new WaitForSeconds(1);
            for (int i = 0; i < 9; i++)
            {
                string path = "/mnt/sdcard/DCIM" + "//" + "langlanggouzaoImage" + i + ".jpg";
                if (System.IO.File.Exists(path))
                {
                    byte[] imagebytes = File.ReadAllBytes(path);
                    Texture2D thistex = new Texture2D((int)Mathf.Round(Screen.height * 1.37f), Screen.height);
                    thistex.LoadImage(imagebytes);
                    Photos[i].texture = thistex;
                    thisSlider.value += 0.1f;
                    showText.text = "加载图片" + "langlanggouzaoImage" + i + ".jpg";
                }
                else
                {
                    break;
                }
                if (Num.GetCapturetextureNum < 8)
                {
                    Num.GetCapturetextureNum += 1;
                }
                else
                {
                    Num.GetCapturetextureNum = 0;
                }
                yield return new WaitForSeconds(0.1f);
            }
            thisSlider.value = 1;
            showText.text = "加载完成";

            Invoke("ProgressEnd", 1);
        }
        else
        {
            CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
            yield return new WaitForSeconds(1);
            for (int i = 0; i < 9; i++)
            {
                string path = "/mnt/sdcard/DCIM" + "//" + "langlanggouzaoImage" + i + ".jpg";
                if (System.IO.File.Exists(path))
                {
                    byte[] imagebytes = File.ReadAllBytes(path);
                    Texture2D thistex = new Texture2D((int)Mathf.Round(Screen.height * 1.37f), Screen.height);
                    thistex.LoadImage(imagebytes);
                    Photos[i].texture = thistex;
                    thisSlider.value += 0.1f;
                    showText.text = "加载图片" + "langlanggouzaoImage" + i + ".jpg";
                }
                else
                {
                    break;
                }
                yield return new WaitForSeconds(0.1f);
            }
            thisSlider.value = 1;
            showText.text = "加载完成";

            Invoke("ProgressEnd", 1);
        }
    }
    private void ProgressEnd()
    {
        Num.GetCapturetextureIsTrue = true;
        Destroy(Progress);
        getUVtexture.SetActive(true);
    }
    public void photosSet()
    {
        Photos[Num.GetCapturetextureNum].texture = GetCapturetexture[Num.GetCapturetextureNum];
    }
}
