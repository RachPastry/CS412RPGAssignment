using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour
{
    public Text npcTextbox;
    private GameObject triggeringNPC;
    private bool triggering;

    public GameObject npcText;


    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        //Interact With NPC, will check if enemy dead and say script accordingly
        if (triggering)
        {
            npcText.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (PlayerPrefs.GetInt("EnemyIsDead") == 0)
                {
                    npcTextbox.text = "Welcome to the Bog Goblin Hunt young student! Click on the goblin to attack him but don't get too close or he'll fight back!";
                    print("Welcome to the Bog Goblin Hunt young student! Click on the goblin to attack him but don't get too close or he'll fight back!");
                }
                if (PlayerPrefs.GetInt("EnemyIsDead") == 1)
                {
                    npcTextbox.text = "The bog goblin is dead! Thank you for saving BU, you are quite impressive";
                    print("The bog goblin is dead! Thank you for saving BU, you are quite impressive");
                }

            }
        }
        else
        {
            npcText.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "NPC")
        {
            triggering = true;
            triggeringNPC = other.gameObject;
        }

    }

    void OnTriggerExit(Collider other)
    {
        triggering = false;
        triggeringNPC = null;
    }
}
