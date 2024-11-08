using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] Weapon weaponHolder;
    Weapon weapon;

    void Start()
    {
        if (weapon != null)
        {
            TurnVisual(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Weapon existingWeapon = other.GetComponentInChildren<Weapon>();

            if (existingWeapon != null)
            {
                TurnVisual(false, existingWeapon);
            }

            weapon = Instantiate(weaponHolder);
            weapon.transform.SetParent(other.transform);
            weapon.transform.position = other.transform.position;
            TurnVisual(true);
        }
    }

    void TurnVisual(bool On)
    {
        gameObject.SetActive(!On);
    }

    void TurnVisual(bool On, Weapon weapon)
    {
        weapon.weaponPickup.gameObject.SetActive(!On);
        if (!On)
        {
            Destroy(weapon.gameObject);
        }
    }
}
