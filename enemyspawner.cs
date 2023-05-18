using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyspawner : MonoBehaviour
{

    public GameObject antEnemy;
    public GameObject antSpawner;
    public int enemyNumber;

    public float respawnTimer = 1.0f;
    private float timer;

    public GameObject gameUI;

    private int currentWave = 1;

    // Start is called before the first frame update
    void Start()
    {
        timer = Time.time + respawnTimer;
        enemyNumber = UpdateEnemyNumber(currentWave);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < Time.time)
        {
            Instantiate(antEnemy, transform.position, Quaternion.identity);
            timer = Time.time + respawnTimer;
            enemyNumber--;
            Debug.Log(enemyNumber);

            //gameUI.SendMessage("AddEnemyTotalList", antEnemy);
        }

        if(enemyNumber == 0)
        {
            UpdateEnemyNumber(currentWave);
            antSpawner.SetActive(false);
        }
    }

    public void WaveUpdate(int wave)
    {
        currentWave = currentWave + wave;
    }

    int UpdateEnemyNumber(int currentWave)
    {
        enemyNumber = currentWave * 5 + 10;
        return enemyNumber;
    }

    

    
}
