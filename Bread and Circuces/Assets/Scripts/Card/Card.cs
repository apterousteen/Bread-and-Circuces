using UnityEngine;

namespace Card
{
    public class Card : EnumCard
    {
        protected internal CardEffect FirstCardEff, FirstCardEffTwo;
        protected internal Stance StartStance, EndStance;
        private TargetType SpellTarget;
        protected internal CardType Type;
        protected internal CardRestriction Restriction, CardSet;
        protected internal string Description;

        protected internal string Name;
        protected internal Sprite Logo;
        protected internal int Manacost, SpellValue, SecondSpellValue;
        protected internal bool IsPlaced;

        public Card(CardRestriction cardSet, string name, string logoPath, int manacost, Stance startStance,
            Stance endStance, CardType type ,
            CardEffect firstCardEffect , int spellValue , CardEffect firstCardEffectTwo,
            int secondSpellValue,
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
            CardSet = cardSet;
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
            CardSet = card.CardSet;
            Description = card.Description;
        }

        public Card GetCopy()
        {
            return new Card(this);
        }
    }
}