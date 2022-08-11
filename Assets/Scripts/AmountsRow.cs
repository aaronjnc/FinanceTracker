using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmountsRow : MonoBehaviour
{
    public enum AmountComparator
    {
        Equal,
        Less_Than,
        Greater_Than,
        To,
    }
    [SerializeField]
    private TMP_Text textInfo;
    private float amount1;
    private float amount2;
    private AmountComparator comparator;
    private bool inclusive;

    public void SingularAmount(float amount1, AmountComparator comp, bool included)
    {
        this.amount1 = amount1;
        comparator = comp;
        inclusive = included;
        if (inclusive)
            textInfo.text = "Include ";
        else
            textInfo.text = "Exclude ";
        string c = comp.ToString().Replace('_', ' ');
        textInfo.text += c + " " + string.Format("{0:C}", amount1);
    }

    public void TwoAmounts(float amount1, float amount2, bool included)
    {
        this.amount1 = amount1;
        this.amount2 = amount2;
        comparator = AmountComparator.To;
        inclusive = included;
        if (inclusive)
            textInfo.text = "Include ";
        else
            textInfo.text = "Exclude ";
        textInfo.text += string.Format("{0:C}", amount1) + " to " + string.Format("{0:C}", amount2);
    }
}
