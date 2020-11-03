using Abilities;
using Assigners;
using CharacterCreator2D;
using characters;
using combat;
using Gui;
using Items;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class Arena : MonoBehaviour
{
    public GameObject abilityBar;
    public GameObject button;
    public GameObject player;
    public GameObject enemy;
    public GameObject abilityButton;

    Fighter playerFighter;
    Fighter enemyFighter;

    List<Ability> abilities = new List<Ability>();

    public static bool abilityLocker;

    public void Start()
    {

        enemy.GetComponent<EnemyControler>().AbilityUser = GetComponent<AbilityUser>();
        enemy.GetComponent<EnemyControler>().PlayerGO = player;

        playerFighter = player.GetComponent<Fighter>();
        enemyFighter = enemy.GetComponent<Fighter>();



        playerFighter.Name = "Player";
        enemyFighter.Name = "Enemy";
        SetBaseAttributes(playerFighter, enemyFighter);

        XmlSerializer serializer = new XmlSerializer(typeof(Ability));
        StreamReader reader = new StreamReader("Assets/Xml/Spells/BaseAttack.xml");
        Ability baseAttack = (Ability)serializer.Deserialize(reader.BaseStream);
        reader = new StreamReader("Assets/Xml/Spells/StrengthBuff.xml");
        Ability strengthBuff = (Ability)serializer.Deserialize(reader.BaseStream);
        reader = new StreamReader("Assets/Xml/Spells/Dot.xml");
        Ability curse = (Ability)serializer.Deserialize(reader.BaseStream);
        reader = new StreamReader("Assets/Xml/Spells/Heal.xml");
        Ability heal = (Ability)serializer.Deserialize(reader.BaseStream);
        reader = new StreamReader("Assets/Xml/Spells/ApRegen.xml");
        Ability apRegen = (Ability)serializer.Deserialize(reader.BaseStream);
        reader = new StreamReader("Assets/Xml/Spells/ManaRegen.xml");
        Ability manaRegen = (Ability)serializer.Deserialize(reader.BaseStream);
        reader = new StreamReader("Assets/Xml/Spells/MoveToTarget.xml");
        Ability moveToEnemy = (Ability)serializer.Deserialize(reader.BaseStream);
        reader = new StreamReader("Assets/Xml/Spells/MoveFromTarget.xml");
        Ability moveFromEnemy = (Ability)serializer.Deserialize(reader.BaseStream);
        reader = new StreamReader("Assets/Xml/Spells/Fireball.xml");
        Ability fireball = (Ability)serializer.Deserialize(reader.BaseStream);
        reader = new StreamReader("Assets/Xml/Spells/Charge.xml");
        Ability charge = (Ability)serializer.Deserialize(reader.BaseStream);
        abilities.Add(moveFromEnemy);
        abilities.Add(fireball);
        playerFighter.SpellBook.Add(baseAttack);
        playerFighter.SpellBook.Add(strengthBuff);
        playerFighter.SpellBook.Add(curse);
        playerFighter.SpellBook.Add(heal);
        playerFighter.SpellBook.Add(moveFromEnemy);
        playerFighter.SpellBook.Add(moveToEnemy);
        playerFighter.SpellBook.Add(fireball);
        playerFighter.SpellBook.Add(charge);

        //enemyFighter.SpellBook.Add(fireball);
        enemyFighter.SpellBook.Add(moveFromEnemy);
        enemyFighter.SpellBook.Add(baseAttack);

        playerFighter.UseAbility(manaRegen, playerFighter, false);
        enemyFighter.UseAbility(manaRegen, enemyFighter, false);
        playerFighter.UseAbility(apRegen, playerFighter, false);
        enemyFighter.UseAbility(apRegen, enemyFighter, false);

        enemyFighter.CharacterAttributes.GetAttribute(AttributeBonus.Bonus.BONUSSTRENGTH).Value = 15;

        foreach (Ability ability in playerFighter.SpellBook)
        {
            GameObject newAbilityButton = Instantiate(abilityButton, abilityBar.transform);
            AbilityButtonScript abilityButtonScript = newAbilityButton.GetComponent<AbilityButtonScript>();
            abilityButtonScript.abilityId = ability.Id;
            abilityButtonScript.player = player;
            abilityButtonScript.enemy = enemy;
            GameObject abilityIcon = newAbilityButton.transform.Find("AbilityIcon").gameObject;
            if (IconAssigner.GetIconName(ability.Id) != null)
            {
                abilityIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>(IconAssigner.path + IconAssigner.GetIconName(ability.Id));
            }
            abilityButtonScript.abilityUser = GetComponent<AbilityUser>();
            newAbilityButton.GetComponent<Button>().interactable = false;
            newAbilityButton.name = ability.Name;
            if (ability.IsAbleTouse(playerFighter, enemyFighter))
            {
                newAbilityButton.GetComponent<Button>().interactable = true;
            }


        }

        XmlSerializer serializer2 = new XmlSerializer(typeof(Equipment));
        StreamReader reader2 = new StreamReader("Assets/Xml/Items/training_sword.xml");
        Equipment trainingSword = (Equipment)serializer2.Deserialize(reader2.BaseStream);

        playerFighter.Equip(trainingSword);

        player.GetComponent<EquipmentViewer>().RefreshEquipment();
    }

    public void EndTurn()
    {
        Debug.Log("here");
        playerFighter.CheckOverTimeEffects();
        playerFighter.EventOccured(null, EventsEnum.TURNEND);

        enemyFighter.EventOccured(null, EventsEnum.TURNSTART);

        enemy.GetComponent<EnemyControler>().TakeTurn(abilityBar);

        enemyFighter.CheckOverTimeEffects();
        enemyFighter.EventOccured(null, EventsEnum.TURNEND);

        playerFighter.EventOccured(null, EventsEnum.TURNSTART);
    }

    private void SetBaseAttributes(params Fighter[] fighters)
    {
        List<Fighter> fightersList = new List<Fighter>();
        fightersList.AddRange(fighters);

        fightersList[0].Name = "PLAYER";
        fightersList[1].Name = "ENEMY";
        fightersList[0].Position = 0;
        fightersList[1].Position = 5;
        fightersList[0].PositionLimit = 5;
        fightersList[1].PositionLimit = 5;
        fightersList[0].EnemyPosition = 5;
        fightersList[1].EnemyPosition = 0;

        foreach (Fighter fighter in fighters)
        {
            fighter.CharacterAttributes.GetAttribute(AttributeBase.Base.BASESTRENGTH).Value = 5;
            fighter.CharacterAttributes.GetAttribute(AttributeBase.Base.BASEAGILITY).Value = 5;
            fighter.CharacterAttributes.GetAttribute(AttributeBase.Base.BASEINTELLECT).Value = 5;
            fighter.CharacterAttributes.GetAttribute(AttributeResources.Resources.ACTUALHP).Value = fighter.CharacterAttributes.GetAttribute(AttributeSum.Sum.MAXHP).Value;
            fighter.CharacterAttributes.GetAttribute(AttributeResources.Resources.ACTUALMANA).Value = fighter.CharacterAttributes.GetAttribute(AttributeSum.Sum.MAXMANA).Value;
            fighter.CharacterAttributes.GetAttribute(AttributeResources.Resources.ACTUALAP).Value = fighter.CharacterAttributes.GetAttribute(AttributeSum.Sum.MAXACTIONPOINTS).Value;
        }
    }

}
