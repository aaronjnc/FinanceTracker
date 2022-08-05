using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Account
{
    private string accountName;
    private double accountValue = 0;
    public delegate void OnAccountValueChangeDelegate(double accountTotal);
    public event OnAccountValueChangeDelegate OnAccountValueChange;
    public Account(string accountName)
    {
        this.accountName = accountName;
    }

    public void UpdateAmount(double amount)
    {
        accountValue += amount;
        OnAccountValueChange(accountValue);
    }

    public string GetAccountName()
    {
        return accountName;
    }

    public double GetAccountValue()
    {
        return accountValue;
    }
}
