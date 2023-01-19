using UnityEngine;


public class Card : EnumCard
{
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

    public Card(CardRestriction set, string name, string logoPath, int manacost, Stance startStance = 0,
        Stance endStance = 0, CardType type = 0,
        CardEffect firstCardEffect = 0, int spellValue = 0, CardEffect firstCardEffectTwo = 0,
        int secondSpellValue = 0,
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