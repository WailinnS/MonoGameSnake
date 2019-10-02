using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake9_10_2019
{
    public class Sprite
    {
        public Texture2D Image { set; get; }
        public Vector2 Position { set; get; }
        public float Rotation { get; set; } //variable to store how much to rotate the sprite
        public Vector2 Origin { get; set; } //sets the origin to rotate from.


        public float X
        {
            get => Position.X;
            set
            {
                Position = new Vector2(value, Position.Y);
            }
        }

        public float Y
        {
            get => Position.Y;
            set
            {
                Position = new Vector2(Position.X, value);
            }
        }

        public Rectangle hitBox
        {
            get
            {
                return new Rectangle((int)(X - Origin.X), (int)(Y - Origin.Y), Image.Width, Image.Height);
            }
        }

        public Color Tint { set; get; }

        public Sprite(Texture2D image, Vector2 position, Color tint)
        {
            Image = image;
            Position = position;
            Tint = tint;

            Origin = new Vector2(image.Width / 2, image.Height / 2); //sets any sprite orign to the center of the image.
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //image, position, null (for rectrangle), tint, rotation(how much to rotate the imge from in radians), origin(at what origin to rotate at, scale,spriteEffects,layerDepth
            spriteBatch.Draw(Image, Position, null, Tint, Rotation, Origin, 1f, SpriteEffects.None, 0);
        }
    }
}
