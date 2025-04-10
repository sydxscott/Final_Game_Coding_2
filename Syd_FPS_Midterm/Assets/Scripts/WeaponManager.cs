using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    // lisrt to hola all weapon instanese the palyer ahas picked up 
    public List<GameObject> weaponsList = new List<GameObject>();
    //index to track currentl active weapon 

    private int currentWeaponIndex = -1; // start with no weapon 


   

    // Update is called once per frame
    void Update()
    {
        //key to switch weapons 

        if (Input.GetKeyDown(KeyCode.Q) && weaponsList.Count > 0)
        {
            // +1 incuements the cufrrent weapon indec by moving 1 next weapon in list 
            //divid sylmbol wraps arount to begingin of list 
            int nextWeaponIndex = (currentWeaponIndex + 1)% weaponsList.Count;
            SwitchWeapon(nextWeaponIndex);

        }


    }





    public void AddWeapon(GameObject weaponPrefab)
    {
        // add instacntionaed wrapn to the leit 
        weaponsList.Add(weaponPrefab);   
        // precent multiple actuve weapons 

        weaponPrefab.SetActive(false); // start with weapon diabled 

        if(weaponsList.Count == 1)
        {

            SwitchWeapon(0);

            


        }




    }

    void SwitchWeapon(int index)
    {

      if(currentWeaponIndex != -1)
        {
            //ensures prevouse one is off
            weaponsList[currentWeaponIndex].SetActive(false);

        }
      //set new weapon as active 

      currentWeaponIndex = index;
        weaponsList[currentWeaponIndex].SetActive(true);



    }


}
