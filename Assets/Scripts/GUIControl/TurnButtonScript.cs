using characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnButtonScript : MonoBehaviour
{
    public GameObject arena;
    public void TurnEnd()
    {
        arena.GetComponent<Arena>().EndTurn();
    }
}
