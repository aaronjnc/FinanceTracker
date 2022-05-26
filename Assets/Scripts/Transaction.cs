using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transaction
{
    private string Date;
    private string Description;
    private double Amount;
    private string Account;
    private string TransactionType;
    public Transaction(string Date, string Description, double Amount, string Account,
        string TransactionType)
    {
        this.Date = Date;
        this.Description = Description;
        this.Amount = Amount;
        this.Account = Account;
        this.TransactionType = TransactionType;
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
    public string GetAccount()
    {
        return Account;
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
        return Account.CompareTo(t2.Account);
    }

    public int CompareType(Transaction t2)
    {
        return TransactionType.CompareTo(t2.TransactionType);
    }

    public bool FilterTransaction()
    {
        Filter f = Filter.Instance;
        if (f.DayFilter != -1 && f.DayFilter != GetDay())
        {
            return false;
        }
        else if (f.MonthFilter != -1 && f.MonthFilter != GetMonth())
        {
            return false;
        }
        else if (f.YearFilter != -1 && f.YearFilter != GetYear())
        {
            return false;
        }
        else if (f.GreaterFilter != -1 && f.GreaterFilter < Amount)
        {
            return false;
        }
        else if (f.LessFilter != -1 && f.LessFilter > Amount)
        {
            return false;
        }
        else if (f.EqualFilter != -1 && f.EqualFilter != Amount)
        {
            return false;
        }
        else if (!f.AccountFilter.Equals("") && !f.AccountFilter.Equals(Account))
        {
            return false;
        }
        else if (!f.TypeFilter.Equals("") && !f.TypeFilter.Equals(TransactionType))
        {
            return false;
        }
        return true;
    }
}
