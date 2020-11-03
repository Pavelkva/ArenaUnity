using characters;
using System.Collections;
using System.Collections.Generic;
using System.Data.OleDb;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gui
{
    public class HpSliderScript : MonoBehaviour
    {
        public GameObject fighterGO;

        private Slider slider;
        private Text text;
        private Fighter fighter;


        // Start is called before the first frame update
        void Start()
        {

            fighter = fighterGO.GetComponent<Fighter>();
            slider = GetComponent<Slider>();
            text = GetComponentInChildren<Text>();
            fighter.ResourcesChanged += ResourcesChanged;
        }

        public void ResourcesChanged(Object sender, FighterEvent e)
        {

            if (slider.value != fighter.CharacterAttributes.GetAttribute(AttributeResources.Resources.ACTUALHP).Value || 
                fighter.CharacterAttributes.GetAttribute(AttributeSum.Sum.MAXHP).Value != slider.maxValue)
            {
                slider.value = fighter.CharacterAttributes.GetAttribute(AttributeResources.Resources.ACTUALHP).Value;
                slider.maxValue = fighter.CharacterAttributes.GetAttribute(AttributeSum.Sum.MAXHP).Value;
                text.text = slider.value.ToString() + "/" + slider.maxValue.ToString();
            }
        }

    }
}
