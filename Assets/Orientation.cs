using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Orientation : MonoBehaviour
{

    public float rotateSpeed;
    public float shootingFOV;

    float rotateY;
    float rotateX;

    private float leftBorder;
    private float rightBorder;

    // Start is called before the first frame update
    void Start()
    {
        leftBorder = (float) Screen.width / 2 - (shootingFOV * Screen.width / 100) / 2;
        rightBorder = (float)Screen.width / 2 + (shootingFOV * Screen.width / 100) / 2;

        Debug.Log(leftBorder);
        Debug.Log(rightBorder);
    }

    // Update is called once per frame
    void Update()
    {
        rotateY += Input.GetAxis("Mouse X") * rotateSpeed;
        rotateX += Input.GetAxis("Mouse Y") * rotateSpeed;
        if (Input.GetButtonDown("Fire1"))
        {
            Vector3 mousePos = Input.mousePosition;
            {
                Debug.Log(mousePos.x);
                Debug.Log(mousePos.y);
            }
        }
    }

    private void FixedUpdate()
    {
        if (!isInShootingZone() && isTurning())
        {
            this.transform.eulerAngles = new Vector3(
                0,
                this.transform.rotation.eulerAngles.y + rotateY,
                0
            );

        }
        

        rotateY = 0;
        rotateX = 0;
    }

    private bool isInShootingZone()
    {
        Vector3 mousePos = Input.mousePosition;
        return ((mousePos.x < rightBorder) &&
            (mousePos.x > leftBorder));
    }

    private bool isTurning()
    {
        Vector3 mousePos = Input.mousePosition;
        return (mousePos.x > rightBorder && rotateY > 0) ||
            (mousePos.x < leftBorder && rotateY < 0);
    }

    public float getRightBorder()
    {
        return rightBorder;
    }

    public float getLeftBorder()
    {
        return leftBorder;
    }

}
