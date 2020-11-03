using Abilities;
using characters;
using combat;
using Gui;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyControler : MonoBehaviour
{
    private Fighter fighter;
    public AbilityUser AbilityUser { get; set; }
    public GameObject PlayerGO { get; set; }

    public void Start()
    {
        fighter = GetComponent<Fighter>();
    }

    public void TakeTurn(GameObject abilityBar)
    {
        StartCoroutine(UseAbility(PlayerGO, abilityBar));
    }

    private IEnumerator UseAbility(GameObject target, GameObject abilityBar)
    {
        PlayerAbilityLocker(abilityBar, true);
        foreach (Ability ability in fighter.SpellBook)
        {
                AbilityUser.Use(transform.gameObject, target, ability);
                while (AbilityUser.AbilityInUse)
                {
                    yield return null;
                } 
        }
        PlayerAbilityLocker(abilityBar, false);
    }

    private void PlayerAbilityLocker(GameObject abilityBar, bool unlock)
    {
        if (!unlock)
        {
            foreach (Transform button in abilityBar.transform)
            {
                if (button.GetComponent<AbilityButtonScript>() != null)
                {
                    button.GetComponent<AbilityButtonScript>().PernamentButtonLock = false;
                }
               
            }
        } 
        else
        {
            foreach (Transform button in abilityBar.transform)
            {
                if (button.GetComponent<AbilityButtonScript>() != null)
                {
                    button.GetComponent<AbilityButtonScript>().PernamentButtonLock = true;
                }
            }
        }
    }
}
