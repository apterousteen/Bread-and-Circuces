using System.Collections.Generic;
using UnityEngine;

public class Card
{
    public enum AbilityType
    {
        NO_ABILITY,
        INSTANT_ACTIVE,
        DOUBLE_ATTACK,
        SHIELD,
        PROVOCATION,
        REGENERATION_EACH_TURN,
        COUNTER_ATTACK

    }

    public string Name;
    public Sprite Logo;
    public int Attack, Defense, Manacost;
    public bool IsPlaced;

    public List<AbilityType> Abilities;

    public bool IsSpell;

    public bool IsAlive
    {
        get
        {
            return Defense > 0;
        }
    }
    public bool HasAbility
    {
        get
        {
            return Abilities.Count > 0;
        }
    }
    public bool IsProvocation
    {
        get
        {
            return Abilities.Exists(x => x == AbilityType.PROVOCATION);
        }
    }

    public int TimesDealedDamage;

    public Card(string name, string logoName, int attack, int defense, int manacost, AbilityType abilityType = 0)
    {
        Name = name;
        Logo = Resources.Load<Sprite>(logoName);
        Attack = attack;
        Defense = defense;
        Manacost = manacost;
        IsPlaced = false;

        Abilities = new List<AbilityType>();

        if (abilityType != 0)
            Abilities.Add(abilityType);

        TimesDealedDamage = 0;
    }

    public Card(Card card)
    {
        Name = card.Name;
        Logo = card.Logo;
        Attack = card.Attack;
        Defense = card.Defense;
        Manacost = card.Manacost;
        IsPlaced = false;

        Abilities = new List<AbilityType>(card.Abilities);

        TimesDealedDamage = 0;
    }

    public void GetDamage(int dmg)
    {
        Defense -= dmg;
    }

    public Card GetCopy()
    {
        return new Card(this);
    }
}

public class SpellCard : Card
{
    public enum SpellType
    {
        NO_SPELL,
        AOE_HEAL,
        HEAL_ALLY_CARD,
        AOE_DAMAGE,
        DAMAGE_TARGET,
        ARMOR,
        BUFF_CARD_DAMAGE,
        DEBUFF_CARD_DAMAGE,
        DRAW_CART
    }

    public enum TargetType
    {
        NO_TARGET,
        SELF,
        AOE_CARD_TARGET,
        ENEMY_CARD_TARGET,
        ALLY_CARD_TARGET
    }

    public SpellType Spell;
    public TargetType SpellTarget;
    public int SpellValue;

    public SpellCard(string name, string logoPath, int manacost, SpellType spellType = 0,
                     int spellValue = 0, TargetType targetType = 0) : base(name, logoPath, 0, 0, manacost)
    {
        IsSpell = true;

        Spell = spellType;
        SpellTarget = targetType;
        SpellValue = spellValue;
    }

    public SpellCard(SpellCard card) : base(card)
    {
        IsSpell = true;

        Spell = card.Spell;
        SpellTarget = card.SpellTarget;
        SpellValue = card.SpellValue;
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
        /*
        CardManager.AllCards.Add(new Card("CardOne", "Sprites/LogoCards/CardOne", 1, 1, 2));
        CardManager.AllCards.Add(new Card("CardTwo", "Sprites/LogoCards/CardTwo", 2, 2, 2));
        CardManager.AllCards.Add(new Card("CardThree", "Sprites/LogoCards/CardThree", 3, 3, 2));
        CardManager.AllCards.Add(new Card("CardFour", "Sprites/LogoCards/CardFour", 4, 4, 2));
        CardManager.AllCards.Add(new Card("CardFive", "Sprites/LogoCards/CardFive", 5, 5, 2));
        CardManager.AllCards.Add(new Card("CardSix", "Sprites/LogoCards/CardSix", 6, 6, 2));
        CardManager.AllCards.Add(new Card("CardSeven", "Sprites/LogoCards/CardSeven", 7, 7, 2));
        CardManager.AllCards.Add(new Card("CardEight", "Sprites/LogoCards/CardEight", 8, 8, 2));
        */

        //Универсальные карты


        //Универсальные в сете "Ретиарий"

        CardManager.AllCards.Add(new SpellCard("Парирование", "Sprites/LogoCards/CHto-to", 1, SpellCard.SpellType.DRAW_CART, 2, SpellCard.TargetType.NO_TARGET));
        CardManager.AllCards.Add(new SpellCard("Тычок с отступлением", "Sprites/LogoCards/CHto-to", 1, SpellCard.SpellType.AOE_DAMAGE, 2, SpellCard.TargetType.ENEMY_CARD_TARGET));
        CardManager.AllCards.Add(new SpellCard("Осторожный удар", "Sprites/LogoCards/CHto-to", 1, SpellCard.SpellType.AOE_DAMAGE, 2, SpellCard.TargetType.ENEMY_CARD_TARGET));//Нужно добавить добор
        CardManager.AllCards.Add(new SpellCard("Выпад вперед", "Sprites/LogoCards/CHto-to", 1, SpellCard.SpellType.AOE_DAMAGE, 2, SpellCard.TargetType.ENEMY_CARD_TARGET));//Нужно добавить передвижение
        CardManager.AllCards.Add(new SpellCard("Шаг назад", "Sprites/LogoCards/CHto-to", 1, SpellCard.SpellType.ARMOR, 2, SpellCard.TargetType.SELF));//Нужно добавить передвижение

        //Универсальные в сете "Скиссор"

        CardManager.AllCards.Add(new SpellCard("Take it", "Sprites/LogoCards/CHto-to", 1, SpellCard.SpellType.ARMOR, 2, SpellCard.TargetType.SELF));//Нужно добавить передвижение
        CardManager.AllCards.Add(new SpellCard("Открывающий удар", "Sprites/LogoCards/CHto-to", 1, SpellCard.SpellType.AOE_DAMAGE, 2, SpellCard.TargetType.ENEMY_CARD_TARGET));//Работа со сбросом
        CardManager.AllCards.Add(new SpellCard("Удар клинком", "Sprites/LogoCards/CHto-to", 1, SpellCard.SpellType.AOE_DAMAGE, 2, SpellCard.TargetType.ENEMY_CARD_TARGET));

        //Универсальные в сете "Мурмиллон"

        CardManager.AllCards.Add(new SpellCard("Блок", "Sprites/LogoCards/CHto-to", 1, SpellCard.SpellType.ARMOR, 2, SpellCard.TargetType.SELF));//Работа с типами карт
        CardManager.AllCards.Add(new SpellCard("Заверщающий рубец", "Sprites/LogoCards/CHto-to", 1, SpellCard.SpellType.AOE_DAMAGE, 2, SpellCard.TargetType.ENEMY_CARD_TARGET));//Работа со способностью карты
        CardManager.AllCards.Add(new SpellCard("Удар клинком", "Sprites/LogoCards/CHto-to", 1, SpellCard.SpellType.AOE_DAMAGE, 2, SpellCard.TargetType.ENEMY_CARD_TARGET));
        CardManager.AllCards.Add(new SpellCard("Прикрыться", "Sprites/LogoCards/CHto-to", 1, SpellCard.SpellType.ARMOR, 2, SpellCard.TargetType.SELF));// Работа с добором + способностью карты
        CardManager.AllCards.Add(new SpellCard("Внезапный удар", "Sprites/LogoCards/CHto-to", 1, SpellCard.SpellType.AOE_DAMAGE, 2, SpellCard.TargetType.ENEMY_CARD_TARGET));// Работа с добором

        //Универсальные в сете "Гопломах"

        CardManager.AllCards.Add(new SpellCard("Парирование", "Sprites/LogoCards/CHto-to", 1, SpellCard.SpellType.DRAW_CART, 2, SpellCard.TargetType.NO_TARGET));
        CardManager.AllCards.Add(new SpellCard("Блок", "Sprites/LogoCards/CHto-to", 1, SpellCard.SpellType.ARMOR, 2, SpellCard.TargetType.SELF));//Работа с типами карт
        CardManager.AllCards.Add(new SpellCard("Выпад вперед", "Sprites/LogoCards/CHto-to", 1, SpellCard.SpellType.AOE_DAMAGE, 2, SpellCard.TargetType.ENEMY_CARD_TARGET));//Нужно добавить передвижение
        CardManager.AllCards.Add(new SpellCard("Прикрыться", "Sprites/LogoCards/CHto-to", 1, SpellCard.SpellType.ARMOR, 2, SpellCard.TargetType.SELF));// Работа с добором + способностью карты
        CardManager.AllCards.Add(new SpellCard("Тычок с отступлением", "Sprites/LogoCards/CHto-to", 1, SpellCard.SpellType.AOE_DAMAGE, 2, SpellCard.TargetType.ENEMY_CARD_TARGET));
        CardManager.AllCards.Add(new SpellCard("Осторожный удар", "Sprites/LogoCards/CHto-to", 1, SpellCard.SpellType.AOE_DAMAGE, 2, SpellCard.TargetType.ENEMY_CARD_TARGET));//Нужно добавить добор
    }
}
