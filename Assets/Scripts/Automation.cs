using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Automation
{
    private List<string> accounts = new List<string>();
    private List<bool> percent = new List<bool>();
    private List<double> amounts = new List<double>();

    public void AddRow(string account, bool percentage, double amount)
    {
        accounts.Add(account);
        percent.Add(percentage);
        amounts.Add(amount);
    }
}
