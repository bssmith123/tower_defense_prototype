using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScripts : MonoBehaviour
{

    LayerMask placementPlane;
    LayerMask turrentLayer;
    private GameObject selectedTile;

    private GameObject currentSelTurrent;

    public GameObject[] turretStruc;
    private short turretStrucIndex;

    private bool canBuild;

    public GameObject turretMenu;
    public GameObject priorityMenu;
    

    private GameObject goldDisplay;
    private int totalGold;
    private bool notEnoughMoney;

    private GameObject healthDisplay;
    private byte health;

    private GameObject waveDisplay;

    private List<GameObject> totalEnemyList = new List<GameObject>();

    private bool startWave;

    /* --------------------------------------------------------- */
    /* used for spawning */
    public GameObject antSpawner;
    public GameObject antEnemy;
    public GameObject redAntEnemy;
    public GameObject spiderEnemy;
    private int wave;
    public float respawnTimer = 1.0f;
    private float timer;
    public int enemyNumber = 10;
    private int currentWave;

    // Start is called before the first frame update
    void Start()
    {
        placementPlane = LayerMask.GetMask("placementPlane");
        turrentLayer = LayerMask.GetMask("turretLayer");
        turretStrucIndex = 0;
        canBuild = false;
        notEnoughMoney = false;
        goldDisplay = GameObject.Find("goldDisplay");
        healthDisplay = GameObject.Find("healthDisplay");
        waveDisplay = GameObject.Find("waveDisplay");
        totalGold = 5000;
        health = 4;
        startWave = false;
        wave = 1;
        currentWave = 0;

        timer = Time.time + respawnTimer;
    }

    public void AddEnemyTotalList(GameObject enemy)
    {
        totalEnemyList.Add(enemy);
    }

    

    void healthLower()
    {
        health--;
    }


#region currencyCheck
    

    public void BuildTurretType1()
    {
        turretStrucIndex = 0;
        if(CanBuy(totalGold, 500))
        {
            canBuild = true;
        } else {
            notEnoughMoney = true;
        }
        
       
        
    }

    public void BuildTurretType2()
    {
        turretStrucIndex = 1;
        if(CanBuy(totalGold, 1000))
        {
            canBuild = true;   
        } else {
            notEnoughMoney = true;
        }
        
#endregion

    }



    public void AddGold(int goldToAdd)
    {
        totalGold = totalGold + goldToAdd;
    }

        public void StartWave()
    {
        startWave = true;
        currentWave++;
        antSpawner.SetActive(true);
        //Debug.Log(wave);
    }

    public void TarFirstEnemyButton()
    {
        currentSelTurrent.SendMessage("TarFirst");
        currentSelTurrent = null;
    }

    public void TarLastEnemyButton()
    {
        currentSelTurrent.SendMessage("TarLast");
        currentSelTurrent = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < Time.time && startWave == true && enemyNumber > 0)
        {
            Instantiate(antEnemy, antSpawner.transform.position, 
            Quaternion.identity);
            timer = Time.time + respawnTimer;
            enemyNumber--;
            /*
            ADD CODE HERE TO HANDLE MORE TYPES OF ENEMY?
            something either within this if statement to accoutn for higher levels

            or make a second if statment outside of it with all the above conditions + a wave counter
            either works
            */
            AddEnemyTotalList(antEnemy);
        }

        if(enemyNumber == 0)
        {
            antSpawner.SetActive(false);
        }



        if(startWave == true)
        {
            if(antSpawner.activeSelf == false)
            {
                GameObject tempEnemy = GameObject.FindWithTag("enemy");
                if(tempEnemy == null)
                {
                    startWave = false;
                    wave++;
                    enemyNumber = currentWave * 5 + 10;
                    turretMenu.SetActive(true);
                }            
            }

            //tap the turret to pull up the target selection
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;


            //KINDA WORKING CURRENTLY TARGETING THE BIG COLLIDER
            //MAKE IT TARGET THE BOX COLLIDER OR SOMETHING

            if(Physics.Raycast(ray, out hit, 100, turrentLayer, QueryTriggerInteraction.Ignore))
            {
                Debug.Log("hovering over");
                if(Input.GetMouseButtonDown(0) && hit.transform.gameObject.tag == "Turret")
                {
                    currentSelTurrent = hit.transform.gameObject;
                    priorityMenu.SetActive(true);
                }

            }
            
        }

        if(canBuild)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, 100, placementPlane))
            {
                selectedTile = hit.collider.gameObject;
                if(Input.GetMouseButtonDown(0) && selectedTile)
                {
                    if(selectedTile.tag == "tileOpen")
                    {
                        Vector3 tempLoc = new Vector3(selectedTile.transform.position.x,
                        selectedTile.transform.position.y + 0.25f,
                        selectedTile.transform.position.z);

                        Instantiate(turretStruc[turretStrucIndex], 
                        tempLoc, Quaternion.identity);

                        selectedTile.tag = "tileTaken";
                        canBuild = false;
                        turretMenu.SetActive(true);

                        switch(turretStrucIndex)
                        {
                            case 0:
                            totalGold -= 500;
                            break;
                            
                            case 1:
                            totalGold -= 1000;
                            break;

                            default:
                            Debug.Log("incorrect");
                            break;
                        }
                    }

                    if(selectedTile.tag == "tileTaken")
                    {
                        canBuild = false;
                        turretMenu.SetActive(true);
                    }
                }

            }
        }
        goldDisplay.GetComponent<Text>().text = "Gold: " + totalGold.ToString();
        healthDisplay.GetComponent<Text>().text = "Health: " + health.ToString();
        waveDisplay.GetComponent<Text>().text = "Wave: " + currentWave.ToString();

        if(notEnoughMoney)
        {
            Debug.Log("not Enough Money");
            turretMenu.SetActive(true);
            notEnoughMoney = false;
        }

        if(health > 10)
        {
            health = 0;
        }

        if(health == 0)
        {
            healthDisplay.GetComponent<Text>().text = "game over man";
        }
        
    }

    /*/
    public void WaveUpdate(int wave)
    {
        currentWave = currentWave + wave;
    }

    int UpdateEnemyNumber(int currentWave)
    {
        enemyNumber = currentWave * 5 + 10;
        return enemyNumber;
    }
*/
    private bool CanBuy(int currentGold, int cost)
    {
        if (currentGold >= cost)
        {
            return true;
        } else
        {
            return false;
        }
    }
}
