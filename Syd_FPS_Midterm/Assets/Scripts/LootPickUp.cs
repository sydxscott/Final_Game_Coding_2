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

    //Renderer rendy;
    //public Material mat;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Destroy(gameObject);

            //adding the items to list of all items gathered
            string item = gameObject.GetComponent<SpriteRenderer>().sprite.name; 
            itemsGathered.Add(item);

            Debug.Log("item added to list: " + item);
            if (item == "Button")
            {
                numButton++;
                haveButton = true;
                // this debug log is not working so for each or if statement is not getting called 
                //come back to this problem later and work on crafting mechanic
                Debug.Log("num button: " + numButton);

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
                Debug.Log("num fabric: " + numFabric);
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


    public void Update()
    {
     
    }


}
