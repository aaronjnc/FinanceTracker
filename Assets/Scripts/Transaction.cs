using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transaction
{
    private string Date;
    private string Description;
    public double Amount;
    private Category category;
    private string TransactionType;
    private GameObject transactionRow;
    public delegate void OnCategoryChangeDelegate(Category newCategory);
    public event OnCategoryChangeDelegate OnCategoryChange;

    public Transaction(string Date, string Description, double Amount, Category c,
        string TransactionType)
    {
        this.Date = Date;
        this.Description = Description;
        this.Amount = Amount;
        category = c;
        this.TransactionType = TransactionType;
    }
    public void SetDate(string Date)
    {
        this.Date = Date;
    }
    public string GetDate()
    {
        return Date;
    }
    public string GetDescription()
    {
        return Description;
    }
    public double GetAmount()
    {
        return Amount;
    }
    public string GetAccountName()
    {
        return category.GetAccountName();
    }
    public Account GetAccount()
    {
        return category.GetAccount();
    }
    public Category GetCategory()
    {
        return category;
    }
    public string GetCategoryName()
    {
        return category.GetCategoryName();
    }
    public string GetTransactionType()
    {
        return TransactionType;
    }
    public int GetYear()
    {
        return int.Parse(Date.Split('/')[2]);
    }

    public int GetMonth()
    {
        return int.Parse(Date.Split('/')[1]);
    }

    public int GetDay()
    {
        return int.Parse(Date.Split('/')[0]);
    }

    public void SetCategory(Category c)
    {
        this.category = c;
        if (OnCategoryChange != null)
            OnCategoryChange(c);
    }
    public int CompareDate(Transaction t2)
    {
        if (GetYear() < t2.GetYear())
        {
            return -1;
        }
        else if (GetYear() > t2.GetYear())
        {
            return 1;
        }
        else
        {
            if (GetMonth() < t2.GetMonth())
            {
                return -1;
            }
            else if (GetMonth() > t2.GetMonth())
            {
                return 1;
            }
            else
            {
                if (GetDay() < t2.GetDay())
                {
                    return -1;
                }
                else if (GetDay() > t2.GetDay())
                {
                    return 1;
                }
                return 0;
            }
        }
    }

    public int CompareDesc(Transaction t2)
    {
        return Description.CompareTo(t2.Description);
    }

    public int CompareAmount(Transaction t2)
    {
        return Amount.CompareTo(t2.Amount);
    }

    public int CompareAccount(Transaction t2)
    {
        return t2.GetAccountName().CompareTo(t2.GetAccountName());
    }

    public int CompareType(Transaction t2)
    {
        return TransactionType.CompareTo(t2.TransactionType);
    }

    public bool FilterTransaction()
    {
        Filter f = Filter.Instance;
        if (f.dayFilter != -1 && f.dayFilter != GetDay())
        {
            return false;
        }
        else if (f.monthFilter != -1 && f.monthFilter != GetMonth())
        {
            return false;
        }
        else if (f.yearFilter != -1 && f.yearFilter != GetYear())
        {
            return false;
        }
        else if (f.greaterFilter != -1 && f.greaterFilter < Amount)
        {
            return false;
        }
        else if (f.lessFilter != -1 && f.lessFilter > Amount)
        {
            return false;
        }
        else if (f.equalFilter != -1 && f.equalFilter != Amount)
        {
            return false;
        }
        else if (!f.categoryFilter.Equals("") && !f.categoryFilter.Equals(category))
        {
            return false;
        }
        else if (!f.typeFilter.Equals("") && !f.typeFilter.Equals(TransactionType))
        {
            return false;
        }
        return true;
    }

    public override string ToString()
    {
        return GetDate() + "|" + GetDescription() + "|" + GetAmount() + "|" + GetCategoryName() + "|" + GetTransactionType();
    }

    public string GetYearAndMonth()
    {
        string month = "";
        if (GetMonth() < 10)
            month = "0" + GetMonth();
        else
            month += GetMonth();
        return GetYear() + " " + month;
    }

    public void SetAsAutomation()
    {
        category = TransactionManager.Instance.GetCategory("Automated");
    }

    public string GetMonthAndYear()
    {
        return MonthDropdownList.Months[GetMonth() - 1] + " " + GetYear();
    }
}
