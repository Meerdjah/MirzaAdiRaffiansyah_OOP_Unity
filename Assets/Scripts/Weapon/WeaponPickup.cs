using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] Weapon weaponHolder;
    Weapon weapon;

    //Method untuk mengaktifkan weapon ketika Player menyentuh Weapon Pickup
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Weapon existingWeapon = other.GetComponentInChildren<Weapon>();

            if (existingWeapon != null)
            {
                TurnVisual(false, existingWeapon);
            }

            weapon = Instantiate(weaponHolder);//Memberikan weapon ke Player
            weapon.transform.SetParent(other.transform);//Membuat weapon mengikuti pergerakan Player
            weapon.transform.position = other.transform.position;//Menyamakan posisi weapon dan Player
            TurnVisual(true);
        }
    }

    //Method untuk menonaktifkan Weapon Pickup
    void TurnVisual(bool On)
    {
        gameObject.SetActive(!On);
        weapon.gameObject.SetActive(On);
    }

    //Method untuk menonaktifkan Weapon Pickup dan menghapus Weapon sebelumnya yang digunakan
    void TurnVisual(bool On, Weapon weapon)
    {
        weapon.weaponPickup.gameObject.SetActive(!On);
        if (!On)
        {
            Destroy(weapon.gameObject);
        }
    }
}
