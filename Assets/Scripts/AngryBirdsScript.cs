using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryBirdsScript : MonoBehaviour
{
    [SerializeField] float _maxDistance;
    [SerializeField] float _forceFactor = 250;
    [SerializeField] int premitedAttempts = 3;

    private Vector3 _offset;
    private Vector3 _birdOrigin;
    private Quaternion _birdOriginalRotation;
    private Rigidbody2D rigid2d;
    private bool isPlayerLunched = false;

    private void Start()
    {
        rigid2d = GetComponent<Rigidbody2D>();
    }

    public bool IsPlayerLunched()
    {
        return isPlayerLunched;
    }

    private void OnMouseDown()
    {
        Vector3 mouseOffset = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseOffset.z = transform.position.z;
        _offset = mouseOffset - transform.position;
        _birdOrigin = transform.position;
        _birdOriginalRotation = transform.rotation;
    }

    private void OnMouseDrag()
    {
        float distance;
        Vector3 newloc;
        Vector3 dragPostion = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dragPostion.z = transform.position.z;
        float newlocX = transform.position.x;

        distance = Vector3.Distance(transform.position, _birdOrigin);
        // Debug.Log(distance);

        if (distance < _maxDistance)
        {
            transform.position = dragPostion - _offset;
            //float newlocX = transform.position.x;
        }
        else
        {
            newloc = new Vector3(newlocX, dragPostion.y - _offset.y, dragPostion.z - _offset.z);
            transform.position = newloc;
        }
    }

    private void OnMouseUp()
    {
        GetComponent<Rigidbody2D>().gravityScale = 1;
        Vector3 drageVector = transform.position - _birdOrigin;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(-drageVector.x * _forceFactor, -drageVector.y * _forceFactor));
        StartCoroutine(Lunched());
    }

    IEnumerator Lunched()
    {
        isPlayerLunched = true;
        yield return new WaitForSeconds(4);
        isPlayerLunched = false;

        ResetPlayer();
    }

    void ResetPlayer()
    {
        if (premitedAttempts >= 0)
        {
            gameObject.transform.position = _birdOrigin;
            gameObject.transform.rotation = _birdOriginalRotation;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            GetComponent<Rigidbody2D>().freezeRotation = true;
            GetComponent<Rigidbody2D>().gravityScale = 0;
            GetComponent<Rigidbody2D>().freezeRotation = false;
            premitedAttempts--;
        }
    }

    [ContextMenu("GetScore")]
    void GetScore()
    {
        ScoreCounter.GetOverallScore();
    }
}
