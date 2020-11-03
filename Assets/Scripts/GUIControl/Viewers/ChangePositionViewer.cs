using CharacterCreator2D;
using characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePositionViewer : MonoBehaviour
{
    public GameObject floor;
    public GameObject enemy;

    private Animator characterAnimator;
    private CharacterViewer characterViewer;
    private Animator animator;
    private Fighter fighter;

    public void Start()
    {
        characterAnimator = GetComponent<Animator>();
        fighter = GetComponent<Fighter>();
        characterViewer = GetComponent<CharacterViewer>();
        animator = GetComponent<Animator>();
        fighter.PositionChanged += PlayReaction;
    }

    public void PlayReaction(Object sender, FighterEvent e)
    {
        enemy.GetComponent<Fighter>().EnemyPosition = fighter.Position;
        if (e != null)
        {
            StartCoroutine(MoveToPosition(transform, floor.transform.GetChild(e.Position).transform.position, 0.7f)); 
        }
    }

    public IEnumerator MoveToPosition(Transform transform, Vector3 position, float timeToMove)
    {
        if (position.x < transform.position.x)
        {
            transform.eulerAngles = new Vector2(0, 180);
            StartMovementAnimation(transform.position.x - position.x);
        }
        else
        {
            transform.eulerAngles = new Vector2(0, 0);
            StartMovementAnimation(position.x - transform.position.x);
        }

        var currentPos = transform.position;
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            transform.position = Vector3.Lerp(currentPos, position, t);
            yield return null;
        }

        if (enemy.transform.position.x < transform.position.x)
        {
            transform.eulerAngles = new Vector2(0, 180);
            enemy.transform.eulerAngles = new Vector2(0, 0);
        }
        else
        {
            transform.eulerAngles = new Vector2(0, 0);
            enemy.transform.eulerAngles = new Vector2(0, 180);
        }

        GetComponent<Animator>().Play("Idle");
    }

    private void StartMovementAnimation(float distance)
    {

        if (distance < 2)
        {
            GetComponent<Animator>().Play("Walk");
        }
        else if (distance < 5)
        {
            GetComponent<Animator>().Play("Run");
        }
        else
        {
            GetComponent<Animator>().Play("Sprint");
        }
    }
}
