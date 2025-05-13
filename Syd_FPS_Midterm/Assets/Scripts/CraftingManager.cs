using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class CraftingManager : MonoBehaviour
{

    //store current item that we are dragging and dropping 
    private Item currentItem;
    public Image customCursor;



    //list of slots for putting in item 
    public Slot[] craftingSlot;

    //list of slots for putting in crafting results

    public Slot[] inventorySlot;

    //items that get dragged into the slots
    public List<Item> itemList;

    //arrary of recpies and their results by corresponding index
    public string[] recipes;
    public Item[] recipeResults;

    //lisdt of crafted items to use in mirror changing room 
    public static List<Item> craftedItems = new List<Item>();

    //the result slot which we will replace with a crafting result image
    public Slot resultSlot;

    //text for the numebr of items
    public TextMeshProUGUI buttonText;
    public TextMeshProUGUI fabricText;
    public TextMeshProUGUI furText;
    public TextMeshProUGUI laceText;
    public TextMeshProUGUI gromText;

    //bools to see if you have crafted this item so it can show up in your closet/ mirror to equip
    public static bool haveFurResult;
    public static bool haveButtonResult;

    //items corrispoding to items 
    public Item button;
    public Item fur;
    public Item fabric;
    public Item lace;
    public Item grom;



    private void Start()
    {


    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            //check if we are draging an item around 
            if (currentItem != null)
            {
                //turn off our current item in custom cursor and instead make it apear in the nearist slot 
                customCursor.gameObject.SetActive(false);

                // creating a variable of nearst slot
                // since this runs everytime we realse our mouse, reset the nearist slot becasue it might be differnt everytime
                Slot nearestSlot = null;
                float shortestDistance = float.MaxValue;

                //finding shortest distance for crafting slot

                //if the current item that we are holding is a crafting material and not a result item (bool in our item script) then we find the closet crafting slot
                if (currentItem.resultItem != true)
                {
                    //go through all of the crafting slots and check for distance 

                    foreach (Slot slot in craftingSlot)
                    {


                        //finding the disitance of mouse to the slot
                        float distance = Vector2.Distance(Input.mousePosition, slot.transform.position);
                        // if the distance that we just calculated is shorter than our current shortest distance then that becomees our new shortsts distance 
                        // and because it's the shortest distance the slot that we are checking in our list becomes our nearist slot/ closest to the mouse
                        if (distance < shortestDistance)
                        {

                            shortestDistance = distance;
                            nearestSlot = slot;



                        }
                    }
                }


                //finding shorest distance for inventory slots 
                //repeat everything from above but only if the item that we are clicking in a result item
                if (currentItem.resultItem == true)
                {

                    foreach (Slot slot in inventorySlot)
                    {
                        float distance = Vector2.Distance(Input.mousePosition, slot.transform.position);

                        if (distance < shortestDistance)
                        {
                            shortestDistance = distance;
                            nearestSlot = slot;
                        }
                    }

                }

                if (!nearestSlot.isFull && itemList[nearestSlot.index] == null)
                {
                    nearestSlot.gameObject.SetActive(true);
                    nearestSlot.GetComponent<Image>().sprite = currentItem.GetComponent<Image>().sprite;
                    nearestSlot.item = currentItem;
                    //when an item is actually in the slot then thats when we lower the count
                    if (nearestSlot.item.itemName == "button") LootPickUp.numButton--;
                    if (nearestSlot.item.itemName == "fur") LootPickUp.numFur--;
                    if (nearestSlot.item.itemName == "fabric") LootPickUp.numFabric--;
                    if (nearestSlot.item.itemName == "lace") LootPickUp.numLace--;
                    if (nearestSlot.item.itemName == "grom") LootPickUp.numGrom--;
                    
                    nearestSlot.isFull = true;

                    //the items in the slots are added to the item list based off of the index of the slots we put in the inspector 
                    itemList[nearestSlot.index] = currentItem;

                    CheckForCreatedRecipies();
                }





                currentItem = null;

                //only check for crafted recipes when there is nothing in the result slot



            }
        }
        UpdateCraftingText();
        CheckForMaterialNum();
    }


    void CheckForCreatedRecipies()
    {
        //we currently dont want anything in the result slot
        //we turned this off because it kept deleting the result item
        resultSlot.gameObject.SetActive(false);
        resultSlot.item = null;

        string currentRecipeString = "";

        //go through the items you dragged into the item slots and add their name to a string
        //if there is no item just add the word null
        foreach (Item item in itemList)
        {
            //if the item isnt empty then its going to add it to the string AND is not full
            if (item != null)
            {
                //if there is something already there then we do not want to add to string
                currentRecipeString += item.itemName;
                Debug.Log(currentRecipeString);
            }
            else
            {
                currentRecipeString += "null";
            }

        }


        //go through the list of recipies until you reach the end
        for (int i = 0; i < recipes.Length; i++)
        {
            //look to see if there are any recipes that match the string we made above
            if (recipes[i] == currentRecipeString)
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

    //needs to add amounts back incase you dont want that item
    public void OnClickSlot(Slot slot)
    {
        if (slot.item.itemName == "button")
        {
            LootPickUp.numButton += 1;
            //Debug.Log("clicked slot");


        }
        if (slot.item.itemName == "fur")
        {
            LootPickUp.numFur += 1;


        }
        if (slot.item.itemName == "fabric")
        {
            LootPickUp.numFabric += 1;


        }
        if (slot.item.itemName == "grom")
        {
            LootPickUp.numGrom += 1;


        }
        if (slot.item.itemName == "lace")
        {
            LootPickUp.numLace += 1;


        }

        //when we click on a slot, reset and make it empty
        slot.item = null;
        itemList[slot.index] = null;
        slot.gameObject.SetActive(false);
        //check for recpies with new empty slot
        slot.isFull = false;
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

        //check to see if it was actually dropped into the slot before subtracting
        /*if (item.itemName == "button")
        {
            LootPickUp.numButton -= 1;
        }
        if (item.itemName == "fabric")
        {
            LootPickUp.numFabric -= 1;
        }
        if (item.itemName == "fur")
        {
            LootPickUp.numFur -= 1;
        }*/

    }


    public void OnMouseDownResult(Slot slot)
    {
        if (slot.item.itemName == "Fur Result")
        {

            haveFurResult = true;
            Debug.Log("Is fur result true:" + haveFurResult);
            craftedItems.Add(slot.item);
            Debug.Log("Items in crafted Item list: " + craftedItems.ToString());

        }
        if (slot.item.itemName == "Button Result")
        {
            haveButtonResult = true;
            Debug.Log("Is button result true:" + haveButtonResult);
            craftedItems.Add(slot.item);
            Debug.Log("Items in crafted Item list: " + craftedItems.ToString());
        }
        Slot avalSlot = null;

        // for all the result slots check to see if there is anything 
        //if there isn't anything in the slot than that becomes the first avalable slot
        foreach (Slot _slot in inventorySlot)
        {
            if (_slot.item == null)
            {

                avalSlot = _slot;
                break;
            }

        }

        // put the crafted item into the avalable slot
        avalSlot.gameObject.SetActive(true);
        avalSlot.GetComponent<Image>().sprite = slot.GetComponent<Image>().sprite;
        avalSlot.item = slot.item;

        // make the result slot empty 
        slot.item = null;
        itemList[slot.index] = null;
        slot.gameObject.SetActive(false);

        //empty out the crafting slots 
        foreach (Slot _slot in craftingSlot)
        {

            _slot.item = null;
            _slot.gameObject.SetActive(false);
            itemList[_slot.index] = null;
            _slot.isFull = false;
        }


    }

    public void CheckForMaterialNum()
    {
        if (LootPickUp.numButton <= 0)
        {

            button.gameObject.SetActive(false);

        }
        else
        {
            button.gameObject.SetActive(true);
        }
        if (LootPickUp.numFur <= 0)
        {
            fur.gameObject.SetActive(false);
        }
        else
        {
            fur.gameObject.SetActive(true);
        }
        if (LootPickUp.numFabric <= 0)
        {
            fabric.gameObject.SetActive(false);
        }
        else
        {
            fabric.gameObject.SetActive(true);
        }
        if (LootPickUp.numLace <= 0)
        {
            lace.gameObject.SetActive(false);
        }
        else
        {
            lace.gameObject.SetActive(true);
        }
        if (LootPickUp.numGrom <= 0)
        {
            grom.gameObject.SetActive(false);
        }
        else
        {
            grom.gameObject.SetActive(true);
        }


    }

    public void UpdateCraftingText()
    {
        buttonText.text = LootPickUp.numButton.ToString();
        fabricText.text = LootPickUp.numFabric.ToString();
        furText.text = LootPickUp.numFur.ToString();
        laceText.text = LootPickUp.numLace.ToString();
        gromText.text = LootPickUp.numGrom.ToString();
    }


}
