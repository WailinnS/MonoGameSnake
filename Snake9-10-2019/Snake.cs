using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake9_10_2019
{
    public class Snake
    {
        GraphicsDevice screen;
        bool pieceAdded = false;
        Texture2D SnakeHead;
        Texture2D SnakeBody;
        public List<SnakePiece> snakePieces = new List<SnakePiece>();
        
        TimeSpan updateTargetTime = TimeSpan.FromMilliseconds(100); //set a variable called updateTargetTime to be 120 miliseconds
        TimeSpan elapsedTime =  TimeSpan.Zero; //makes a timespan varialbe to check on how much time has passed, sets to zero.

        public Snake(Texture2D snakeHead, Texture2D snakeBody, Vector2 position)
        {
            SnakeHead = snakeHead;
            SnakeBody = snakeBody;

            snakePieces.Add(new SnakePiece(SnakeHead, position, Color.White));
        }

        public void ResetGame()
        {
            snakePieces.Clear();
            snakePieces.Add(new SnakePiece(SnakeHead, new Vector2(screen.Viewport.Width/2,screen.Viewport.Height/2) , Color.White));
        }



        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < snakePieces.Count; i++)
            {
                snakePieces[i].Draw(spriteBatch);
            }
        }

        public void Grow()
        {
            snakePieces.Add(new SnakePiece(SnakeBody, snakePieces[snakePieces.Count - 1].Position, Color.White));
            pieceAdded = true;
            
        }
        public void Update(KeyboardState ks, GameTime gameTime,GraphicsDevice graphics ,Food food)
        {
            screen = graphics;
            elapsedTime += gameTime.ElapsedGameTime; //adds the current gametime to elapsedTime for running count.

            if (snakePieces[0].hitBox.Intersects(food.hitBox))
            {
                food.GenNewPos(graphics);
                Grow();
            }


            //check direction of head
            if (ks.IsKeyDown(Keys.Down) && snakePieces[0].Direction != Directions.Up)
            {
                snakePieces[0].Direction = Directions.Down;
                snakePieces[0].Rotation = MathHelper.Pi;
            }
            else if (ks.IsKeyDown(Keys.Up) && snakePieces[0].Direction != Directions.Down)
            {
                snakePieces[0].Direction = Directions.Up;
                snakePieces[0].Rotation = 0;
            }
            else if(ks.IsKeyDown(Keys.Left) && snakePieces[0].Direction != Directions.Right)
            {
                snakePieces[0].Direction = Directions.Left;
                snakePieces[0].Rotation = MathHelper.Pi * 3 / 2;
            }
            else if(ks.IsKeyDown(Keys.Right) && snakePieces[0].Direction != Directions.Left)
            {
                snakePieces[0].Direction = Directions.Right;
                snakePieces[0].Rotation = MathHelper.PiOver2;
            }


            //updates when the elapsed time has been met/exceeded or if a new piece is added.
            if (elapsedTime >= updateTargetTime || pieceAdded)
            {
                elapsedTime = TimeSpan.Zero; //resets the elapsedTime back to zero so that it will update again.

                //change all directions of the snake - head to tail
                for (int i = 0; i < snakePieces.Count; i++)
                {
                    switch (snakePieces[i].Direction)
                    {
                        case Directions.Up:
                            snakePieces[i].Position = new Vector2(snakePieces[i].Position.X, snakePieces[i].Position.Y - SnakeHead.Bounds.Height);
                            break;
                        case Directions.Down:
                            snakePieces[i].Position = new Vector2(snakePieces[i].Position.X, snakePieces[i].Position.Y + SnakeHead.Bounds.Height);
                            break;
                        case Directions.Left:
                            snakePieces[i].X -= snakePieces[i].Image.Width; //better way to do it. instead of the top 2 and bottom one.                           
                            break;
                        case Directions.Right:
                            snakePieces[i].Position = new Vector2(snakePieces[i].Position.X + SnakeHead.Width, snakePieces[i].Position.Y);
                            break;
                    }
                }

                //updates the direction of the snake from back to front.
                for (int i = snakePieces.Count - 1; i > 0; i--)
                {
                  snakePieces[i].Direction = snakePieces[i - 1].Direction;
                }


                //checks if it hits itslef
                for (int i = 1; i < snakePieces.Count; i++)
                {
                    if (snakePieces[0].hitBox.Intersects(snakePieces[i].hitBox))
                    {
                        ResetGame();
                    }                   
                }

                //checks if it hits walls
                if(snakePieces[0].Position.X + snakePieces[0].Image.Width > screen.Viewport.Width + 20)
                {
                    ResetGame();
                }
                else if(snakePieces[0].X < screen.Viewport.X)
                {
                    ResetGame();
                }
                else if(snakePieces[0].Y + snakePieces[0].Image.Height > screen.Viewport.Height + 20)
                {
                    ResetGame();
                }
                else if(snakePieces[0].Y < screen.Viewport.Y)
                {
                    ResetGame();
                }


            }
            
            //resets so that i don't keep updating.
            pieceAdded = false;
       
        }
    }
}
