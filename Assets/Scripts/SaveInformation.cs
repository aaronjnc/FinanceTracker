using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class SaveInformation
{
    private static string path = "";

    public static void Save()
    {
        path = Application.persistentDataPath;
        if (!Directory.Exists(path + "/transactions"))
            Directory.CreateDirectory(path + "/transactions");
        TransactionManager.Instance.SaveMonths(path + "/transactions");
        string typeString = TransactionManager.Instance.GetTypesString();
        File.WriteAllText(path + "/types.txt", typeString);
        string accountString = TransactionManager.Instance.GetAccountsString();
        File.WriteAllText(path + "/accounts.txt", accountString);
        string categoryString = TransactionManager.Instance.GetCategoriesString();
        File.WriteAllText(path + "/category.txt", categoryString);
        if (!Directory.Exists(path + "/automations"))
            Directory.CreateDirectory(path + "/automations");
        TransactionManager.Instance.SaveAutomations(path + "/automations");
    }

    public static void Load()
    {
        path = Application.persistentDataPath;
        LoadAccounts(path);
        LoadCategories(path);
        LoadTypes(path);
        LoadMonthList(path);
        LoadAutomations(path);
    }

    private static void LoadMonthList(string monthPath)
    {
        monthPath = Path.Combine(monthPath, "transactions");
        if (!Directory.Exists(monthPath))
            return;
        string[] months = Directory.GetFiles(monthPath);
        List<string> monthList = new List<string>();
        for (int i = 0; i < months.Length; i++)
        {
            months[i] = Path.GetFileName(months[i]);
            months[i] = months[i].Substring(0, months[i].Length - 4);
            monthList.Add(MonthDropdownList.GetMonthAndYear(months[i]));
        }
        TransactionManager.Instance.AddMonths(monthList);
    }

    private static void LoadAccounts(string accountPath)
    {
        accountPath = Path.Combine(accountPath, "accounts.txt");
        if (!File.Exists(accountPath))
            return;
        StringReader accounts = new StringReader(File.ReadAllText(accountPath));
        string line = accounts.ReadLine();
        while (line != null && line != "")
        {
            TransactionManager.Instance.AddAccount(line);
            line = accounts.ReadLine();
        }
    }

    private static void LoadCategories(string categoryPath)
    {
        categoryPath = Path.Combine(categoryPath, "category.txt");
        if (!File.Exists(categoryPath))
            return;
        StringReader catReader = new StringReader(File.ReadAllText(categoryPath));
        string line = catReader.ReadLine();
        while (line != null && line != "")
        {
            string[] parts = line.Split(' ');
            Category c = TransactionManager.Instance.AddCategory(parts[0], parts[1]);
            double amount = double.Parse(parts[2]);
            c.UpdateAmount(amount);
            TransactionManager.Instance.LoadMoney(amount);
            line = catReader.ReadLine();
        }
    }

    private static void LoadTypes(string typePath)
    {
        typePath = Path.Combine(typePath, "types.txt");
        if (!File.Exists(typePath))
            return;
        StringReader typeReader = new StringReader(File.ReadAllText(typePath));
        string line = typeReader.ReadLine();
        while (line != null && line != "")
        {
            TransactionManager.Instance.AddType(line);
            line = typeReader.ReadLine();
        }
    }

    public static void LoadMonth(string YearAndMonth)
    {
        path = Path.Combine(Application.persistentDataPath, "transactions", YearAndMonth + ".txt");
        if (!File.Exists(path))
        {
            Debug.Log("invalid month " + YearAndMonth);
            return;
        }
        string text = File.ReadAllText(path);
        StringReader stringReader = new StringReader(text);
        string line = stringReader.ReadLine();
        while (line != null && line != "")
        {
            string[] parts = line.Split('|');
            Transaction t = new Transaction(parts[0], parts[1], int.Parse(parts[2]), 
                TransactionManager.Instance.GetCategory(parts[3]), parts[4]);
            TransactionManager.Instance.LoadTransaction(t);
            line = stringReader.ReadLine();
        }
    }

    private static void LoadAutomations(string path)
    {
        path = Path.Combine(path, "automations");
        if (!Directory.Exists(path))
            return;
        string[] automations = Directory.GetFiles(path);
        for (int i = 0; i < automations.Length; i++)
        {

        }
    }
}
