using System.Collections;
using System.Collections.Generic;
using Card;
using UnityEngine;

public class AI : MonoBehaviour
{
    public void MakeTurn()
    {
        StartCoroutine(EnemyTurn(GameManagerScript.Instance.CurrentGame.Enemy.HandCards));//рука противника
    }
    IEnumerator EnemyTurn(List<CardController> cards)
    {
        for (int count = GameManagerScript.Instance.CurrentGame.Enemy.Mana; count > 0; count--)
        {
            if (GameManagerScript.Instance.CurrentGame.Enemy.HandCards.Count == 0)
                break;

        }
        yield return new WaitForSeconds(.5f);
    }

    void CastSpell(CardController card)
    {
    }
}

