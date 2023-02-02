using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootSingularity : MonoBehaviour
{
    public float shootRadius;

    Orientation orientation;
    Vector2 crosshair;
    Vector2 center;

    private void Start()
    {
        orientation = GetComponent<Orientation>();
        center = new Vector2(Screen.width / 2, Screen.height / 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Vector3 mousePos = Input.mousePosition;
            {
                Debug.Log(mousePos.x);
                Debug.Log(mousePos.y);
            }
        }
        if (orientation.isInShootingZone())
        {
            crosshair = Input.mousePosition;
        } else
        {
            SetCrosshairWithinBorders();
        }

    }

    void SetCrosshairWithinBorders()
    {
        if (orientation.isOutOfLeftBorder())
        {
            float yPos = (Input.mousePosition.y - center.y) *
                ((orientation.getLeftBorder() - center.x) / (Input.mousePosition.x - center.x)) + center.y;

            crosshair = new Vector2
            {
                x = orientation.getLeftBorder(),
                y = yPos
            };
        } 
        else
        {
            float yPos = (Input.mousePosition.y - center.y) *
                ((orientation.getRightBorder() - center.x) / (Input.mousePosition.x - center.x)) + center.y;

            crosshair = new Vector2
            {
                x = orientation.getRightBorder(),
                y = yPos
            };
        }
    }

    public Vector2 GetCrosshair()
    {
        return crosshair;
    }

    public Vector2 GetCenter() { return center; }
}
