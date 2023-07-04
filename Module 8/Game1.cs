using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Module_8
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _texture;
        private Texture2D[] _textures;
        private int currentFrame;
        private int totalFrames;
        private int frameWidth;
        private int frameHeight;
        private Vector2 _position;
        private bool isWalking;
        private bool isFacingRight;
        private bool isJumping;
        private int frameWidthAttack;
        private int frameHeightAttack;
        private float jumpVelocity;
        private const float Gravity = 0.5f;
        private const int AnimationDelay = 200;
        private int animationTimer;
        private bool isAttacking;
        private SpriteFont _font;
        private const int MenuSelectionDelay = 200; // delay should implemented fixed 
        private int menuTimer;
        private Texture2D[] _textures2;
        private int totalFrames2;
        private int frameWidth2;
        private int frameHeight2;
        private const int AnimationDelay2 = 200; // animdelay for char2
        private Texture2D _textureWalkingRight;
        private Texture2D _textureWalkingLeft;
        private Texture2D[] _textures3;
        private int totalFrames3;
        private int frameWidth3;
        private int frameHeight3;
        private const int AnimationDelay3 = 200;
        SpriteFont buttonFont;
        Button[] button;
        SpriteFont menuFont;
        Texture2D bgMenu;
        Texture2D bg2;


        private enum GameState
        {
            MainMenu,
            CharacterSelection,
            Playing,
            About,
            Exiting
        }

        private GameState _gameState;
        private int currentOption;

        private string[] characterNames = { "Brute", "Char2", "Char3", "Char4" };
        private int characterSelection;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            // game window size
            _graphics.PreferredBackBufferWidth = 1000;
            _graphics.PreferredBackBufferHeight = 600;
        }

        protected override void Initialize()
        {
            _gameState = GameState.MainMenu;
            currentOption = 0;
            characterSelection = 0;

            Texture2D butTex = Content.Load<Texture2D>("menuButton");
            Color butColor = Color.White;

            button = new Button[3];
            Rectangle butRect1 = new Rectangle(300, 200, 350, 75);
            button[0] = new Button(butTex, butRect1, butColor);

            Rectangle butRect2 = new Rectangle(300, 293, 350, 75);
            button[1] = new Button(butTex, butRect2, butColor);

            Rectangle butRect3 = new Rectangle(300, 385, 350, 75);
            button[2] = new Button(butTex, butRect3, butColor);


            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            buttonFont = Content.Load<SpriteFont>("ButtonFont");
            bgMenu = Content.Load<Texture2D>("bgMenu");
            bg2 = Content.Load<Texture2D>("bg2");
            menuFont = Content.Load<SpriteFont>("GameFont");
            _textures = new Texture2D[4];
            _textures[0] = Content.Load<Texture2D>("char1idle");
            _textures[1] = Content.Load<Texture2D>("char1left");
            _textures[2] = Content.Load<Texture2D>("char1right");
            _textures[3] = Content.Load<Texture2D>("char1attack");
            _font = Content.Load<SpriteFont>("SpriteFont"); // Replace the font anytime

            //asdddddddddd

            _textures2 = new Texture2D[2]; // For char 2
            _textureWalkingRight = Content.Load<Texture2D>("char2right");
            _textureWalkingLeft = Content.Load<Texture2D>("char2left");
            _textures2[0] = Content.Load<Texture2D>("char2idle");
            _textures2[1] = Content.Load<Texture2D>("char2attack");
            totalFrames2 = 3; // more to be add as long new frames were being added
            frameWidth2 = 110; // to be changed
            frameHeight2 = 100; // frame height fix

            _textures3 = new Texture2D[2]; // For char3
            _textures3[0] = Content.Load<Texture2D>("char3idle");
            // Add the second frame if available
            // _textures3[1] = Content.Load<Texture2D>("char3frame2");
            totalFrames3 = 2; // Number of frames in the idle animation
            frameWidth3 = 113; // Width of each frame
            frameHeight3 = 100; // Height of each frame


            _position = new Vector2(100, 360);
            currentFrame = 0;
            totalFrames = 8;
            frameWidth = _textures[0].Width / totalFrames;
            frameHeight = _textures[0].Height;
            isWalking = false;
            isFacingRight = true;
            isJumping = false;
            jumpVelocity = 0f;
            animationTimer = 0;
            isAttacking = false;
        }

        protected override void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();
            var movementSpeed = (keyboardState.IsKeyDown(Keys.LeftShift) || keyboardState.IsKeyDown(Keys.RightShift)) ? 5 : 3;

            if (_gameState == GameState.MainMenu)
            {
                UpdateMainMenu(keyboardState, gameTime);
            }
            else if (_gameState == GameState.CharacterSelection)
            {
                UpdateCharacterSelection(keyboardState, gameTime);
            }
            else if (_gameState == GameState.Playing)
            {
                UpdateGame(keyboardState, movementSpeed, gameTime);
            }
            else if (_gameState == GameState.About)
            {
                UpdateAbout(keyboardState);
            }

            base.Update(gameTime);
        }

        private void UpdateMainMenu(KeyboardState keyboardState, GameTime gameTime) // MAIN MENU STATE (W,A or Arrow Keys, Enter, ESC)
        {
            menuTimer += gameTime.ElapsedGameTime.Milliseconds;

            if (menuTimer >= MenuSelectionDelay)
            {
                if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W))
                {
                    currentOption--;
                    if (currentOption < 0)
                        currentOption = 2;

                    menuTimer = 0;
                }
                else if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S))
                {
                    currentOption++;
                    if (currentOption > 2)
                        currentOption = 0;

                    menuTimer = 0;
                }
                else if (keyboardState.IsKeyDown(Keys.Enter))
                {
                    if (currentOption == 0)
                    {
                        _gameState = GameState.CharacterSelection;
                        characterSelection = 0;
                    }
                    else if (currentOption == 1)
                        _gameState = GameState.About;
                    else if (currentOption == 2)
                        Exit();

                    menuTimer = 0;
                }
            }
        }

        private void UpdateCharacterSelection(KeyboardState keyboardState, GameTime gameTime)
        {
            menuTimer += gameTime.ElapsedGameTime.Milliseconds;

            if (menuTimer >= MenuSelectionDelay)
            {
                if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
                {
                    characterSelection--;
                    if (characterSelection < 0)
                        characterSelection = characterNames.Length - 1;

                    menuTimer = 0;
                }
                else if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
                {
                    characterSelection++;
                    if (characterSelection >= characterNames.Length)
                        characterSelection = 0;

                    menuTimer = 0;
                }
                else if (keyboardState.IsKeyDown(Keys.Enter))
                {

                    LoadCharacter(characterNames[characterSelection]);
                    _gameState = GameState.Playing;

                    menuTimer = 0;
                }
            }


        }

        private void LoadCharacter(string characterName)
        {
            if (characterName == "Char2")
            {
                _textures = _textures2; // Textures for char2
                totalFrames = totalFrames2; // Total frames for char2
                frameWidth = frameWidth2; // Frame width for char2
                frameHeight = frameHeight2; // Frame height for char2
            }
            else if (characterName == "Char3")
            {
                _textures = _textures3; // Textures for char3
                totalFrames = totalFrames3; // Total frames for char3
                frameWidth = frameWidth3; // Frame width for char3
                frameHeight = frameHeight3; // Frame height for char3
            }
            else
            {
                // Handle other character loadings here
            }
        }


        private void UpdateGame(KeyboardState keyboardState, int movementSpeed, GameTime gameTime)
        {
            if (keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.Left))
            {
                UpdateMovement(-movementSpeed, false, gameTime);
            }
            else if (keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Right))
            {
                UpdateMovement(movementSpeed, true, gameTime);
            }
            else
            {
                isWalking = false;
                UpdateAnimation(gameTime);
            }

            if ((keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.Up)) && !isJumping)
            {
                jumpVelocity = -10f;
                isJumping = true;
            }

            if (isJumping)
            {
                jumpVelocity += Gravity;
                _position.Y += jumpVelocity;

                if (_position.Y >= 360)
                {
                    _position.Y = 360;
                    jumpVelocity = 0f;
                    isJumping = false;
                }
            }

            var mouseState = Mouse.GetState();

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                isAttacking = true;
                currentFrame = 0;
            }
            else
            {
                isAttacking = false;
            }
        }

        private void UpdateAbout(KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.Escape))
                _gameState = GameState.MainMenu;
        }

        private void UpdateMovement(int speed, bool isRight, GameTime gameTime)
        {
            _position.X += speed;
            isFacingRight = isRight;
            isWalking = true;
            UpdateAnimation(gameTime);
        }


        private void UpdateAnimation(GameTime gameTime)
        {
            animationTimer += gameTime.ElapsedGameTime.Milliseconds;

            if (animationTimer >= (characterSelection == 1 ? AnimationDelay2 : characterSelection == 2 ? AnimationDelay3 : AnimationDelay))
            {
                animationTimer = 0;

                if (characterSelection == 1)
                {
                    if (isAttacking)
                    {
                        currentFrame++;
                        if (currentFrame >= totalFrames2)
                        {
                            currentFrame = 0;
                            isAttacking = false;
                        }
                    }
                    else
                    {
                        currentFrame++;
                        if (currentFrame >= totalFrames2 - 1) // -1 to exclude the attacking frame
                            currentFrame = 0;
                    }
                }
                else
                {
                    currentFrame++;
                    if (currentFrame >= (isFacingRight ? totalFrames : totalFrames2))
                        currentFrame = 0;
                }
            }
        }




        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            //_spriteBatch.Draw(bgMenu, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);

            if (_gameState == GameState.Playing)
            {
                DrawGame();
            }
            else if (_gameState == GameState.MainMenu)
            {
                DrawMainMenu();
            }
            else if (_gameState == GameState.About)
            {
                DrawAbout();
            }
            else if (_gameState == GameState.CharacterSelection)
            {
                DrawCharacterSelection();
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawGame()
        {
            //background
            _spriteBatch.Draw(bg2, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);

            if (characterSelection == 1)
            {
                if (isWalking)
                {
                    Texture2D currentTexture;
                    int frameX = currentFrame * frameWidth2;
                    int frameY = 0;

                    if (isFacingRight)
                    {
                        currentTexture = _textureWalkingRight;
                        frameX = currentFrame * frameWidth2;
                    }
                    else
                    {
                        currentTexture = _textureWalkingLeft; // walking left t
                        frameX = (_textures2[0].Width - (currentFrame + 1) * frameWidth2); // Calculate the frameX for walking left
                    }

                    _spriteBatch.Draw(currentTexture, _position, new Rectangle(frameX, frameY, frameWidth2, frameHeight2), Color.White);
                }
                else if (isAttacking)
                {
                    int frameX = currentFrame * frameWidth2;
                    int frameY = 0;
                    _spriteBatch.Draw(_textures2[1], _position, new Rectangle(frameX, frameY, frameWidth2, frameHeight2), Color.White);
                    currentFrame++;
                    if (currentFrame >= totalFrames2)
                    {
                        currentFrame = 0;
                        isAttacking = false;
                    }
                }
                else
                {
                    int frameX = currentFrame * frameWidth2;
                    int frameY = 0;
                    _spriteBatch.Draw(_textures2[0], _position, new Rectangle(frameX, frameY, frameWidth2, frameHeight2), Color.White);
                }
            }
            else
            {
                if (isWalking)
                {
                    Texture2D currentTexture;
                    int frameX = currentFrame * frameWidth;
                    int frameY = 0;

                    if (isFacingRight)
                    {
                        currentTexture = _textures[2];
                        frameX = currentFrame * frameWidth;
                    }
                    else
                    {
                        currentTexture = _textures[1];
                    }

                    _spriteBatch.Draw(currentTexture, _position, new Rectangle(frameX, frameY, frameWidth, frameHeight), Color.White);
                }
                else if (isAttacking)
                {
                    int frameX = currentFrame * frameWidth;
                    int frameY = 0;
                    _spriteBatch.Draw(_textures[3], _position, new Rectangle(frameX, frameY, frameWidth, frameHeight), Color.White);
                    currentFrame++;
                    if (currentFrame >= totalFrames)
                    {
                        currentFrame = 0;
                        isAttacking = false;
                    }
                }
                else
                {
                    int frameX = currentFrame * frameWidth;
                    int frameY = 0;
                    _spriteBatch.Draw(_textures[0], _position, new Rectangle(frameX, frameY, frameWidth, frameHeight), Color.White);
                }
            }
        }





        private void DrawMainMenu()
        {
            var screenWidth = _graphics.PreferredBackBufferWidth;
            var screenHeight = _graphics.PreferredBackBufferHeight;
            var menuTitleSize = _font.MeasureString("Main Menu");
            var menuTitlePosition = new Vector2((screenWidth - menuTitleSize.X) / 2, 50);

            // Calculate the button positions
            var buttonY = menuTitlePosition.Y + menuTitleSize.Y + _font.LineSpacing * 2;
            var buttonStartX = menuTitlePosition.X;

            //background
            _spriteBatch.Draw(bgMenu, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);

            //title
            _spriteBatch.DrawString(menuFont, "Beach,       !", new Vector2(275, 90), Color.MediumAquamarine);
            _spriteBatch.DrawString(menuFont, "Run", new Vector2(535, 90), Color.Coral);

            //buttons
            for (int i = 0; i < button.Length; i++)
            {
                _spriteBatch.Draw(button[i].ButTex, button[i].ButRect, button[i].ButColor);
            }

            _spriteBatch.DrawString(buttonFont, "New Game", new Vector2(400, 218), currentOption == 0 ? Color.Yellow : Color.White);
            _spriteBatch.DrawString(buttonFont, "Load Game", new Vector2(395, 293 + _font.LineSpacing), currentOption == 1 ? Color.Yellow : Color.White);
            _spriteBatch.DrawString(buttonFont, "Exit", new Vector2(450, 368 + _font.LineSpacing * 2), currentOption == 2 ? Color.Yellow : Color.White);
        }

        private void DrawAbout()
        {
            var screenWidth = _graphics.PreferredBackBufferWidth;
            var screenHeight = _graphics.PreferredBackBufferHeight;
            var aboutText = "This is a sample game created using MonoGame.";
            var aboutTextSize = _font.MeasureString(aboutText);
            var aboutTextPosition = new Vector2((screenWidth - aboutTextSize.X) / 2, (screenHeight - aboutTextSize.Y) / 2);

            //background
            _spriteBatch.Draw(bgMenu, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
            _spriteBatch.DrawString(_font, aboutText, aboutTextPosition, Color.White);
        }

        private void DrawCharacterSelection()
        {
            var screenWidth = _graphics.PreferredBackBufferWidth;
            var screenHeight = _graphics.PreferredBackBufferHeight;
            var menuTitleSize = _font.MeasureString("Character Selection");
            var menuTitlePosition = new Vector2(135, 50);

            //background
            _spriteBatch.Draw(bgMenu, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);

            _spriteBatch.DrawString(menuFont, "Character Selection", menuTitlePosition, Color.Coral);

            var characterNameSize = _font.MeasureString(characterNames[characterSelection]);
            var characterNamePosition = new Vector2((screenWidth - characterNameSize.X) / 2, (screenHeight - characterNameSize.Y) / 2);

            _spriteBatch.DrawString(_font, characterNames[characterSelection], characterNamePosition, Color.White);

            if (characterSelection == 1)
            {
                int frameX = currentFrame * frameWidth2;
                int frameY = 0;
                _spriteBatch.Draw(_textures2[0], new Vector2((screenWidth - frameWidth2) / 2, (screenHeight - frameHeight2) / 2), new Rectangle(frameX, frameY, frameWidth2, frameHeight2), Color.White);
            }
        }

    }
}