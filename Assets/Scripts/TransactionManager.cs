using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;
using System.IO;

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
    private GameObject TableRow;
    [SerializeField]
    private Transform ScrollParent;
    private Dictionary<string, List<Transaction>> transactionsDictionary = new Dictionary<string, List<Transaction>>();
    private List<Row> rows = new List<Row>();
    private Dictionary<string, Account> accounts = new Dictionary<string, Account>();
    private List<string> TransactionTypes = new List<string>();
    private Dictionary<string, Category> categories = new Dictionary<string, Category>();
    private Dictionary<string, Automation> automations = new Dictionary<string, Automation>();
    public delegate void OnAccountNumberChangeDelegate(int newCount);
    public event OnAccountNumberChangeDelegate OnAccountNumberChange;
    public delegate void OnTypeNumberChangeDelegate(int newCount);
    public event OnTypeNumberChangeDelegate OnTypeNumberChange;
    public delegate void OnCategoryNumberChangeDelegate(int newCount);
    public event OnCategoryNumberChangeDelegate OnCategoryNumberChange;
    [SerializeField]
    private TextMeshProUGUI totalTextBox;
    private float totalAmount = 0;
    private int SortingMethod = 0;
    private void Awake()
    {
        _instance = this;
        totalTextBox.text = totalAmount.ToString("C2");
    }

    public void UpdateTransactions(Transaction t)
    {
        AddTransaction(t);
        if (automations.ContainsKey(t.GetTransactionType()))
        {
            List<Transaction> transactions = automations[t.GetTransactionType()].CreateTransactions(t);
            foreach (Transaction transaction in transactions)
            {
                AddTransaction(transaction);
            }
        }
        SortBy(SortingMethod);
    }
    public void AddTransaction(Transaction t)
    {
        if (!transactionsDictionary.ContainsKey(t.GetYearAndMonth()))
        {
            transactionsDictionary.Add(t.GetYearAndMonth(), new List<Transaction>());
        }
        transactionsDictionary[t.GetYearAndMonth()].Add(t);
        totalAmount += (float)t.GetAmount();
        totalTextBox.text = totalAmount.ToString("C2");
    }
    private void DisplayTable()
    {
        int i = 0;
        foreach (string key in transactionsDictionary.Keys)
        {
            foreach (Transaction t in transactionsDictionary[key])
            {
                if (i >= rows.Count)
                    AddNewRow();
                if (t.FilterTransaction())
                {
                    rows[i].Display(t, i);
                }
                else
                {
                    rows[i].Disable();
                }
                i++;
            }
        }
    }
    public void AddNewRow()
    {
        GameObject row = Instantiate(TableRow, ScrollParent);
        row.transform.localPosition = new Vector3(0, 0, 0);
        rows.Add(row.GetComponent<Row>());
    }
    private void SortBy(int i)
    {
        switch (i)
        {
            case 0:
                SortAllLists(SortByDate);
                break;
            case 1:
                SortAllLists(SortByDescription);
                break;
            case 2:
                SortAllLists(SortByAmount);
                break;
            case 3:
                SortAllLists(SortByAccount);
                break;
            case 4:
                SortAllLists(SortByType);
                break;
        }
        SortingMethod = i;
        DisplayTable();
    }

    private void SortAllLists(Comparison<Transaction> sortBy)
    {
        foreach (List<Transaction> v in transactionsDictionary.Values)
        {
            v.Sort(sortBy);
        }
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
        if (TransactionTypes.Contains(account.text) || account.text == "")
            return;
        accounts.Add(account.text, new Account(account.text));
        AccountTotals.Instance.AddAccount(accounts[account.text]);
        account.text = "";
        OnAccountNumberChange(accounts.Count);
    }
    public void RemoveAccount(TMP_Dropdown account)
    {
        var accountName = account.options[account.value].text;
        if (!accounts.ContainsKey(accountName))
            return;
        accounts.Remove(accountName);
        AccountTotals.Instance.RemoveAccount(accountName);
        OnAccountNumberChange(accounts.Count);
    }
    public void AddType(TMP_InputField transactionType)
    {
        if (TransactionTypes.Contains(transactionType.text) || transactionType.text == "")
            return;
        TransactionTypes.Add(transactionType.text);
        transactionType.text = "";
        OnTypeNumberChange(TransactionTypes.Count);
    }
    public void RemoveType(TMP_Dropdown transactionType)
    {
        var typeName = transactionType.options[transactionType.value].text;
        if (TransactionTypes.Contains(typeName))
            TransactionTypes.Remove(typeName);
        OnTypeNumberChange(TransactionTypes.Count);
    }

    public void AddCategory(TMP_InputField categoryInput)
    {
        TMP_Dropdown accountDropdown = categoryInput.gameObject.GetComponentInChildren<TMP_Dropdown>();
        var categoryName = categoryInput.text;
        if (categories.ContainsKey(categoryName) || categoryName == "")
        {
            return;
        }
        Account act = accounts[accountDropdown.options[accountDropdown.value].text];
        if (act == null)
            return;
        categories.Add(categoryName, new Category(categoryName, act));
        categoryInput.text = "";
        accountDropdown.value = 0;
        CategoryTotals.Instance.AddCategory(categories[categoryName]);
        OnCategoryNumberChange(categories.Count);
    }

    public void RemoveCategory(TMP_Dropdown categoryDropdown)
    {
        var categoryName = categoryDropdown.options[categoryDropdown.value].text;
        if (!categories.ContainsKey(categoryName))
            return;
        categories.Remove(categoryName);
        CategoryTotals.Instance.RemoveCategory(categoryName);
        OnCategoryNumberChange(categories.Count);
    }

    public List<string> GetAccounts()
    {
        return new List<string>(accounts.Keys);
    }

    public List<string> GetTypes()
    {
        return TransactionTypes;
    }

    public List<string> GetCategories()
    {
        return new List<string>(categories.Keys);
    }

    public int GetAccountCount()
    {
        return accounts.Count;
    }

    public int GetTypeCount()
    {
        return TransactionTypes.Count;
    }

    public void AddAutomation(string typeName, Automation auto)
    {
        if (automations.ContainsKey(typeName))
            automations.Remove(typeName);
        automations.Add(typeName, auto);
    }

    public void SaveMonths(string path)
    {
        foreach (string key in transactionsDictionary.Keys)
        {
            StringBuilder monthString = new StringBuilder();
            foreach (Transaction t in transactionsDictionary[key])
            {
                monthString.AppendLine(t.ToString());
            }
            string newPath = Path.Combine(path, key + ".txt");
            File.WriteAllText(newPath, monthString.ToString());
        }
    }

    public string GetAccountsString()
    {
        StringBuilder accountString = new StringBuilder();
        foreach (string account in accounts.Keys)
        {
            accountString.AppendLine(account + " " + accounts[account].GetAccountValue());
        }
        return accountString.ToString();
    }

    public string GetTypesString()
    {
        StringBuilder typeString = new StringBuilder();
        foreach (string transactionType in TransactionTypes)
        {
            typeString.AppendLine(transactionType);
        }
        return typeString.ToString();
    }

    public void SaveInfo()
    {
        SaveInformation.Save();
    }

    public Account GetAccount(string account)
    {
        return accounts[account];
    }

    public Category GetCategory(string category)
    {
        return categories[category];
    }

    public void SaveAutomations(string path)
    {
        foreach (string key in automations.Keys)
        {
            string newPath = Path.Combine(path, key + ".txt");
            File.WriteAllText(newPath, automations[key].ToString());
        }
    }
}
