using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCube : MonoBehaviour
{
    public GameObject target;

    Vector2 firstClickPosition;
    Vector2 secondClickPosition;
    Vector2 currentSwipe;

    Vector3 initMousePosition;
    Vector3 mouseDelta;
    
    float speed = 100f;

    void Start()
    {
        currentSwipe = new Vector2(0, 0);
    }

    void Update()
    {
        Swipe();
        Drag();
    }

    void Swipe()
    {
        if(Input.GetMouseButtonDown(1))
        {
            firstClickPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        if(Input.GetMouseButtonUp(1))
        {
            secondClickPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            currentSwipe = new Vector2(firstClickPosition.x - secondClickPosition.x, firstClickPosition.y - secondClickPosition.y);
            currentSwipe.Normalize();
            determineSwipe();
        }
    }

    void Drag()
    {
        if(Input.GetMouseButton(1))
        {
            mouseDelta = Input.mousePosition - initMousePosition;
            mouseDelta *= 1f;
            transform.rotation *= Quaternion.Euler(mouseDelta.y, -mouseDelta.x, 0);
        }
        else
        {
            if(transform.rotation != target.transform.rotation)
            {
                var theta = speed * Time.deltaTime;
                transform.rotation = Quaternion.RotateTowards(transform.rotation, target.transform.rotation, theta);
            }
        }
        initMousePosition = Input.mousePosition;
    }

    void determineSwipe()
    {
        if(currentSwipe.y < 0.5f && currentSwipe.y > -0.5f)
        {
            if(currentSwipe.x < 0)
            {
                target.transform.Rotate(0, -90, 0, Space.World);
            }
            else if(currentSwipe.x > 0)
            {
                target.transform.Rotate(0, 90, 0, Space.World);
            }
        }
        else
        {
            if(currentSwipe.y < 0)
            {
                if(currentSwipe.x < 0)
                {
                    target.transform.Rotate(90, 0, 0, Space.World);
                }
                else if(currentSwipe.x > 0)
                {
                    target.transform.Rotate(0, 0, 90, Space.World);
                }
            }
            else if(currentSwipe.y > 0)
            {
                if(currentSwipe.x > 0)
                {
                    target.transform.Rotate(-90, 0, 0, Space.World);
                }
                else if(currentSwipe.x < 0)
                {
                    target.transform.Rotate(0, 0, -90, Space.World);
                }
            }
        }
    }
}
