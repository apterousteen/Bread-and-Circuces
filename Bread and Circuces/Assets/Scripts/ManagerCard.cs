using System.Collections.Generic;
using UnityEngine;

public class Card
{
    public enum CardEffect
    {
        Damage,
        Defense,
        Survived,
        Movement,
        CardDrow,
        ResetCard,
        Status,
        ManaAdd,
        Type,
        Mechanics,
        No
    }
    public enum TargetType
    {
        NoTarget,
        This,
        Enemy,
        Ally
    }
    public enum Stance
    {
        Defensive,
        Advance,
        Attacking,
        Raging
    }

    public CardEffect FirstCardEff, FirstCardEffTwo;
    public Stance StartStance, EndStance;
    public TargetType SpellTarget;

    public string Name;
    public Sprite Logo;
    public int Manacost, SpellValue, SecondSpellValue;
    public bool IsPlaced;

    public Card(string name, string logoPath, int manacost, Stance startStance = 0, Stance endStance = 0,
        CardEffect firstCardEffect = 0, int spellValue = 0, CardEffect firstCardEffectTwo = 0, int secondSpellValue = 0,
        TargetType targetType = 0)
    {
        Name = name;
        Logo = Resources.Load<Sprite>(logoPath);
        Manacost = manacost;
        IsPlaced = false;

        FirstCardEff = firstCardEffect;
        FirstCardEffTwo = firstCardEffectTwo;
        StartStance = startStance;
        EndStance = endStance;

        SpellTarget = targetType;
        SpellValue = spellValue;
        SecondSpellValue = secondSpellValue;

    }

    public Card(Card card)
    {
        Name = card.Name;
        Logo = card.Logo;
        Manacost = card.Manacost;
        IsPlaced = false;

        FirstCardEff = card.FirstCardEff;
        FirstCardEffTwo = card.FirstCardEffTwo;
        StartStance = card.StartStance;
        EndStance = card.EndStance;

        SpellTarget = card.SpellTarget;
        SpellValue = card.SpellValue;
        SecondSpellValue = card.SecondSpellValue;

    }
    
    public Card GetCopy()
    {
        return new Card(this);
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
        //"Ретиарий"

        CardManager.AllCards.Add(new Card("Бросок сети", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Defensive, Card.Stance.Attacking, Card.CardEffect.Damage, 0, Card.CardEffect.Status, 0, Card.TargetType.Enemy));// Работа со способностью
        CardManager.AllCards.Add(new Card("Уворот", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Defensive, Card.Stance.Advance, Card.CardEffect.Defense, 3, Card.CardEffect.Movement, 1, Card.TargetType.This));
        CardManager.AllCards.Add(new Card("Уворот", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Defensive, Card.Stance.Advance, Card.CardEffect.Defense, 3, Card.CardEffect.Movement, 1, Card.TargetType.This));
        CardManager.AllCards.Add(new Card("Уворот", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Defensive, Card.Stance.Advance, Card.CardEffect.Defense, 3, Card.CardEffect.Movement, 1, Card.TargetType.This));
        CardManager.AllCards.Add(new Card("Уворот", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Defensive, Card.Stance.Advance, Card.CardEffect.Defense, 3, Card.CardEffect.Movement, 1, Card.TargetType.This));
        CardManager.AllCards.Add(new Card("Тычок с отступлением", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Attacking, Card.Stance.Defensive, Card.CardEffect.Damage, 2, Card.CardEffect.Movement, 1, Card.TargetType.Enemy));
        CardManager.AllCards.Add(new Card("Тычок с отступлением", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Attacking, Card.Stance.Defensive, Card.CardEffect.Damage, 2, Card.CardEffect.Movement, 1, Card.TargetType.Enemy));
        CardManager.AllCards.Add(new Card("Протыкание ноги", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Attacking, Card.Stance.Attacking, Card.CardEffect.Damage, 4, Card.CardEffect.ResetCard, 1, Card.TargetType.Enemy));
        CardManager.AllCards.Add(new Card("Протыкание ноги", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Attacking, Card.Stance.Attacking, Card.CardEffect.Damage, 4, Card.CardEffect.ResetCard, 1, Card.TargetType.Enemy));
        CardManager.AllCards.Add(new Card("Осторожный удар", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Attacking, Card.Stance.Defensive, Card.CardEffect.Damage, 3, Card.CardEffect.CardDrow, 1, Card.TargetType.Enemy));//Корректировка способностей
        CardManager.AllCards.Add(new Card("Осторожный удар", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Attacking, Card.Stance.Defensive, Card.CardEffect.Damage, 3, Card.CardEffect.CardDrow, 1, Card.TargetType.Enemy));//Корректировка способностей
        CardManager.AllCards.Add(new Card("Выпад вперед", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Advance, Card.Stance.Attacking, Card.CardEffect.Movement, 1, Card.CardEffect.Damage, 2, Card.TargetType.Enemy));//Нужно добавить передвижение
        CardManager.AllCards.Add(new Card("Выпад вперед", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Advance, Card.Stance.Attacking, Card.CardEffect.Movement, 1, Card.CardEffect.Damage, 2, Card.TargetType.Enemy));//Нужно добавить передвижение
        CardManager.AllCards.Add(new Card("Выпад вперед", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Advance, Card.Stance.Attacking, Card.CardEffect.Movement, 1, Card.CardEffect.Damage, 2, Card.TargetType.Enemy));//Нужно добавить передвижение
        CardManager.AllCards.Add(new Card("Шаг назад", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Advance, Card.Stance.Defensive, Card.CardEffect.Defense, 1, Card.CardEffect.Movement, 1, Card.TargetType.This));//Работа со способностью
        CardManager.AllCards.Add(new Card("Шаг назад", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Advance, Card.Stance.Defensive, Card.CardEffect.Defense, 1, Card.CardEffect.Movement, 1, Card.TargetType.This));//Работа со способностью

        //Универсальные в сете "Скиссор"

        CardManager.AllCards.Add(new Card("Яростный рывок", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Advance, Card.Stance.Raging, Card.CardEffect.Damage, 4, Card.CardEffect.Movement, 0, Card.TargetType.Enemy));//Работа со способностью
        CardManager.AllCards.Add(new Card("Яростный рывок", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Advance, Card.Stance.Raging, Card.CardEffect.Damage, 4, Card.CardEffect.Movement, 0, Card.TargetType.Enemy));//Работа со способностью
        CardManager.AllCards.Add(new Card("Яростный рывок", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Advance, Card.Stance.Raging, Card.CardEffect.Damage, 4, Card.CardEffect.Movement, 0, Card.TargetType.Enemy));//Работа со способностью
        CardManager.AllCards.Add(new Card("Внезапный удар", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Advance, Card.Stance.Attacking, Card.CardEffect.Damage, 3, Card.CardEffect.CardDrow, 1, Card.TargetType.Enemy));
        CardManager.AllCards.Add(new Card("Внезапный удар", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Advance, Card.Stance.Attacking, Card.CardEffect.Damage, 3, Card.CardEffect.CardDrow, 1, Card.TargetType.Enemy));
        CardManager.AllCards.Add(new Card("Внезапный удар", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Advance, Card.Stance.Attacking, Card.CardEffect.Damage, 3, Card.CardEffect.CardDrow, 1, Card.TargetType.Enemy));
        CardManager.AllCards.Add(new Card("Rip and tear", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Raging, Card.Stance.Attacking, Card.CardEffect.Damage, 3, Card.CardEffect.ResetCard, 2, Card.TargetType.Enemy));//Работа со способностью
        CardManager.AllCards.Add(new Card("Яростная серия", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Raging, Card.Stance.Raging, Card.CardEffect.Damage, 2, Card.CardEffect.ManaAdd, 1, Card.TargetType.Enemy));
        CardManager.AllCards.Add(new Card("Зацепить оружие", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Attacking, Card.Stance.Raging, Card.CardEffect.Defense, 0, Card.CardEffect.CardDrow, 2, Card.TargetType.This));// + механика
        CardManager.AllCards.Add(new Card("Зацепить оружие", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Attacking, Card.Stance.Raging, Card.CardEffect.Defense, 0, Card.CardEffect.CardDrow, 2, Card.TargetType.This));// + механика
        CardManager.AllCards.Add(new Card("Разрезающий удар", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Attacking, Card.Stance.Advance, Card.CardEffect.Damage, 4, Card.CardEffect.CardDrow, 1, Card.TargetType.Enemy));
        CardManager.AllCards.Add(new Card("Разрезающий удар", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Attacking, Card.Stance.Advance, Card.CardEffect.Damage, 4, Card.CardEffect.CardDrow, 1, Card.TargetType.Enemy));
        CardManager.AllCards.Add(new Card("Удар клинком", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Attacking, Card.Stance.Attacking, Card.CardEffect.Damage, 3, Card.CardEffect.No, 0, Card.TargetType.Enemy));
        CardManager.AllCards.Add(new Card("Удар клинком", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Attacking, Card.Stance.Attacking, Card.CardEffect.Damage, 3, Card.CardEffect.No, 0, Card.TargetType.Enemy));
        CardManager.AllCards.Add(new Card("Удар клинком", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Attacking, Card.Stance.Attacking, Card.CardEffect.Damage, 3, Card.CardEffect.No, 0, Card.TargetType.Enemy));


        //Универсальные в сете "Мурмиллон"

        CardManager.AllCards.Add(new Card("Оглушение щитом", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Defensive, Card.Stance.Attacking, Card.CardEffect.Damage, 0, Card.CardEffect.ResetCard, 2, Card.TargetType.Enemy));//+механика
        CardManager.AllCards.Add(new Card("Оглушение щитом", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Defensive, Card.Stance.Attacking, Card.CardEffect.Damage, 0, Card.CardEffect.ResetCard, 2, Card.TargetType.Enemy));//+механика
        CardManager.AllCards.Add(new Card("Блок", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Defensive, Card.Stance.Defensive, Card.CardEffect.Defense, 2, Card.CardEffect.Type, 1, Card.TargetType.This));//Работа с типами карт
        CardManager.AllCards.Add(new Card("Блок", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Defensive, Card.Stance.Defensive, Card.CardEffect.Defense, 2, Card.CardEffect.Type, 1, Card.TargetType.This));//Работа с типами карт
        CardManager.AllCards.Add(new Card("Блок", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Defensive, Card.Stance.Defensive, Card.CardEffect.Defense, 2, Card.CardEffect.Type, 1, Card.TargetType.This));//Работа с типами карт
        CardManager.AllCards.Add(new Card("Уворот", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Defensive, Card.Stance.Advance, Card.CardEffect.Defense, 3, Card.CardEffect.Movement, 1, Card.TargetType.This));
        CardManager.AllCards.Add(new Card("Заверщающий рубец", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Attacking, Card.Stance.Advance, Card.CardEffect.Damage, 2, Card.CardEffect.Mechanics, 1, Card.TargetType.Enemy));//Работа со способностью карты
        CardManager.AllCards.Add(new Card("Заверщающий рубец", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Attacking, Card.Stance.Advance, Card.CardEffect.Damage, 2, Card.CardEffect.Mechanics, 1, Card.TargetType.Enemy));//Работа со способностью карты
        CardManager.AllCards.Add(new Card("Удар клинком", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Attacking, Card.Stance.Attacking, Card.CardEffect.Damage, 3, Card.CardEffect.No, 0, Card.TargetType.Enemy));
        CardManager.AllCards.Add(new Card("Удар клинком", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Attacking, Card.Stance.Attacking, Card.CardEffect.Damage, 3, Card.CardEffect.No, 0, Card.TargetType.Enemy));
        CardManager.AllCards.Add(new Card("Прикрыться", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Advance, Card.Stance.Defensive, Card.CardEffect.Defense, 1, Card.CardEffect.CardDrow, 1, Card.TargetType.This));// Работа со способностью карты
        CardManager.AllCards.Add(new Card("Прикрыться", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Advance, Card.Stance.Defensive, Card.CardEffect.Defense, 1, Card.CardEffect.CardDrow, 1, Card.TargetType.This));// Работа со способностью карты
        CardManager.AllCards.Add(new Card("Внезапный удар", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Advance, Card.Stance.Attacking, Card.CardEffect.Damage, 3, Card.CardEffect.CardDrow, 1, Card.TargetType.Enemy));
        CardManager.AllCards.Add(new Card("Внезапный удар", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Advance, Card.Stance.Attacking, Card.CardEffect.Damage, 3, Card.CardEffect.CardDrow, 1, Card.TargetType.Enemy));


        //Универсальные в сете "Гопломах"

        CardManager.AllCards.Add(new Card("Уворот", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Defensive, Card.Stance.Advance, Card.CardEffect.Defense, 3, Card.CardEffect.Movement, 1, Card.TargetType.This));
        CardManager.AllCards.Add(new Card("Уворот", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Defensive, Card.Stance.Advance, Card.CardEffect.Defense, 3, Card.CardEffect.Movement, 1, Card.TargetType.This));
        CardManager.AllCards.Add(new Card("Блок", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Defensive, Card.Stance.Defensive, Card.CardEffect.Defense, 2, Card.CardEffect.Type, 1, Card.TargetType.This));//Работа с типами карт
        CardManager.AllCards.Add(new Card("Блок", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Defensive, Card.Stance.Defensive, Card.CardEffect.Defense, 2, Card.CardEffect.Type, 1, Card.TargetType.This));//Работа с типами карт
        CardManager.AllCards.Add(new Card("Укол из-за щита", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Defensive, Card.Stance.Attacking, Card.CardEffect.Damage, 3, Card.CardEffect.CardDrow, 1, Card.TargetType.Enemy));
        CardManager.AllCards.Add(new Card("Укол из-за щита", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Defensive, Card.Stance.Attacking, Card.CardEffect.Damage, 3, Card.CardEffect.CardDrow, 1, Card.TargetType.Enemy));
        CardManager.AllCards.Add(new Card("Выпад вперед", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Advance, Card.Stance.Attacking, Card.CardEffect.Damage, 2, Card.CardEffect.Movement, 1, Card.TargetType.Enemy));
        CardManager.AllCards.Add(new Card("Выпад вперед", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Advance, Card.Stance.Attacking, Card.CardEffect.Damage, 2, Card.CardEffect.Movement, 1, Card.TargetType.Enemy));
        CardManager.AllCards.Add(new Card("Выпад вперед", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Advance, Card.Stance.Attacking, Card.CardEffect.Damage, 2, Card.CardEffect.Movement, 1, Card.TargetType.Enemy));
        CardManager.AllCards.Add(new Card("Прикрыться", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Advance, Card.Stance.Defensive, Card.CardEffect.Defense, 1, Card.CardEffect.CardDrow, 1, Card.TargetType.This));// Работа со способностью карты
        CardManager.AllCards.Add(new Card("Прикрыться", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Advance, Card.Stance.Defensive, Card.CardEffect.Defense, 1, Card.CardEffect.CardDrow, 1, Card.TargetType.This));// Работа со способностью карты
        CardManager.AllCards.Add(new Card("Тычок с отступлением", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Attacking, Card.Stance.Defensive, Card.CardEffect.Damage, 2, Card.CardEffect.Movement, 1, Card.TargetType.Enemy));// Работа с передвижением
        CardManager.AllCards.Add(new Card("Тычок с отступлением", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Attacking, Card.Stance.Defensive, Card.CardEffect.Damage, 2, Card.CardEffect.Movement, 1, Card.TargetType.Enemy));// Работа с передвижением
        CardManager.AllCards.Add(new Card("Осторожный удар", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Attacking, Card.Stance.Advance, Card.CardEffect.Damage, 3, Card.CardEffect.CardDrow, 1, Card.TargetType.Enemy));//Работа со способностью карты
        CardManager.AllCards.Add(new Card("Осторожный удар", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Attacking, Card.Stance.Advance, Card.CardEffect.Damage, 3, Card.CardEffect.CardDrow, 1, Card.TargetType.Enemy));//Работа со способностью карты
        CardManager.AllCards.Add(new Card("Преследование", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Attacking, Card.Stance.Attacking, Card.CardEffect.Damage, 4, Card.CardEffect.Movement, 1, Card.TargetType.Enemy));//передвижение
        CardManager.AllCards.Add(new Card("Преследование", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Attacking, Card.Stance.Attacking, Card.CardEffect.Damage, 4, Card.CardEffect.Movement, 1, Card.TargetType.Enemy));//передвижение

    }
}
