using System.Collections.Generic;
using UnityEngine;

namespace Tools.Components.Universal
{
    public class PotionsImages : MonoBehaviour
    {
        public static PotionsImages Instance;

        public List<Sprite> sprites;

        public Dictionary<int, Sprite> spritesDictionary;

        private void Awake()
        {
            Instance = this;
            spritesDictionary = new Dictionary<int, Sprite>();
            for (int i = 0; i < sprites.Count; i++)
            {
                spritesDictionary.Add(i, sprites[i]);
            }
        }
    }
}
