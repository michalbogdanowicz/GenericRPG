using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScritpt : MonoBehaviour
{
    public float ZoomSpeed;
    public float CameraSpeed;
    // Use this for initialization
    void Start()
    {

    }

    public Camera camera;
    // Update is called once per frame
    void Update()
    {
        float xAxisValue = Input.GetAxis("Horizontal");
        float yAxisValue = Input.GetAxis("Vertical");
        float zAxisValue = Input.GetAxis("Mouse ScrollWheel");

        if (camera != null)
        {

            if (Input.GetKey("mouse 1"))
            {

                transform.position += new Vector3(ProperChange(Input.GetAxisRaw("Mouse X")) * -1, ProperChange(Input.GetAxisRaw("Mouse Y")) * -1, 0f);
            }
            if (zAxisValue != 0)
            {
                camera.orthographicSize -= zAxisValue * ZoomSpeed;
            }

            if (xAxisValue != 0f || yAxisValue != 0f)
            {
                camera.transform.Translate(new Vector3(ProperChange(xAxisValue), ProperChange(yAxisValue), 0));
            }
        }
    }

    private float ProperChange(float value)
    {
        return value * Time.deltaTime * CameraSpeed;
    }
}
