using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Filter : MonoBehaviour
{
    private static Filter _instance;
    public static Filter Instance
    {
        get
        {
            return _instance;
        }
    }
    [HideInInspector]
    public int DayFilter = -1;
    [HideInInspector]
    public int MonthFilter = -1;
    [HideInInspector]
    public int YearFilter = -1;
    [HideInInspector]
    public int GreaterFilter = -1;
    [HideInInspector]
    public int LessFilter = -1;
    [HideInInspector]
    public int EqualFilter = -1;
    [HideInInspector]
    public string AccountFilter = "";
    [HideInInspector]
    public string TypeFilter = "";
    private void Awake()
    {
        _instance = this;
    }
}
