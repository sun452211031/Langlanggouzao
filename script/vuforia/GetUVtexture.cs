using UnityEngine;
using System.Collections;
using Vuforia;
public class GetUVtexture : MonoBehaviour
{
    public GameObject upButton;
    private int limit = 10000;
    public Renderer[] ModelSet;
    public GameObject ModelShow;
    public GameObject Canvas;

    public GameObject ImageBox;
    public GameObject False;
    public GameObject Ture;

    public Transform Pos1;
    public Transform Pos2;
    public Transform Pos3;
    public Transform Pos4;

    public Transform Cam;

    bool bool_3;
    bool bool_4;

    private bool CanCaptureImage = true;
    void Update()
    {
        if (CanCaptureImage == true)
        {
            if (manager.PosIsAllRender >= 4)
            {
                bool bool_1 = Camera.main.WorldToScreenPoint(Pos1.transform.position).y < Screen.height;
                bool bool_2 = Camera.main.WorldToScreenPoint(Pos2.transform.position).y < Screen.height;
                bool bool_3 = Camera.main.WorldToScreenPoint(Pos3.transform.position).y > 0;
                bool bool_4 = Camera.main.WorldToScreenPoint(Pos4.transform.position).y > 0;
                bool bool_5 = Mathf.Pow(((Vector3.Distance(Pos1.position, Cam.position) - Vector3.Distance(Pos3.position, Cam.position))), 2) < limit;
                bool bool_6 = Mathf.Pow(((Vector3.Distance(Pos2.position, Cam.position) - Vector3.Distance(Pos4.position, Cam.position))), 2) < limit;
                if (bool_5 == true && bool_6 == true)
                {
                    if (bool_1 == true && bool_2 == true && bool_3 == true && bool_4 == true)
                    {
                        False.SetActive(false);
                        Ture.SetActive(true);
                        StartCoroutine("CaptureImage");
                        CanCaptureImage = false;
                    }
                }
            }
        }
    }
    IEnumerator CaptureImage()
    {
        yield return new WaitForSeconds(0.2f);
        Canvas.SetActive(false);
        yield return new WaitForEndOfFrame();
        int width_screen = Screen.width;
        int height_screen = Screen.height;
        Texture2D captureTex = new Texture2D(width_screen, height_screen, TextureFormat.RGB24, false);
        captureTex.ReadPixels(new Rect(0, 0, width_screen, height_screen), 0, 0, true);
        captureTex.Apply();

        Vector2 thisPos1 = Camera.main.WorldToScreenPoint(Pos1.transform.position);
        Vector2 thisPos2 = Camera.main.WorldToScreenPoint(Pos2.transform.position);
        Vector2 thisPos3 = Camera.main.WorldToScreenPoint(Pos3.transform.position);
        Vector2 thisPos4 = Camera.main.WorldToScreenPoint(Pos4.transform.position);

        int UVsize = 1000;

        Texture2D uvTex = new Texture2D(UVsize, UVsize, TextureFormat.RGB24, false);

        for (int f = 0; f < UVsize; f += 1)
        {
            float UVcount = (float)f / (float)UVsize; ;
            Vector2 P1toP3 = Vector2.Lerp(thisPos3, thisPos1, UVcount);
            Vector2 P2toP4 = Vector2.Lerp(thisPos4, thisPos2, UVcount);
            for (int f2 = 0; f2 < UVsize; f2 += 1)
            {
                float UVcount2 = (float)f2 / (float)UVsize;
                Vector2 P1P3toP2P4 = Vector2.Lerp(P1toP3, P2toP4, UVcount2);
                Color getPixel = captureTex.GetPixel((int)P1P3toP2P4.x, (int)P1P3toP2P4.y);
                uvTex.SetPixel(f2, f, getPixel);
            }
        }
        uvTex.Apply();
        for (int i = 0; i < ModelSet.Length; i++)
        {
            ModelSet[i].sharedMaterial.mainTexture = uvTex;
        }
        ModelShow.SetActive(true);
        Canvas.SetActive(true);
        Destroy(ImageBox);
        upButton.SetActive(true);
        Destroy(gameObject);
    }
}
