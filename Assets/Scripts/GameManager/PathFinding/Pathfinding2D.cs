using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Pathfinding2D : MonoBehaviour
{
    private Seeker seeker;

    [SerializeField] Transform objetive;

    Path path;
    int node;
    [SerializeField] float speed;
    [SerializeField] float stopDistance;

    private void Awake()
    {
        seeker = GetComponent<Seeker>();
    }
    void Start()
    {
        FindPath();
    }

    private void Update()
    {
        if(path != null) Move();
    }

    public void FindPath()
    {
        seeker.StartPath(transform.position, objetive.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if(p.error)
        {

        }
        else
        {
            path = p;
        }
    }

    void Move()
    {
        Vector2 direction = (Vector2)(transform.position - path.vectorPath[node]);

        transform.position += path.vectorPath[node].normalized * speed;

        if (stopDistance >= (transform.position - path.vectorPath[node]).magnitude) node++;
    }
}
