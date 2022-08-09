using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Category
{
    private string categoryName;
    private double categoryValue = 0;
    private Account parentAccount;
    public delegate void OnCategoryValueChangeDelegate(double accountTotal);
    public event OnCategoryValueChangeDelegate OnCategoryValueChange;
    public Category(string categoryName, Account parentAccount)
    {
        this.categoryName = categoryName;
        this.parentAccount = parentAccount;
        parentAccount?.AddCategory(this);
    }

    public void UpdateAmount(double amount)
    {
        categoryValue += amount;
        parentAccount.UpdateAmount();
        OnCategoryValueChange(categoryValue);
    }

    public string GetAccountName()
    {
        return parentAccount.GetAccountName();
    }

    public void ChangeAccount(Account newAccount)
    {
        if (newAccount == parentAccount)
            return;
        parentAccount.RemoveCategory(this);
        parentAccount = newAccount;
        parentAccount.AddCategory(this);
    }

    public Account GetAccount()
    {
        return parentAccount;
    }

    public string GetCategoryName()
    {
        return categoryName;
    }

    public double GetCategoryValue()
    {
        return categoryValue;
    }
}
