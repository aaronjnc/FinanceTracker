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
        else if (!f.accountFilter.Equals("") && !f.accountFilter.Equals(Account))
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
        return GetDate() + "|" + GetDescription() + "|" + GetAmount() + "|" + GetAccount() + "|" + GetTransactionType();
    }

    public string GetYearAndMonth()
    {
        return GetYear() + " " + GetMonth();
    }
}
