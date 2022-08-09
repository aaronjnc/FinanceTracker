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
    private TMP_Dropdown categoryName;
    [SerializeField] 
    private TMP_Dropdown numberType;
    [SerializeField] 
    private TMP_InputField amount;

    public bool IsValid()
    {
        return GetCategoryName() != "" && amount.text != "";
    }

    public string GetCategoryName()
    {
        return categoryName.options[categoryName.value].text;
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

    public void BeginSetValues(string categoryName, AutomationType automation, double amount)
    {
        StartCoroutine(Wait(categoryName, automation, amount));
    }

    public void SetValues(string categoryName, AutomationType automation, double amount)
    {
        for (int i = 0; i < this.categoryName.options.Count; i++)
        {
            if (this.categoryName.options[i].text == categoryName)
            {
                this.categoryName.value = i;
            }
        }
        switch (automation)
        {
            case AutomationType.Amount:
                numberType.value = 0;
                break;
            case AutomationType.Percentage:
                numberType.value = 1;
                break;
            default:
                numberType.value = 2;
                break;
        }
        this.amount.text = amount.ToString();
    }

    IEnumerator Wait(string categoryName, AutomationType automation, double amount)
    {
        yield return new WaitForEndOfFrame();
        SetValues(categoryName, automation, amount);
    }
}
