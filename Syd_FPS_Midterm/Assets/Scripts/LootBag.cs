using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBag : MonoBehaviour
{
    public GameObject droppedItemPrefab;
    public List <Loot> lootList = new List <Loot>();

    Loot GetDroppedItem()
    {
        //generating which loot will pop up based off a random number that corralates to the drop rate 
        int randomNumber = Random.Range(0, 101);
        List<Loot> possibleItems = new List <Loot>();   
        //loop to see is the random numebr we got is less than or equal to the drop chance of the item 
        //if random number is 80 than our buttons which have a drop chnace of 75 will not be added to the list of posible items 
        foreach (Loot item in lootList)
        {
            if(randomNumber <= item.dropChance)
            {
                possibleItems.Add(item);
            
            }

        }
        // if you haev found some loot that can be droped (it meets the number requireemnt)
        if (possibleItems.Count > 0)
        {
            //geting a random item from the posible items
            Loot droppedItem = possibleItems[Random.Range(0, possibleItems.Count)]; 
            return droppedItem;
        }

        //if no loot found:
        Debug.Log("No Loot Droped");
        return null;


    }

}
