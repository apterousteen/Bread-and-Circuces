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
    public enum FirstCardEffect
    {
        Damage,
        Defense,
        Survived
    }
    public enum SecondCardEffect
    {
        CardDrow,
        Movement,
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
        Defensive_Defensive,
        Defensive_Advance,
        Defensive_Attacking,
        Defensive_Raging,
        Advance_Defensive,
        Advance_Advance,
        Advance_Attacking,
        Advance_Raging,
        Attacking_Defensive,
        Attacking_Advance,
        Attacking_Attacking,
        Attacking_Raging,
        Raging_Defensive,
        Raging_Advance,
        Raging_Attacking,
        Raging_Raging,
    }

    public FirstCardEffect FirstCardEff;
    public SecondCardEffect SecondCardEff;
    public Stance StanceType;
    public TargetType SpellTarget;
    public int SpellValue;
    public int SecondSpellValue;

    public SpellCard(string name, string logoPath, int manacost, Stance stance = 0,
        FirstCardEffect firstCardEffect = 0, int spellValue = 0, SecondCardEffect secondCardEffect = 0, int secondSpellValue = 0,
        TargetType targetType = 0) : base(name, logoPath, manacost)
    {
        IsSpell = true;

        FirstCardEff = firstCardEffect;
        SecondCardEff = secondCardEffect;
        StanceType = stance;

        SpellTarget = targetType;
        SpellValue = spellValue;
        SecondSpellValue = secondSpellValue;
    }

    public SpellCard(SpellCard card) : base(card)
    {
        IsSpell = true;

        FirstCardEff = card.FirstCardEff;
        SecondCardEff = card.SecondCardEff;
        StanceType = card.StanceType;

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

        CardManager.AllCards.Add(new SpellCard("Бросок сети", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Defensive_Attacking, SpellCard.FirstCardEffect.Damage, 0, SpellCard.SecondCardEffect.Status, 0, SpellCard.TargetType.Enemy));// Работа со способностью
        CardManager.AllCards.Add(new SpellCard("Уворот", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Defensive_Advance, SpellCard.FirstCardEffect.Defense, 3, SpellCard.SecondCardEffect.Movement, 1, SpellCard.TargetType.This));
        CardManager.AllCards.Add(new SpellCard("Уворот", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Defensive_Advance, SpellCard.FirstCardEffect.Defense, 3, SpellCard.SecondCardEffect.Movement, 1, SpellCard.TargetType.This));
        CardManager.AllCards.Add(new SpellCard("Уворот", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Defensive_Advance, SpellCard.FirstCardEffect.Defense, 3, SpellCard.SecondCardEffect.Movement, 1, SpellCard.TargetType.This));
        CardManager.AllCards.Add(new SpellCard("Уворот", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Defensive_Advance, SpellCard.FirstCardEffect.Defense, 3, SpellCard.SecondCardEffect.Movement, 1, SpellCard.TargetType.This));
        CardManager.AllCards.Add(new SpellCard("Тычок с отступлением", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Attacking_Defensive, SpellCard.FirstCardEffect.Damage, 2, SpellCard.SecondCardEffect.Movement, 1, SpellCard.TargetType.Enemy));
        CardManager.AllCards.Add(new SpellCard("Тычок с отступлением", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Attacking_Defensive, SpellCard.FirstCardEffect.Damage, 2, SpellCard.SecondCardEffect.Movement, 1, SpellCard.TargetType.Enemy));
        CardManager.AllCards.Add(new SpellCard("Протыкание ноги", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Attacking_Attacking, SpellCard.FirstCardEffect.Damage, 4, SpellCard.SecondCardEffect.ResetCard, 1, SpellCard.TargetType.Enemy));
        CardManager.AllCards.Add(new SpellCard("Протыкание ноги", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Attacking_Attacking, SpellCard.FirstCardEffect.Damage, 4, SpellCard.SecondCardEffect.ResetCard, 1, SpellCard.TargetType.Enemy));
        CardManager.AllCards.Add(new SpellCard("Осторожный удар", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Attacking_Defensive, SpellCard.FirstCardEffect.Damage, 3, SpellCard.SecondCardEffect.CardDrow, 1, SpellCard.TargetType.Enemy));//Корректировка способностей
        CardManager.AllCards.Add(new SpellCard("Осторожный удар", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Attacking_Defensive, SpellCard.FirstCardEffect.Damage, 3, SpellCard.SecondCardEffect.CardDrow, 1, SpellCard.TargetType.Enemy));//Корректировка способностей
        CardManager.AllCards.Add(new SpellCard("Выпад вперед", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Advance_Attacking, SpellCard.FirstCardEffect.Damage, 2, SpellCard.SecondCardEffect.Movement, 1, SpellCard.TargetType.Enemy));//Нужно добавить передвижение
        CardManager.AllCards.Add(new SpellCard("Выпад вперед", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Advance_Attacking, SpellCard.FirstCardEffect.Damage, 2, SpellCard.SecondCardEffect.Movement, 1, SpellCard.TargetType.Enemy));//Нужно добавить передвижение
        CardManager.AllCards.Add(new SpellCard("Выпад вперед", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Advance_Attacking, SpellCard.FirstCardEffect.Damage, 2, SpellCard.SecondCardEffect.Movement, 1, SpellCard.TargetType.Enemy));//Нужно добавить передвижение
        CardManager.AllCards.Add(new SpellCard("Шаг назад", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Advance_Defensive, SpellCard.FirstCardEffect.Defense, 1, SpellCard.SecondCardEffect.Movement, 1, SpellCard.TargetType.This));//Работа со способностью
        CardManager.AllCards.Add(new SpellCard("Шаг назад", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Advance_Defensive, SpellCard.FirstCardEffect.Defense, 1, SpellCard.SecondCardEffect.Movement, 1, SpellCard.TargetType.This));//Работа со способностью

        //Универсальные в сете "Скиссор"

        CardManager.AllCards.Add(new SpellCard("Яростный рывок", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Advance_Raging, SpellCard.FirstCardEffect.Damage, 4, SpellCard.SecondCardEffect.Movement, 0, SpellCard.TargetType.Enemy));//Работа со способностью
        CardManager.AllCards.Add(new SpellCard("Яростный рывок", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Advance_Raging, SpellCard.FirstCardEffect.Damage, 4, SpellCard.SecondCardEffect.Movement, 0, SpellCard.TargetType.Enemy));//Работа со способностью
        CardManager.AllCards.Add(new SpellCard("Яростный рывок", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Advance_Raging, SpellCard.FirstCardEffect.Damage, 4, SpellCard.SecondCardEffect.Movement, 0, SpellCard.TargetType.Enemy));//Работа со способностью
        CardManager.AllCards.Add(new SpellCard("Внезапный удар", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Advance_Attacking, SpellCard.FirstCardEffect.Damage, 3, SpellCard.SecondCardEffect.CardDrow, 1, SpellCard.TargetType.Enemy));
        CardManager.AllCards.Add(new SpellCard("Внезапный удар", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Advance_Attacking, SpellCard.FirstCardEffect.Damage, 3, SpellCard.SecondCardEffect.CardDrow, 1, SpellCard.TargetType.Enemy));
        CardManager.AllCards.Add(new SpellCard("Внезапный удар", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Advance_Attacking, SpellCard.FirstCardEffect.Damage, 3, SpellCard.SecondCardEffect.CardDrow, 1, SpellCard.TargetType.Enemy));
        CardManager.AllCards.Add(new SpellCard("Rip and tear", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Raging_Advance, SpellCard.FirstCardEffect.Damage, 3, SpellCard.SecondCardEffect.ResetCard, 2, SpellCard.TargetType.Enemy));//Работа со способностью
        CardManager.AllCards.Add(new SpellCard("Яростная серия", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Raging_Raging, SpellCard.FirstCardEffect.Damage, 2, SpellCard.SecondCardEffect.ManaAdd, 1, SpellCard.TargetType.Enemy));
        CardManager.AllCards.Add(new SpellCard("Зацепить оружие", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Attacking_Raging, SpellCard.FirstCardEffect.Defense, 0, SpellCard.SecondCardEffect.CardDrow, 2, SpellCard.TargetType.This));// + механика
        CardManager.AllCards.Add(new SpellCard("Зацепить оружие", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Attacking_Raging, SpellCard.FirstCardEffect.Defense, 0, SpellCard.SecondCardEffect.CardDrow, 2, SpellCard.TargetType.This));// + механика
        CardManager.AllCards.Add(new SpellCard("Разрезающий удар", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Attacking_Advance, SpellCard.FirstCardEffect.Damage, 4, SpellCard.SecondCardEffect.CardDrow, 1, SpellCard.TargetType.Enemy));
        CardManager.AllCards.Add(new SpellCard("Разрезающий удар", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Attacking_Advance, SpellCard.FirstCardEffect.Damage, 4, SpellCard.SecondCardEffect.CardDrow, 1, SpellCard.TargetType.Enemy));
        CardManager.AllCards.Add(new SpellCard("Удар клинком", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Attacking_Attacking, SpellCard.FirstCardEffect.Damage, 3, SpellCard.SecondCardEffect.No, 0, SpellCard.TargetType.Enemy));
        CardManager.AllCards.Add(new SpellCard("Удар клинком", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Attacking_Attacking, SpellCard.FirstCardEffect.Damage, 3, SpellCard.SecondCardEffect.No, 0, SpellCard.TargetType.Enemy));
        CardManager.AllCards.Add(new SpellCard("Удар клинком", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Attacking_Attacking, SpellCard.FirstCardEffect.Damage, 3, SpellCard.SecondCardEffect.No, 0, SpellCard.TargetType.Enemy));


        //Универсальные в сете "Мурмиллон"

        CardManager.AllCards.Add(new SpellCard("Оглушение щитом", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Defensive_Attacking, SpellCard.FirstCardEffect.Damage, 0, SpellCard.SecondCardEffect.ResetCard, 2, SpellCard.TargetType.Enemy));//+механика
        CardManager.AllCards.Add(new SpellCard("Оглушение щитом", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Defensive_Attacking, SpellCard.FirstCardEffect.Damage, 0, SpellCard.SecondCardEffect.ResetCard, 2, SpellCard.TargetType.Enemy));//+механика
        CardManager.AllCards.Add(new SpellCard("Блок", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Defensive_Defensive, SpellCard.FirstCardEffect.Defense, 2, SpellCard.SecondCardEffect.Type, 1, SpellCard.TargetType.This));//Работа с типами карт
        CardManager.AllCards.Add(new SpellCard("Блок", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Defensive_Defensive, SpellCard.FirstCardEffect.Defense, 2, SpellCard.SecondCardEffect.Type, 1, SpellCard.TargetType.This));//Работа с типами карт
        CardManager.AllCards.Add(new SpellCard("Блок", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Defensive_Defensive, SpellCard.FirstCardEffect.Defense, 2, SpellCard.SecondCardEffect.Type, 1, SpellCard.TargetType.This));//Работа с типами карт
        CardManager.AllCards.Add(new SpellCard("Уворот", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Defensive_Advance, SpellCard.FirstCardEffect.Defense, 3, SpellCard.SecondCardEffect.Movement, 1, SpellCard.TargetType.This));
        CardManager.AllCards.Add(new SpellCard("Заверщающий рубец", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Attacking_Advance, SpellCard.FirstCardEffect.Damage, 2, SpellCard.SecondCardEffect.Mechanics, 1, SpellCard.TargetType.Enemy));//Работа со способностью карты
        CardManager.AllCards.Add(new SpellCard("Заверщающий рубец", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Attacking_Advance, SpellCard.FirstCardEffect.Damage, 2, SpellCard.SecondCardEffect.Mechanics, 1, SpellCard.TargetType.Enemy));//Работа со способностью карты
        CardManager.AllCards.Add(new SpellCard("Удар клинком", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Attacking_Attacking, SpellCard.FirstCardEffect.Damage, 3, SpellCard.SecondCardEffect.No, 0, SpellCard.TargetType.Enemy));
        CardManager.AllCards.Add(new SpellCard("Удар клинком", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Attacking_Attacking, SpellCard.FirstCardEffect.Damage, 3, SpellCard.SecondCardEffect.No, 0, SpellCard.TargetType.Enemy));
        CardManager.AllCards.Add(new SpellCard("Прикрыться", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Advance_Defensive, SpellCard.FirstCardEffect.Defense, 1, SpellCard.SecondCardEffect.CardDrow, 1, SpellCard.TargetType.This));// Работа со способностью карты
        CardManager.AllCards.Add(new SpellCard("Прикрыться", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Advance_Defensive, SpellCard.FirstCardEffect.Defense, 1, SpellCard.SecondCardEffect.CardDrow, 1, SpellCard.TargetType.This));// Работа со способностью карты
        CardManager.AllCards.Add(new SpellCard("Внезапный удар", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Advance_Attacking, SpellCard.FirstCardEffect.Damage, 3, SpellCard.SecondCardEffect.CardDrow, 1, SpellCard.TargetType.Enemy));
        CardManager.AllCards.Add(new SpellCard("Внезапный удар", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Advance_Attacking, SpellCard.FirstCardEffect.Damage, 3, SpellCard.SecondCardEffect.CardDrow, 1, SpellCard.TargetType.Enemy));


        //Универсальные в сете "Гопломах"

        CardManager.AllCards.Add(new SpellCard("Уворот", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Defensive_Advance, SpellCard.FirstCardEffect.Defense, 3, SpellCard.SecondCardEffect.Movement, 1, SpellCard.TargetType.This));
        CardManager.AllCards.Add(new SpellCard("Уворот", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Defensive_Advance, SpellCard.FirstCardEffect.Defense, 3, SpellCard.SecondCardEffect.Movement, 1, SpellCard.TargetType.This));
        CardManager.AllCards.Add(new SpellCard("Блок", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Defensive_Defensive, SpellCard.FirstCardEffect.Defense, 2, SpellCard.SecondCardEffect.Type, 1, SpellCard.TargetType.This));//Работа с типами карт
        CardManager.AllCards.Add(new SpellCard("Блок", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Defensive_Defensive, SpellCard.FirstCardEffect.Defense, 2, SpellCard.SecondCardEffect.Type, 1, SpellCard.TargetType.This));//Работа с типами карт
        CardManager.AllCards.Add(new SpellCard("Укол из-за щита", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Defensive_Attacking, SpellCard.FirstCardEffect.Damage, 3, SpellCard.SecondCardEffect.CardDrow, 1, SpellCard.TargetType.Enemy));
        CardManager.AllCards.Add(new SpellCard("Укол из-за щита", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Defensive_Attacking, SpellCard.FirstCardEffect.Damage, 3, SpellCard.SecondCardEffect.CardDrow, 1, SpellCard.TargetType.Enemy));
        CardManager.AllCards.Add(new SpellCard("Выпад вперед", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Advance_Attacking, SpellCard.FirstCardEffect.Damage, 2, SpellCard.SecondCardEffect.Movement, 1, SpellCard.TargetType.Enemy));
        CardManager.AllCards.Add(new SpellCard("Выпад вперед", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Advance_Attacking, SpellCard.FirstCardEffect.Damage, 2, SpellCard.SecondCardEffect.Movement, 1, SpellCard.TargetType.Enemy));
        CardManager.AllCards.Add(new SpellCard("Выпад вперед", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Advance_Attacking, SpellCard.FirstCardEffect.Damage, 2, SpellCard.SecondCardEffect.Movement, 1, SpellCard.TargetType.Enemy));
        CardManager.AllCards.Add(new SpellCard("Прикрыться", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Advance_Defensive, SpellCard.FirstCardEffect.Defense, 1, SpellCard.SecondCardEffect.CardDrow, 1, SpellCard.TargetType.This));// Работа со способностью карты
        CardManager.AllCards.Add(new SpellCard("Прикрыться", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Advance_Defensive, SpellCard.FirstCardEffect.Defense, 1, SpellCard.SecondCardEffect.CardDrow, 1, SpellCard.TargetType.This));// Работа со способностью карты
        CardManager.AllCards.Add(new SpellCard("Тычок с отступлением", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Attacking_Defensive, SpellCard.FirstCardEffect.Damage, 2, SpellCard.SecondCardEffect.Movement, 1, SpellCard.TargetType.Enemy));// Работа с передвижением
        CardManager.AllCards.Add(new SpellCard("Тычок с отступлением", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Attacking_Defensive, SpellCard.FirstCardEffect.Damage, 2, SpellCard.SecondCardEffect.Movement, 1, SpellCard.TargetType.Enemy));// Работа с передвижением
        CardManager.AllCards.Add(new SpellCard("Осторожный удар", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Attacking_Advance, SpellCard.FirstCardEffect.Damage, 3, SpellCard.SecondCardEffect.CardDrow, 1, SpellCard.TargetType.Enemy));//Работа со способностью карты
        CardManager.AllCards.Add(new SpellCard("Осторожный удар", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Attacking_Advance, SpellCard.FirstCardEffect.Damage, 3, SpellCard.SecondCardEffect.CardDrow, 1, SpellCard.TargetType.Enemy));//Работа со способностью карты
        CardManager.AllCards.Add(new SpellCard("Преследование", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Attacking_Attacking, SpellCard.FirstCardEffect.Damage, 4, SpellCard.SecondCardEffect.Movement, 1, SpellCard.TargetType.Enemy));//передвижение
        CardManager.AllCards.Add(new SpellCard("Преследование", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Attacking_Attacking, SpellCard.FirstCardEffect.Damage, 4, SpellCard.SecondCardEffect.Movement, 1, SpellCard.TargetType.Enemy));//передвижение

    }
}
