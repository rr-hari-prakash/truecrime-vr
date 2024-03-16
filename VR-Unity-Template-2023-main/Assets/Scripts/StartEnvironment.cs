using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartEnvironment : GamePhase
{
    private int m_currentGamePhase;
    public GameObject environment;



    protected override void initializeInternal(){
        SceneManager.LoadScene("PlayScene");
    }
   
}
