/* Ball.cs
 * Purpose: Ball component of pong game, with random speed.
 * 
 * Revision History
 *      Steven Bulgin, 2014.11.01: Created
 */

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace SBAssignment4
{

    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Ball : Microsoft.Xna.Framework.DrawableGameComponent
    {

        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Vector2 position;
        private Vector2 startPos;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        private Vector2 speed;

        public Vector2 Speed
        {
            get { return speed; }
            set { speed = value; }
        }
        private SoundEffect hit;
        private SoundEffect miss;
        private Vector2 stage;

        private int bottom;
        

        private bool reset = true;

        public bool Reset
        {
            get { return reset; }
            set { reset = value; }
        }

        private bool enterPressed;

        public bool EnterPressed
        {
            get { return enterPressed; }
            set { enterPressed = value; }
        }

        public int leftScore = 0;

        public int LeftScore
        {
            get { return leftScore; }
            set { leftScore = value; }
        }
        public int rightScore = 0;

        public int RightScore
        {
            get { return rightScore; }
            set { rightScore = value; }
        }
        
        //My Function

        /// <summary>
        /// Randomizes the ball
        /// </summary>
        public void randomizer()
        {
            Random randSpeed = new Random();
            int negX = randSpeed.Next(0, 2);
            int negY = randSpeed.Next(0, 2);
            int X = randSpeed.Next(3, 10);
            int Y = randSpeed.Next(3, 10);
            
            if (negX == 0)
            {
                X = -X;
            }
            if (negY == 0)
            {
                Y = -Y;
            }

            speed.X = X;
            speed.Y = Y;
        }



        
        
       

        /// <summary>
        /// constructor for Ball.cs
        /// </summary>
        /// <param name="game"></param>
        /// <param name="spriteBatch"></param>
        /// <param name="tex"></param>
        /// <param name="position"></param>
        /// <param name="hit"></param>
        /// <param name="miss"></param>
        /// <param name="stage"></param>
        public Ball(Game game, SpriteBatch spriteBatch,
            Texture2D tex, Vector2 position, SoundEffect hit, SoundEffect miss, Vector2 stage, int bottom)
            : base(game)
        {
            // TODO: Construct any child components here

            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.position = position;
            this.hit = hit;
            this.miss = miss;
            this.stage = stage;
            startPos = position;
            this.bottom = bottom;

            
           

        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            
            KeyboardState ks = Keyboard.GetState();




                if (enterPressed)
                {
                    randomizer();
                    enterPressed = false;

                    

                    


                    reset = false;

                }
                position += speed;


                if (position.Y < 0)
                {
                    speed.Y = Math.Abs(speed.Y);
                    hit.Play();
                }
                if (position.Y > stage.Y - bottom)
                {
                    speed.Y = -speed.Y;
                    hit.Play();
                }

                if (position.X <= 0)
                {
                    rightScore++;
                }
                if (position.X >= stage.X)
                {
                    leftScore++;
                }
                

                if ((position.X >= stage.X) || (position.X <= 0))
                {

                    position = startPos;
                    speed.X = 0;
                    speed.Y = 0;
                    miss.Play();
                    reset = true;
                    enterPressed = false;
                    
                    
                } 

           

                
            
            


            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, position, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);

        }

        public Rectangle getBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y,
                tex.Width, tex.Height);
        }
    }
}
