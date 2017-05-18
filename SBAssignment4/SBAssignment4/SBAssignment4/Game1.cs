/* Game1.cs
 * Purpose: To build a Pong game with multiple game components, paddles, ball, sounds, winning msg, etc 
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
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        const int WINNING_SCORE = 2;


        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Vector2 stage;
        ScoreBoard scoreBoard;
        Bat batLeft, batRight;
        Ball ball;
        CollisionManager cm;
        ScoreString leftScore, rightScore, win;
        SpriteFont font;
        Vector2 leftScorePos, rightScorePos, winMsgPos;
        SoundEffect applause;
        string rightPlayer = "Sabbir";
        string leftPlayer = "Steve";
        string leftScoreMsg, rightScoreMsg, winMsg;
        private bool enterFlag;
        private bool spaceBarLock;

        //My Function

        /// <summary>
        /// Resets variable for game
        /// </summary>
        private void gameReset()
        {
            enterFlag = false;
            spaceBarLock = false;
            win.Message = "";
            ball.rightScore = 0;
            ball.leftScore = 0;

        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            SoundEffect hit = Content.Load<SoundEffect>("sounds/click");
            SoundEffect miss = Content.Load<SoundEffect>("sounds/ding");
            applause = Content.Load<SoundEffect>("sounds/applause1");

            Vector2 batspeed = new Vector2(0, 5);
            stage = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);

            //ScoreBoard
            Texture2D scoreBoardTex = Content.Load<Texture2D>("images/Scorebar");
            Vector2 scoreBoardPos = new Vector2(0, stage.Y - scoreBoardTex.Height);
            scoreBoard = new ScoreBoard(this, spriteBatch, scoreBoardTex, scoreBoardPos);
            this.Components.Add(scoreBoard);

            

            //Ball
            Texture2D ballTex = Content.Load<Texture2D>("images/Ball");
            Vector2 ballPos = new Vector2(stage.X / 2 - ballTex.Width / 2, stage.Y / 2 - ballTex.Height / 2 - scoreBoardTex.Height/2);
            Vector2 ballSpeed = new Vector2(1, 1);
            ball = new Ball(this, spriteBatch, ballTex, ballPos, hit, miss, stage, scoreBoardTex.Height+ballTex.Height);
            this.Components.Add(ball);
            
            //Left bat
            Texture2D batLeftTex = Content.Load<Texture2D>("images/BatLeft");
            Vector2 batLeftPos = new Vector2(0, stage.Y / 2 - batLeftTex.Height / 2 - scoreBoardTex.Height / 2);
            batLeft = new Bat(this, spriteBatch, batLeftTex, batLeftPos, batspeed, stage);
            this.Components.Add(batLeft);

            //Right bat
            Texture2D batRightTex = Content.Load<Texture2D>("images/BatRight");
            Vector2 batRightPos = new Vector2(stage.X - batRightTex.Width , stage.Y / 2 - batLeftTex.Height / 2 - scoreBoardTex.Height/2);
            batRight = new Bat(this, spriteBatch, batRightTex, batRightPos, batspeed, stage);
            this.Components.Add(batRight);

            //Collision Manager
            cm = new CollisionManager(this, batLeft, batRight, ball, hit, stage);
            this.Components.Add(cm);

            
            font = Content.Load<SpriteFont>("fonts/SpriteFont1");

            //Left Score
            leftScorePos = new Vector2(0, stage.Y - scoreBoardTex.Height/2 - font.LineSpacing /2);
            leftScoreMsg = leftPlayer + ": " + ball.LeftScore.ToString();
            leftScore = new ScoreString(this, spriteBatch, leftScoreMsg, leftScorePos, Color.Black, font);
            this.Components.Add(leftScore);

            //Right Score
            rightScoreMsg = rightPlayer + ": " + ball.RightScore.ToString();
            rightScorePos = new Vector2(stage.X - font.MeasureString(rightScoreMsg).X, stage.Y - scoreBoardTex.Height / 2 - font.LineSpacing / 2);
            rightScore = new ScoreString(this, spriteBatch, rightScoreMsg, rightScorePos, Color.Black, font);
            this.Components.Add(rightScore);

            //Winning Msg
            winMsg = "";
            winMsgPos = new Vector2(stage.X / 2 - font.MeasureString("Press spacebar to restart").X / 2, stage.Y - scoreBoardTex.Height / 2 - font.LineSpacing);
            win = new ScoreString(this, spriteBatch, winMsg, winMsgPos, Color.Blue, font);
            this.Components.Add(win);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            KeyboardState ks = Keyboard.GetState();

            

            batLeft.BatSet = ball.Reset;
            batRight.BatSet = ball.Reset;
            batLeft.BatMove = ball.EnterPressed;
            batRight.BatMove = ball.EnterPressed;

            
            if (enterFlag==false)
            {
                
                if (ks.IsKeyDown(Keys.Enter))
                {
                    enterFlag = true;
                    ball.EnterPressed = enterFlag;
                } 
            }

            if (ks.IsKeyDown(Keys.A))
            {
                batLeft.BatUp();
            }
           
            if (ks.IsKeyDown(Keys.Z))
            {
                batLeft.BatDown();
            }
           
            if (ks.IsKeyDown(Keys.Up))
            {
                batRight.BatUp();
            }            

            if (ks.IsKeyDown(Keys.Down))
            {
                batRight.BatDown();
            }

            if (ball.Reset == true)
            {
                enterFlag = false;
            }

            if (ks.IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            string rightScoreMsg = rightPlayer + ": " + ball.RightScore.ToString();
            rightScore.Message = rightScoreMsg;

            string leftScoreMsg = leftScoreMsg = leftPlayer + ": " + ball.leftScore.ToString();
            leftScore.Message = leftScoreMsg;

            if (ball.rightScore == WINNING_SCORE)
            {
                if (win.Message == "")
                {
                    applause.Play();
                }

                winMsg = rightPlayer + " wins!\nPress spacebar to restart";
                win.Message = winMsg;
                
                spaceBarLock = true;
                
            }
            if (ball.leftScore == WINNING_SCORE)
            {
                
                if(win.Message=="")
                {
                    applause.Play();
                }

                winMsg = leftPlayer + " wins!\nPress spacebar to restart";
                win.Message = winMsg;
                enterFlag = true;
                spaceBarLock = true;                
            }

            if (win.Message != "")
            {
                ball.EnterPressed = false;
            }

            if (spaceBarLock)
            {
                if (ks.IsKeyDown(Keys.Space))
                {
                    gameReset();
                }
            }
            
            

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            
            base.Draw(gameTime);
        }
    }
}
