using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TransactionManager : MonoBehaviour
{
    private static TransactionManager _instance;
    public static TransactionManager Instance
    {
        get
        {
            return _instance;
        }
    }
    [SerializeField]
    public GameObject TableRow;
    public Transform ScrollParent;
    private List<Transaction> transactions = new List<Transaction>();
    private List<Row> rows = new List<Row>();
    private List<string> Accounts = new List<string>();
    private List<string> TransactionTypes = new List<string>();
    public delegate void OnAccountNumberChangeDelegate(int newCount);
    public event OnAccountNumberChangeDelegate OnAccountNumberChange;
    public delegate void OnTypeNumberChangeDelegate(int newCount);
    public event OnTypeNumberChangeDelegate OnTypeNumberChange;
    private int SortingMethod = 0;
    private void Awake()
    {
        _instance = this;
    }

    public void AddTransaction(Transaction t)
    {
        transactions.Add(t);
        SortBy(SortingMethod);
    }
    public void DisplayTable()
    {
        if (rows.Count < transactions.Count)
        {
            for (int i = rows.Count; i < transactions.Count; i++)
            {
                GameObject row = Instantiate(TableRow, ScrollParent);
                rows.Add(row.GetComponent<Row>());
            }
        }
        for (int i = 0; i < transactions.Count; i++)
        {
            if (transactions[i].FilterTransaction())
            {
                rows[i].Display(transactions[i], i);
            }
            else
            {
                rows[i].Disable();
            }
        }
    }
    public void SortBy(int i)
    {
        switch (i)
        {
            case 0:
                transactions.Sort(SortByDate);
                break;
            case 1:
                transactions.Sort(SortByDescription);
                break;
            case 2:
                transactions.Sort(SortByAmount);
                break;
            case 3:
                transactions.Sort(SortByAccount);
                break;
            case 4:
                transactions.Sort(SortByType);
                break;
        }
        SortingMethod = i;
        DisplayTable();
    }
    public void AddFilter()
    {
        DisplayTable();
    }
    static int SortByDate(Transaction t1, Transaction t2)
    {
        return t1.CompareDate(t2);
    }
    static int SortByDescription(Transaction t1, Transaction t2)
    {
        return t1.CompareDesc(t2);
    }
    static int SortByAmount(Transaction t1, Transaction t2)
    {
        return t1.CompareAmount(t2);
    }
    static int SortByAccount(Transaction t1, Transaction t2)
    {
        return t1.CompareAccount(t2);
    }
    static int SortByType(Transaction t1, Transaction t2)
    {
        return t1.CompareType(t2);
    }
    public void AddAccount(TMP_InputField account)
    {
        if (TransactionTypes.Contains(account.text))
            return;
        Accounts.Add(account.text);
        account.text = "";
        OnAccountNumberChange(Accounts.Count);
    }
    public void RemoveAccount(TMP_Dropdown account)
    {
        string accountName = account.options[account.value].text;
        if (Accounts.Contains(accountName))
            Accounts.Remove(accountName);
        OnAccountNumberChange(Accounts.Count);
    }
    public void AddType(TMP_InputField transactionType)
    {
        if (TransactionTypes.Contains(transactionType.text))
            return;
        TransactionTypes.Add(transactionType.text);
        transactionType.text = "";
        OnTypeNumberChange(TransactionTypes.Count);
    }
    public void RemoveType(TMP_Dropdown transactionType)
    {
        string typeName = transactionType.options[transactionType.value].text;
        if (TransactionTypes.Contains(typeName))
            TransactionTypes.Remove(typeName);
        OnTypeNumberChange(TransactionTypes.Count);
    }

    public List<string> GetAccounts()
    {
        return Accounts;
    }

    public List<string> GetTypes()
    {
        return TransactionTypes;
    }
}
