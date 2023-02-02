using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootSingularity : MonoBehaviour
{
    public float shootRadius;

    Orientation orientation;
    Vector2 crossheir;
    Vector2 center;

    private void Start()
    {
        orientation = GetComponent<Orientation>();
        center = new Vector2(Screen.width, Screen.height);
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
            crossheir = Input.mousePosition;

            Debug.Log(crossheir);
        } else
        {
            
        }

    }
}
