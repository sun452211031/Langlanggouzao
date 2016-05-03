using UnityEngine;
using System.Collections;
using System;
public class modelControl : MonoBehaviour
{
    DateTime t1, t2;
    private Vector2 oldPosition1;
    private Vector2 oldPosition2;
    public Animation MainObj;
    public GameObject Partical;
    public GameObject rotate;
    public float StartAnimationtime = 0;

    IEnumerator Start()
    {
        t1 = DateTime.Now;
        MainObj.Play("Take 001");
        yield return new WaitForSeconds(StartAnimationtime);
        if (StartAnimationtime != 0)
        {
            MainObj.Play("Take 002");
        }
    }
    void Update()
    {
        if (Application.isEditor)
        {
            if (Input.GetMouseButton(0))
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

        if (!Application.isEditor && MainObj.gameObject.activeSelf == true)
        {
            if (Input.touchCount == 1 && !MainObj.animation.IsPlaying("Take 003"))
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
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    t2 = DateTime.Now;
                    if (t2 - t1 < new TimeSpan(0, 0, 0, 0, 500))
                    {
                        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                        RaycastHit thisRaycastHit;
                        if (Physics.Raycast(ray, out thisRaycastHit))
                        {
                            var HitObjName = thisRaycastHit.collider.gameObject.name;
                            if (HitObjName == "model" && !MainObj.IsPlaying("Take 003"))
                            {
                                this.transform.rotation = Quaternion.Euler(Vector3.zero);
                                rotate.transform.rotation = Quaternion.Euler(Vector3.zero);
                                MainObj.Play("Take 003");
                                if (!MainObj.audio.isPlaying)
                                {
                                    MainObj.audio.Play();
                                    Invoke("Play002", MainObj["Take 003"].length);
                                }
                                if (Partical != null)
                                {
                                    Partical.SetActive(true);
                                }
                            }
                        }
                    }
                    t1 = t2;
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

    private void Play002()
    {
        MainObj.Play("Take 002");
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
