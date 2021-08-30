using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Transform[] spwanPoints;

    //timer
    [SerializeField] float time;
    [SerializeField] float timmer;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;

        spwanPoints = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            spwanPoints[i] = transform.GetChild(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(timmer < Time.time)
        {
             Spwan();
             timmer = Time.time + time;
        }
    }

    void Spwan()
    {
        Transform moreProximeTransform = spwanPoints[0];
        float moreProximeDistance = 1000;

        foreach (Transform item in spwanPoints)
        {
            float distance = (item.position - player.position).magnitude;
            
            if(distance < moreProximeDistance)
            {
                moreProximeTransform = item;
                moreProximeDistance = distance;
            }
        }

        EnemyPooling.enemyPooling.GetEnemy(moreProximeTransform.position, Quaternion.identity);
    }
}
