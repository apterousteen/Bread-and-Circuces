using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBand
{
    public List<UnitInfo> bandMembers;

    public string[] units;

    public UnitBand()
    {
        bandMembers = new List<UnitInfo>();
    }

    public void SelectUnits(string unitA, string unitB)
    {
        units = new string[2] { unitA, unitB };
    }
}
