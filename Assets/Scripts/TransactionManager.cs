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
    private List<string> loadedMonths = new List<string>();
    private List<string> loadedYears = new List<string>();
    private float totalAmount = 0;
    private int SortingMethod = 0;
    [SerializeField]
    private TMP_Dropdown monthsDropdown;
    private Row chosenRow;
    [SerializeField]
    private Button RemoveRow;
    private void Awake()
    {
        _instance = this;
        monthsDropdown.ClearOptions();
        totalTextBox.text = totalAmount.ToString("C2");
    }

    private void Start()
    {
        SaveInformation.Load();
        AddCategory("Automated", "Automated");
    }

    public void UpdateTransactions(Transaction t)
    {
        if (automations.ContainsKey(t.GetTransactionType()))
        {
            List<Transaction> transactions = automations[t.GetTransactionType()].CreateTransactions(t);
            t.SetAsAutomation();
            AddTransaction(t);
            foreach (Transaction transaction in transactions)
            {
                AddTransaction(transaction);
            }
        }
        else
        {
            AddTransaction(t);
        }
        SortBy(SortingMethod);
    }
    public void LoadTransaction(Transaction t)
    {
        if (!transactionsDictionary.ContainsKey(t.GetYearAndMonth()))
        {
            transactionsDictionary.Add(t.GetYearAndMonth(), new List<Transaction>());
        }
        transactionsDictionary[t.GetYearAndMonth()].Add(t);
        SortBy(SortingMethod);
    }
    public void AddTransaction(Transaction t)
    {
        if (!transactionsDictionary.ContainsKey(t.GetYearAndMonth()))
        {
            transactionsDictionary.Add(t.GetYearAndMonth(), new List<Transaction>());
        }
        transactionsDictionary[t.GetYearAndMonth()].Add(t);
        if (!t.GetCategoryName().Equals("Automated"))
        {
            t.GetCategory().UpdateAmount(t.GetAmount());
            totalAmount += (float)t.GetAmount();
            totalTextBox.text = totalAmount.ToString("C2");
        }
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
        AddAccount(account.text);
        account.text = "";
    }
    public void AddAccount(string actName)
    {
        if (accounts.ContainsKey(actName) || actName.Equals(""))
            return;
        accounts.Add(actName, new Account(actName));
        AccountTotals.Instance.AddAccount(accounts[actName]);
        if (OnAccountNumberChange != null)
            OnAccountNumberChange(accounts.Count);
    }
    public void MoveAccount(GameObject parentObject)
    {
        TMP_Dropdown[] childrenDropdown = parentObject.GetComponentsInChildren<TMP_Dropdown>();
        string account1Name = childrenDropdown[0].options[childrenDropdown[0].value].text;
        string account2Name = childrenDropdown[1].options[childrenDropdown[1].value].text;
        Account oldAccount = accounts[account1Name];
        Account account = accounts[account2Name];
        oldAccount.MoveCategories(account);
        AccountTotals.Instance.SwitchAccount(accounts[account1Name], account);
        accounts.Remove(account1Name);
        if (OnAccountNumberChange != null)
            OnAccountNumberChange(accounts.Count);
    }
    public void AddType(TMP_InputField transactionType)
    {
        AddType(transactionType.text);
        transactionType.text = "";
    }
    public void AddType(string typeName)
    {
        if (TransactionTypes.Contains(typeName) || typeName == "")
            return;
        TransactionTypes.Add(typeName);
        if (OnTypeNumberChange != null)
            OnTypeNumberChange(TransactionTypes.Count);
    }
    public void RemoveType(TMP_Dropdown transactionType)
    {
        var typeName = transactionType.options[transactionType.value].text;
        if (TransactionTypes.Contains(typeName))
            TransactionTypes.Remove(typeName);
        if (OnTypeNumberChange != null)
            OnTypeNumberChange(TransactionTypes.Count);
    }
    public void AddCategory(TMP_InputField categoryInput)
    {
        TMP_Dropdown accountDropdown = categoryInput.gameObject.GetComponentInChildren<TMP_Dropdown>();
        var categoryName = categoryInput.text;
        var accountName = accountDropdown.options[accountDropdown.value].text;
        AddCategory(categoryName, accountName);
        categoryInput.text = "";
        accountDropdown.value = 0;
    }
    public Category AddCategory(string categoryName, string accountName)
    {
        if (categories.ContainsKey(categoryName) || categoryName == "")
            return null;
        if (accountName.Equals("") || 
            (!accounts.ContainsKey(accountName) && !accountName.Equals("Automated")))
            return null;
        Account act = null;
        if (!accountName.Equals("Automated"))
            act = GetAccount(accountName);
        Category newCat = new Category(categoryName, act);
        categories.Add(categoryName, newCat);
        CategoryTotals.Instance.AddCategory(newCat);
        if (OnCategoryNumberChange != null)
            OnCategoryNumberChange(categories.Count);
        return newCat;
    }

    public void MoveCategory(GameObject parentObject)
    {
        TMP_Dropdown[] dropdowns = parentObject.GetComponentsInChildren<TMP_Dropdown>();
        string originalCategory = dropdowns[0].options[dropdowns[0].value].text;
        string newCategory = dropdowns[1].options[dropdowns[1].value].text;
        Category c = GetCategory(newCategory);
        LoadAllMonths();
        foreach (List<Transaction> transactions in transactionsDictionary.Values)
        {
            foreach(Transaction t in transactions)
            {
                if (t.GetCategoryName().Equals(originalCategory))
                {
                    t.SetCategory(c);
                    c.UpdateAmount(t.GetAmount());
                }
            }
        }
        categories.Remove(originalCategory);
        CategoryTotals.Instance.RemoveCategory(originalCategory);
        if (OnCategoryNumberChange != null)
            OnCategoryNumberChange(categories.Count);
    }

    public void EditCategory(GameObject parentObject)
    {
        TMP_Dropdown[] dropdowns = parentObject.GetComponentsInChildren<TMP_Dropdown>();
        string category = dropdowns[0].options[dropdowns[0].value].text;
        string account = dropdowns[1].options[dropdowns[1].value].text;
        Category c = GetCategory(category);
        c.ChangeAccount(GetAccount(account));
    }

    public void RemoveCategory(TMP_Dropdown categoryDropdown)
    {
        var categoryName = categoryDropdown.options[categoryDropdown.value].text;
        if (!categories.ContainsKey(categoryName))
            return;
        categories.Remove(categoryName);
        CategoryTotals.Instance.RemoveCategory(categoryName);
        if (OnCategoryNumberChange != null)
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
        RemoveAutomation(typeName);
        automations.Add(typeName, auto);
    }

    public void RemoveAutomation(string typeName)
    {
        if (automations.ContainsKey(typeName))
            automations.Remove(typeName);
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
            accountString.AppendLine(account);
        }
        return accountString.ToString();
    }

    public string GetCategoriesString()
    {
        StringBuilder categoryString = new StringBuilder();
        foreach (string category in categories.Keys)
        {
            if (category.Equals("Automated"))
                continue;
            Category cat = categories[category];
            categoryString.AppendLine(category + " " + cat.GetAccountName() + " " + categories[category].GetCategoryValue());
        }
        return categoryString.ToString();
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

    public int GetCategoryCount()
    {
        return categories.Count;
    }

    public Automation GetAutomation(string typeName)
    {
        if (automations.ContainsKey(typeName))
            return automations[typeName];
        return null;
    }

    public void SaveAutomations(string path)
    {
        foreach (string key in automations.Keys)
        {
            string newPath = Path.Combine(path, key + ".txt");
            File.WriteAllText(newPath, automations[key].ToString());
        }
    }

    public void AddMonths(List<string> months)
    {
        monthsDropdown.AddOptions(months);
    }

    public void LoadMonth()
    {
        string monthAndYear = monthsDropdown.options[monthsDropdown.value].text;
        LoadMonth(monthAndYear, monthsDropdown.value);
        /*string yearAndMonth = MonthDropdownList.GetYearAndMonth(monthAndYear);
        monthsDropdown.options.RemoveAt(monthsDropdown.value);
        monthsDropdown.RefreshShownValue();
        SaveInformation.LoadMonth(yearAndMonth);*/
    }

    private void LoadMonth(string monthAndYear, int value)
    {
        string yearAndMonth = MonthDropdownList.GetYearAndMonth(monthAndYear);
        monthsDropdown.options.RemoveAt(value);
        monthsDropdown.RefreshShownValue();
        SaveInformation.LoadMonth(yearAndMonth);
    }

    public void LoadAllMonths()
    {
        for (int i = 0; i < monthsDropdown.options.Count; i++)
        {
            string monthAndYear = monthsDropdown.options[i].text;
            LoadMonth(monthAndYear, i);
            i--;
        }
    }
    
    public void LoadMoney(double amount)
    {
        totalAmount += (float)amount;
        totalTextBox.text = totalAmount.ToString("C2");
    }

    public void ChooseRow(Row r)
    {
        chosenRow = r;
        RemoveRow.interactable = true;
    }

    public void DeleteRow()
    {
        if (chosenRow == null)
            return;
        Transaction t = chosenRow.GetTransaction();
        Destroy(chosenRow.gameObject);
        chosenRow = null;
        transactionsDictionary[t.GetYearAndMonth()].Remove(t);
        RemoveRow.interactable = false;
    }
}
