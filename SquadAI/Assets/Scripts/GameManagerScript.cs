using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript instance = null;

    public enum GameState
    {
        InGame,
        GameLost,
        GameWon,
    }

    public GameState gameState;

    public int coinsTotal;
    public TextMeshProUGUI coinsText;
    public Canvas inGameCanvas;
    public Canvas loseCanvas;
    public Canvas winCanvas;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        else if (instance != null)
        {
            Destroy(gameObject);
        }

        Time.timeScale = 1;
        inGameCanvas.enabled = true;
        loseCanvas.enabled = false;
        winCanvas.enabled = false;
    }

    private void Update()
    {
        coinsText.text = "Coins: " + coinsTotal.ToString();

        if (gameState == GameState.GameLost)
        {
            GameLost();
        }

        if (gameState == GameState.GameWon)
        {
            GameWon();
        }
    }

    void GameLost()
    {
        Time.timeScale = 0.25f;
        loseCanvas.enabled = true;
    }

    void GameWon()
    {
        Time.timeScale = 0.25f;
        winCanvas.enabled = true;
    }

    public void RestartGame()
    {
        string scene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(scene);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
