using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOVRectangle : MonoBehaviour
{
    private RectTransform rectangle;
    public Orientation orientation;
    private const int WIDTH = 1920;
    private const int HEIGHT = 1080;
    private void OnEnable()
    {
        rectangle = GetComponent<RectTransform>();
        float FOV = orientation.shootingFOV;

        rectangle.sizeDelta = new Vector2(FOV / 100 * WIDTH, FOV / 100 * HEIGHT);
    }
}
