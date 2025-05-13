using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class DialogeManager : MonoBehaviour
{
    public  TextMeshProUGUI dialogeUI;
    public  Image textBackground;
    public  Canvas canvas;
    public  bool currentlyTyping;
    private  string dialogueText;
    public  float textSpeed = 5;
   public bool isTyping = false;
    int textboxCounter;



    private void Start()
    {
       dialogeUI = canvas.GetComponentInChildren<TextMeshProUGUI>();
       textBackground = canvas.GetComponentInChildren<Image>();
        
    }

    private void Update()
    {
        if (EnemySpawner.levelOver)
        {
            LevelOneZombiesDead();

        }
    }

    public void LevelOneZombiesDead()
    {
       // StopAllCoroutines();
        textBackground.enabled = true;
        dialogeUI.enabled = true;
        dialogueText = "I think all the uggo zombies are gone!";
        dialogeUI.text = dialogueText;
        StartCoroutine(NextLine());
        dialogueText = "Lets go back to my room to craft with the materials I gathered!";
        dialogeUI.text = dialogueText;
        StartCoroutine(NextLine());
        textBackground.enabled = false;
        dialogeUI.enabled = false;


    }

    //create funstions taht can be called in different scripts when different things happen 
    // each of these functiosn has an array for dialoge line that it cycles throgh 
    // use seth code 




    public  IEnumerator NextLine()
    {
       yield return new WaitForSeconds(3);


    }




}
