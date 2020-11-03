using Assigners;
using CharacterCreator2D;
using characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionViewer : MonoBehaviour
{
    private Animator characterAnimator;
    private CharacterViewer characterViewer;
    private Fighter fighter;

    public void Start()
    {
        characterAnimator = GetComponent<Animator>();
        fighter = GetComponent<Fighter>();
        characterViewer = GetComponent<CharacterViewer>();
        fighter.EffectTaked += PlayReaction;
    }

    public void PlayReaction(Object sender, FighterEvent e)
    {
        int abilityEffectId = e.AbilityEffectEvent.AbilityEffect.Id;
        if (e != null && e.AbilityEffectEvent.Hit > 0)
        {
            if (ReactionAssigner.GetAnimationName(abilityEffectId) != null)
            {
                foreach (string animation in ReactionAssigner.GetAnimationName(abilityEffectId))
                {
                    characterAnimator.Play(animation);
                }
                foreach (string animation in ReactionAssigner.GetEmoteName(abilityEffectId))
                {
                    characterViewer.Emote(animation, 0.4f);
                }
            }     
        }
        
    }

}
