using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairMovement : MonoBehaviour
{
    public ShootSingularity shooter;

    private Vector2 crosshairPos;
    private RectTransform crosshairImg;
    private void OnEnable()
    {
        crosshairImg= GetComponent<RectTransform>();
    }
    void Update()
    {
        crosshairPos = new Vector2(shooter.GetCrosshair().x - shooter.GetCenter().x, 
            shooter.GetCrosshair().y - shooter.GetCenter().y);
        crosshairImg.anchoredPosition = crosshairPos;
    }
}
