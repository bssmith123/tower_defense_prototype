using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DeathTimer", 5.0f);
    }

    void DeathTimer()
    {
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //public GameObject turretLook;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "enemy")
        {
            //turretLook.SendMessage("RemoveEnemy", other.gameObject);
            Destroy(gameObject);
        }
    }
}
