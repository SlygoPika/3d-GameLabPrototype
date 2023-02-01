using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootSingularity : MonoBehaviour
{

    public float FOVangle;
    public float shootRadius;

    Orientation orientation;


    private void Start()
    {
        orientation = GetComponent<Orientation>();
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

    }
}
