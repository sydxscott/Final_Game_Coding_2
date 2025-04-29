using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName;
    public bool resultItem;

    private void Update()
    {
        if (itemName == "button" && LootPickUp.numButton == 0)
        {
            gameObject.SetActive(false);
        }
        if (itemName == "fur" && LootPickUp.numFur == 0)
        {
            gameObject.SetActive(false);
        }
        if (itemName == "fabric" && LootPickUp.numFabric == 0)
        {
            gameObject.SetActive(false);
        }

    }




}
