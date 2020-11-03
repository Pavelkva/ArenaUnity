using UnityEngine;
namespace combat
{
    public class MoveButtonScript : MonoBehaviour
    {
        public GameObject floor;

        public void ShowFloor()
        {
            floor.transform.localScale = new Vector2(1, 1);
        }

    }
}
