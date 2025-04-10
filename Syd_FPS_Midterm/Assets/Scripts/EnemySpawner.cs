using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabsList;

    public Transform[] spawnPointsList;

    public int waveNumber = 1;
    //time to wait before spawning a new wave 
    public float timeBetweenWaves = 20f;
    public int enemiesPerWave = 3;
    public int enemiesAlive = 0;
    public static int badZombies;

    public TextMeshProUGUI enemiesAliveText;




    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnWave());


    }

    void Update()
    {
        badZombieResmaining();


        if (waveNumber > 3 && badZombies == 0)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene(5);
        }

    }




    IEnumerator SpawnWave()// infanite loop tocontinue spawn waves
    {
        while(waveNumber <= 3)
        {
            yield return new WaitForSeconds(timeBetweenWaves);
            //spawn enmey function here
            SpawnEnemies();

        }
    }
    
    private void SpawnEnemies()
    {
        for(int i = 0; i< enemiesPerWave; i++)
        {
            Transform spawnPoint = spawnPointsList[Random.Range(0, spawnPointsList.Length)];

            GameObject enemyPrefab = enemyPrefabsList[Random.Range(0, enemyPrefabsList.Length)];

            GameObject newEnmey = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            enemiesAlive++;

            

            if (newEnmey.tag == "Bad Zombie")
            {
                badZombies++;

            }


        }
        waveNumber++;
        enemiesPerWave += 2;


    }

   
    private void badZombieResmaining()
    {
        enemiesAliveText.text = "Bad Zombies Remaining: " + badZombies;



    }
   
}
  