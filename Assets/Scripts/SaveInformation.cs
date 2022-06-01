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
        TransactionManager.Instance.SaveMonths(path + "/transactions");
        string typeString = TransactionManager.Instance.GetTypesString();
        File.WriteAllText(path + "/types.txt", typeString);
        string accountString = TransactionManager.Instance.GetAccountsString();
        File.WriteAllText(path + "/accounts.txt", accountString);
        if (!Directory.Exists(path + "/automations"))
            Directory.CreateDirectory(path + "/automations");
        TransactionManager.Instance.SaveAutomations(path + "/automations");
    }

    public static void Load()
    {
        path = Application.persistentDataPath;
    }

    public static void LoadMonth(string YearAndMonth)
    {
        path = Path.Combine(Application.persistentDataPath, "transactions", YearAndMonth + ".txt");
        string text = File.ReadAllText(path);
        StringReader stringReader = new StringReader(text);
        string line = stringReader.ReadLine();
        while (line != null && line != "")
        {
            string[] parts = line.Split('|');
            Transaction t = new Transaction(parts[0], parts[1], int.Parse(parts[2]), 
                TransactionManager.Instance.GetCategory(parts[3]), parts[4]);
            TransactionManager.Instance.UpdateTransactions(t);
        }
    }
}
