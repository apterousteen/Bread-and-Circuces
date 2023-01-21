using System.Collections;
using System.Collections.Generic;
using Card;
using UnityEngine;

public class Player
{
    public int Mana, Manapool, activatedUnits;
    public List<Card.Card> Deck, DiscardPile;
    public Team team;
    public UnitBand units; 
    public List<CardController> HandCards;
    const int MAX_MANAPOOL = 4;

    public Player()
    {
        Mana = Manapool = 4;
        activatedUnits = 0;
        Deck = new List<Card.Card>();
        DiscardPile = new List<Card.Card>();
        HandCards = new List<CardController>();
        units = new UnitBand();
    }

    public void UpdateForNewGame()
    {
        Mana = Manapool = 4;
        activatedUnits = 0;
        Deck = new List<Card.Card>();
        DiscardPile = new List<Card.Card>();
        HandCards = new List<CardController>();
    }

    public void RestoreRoundMana()
    {
        Mana = Manapool = 4;
    }

    public void SpellManapool()
    {
        if (Mana == MAX_MANAPOOL)
            return;
        else
            Mana += 1;
    }
}
