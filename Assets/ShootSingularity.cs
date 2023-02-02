using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootSingularity : MonoBehaviour
{
    public float shootRadius;
    public GameObject singularity;
    public Camera cam;

    Orientation orientation;
    Vector2 crosshair;
    Vector2 center;

    private Queue<GameObject> singularityQueue;

    private void Start()
    {
        orientation = GetComponent<Orientation>();
        center = new Vector2(Screen.width / 2, Screen.height / 2);

        Debug.Log(cam.fieldOfView);
        // Shoot Radius must create a "sphere" larger than the FOV

        //GameObject obj = Instantiate(singularity, this.transform.position, Quaternion.identity);
        //obj.transform.localScale = new Vector3(shootRadius, shootRadius, shootRadius);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Vector3 rayDirection = GetCamShootRay();
            Vector3[] intersections = GetIntersections(cam.transform.position,
                cam.transform.position + rayDirection * 50,
                transform.position, shootRadius);

            GameObject obj = Instantiate(singularity, intersections[0], Quaternion.identity);

            //Debug.DrawLine(cam.transform.position, cam.transform.position + rayDirection * 10, Color.red, 0.5f);
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

    private Vector3 GetCamShootRay()
    {
        Vector3 camDirection = Camera.main.transform.forward;
        Vector3 up = Camera.main.transform.up;
        Vector3 side = Vector3.Cross(camDirection, up).normalized;

        Vector2 crosshairDir = (crosshair - center);
        // Debug.Log(up);
        // Debug.Log(camDirection);
        // Debug.Log(side);

        Vector3 nearPlaneOrigin = cam.transform.position + camDirection;
        Vector3 nearPlanePoint = nearPlaneOrigin -
            crosshairDir.x / Screen.width * 2.05f * side +
            crosshairDir.y / Screen.height * 1.15f * up;

        Vector3 rayDirection = (nearPlanePoint - cam.transform.position);

        return rayDirection;
    }

    public static Vector3[] GetIntersections(Vector3 a, Vector3 b, Vector3 center, float radius)
    {
        Vector3 d = b - a;
        Vector3 f = a - center;

        float aa = Vector3.Dot(d, d);
        float bb = 2 * Vector3.Dot(f, d);
        float cc = Vector3.Dot(f, f) - (radius * radius);

        float discriminant = (bb * bb) - (4 * aa * cc);
        if (discriminant < 0)
        {
            return new Vector3[0];
        }
        else if (discriminant == 0)
        {
            float t = -0.5f * bb / aa;
            Vector3[] result = new Vector3[1];
            result[0] = a + t * d;
            return result;
        }
        else
        {
            float t1 = (-bb + Mathf.Sqrt(discriminant)) / (2 * aa);
            float t2 = (-bb - Mathf.Sqrt(discriminant)) / (2 * aa);
            Vector3[] result = new Vector3[2];
            result[0] = a + t1 * d;
            result[1] = a + t2 * d;
            return result;
        }
    }
    public Vector2 GetCrosshair()
    {
        return crosshair;
    }

    public Vector2 GetCenter() { return center; }
}
