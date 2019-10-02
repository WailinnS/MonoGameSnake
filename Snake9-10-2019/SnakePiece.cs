using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake9_10_2019
{
    public class SnakePiece : Sprite
    {
        //Vector2 Speed { set; get; }
        public Directions Direction { set; get; }
        public SnakePiece(Texture2D image, Vector2 postion, Color tint)
            : base(image,postion, tint)
        {
      
            Direction = Directions.None;
        }


    }
}
