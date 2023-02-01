using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform Player;
    public float yDelta;
    public float radius;

    Transform Cam;
    float angleY;

    // Start is called before the first frame update
    void Start()
    {
        Cam = GetComponent<Transform>();
        
    }

    // Update is called once per frame
    void Update()
    {
        angleY = Player.rotation.eulerAngles.y;

    }

    private void FixedUpdate()
    {
        SetCamPosition();
        
        Cam.rotation = Quaternion.Euler(0, angleY, 0);
    }

    void SetCamPosition()
    { 
        Cam.position = new Vector3(
            Player.position.x - radius * Mathf.Sin(angleY / 360 * 2 * Mathf.PI),
            Player.position.y + 2,
            Player.position.z - radius * Mathf.Cos(angleY / 360 * 2 * Mathf.PI)
        );
    }
}
