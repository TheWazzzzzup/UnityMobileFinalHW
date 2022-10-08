using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class ObstcalBehavior : MonoBehaviour
{
    public UnityEvent AddScore;

    [SerializeField] GameManager gameManager;
    [SerializeField] TextMeshProUGUI textMeshProUGUI;
    [SerializeField] float changeInVecotr = 1;

    private Vector3 startVec3;

    private string playerTag = "Player";
    
    private bool playerLunched;
    private bool aboutToDestroy;
    
    private int scoreOfUnit;

    private void Start()
    {
        startVec3 = transform.position;
        AddScore.AddListener(CoroutineTriger);
        scoreOfUnit = ScoreCounter.GetRandomScore();
    }

    private void Update()
    {
        playerLunched = gameManager.CurrentPlayerLaunched();

        if (playerLunched == true)
        {
            CheckForPositionChange();
        }
    }

        IEnumerator DestorySelfOnDelay(int delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
        ScoreCounter.AddScore(scoreOfUnit);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == playerTag && aboutToDestroy == false)
        {
            AddScore.Invoke();
            aboutToDestroy = true;
        }
    }
    
    private bool AbsChangeInVec3(Vector3 pos,Vector3 curPos)
    {
        float x = Mathf.Abs(curPos.x - pos.x);
        float y = Mathf.Abs(curPos.y - pos.y);
        float z = Mathf.Abs(curPos.z - pos.z);
        if (x > changeInVecotr || y > changeInVecotr || z > changeInVecotr)
        {
            return true;
        }
        else { return false; }
    }

    private void CheckForPositionChange()
    {
        if (AbsChangeInVec3(startVec3, transform.position) && aboutToDestroy == false)
        {
            AddScore.Invoke();
            aboutToDestroy = true;
        }
    }

    private void CoroutineTriger()
    {
        StartCoroutine(DestorySelfOnDelay(3));
    }
}
