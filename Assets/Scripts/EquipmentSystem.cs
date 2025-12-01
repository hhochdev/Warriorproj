using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EquipmentSystem : MonoBehaviour
{
    [SerializeField] GameObject weaponHolder;
    [SerializeField] GameObject weapon;
    [SerializeField] GameObject weaponSheath;


    GameObject currentWeaponInHand;
    GameObject currentWeaponInSheath;
    void Start()
    {
        currentWeaponInSheath = Instantiate(weapon, weaponSheath.transform);
        currentWeaponInSheath.transform.localPosition = new Vector3(0, 0, 0);
        currentWeaponInSheath.transform.localRotation = quaternion.identity;
    }

    public void DrawWeapon()
    {
        currentWeaponInHand = Instantiate(weapon, weaponHolder.transform);
        currentWeaponInHand.transform.localPosition = new Vector3(0, 0, 0);
        currentWeaponInHand.transform.localRotation = quaternion.identity;
        Destroy(currentWeaponInSheath);
        Debug.Log("sheathdestroy");
    }

    public void SheathWeapon()
    {
        Debug.Log("2");
        currentWeaponInSheath = Instantiate(weapon, weaponSheath.transform);
        currentWeaponInSheath.transform.localPosition = new Vector3(0, 0, 0);
        currentWeaponInSheath.transform.localRotation = quaternion.identity;
        Destroy(currentWeaponInHand);
        Debug.Log("hand wep destroyed");
    }
}