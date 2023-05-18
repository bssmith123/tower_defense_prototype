using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{

    public GameObject goal;
    public GameObject gameUI;    
    private float enemySpeed;

    // Start is called before the first frame update
    void Start()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        goal = GameObject.Find("goal");
        agent.destination = goal.transform.position;
        enemySpeed = Random.Range(1, 5);
        agent.speed = enemySpeed;
        gameUI = GameObject.Find("Canvas");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "goal")
        Destroy(gameObject);

        if(other.tag == "projectile")
        {
            transform.position = new Vector3(this.transform.position.x, -10, 
            this.transform.position.y);
            gameUI.SendMessage("AddGold", 100);
            Destroy(gameObject);

        }
    }
}
