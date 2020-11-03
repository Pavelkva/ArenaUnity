using Abilities;
using Assigners;
using characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class OverTimeShow : MonoBehaviour
{
    public GameObject fighter;
    public GameObject overTimeIcon;

    void Start()
    {
        Fighter fighterGO = fighter.GetComponent<Fighter>();
        fighterGO.OverTimeChanged += OverTimeChanged;
    }
    // Update is called once per frame
    public void OverTimeChanged(object sender, FighterEvent resourcesChangedEvent)
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        foreach (OverTime overTime in fighter.GetComponent<Fighter>().SpellEffects)
        {
            GameObject overTimeIconGO = Instantiate(overTimeIcon, transform);
            GameObject abilityIcon = overTimeIconGO.transform.Find("AbilityIcon").gameObject;
            abilityIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>(IconAssigner.path + IconAssigner.GetIconName(overTime.Id));
            GameObject textGO = overTimeIconGO.transform.Find("ReamingTime").gameObject;
            textGO.GetComponent<Text>().text = overTime.ReamingTime.ToString();
            GameObject textStacksGO = overTimeIconGO.transform.Find("Stacks").gameObject;
            if (overTime.Stacks == 1)
            {
                textStacksGO.GetComponent<Text>().text = null;
            } 
            else
            {
                textStacksGO.GetComponent<Text>().text = overTime.Stacks.ToString();
            }

            
        }
    }
}
