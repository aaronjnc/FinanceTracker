using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ImplementAutomation : MonoBehaviour
{
    [SerializeField] 
    private TMP_Dropdown typeName;
    [SerializeField] 
    private GameObject contentObject;

    public void Implement()
    {
        Automation auto = new Automation();
        foreach (AccountOption a in contentObject.GetComponentsInChildren<AccountOption>())
        {
            if (!a.IsValid())
                continue;
            auto.AddRow(a.GetCategoryName(), a.GetAutomationType(), a.GetAmount());
        }
        TransactionManager.Instance.AddAutomation(typeName.options[typeName.value].text, auto);
    }
}
