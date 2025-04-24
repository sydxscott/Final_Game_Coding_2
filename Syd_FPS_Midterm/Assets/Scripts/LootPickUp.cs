using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LootPickUp : MonoBehaviour
{
    public List <string> itemsGathered = new List<string> ();
    
    //numbers to see how many of each material we have
    public static int numButton;
    public static int numFur;
    public static int numFabric;
    public static int numLace;
    public static int numGrom;

    //bools to check if we have an item 
    public static bool haveButton;
    public static bool haveFur;
    public static bool haveFabric;
    public static bool haveLace;
    public static bool haveGrom;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {

            Destroy(gameObject);

            //adding the items to list of all items gathered
            string item = gameObject.GetComponent<SpriteRenderer>().sprite.name; 
            itemsGathered.Add(item);

            Debug.Log("item added to list: " + item);
        }
    }


    private void Update()
    {
        // go through item list and for each one check what type of item it is 
        // add to the amount of that item we have if we have it and make sure we know that we indeed have it
        foreach(string item in itemsGathered)
        {
            if (item == "Button")
            {
                numButton++;
                haveButton = true;
                // this debug log is not working so for each or if statement is not getting called 
                //come back to this problem later and work on crafting mechanic
                Debug.Log("num button: ");

            }
            if (item == "Fur")
            {
                numFur++;
                haveFur = true;

            }
            if(item == "Fabric")
            {
                numFabric++;
                haveFabric = true;
            }
            if(item == "Lace")
            {
                numLace++;
                haveLace = true;
            }
            if(item == "Gromemt")
            {
                numGrom++;
                haveGrom = true;
            }


        }

    }


}
