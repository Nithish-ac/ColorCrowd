using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CanonRotate : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 targetPosition;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && GameManager.instance.startCannonRotation)
        {
            SetTargetPosition();
        }

        if (Input.GetMouseButton(0) && GameManager.instance.startCannonRotation)
        {
            RotateCannon();
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
        transform.rotation = toRotation;
        GameManager.instance.Shoot();
    }
}
