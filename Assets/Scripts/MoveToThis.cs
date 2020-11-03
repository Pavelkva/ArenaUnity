using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace combat
{
    public class MoveToThis : MonoBehaviour
    {
        public GameObject player;
        public GameObject enemy;
        public float time;

        private void OnMouseDown()
        {
            Vector2 targetObject = transform.position;
            StartCoroutine(MoveToPosition(player.transform, targetObject, time));
            transform.parent.transform.localScale = new Vector2(0, 0);
        }

        public IEnumerator MoveToPosition(Transform transform, Vector3 position, float timeToMove)
        {
            if (position.x < player.transform.position.x)
            {
                player.transform.eulerAngles = new Vector2(0, 180);
                StartMovementAnimation(player.transform.position.x - position.x);
            }
            else
            {
                player.transform.eulerAngles = new Vector2(0, 0);
                StartMovementAnimation(position.x - player.transform.position.x);
            }

            var currentPos = transform.position;
            var t = 0f;
            while (t < 1)
            {
                t += Time.deltaTime / timeToMove;
                transform.position = Vector3.Lerp(currentPos, position, t);
                yield return null;
            }
            player.GetComponent<Animator>().Play("Idle");

            if (enemy.transform.position.x < player.transform.position.x)
            {
                player.transform.eulerAngles = new Vector2(0, 180);
                enemy.transform.eulerAngles = new Vector2(0, 0);
            }
            else
            {
                player.transform.eulerAngles = new Vector2(0, 0);
                enemy.transform.eulerAngles = new Vector2(0, 180);
            }
        }

        private void StartMovementAnimation(float distance)
        {

            if (distance < 2)
            {
                player.GetComponent<Animator>().Play("Walk");
            }
            else if (distance < 5)
            {
                player.GetComponent<Animator>().Play("Run");
            }
            else
            {
                player.GetComponent<Animator>().Play("Sprint");
            }
        }
    }
}
