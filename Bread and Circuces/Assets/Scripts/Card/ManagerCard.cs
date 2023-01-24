using System.Collections.Generic;
using UnityEngine;

namespace Card
{
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
            var defAttStance = new List<Stance>() { Stance.Defensive, Stance.Attacking };
            var defAdvStance = new List<Stance>() { Stance.Defensive, Stance.Advance };
            var advAttStance = new List<Stance>() { Stance.Attacking, Stance.Advance };
            var defStance = new List<Stance>() { Stance.Defensive };
            var attStance = new List<Stance>() { Stance.Attacking };
            var advStance = new List<Stance> { Stance.Advance };
            var anyStance = new List<Stance>() { Stance.Defensive, Stance.Advance, Stance.Attacking };
            var rageStance = new List<Stance>() { Stance.Raging };

            //"Ретиарий"

            CardManager.AllCards.Add(new Card(EnumCard.CardRestriction.Retiarius, "Бросок сети",
                "Sprites/LogoCards/БросокСети", 1, anyStance, Stance.Attacking, EnumCard.CardType.Defense,
                EnumCard.CardEffect.Defense, 15,
                EnumCard.CardEffect.CancelCard, 0, EnumCard.TargetType.Enemy, "Отмените все эффекты вражеской карты",
                EnumCard.CardRestriction.Retiarius)); // Работа со способностью
            CardManager.AllCards.Add(new Card(EnumCard.CardRestriction.Retiarius, "Уворот", "Sprites/LogoCards/Уворот",
                0,
                defStance, Stance.Advance, EnumCard.CardType.Defense, EnumCard.CardEffect.Defense, 3,
                EnumCard.CardEffect.Movement, 1, EnumCard.TargetType.This, "После атаки передвиньте этого бойца на 1 гекс"));
            CardManager.AllCards.Add(new Card(EnumCard.CardRestriction.Retiarius, "Уворот", "Sprites/LogoCards/Уворот",
                0,
                defStance, Stance.Advance, EnumCard.CardType.Defense, EnumCard.CardEffect.Defense, 3,
                EnumCard.CardEffect.Movement, 1, EnumCard.TargetType.This, "После атаки передвиньте этого бойца на 1 гекс"));
            CardManager.AllCards.Add(new Card(EnumCard.CardRestriction.Retiarius, "Тычок с отскоком",
                "Sprites/LogoCards/ТычокСОтскоком", 1, advAttStance, Stance.Defensive, EnumCard.CardType.Attack,
                EnumCard.CardEffect.Damage, 2, EnumCard.CardEffect.Movement, 1, EnumCard.TargetType.Enemy,
                "После атаки передвиньте этого бойца на 1 гекс"));
            CardManager.AllCards.Add(new Card(EnumCard.CardRestriction.Retiarius, "Тычок с отскоком",
                "Sprites/LogoCards/ТычокСОтскоком", 1, advAttStance, Stance.Defensive, EnumCard.CardType.Attack,
                EnumCard.CardEffect.Damage, 2, EnumCard.CardEffect.Movement, 1, EnumCard.TargetType.Enemy,
                "После атаки передвиньте этого бойца на 1 гекс"));
            CardManager.AllCards.Add(new Card(EnumCard.CardRestriction.Retiarius, "Протыкание ноги",
                "Sprites/LogoCards/ПротыканиеНоги", 1, attStance, Stance.Attacking, EnumCard.CardType.Attack,
                EnumCard.CardEffect.Damage, 4, EnumCard.CardEffect.DiscardEnemy, 1, EnumCard.TargetType.Enemy,
                "Цель должна сбросить одну карту с руки на выбор", EnumCard.CardRestriction.Retiarius));
            CardManager.AllCards.Add(new Card(EnumCard.CardRestriction.Retiarius, "Протыкание ноги",
                "Sprites/LogoCards/ПротыканиеНоги", 1, attStance, Stance.Attacking, EnumCard.CardType.Attack,
                EnumCard.CardEffect.Damage, 4, EnumCard.CardEffect.DiscardEnemy, 1, EnumCard.TargetType.Enemy,
                "Цель должна сбросить одну карту с руки на выбор", EnumCard.CardRestriction.Retiarius));
            CardManager.AllCards.Add(new Card(EnumCard.CardRestriction.Retiarius, "Осторожный удар",
                "Sprites/LogoCards/ОсторожныйУдар", 1, attStance, Stance.Advance, EnumCard.CardType.Attack,
                EnumCard.CardEffect.Damage, 3, EnumCard.CardEffect.NearCardDrow, 1, EnumCard.TargetType.Enemy,
                "Если на соседних гексах нет врага, возьмите карту")); //нужен метод
            CardManager.AllCards.Add(new Card(EnumCard.CardRestriction.Retiarius, "Осторожный удар",
                "Sprites/LogoCards/ОсторожныйУдар", 1, attStance, Stance.Advance, EnumCard.CardType.Attack,
                EnumCard.CardEffect.Damage, 3, EnumCard.CardEffect.NearCardDrow, 1, EnumCard.TargetType.Enemy,
                "Если на соседних гексах нет врага, возьмите карту")); //нужен метод
            CardManager.AllCards.Add(new Card(EnumCard.CardRestriction.Retiarius, "Выпад вперед",
                "Sprites/LogoCards/ВыпадВперёд", 1,
                defAdvStance, Stance.Attacking, EnumCard.CardType.Attack, EnumCard.CardEffect.Movement, 1,
                EnumCard.CardEffect.Damage,
                2, EnumCard.TargetType.Enemy, "Перед атакой передвиньте этого бойца на один гекс"));
            CardManager.AllCards.Add(new Card(EnumCard.CardRestriction.Retiarius, "Выпад вперед",
                "Sprites/LogoCards/ВыпадВперёд", 1,
                defAdvStance, Stance.Attacking, EnumCard.CardType.Attack, EnumCard.CardEffect.Movement, 1,
                EnumCard.CardEffect.Damage,
                2, EnumCard.TargetType.Enemy, "Перед атакой передвиньте этого бойца на один гекс"));
            CardManager.AllCards.Add(new Card(EnumCard.CardRestriction.Retiarius, "Выпад вперед",
                "Sprites/LogoCards/ВыпадВперёд", 1,
                defAdvStance, Stance.Attacking, EnumCard.CardType.Attack, EnumCard.CardEffect.Movement, 1,
                EnumCard.CardEffect.Damage,
                2, EnumCard.TargetType.Enemy, "Перед атакой передвиньте этого бойца на один гекс"));
            CardManager.AllCards.Add(new Card(EnumCard.CardRestriction.Retiarius, "Выпад вперед",
                "Sprites/LogoCards/ВыпадВперёд", 1,
                defAdvStance, Stance.Attacking, EnumCard.CardType.Attack, EnumCard.CardEffect.Movement, 1,
                EnumCard.CardEffect.Damage,
                2, EnumCard.TargetType.Enemy, "Перед атакой передвиньте этого бойца на один гекс"));
            CardManager.AllCards.Add(new Card(EnumCard.CardRestriction.Retiarius, "Шаг назад",
                "Sprites/LogoCards/ШагНазад", 0,
                defAdvStance, Stance.Defensive, EnumCard.CardType.Defense, EnumCard.CardEffect.Defense, 1,
                EnumCard.CardEffect.Movement, 1, EnumCard.TargetType.This, "После атаки передвиньте этого бойца на 1 гекс"));
            CardManager.AllCards.Add(new Card(EnumCard.CardRestriction.Retiarius, "Шаг назад",
                "Sprites/LogoCards/ШагНазад", 0,
                defAdvStance, Stance.Defensive, EnumCard.CardType.Defense, EnumCard.CardEffect.Defense, 1,
                EnumCard.CardEffect.Movement, 1, EnumCard.TargetType.This, "После атаки передвиньте этого бойца на 1 гекс"));

            //Универсальные в сете "Скиссор"

            CardManager.AllCards.Add(new Card(EnumCard.CardRestriction.Scissor, "Яростный рывок",
                "Sprites/LogoCards/ЯростныйРывок",
                1, advAttStance, Stance.Raging, EnumCard.CardType.Attack, EnumCard.CardEffect.ChargeStart, 4,
                EnumCard.CardEffect.ChargeEnd, 4, EnumCard.TargetType.Enemy,
                "Перед атакой передвиньте этого бойца на расстояние до 4 гексов. -1 урон за каждый пройденный гекс",
                EnumCard.CardRestriction.Scissor)); //нужен метод
            CardManager.AllCards.Add(new Card(EnumCard.CardRestriction.Scissor, "Яростный рывок",
                "Sprites/LogoCards/ЯростныйРывок",
                1, advStance, Stance.Raging, EnumCard.CardType.Attack, EnumCard.CardEffect.ChargeStart, 4,
                EnumCard.CardEffect.ChargeEnd, 4, EnumCard.TargetType.Enemy,
                "Перед атакой передвиньте этого бойца на расстояние до 4 гексов. -1 урон за каждый пройденный гекс",
                EnumCard.CardRestriction.Scissor)); //нужен метод
            CardManager.AllCards.Add(new Card(EnumCard.CardRestriction.Scissor, "Яростный рывок",
                "Sprites/LogoCards/ЯростныйРывок",
                1, advStance, Stance.Raging, EnumCard.CardType.Attack, EnumCard.CardEffect.ChargeStart, 4,
                EnumCard.CardEffect.ChargeEnd, 4, EnumCard.TargetType.Enemy,
                "Перед атакой передвиньте этого бойца на расстояние до 4 гексов. -1 урон за  каждый пройденный гекс",
                EnumCard.CardRestriction.Scissor)); //нужен метод
            CardManager.AllCards.Add(new Card(EnumCard.CardRestriction.Scissor, "Внезапный удар",
                "Sprites/LogoCards/ВнезапныйУдар",
                1, advStance, Stance.Attacking, EnumCard.CardType.Attack, EnumCard.CardEffect.Damage, 3,
                EnumCard.CardEffect.CardDrow, 1, EnumCard.TargetType.Enemy, "Возьмите карту"));
            CardManager.AllCards.Add(new Card(EnumCard.CardRestriction.Scissor, "Внезапный удар",
                "Sprites/LogoCards/ВнезапныйУдар",
                1, advStance, Stance.Attacking, EnumCard.CardType.Attack, EnumCard.CardEffect.Damage, 3,
                EnumCard.CardEffect.CardDrow, 1, EnumCard.TargetType.Enemy, "Возьмите карту"));
            CardManager.AllCards.Add(new Card(EnumCard.CardRestriction.Scissor, "Внезапный удар",
                "Sprites/LogoCards/ВнезапныйУдар",
                1, advStance, Stance.Attacking, EnumCard.CardType.Attack, EnumCard.CardEffect.Damage, 3,
                EnumCard.CardEffect.CardDrow, 1, EnumCard.TargetType.Enemy, "Возьмите карту"));
            CardManager.AllCards.Add(new Card(EnumCard.CardRestriction.Scissor, "Rip and tear",
                "Sprites/LogoCards/RipAndTear", 1,
                rageStance, Stance.Advance, EnumCard.CardType.Attack, EnumCard.CardEffect.DiscardSelf, 2,
                EnumCard.CardEffect.DamageAfterDiscard, 3, EnumCard.TargetType.Enemy,
                "Перед атакой сбросьте до двух карт. +2 к урону за каждую сброшенную карту",
                EnumCard.CardRestriction.Scissor)); //нужен метод   
            CardManager.AllCards.Add(new Card(EnumCard.CardRestriction.Scissor, "Яростная серия",
                "Sprites/LogoCards/ЯростнаяСерия",
                1, rageStance, Stance.Raging, EnumCard.CardType.Attack, EnumCard.CardEffect.Damage, 2,
                EnumCard.CardEffect.ManaAdd,
                1, EnumCard.TargetType.Enemy, "Получите 1 очко действия", EnumCard.CardRestriction.Scissor));
            CardManager.AllCards.Add(new Card(EnumCard.CardRestriction.Scissor, "Зацепить оружие",
                "Sprites/LogoCards/ЗацепитьОружие", 0, advAttStance, Stance.Raging, EnumCard.CardType.Defense,
                EnumCard.CardEffect.Defense, 0, EnumCard.CardEffect.AliveCardDrow, 2, EnumCard.TargetType.This,
                "Если этот боец выжил, возьмите 2 карты", EnumCard.CardRestriction.Scissor));
            CardManager.AllCards.Add(new Card(EnumCard.CardRestriction.Scissor, "Зацепить оружие",
                "Sprites/LogoCards/ЗацепитьОружие", 0, advAttStance, Stance.Raging, EnumCard.CardType.Defense,
                EnumCard.CardEffect.Defense, 0, EnumCard.CardEffect.AliveCardDrow, 2, EnumCard.TargetType.This,
                "Если этот боец выжил, возьмите 2 карты", EnumCard.CardRestriction.Scissor));
            CardManager.AllCards.Add(new Card(EnumCard.CardRestriction.Scissor, "Разрезающий удар",
                "Sprites/LogoCards/РазрезающийУдар", 1, attStance, Stance.Advance, EnumCard.CardType.Attack,
                EnumCard.CardEffect.Damage, 4, EnumCard.CardEffect.CardDrow, 1, EnumCard.TargetType.Enemy, "Возьмите карту",
                EnumCard.CardRestriction.Scissor));
            CardManager.AllCards.Add(new Card(EnumCard.CardRestriction.Scissor, "Разрезающий удар",
                "Sprites/LogoCards/РазрезающийУдар", 1, attStance, Stance.Advance, EnumCard.CardType.Attack,
                EnumCard.CardEffect.Damage, 4, EnumCard.CardEffect.CardDrow, 1, EnumCard.TargetType.Enemy, "Возьмите карту",
                EnumCard.CardRestriction.Scissor));
            CardManager.AllCards.Add(new Card(EnumCard.CardRestriction.Scissor, "Удар клинком",
                "Sprites/LogoCards/УдарКлинком", 1,
                attStance, Stance.Attacking, EnumCard.CardType.Attack, EnumCard.CardEffect.Damage, 3, EnumCard.CardEffect.No,
                0,
                EnumCard.TargetType.Enemy));
            CardManager.AllCards.Add(new Card(EnumCard.CardRestriction.Scissor, "Удар клинком",
                "Sprites/LogoCards/УдарКлинком", 1,
                attStance, Stance.Attacking, EnumCard.CardType.Attack, EnumCard.CardEffect.Damage, 3, EnumCard.CardEffect.No,
                0,
                EnumCard.TargetType.Enemy));
            CardManager.AllCards.Add(new Card(EnumCard.CardRestriction.Scissor, "Удар клинком",
                "Sprites/LogoCards/УдарКлинком", 1,
                attStance, Stance.Attacking, EnumCard.CardType.Attack, EnumCard.CardEffect.Damage, 3, EnumCard.CardEffect.No,
                0,
                EnumCard.TargetType.Enemy));


            //Универсальные в сете "Мурмиллон"

            CardManager.AllCards.Add(new Card(EnumCard.CardRestriction.Murmillo, "Оглушение щитом",
                "Sprites/LogoCards/ОглушениеЩитом", 1, anyStance, Stance.Attacking, EnumCard.CardType.Attack,
                EnumCard.CardEffect.Damage, 0, EnumCard.CardEffect.Stun, 2, EnumCard.TargetType.Enemy,
                "Смените стойку цели на атакующую. Противник должен сбросить две карты",
                EnumCard.CardRestriction.Murmillo));
            CardManager.AllCards.Add(new Card(EnumCard.CardRestriction.Murmillo, "Оглушение щитом",
                "Sprites/LogoCards/ОглушениеЩитом", 1, anyStance, Stance.Attacking, EnumCard.CardType.Attack,
                EnumCard.CardEffect.Damage, 0, EnumCard.CardEffect.Stun, 2, EnumCard.TargetType.Enemy,
                "Смените стойку цели на атакующую. Противник должен сбросить две карты",
                EnumCard.CardRestriction.Murmillo));
            CardManager.AllCards.Add(new Card(EnumCard.CardRestriction.Murmillo, "Блок", "Sprites/LogoCards/Блок", 0,
                defStance, Stance.Defensive, EnumCard.CardType.Defense, EnumCard.CardEffect.ShieldedDefense, 2,
                EnumCard.CardEffect.No, 0, EnumCard.TargetType.This, "+1 к защите, если этот боец Щитовик"));
            CardManager.AllCards.Add(new Card(EnumCard.CardRestriction.Murmillo, "Блок", "Sprites/LogoCards/Блок", 0,
                defStance, Stance.Defensive, EnumCard.CardType.Defense, EnumCard.CardEffect.ShieldedDefense, 2,
                EnumCard.CardEffect.No, 0, EnumCard.TargetType.This, "+1 к защите, если этот боец Щитовик"));
            CardManager.AllCards.Add(new Card(EnumCard.CardRestriction.Murmillo, "Блок", "Sprites/LogoCards/Блок", 0,
                defStance, Stance.Defensive, EnumCard.CardType.Defense, EnumCard.CardEffect.ShieldedDefense, 2,
                EnumCard.CardEffect.No, 0, EnumCard.TargetType.This, "+1 к защите, если этот боец Щитовик"));
            CardManager.AllCards.Add(new Card(EnumCard.CardRestriction.Murmillo, "Блок", "Sprites/LogoCards/Блок", 0,
                defStance, Stance.Defensive, EnumCard.CardType.Defense, EnumCard.CardEffect.ShieldedDefense, 2,
                EnumCard.CardEffect.No, 0, EnumCard.TargetType.This, "+1 к защите, если этот боец Щитовик"));
            CardManager.AllCards.Add(new Card(EnumCard.CardRestriction.Murmillo, "Заверщающий рубец",
                "Sprites/LogoCards/ЗавершающийРубец", 1, attStance, Stance.Advance, EnumCard.CardType.Attack,
                EnumCard.CardEffect.DamageFinisher, 2, EnumCard.CardEffect.No, 0, EnumCard.TargetType.Enemy,
                "+1 к урону за каждую карту, разыгранную в эту активацию"));
            CardManager.AllCards.Add(new Card(EnumCard.CardRestriction.Murmillo, "Заверщающий рубец",
                "Sprites/LogoCards/ЗавершающийРубец", 1, attStance, Stance.Advance, EnumCard.CardType.Attack,
                EnumCard.CardEffect.DamageFinisher, 2, EnumCard.CardEffect.No, 0, EnumCard.TargetType.Enemy,
                "+1 к урону за каждую карту, разыгранную в эту активацию"));
            CardManager.AllCards.Add(new Card(EnumCard.CardRestriction.Murmillo, "Отталкивание",
                "Sprites/LogoCards/Отталкивание", 1,
                anyStance, Stance.Defensive, EnumCard.CardType.Attack, EnumCard.CardEffect.PushBackEnemy, 1,
                EnumCard.CardEffect.CardDrow, 2, EnumCard.TargetType.Enemy, "Передвиньте врага на 1 гекс. Возьмите 2 карты",
                EnumCard.CardRestriction.Murmillo));
            CardManager.AllCards.Add(new Card(EnumCard.CardRestriction.Murmillo, "Отталкивание",
                "Sprites/LogoCards/Отталкивание", 1,
                anyStance, Stance.Defensive, EnumCard.CardType.Attack, EnumCard.CardEffect.PushBackEnemy, 1,
                EnumCard.CardEffect.CardDrow, 2, EnumCard.TargetType.Enemy, "Передвиньте врага на 1 гекс. Возьмите 2 карты",
                EnumCard.CardRestriction.Murmillo));
            CardManager.AllCards.Add(new Card(EnumCard.CardRestriction.Murmillo, "Удар клинком",
                "Sprites/LogoCards/УдарКлинком", 1,
                attStance, Stance.Attacking, EnumCard.CardType.Attack, EnumCard.CardEffect.Damage, 3, EnumCard.CardEffect.No,
                0,
                EnumCard.TargetType.Enemy));
            CardManager.AllCards.Add(new Card(EnumCard.CardRestriction.Murmillo, "Удар клинком",
                "Sprites/LogoCards/УдарКлинком", 1,
                attStance, Stance.Attacking, EnumCard.CardType.Attack, EnumCard.CardEffect.Damage, 3, EnumCard.CardEffect.No,
                0,
                EnumCard.TargetType.Enemy));
            CardManager.AllCards.Add(new Card(EnumCard.CardRestriction.Murmillo, "Прикрыться",
                "Sprites/LogoCards/Прикрыться", 0,
                defAdvStance, Stance.Defensive, EnumCard.CardType.Defense, EnumCard.CardEffect.ShieldedDefense, 1,
                EnumCard.CardEffect.CardDrow, 1, EnumCard.TargetType.This,
                "+1 к защите, если этот боец Щитовик. Возьмите карту"));
            CardManager.AllCards.Add(new Card(EnumCard.CardRestriction.Murmillo, "Прикрыться",
                "Sprites/LogoCards/Прикрыться", 0,
                defAdvStance, Stance.Defensive, EnumCard.CardType.Defense, EnumCard.CardEffect.ShieldedDefense, 1,
                EnumCard.CardEffect.CardDrow, 1, EnumCard.TargetType.This,
                "+1 к защите, если этот боец Щитовик. Возьмите карту"));
            CardManager.AllCards.Add(new Card(EnumCard.CardRestriction.Murmillo, "Прикрыться",
                "Sprites/LogoCards/Прикрыться", 0,
                defAdvStance, Stance.Defensive, EnumCard.CardType.Defense, EnumCard.CardEffect.ShieldedDefense, 1,
                EnumCard.CardEffect.CardDrow, 1, EnumCard.TargetType.This,
                "+1 к защите, если этот боец Щитовик. Возьмите карту"));
            CardManager.AllCards.Add(new Card(EnumCard.CardRestriction.Murmillo, "Прикрыться",
                "Sprites/LogoCards/Прикрыться", 0,
                defAdvStance, Stance.Defensive, EnumCard.CardType.Defense, EnumCard.CardEffect.ShieldedDefense, 1,
                EnumCard.CardEffect.CardDrow, 1, EnumCard.TargetType.This,
                "+1 к защите, если этот боец Щитовик. Возьмите карту"));
            CardManager.AllCards.Add(new Card(EnumCard.CardRestriction.Murmillo, "Внезапный удар",
                "Sprites/LogoCards/ВнезапныйУдар",
                1, advStance, Stance.Attacking, EnumCard.CardType.Attack, EnumCard.CardEffect.Damage, 3,
                EnumCard.CardEffect.CardDrow, 1, EnumCard.TargetType.Enemy, "Возьмите карту"));
            CardManager.AllCards.Add(new Card(EnumCard.CardRestriction.Murmillo, "Внезапный удар",
                "Sprites/LogoCards/ВнезапныйУдар",
                1, advStance, Stance.Attacking, EnumCard.CardType.Attack, EnumCard.CardEffect.Damage, 3,
                EnumCard.CardEffect.CardDrow, 1, EnumCard.TargetType.Enemy, "Возьмите карту"));


            //Универсальные в сете "Гопломах"

            CardManager.AllCards.Add(new Card(EnumCard.CardRestriction.Hoplomachus, "Уворот",
                "Sprites/LogoCards/Уворот", 0,
                defStance, Stance.Advance, EnumCard.CardType.Defense, EnumCard.CardEffect.Defense, 3,
                EnumCard.CardEffect.Movement, 1, EnumCard.TargetType.This, "После атаки передвиньте этого бойца на 1 гекс"));
            CardManager.AllCards.Add(new Card(EnumCard.CardRestriction.Hoplomachus, "Уворот",
                "Sprites/LogoCards/Уворот", 0,
                defStance, Stance.Advance, EnumCard.CardType.Defense, EnumCard.CardEffect.Defense, 3,
                EnumCard.CardEffect.Movement, 1, EnumCard.TargetType.This, "После атаки передвиньте этого бойца на 1 гекс"));
            CardManager.AllCards.Add(new Card(EnumCard.CardRestriction.Hoplomachus, "Блок", "Sprites/LogoCards/Блок", 0,
                defStance, Stance.Defensive, EnumCard.CardType.Defense, EnumCard.CardEffect.ShieldedDefense, 2,
                EnumCard.CardEffect.No, 1, EnumCard.TargetType.This, "+1 к защите, если этот боец Щитовик"));
            CardManager.AllCards.Add(new Card(EnumCard.CardRestriction.Hoplomachus, "Блок", "Sprites/LogoCards/Блок", 0,
                defStance, Stance.Defensive, EnumCard.CardType.Defense, EnumCard.CardEffect.ShieldedDefense, 2,
                EnumCard.CardEffect.No, 1, EnumCard.TargetType.This, "+1 к защите, если этот боец Щитовик"));
            CardManager.AllCards.Add(new Card(EnumCard.CardRestriction.Hoplomachus, "Блок", "Sprites/LogoCards/Блок", 0,
                defStance, Stance.Defensive, EnumCard.CardType.Defense, EnumCard.CardEffect.ShieldedDefense, 2,
                EnumCard.CardEffect.No, 1, EnumCard.TargetType.This, "+1 к защите, если этот боец Щитовик"));
            CardManager.AllCards.Add(new Card(EnumCard.CardRestriction.Hoplomachus, "Выпад вперед",
                "Sprites/LogoCards/ВыпадВперёд",
                1, defAdvStance, Stance.Attacking, EnumCard.CardType.Attack, EnumCard.CardEffect.Movement, 1,
                EnumCard.CardEffect.Damage, 2, EnumCard.TargetType.Enemy, "Перед атакой передвиньте этого бойца на один гекс"));
            CardManager.AllCards.Add(new Card(EnumCard.CardRestriction.Hoplomachus, "Выпад вперед",
                "Sprites/LogoCards/ВыпадВперёд",
                1, defAdvStance, Stance.Attacking, EnumCard.CardType.Attack, EnumCard.CardEffect.Movement, 1,
                EnumCard.CardEffect.Damage, 2, EnumCard.TargetType.Enemy, "Перед атакой передвиньте этого бойца на один гекс"));
            CardManager.AllCards.Add(new Card(EnumCard.CardRestriction.Hoplomachus, "Выпад вперед",
                "Sprites/LogoCards/ВыпадВперёд",
                1, defAdvStance, Stance.Attacking, EnumCard.CardType.Attack, EnumCard.CardEffect.Movement, 1,
                EnumCard.CardEffect.Damage, 2, EnumCard.TargetType.Enemy, "Перед атакой передвиньте этого бойца на один гекс"));
            CardManager.AllCards.Add(new Card(EnumCard.CardRestriction.Hoplomachus, "Прикрыться",
                "Sprites/LogoCards/Прикрыться", 0,
                advAttStance, Stance.Defensive, EnumCard.CardType.Defense, EnumCard.CardEffect.ShieldedDefense, 1,
                EnumCard.CardEffect.CardDrow, 1, EnumCard.TargetType.This,
                "+1 к защите, если этот боец Щитовик. Возьмите карту"));
            CardManager.AllCards.Add(new Card(EnumCard.CardRestriction.Hoplomachus, "Прикрыться",
                "Sprites/LogoCards/Прикрыться", 0,
                advAttStance, Stance.Defensive, EnumCard.CardType.Defense, EnumCard.CardEffect.ShieldedDefense, 1,
                EnumCard.CardEffect.CardDrow, 1, EnumCard.TargetType.This,
                "+1 к защите, если этот боец Щитовик. Возьмите карту"));
            CardManager.AllCards.Add(new Card(EnumCard.CardRestriction.Hoplomachus, "Тычок с отскоком",
                "Sprites/LogoCards/ТычокСОтскоком", 1, advAttStance, Stance.Defensive, EnumCard.CardType.Attack,
                EnumCard.CardEffect.Damage, 2, EnumCard.CardEffect.Movement, 1, EnumCard.TargetType.Enemy,
                "После атаки передвиньте этого бойца на 1 гекс"));
            CardManager.AllCards.Add(new Card(EnumCard.CardRestriction.Hoplomachus, "Тычок с отскоком",
                "Sprites/LogoCards/ТычокСОтскоком", 1, advAttStance, Stance.Defensive, EnumCard.CardType.Attack,
                EnumCard.CardEffect.Damage, 2, EnumCard.CardEffect.Movement, 1, EnumCard.TargetType.Enemy,
                "После атаки передвиньте этого бойца на 1 гекс"));
            CardManager.AllCards.Add(new Card(EnumCard.CardRestriction.Hoplomachus, "Осторожный удар",
                "Sprites/LogoCards/ОсторожныйУдар", 1, attStance, Stance.Advance, EnumCard.CardType.Attack,
                EnumCard.CardEffect.Damage, 3, EnumCard.CardEffect.NearCardDrow, 1, EnumCard.TargetType.Enemy,
                "Если на соседних гексах нет врага, возьмите карту")); //нужен метод
            CardManager.AllCards.Add(new Card(EnumCard.CardRestriction.Hoplomachus, "Осторожный удар",
                "Sprites/LogoCards/ОсторожныйУдар", 1, attStance, Stance.Advance, EnumCard.CardType.Attack,
                EnumCard.CardEffect.Damage, 3, EnumCard.CardEffect.NearCardDrow, 1, EnumCard.TargetType.Enemy,
                "Если на соседних гексах нет врага, возьмите карту")); //нужен метод
            CardManager.AllCards.Add(new Card(EnumCard.CardRestriction.Hoplomachus, "Преследование",
                "Sprites/LogoCards/Преследование", 1, attStance, Stance.Attacking, EnumCard.CardType.Attack,
                EnumCard.CardEffect.Movement, 2, EnumCard.CardEffect.Damage, 4, EnumCard.TargetType.Enemy,
                "Перед атакой передвиньте этого бойца на один гекс", EnumCard.CardRestriction.Hoplomachus));
            CardManager.AllCards.Add(new Card(EnumCard.CardRestriction.Hoplomachus, "Преследование",
                "Sprites/LogoCards/Преследование", 1, attStance, Stance.Attacking, EnumCard.CardType.Attack,
                EnumCard.CardEffect.Movement, 2, EnumCard.CardEffect.Damage, 4, EnumCard.TargetType.Enemy,
                "Перед атакой передвиньте этого бойца на один гекс", EnumCard.CardRestriction.Hoplomachus));
            CardManager.AllCards.Add(new Card(EnumCard.CardRestriction.Hoplomachus, "Преследование",
                "Sprites/LogoCards/Преследование", 1, attStance, Stance.Attacking, EnumCard.CardType.Attack,
                EnumCard.CardEffect.Movement, 2, EnumCard.CardEffect.Damage, 4, EnumCard.TargetType.Enemy,
                "Перед атакой передвиньте этого бойца на один гекс", EnumCard.CardRestriction.Hoplomachus));
        }
    }
}