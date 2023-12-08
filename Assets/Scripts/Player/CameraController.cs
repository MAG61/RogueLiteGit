using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float damping = 0.2f;
    public Vector3 offset;
    [SerializeField] private BoxCollider2D col;
    private float maxX, minX, maxY, minY;
    private Vector3 topRight, botLeft;
    void Start()
    {
        maxX = col.bounds.max.x; minX = -col.bounds.max.x; maxY = col.bounds.max.y; minY = -col.bounds.max.y;
        topRight = col.bounds.max;
        botLeft = col.bounds.min;
    }
    void Update()
    {

        if (Input.mouseScrollDelta.y > 0 && GetComponent<Camera>().orthographicSize > 5) GetComponent<Camera>().orthographicSize -= 0.25f;
        if (Input.mouseScrollDelta.y < 0 && GetComponent<Camera>().orthographicSize < 8) GetComponent<Camera>().orthographicSize += 0.25f;
    }

    private void FixedUpdate()
    {
        Vector3 newPosition = new Vector3(Mathf.Clamp(target.position.x, minX, maxX), Mathf.Clamp(target.position.y, minY, maxY), 0);
        transform.position = (Vector3.Lerp(transform.position, newPosition, damping) + offset);
    }
}
