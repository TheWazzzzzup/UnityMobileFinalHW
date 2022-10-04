using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Vector3 _offset;

    private void OnMouseDown()
    {
        Vector3 mouseOffset = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseOffset.z = transform.position.z;
        _offset = mouseOffset - transform.position;
    }

    private void OnMouseDrag()
    {
        Vector3 dragPostion = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dragPostion.z = transform.position.z;
        transform.position = dragPostion - _offset;
    }
}
