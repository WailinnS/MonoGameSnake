using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake9_10_2019
{
    public class Food:Sprite
    {

        Random rndGen = new Random();

        public Food(Texture2D image, Vector2 position, Color tint)
            : base(image, position, tint)
        {
        }

        public void GenNewPos(GraphicsDevice graphicsDevice)
        {

            X = rndGen.Next(graphicsDevice.Viewport.Bounds.Width);
            Y = rndGen.Next(graphicsDevice.Viewport.Bounds.Height);
        }
        
    }
}
