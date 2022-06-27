using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static bool gameOver;
    public GameObject gameOverPanel;

    public static bool isGameStarted;
    public GameObject startingText;
    public static int numberOfCoins;
    public Text coinsText;

    
    void Start()
    {
        Time.timeScale = 1;
        gameOver = false;
        isGameStarted = false;
        numberOfCoins = 0;

        // InvokeRepeating("fpsChecker",0,1);
    }
    

    void Update()
    {
        if(gameOver){
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
        }
        coinsText.text = "Coins: " + numberOfCoins;

        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            isGameStarted = true;
            Destroy(startingText);
        }
    }

    void fpsChecker()
    {
        Debug.Log(1/Time.deltaTime);
    }
}
