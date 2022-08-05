using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using static AccountOption;

public class Automation
{
    private List<string> category = new List<string>();
    private List<AutomationType> automationType = new List<AutomationType>();
    private List<double> amounts = new List<double>();

    public void AddRow(string account, AutomationType percentage, double amount)
    {
        category.Add(account);
        automationType.Add(percentage);
        amounts.Add(amount/100);
    }

    public List<Transaction> CreateTransactions(Transaction original)
    {
        List<Transaction> transactions = new List<Transaction>();
        double leftoverMoney = original.GetAmount();
        for (int i = 0; i < category.Count; i++)
        {
            double value = 0;
            if (automationType[i] == AutomationType.Percentage)
            {
                value = leftoverMoney * amounts[i];
            }
            else if (automationType[i] == AutomationType.Amount)
            {
                if (leftoverMoney > amounts[i])
                {
                    value = leftoverMoney;
                }
                else
                {
                    value = amounts[i];
                }
            }
            else
            {
                value = leftoverMoney;
            }
            leftoverMoney -= value;
            Transaction t = new Transaction(original.GetDate(), "Automatic Transfer", value, 
                TransactionManager.Instance.GetCategory(category[i]), "Automated");
            transactions.Add(t);
            if (leftoverMoney == 0)
                break;
        }
        return transactions;
    }

    public override string ToString()
    {
        StringBuilder stringRep = new StringBuilder();
        for (int i = 0; i < category.Count; i++)
        {
            stringRep.Append(category[i] + "|" + automationType[i].ToString() + "|" + amounts[i]);
        }
        return stringRep.ToString();
    }
}
