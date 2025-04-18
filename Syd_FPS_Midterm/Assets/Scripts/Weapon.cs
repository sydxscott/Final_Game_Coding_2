using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public GameObject bulletPrefab;
    public Transform bulletSpawnPos;
    public float bulletVelocity = 30f;
    public float bulletPrefabLifeTime;
    public static int numberOfBullets = 12;
    public float reloadTime = .5f;

    //public TextMeshProUGUI ammoText;



  

    // Update is called once per frame
    void Update()
    {

        
        if (numberOfBullets > 0)
        {
            if (this.gameObject.activeInHierarchy && Input.GetMouseButtonDown(0))
            {
                FireWeapon();
                numberOfBullets -= 1;
                Debug.Log("lossing 1 amo");
            }

        }
        if (Input.GetKey(KeyCode.R))
        {
            numberOfBullets = 12;
            //WaitToReload();
            Debug.Log("reload called");


        }


        //updating the text 
        //ammoText.text = "Ammo: " + numberOfBullets.ToString() + " /12";


    }


    void FireWeapon()
    {
        //spawing the bullet
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPos.position, Quaternion.identity);
       



        //shoot bulet 

        bullet.GetComponent<Rigidbody>().AddForce(bulletSpawnPos.forward.normalized * bulletVelocity, ForceMode.Impulse);

        StartCoroutine(DestroyBulletAfterTime(bullet, bulletPrefabLifeTime));






    }

    IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {


        yield return new WaitForSeconds(delay);

        Destroy(bullet);

    }

     private IEnumerator WaitToReload()
    {
        yield return new WaitForSeconds(reloadTime);

        numberOfBullets = 12;
        FireWeapon();

    }




}
