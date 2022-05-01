using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Card
{
    public string Name;
    public Sprite Logo;
    public int Attack, Defense, Manacost;
    public bool IsPlaced;

    public bool IsAlive
    {
        get
        {
            return Defense > 0;
        }
    }

    public Card(string name, string logoName, int attack, int defense, int manacost)
    {
        Name = name;
        Logo = Resources.Load<Sprite>(logoName);
        Attack = attack;
        Defense = defense;
        Manacost = manacost;
        IsPlaced = false;
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

        CardManager.AllCards.Add(new Card("CardOne", "Sprites/LogoCards/CardOne", 1, 1, 2));
        CardManager.AllCards.Add(new Card("CardTwo", "Sprites/LogoCards/CardTwo", 2, 2, 2));
        CardManager.AllCards.Add(new Card("CardThree", "Sprites/LogoCards/CardThree", 3, 3, 2));
        CardManager.AllCards.Add(new Card("CardFour", "Sprites/LogoCards/CardFour", 4, 4, 2));
        CardManager.AllCards.Add(new Card("CardFive", "Sprites/LogoCards/CardFive", 5, 5, 2));
        CardManager.AllCards.Add(new Card("CardSix", "Sprites/LogoCards/CardSix", 6, 6, 2));
        CardManager.AllCards.Add(new Card("CardSeven", "Sprites/LogoCards/CardSeven", 7, 7, 2));
        CardManager.AllCards.Add(new Card("CardEight", "Sprites/LogoCards/CardEight", 8, 8, 2));

    }
}
