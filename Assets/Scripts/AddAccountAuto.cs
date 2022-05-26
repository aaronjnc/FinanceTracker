using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddAccountAuto : MonoBehaviour
{
    [SerializeField]
    private Transform ContentObject;
    [SerializeField]
    private GameObject AccountLine;
    int lines = 0;

    public void AddNewLine()
    {
        GameObject line = Instantiate(AccountLine, ContentObject);
    }
}
