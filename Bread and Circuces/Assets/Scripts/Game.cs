using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Card;
using Meta;

public class Game : MonoBehaviour
{
    public Player Player, Enemy;

    public Game(bool one)
    {
        if (RunInfo.Instance != null)
        {
            RunInfo.Instance.isTutorial = false;
            Player = RunInfo.Instance.Player;
            Player.UpdateForNewGame();
        }
        else
        {
            Player = new Player();
            Player.team = Team.Player;
            Player.units.SelectUnits("Hoplomachus", "Murmillo");
        }

        Enemy = new Player();
        Enemy.team = Team.Enemy;
        var num = one ? 1 : 2;
        if (one)
            Player.units.SelectUnits("Murmillo");
        GenerateUnits(Enemy, num);

        Enemy.Deck = GiveDeckCard(Enemy);
        Player.Deck = GiveDeckCard(Player);
        var allPlayerCards = Player.Deck.Count;
        var notUniversalCards = Player.Deck.Where(x => x.Restriction == EnumCard.CardRestriction.Scissor).Count();
        Enemy.DiscardPile = new List<Card.Card>();
        Player.DiscardPile = new List<Card.Card>();
    }

    void GenerateUnits(Player player, int number)
    {
        var units = new string[4] { "Hoplomachus", "Murmillo", "Scissor", "Retiarius" };
        int first = Random.Range(0, 4);
        if(number == 1)
        {
            player.units.SelectUnits(units[first]);
            return;
        }
        var second = -1;
        while (second == first || second < 0)
            second = Random.Range(0, 4);
        player.units.SelectUnits(units[first], units[second]);
    }

    List<Card.Card> GiveDeckCard(Player player)
    {
        List<Card.Card> list = new List<Card.Card>();
        Debug.Log(CardManager.AllCards.Where(x => x.Restriction != EnumCard.CardRestriction.Universal).Count());
        foreach (var unit in player.units.units)
        {
            var unitDeck = CardManager.AllCards.Where(x => x.CardSet.ToString() == unit);
            foreach (var card in unitDeck)
                list.Add(card.GetCopy());
        }
        ShuffleDeck(list);
        return list;
    }

    public void ShuffleDeck(List<Card.Card> deck)
    {
        int n = deck.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            var value = deck[k];
            deck[k] = deck[n];
            deck[n] = value;
        }
    }
}
