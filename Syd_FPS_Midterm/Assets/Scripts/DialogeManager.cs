using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogeManager : MonoBehaviour
{
    public static TextMeshProUGUI dialoge;
    public static Image textBackground;
    public Canvas canvas;

    public string[] dialogeLines;

    private void Start()
    {
       dialoge = canvas.GetComponentInChildren<TextMeshProUGUI>();
       textBackground = canvas.GetComponentInChildren<Image>();
        
    }

    public static void LevelOneZombiesDead()
    {

        textBackground.enabled = true;
        dialoge.enabled = true;
        dialoge.text = "I think all the uggo zombies are gone!";



    }

    //create funstions taht can be called in different scripts when different things happen 
    // each of these functiosn has an array for dialoge line that it cycles throgh 
    // use seth code 


}
