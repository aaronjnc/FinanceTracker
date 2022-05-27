using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseRow : MonoBehaviour
{
    private Row currentRow = null;
    public void ChangeRow(Row r)
    {
        if (currentRow != null)
            currentRow.DeselectRow();
        currentRow = r;
    }
}
