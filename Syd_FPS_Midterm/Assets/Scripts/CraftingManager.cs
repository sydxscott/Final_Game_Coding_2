using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class CraftingManager : MonoBehaviour
{

    //store current item that we are dragging and dropping 
    private Item currentItem;
    public Image customCursor;

    public Slot[] craftingSlot;

    //items that get dragged into the slots
    public List<Item> itemList;

    //arrary of recpies and their results by corresponding index
    public string[] recipes;
    public Item[] recipeResults;

    //the result slot which we will replace with a crafting result image
    public Slot resultSlot;

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            //check if we are draging an item around 
            if(currentItem != null)
            {
                //turn off our current tiem in custom cursor and instead make it apear in the nearist slot 
                customCursor.gameObject.SetActive(false);
                Slot nearestSlot = null;
                float shortestDistance = float.MaxValue;

                foreach (Slot slot in craftingSlot)
                {
                    float distance = Vector2.Distance(Input.mousePosition, slot.transform.position);

                    if (distance < shortestDistance)
                    {
                        shortestDistance = distance;
                        nearestSlot = slot;
                    }
                }
                nearestSlot.gameObject.SetActive(true);
                nearestSlot.GetComponent<Image>().sprite = currentItem.GetComponent<Image>().sprite;
                nearestSlot.item = currentItem;
                itemList[nearestSlot.index] = currentItem;

                currentItem = null;

                CheckForCreatedRecipies();
            }
        }
    }


    void CheckForCreatedRecipies()
    {
        //we currently dont want anything in the result slot
        resultSlot.gameObject.SetActive(false);
        resultSlot.item = null;

        string currentRecipeString = "";
       
        //go through the items you dragged into the item slots and add their name to a string
        //if there is no item just add the word null
        foreach(Item item in itemList)
        {
            if(item != null)
            {
                currentRecipeString += item.itemName;

            }
            else
            {
                currentRecipeString += "null";
            }

        }

        //go through the list of recipies until you reach the end
        for(int i = 0; i <recipes.Length; i++)
        {
            //look to see if there are any recipes that match the string we made above
            if(recipes[i] == currentRecipeString)
            {
                // if there is a recipie that matches then make sure the result slot is active so we can put a result in 
                resultSlot.gameObject.SetActive(true);
                //set the result slot to the same image that the corresponging index of the recipe results have
                resultSlot.GetComponent<Image>().sprite = recipeResults[i].GetComponent<Image>().sprite;
                //the item in the slot should match the item in results
                resultSlot.item = recipeResults[i];
            }
        }
    }

    public void OnClickSlot(Slot slot)
    {
        slot.item = null;
        itemList[slot.index] = null;
        slot.gameObject.SetActive(false);
        CheckForCreatedRecipies();


    }

    //draging and droping items into slots:
    public void OnMouseDownItem(Item item)
    {
        //if we aren't dragging and droping an item currently then we should have it be null 
        if (currentItem == null)
        {
            currentItem = item;
            customCursor.gameObject.SetActive(true);
            customCursor.sprite = currentItem.GetComponent<Image>().sprite;

        }




    }


}
