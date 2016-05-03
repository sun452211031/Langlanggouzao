using UnityEngine;
using System.Collections;
using System;
public class modelControl_Photo : MonoBehaviour
{

    private Vector2 oldPosition1;
    private Vector2 oldPosition2;
    public GameObject rotate;

    void Update()
    {
        if (!Application.isEditor)
        {
            if (Input.touchCount == 1 )
            {
                if (Input.GetTouch(0).phase == TouchPhase.Moved)//左右滑动
                {
                    if (Input.GetAxis("Mouse X") > 0)
                    {
                        rotate.transform.Rotate(0, -250 * Time.deltaTime, 0);
                    }
                    else if (Input.GetAxis("Mouse X") < 0)
                    {
                        rotate.transform.Rotate(0, 250 * Time.deltaTime, 0);
                    }

                    if (Input.GetAxis("Mouse Y") > 0)
                    {
                        this.transform.Rotate(150 * Time.deltaTime, 0, 0);
                    }
                    else if (Input.GetAxis("Mouse Y") < 0)
                    {
                        this.transform.Rotate(-150 * Time.deltaTime, 0, 0);

                    }
                }

            }
            else if (Input.touchCount > 1)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(1).phase == TouchPhase.Moved)//手势缩放
                {
                    var tempPosition1 = Input.GetTouch(0).position;
                    var tempPosition2 = Input.GetTouch(1).position;
                    if (isEnlarge(oldPosition1, oldPosition2, tempPosition1, tempPosition2))
                    {
                        var thisScale = this.transform.localScale;
                        if (thisScale.x < 1.5f)
                        {
                            this.transform.localScale = new Vector3(thisScale.x + 0.01f, thisScale.y + 0.01f, thisScale.z + 0.01f);
                        }
                    }
                    else
                    {
                        var thisScale = this.transform.localScale;
                        if (thisScale.x > 0.5f)
                        {
                            this.transform.localScale = new Vector3(thisScale.x - 0.01f, thisScale.y - 0.01f, thisScale.z - 0.01f);
                        }
                    }
                    oldPosition1 = tempPosition1;
                    oldPosition2 = tempPosition2;
                }
            }
        }
    }

    private bool isEnlarge(Vector2 oP1, Vector2 oP2, Vector2 nP1, Vector2 nP2)
    {
        var leng1 = Mathf.Sqrt((oP1.x - oP2.x) * (oP1.x - oP2.x) + (oP1.y - oP2.y) * (oP1.y - oP2.y));
        var leng2 = Mathf.Sqrt((nP1.x - nP2.x) * (nP1.x - nP2.x) + (nP1.y - nP2.y) * (nP1.y - nP2.y));
        if (leng1 < leng2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
