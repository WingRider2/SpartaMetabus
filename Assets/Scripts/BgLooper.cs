using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgLooper : MonoBehaviour
{
    public int numBgCount = 5;
    public int obstacleCount = 0;

    public Vector3 obstacleLastPos = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        Obstacle[] obstacles = GameObject.FindObjectsOfType<Obstacle>();
        obstacleLastPos = obstacles[0].transform.position;
        obstacleCount = obstacles.Length;

        for (int i = 0; i < obstacles.Length; i++)
        {
            obstacleLastPos = obstacles[i].SetRandomPlace(obstacleLastPos, obstacleCount);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("BackGround")){
            float widthOfBgObject  =  ((BoxCollider2D)collision).size.x;
            Vector3 pos = collision.transform.position;
            pos.x += widthOfBgObject * numBgCount;
            collision.transform.position = pos;
            return;
        }

        Obstacle obstacle = collision.GetComponent<Obstacle>();
        if (obstacle)
        {
            obstacleLastPos = obstacle.SetRandomPlace(obstacleLastPos, obstacleCount);
        }
    }
}
