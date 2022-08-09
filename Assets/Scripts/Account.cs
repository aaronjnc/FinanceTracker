using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Account
{
    private string accountName;
    private double accountValue = 0;
    private List<Category> childCategories = new List<Category>();
    public delegate void OnAccountValueChangeDelegate(double accountTotal);
    public event OnAccountValueChangeDelegate OnAccountValueChange;
    public Account(string accountName)
    {
        this.accountName = accountName;
    }

    public void AddCategory(Category c)
    {
        childCategories.Add(c);
        UpdateAmount();
    }

    public void RemoveCategory(Category c)
    {
        if (childCategories.Contains(c))
        {
            childCategories.Remove(c);
        }
        UpdateAmount();
    }

    public void MoveCategories(Account newAccount)
    {
        for (int i = 0; i < childCategories.Count; i++)
        {
            childCategories[i].ChangeAccount(newAccount);
            i--;
        }
    }

    public void UpdateAmount()
    {
        accountValue = 0;
        foreach (Category c in childCategories)
        {
            accountValue += c.GetCategoryValue();
        }
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
