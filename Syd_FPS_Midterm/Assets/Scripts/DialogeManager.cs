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
        if (EnemySpawner.startEnemyDeadText)
        {
            LevelOneZombiesDead();

     

        }
    }

    public void LevelOneZombiesDead()
    {
       // StopAllCoroutines();
        textBackground.enabled = true;
        dialogeUI.enabled = true;
        dialogueText = " Z I think all the uggo zombies are gone!";
        //dialogeUI.text = dialogueText;
        StartCoroutine(TypeText());



    }

    //create funstions taht can be called in different scripts when different things happen 
    // each of these functiosn has an array for dialoge line that it cycles throgh 
    // use seth code 




    public  IEnumerator TypeText()
    {
        Debug.Log("corutine has been called");

            currentlyTyping = true;

            foreach (char letter in dialogueText)
            {
                Debug.Log("add a letetr");
                dialogeUI.text += letter;
                yield return new WaitForSeconds(textSpeed);
            }
           
            currentlyTyping = false;


        yield break;


    }




}
