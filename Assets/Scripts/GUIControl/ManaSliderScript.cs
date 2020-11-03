using characters;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gui
{ 
    public class ManaSliderScript : MonoBehaviour
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
            if (slider.value != fighter.CharacterAttributes.GetAttribute(AttributeResources.Resources.ACTUALMANA).Value ||
                fighter.CharacterAttributes.GetAttribute(AttributeSum.Sum.MAXMANA).Value != slider.maxValue)
            {
                slider.value = fighter.CharacterAttributes.GetAttribute(AttributeResources.Resources.ACTUALMANA).Value;
                slider.maxValue = fighter.CharacterAttributes.GetAttribute(AttributeSum.Sum.MAXMANA).Value;
                text.text = slider.value.ToString() + "/" + slider.maxValue.ToString();
            }
        }
    }
}
