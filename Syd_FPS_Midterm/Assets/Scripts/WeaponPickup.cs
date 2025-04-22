   using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{

    public GameObject weaponPrefab;

    //the transfromsocket to which the eweapon iwll be parented to he player 
    public Transform weaponSocket;

   

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            //instantiate and parent directlyu to weapon sockey 
            GameObject newWeapon = Instantiate(weaponPrefab, weaponSocket.position, Quaternion.identity, weaponSocket);

            //restetin locla pos and rotaiton to ensure it first in the socket 
            newWeapon.transform.localPosition = Vector3.zero;
            newWeapon.transform.localRotation = Quaternion.identity;

            //add ti to the list 

            other.GetComponent<WeaponManager>().AddWeapon(newWeapon);  

            //setroy weapon puck up game object 
            Destroy(gameObject);


        }
    }

}
