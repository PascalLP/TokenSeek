using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Playing,
    GameOver,
}
public class GameManager : MonoBehaviour
{
    public GameState currentState;

    //game score
    public int playerScore = 0;
    public int seekerScore = 0;
    public int tokenCount;
    Scoring scoreBoard;


    public bool isGameOver;
    public bool isSomeOneFrozen = false;

    public GameObject gameOverPanel;
    public GameObject player;

    public static GameManager instance;

    private void Start()
    {
        currentState = GameState.Playing;
        scoreBoard = FindObjectOfType<Scoring>();
        player = GameObject.Find("player");
        instance = this;
        tokenCount = 13;
    }

    private void Update()
    {
        switch (currentState)
        {
            case GameState.Playing:
                if (tokenCount>0)
                {
                    if(playerScore>=7 || seekerScore >= 7)
                    {
                        currentState=GameState.GameOver;
                    }
                    return;
                }else
                {
                    currentState = GameState.GameOver;
                }
                break;
            case GameState.GameOver:
                HandleGameOver();
                Time.timeScale = 0f;
                break;
        }
    }

    private void HandleGameOver()
    {
        if(scoreBoard != null)
        {
            if (playerScore > seekerScore)
            {
                scoreBoard.titleUI.text = "GAME OVER \nPLAYER WINS!";
            }
            else if(playerScore < seekerScore)
            {
                scoreBoard.titleUI.text = "GAME OVER \nSEEKER WINS!";
            }
        }

        // display game over panel
        gameOverPanel.SetActive(true);
    }

    public void AddScore(string colEntTag)
    {
        if (colEntTag == "Player")
        {
            playerScore++;
            scoreBoard.UpdateScore(colEntTag, playerScore);
        }
        else if (colEntTag == "Seeker")
        {
            seekerScore++;
            scoreBoard.UpdateScore(colEntTag, seekerScore);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        currentState = GameState.Playing;
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1f;
    }



}
