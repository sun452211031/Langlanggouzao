using UnityEngine;
using System.Collections;

public class invokeClose : MonoBehaviour
{
    public float invokeTime;
    void OnEnable()
    {
        Invoke("Close", invokeTime);
    }
    void Close()
    {
        gameObject.SetActive(false);
    }
}
