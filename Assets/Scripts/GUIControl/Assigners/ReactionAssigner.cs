using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assigners
{
    public static class ReactionAssigner
    {
        private static Dictionary<int, string[]> animDictionary = new Dictionary<int, string[]>();
        private static Dictionary<int, string[]> emoteDictionary = new Dictionary<int, string[]>();
        static ReactionAssigner()
        {
            animDictionary.Add(0, new string[] { "Hit", "Idle" });
            emoteDictionary.Add(0, new string[] { "Hurt" });

            animDictionary.Add(11, new string[] { "Hit", "Idle" });
            emoteDictionary.Add(11, new string[] { "Hurt" });

            animDictionary.Add(12, new string[] { "Hit", "Idle" });
            emoteDictionary.Add(12, new string[] { "Hurt" });

        }

        public static string[] GetAnimationName(int id)
        {
            if (animDictionary.ContainsKey(id))
            {
                return animDictionary[id];
            }
            return null;
        }


        public static string[] GetEmoteName(int id)
        {
            if (emoteDictionary.ContainsKey(id))
            {
                return emoteDictionary[id];
            }
            return null;
        }
    }
}