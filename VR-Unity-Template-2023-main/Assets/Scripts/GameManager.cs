using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

     [SerializeField] private GamePhase m_currentGamePhase;

        /// <summary>
        ///     Represents the full list of game phases of the gameplay.
        /// </summary>
        [SerializeField] private List<GamePhase> m_listOfGamePhases;
        [SerializeField] private GameObject m_sceneUnderstanding;

        private bool m_gameplayStarted;

        private int m_currentGamePhaseIndex;

    void Awake()
    {
            // Ensure we have a definition for the gameplay phases
            Debug.Assert(m_listOfGamePhases != null, nameof(m_listOfGamePhases) + " != null");
            Debug.Assert(m_listOfGamePhases.Count > 0, nameof(m_listOfGamePhases) + " count > 0");

            // No game phase is initially performed. To start the gameplay we call StartGameplay().
            m_currentGamePhaseIndex = -1;
    }


    void Start()
    {
        
    }

    public void StartGameplay()
    {
        m_gameplayStarted = true;
        m_currentGamePhaseIndex = 0;
        m_currentGamePhase = m_listOfGamePhases[m_currentGamePhaseIndex];
        m_currentGamePhase.Initialize();
        Debug.Log("Gameplay started");
    }

    public void NextGamePhase()
    {
        if (m_currentGamePhaseIndex < m_listOfGamePhases.Count - 1)
        {
            m_currentGamePhaseIndex++;
            m_currentGamePhase = m_listOfGamePhases[m_currentGamePhaseIndex];
            m_currentGamePhase.Initialize();
        }
        else
        {
            Debug.Log("Gameplay finished");
            m_gameplayStarted = false;
            SceneManager.LoadScene(0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
