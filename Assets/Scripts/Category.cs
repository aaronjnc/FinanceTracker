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
    }


    public void UpdateAmount(double amount)
    {
        categoryValue += amount;
        OnCategoryValueChange(categoryValue);
    }

    public string GetAccountName()
    {
        return categoryName;
    }

    public double GetAccountValue()
    {
        return categoryValue;
    }
}
