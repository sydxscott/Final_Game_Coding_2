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

    public void Refill()
    {
        LootPickUp.numButton = 10;
        LootPickUp.numFur = 10;
        LootPickUp.numFabric = 10;
        LootPickUp.numLace = 10;
        LootPickUp.numGrom = 10;
    }

    public void Mirror()
    {
        SceneManager.LoadScene(7);
    }
}
