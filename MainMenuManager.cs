﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void beginQuestButtonHandler()
    {
        SceneManager.LoadScene("Lvl1");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
