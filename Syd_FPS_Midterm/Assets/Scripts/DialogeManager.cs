using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogeManager : MonoBehaviour
{
    public TextMeshProUGUI dialoge;
    public Image textBox;

    public string[] dialogeLines;

    private void Start()
    {
       
        
    }

    public void LevelOneZombiesDead()
    {
        dialoge.gameObject.SetActive(true);
        textBox.gameObject.SetActive(true);

        dialoge.text = "I think all the uggo zombies are gone!";



    }

    //create funstions taht can be called in different scripts when different things happen 
    // each of these functiosn has an array for dialoge line that it cycles throgh 
    // use seth code 


}
