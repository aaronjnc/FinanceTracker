using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PageSwitcher : MonoBehaviour
{
    [SerializeField] 
    private GameObject[] panels;

    private int currentPanel = 0;
    public void SwitchPage()
    {
        panels[currentPanel].SetActive(false);
        currentPanel = GetComponent<TMP_Dropdown>().value;
        panels[currentPanel].SetActive(true);
    }
}
