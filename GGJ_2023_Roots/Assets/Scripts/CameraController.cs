using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed;
    public float minRot;
    public float maxRot;

    private float curRot;
    private float curZoom;

    public float minZoom;
    public float maxZoom;

    public float zoomSpeed;
    public float rotateSpeed;

    private Camera cam;
    public static CameraController instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        cam = Camera.main;
        curZoom = cam.transform.localPosition.y;
        curRot = -50;

    }

    private void Update()
    {
        //Zoom
        curZoom += Input.GetAxis("Mouse ScrollWheel") * -zoomSpeed;
        curZoom = Mathf.Clamp(curZoom, minZoom, maxZoom);

        cam.transform.localPosition = Vector3.up * curZoom;

        //camera rotation
        if (Input.GetMouseButton(1))
        {
            float x = Input.GetAxis("Mouse X");
            float y = Input.GetAxis("Mouse Y");

            curRot += -y * rotateSpeed;
            curRot = Mathf.Clamp(curRot, minRot, maxRot);

            transform.eulerAngles = new Vector3(curRot, transform.eulerAngles.y + (x * rotateSpeed), 0.0f);

        }

        //movement
        Vector3 forward = cam.transform.forward;
        forward.y = 0.0f;
        forward.Normalize();

        Vector3 right = cam.transform.right;

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        Vector3 direction = forward * moveZ + right * moveX;
        direction.Normalize();

        direction *= moveSpeed * Time.deltaTime;

        transform.position += direction;
    }

}
