using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Module_8
{
    public class Game1 : Game
    {
        private Vector2 position = new Vector2(80, 80);

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D stage1, stage2;
        private int Speed = 5, Gravity = 1, SpeedY = 15;
        private bool Jump = false;

        Texture2D pText, tiles, eText, e2Text, e3Text, e4Text, hpText,
            e5Text, h1Text, h2Text, hbText, MainMtext, pausedText, gotext, winText,
            tpText, tp2Text;

        Rectangle pSource, pDisplay, eSource, eDisplay, e2Source, hpDis,
            e2Display, e3Display, e3Source, e4Display, e4Source, e5Display,
            e5Source, h1Display, h2Display, hbDisplay, MainMDis, pausedDis, goDis,
            winDis, tpDis, tp2Dis;

        Color pColor,
            eColor,
            e2Color,
            e3Color,
            e4Color,
            e5Color;

        int delay, changeAnim, red = 0, green = 0, blue = 0;

        cButton btnPlay, btnQuit, btnContinue, btnExit1, btnExit2, btnExit3, btnExit4, btnL1, btnL2;

        bool paused = false;
        bool gameover1 = false;
        bool youwin1 = false;

        enum GameState
        {
            MainMenu, Choose,
            Level1, Level2, Playing,
        }
        GameState MainGameState = GameState.MainMenu;
        GameState ChooseGameState = GameState.Choose;
        int screenWidth = 860, screenHeight = 600;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 860;
            _graphics.PreferredBackBufferHeight = 600;
            _graphics.ApplyChanges();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }


        protected override void Initialize()
        {
            //player
            pText = Content.Load<Texture2D>("RightRun");
            pSource = new Rectangle(0, 0, pText.Width / 6, pText.Height);
            pDisplay = new Rectangle(10, 460, 80, 80);
            pColor = Color.White;

            //teleporter
            tpText = Content.Load<Texture2D>("teleporter");
            tpDis = new Rectangle(350, 200, 80, 80);

            //enemy1
            eText = Content.Load<Texture2D>("rimuru");
            eSource = new Rectangle(0, 0, eText.Width, eText.Height);
            eDisplay = new Rectangle(350, 465, 80, 80);
            eColor = Color.White;

            //enemy2
            e2Text = Content.Load<Texture2D>("rimuru");
            e2Source = new Rectangle(0, 0, eText.Width, eText.Height);
            e2Display = new Rectangle(650, 465, 80, 80);
            e2Color = Color.White;

            //enemy3
            e3Text = Content.Load<Texture2D>("rimuru");
            e3Source = new Rectangle(0, 0, eText.Width, eText.Height);
            e3Display = new Rectangle(350, 465, 80, 80);
            e3Color = Color.White;

            //enemy4
            e4Text = Content.Load<Texture2D>("rimuru");
            e4Source = new Rectangle(0, 0, eText.Width, eText.Height);
            e4Display = new Rectangle(650, 465, 80, 80);
            e4Color = Color.White;

            //enemy5
            e5Text = Content.Load<Texture2D>("rimuru");
            e5Source = new Rectangle(0, 0, eText.Width, eText.Height);
            e5Display = new Rectangle(50, 465, 80, 80);
            e5Color = Color.White;

            //player healthbar1
            h1Text = Content.Load<Texture2D>("healthbar");
            h1Display = new Rectangle(75, 30, 200, 50);
            hbText = Content.Load<Texture2D>("healthborder");
            hbDisplay = new Rectangle(30, 30, 250, 50);

            //player healthbar2
            h2Text = Content.Load<Texture2D>("healthbar");
            h2Display = new Rectangle(75, 30, 200, 50);
            hbText = Content.Load<Texture2D>("healthborder");
            hbDisplay = new Rectangle(30, 30, 250, 50);

            //potion
            hpText = Content.Load<Texture2D>("potion");
            hpDis = new Rectangle(150, 280, 30, 30);

            //==Main Menu==
            _graphics.PreferredBackBufferWidth = screenWidth;
            _graphics.PreferredBackBufferHeight = screenHeight;
            MainMtext = Content.Load<Texture2D>("menutext");
            MainMDis = new Rectangle(100, 50, MainMtext.Width, MainMtext.Height);

            //play
            btnPlay = new cButton(Content.Load<Texture2D>("Start"),
                _graphics.GraphicsDevice);
            btnPlay.setPosition(new Vector2(350, 350));

            //Level1
            btnL1 = new cButton(Content.Load<Texture2D>("Level1"),
            _graphics.GraphicsDevice);
            btnL1.setPosition(new Vector2(350, 300));

            //Level2
            btnL2 = new cButton(Content.Load<Texture2D>("Level2"),
            _graphics.GraphicsDevice);
            btnL2.setPosition(new Vector2(350, 400));

            //paused
            pausedText = Content.Load<Texture2D>("Menu");
            pausedDis = new Rectangle(320, 50, 200, 100);
            btnContinue = new cButton(Content.Load<Texture2D>("Continue"), _graphics.GraphicsDevice);
            btnContinue.setPosition(new Vector2(350, 350));

            //MainMenu Exit
            btnExit1 = new cButton(Content.Load<Texture2D>("Quit"), _graphics.GraphicsDevice);
            btnExit1.setPosition(new Vector2(350, 450));

            //Pause Exit
            btnExit2 = new cButton(Content.Load<Texture2D>("Quit"), _graphics.GraphicsDevice);
            btnExit2.setPosition(new Vector2(350, 450));

            //Game Over Exit
            btnExit3 = new cButton(Content.Load<Texture2D>("Quit"), _graphics.GraphicsDevice);
            btnExit3.setPosition(new Vector2(350, 450));
            base.Initialize();

            //You Win Exit
            btnExit4 = new cButton(Content.Load<Texture2D>("Quit"), _graphics.GraphicsDevice);
            btnExit4.setPosition(new Vector2(350, 450));

            base.Initialize();
            //Game Over
            gotext = Content.Load<Texture2D>("gameover");
            goDis = new Rectangle(100, 50, 150, 150);

            //You Win
            winText = Content.Load<Texture2D>("youwin");
            winDis = new Rectangle(100, 50, 150, 150);
        }

        protected override void LoadContent()
        {
            stage1 = Content.Load<Texture2D>("background");
            stage2 = Content.Load<Texture2D>("stage2");
            tiles = Content.Load<Texture2D>("Floor");
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            //Random Color
            Random r = new Random();
            {
                red = r.Next(255);
                green = r.Next(255);
                blue = r.Next(255);
            }

            //Main Menu
            MouseState mouse = Mouse.GetState();
            switch (MainGameState)
            {
                case GameState.MainMenu:
                    //Level1
                    if (btnPlay.isClicked == true)
                        MainGameState = GameState.Level1;
                    btnPlay.Update(mouse);
                    //QQuit
                    if (btnExit1.isClicked == true)
                        Exit();
                    btnExit1.Update(mouse);
                    break;

                case GameState.Level1:
                    //paused
                    if (!paused)
                    {
                        //full health increase
                        if (pDisplay.Intersects(hpDis))
                        {
                            h1Display.Width = 200;
                        }

                        //healthbar1 decrease
                        if (pDisplay.Intersects(eDisplay) || pDisplay.Intersects(e2Display))
                        {
                            h1Display.Width -= 1;
                            //knockback
                            pText = Content.Load<Texture2D>("knockback");
                            PlayAnimation2(0);
                            if (h1Display.Width == 0)
                            {
                                gameover1 = true;
                            }
                        }

                        //attack in enemy1
                        if (pDisplay.Intersects(eDisplay) && Mouse.GetState().LeftButton == ButtonState.Pressed)
                        {
                            pText = Content.Load<Texture2D>("RightSlash");
                            eColor = new Color(red, green, blue);
                            eDisplay.Y -= 5;
                            PlayAnimation2(0);
                        }

                        //attack in enemy2
                        if (pDisplay.Intersects(e2Display) && Mouse.GetState().LeftButton == ButtonState.Pressed)
                        {
                            pText = Content.Load<Texture2D>("RightSlash");
                            e2Color = new Color(red, green, blue);
                            e2Display.Y -= 5;
                            PlayAnimation2(0);
                        }

                        //teleport
                        if (eDisplay.Intersects(tpDis))
                        {
                            eDisplay.Y -= 800;
                            tpDis = new Rectangle(650, 200, 80, 80);
                        }

                        //win
                        if (e2Display.Intersects(tpDis))
                        {
                            MainGameState = GameState.Level2;
                        }

                        //solo attack
                        if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                        {
                            pText = Content.Load<Texture2D>("RightSlash");
                            PlayAnimation2(0);
                        }

                        //pause
                        if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                        {
                            paused = true;

                            btnContinue.isClicked = false;
                        }

                        //MOVEMENTS
                        //right
                        if (Keyboard.GetState().IsKeyDown(Keys.D) && pDisplay.X <= 810)
                        {
                            pText = Content.Load<Texture2D>("RightRun");
                            pSource.Y = 1;
                            pDisplay.X += 4;
                            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
                            {
                                pDisplay.X += 5;
                            }
                            PlayAnimation(0);
                        }

                        //left
                        if (Keyboard.GetState().IsKeyDown(Keys.A) && pDisplay.X >= -30)
                        {
                            pText = Content.Load<Texture2D>("LeftRun");
                            pSource.Y = 1;
                            pDisplay.X -= 4;
                            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
                            {
                                pDisplay.X -= 5;
                            }
                            PlayAnimation(0);
                        }

                        //jump
                        if (Keyboard.GetState().IsKeyDown(Keys.Space))
                        {
                            if (!Jump)
                            {
                                Jump = true;
                            }
                        }
                        if (Jump)
                        {
                            pDisplay.Y -= SpeedY;
                            SpeedY -= (Gravity / 5);
                            Gravity += 1;

                            if (Gravity >= 25)
                            {
                                SpeedY = 15;
                                Gravity = 1;
                                pDisplay.Y = 460;
                                Jump = false;
                            }
                        }
                    }
                    else if (paused)
                    {
                        if (btnContinue.isClicked)
                            paused = false;
                        if (btnExit2.isClicked)
                            MainGameState = GameState.MainMenu;

                        btnContinue.Update(mouse);
                        btnExit2.Update(mouse);
                    }

                    break;
                case GameState.Level2:

                    if (!paused)
                    {
                        //full health increase
                        if (pDisplay.Intersects(hpDis))
                        {
                            h2Display.Width = 200;
                        }

                        //healthbar2 decrease
                        if (pDisplay.Intersects(e3Display) || pDisplay.Intersects(e4Display) || pDisplay.Intersects(e5Display))
                        {
                            h2Display.Width -= 1;
                            //knockback
                            pText = Content.Load<Texture2D>("knockback");
                            PlayAnimation2(0);
                            if (h2Display.Width == 0)
                            {
                                gameover1 = true;
                            }
                        }

                        //attack in enemy3
                        if (pDisplay.Intersects(e3Display) && Mouse.GetState().LeftButton == ButtonState.Pressed)
                        {
                            pText = Content.Load<Texture2D>("RightSlash");
                            e3Color = new Color(red, green, blue);
                            e3Display.Y -= 5;
                            PlayAnimation2(0);
                        }

                        //attack in enemy4
                        if (pDisplay.Intersects(e4Display) && Mouse.GetState().LeftButton == ButtonState.Pressed)
                        {
                            pText = Content.Load<Texture2D>("RightSlash");
                            e4Color = new Color(red, green, blue);
                            e4Display.Y -= 5;
                            PlayAnimation2(0);
                        }

                        //attack in enemy5
                        if (pDisplay.Intersects(e5Display) && Mouse.GetState().LeftButton == ButtonState.Pressed)
                        {
                            pText = Content.Load<Texture2D>("RightSlash");
                            e5Color = new Color(red, green, blue);
                            e5Display.Y -= 5;
                            PlayAnimation2(0);
                        }

                        if (e4Display.Intersects(tpDis))
                        {
                            e4Display.Y -= 800;
                            tpDis = new Rectangle(350, 200, 80, 80);
                        }

                        if (e3Display.Intersects(tpDis))
                        {
                            e3Display.Y -= 800;
                            tpDis = new Rectangle(50, 200, 80, 80);
                        }

                        if (e5Display.Intersects(tpDis))
                        {
                            youwin1 = true;
                        }

                        //solo attack
                        if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                        {
                            pText = Content.Load<Texture2D>("RightSlash");
                            PlayAnimation2(0);
                        }

                        //pause
                        if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                        {
                            paused = true;

                            btnContinue.isClicked = false;
                        }

                        //MOVEMENTS
                        //right
                        if (Keyboard.GetState().IsKeyDown(Keys.D) && pDisplay.X <= 810)
                        {
                            pText = Content.Load<Texture2D>("RightRun");
                            pSource.Y = 1;
                            pDisplay.X += 4;
                            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
                            {
                                pDisplay.X += 5;
                            }
                            PlayAnimation(0);
                        }

                        //left
                        if (Keyboard.GetState().IsKeyDown(Keys.A) && pDisplay.X >= -30)
                        {
                            pText = Content.Load<Texture2D>("LeftRun");
                            pSource.Y = 1;
                            pDisplay.X -= 4;
                            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
                            {
                                pDisplay.X -= 5;
                            }
                            PlayAnimation(0);
                        }

                        //jump
                        if (Keyboard.GetState().IsKeyDown(Keys.Space))
                        {
                            if (!Jump)
                            {
                                Jump = true;
                            }
                        }
                        if (Jump)
                        {
                            pDisplay.Y -= SpeedY;
                            SpeedY -= (Gravity / 5);
                            Gravity += 1;

                            if (Gravity >= 25)
                            {
                                SpeedY = 15;
                                Gravity = 1;
                                pDisplay.Y = 460;
                                Jump = false;
                            }
                        }
                    }

                    else if (paused)
                    {
                        if (btnContinue.isClicked)
                            paused = false;
                        if (btnExit2.isClicked)
                            MainGameState = GameState.MainMenu;

                        btnContinue.Update(mouse);
                        btnExit2.Update(mouse);
                    }
                    break;
            }

            if (gameover1)
            {
                paused = false;
                if (btnExit3.isClicked)
                    Exit();

                btnExit3.Update(mouse);
            }
            else if (youwin1)
            {
                paused = false;
                if (btnExit4.isClicked)
                    Exit();

                btnExit4.Update(mouse);
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            switch (MainGameState)



            {
                case GameState.MainMenu:
                    _spriteBatch.Draw(Content.Load<Texture2D>("MainBG"),
                        new Rectangle(0, 0, screenWidth, screenHeight),
                        Color.White);
                    _spriteBatch.Draw(MainMtext, MainMDis, Color.White);
                    btnPlay.Draw(_spriteBatch);
                    btnExit1.Draw(_spriteBatch);
                    break;

                case GameState.Level1:
                    _spriteBatch.Draw(stage1, new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
                    //stage name
                    _spriteBatch.DrawString(buttonFont, "Stage 1", new Vector2(10, 10), Color.Sienna);
                    _spriteBatch.Draw(pText, pDisplay, pSource, pColor);
                    //enemy1
                    _spriteBatch.Draw(eText, eDisplay, eSource, eColor);
                    //enemy2
                    _spriteBatch.Draw(e2Text, e2Display, e2Source, e2Color);
                    //player health
                    _spriteBatch.Draw(h1Text, h1Display, Color.White);
                    _spriteBatch.Draw(hbText, hbDisplay, Color.White);
                    //potion
                    _spriteBatch.Draw(hpText, hpDis, Color.White);
                    //teleporter
                    _spriteBatch.Draw(tpText, tpDis, Color.White);
                    if (paused)
                    {
                        _spriteBatch.Draw(pausedText, pausedDis, Color.White);
                        btnContinue.Draw(_spriteBatch);
                        btnExit2.Draw(_spriteBatch);
                    }
                    if (gameover1)
                    {
                        _spriteBatch.Draw(stage1, new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
                        _spriteBatch.Draw(gotext, new Rectangle(330, 50, 150, 150), Color.White);
                        btnExit3.Draw(_spriteBatch);
                    }
                    if (youwin1)
                    {
                        _spriteBatch.Draw(stage1, new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
                        _spriteBatch.Draw(winText, new Rectangle(330, 50, 150, 150), Color.White);
                        btnExit4.Draw(_spriteBatch);
                    }
                    break;

                case GameState.Level2:
                    _spriteBatch.Draw(stage2, new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
                    //stage name
                    _spriteBatch.DrawString(buttonFont, "Stage 1", new Vector2(10, 10), Color.Sienna);
                    _spriteBatch.Draw(pText, pDisplay, pSource, pColor);
                    //enemy3
                    _spriteBatch.Draw(e3Text, e3Display, e3Source, e3Color);
                    //enemy4
                    _spriteBatch.Draw(e4Text, e4Display, e4Source, e4Color);
                    //enemy5
                    _spriteBatch.Draw(e5Text, e5Display, e5Source, e5Color);
                    //player health
                    _spriteBatch.Draw(h2Text, h2Display, Color.White);
                    _spriteBatch.Draw(hbText, hbDisplay, Color.White);
                    //potion
                    _spriteBatch.Draw(hpText, hpDis, Color.White);
                    //teleporter
                    _spriteBatch.Draw(tpText, tpDis, Color.White);
                    if (paused)
                    {
                        _spriteBatch.Draw(pausedText, pausedDis, Color.White);
                        btnContinue.Draw(_spriteBatch);
                        btnExit2.Draw(_spriteBatch);
                    }
                    if (gameover1)
                    {
                        _spriteBatch.Draw(stage2, new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
                        _spriteBatch.Draw(gotext, new Rectangle(330, 50, 150, 150), Color.White);
                        btnExit3.Draw(_spriteBatch);
                    }
                    if (youwin1)
                    {
                        _spriteBatch.Draw(stage2, new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
                        _spriteBatch.Draw(winText, new Rectangle(330, 50, 150, 150), Color.White);
                        btnExit4.Draw(_spriteBatch);
                    }
                    break;

            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        public void PlayAnimation(int reset)
        {

            if (delay > 15)


            {
                if (reset != changeAnim)
                {
                    pSource.X = 0;
                }
                else
                {
                    if (pSource.X < pText.Width - ((pText.Width / 6) * 2))
                    {
                        pSource.X += pText.Width / 6;
                    }
                    else
                    {
                        pSource.X = 4;
                    }
                }
                delay = 10;
            }
            delay++;
            changeAnim = reset;
        }

        public void PlayAnimation2(int reset)
        {
            if (delay > 15)
            {
                if (reset != changeAnim)
                {
                    pSource.X = 0;
                }
                else
                {
                    if (pSource.X < pText.Width - ((pText.Width / 1) * 50))
                    {
                        pSource.X += pText.Width / 1;
                    }
                    else
                    {
                        pSource.X = 4;
                    }
                }
                delay = 15;
            }
            delay++;
            changeAnim = reset;
        }
    }
}
