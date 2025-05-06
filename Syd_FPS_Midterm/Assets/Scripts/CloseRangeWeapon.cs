using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CloseRangeWeapon : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bad Zombie" || other.gameObject.tag == "Good Zombie")
        {
            Debug.Log("Enmey in range");

            if (Input.GetMouseButtonDown(0))
            {

                EnemyAI.enemyHealth -= 3;
                Debug.Log("Enemy Hit");

            }

        }


    }

}
