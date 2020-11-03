using Abilities;
using Assigners;
using characters;
using Gui;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.UI;

namespace combat
{
    public class AbilityButtonScript : MonoBehaviour
    {
        public GameObject player;
        public GameObject enemy;

        public bool selfCast;
        public int abilityId;
        public AbilityUser abilityUser;
        public bool EndTurn;

        private Fighter fighterCharacter;
        private Fighter fighterCharacterTarget;

        
        private bool pernamentButtonLock;
        public bool PernamentButtonLock 
        {
            get { return pernamentButtonLock; }
            set 
            { 

                pernamentButtonLock = value;
                if (pernamentButtonLock)
                {
                    GetComponent<Button>().interactable = false;
                } 
                else
                {
                    CheckButtonAvailability(fighterCharacter, null);
                }
                
            } 
        }

        // Start is called before the first frame update
        void Start()
        {
            fighterCharacter = player.GetComponent<Fighter>();
            fighterCharacterTarget = enemy.GetComponent<Fighter>();
            if (selfCast)
            {
                fighterCharacterTarget = fighterCharacter;
            }
            fighterCharacter.ResourcesChanged += CheckButtonAvailability;
            fighterCharacter.PositionChanged += CheckButtonAvailability;
        }

        private void CheckButtonAvailability(object sender, FighterEvent fighterEvent)
        {
            if (PernamentButtonLock)
            {
                return;
            }
            foreach (Ability abilityFromSpellBook in ((Fighter)sender).SpellBook)
            {
                if (abilityFromSpellBook.Id == abilityId)
                {
                    if (abilityFromSpellBook.IsAbleTouse((Fighter)sender, fighterCharacterTarget))
                    {
                        GetComponent<Button>().interactable = true;
                    }
                    else
                    {
                        GetComponent<Button>().interactable = false;
                    }
                }
            }
            if (EndTurn)
            {
                GetComponent<Button>().interactable = true;
            }
        }

        public void Use()
        {
            if (EndTurn)
            {
                return;
            }
            Ability ability = null;
            foreach (Ability abilityFromSpellBook in fighterCharacter.SpellBook)
            {
                if (abilityFromSpellBook.Id == abilityId)
                {
                    ability = abilityFromSpellBook;
                }
            }
            abilityUser.Use(player, enemy, ability);
        }


    }
}
