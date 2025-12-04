using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPopUp : MonoBehaviour
{
    public GameObject gameOverPopUp;
    public GameObject LosePopUp;
    public GameObject newBestScorePopUp;

    void Start()
    {
        gameOverPopUp.SetActive(false);
        LosePopUp.SetActive(false);
        newBestScorePopUp.SetActive(false);
    }
    private void OnEnable()
    {
        GameEvent.GameOver += ShowGameOverPopUp;
    }  
    private void OnDisable()
    {
        GameEvent.GameOver -= ShowGameOverPopUp;
    }

    private void ShowGameOverPopUp(bool isNewBestScore)
    {
        gameOverPopUp.SetActive(true);
        LosePopUp.SetActive(false);
        newBestScorePopUp.SetActive(true);
    }
}
