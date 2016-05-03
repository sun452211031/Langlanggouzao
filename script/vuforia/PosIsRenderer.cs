using UnityEngine;
using System.Collections;

public class PosIsRenderer : MonoBehaviour
{
    void OnBecameVisible()
    {
        manager.PosIsAllRender += 1;
    }
    void OnBecameInvisible()
    {
        manager.PosIsAllRender -= 1;
    }
}
