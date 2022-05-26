using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AccountOption : MonoBehaviour
{
    [SerializeField] 
    private TMP_Dropdown accountName;
    [SerializeField] 
    private TMP_Dropdown numberType;
    [SerializeField] 
    private TMP_InputField amount;

    public string GetAccountName()
    {
        return accountName.options[accountName.value].text;
    }

    public bool IsPercentage()
    {
        return numberType.options[accountName.value].text.Equals("Percent");
    }

    public int GetAmount()
    {
        return int.Parse(amount.text);
    }
}
