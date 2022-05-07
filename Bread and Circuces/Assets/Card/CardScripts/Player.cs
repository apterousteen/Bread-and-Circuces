using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public int Mana, Manapool;
    const int MAX_MANAPOOL = 4;

    public Player()
    {
        Mana = Manapool = 4;
    }

    public void RestoreRoundMana()
    {
        Mana = Manapool = 4;
    }

    /*public void IncreaseManapool()
    {
        
    }*/

    public void GetDamage(int damage)
    {

    }
}
