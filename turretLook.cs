using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class turretLook : MonoBehaviour
{

    private Queue<GameObject> enemyQueue = new Queue<GameObject>();
    private List<GameObject> enemyList = new List<GameObject>();
    private GameObject primaryTarget;

    public GameObject spawner;

    public bool tarFirst;
    public bool tarLast;

    



    // Start is called before the first frame update
    void Start()
    {
      tarFirst = true;
      tarLast = false;
    }

    public void TarFirst()
    {
        tarFirst = true;
        tarLast = false;
    }

    public void TarLast()
    {
        tarFirst = false;
        tarLast = true;
    }

    // Update is called once per frame
    void Update()
    {

        

        if(tarFirst == true)
        {

            if(enemyQueue.Count > 0)
            {
                primaryTarget = enemyQueue.Peek();

                if(enemyQueue.Peek() == null)
                {
                    enemyQueue.Dequeue();
                }
                if(primaryTarget)
                {
                    transform.LookAt(primaryTarget.transform.position);
                    spawner.SendMessage("Attack");
                }
            }
        }
        
        if(tarLast == true)
        {

            
            if(enemyList.Count > 0)
            {
                
                primaryTarget = enemyList.Last();

                if(enemyList.Last() == null)
                {
                    enemyList.Remove(enemyList.Last());
                }

                if(primaryTarget)
                {
                    transform.LookAt(primaryTarget.transform.position);
                    spawner.SendMessage("Attack");
                }
            }

        }


    }

    

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "enemy")
        {
            enemyList.Add(other.gameObject);
            enemyQueue.Enqueue(other.gameObject);
            
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "enemy")
        {
            enemyQueue.Dequeue();
            enemyList.Remove(other.gameObject);
            primaryTarget = null;
            //RemoveEnemy(other.gameObject);

            //GameObject tempEnemy = other.gameObject;
            //for(int i = 0; i < enemyList.Count; i++)
            //{
            //    enemyList.Remove(tempEnemy);
            //}
        }

    }

    void RemoveEnemy(GameObject enemyToRemove)
    {
        GameObject tempEnemy = enemyToRemove;
        for(int i = 0; i < enemyList.Count; i++)
            {
                enemyList.Remove(tempEnemy);
            }
    }
}
