using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AccountOption : MonoBehaviour
{
    public enum AutomationType
    {
        Percentage,
        Amount,
        Remaining,
    }

    [SerializeField] 
    private TMP_Dropdown accountName;
    [SerializeField] 
    private TMP_Dropdown numberType;
    [SerializeField] 
    private TMP_InputField amount;

    public bool IsValid()
    {
        return GetAccountName() != "" && amount.text != "";
    }

    public string GetAccountName()
    {
        return accountName.options[accountName.value].text;
    }

    public AutomationType GetAutomationType()
    {
        string entry = numberType.options[numberType.value].text;
        if (entry.Equals("Percent")) {
            return AutomationType.Percentage;
        }
        else if (entry.Equals("Remaining"))
        {
            return AutomationType.Remaining;
        }
        else
        {
            return AutomationType.Amount;
        }
    }

    public int GetAmount()
    {
        return int.Parse(amount.text);
    }
}
