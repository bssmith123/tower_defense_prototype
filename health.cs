using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class health : MonoBehaviour
{

    public GameObject healthUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "enemy")
        {
            healthUI.SendMessage("healthLower");
        }
    }


}
