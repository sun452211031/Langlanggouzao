using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class shareButton : MonoBehaviour, IPointerClickHandler
{
    public manager findmanager;
    public string message;
    public GameObject share;
    public RawImage shareImage;
    private RawImage thisRawImage;
    void Start()
    {
        thisRawImage = gameObject.GetComponent<RawImage>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (thisRawImage.texture.name != "xiangkuang")
        {
            shareImage.texture = thisRawImage.texture;
            share.SetActive(true);
            findmanager.SendMessage(message, int.Parse(this.name), SendMessageOptions.DontRequireReceiver);
        }
    }
}
