using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryBirdsScript : MonoBehaviour
{
    public bool PlayerLifeCycle;

    [SerializeField] float _maxDistance;
    [SerializeField] float _forceFactor = 250;
    [SerializeField] GameManager gameManager;

    private Rigidbody2D rigid2d;

    private Vector3 _offset;
    private Vector3 _birdOrigin;
    
    private Quaternion _birdOriginalRotation;

    private void Awake()
    {
        PlayerLifeCycle = false;
        rigid2d = GetComponent<Rigidbody2D>();
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
        StartCoroutine(gameManager.Lunched());
    }

    private void OnEnable()
    {
        PlayerLifeCycle = true;
        gameObject.transform.position = _birdOrigin;
        gameObject.transform.rotation = _birdOriginalRotation;
        rigid2d.velocity = new Vector2(0, 0);
        rigid2d.freezeRotation = true;
        rigid2d.gravityScale = 0;
        rigid2d.freezeRotation = false;
    }

    private void OnDisable()
    {
        PlayerLifeCycle = false;
    }

    public void InitGameManager(GameManager gameManager)
    {
        if (gameManager != null)
        {
            this.gameManager = gameManager;
        }
    }

}
