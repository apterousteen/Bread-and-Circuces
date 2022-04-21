using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Card
{
    public string Name;
    public Sprite Logo;
    public int Attack, Defense, CostCast;

    public bool IsAlive
    {
        get
        {
            return Defense > 0; 
        }
    }

    public Card(string name, string logoName, int attack, int defense, int costCast)
    {
        Name = name;
        Logo = Resources.Load<Sprite>(logoName);
        Attack = attack;
        Defense = defense;
        CostCast = costCast;
    }

    public void GetDamage(int dmg)
    {
        Defense -= dmg;
    }
}

public static class CardManager
{
    public static List<Card> AllCards = new List<Card>();
}

public class ManagerCard : MonoBehaviour
{
    public void Awake()
    {

        CardManager.AllCards.Add(new Card("CardOne", "Sprites/LogoCards/CardOne", 1, 1, 1));
        CardManager.AllCards.Add(new Card("CardTwo", "Sprites/LogoCards/CardTwo", 2, 2, 1));
        CardManager.AllCards.Add(new Card("CardThree", "Sprites/LogoCards/CardThree", 3, 3, 1));
        CardManager.AllCards.Add(new Card("CardFour", "Sprites/LogoCards/CardFour", 4, 4, 1));
        CardManager.AllCards.Add(new Card("CardFive", "Sprites/LogoCards/CardFive", 5, 5, 1));
        CardManager.AllCards.Add(new Card("CardSix", "Sprites/LogoCards/CardSix", 6, 6, 1));
        CardManager.AllCards.Add(new Card("CardSeven", "Sprites/LogoCards/CardSeven", 7, 7, 1));
        CardManager.AllCards.Add(new Card("CardEight", "Sprites/LogoCards/CardEight", 8, 8, 1));

    }
}
