using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Module_8
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        SpriteFont menuFont;
        Texture2D bgMenu;

        SpriteFont buttonFont;
        Button[] button;

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
            Texture2D butTex = Content.Load<Texture2D>("menuButton");
            Color butColor = Color.White;

            button = new Button[3]; 
            Rectangle butRect1 = new Rectangle(300, 200, 350, 75);
            button[0] = new Button(butTex, butRect1, butColor);

            Rectangle butRect2 = new Rectangle(300, 275, 350, 75);
            button[1] = new Button(butTex, butRect2 , butColor);

            Rectangle butRect3 = new Rectangle(300, 350, 350, 75);
            button[2] = new Button(butTex, butRect3, butColor);

            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            bgMenu = Content.Load<Texture2D>("bgMenu");

            menuFont = Content.Load<SpriteFont>("GameFont");
            buttonFont = Content.Load<SpriteFont>("ButtonFont");
        }

        protected override void Update(GameTime gameTime)
        {
            MouseState mouse = Mouse.GetState();

            for(int i = 0; i < button.Length; i++)
            {
                if (button[i].ButRect.Contains(Mouse.GetState().Position))
                {
                    button[i].ButColor = Color.Wheat;

                    //if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    //{

                    ////}
                }
                else
                {
                    button[i].ButColor = Color.White;
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _spriteBatch.Draw(bgMenu, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);

            //title
            _spriteBatch.DrawString(menuFont, "Beach,       !", new Vector2(275,90), Color.MediumAquamarine);
            _spriteBatch.DrawString(menuFont, "Run", new Vector2(535, 90), Color.Coral);

            //buttons
            for(int i = 0; i < button.Length; i++)
            {
                _spriteBatch.Draw(button[i].ButTex, button[i].ButRect, button[i].ButColor);
            }
            
            //button text
            _spriteBatch.DrawString(buttonFont, "New Game", new Vector2(400,218), Color.SeaShell);
            _spriteBatch.DrawString(buttonFont, "Load Game", new Vector2(395, 293), Color.SeaShell);
            _spriteBatch.DrawString(buttonFont, "Exit", new Vector2(450, 368), Color.SeaShell);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}