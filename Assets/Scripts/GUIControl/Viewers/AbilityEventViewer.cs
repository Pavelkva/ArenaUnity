using Abilities;
using characters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class AbilityEventViewer : MonoBehaviour
{
    private Fighter fighter;
    private List<int> ignore;

    public void Start()
    {
        fighter = GetComponent<Fighter>();
        fighter.EffectTaked += ShowText;
        ignore = new List<int> { 6, 5 };
    }

    // Update is called once per frame
    private void ShowText(object sender, FighterEvent fighterEvent)
    {
        if (!ignore.Contains(fighterEvent.AbilityEffectEvent.AbilityEffect.Id))
        {
            AbilityEffectEvent abee = fighterEvent.AbilityEffectEvent;

            GameObject textobjectInsta = Instantiate(Resources.Load("Prefabs/TextObject") as GameObject);
            textobjectInsta.transform.position = new Vector3(transform.position.x, transform.position.y + 7, transform.position.z);

            TextMeshPro textMesh = textobjectInsta.GetComponent<TextMeshPro>();
            textMesh.color = GetTextColor(abee);
            textMesh.text = GetTextValue(abee);
            StartCoroutine(MoveToPosition(textobjectInsta.transform, new Vector3(textobjectInsta.transform.position.x, textobjectInsta.transform.position.y + 2, textobjectInsta.transform.position.z), 1.5f));
            StartCoroutine(FadeTextToZeroAlpha(1.5f, textobjectInsta.GetComponent<TextMeshPro>(), textobjectInsta));
        }
    }

    private IEnumerator FadeTextToZeroAlpha(float t, TextMeshPro i, GameObject objectToDestroy)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
        Destroy(objectToDestroy);
    }

    public IEnumerator MoveToPosition(Transform transform, Vector3 position, float timeToMove)
    {
        var currentPos = transform.position;
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            transform.position = Vector3.Lerp(currentPos, position, t);
            yield return null;
        }
    }

    private Color GetTextColor(AbilityEffectEvent abee)
    {
        switch (abee.EventTarget)
        {
            case AbilityEffectEvent.EventTargetType.DAMAGE:
                return new Color(0.74f, 0.01f, 0.01f);
            case AbilityEffectEvent.EventTargetType.HEAL:
                return new Color(0.01f, 0.74f, 0.01f);
            default:
                return new Color(1, 1, 1);
        }
    }

    private string GetTextValue(AbilityEffectEvent abee)
    {
        if (abee.Hit == 0)
        {
            return "miss";
        }
        if (abee.EventTarget == AbilityEffectEvent.EventTargetType.DAMAGE)
        {
            return "-" + abee.RealValue.ToString();
        }
        return abee.RealValue.ToString();
    }
}
