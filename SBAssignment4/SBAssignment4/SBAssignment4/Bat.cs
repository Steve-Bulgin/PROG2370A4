/* Bat.cs
 * Purpose: Bat component of pong game 
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
    public class Bat : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Vector2 position;
        private Vector2 startPos;
        private Vector2 speed;
        private Vector2 stage;
        private bool batMove;

        public bool BatMove
        {
            get { return batMove; }
            set { batMove = value; }
        }

        private bool batSet;

        public bool BatSet
        {
            get { return batSet; }
            set { batSet = value; }
        }

        //My Functions


        /// <summary>
        /// Moves Bat up. Called in game1 when appropriate key pressed
        /// </summary>
        public void BatUp()
        {
            position.Y -= speed.Y;

            if (position.Y < 0)
            {
                position.Y = 0;
            }
        }


        /// <summary>
        /// Moves Bat down. Called in game1 when appropriate key pressed
        /// </summary>
        public void BatDown()
        {
            position.Y += speed.Y;

            if (position.Y > stage.Y - tex.Height - 61)
            {
                position.Y = stage.Y - tex.Height - 61;
            }
        }

        /// <summary>
        /// constructor for Bat.cs
        /// </summary>
        /// <param name="game"></param>
        /// <param name="spriteBatch"></param>
        /// <param name="tex"></param>
        /// <param name="position"></param>
        /// <param name="speed"></param>
        /// <param name="stage"></param>
        public Bat(Game game, SpriteBatch spriteBatch, 
            Texture2D tex,
            Vector2 position,
            Vector2 speed,
            Vector2 stage)
            : base(game)
        {
            // TODO: Construct any child components here

            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.position = position;
            this.speed = speed;
            this.stage = stage;
            startPos = position;

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

            

            if (batSet)
            {
                position = startPos;
                batSet = false;
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
