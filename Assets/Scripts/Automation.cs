using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Automation
{
    private List<string> category = new List<string>();
    private List<bool> percent = new List<bool>();
    private List<double> amounts = new List<double>();

    public void AddRow(string account, bool percentage, double amount)
    {
        category.Add(account);
        percent.Add(percentage);
        amounts.Add(amount/100);
    }

    public List<Transaction> CreateTransactions(Transaction original)
    {
        List<Transaction> transactions = new List<Transaction>();
        double leftoverMoney = original.GetAmount();
        for (int i = 0; i < category.Count; i++)
        {
            double value = 0;
            if (percent[i])
            {
                value = leftoverMoney * amounts[i];
            }
            else
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
            stringRep.Append(category[i] + "|" + percent[i] + "|" + amounts[i]);
        }
        return stringRep.ToString();
    }
}
