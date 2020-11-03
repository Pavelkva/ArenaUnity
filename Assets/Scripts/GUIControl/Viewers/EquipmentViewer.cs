using CharacterCreator2D;
using characters;
using Items;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EquipmentViewer : MonoBehaviour
{
    private CharacterViewer characterViewer;
    private Fighter fighter;
    public void RefreshEquipment()
    {
        characterViewer = GetComponent<CharacterViewer>();
        fighter = GetComponent<Fighter>();
        foreach (Equipment.Parts part in fighter.getEquiped().Keys)
        {
            if (fighter.getEquiped()[part] != null)
            {
                foreach (SlotCategory slotCategory in (SlotCategory[])Enum.GetValues(typeof(SlotCategory)))
                {
                    if (slotCategory.ToString().Equals(part.ToString()))
                    {
                        characterViewer.EquipPart(slotCategory, EquipAssigner.GetEquipName(fighter.getEquiped()[part].Id));
                        characterViewer.SetPartColor(slotCategory, 
                            EquipAssigner.GetEquipColor(fighter.getEquiped()[part].Id, 1),
                            EquipAssigner.GetEquipColor(fighter.getEquiped()[part].Id, 2),
                            EquipAssigner.GetEquipColor(fighter.getEquiped()[part].Id, 3));
                    }
                }
            }
        }
    }
}
