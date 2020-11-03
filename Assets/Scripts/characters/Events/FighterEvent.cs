using Abilities;
using characters;
using System;

public class FighterEvent : EventArgs
{
    public int Position { get; set; }
    public AbilityEffectEvent AbilityEffectEvent {get;set;}

    public FighterEvent(int position, AbilityEffectEvent abilityEffectEvent)
    {
        Position = position;
        AbilityEffectEvent = abilityEffectEvent;
    }
}
