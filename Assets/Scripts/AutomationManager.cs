using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AutomationManager : MonoBehaviour
{
    [SerializeField]
    private GameObject AddButton;
    [SerializeField]
    private GameObject RemoveButton;
    [SerializeField]
    private TMP_Dropdown TypesDropdown;
    private Transform contentObject;
    [SerializeField]
    private GameObject automationRow;
    private string TypeName;
    private void Start()
    {
        contentObject = GetComponentInChildren<ContentSizeFitter>().gameObject.transform;
    }
    public void TypeUpdated()
    {
        ClearRows();
        TypeName = TypesDropdown.options[TypesDropdown.value].text;
        if (TypesDropdown.value == 0)
        {
            AddButton.SetActive(false);
            RemoveButton.SetActive(false);
            return;
        }
        AddButton.SetActive(true);
        RemoveButton.SetActive(true);
        RemoveButton.GetComponent<Button>().interactable = false;
        Automation a = TransactionManager.Instance.GetAutomation(TypeName);
        if (a != null)
        {
            a.SpawnRows(contentObject, automationRow);
        }
    }

    public void ClearRows()
    {
        for (int i = contentObject.childCount - 1; i >= 0; i--)
        {
            Destroy(contentObject.GetChild(i).gameObject);
        }
    }

    public void RemoveAutomation()
    {
        TransactionManager.Instance.RemoveAutomation(TypeName);
        ClearRows();
    }

    public void SaveAutomation()
    {
        Automation auto = new Automation();
        foreach (AccountOption a in GetComponentsInChildren<AccountOption>())
        {
            if (!a.IsValid())
                continue;
            auto.AddRow(a.GetCategoryName(), a.GetAutomationType(), a.GetAmount());
        }
        TransactionManager.Instance.AddAutomation(TypeName, auto);
        TypesDropdown.value = 0;
    }
}
