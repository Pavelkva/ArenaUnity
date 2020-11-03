using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;

namespace Assigners
{
    public static class IconAssigner
    {
        private static Dictionary<int, string> iconDictionary = new Dictionary<int, string>();

        public static string path = "Images/Icons/";

        static IconAssigner()
        {
            iconDictionary.Add(0, "sword");
            iconDictionary.Add(1, "scream");
            iconDictionary.Add(2, "skull");
            iconDictionary.Add(3, "skull");
            iconDictionary.Add(4, "healing");
            iconDictionary.Add(5, "healing");
            iconDictionary.Add(6, "mp");
            iconDictionary.Add(7, "skull");
            iconDictionary.Add(10, "fireball");
            iconDictionary.Add(12, "fireball");
            iconDictionary.Add(50, "walking-boot-left");
            iconDictionary.Add(40, "walking-boot-right");
        }

        public static string GetIconName(int id)
        {
            if (iconDictionary.ContainsKey(id))
            {
                return iconDictionary[id];
            }
            return null;
        }
    }
}

