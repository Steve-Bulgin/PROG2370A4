/* CollisionManager.cs
 * Purpose: Detects when a ball and bat make contact 
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
    public class CollisionManager : Microsoft.Xna.Framework.GameComponent
    {
        private Bat batLeft, batRight;
        private Ball ball;
        private SoundEffect hit;
        private Vector2 stage;

        public CollisionManager(Game game, Bat batLeft, Bat batRight, Ball ball, SoundEffect hit, Vector2 stage)
            : base(game)
        {
            // TODO: Construct any child components here
            this.batLeft = batLeft;
            this.batRight = batRight;
            this.ball = ball;
            this.hit = hit;
            this.stage = stage;

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
            Rectangle recBall = ball.getBounds();
            Rectangle recBatLeft = batLeft.getBounds();
            Rectangle recBatRight = batRight.getBounds(); 

            
                if (recBall.Intersects(recBatRight))
                {
                    ball.Speed = new Vector2(-Math.Abs(ball.Speed.X), ball.Speed.Y);
                    hit.Play();
                }             
            
                if (recBall.Intersects(recBatLeft))
                {
                    ball.Speed = new Vector2(Math.Abs(ball.Speed.X), ball.Speed.Y);
                    hit.Play();
                }
            

            base.Update(gameTime);
        }
    }
}
