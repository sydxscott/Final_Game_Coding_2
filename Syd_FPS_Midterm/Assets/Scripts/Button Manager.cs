using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{

    public void MainMenu()
    {
        SceneManager.LoadScene(0);

    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Objective()
    {
        SceneManager.LoadScene(2);
  
    }

    public void Controls()
    {
        SceneManager.LoadScene(3);
    }
}
