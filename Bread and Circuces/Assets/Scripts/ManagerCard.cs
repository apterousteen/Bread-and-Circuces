using System.Collections.Generic;
using UnityEngine;

public class Card
{

    public string Name;
    public Sprite Logo;
    public int Manacost;
    public bool IsPlaced;

    public bool IsSpell;

    public Card(string name, string logoName, int manacost)
    {
        Name = name;
        Logo = Resources.Load<Sprite>(logoName);
        Manacost = manacost;
        IsPlaced = false;

    }

    public Card(Card card)
    {
        Name = card.Name;
        Logo = card.Logo;
        Manacost = card.Manacost;
        IsPlaced = false;

    }

    public Card GetCopy()
    {
        return new Card(this);
    }
}

public class SpellCard : Card
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
    public int SpellValue;
    public int SecondSpellValue;

    public SpellCard(string name, string logoPath, int manacost, Stance startStance = 0, Stance endStance = 0,
        CardEffect firstCardEffect = 0, int spellValue = 0, CardEffect firstCardEffectTwo = 0, int secondSpellValue = 0,
        TargetType targetType = 0) : base(name, logoPath, manacost)
    {
        IsSpell = true;

        FirstCardEff = firstCardEffect;
        FirstCardEffTwo = firstCardEffectTwo;
        StartStance = startStance;
        EndStance = endStance;

        SpellTarget = targetType;
        SpellValue = spellValue;
        SecondSpellValue = secondSpellValue;
    }

    public SpellCard(SpellCard card) : base(card)
    {
        IsSpell = true;

        FirstCardEff = card.FirstCardEff;
        FirstCardEffTwo = card.FirstCardEffTwo;
        StartStance = card.StartStance;
        EndStance = card.EndStance;

        SpellTarget = card.SpellTarget;
        SpellValue = card.SpellValue;
        SecondSpellValue = card.SecondSpellValue;
    }

    public new SpellCard GetCopy()
    {
        return new SpellCard(this);
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

        CardManager.AllCards.Add(new SpellCard("Бросок сети", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Defensive, SpellCard.Stance.Attacking, SpellCard.CardEffect.Damage, 0, SpellCard.CardEffect.Status, 0, SpellCard.TargetType.Enemy));// Работа со способностью
        CardManager.AllCards.Add(new SpellCard("Уворот", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Defensive, SpellCard.Stance.Advance, SpellCard.CardEffect.Defense, 3, SpellCard.CardEffect.Movement, 1, SpellCard.TargetType.This));
        CardManager.AllCards.Add(new SpellCard("Уворот", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Defensive, SpellCard.Stance.Advance, SpellCard.CardEffect.Defense, 3, SpellCard.CardEffect.Movement, 1, SpellCard.TargetType.This));
        CardManager.AllCards.Add(new SpellCard("Уворот", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Defensive, SpellCard.Stance.Advance, SpellCard.CardEffect.Defense, 3, SpellCard.CardEffect.Movement, 1, SpellCard.TargetType.This));
        CardManager.AllCards.Add(new SpellCard("Уворот", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Defensive, SpellCard.Stance.Advance, SpellCard.CardEffect.Defense, 3, SpellCard.CardEffect.Movement, 1, SpellCard.TargetType.This));
        CardManager.AllCards.Add(new SpellCard("Тычок с отступлением", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Attacking, SpellCard.Stance.Defensive, SpellCard.CardEffect.Damage, 2, SpellCard.CardEffect.Movement, 1, SpellCard.TargetType.Enemy));
        CardManager.AllCards.Add(new SpellCard("Тычок с отступлением", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Attacking, SpellCard.Stance.Defensive, SpellCard.CardEffect.Damage, 2, SpellCard.CardEffect.Movement, 1, SpellCard.TargetType.Enemy));
        CardManager.AllCards.Add(new SpellCard("Протыкание ноги", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Attacking, SpellCard.Stance.Attacking, SpellCard.CardEffect.Damage, 4, SpellCard.CardEffect.ResetCard, 1, SpellCard.TargetType.Enemy));
        CardManager.AllCards.Add(new SpellCard("Протыкание ноги", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Attacking, SpellCard.Stance.Attacking, SpellCard.CardEffect.Damage, 4, SpellCard.CardEffect.ResetCard, 1, SpellCard.TargetType.Enemy));
        CardManager.AllCards.Add(new SpellCard("Осторожный удар", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Attacking, SpellCard.Stance.Defensive, SpellCard.CardEffect.Damage, 3, SpellCard.CardEffect.CardDrow, 1, SpellCard.TargetType.Enemy));//Корректировка способностей
        CardManager.AllCards.Add(new SpellCard("Осторожный удар", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Attacking, SpellCard.Stance.Defensive, SpellCard.CardEffect.Damage, 3, SpellCard.CardEffect.CardDrow, 1, SpellCard.TargetType.Enemy));//Корректировка способностей
        CardManager.AllCards.Add(new SpellCard("Выпад вперед", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Advance, SpellCard.Stance.Attacking, SpellCard.CardEffect.Movement, 1, SpellCard.CardEffect.Damage, 2, SpellCard.TargetType.Enemy));//Нужно добавить передвижение
        CardManager.AllCards.Add(new SpellCard("Выпад вперед", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Advance, SpellCard.Stance.Attacking, SpellCard.CardEffect.Movement, 1, SpellCard.CardEffect.Damage, 2, SpellCard.TargetType.Enemy));//Нужно добавить передвижение
        CardManager.AllCards.Add(new SpellCard("Выпад вперед", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Advance, SpellCard.Stance.Attacking, SpellCard.CardEffect.Movement, 1, SpellCard.CardEffect.Damage, 2, SpellCard.TargetType.Enemy));//Нужно добавить передвижение
        CardManager.AllCards.Add(new SpellCard("Шаг назад", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Advance, SpellCard.Stance.Defensive, SpellCard.CardEffect.Defense, 1, SpellCard.CardEffect.Movement, 1, SpellCard.TargetType.This));//Работа со способностью
        CardManager.AllCards.Add(new SpellCard("Шаг назад", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Advance, SpellCard.Stance.Defensive, SpellCard.CardEffect.Defense, 1, SpellCard.CardEffect.Movement, 1, SpellCard.TargetType.This));//Работа со способностью

        //Универсальные в сете "Скиссор"

        CardManager.AllCards.Add(new SpellCard("Яростный рывок", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Advance, SpellCard.Stance.Raging, SpellCard.CardEffect.Damage, 4, SpellCard.CardEffect.Movement, 0, SpellCard.TargetType.Enemy));//Работа со способностью
        CardManager.AllCards.Add(new SpellCard("Яростный рывок", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Advance, SpellCard.Stance.Raging, SpellCard.CardEffect.Damage, 4, SpellCard.CardEffect.Movement, 0, SpellCard.TargetType.Enemy));//Работа со способностью
        CardManager.AllCards.Add(new SpellCard("Яростный рывок", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Advance, SpellCard.Stance.Raging, SpellCard.CardEffect.Damage, 4, SpellCard.CardEffect.Movement, 0, SpellCard.TargetType.Enemy));//Работа со способностью
        CardManager.AllCards.Add(new SpellCard("Внезапный удар", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Advance, SpellCard.Stance.Attacking, SpellCard.CardEffect.Damage, 3, SpellCard.CardEffect.CardDrow, 1, SpellCard.TargetType.Enemy));
        CardManager.AllCards.Add(new SpellCard("Внезапный удар", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Advance, SpellCard.Stance.Attacking, SpellCard.CardEffect.Damage, 3, SpellCard.CardEffect.CardDrow, 1, SpellCard.TargetType.Enemy));
        CardManager.AllCards.Add(new SpellCard("Внезапный удар", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Advance, SpellCard.Stance.Attacking, SpellCard.CardEffect.Damage, 3, SpellCard.CardEffect.CardDrow, 1, SpellCard.TargetType.Enemy));
        CardManager.AllCards.Add(new SpellCard("Rip and tear", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Raging, SpellCard.Stance.Attacking, SpellCard.CardEffect.Damage, 3, SpellCard.CardEffect.ResetCard, 2, SpellCard.TargetType.Enemy));//Работа со способностью
        CardManager.AllCards.Add(new SpellCard("Яростная серия", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Raging, SpellCard.Stance.Raging, SpellCard.CardEffect.Damage, 2, SpellCard.CardEffect.ManaAdd, 1, SpellCard.TargetType.Enemy));
        CardManager.AllCards.Add(new SpellCard("Зацепить оружие", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Attacking, SpellCard.Stance.Raging, SpellCard.CardEffect.Defense, 0, SpellCard.CardEffect.CardDrow, 2, SpellCard.TargetType.This));// + механика
        CardManager.AllCards.Add(new SpellCard("Зацепить оружие", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Attacking, SpellCard.Stance.Raging, SpellCard.CardEffect.Defense, 0, SpellCard.CardEffect.CardDrow, 2, SpellCard.TargetType.This));// + механика
        CardManager.AllCards.Add(new SpellCard("Разрезающий удар", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Attacking, SpellCard.Stance.Advance, SpellCard.CardEffect.Damage, 4, SpellCard.CardEffect.CardDrow, 1, SpellCard.TargetType.Enemy));
        CardManager.AllCards.Add(new SpellCard("Разрезающий удар", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Attacking, SpellCard.Stance.Advance, SpellCard.CardEffect.Damage, 4, SpellCard.CardEffect.CardDrow, 1, SpellCard.TargetType.Enemy));
        CardManager.AllCards.Add(new SpellCard("Удар клинком", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Attacking, SpellCard.Stance.Attacking, SpellCard.CardEffect.Damage, 3, SpellCard.CardEffect.No, 0, SpellCard.TargetType.Enemy));
        CardManager.AllCards.Add(new SpellCard("Удар клинком", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Attacking, SpellCard.Stance.Attacking, SpellCard.CardEffect.Damage, 3, SpellCard.CardEffect.No, 0, SpellCard.TargetType.Enemy));
        CardManager.AllCards.Add(new SpellCard("Удар клинком", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Attacking, SpellCard.Stance.Attacking, SpellCard.CardEffect.Damage, 3, SpellCard.CardEffect.No, 0, SpellCard.TargetType.Enemy));


        //Универсальные в сете "Мурмиллон"

        CardManager.AllCards.Add(new SpellCard("Оглушение щитом", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Defensive, SpellCard.Stance.Attacking, SpellCard.CardEffect.Damage, 0, SpellCard.CardEffect.ResetCard, 2, SpellCard.TargetType.Enemy));//+механика
        CardManager.AllCards.Add(new SpellCard("Оглушение щитом", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Defensive, SpellCard.Stance.Attacking, SpellCard.CardEffect.Damage, 0, SpellCard.CardEffect.ResetCard, 2, SpellCard.TargetType.Enemy));//+механика
        CardManager.AllCards.Add(new SpellCard("Блок", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Defensive, SpellCard.Stance.Defensive, SpellCard.CardEffect.Defense, 2, SpellCard.CardEffect.Type, 1, SpellCard.TargetType.This));//Работа с типами карт
        CardManager.AllCards.Add(new SpellCard("Блок", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Defensive, SpellCard.Stance.Defensive, SpellCard.CardEffect.Defense, 2, SpellCard.CardEffect.Type, 1, SpellCard.TargetType.This));//Работа с типами карт
        CardManager.AllCards.Add(new SpellCard("Блок", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Defensive, SpellCard.Stance.Defensive, SpellCard.CardEffect.Defense, 2, SpellCard.CardEffect.Type, 1, SpellCard.TargetType.This));//Работа с типами карт
        CardManager.AllCards.Add(new SpellCard("Уворот", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Defensive, SpellCard.Stance.Advance, SpellCard.CardEffect.Defense, 3, SpellCard.CardEffect.Movement, 1, SpellCard.TargetType.This));
        CardManager.AllCards.Add(new SpellCard("Заверщающий рубец", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Attacking, SpellCard.Stance.Advance, SpellCard.CardEffect.Damage, 2, SpellCard.CardEffect.Mechanics, 1, SpellCard.TargetType.Enemy));//Работа со способностью карты
        CardManager.AllCards.Add(new SpellCard("Заверщающий рубец", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Attacking, SpellCard.Stance.Advance, SpellCard.CardEffect.Damage, 2, SpellCard.CardEffect.Mechanics, 1, SpellCard.TargetType.Enemy));//Работа со способностью карты
        CardManager.AllCards.Add(new SpellCard("Удар клинком", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Attacking, SpellCard.Stance.Attacking, SpellCard.CardEffect.Damage, 3, SpellCard.CardEffect.No, 0, SpellCard.TargetType.Enemy));
        CardManager.AllCards.Add(new SpellCard("Удар клинком", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Attacking, SpellCard.Stance.Attacking, SpellCard.CardEffect.Damage, 3, SpellCard.CardEffect.No, 0, SpellCard.TargetType.Enemy));
        CardManager.AllCards.Add(new SpellCard("Прикрыться", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Advance, SpellCard.Stance.Defensive, SpellCard.CardEffect.Defense, 1, SpellCard.CardEffect.CardDrow, 1, SpellCard.TargetType.This));// Работа со способностью карты
        CardManager.AllCards.Add(new SpellCard("Прикрыться", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Advance, SpellCard.Stance.Defensive, SpellCard.CardEffect.Defense, 1, SpellCard.CardEffect.CardDrow, 1, SpellCard.TargetType.This));// Работа со способностью карты
        CardManager.AllCards.Add(new SpellCard("Внезапный удар", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Advance, SpellCard.Stance.Attacking, SpellCard.CardEffect.Damage, 3, SpellCard.CardEffect.CardDrow, 1, SpellCard.TargetType.Enemy));
        CardManager.AllCards.Add(new SpellCard("Внезапный удар", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Advance, SpellCard.Stance.Attacking, SpellCard.CardEffect.Damage, 3, SpellCard.CardEffect.CardDrow, 1, SpellCard.TargetType.Enemy));


        //Универсальные в сете "Гопломах"

        CardManager.AllCards.Add(new SpellCard("Уворот", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Defensive, SpellCard.Stance.Advance, SpellCard.CardEffect.Defense, 3, SpellCard.CardEffect.Movement, 1, SpellCard.TargetType.This));
        CardManager.AllCards.Add(new SpellCard("Уворот", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Defensive, SpellCard.Stance.Advance, SpellCard.CardEffect.Defense, 3, SpellCard.CardEffect.Movement, 1, SpellCard.TargetType.This));
        CardManager.AllCards.Add(new SpellCard("Блок", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Defensive, SpellCard.Stance.Defensive, SpellCard.CardEffect.Defense, 2, SpellCard.CardEffect.Type, 1, SpellCard.TargetType.This));//Работа с типами карт
        CardManager.AllCards.Add(new SpellCard("Блок", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Defensive, SpellCard.Stance.Defensive, SpellCard.CardEffect.Defense, 2, SpellCard.CardEffect.Type, 1, SpellCard.TargetType.This));//Работа с типами карт
        CardManager.AllCards.Add(new SpellCard("Укол из-за щита", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Defensive, SpellCard.Stance.Attacking, SpellCard.CardEffect.Damage, 3, SpellCard.CardEffect.CardDrow, 1, SpellCard.TargetType.Enemy));
        CardManager.AllCards.Add(new SpellCard("Укол из-за щита", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Defensive, SpellCard.Stance.Attacking, SpellCard.CardEffect.Damage, 3, SpellCard.CardEffect.CardDrow, 1, SpellCard.TargetType.Enemy));
        CardManager.AllCards.Add(new SpellCard("Выпад вперед", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Advance, SpellCard.Stance.Attacking, SpellCard.CardEffect.Damage, 2, SpellCard.CardEffect.Movement, 1, SpellCard.TargetType.Enemy));
        CardManager.AllCards.Add(new SpellCard("Выпад вперед", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Advance, SpellCard.Stance.Attacking, SpellCard.CardEffect.Damage, 2, SpellCard.CardEffect.Movement, 1, SpellCard.TargetType.Enemy));
        CardManager.AllCards.Add(new SpellCard("Выпад вперед", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Advance, SpellCard.Stance.Attacking, SpellCard.CardEffect.Damage, 2, SpellCard.CardEffect.Movement, 1, SpellCard.TargetType.Enemy));
        CardManager.AllCards.Add(new SpellCard("Прикрыться", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Advance, SpellCard.Stance.Defensive, SpellCard.CardEffect.Defense, 1, SpellCard.CardEffect.CardDrow, 1, SpellCard.TargetType.This));// Работа со способностью карты
        CardManager.AllCards.Add(new SpellCard("Прикрыться", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Advance, SpellCard.Stance.Defensive, SpellCard.CardEffect.Defense, 1, SpellCard.CardEffect.CardDrow, 1, SpellCard.TargetType.This));// Работа со способностью карты
        CardManager.AllCards.Add(new SpellCard("Тычок с отступлением", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Attacking, SpellCard.Stance.Defensive, SpellCard.CardEffect.Damage, 2, SpellCard.CardEffect.Movement, 1, SpellCard.TargetType.Enemy));// Работа с передвижением
        CardManager.AllCards.Add(new SpellCard("Тычок с отступлением", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Attacking, SpellCard.Stance.Defensive, SpellCard.CardEffect.Damage, 2, SpellCard.CardEffect.Movement, 1, SpellCard.TargetType.Enemy));// Работа с передвижением
        CardManager.AllCards.Add(new SpellCard("Осторожный удар", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Attacking, SpellCard.Stance.Advance, SpellCard.CardEffect.Damage, 3, SpellCard.CardEffect.CardDrow, 1, SpellCard.TargetType.Enemy));//Работа со способностью карты
        CardManager.AllCards.Add(new SpellCard("Осторожный удар", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Attacking, SpellCard.Stance.Advance, SpellCard.CardEffect.Damage, 3, SpellCard.CardEffect.CardDrow, 1, SpellCard.TargetType.Enemy));//Работа со способностью карты
        CardManager.AllCards.Add(new SpellCard("Преследование", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Attacking, SpellCard.Stance.Attacking, SpellCard.CardEffect.Damage, 4, SpellCard.CardEffect.Movement, 1, SpellCard.TargetType.Enemy));//передвижение
        CardManager.AllCards.Add(new SpellCard("Преследование", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Attacking, SpellCard.Stance.Attacking, SpellCard.CardEffect.Damage, 4, SpellCard.CardEffect.Movement, 1, SpellCard.TargetType.Enemy));//передвижение

    }
}
