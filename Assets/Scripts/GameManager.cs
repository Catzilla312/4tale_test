using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    Vector3[] coinPositions = new [] { 
        new Vector3(1.9f,0.5f,4.3f), 
        new Vector3(1.6f,0.5f,4.6f), 
        new Vector3(-2.1f,0.5f,-0.9f), 
        new Vector3(-2.1f,0.5f,-3.8f), 
        new Vector3(1.5f,0.5f,-0.9f), 
        new Vector3(1.7f,0.5f,-4.4f), 
        };
    public GameObject endUI;
    string[] gameResult = {"LOSE", "WIN"};

    private int currentPlacement;
    private int numberOfCoins = 0;
    public Text scoreText;
    public Text timerText;
    public Text resultText;
    private int currentTime;

    public int maxTime = 15;
    public int timeIncrement = 5;
    public int maxScore = 10;
    public int agentSpeed = 5;

    private void Awake(){
        instance = this;
        Debug.Log("GAME MANAGER AWAke");
    }

    public GameObject coin;
    void Start()
    {
        CoinPlacement();
        currentTime = maxTime;
        InvokeRepeating("Timer", 0f, 1.0f);
        EventManager.CoinPickUp.AddListener(CoinPlacement);
        EventManager.CoinPickUp.AddListener(UpdateScore);
        EventManager.CoinPickUp.AddListener(IncrementTime);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void CoinPlacement(){
        int newPlacement = UnityEngine.Random.Range(0,6);
        while (currentPlacement == newPlacement){
           newPlacement = UnityEngine.Random.Range(0,6);     
        }
        currentPlacement = newPlacement;
        Instantiate(coin,coinPositions[currentPlacement],Quaternion.identity);
    }

    private void UpdateScore(){
        int newScore = numberOfCoins + 1;
        if(newScore == maxScore){
            Debug.Log("WIN");
            scoreText.text = newScore.ToString();
            StopGame(1);
        }else{
            numberOfCoins = newScore;
            scoreText.text = numberOfCoins.ToString();
        }        
    }

    private void Timer(){
        if(currentTime > 0){
            timerText.text = currentTime.ToString();
            currentTime--;
        }else{
            Debug.Log("LOSE");
            StopGame(0);
        }
    }

    private void IncrementTime(){
        if(currentTime + timeIncrement > maxTime){
            currentTime = maxTime;
        }else{
            currentTime +=timeIncrement;
        }
        timerText.text = currentTime.ToString();
    }

    private void StopGame(int state){
        Debug.Log("STOPE GAME");
        resultText.text = gameResult[state];
        Time.timeScale=0f;
        endUI.SetActive(true);
    }

    public void ExitGame(){
        Debug.Log("YEAS");
        Application.Quit();
    }
    public void Retry(){
        Debug.Log("Retroo");
        Time.timeScale=1f;
        SceneManager.LoadScene( SceneManager.GetActiveScene().name );
    }
}
