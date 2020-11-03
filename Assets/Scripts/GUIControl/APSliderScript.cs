using characters;
using UnityEngine;
using UnityEngine.UI;

namespace Gui
{
    public class APSliderScript : MonoBehaviour
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

        // Update is called once per frame
        public void ResourcesChanged(Object sender, FighterEvent e)
        {
            if (slider.value != fighter.CharacterAttributes.GetAttribute(AttributeResources.Resources.ACTUALAP).Value ||
                fighter.CharacterAttributes.GetAttribute(AttributeSum.Sum.MAXACTIONPOINTS).Value != slider.maxValue)
            {
                slider.value = fighter.CharacterAttributes.GetAttribute(AttributeResources.Resources.ACTUALAP).Value;
                slider.maxValue = fighter.CharacterAttributes.GetAttribute(AttributeSum.Sum.MAXACTIONPOINTS).Value;
                text.text = slider.value.ToString() + "/" + slider.maxValue.ToString();
            }
        }

    }
}