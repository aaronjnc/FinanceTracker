using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AccountLine : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI accountTotal;
    [SerializeField]
    private TextMeshProUGUI accountNameField;
    private Account account;
    public void SetAccount(Account act)
    {
        account = act;
        UpdateName(account.GetAccountName());
        account.OnAccountValueChange += UpdateValue;
    }
    public void UpdateName(string accountName)
    {
        accountNameField.text = accountName;
    }
    public void UpdateValue(double amount)
    {
        accountTotal.text = amount.ToString();
    }
}
