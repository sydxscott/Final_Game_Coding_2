using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MirrorManager : MonoBehaviour
{
    public Slot[] closetSlot;
    public Slot avalabileSlot;

    public Item buttonResult;
    public Item furResult;
    Renderer rendy;
    public Material mat;


    // Start is called before the first frame update
    void Start()
    {
        rendy = GetComponent<Renderer>();
        mat = rendy.GetComponent<Material>();

        foreach (Slot _slot in closetSlot)
        {
            if (_slot.item != null)
            {
                foreach(Item _item in CraftingManager.craftedItems)
                {
                    _slot.gameObject.SetActive(true);
                    _slot.GetComponent<Image>().sprite = _item.GetComponent<Image>().sprite;
                    _slot.item = _item;

                }
               
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        //if this and that = red shirt true
        //if true closerList.add
        //if player grabs red sprite
        //set material to red
        //if mose down compare tag red shirt
        // mat.color = Color.green;


        //foreach (Slot _slot in closetSlot)
        //{
        //    if (_slot.item == null)
        //    {

        //        avalabileSlot = _slot;
        //        break;
        //    }

        //}

        //if (CraftingManager.haveButtonResult)
        //{
        //    avalabileSlot.item = furResult;
        //    avalabileSlot.gameObject.SetActive(true);
          


        //}


    }
}
