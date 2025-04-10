using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public GameObject bulletPrefab;
    public Transform bulletSpawnPos;
    public float bulletVelocity = 30f;
    public float bulletPrefabLifeTime;



  

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.activeInHierarchy && Input.GetMouseButtonDown(0))
        {
           FireWeapon();
        }




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




}
