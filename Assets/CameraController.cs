using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float damping = 0.2f;
    public Vector3 offset;
    void Start()
    {
        
    }
    void Update()
    {
        if (Input.mouseScrollDelta.y > 0 && GetComponent<Camera>().orthographicSize > 5) GetComponent<Camera>().orthographicSize -= 0.25f;
        if (Input.mouseScrollDelta.y < 0 && GetComponent<Camera>().orthographicSize < 8) GetComponent<Camera>().orthographicSize += 0.25f;
    }

    private void FixedUpdate()
    {
        transform.position = (Vector3.Lerp(transform.position, target.position, damping) + offset);
    }
}
