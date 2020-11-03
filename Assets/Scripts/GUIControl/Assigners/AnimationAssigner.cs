using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assigners
{
    public static class AnimationAssigner
    {
        private static Dictionary<int, string[]> animDictionary = new Dictionary<int, string[]>();
        static AnimationAssigner()
        {
            animDictionary.Add(0, new string[] { "Attack Main Hand 1" });
            animDictionary.Add(1, new string[] { "Cast 1" });
            animDictionary.Add(2, new string[] { "Cast 2" });
            animDictionary.Add(4, new string[] { "Cast 1" });
            animDictionary.Add(10, new string[] { "Cast 2" });

        }

        public static string[] GetAnimationName(int id)
        {
            if (animDictionary.ContainsKey(id))
            {
                return animDictionary[id];
            }
            return null;
        }
    }
}


