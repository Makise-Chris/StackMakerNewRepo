using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileInput : MonoBehaviour
{
    public static MobileInput instance;

    public bool swipeLeft, swipeRight, swipeUp, swipeDown;
    public Vector2 swipeDelta, startTouch;
    [SerializeField] private float deadZone = 50;
    private float sensitiveThreshold = 0.01f;

    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            startTouch = swipeDelta = Vector2.zero;
        }

        swipeDelta = Vector2.zero;
        if ((startTouch - Vector2.zero).sqrMagnitude > sensitiveThreshold)
        {
            if (Input.touches.Length != 0)
            {
                swipeDelta = Input.touches[0].position - startTouch;
            }
            else if (Input.GetMouseButton(0))
            {
                swipeDelta = (Vector2)Input.mousePosition - startTouch;
            }
        }

        if (swipeDelta.magnitude > deadZone && !swipeLeft && !swipeRight && !swipeUp && !swipeDown)
        {
            float x = swipeDelta.x;
            float y = swipeDelta.y;
            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                if (x < 0)
                {
                    if (!PlayerMovement.instance.lockLeft)
                    {
                        swipeLeft = true;
                    }
                    else swipeLeft = false;
                    swipeRight = swipeUp = swipeDown = false;
                }
                else
                {
                    if (!PlayerMovement.instance.lockRight)
                    {
                        swipeRight = true;
                    }
                    else swipeRight = false;
                    swipeLeft = swipeUp = swipeDown = false;
                }
            }
            else
            {
                if (y < 0)
                {
                    if (!PlayerMovement.instance.lockDown)
                    {
                        swipeDown = true;
                    }
                    else swipeDown = false;
                    swipeLeft = swipeRight = swipeUp = false;
                }
                else
                {
                    if (!PlayerMovement.instance.lockUp)
                    {
                        swipeUp = true;
                    }
                    else swipeUp = false;
                    swipeLeft = swipeRight = swipeDown = false;
                }
            }

            startTouch = swipeDelta = Vector2.zero;
        }
    }
}
