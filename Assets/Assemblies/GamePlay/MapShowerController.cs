using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ2024
{
    public class MapShowerController : MonoBehaviour
    {
        public Sprite[] sprites;
        private Stack<Sprite> spriteStack = new Stack<Sprite>();

        // Start is called before the first frame update
        void Awake()
        {

            for (int i = sprites.Length - 1; i >= 0; i--)
            {
                spriteStack.Push(sprites[i]);
            }
           

            SpriteRenderer rd = GetComponent<SpriteRenderer>();
            rd.sprite = null;

        }





        }
}
