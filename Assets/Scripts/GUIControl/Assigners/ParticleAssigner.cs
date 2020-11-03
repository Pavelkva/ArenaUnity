using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Assigners
{
    public static class ParticleAssigner
    {
        private static Dictionary<int, PrefabSettings[]> particleDictionary = new Dictionary<int, PrefabSettings[]>();
        public static string path = "Prefabs/Hovl Studio/Procedural fire/Prefabs/";
        static ParticleAssigner()
        {
            particleDictionary.Add(2, new PrefabSettings[]
            {
                new PrefabSettings(path + "MagicEffect", PrefabSettings.Movement.STATICTARGET, 1, 90, 4, 0, null),
            });
            particleDictionary.Add(10, new PrefabSettings[]
            {
                new PrefabSettings(path + "Magic fire pro red", PrefabSettings.Movement.MOVECASTERTARGET, 0.5f, 90, 4, -1,
                    new PrefabSettings[] {new PrefabSettings(path + "Explosion", PrefabSettings.Movement.STATICTARGET, 1, 90, 4, 1, null) }),
            });
            particleDictionary.Add(3, new PrefabSettings[]
            {
                new PrefabSettings(path + "Magic fire pro red", PrefabSettings.Movement.STATICTARGET, 0.5f, 90, 4, -1,
                   null )
            });
        }

        public static PrefabSettings[] GetParticlesSettings(int id)
        {
            if (particleDictionary.ContainsKey(id))
            {
                return particleDictionary[id];
            }
            return null;
        }

        public class PrefabSettings
        {
            public enum Movement
            {
                STATICCASTER,
                STATICTARGET,
                MOVECASTERTARGET,
                MOVETARGETCASTER,

            }
            public float TimeToGetTarget { get; set; }
            public string PrefabName { get; set; }
            public Movement PrefabMovement { get; set; }
            public float RotationZ { get; set; }
            public float PositionY { get; set; }
            public PrefabSettings[] EffectAfter {get; set;}
            public int IndexOfUse { get; set; }

            public PrefabSettings(string prefabName, Movement prefabMovement, float timeToGetTarget, float rotationZ, float positionY, int indexOfuse, PrefabSettings[] effectAfter)
            {
                PrefabName = prefabName;
                PrefabMovement = prefabMovement;
                TimeToGetTarget = timeToGetTarget;
                RotationZ = rotationZ;
                PositionY = positionY;
                EffectAfter = effectAfter;
                IndexOfUse = indexOfuse;
            }
        }

        
    }
}

