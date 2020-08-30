using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private float minZoom = 10;
    [SerializeField] private float maxZoom = 54;
    [SerializeField] private float zoomAmount = 3f;
    [SerializeField] private float zoomLerpSpeed = 10f;
    [SerializeField] private float movementSpeed = 2;

    private float horizontalMovement;
    private float verticalMovement;
    private float scrollInput;

    void Update()
    {
        horizontalMovement = Input.GetAxis("Horizontal");
        verticalMovement = Input.GetAxis("Vertical");
        scrollInput = Input.GetAxis("Mouse ScrollWheel");

        cam.orthographicSize -= scrollInput * zoomAmount;
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minZoom, maxZoom);
        transform.position += Vector3.right * horizontalMovement * movementSpeed;
        transform.position += Vector3.up * verticalMovement * movementSpeed;
    }
}
