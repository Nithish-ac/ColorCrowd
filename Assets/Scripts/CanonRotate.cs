using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CanonRotate : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 targetPosition;
    Vector3 initialMousePosition;

    public float speed;

    public Transform cross;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && GameManager.instance.startCannonRotation)
        {
            SetTargetPosition();
            initialMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0) && GameManager.instance.startCannonRotation)
        {
            RotateCannon();
            HandleDragging();
        }
    }

    void SetTargetPosition()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            targetPosition = hit.point;
        }
    }

    void RotateCannon()
    {
        Vector3 direction = targetPosition - transform.position;
        Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, speed * Time.deltaTime);

        // Align crosshair with the cannon's forward direction
        cross.forward = direction.normalized;
    }

    void HandleDragging()
    {
        Vector3 currentMousePosition = Input.mousePosition;
        Vector3 mouseDelta = currentMousePosition - initialMousePosition;

        // Do something with mouseDelta for dragging, e.g., move an object or camera

        initialMousePosition = currentMousePosition;
    }
}
