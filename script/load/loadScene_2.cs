using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class loadScene_2 : MonoBehaviour, IPointerClickHandler
{
    public GameObject Progress;
    public void OnPointerClick(PointerEventData eventData)
    {
        Progress.SetActive(true);
        Application.LoadLevel(gameObject.name);
    }
}
