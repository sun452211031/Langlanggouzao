using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class loadScene : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Application.LoadLevel(gameObject.name);
    }
}
