using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class SelectWeapon : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> weaponList = new List<GameObject>();
    [SerializeField]
    private ThrownSystem thrownSystem;
    private int currentWeapon;
    [SerializeField]
    private GameObject panel;
    private void Awake()
    {
        currentWeapon = 0;
        thrownSystem.Initialized(weaponList[currentWeapon]);
    }
    
    public void SetWeapon(int weapon)
    {
        if (weapon > weaponList.Count - 1) return;

        currentWeapon = weapon; 

        thrownSystem.SetCurrentWeapon(weaponList[currentWeapon]);
    }

    public void ClosePanel()
    {
        GameManager.Instance.isPause = false;
        panel.SetActive(false);
    }
}
