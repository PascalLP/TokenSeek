using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    // Restart Button
    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

    // Main Menu Button
    // Play Button
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
