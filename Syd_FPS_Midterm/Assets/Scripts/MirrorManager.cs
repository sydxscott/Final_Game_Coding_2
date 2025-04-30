using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorManager : MonoBehaviour
{
    public Slot[] closetSlot;
    public Slot avalabileSlot;

    public Item buttonResult;
    public Item furResult;




    // Start is called before the first frame update
    void Start()
    {
  
        foreach (Slot _slot in closetSlot)
        {
            if (_slot.item == null)
            {

                avalabileSlot = _slot;
                break;
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Slot _slot in closetSlot)
        {
            if (_slot.item == null)
            {

                avalabileSlot = _slot;
                break;
            }

        }

        if (CraftingManager.haveButtonResult)
        {
            avalabileSlot.item = furResult;
            avalabileSlot.gameObject.SetActive(true);
          


        }


    }
}
