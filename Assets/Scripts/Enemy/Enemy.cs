using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    [Header("vida")]//vida
    [SerializeField] float life = 5;
    [SerializeField] SpriteRenderer spriteRender;
    [SerializeField] Color color;
    [SerializeField] float resetColorTime = 0.02f;

    [Header("Pathfinding")]//pathfinding
    [SerializeField] float speed = 3;
    [SerializeField] Transform player; 
    [SerializeField] AIDestinationSetter destination;

    void Awake()
    {
        //randomizar las stats
        life += (int)Random.Range(-1, 2);
        speed += Random.Range(-0.5f,1.5f);

        //pathfinding
        destination = GetComponent<AIDestinationSetter>();
        player = GameObject.FindWithTag("Player").transform;

        destination.target = player;

        GetComponent<AIPath>().maxSpeed = speed;

        //color
        spriteRender = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(life <= 0) Death();
    }

    void Death()
    {
        Destroy();
    }

    void Damage()
    {
        life -= 1;
        spriteRender.color = color;
        StartCoroutine("ResetColor");
    }

    //corrutinas
    IEnumerator ResetColor()
    {
        yield return new WaitForSeconds(resetColorTime);
        spriteRender.color = new Color(255, 255, 255, 255);
    }

    //globales
    public void Destroy()
    {
        gameObject.SetActive(false);
        spriteRender.color = new Color(255, 255, 255, 255);
        life = 5;
        EnemyDeathPooling.enemyDeathPooling.GetEnemyDeath(transform.position, Quaternion.identity);
        StopAllCoroutines();
    }

    public void ReSpawn()
    {
        gameObject.SetActive(true);
    }
    
    //cosas de unity
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Bullet")) 
        {
            Damage();
        }
    }
}
