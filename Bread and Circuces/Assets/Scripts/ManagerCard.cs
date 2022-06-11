using System.Collections.Generic;
using UnityEngine;

public enum CardRestriction
{
    Universal,
    Retiarius,
    Hoplomachus,
    Murmillo,
    Scissor
}

public class Card
{

    public enum CardEffect
    {
        Damage,
        DamageAfterDiscard,
        DamageFinisher,
        Defense,
        ShieldedDefense,
        Movement,
        CardDrow,
        AliveCardDrow,
        NearCardDrow,
        ManaAdd,
        ChargeStart,
        ChargeEnd,
        PushBackEnemy,
        Stun,
        CancelCard,
        DiscardEnemy,
        DiscardSelf,
        No
    }
    public enum TargetType
    {
        NoTarget,
        This,
        Enemy,
        Ally
    }
    public enum CardType
    {
        Attack,
        Defense
    }
    

    public CardEffect FirstCardEff, FirstCardEffTwo;
    public Stance StartStance, EndStance;
    public TargetType SpellTarget;
    public CardType Type;
    public CardRestriction Restriction;
    public CardRestriction Set;
    public string Description;

    public string Name;
    public Sprite Logo;
    public int Manacost, SpellValue, SecondSpellValue;
    public bool IsPlaced;

    public Card(CardRestriction set, string name, string logoPath, int manacost, Stance startStance = 0, Stance endStance = 0, CardType type = 0,
        CardEffect firstCardEffect = 0, int spellValue = 0, CardEffect firstCardEffectTwo = 0, int secondSpellValue = 0,
        TargetType targetType = 0, string description = "", CardRestriction restriction = 0)
    {
        Name = name;
        Logo = Resources.Load<Sprite>(logoPath);
        if (Logo == null)
            Debug.Log("No logo");
        Manacost = manacost;
        IsPlaced = false;

        FirstCardEff = firstCardEffect;
        FirstCardEffTwo = firstCardEffectTwo;
        StartStance = startStance;
        EndStance = endStance;
        Type = type;

        SpellTarget = targetType;
        SpellValue = spellValue;
        SecondSpellValue = secondSpellValue;
        Restriction = restriction;
        Set = set;
        Description = description;
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
        Type = card.Type;

        SpellTarget = card.SpellTarget;
        SpellValue = card.SpellValue;
        SecondSpellValue = card.SecondSpellValue;
        Restriction = card.Restriction;
        Set = card.Set;
        Description = card.Description;
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
        if (CardManager.AllCards.Count != 0)
            return;

        //"Ретиарий"

        CardManager.AllCards.Add(new Card(CardRestriction.Retiarius, "Бросок сети", "Sprites/LogoCards/БросокСети", 1, Stance.Defensive, Stance.Attacking, Card.CardType.Defense, Card.CardEffect.Defense, 15, Card.CardEffect.CancelCard, 0, Card.TargetType.Enemy, "Отмените все эффекты вражеской карты", CardRestriction.Retiarius));// Работа со способностью
        CardManager.AllCards.Add(new Card(CardRestriction.Retiarius, "Уворот", "Sprites/LogoCards/Уворот", 0, Stance.Defensive, Stance.Advance, Card.CardType.Defense, Card.CardEffect.Defense, 3, Card.CardEffect.Movement, 1, Card.TargetType.This, "После атаки передвиньте этого бойца на 1 гекс"));
        CardManager.AllCards.Add(new Card(CardRestriction.Retiarius, "Уворот", "Sprites/LogoCards/Уворот", 0, Stance.Defensive, Stance.Advance, Card.CardType.Defense, Card.CardEffect.Defense, 3, Card.CardEffect.Movement, 1, Card.TargetType.This, "После атаки передвиньте этого бойца на 1 гекс"));
        CardManager.AllCards.Add(new Card(CardRestriction.Retiarius, "Контратака", "Sprites/LogoCards/Контратака", 1, Stance.Defensive, Stance.Attacking, Card.CardType.Attack, Card.CardEffect.Damage, 3, Card.CardEffect.CardDrow, 1, Card.TargetType.Enemy, "Возьмите карту"));
        CardManager.AllCards.Add(new Card(CardRestriction.Retiarius, "Контратака", "Sprites/LogoCards/Контратака", 1, Stance.Defensive, Stance.Attacking, Card.CardType.Attack, Card.CardEffect.Damage, 3, Card.CardEffect.CardDrow, 1, Card.TargetType.Enemy, "Возьмите карту"));
        CardManager.AllCards.Add(new Card(CardRestriction.Retiarius, "Тычок с отскоком", "Sprites/LogoCards/ТычокСОтскоком", 1, Stance.Attacking, Stance.Defensive, Card.CardType.Attack, Card.CardEffect.Damage, 2, Card.CardEffect.Movement, 1, Card.TargetType.Enemy, "После атаки передвиньте этого бойца на 1 гекс"));
        CardManager.AllCards.Add(new Card(CardRestriction.Retiarius, "Тычок с отскоком", "Sprites/LogoCards/ТычокСОтскоком", 1, Stance.Attacking, Stance.Defensive, Card.CardType.Attack, Card.CardEffect.Damage, 2, Card.CardEffect.Movement, 1, Card.TargetType.Enemy, "После атаки передвиньте этого бойца на 1 гекс"));
        CardManager.AllCards.Add(new Card(CardRestriction.Retiarius, "Протыкание ноги", "Sprites/LogoCards/ПротыканиеНоги", 1, Stance.Attacking, Stance.Attacking, Card.CardType.Attack, Card.CardEffect.Damage, 4, Card.CardEffect.DiscardEnemy, 1, Card.TargetType.Enemy, "Цель должна сбросить одну карту с руки на выбор", CardRestriction.Retiarius));
        CardManager.AllCards.Add(new Card(CardRestriction.Retiarius, "Протыкание ноги", "Sprites/LogoCards/ПротыканиеНоги", 1, Stance.Attacking, Stance.Attacking, Card.CardType.Attack, Card.CardEffect.Damage, 4, Card.CardEffect.DiscardEnemy, 1, Card.TargetType.Enemy, "Цель должна сбросить одну карту с руки на выбор", CardRestriction.Retiarius));
        CardManager.AllCards.Add(new Card(CardRestriction.Retiarius, "Осторожный удар", "Sprites/LogoCards/ОсторожныйУдар", 1, Stance.Attacking, Stance.Advance, Card.CardType.Attack, Card.CardEffect.Damage, 3, Card.CardEffect.NearCardDrow, 1, Card.TargetType.Enemy, "Если на соседних гексах нет врага, возьмите карту"));//нужен метод
        CardManager.AllCards.Add(new Card(CardRestriction.Retiarius, "Осторожный удар", "Sprites/LogoCards/ОсторожныйУдар", 1, Stance.Attacking, Stance.Advance, Card.CardType.Attack, Card.CardEffect.Damage, 3, Card.CardEffect.NearCardDrow, 1, Card.TargetType.Enemy, "Если на соседних гексах нет врага, возьмите карту"));//нужен метод
        CardManager.AllCards.Add(new Card(CardRestriction.Retiarius, "Выпад вперед", "Sprites/LogoCards/ВыпадВперёд", 1, Stance.Advance, Stance.Attacking, Card.CardType.Attack, Card.CardEffect.Movement, 1, Card.CardEffect.Damage, 2, Card.TargetType.Enemy, "Перед атакой передвиньте этого бойца на один гекс"));
        CardManager.AllCards.Add(new Card(CardRestriction.Retiarius, "Выпад вперед", "Sprites/LogoCards/ВыпадВперёд", 1, Stance.Advance, Stance.Attacking, Card.CardType.Attack, Card.CardEffect.Movement, 1, Card.CardEffect.Damage, 2, Card.TargetType.Enemy, "Перед атакой передвиньте этого бойца на один гекс"));
        CardManager.AllCards.Add(new Card(CardRestriction.Retiarius, "Выпад вперед", "Sprites/LogoCards/ВыпадВперёд", 1, Stance.Advance, Stance.Attacking, Card.CardType.Attack, Card.CardEffect.Movement, 1, Card.CardEffect.Damage, 2, Card.TargetType.Enemy, "Перед атакой передвиньте этого бойца на один гекс"));
        CardManager.AllCards.Add(new Card(CardRestriction.Retiarius, "Шаг назад", "Sprites/LogoCards/ШагНазад", 0, Stance.Advance, Stance.Defensive, Card.CardType.Defense, Card.CardEffect.Defense, 1, Card.CardEffect.Movement, 1, Card.TargetType.This, "После атаки передвиньте этого бойца на 1 гекс"));
        CardManager.AllCards.Add(new Card(CardRestriction.Retiarius,"Шаг назад", "Sprites/LogoCards/ШагНазад", 0, Stance.Advance, Stance.Defensive, Card.CardType.Defense, Card.CardEffect.Defense, 1, Card.CardEffect.Movement, 1, Card.TargetType.This, "После атаки передвиньте этого бойца на 1 гекс"));

        //Универсальные в сете "Скиссор"

        CardManager.AllCards.Add(new Card(CardRestriction.Scissor, "Яростный рывок", "Sprites/LogoCards/ЯростныйРывок", 1, Stance.Advance, Stance.Raging, Card.CardType.Attack, Card.CardEffect.ChargeStart, 4, Card.CardEffect.ChargeEnd, 4, Card.TargetType.Enemy, "Перед атакой передвиньте этого бойца на расстояние до 4 гексов. -1 урон за каждый пройденный гекс", CardRestriction.Scissor));//нужен метод
        CardManager.AllCards.Add(new Card(CardRestriction.Scissor, "Яростный рывок", "Sprites/LogoCards/ЯростныйРывок", 1, Stance.Advance, Stance.Raging, Card.CardType.Attack, Card.CardEffect.ChargeStart, 4, Card.CardEffect.ChargeEnd, 4, Card.TargetType.Enemy, "Перед атакой передвиньте этого бойца на расстояние до 4 гексов. -1 урон за каждый пройденный гекс", CardRestriction.Scissor));//нужен метод
        CardManager.AllCards.Add(new Card(CardRestriction.Scissor, "Яростный рывок", "Sprites/LogoCards/ЯростныйРывок", 1, Stance.Advance, Stance.Raging, Card.CardType.Attack, Card.CardEffect.ChargeStart, 4 ,Card.CardEffect.ChargeEnd, 4, Card.TargetType.Enemy, "Перед атакой передвиньте этого бойца на расстояние до 4 гексов. -1 урон за  каждый пройденный гекс", CardRestriction.Scissor));//нужен метод
        CardManager.AllCards.Add(new Card(CardRestriction.Scissor, "Внезапный удар", "Sprites/LogoCards/ВнезапныйУдар", 1, Stance.Advance, Stance.Attacking, Card.CardType.Attack, Card.CardEffect.Damage, 3, Card.CardEffect.CardDrow, 1, Card.TargetType.Enemy, "Возьмите карту"));
        CardManager.AllCards.Add(new Card(CardRestriction.Scissor, "Внезапный удар", "Sprites/LogoCards/ВнезапныйУдар", 1, Stance.Advance, Stance.Attacking, Card.CardType.Attack, Card.CardEffect.Damage, 3, Card.CardEffect.CardDrow, 1, Card.TargetType.Enemy, "Возьмите карту"));
        CardManager.AllCards.Add(new Card(CardRestriction.Scissor, "Внезапный удар", "Sprites/LogoCards/ВнезапныйУдар", 1, Stance.Advance, Stance.Attacking, Card.CardType.Attack, Card.CardEffect.Damage, 3, Card.CardEffect.CardDrow, 1, Card.TargetType.Enemy, "Возьмите карту"));
        CardManager.AllCards.Add(new Card(CardRestriction.Scissor, "Rip and tear", "Sprites/LogoCards/RipAndTear", 1, Stance.Raging, Stance.Advance, Card.CardType.Attack, Card.CardEffect.DiscardSelf, 2, Card.CardEffect.DamageAfterDiscard, 3, Card.TargetType.Enemy, "Перед атакой сбросьте до двух карт. +2 к урону за каждую сброшенную карту", CardRestriction.Scissor));//нужен метод   
        CardManager.AllCards.Add(new Card(CardRestriction.Scissor, "Яростная серия", "Sprites/LogoCards/ЯростнаяСерия", 1, Stance.Raging, Stance.Raging, Card.CardType.Attack, Card.CardEffect.Damage, 2, Card.CardEffect.ManaAdd, 1, Card.TargetType.Enemy, "Получите 1 очко действия", CardRestriction.Scissor));
        CardManager.AllCards.Add(new Card(CardRestriction.Scissor, "Зацепить оружие", "Sprites/LogoCards/ЗацепитьОружие", 0, Stance.Attacking, Stance.Raging, Card.CardType.Defense, Card.CardEffect.Defense, 0, Card.CardEffect.AliveCardDrow, 2, Card.TargetType.This, "Если этот боец выжил, возьмите 2 карты", CardRestriction.Scissor));
        CardManager.AllCards.Add(new Card(CardRestriction.Scissor, "Зацепить оружие", "Sprites/LogoCards/ЗацепитьОружие", 0, Stance.Attacking, Stance.Raging, Card.CardType.Defense, Card.CardEffect.Defense, 0, Card.CardEffect.AliveCardDrow, 2, Card.TargetType.This, "Если этот боец выжил, возьмите 2 карты", CardRestriction.Scissor));
        CardManager.AllCards.Add(new Card(CardRestriction.Scissor, "Разрезающий удар", "Sprites/LogoCards/РазрезающийУдар", 1, Stance.Attacking, Stance.Advance, Card.CardType.Attack, Card.CardEffect.Damage, 4, Card.CardEffect.CardDrow, 1, Card.TargetType.Enemy, "Возьмите карту", CardRestriction.Scissor));
        CardManager.AllCards.Add(new Card(CardRestriction.Scissor, "Разрезающий удар", "Sprites/LogoCards/РазрезающийУдар", 1, Stance.Attacking, Stance.Advance, Card.CardType.Attack, Card.CardEffect.Damage, 4, Card.CardEffect.CardDrow, 1, Card.TargetType.Enemy, "Возьмите карту", CardRestriction.Scissor));
        CardManager.AllCards.Add(new Card(CardRestriction.Scissor, "Удар клинком", "Sprites/LogoCards/УдарКлинком", 1, Stance.Attacking, Stance.Attacking, Card.CardType.Attack, Card.CardEffect.Damage, 3, Card.CardEffect.No, 0, Card.TargetType.Enemy));
        CardManager.AllCards.Add(new Card(CardRestriction.Scissor, "Удар клинком", "Sprites/LogoCards/УдарКлинком", 1, Stance.Attacking, Stance.Attacking, Card.CardType.Attack, Card.CardEffect.Damage, 3, Card.CardEffect.No, 0, Card.TargetType.Enemy));
        CardManager.AllCards.Add(new Card(CardRestriction.Scissor, "Удар клинком", "Sprites/LogoCards/УдарКлинком", 1, Stance.Attacking, Stance.Attacking, Card.CardType.Attack, Card.CardEffect.Damage, 3, Card.CardEffect.No, 0, Card.TargetType.Enemy));


        //Универсальные в сете "Мурмиллон"

        CardManager.AllCards.Add(new Card(CardRestriction.Murmillo, "Оглушение щитом", "Sprites/LogoCards/Оглушение", 1, Stance.Defensive, Stance.Attacking, Card.CardType.Attack, Card.CardEffect.Damage, 0, Card.CardEffect.Stun, 2, Card.TargetType.Enemy, "Смените стойку цели на наступательную. Противник должен сбросить две карты", CardRestriction.Murmillo));
        CardManager.AllCards.Add(new Card(CardRestriction.Murmillo, "Оглушение щитом", "Sprites/LogoCards/Оглушение", 1, Stance.Defensive, Stance.Attacking, Card.CardType.Attack, Card.CardEffect.Damage, 0, Card.CardEffect.Stun, 2, Card.TargetType.Enemy, "Смените стойку цели на наступательную. Противник должен сбросить две карты", CardRestriction.Murmillo));
        CardManager.AllCards.Add(new Card(CardRestriction.Murmillo, "Блок", "Sprites/LogoCards/Блок", 0, Stance.Defensive, Stance.Defensive, Card.CardType.Defense, Card.CardEffect.ShieldedDefense, 2, Card.CardEffect.No, 0, Card.TargetType.This, "+1 к защите, если этот боец Щитовик"));
        CardManager.AllCards.Add(new Card(CardRestriction.Murmillo, "Блок", "Sprites/LogoCards/Блок", 0, Stance.Defensive, Stance.Defensive, Card.CardType.Defense, Card.CardEffect.ShieldedDefense, 2, Card.CardEffect.No, 0, Card.TargetType.This, "+1 к защите, если этот боец Щитовик"));
        CardManager.AllCards.Add(new Card(CardRestriction.Murmillo, "Блок", "Sprites/LogoCards/Блок", 0, Stance.Defensive, Stance.Defensive, Card.CardType.Defense, Card.CardEffect.ShieldedDefense, 2, Card.CardEffect.No, 0, Card.TargetType.This, "+1 к защите, если этот боец Щитовик"));
        CardManager.AllCards.Add(new Card(CardRestriction.Murmillo, "Уворот", "Sprites/LogoCards/Уворот", 0, Stance.Defensive, Stance.Advance, Card.CardType.Defense, Card.CardEffect.Defense, 3, Card.CardEffect.Movement, 1, Card.TargetType.This, "После атаки передвиньте этого бойца на 1 гекс"));
        CardManager.AllCards.Add(new Card(CardRestriction.Murmillo, "Заверщающий рубец", "Sprites/LogoCards/Рубец", 1, Stance.Attacking, Stance.Advance, Card.CardType.Attack, Card.CardEffect.DamageFinisher, 2, Card.CardEffect.No, 0, Card.TargetType.Enemy, "+1 к урону за каждую карту, разыгранную в эту активацию"));
        CardManager.AllCards.Add(new Card(CardRestriction.Murmillo, "Заверщающий рубец", "Sprites/LogoCards/Рубец", 1, Stance.Attacking, Stance.Advance, Card.CardType.Attack, Card.CardEffect.DamageFinisher, 2, Card.CardEffect.No, 0, Card.TargetType.Enemy, "+1 к урону за каждую карту, разыгранную в эту активацию"));
        CardManager.AllCards.Add(new Card(CardRestriction.Murmillo, "Отталкивание", "Sprites/LogoCards/Отталкивание", 1, Stance.Attacking, Stance.Defensive, Card.CardType.Attack, Card.CardEffect.PushBackEnemy, 1, Card.CardEffect.CardDrow, 1, Card.TargetType.Enemy, "Передвиньте врага на 1 гекс. Возьмите карту", CardRestriction.Murmillo));
        CardManager.AllCards.Add(new Card(CardRestriction.Murmillo, "Отталкивание", "Sprites/LogoCards/Отталкивание", 1, Stance.Attacking, Stance.Defensive, Card.CardType.Attack, Card.CardEffect.PushBackEnemy, 1, Card.CardEffect.CardDrow, 1, Card.TargetType.Enemy, "Передвиньте врага на 1 гекс. Возьмите карту", CardRestriction.Murmillo));
        CardManager.AllCards.Add(new Card(CardRestriction.Murmillo, "Удар клинком", "Sprites/LogoCards/УдарКлинком", 1, Stance.Attacking, Stance.Attacking, Card.CardType.Attack, Card.CardEffect.Damage, 3, Card.CardEffect.No, 0, Card.TargetType.Enemy));
        CardManager.AllCards.Add(new Card(CardRestriction.Murmillo, "Удар клинком", "Sprites/LogoCards/УдарКлинком", 1, Stance.Attacking, Stance.Attacking, Card.CardType.Attack, Card.CardEffect.Damage, 3, Card.CardEffect.No, 0, Card.TargetType.Enemy));
        CardManager.AllCards.Add(new Card(CardRestriction.Murmillo, "Прикрыться", "Sprites/LogoCards/Прикрыться", 0, Stance.Advance, Stance.Defensive, Card.CardType.Defense, Card.CardEffect.ShieldedDefense, 1, Card.CardEffect.CardDrow, 1, Card.TargetType.This, "+1 к защите, если этот боец Щитовик. Возьмите карту"));
        CardManager.AllCards.Add(new Card(CardRestriction.Murmillo, "Прикрыться", "Sprites/LogoCards/Прикрыться", 0, Stance.Advance, Stance.Defensive, Card.CardType.Defense, Card.CardEffect.ShieldedDefense, 1, Card.CardEffect.CardDrow, 1, Card.TargetType.This, "+1 к защите, если этот боец Щитовик. Возьмите карту"));
        CardManager.AllCards.Add(new Card(CardRestriction.Murmillo, "Внезапный удар", "Sprites/LogoCards/ВнезапныйУдар", 1, Stance.Advance, Stance.Attacking, Card.CardType.Attack, Card.CardEffect.Damage, 3, Card.CardEffect.CardDrow, 1, Card.TargetType.Enemy, "Возьмите карту"));
        CardManager.AllCards.Add(new Card(CardRestriction.Murmillo, "Внезапный удар", "Sprites/LogoCards/ВнезапныйУдар", 1, Stance.Advance, Stance.Attacking, Card.CardType.Attack, Card.CardEffect.Damage, 3, Card.CardEffect.CardDrow, 1, Card.TargetType.Enemy, "Возьмите карту"));


        //Универсальные в сете "Гопломах"

        CardManager.AllCards.Add(new Card(CardRestriction.Hoplomachus, "Уворот", "Sprites/LogoCards/Уворот", 0, Stance.Defensive, Stance.Advance, Card.CardType.Defense, Card.CardEffect.Defense, 3, Card.CardEffect.Movement, 1, Card.TargetType.This, "После атаки передвиньте этого бойца на 1 гекс"));
        CardManager.AllCards.Add(new Card(CardRestriction.Hoplomachus, "Уворот", "Sprites/LogoCards/Уворот", 0, Stance.Defensive, Stance.Advance, Card.CardType.Defense, Card.CardEffect.Defense, 3, Card.CardEffect.Movement, 1, Card.TargetType.This, "После атаки передвиньте этого бойца на 1 гекс"));
        CardManager.AllCards.Add(new Card(CardRestriction.Hoplomachus, "Блок", "Sprites/LogoCards/Блок", 0, Stance.Defensive, Stance.Defensive, Card.CardType.Defense, Card.CardEffect.ShieldedDefense, 2, Card.CardEffect.No, 1, Card.TargetType.This, "+1 к защите, если этот боец Щитовик"));
        CardManager.AllCards.Add(new Card(CardRestriction.Hoplomachus, "Блок", "Sprites/LogoCards/Блок", 0, Stance.Defensive, Stance.Defensive, Card.CardType.Defense, Card.CardEffect.ShieldedDefense, 2, Card.CardEffect.No, 1, Card.TargetType.This, "+1 к защите, если этот боец Щитовик"));
        CardManager.AllCards.Add(new Card(CardRestriction.Hoplomachus, "Контратака", "Sprites/LogoCards/Контратака", 1, Stance.Defensive, Stance.Attacking, Card.CardType.Attack, Card.CardEffect.Damage, 3, Card.CardEffect.CardDrow, 1, Card.TargetType.Enemy, "Возьмите карту"));
        CardManager.AllCards.Add(new Card(CardRestriction.Hoplomachus, "Контратака", "Sprites/LogoCards/Контратака", 1, Stance.Defensive, Stance.Attacking, Card.CardType.Attack, Card.CardEffect.Damage, 3, Card.CardEffect.CardDrow, 1, Card.TargetType.Enemy, "Возьмите карту"));
        CardManager.AllCards.Add(new Card(CardRestriction.Hoplomachus, "Выпад вперед", "Sprites/LogoCards/ВыпадВперёд", 1, Stance.Advance, Stance.Attacking, Card.CardType.Attack, Card.CardEffect.Movement, 1, Card.CardEffect.Damage, 2, Card.TargetType.Enemy, "Перед атакой передвиньте этого бойца на один гекс"));
        CardManager.AllCards.Add(new Card(CardRestriction.Hoplomachus, "Выпад вперед", "Sprites/LogoCards/ВыпадВперёд", 1, Stance.Advance, Stance.Attacking, Card.CardType.Attack, Card.CardEffect.Movement, 1, Card.CardEffect.Damage, 2, Card.TargetType.Enemy, "Перед атакой передвиньте этого бойца на один гекс"));
        CardManager.AllCards.Add(new Card(CardRestriction.Hoplomachus, "Выпад вперед", "Sprites/LogoCards/ВыпадВперёд", 1, Stance.Advance, Stance.Attacking, Card.CardType.Attack, Card.CardEffect.Movement, 1, Card.CardEffect.Damage, 2, Card.TargetType.Enemy, "Перед атакой передвиньте этого бойца на один гекс"));
        CardManager.AllCards.Add(new Card(CardRestriction.Hoplomachus, "Прикрыться", "Sprites/LogoCards/Прикрыться", 0, Stance.Advance, Stance.Defensive, Card.CardType.Defense, Card.CardEffect.ShieldedDefense, 1, Card.CardEffect.CardDrow, 1, Card.TargetType.This, "+1 к защите, если этот боец Щитовик. Возьмите карту"));
        CardManager.AllCards.Add(new Card(CardRestriction.Hoplomachus, "Прикрыться", "Sprites/LogoCards/Прикрыться", 0, Stance.Advance, Stance.Defensive, Card.CardType.Defense, Card.CardEffect.ShieldedDefense, 1, Card.CardEffect.CardDrow, 1, Card.TargetType.This, "+1 к защите, если этот боец Щитовик. Возьмите карту"));
        CardManager.AllCards.Add(new Card(CardRestriction.Hoplomachus, "Тычок с отскоком", "Sprites/LogoCards/ТычокСОтскоком", 1, Stance.Attacking, Stance.Defensive, Card.CardType.Attack, Card.CardEffect.Damage, 2, Card.CardEffect.Movement, 1, Card.TargetType.Enemy, "После атаки передвиньте этого бойца на 1 гекс"));
        CardManager.AllCards.Add(new Card(CardRestriction.Hoplomachus, "Тычок с отскоком", "Sprites/LogoCards/ТычокСОтскоком", 1, Stance.Attacking, Stance.Defensive, Card.CardType.Attack, Card.CardEffect.Damage, 2, Card.CardEffect.Movement, 1, Card.TargetType.Enemy, "После атаки передвиньте этого бойца на 1 гекс"));
        CardManager.AllCards.Add(new Card(CardRestriction.Hoplomachus, "Осторожный удар", "Sprites/LogoCards/ОсторожныйУдар", 1, Stance.Attacking, Stance.Advance, Card.CardType.Attack, Card.CardEffect.Damage, 3, Card.CardEffect.NearCardDrow, 1, Card.TargetType.Enemy, "Если на соседних гексах нет врага, возьмите карту"));//нужен метод
        CardManager.AllCards.Add(new Card(CardRestriction.Hoplomachus, "Осторожный удар", "Sprites/LogoCards/ОсторожныйУдар", 1, Stance.Attacking, Stance.Advance, Card.CardType.Attack, Card.CardEffect.Damage, 3, Card.CardEffect.NearCardDrow, 1, Card.TargetType.Enemy, "Если на соседних гексах нет врага, возьмите карту"));//нужен метод
        CardManager.AllCards.Add(new Card(CardRestriction.Hoplomachus, "Преследование", "Sprites/LogoCards/Преследование", 1, Stance.Attacking, Stance.Attacking, Card.CardType.Attack, Card.CardEffect.Movement, 1, Card.CardEffect.Damage, 4, Card.TargetType.Enemy, "Перед атакой передвиньте этого бойца на один гекс", CardRestriction.Hoplomachus));
        CardManager.AllCards.Add(new Card(CardRestriction.Hoplomachus, "Преследование", "Sprites/LogoCards/Преследование", 1, Stance.Attacking, Stance.Attacking, Card.CardType.Attack, Card.CardEffect.Movement, 1, Card.CardEffect.Damage, 4, Card.TargetType.Enemy, "Перед атакой передвиньте этого бойца на один гекс", CardRestriction.Hoplomachus));

    }
}
