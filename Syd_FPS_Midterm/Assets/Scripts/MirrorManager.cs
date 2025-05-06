using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class MirrorManager : MonoBehaviour
{
    public Slot[] closetSlot;

    //public Renderer rendy;
    //public Material mat;


    // Start is called before the first frame update
    void Start()
    {
        //rendy = GetComponent<Renderer>();
        //mat = rendy.GetComponent<Material>();  

        // grab all crafted items
        for (int i = 0; i < CraftingManager.craftedItems.Count; i++)
        {
            //check if the first slot is empty
            if (closetSlot[i].item == null)
            {
                print(2);
                closetSlot[i].gameObject.SetActive(true);
                closetSlot[i].GetComponent<Image>().sprite = CraftingManager.craftedItems[i].GetComponent<Image>().sprite;
                closetSlot[i].item = CraftingManager.craftedItems[i];
            }
        }
    }

    void Update()
    {
        //if this and that = red shirt true
        //if true closerList.add
        //if player grabs red sprite
        //set material to red
        //if mose down compare tag red shirt
        // mat.color = Color.green;
       


    }

    public void OnClickChangeOutfit(Slot slot)
    {
        Debug.Log("clicking on slot");

        if (slot.item.name == "Button result")
        {
            Debug.Log("button yass");
           // mat.color = Color.white;
        }

    }

}
