using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;



namespace ThunderingTanks.Objects
{
    public class Menu
    {
        public const string ContentFolderMusic = "Music/";
        public const string ContentFolderModels = "Models/";
        public const string ContentFolderTextures = "Textures/";
        public const string ContentFolderEffects = "Effects/";
        public const string ContentFolderFonts = "Fonts/";

        private float ScreenWidth { get; set; }
        private float ScreenHeight { get; set; }

        public float MasterSound { get; set; }
        private Song backgroundSound { get; set; }

        private SpriteFont _font;
        private SpriteFont WarIsOver;

        private Texture2D _cursorTexture;

        private bool _playing = true;

        #region BOTONS

        private bool controlsMenuActive = false;
        private Rectangle _backButton;
        private Rectangle _playButton;
        private Rectangle _exitButton;
        private Rectangle _controlsButton;
        private Rectangle _soundOnButton;
        private Rectangle _soundOffButton;
        private Texture2D RectangleButtonHover { get; set; }
        private Texture2D RectangleButton { get; set; }
        private Texture2D PlayButton { get; set; }
        private Texture2D PlayButtonHover { get; set; }
        private Texture2D SoundOnButton { get; set; }
        private Texture2D SoundOnButtonHover { get; set; }
        private Texture2D SoundOffButton { get; set; }
        private Texture2D SoundOffButtonHover { get; set; }
        private Texture2D RectangleButtonNormal { get; set; }
        private Texture2D PlayButtonNormal { get; set; }
        private Texture2D SoundOnButtonNormal { get; set; }
        private Texture2D SoundOffButtonNormal { get; set; }

        #endregion

        private bool SoundIsOn = true;
        private bool ResetMouse = true;

        public Menu(ContentManager contentManager)
        {

            ScreenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            ScreenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

            int buttonWidth = 400;
            int buttonHeight = 200;
            int buttonSpacing = 20;

            int buttonY = (int)(ScreenHeight - 3 * buttonHeight - buttonSpacing) / 2;
            int buttonX = (int)ScreenWidth - buttonWidth - 300;

            _playButton = new Rectangle(buttonX, buttonY, buttonWidth, buttonHeight);
            _controlsButton = new Rectangle(buttonX, buttonY + buttonHeight + buttonSpacing, buttonWidth, buttonHeight);
            _exitButton = new Rectangle(buttonX, buttonY + 2 * buttonHeight + buttonSpacing, buttonWidth, buttonHeight);
            _backButton = new Rectangle(buttonX, buttonY + 3 * buttonHeight + buttonSpacing, buttonWidth, buttonHeight);

            _soundOnButton = new Rectangle(50, (int)(ScreenHeight - 300), 100, 100);
            _soundOffButton = new Rectangle(50, (int)(ScreenHeight - 300), 100, 100);

        }

        public void LoadContent(ContentManager Content)
        {

            backgroundSound = Content.Load<Song>(ContentFolderMusic + "TankGameBackgroundSound");
            _cursorTexture = Content.Load<Texture2D>(ContentFolderTextures + "proyectilMouse");
            _font = Content.Load<SpriteFont>(ContentFolderFonts + "arial");
            WarIsOver = Content.Load<SpriteFont>(ContentFolderFonts + "warisover/WarIsOver");

            RectangleButtonNormal = Content.Load<Texture2D>(ContentFolderTextures + "Menu/Default@4x");
            PlayButtonNormal = Content.Load<Texture2D>(ContentFolderTextures + "Menu/PlayButton");
            SoundOnButtonNormal = Content.Load<Texture2D>(ContentFolderTextures + "Menu/SoundOn");
            SoundOffButtonNormal = Content.Load<Texture2D>(ContentFolderTextures + "Menu/SoundOff");

            RectangleButtonHover = Content.Load<Texture2D>(ContentFolderTextures + "Menu/RectangleButtonHover");
            PlayButtonHover = Content.Load<Texture2D>(ContentFolderTextures + "Menu/PlayButtonHover");
            SoundOnButtonHover = Content.Load<Texture2D>(ContentFolderTextures + "Menu/SoundOnHover");
            SoundOffButtonHover = Content.Load<Texture2D>(ContentFolderTextures + "Menu/SoundOffHover");

        }

        public void Update(ref bool juegoIniciado, GameTime gameTime)
        {
            var mouseState = Mouse.GetState();
            var mousePosition = new Point(mouseState.X, mouseState.Y);

            if (mouseState.LeftButton == ButtonState.Released)
                ResetMouse = true;

            if (controlsMenuActive)
            {
                // Logica del menu de controles
                if (mouseState.LeftButton == ButtonState.Pressed && _backButton.Contains(mousePosition))
                {
                    controlsMenuActive = false;
                    SoundIsOn = true;
                    ResetMouse = false;
                }

                if (_backButton.Contains(mousePosition))
                {
                    RectangleButton = RectangleButtonHover;
                }
                else
                {
                    RectangleButton = RectangleButtonNormal;
                }
            }
            else
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    if (_playButton.Contains(mousePosition))
                    {
                        juegoIniciado = true;
                        MediaPlayer.Stop();
                    }
                    else if (_exitButton.Contains(mousePosition))
                    {
                        Environment.Exit(0);
                    }
                    else if (_controlsButton.Contains(mousePosition))
                    {
                        controlsMenuActive = true;
                    }
                    if (SoundIsOn && _soundOffButton.Contains(mousePosition) && ResetMouse)
                    {
                        if (MediaPlayer.State == MediaState.Playing)
                            MediaPlayer.Pause();

                        SoundIsOn = false;
                        ResetMouse = false;
                    }
                    else if (!SoundIsOn && _soundOnButton.Contains(mousePosition) && ResetMouse)
                    {
                        if (MediaPlayer.State == MediaState.Paused)
                            MediaPlayer.Resume();

                        SoundIsOn = true;
                        ResetMouse = false;
                    }
                }

                // Logica para botones de menu principal
                if (_playButton.Contains(mousePosition))
                {
                    PlayButton = PlayButtonHover;
                }
                else
                {
                    PlayButton = PlayButtonNormal;
                }

                if (_exitButton.Contains(mousePosition))
                {
                    RectangleButton = RectangleButtonHover;
                }
                else
                {
                    RectangleButton = RectangleButtonNormal;
                }

                if (_soundOnButton.Contains(mousePosition))
                {
                    SoundOnButton = SoundOnButtonHover;
                }
                else
                {
                    SoundOnButton = SoundOnButtonNormal;
                }

                if (_soundOffButton.Contains(mousePosition))
                {
                    SoundOffButton = SoundOffButtonHover;
                }
                else
                {
                    SoundOffButton = SoundOffButtonNormal;
                }

                if (_playing)
                {
                    MediaPlayer.Play(backgroundSound);
                    _playing = false;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (controlsMenuActive)
            {
                // Dibujar el menu de controles
                spriteBatch.Draw(RectangleButton, _backButton, Color.Gray);
                Vector2 backTextPosition = new Vector2(_backButton.X + (_backButton.Width - (_font.MeasureString("back").X + 100)) / 2, _backButton.Y + (_backButton.Height - (_font.MeasureString("back").Y + 70)) / 2);

                spriteBatch.DrawString(WarIsOver, "BACK", backTextPosition, Color.White, 0f, Vector2.Zero, 3f, SpriteEffects.None, 0f);

                spriteBatch.DrawString(WarIsOver, "CONTROLES", new Vector2(ScreenHeight + 50, 200), Color.SandyBrown, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
                spriteBatch.DrawString(WarIsOver, "ADELANTE", new Vector2(ScreenHeight + 50, 300), Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
                spriteBatch.DrawString(WarIsOver, "ATRAS", new Vector2(ScreenHeight + 50, 350), Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
                spriteBatch.DrawString(WarIsOver, "IZQUIERDA", new Vector2(ScreenHeight + 50, 400), Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
                spriteBatch.DrawString(WarIsOver, "DERECHA", new Vector2(ScreenHeight + 50, 450), Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);

                spriteBatch.DrawString(WarIsOver, "DISPARAR", new Vector2(ScreenHeight + 50, 500), Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
                spriteBatch.DrawString(WarIsOver, "PAUSA", new Vector2(ScreenHeight + 50, 550), Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);

                spriteBatch.DrawString(WarIsOver, ": W", new Vector2(ScreenHeight + 270, 300), Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
                spriteBatch.DrawString(WarIsOver, ": S", new Vector2(ScreenHeight + 270, 350), Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
                spriteBatch.DrawString(WarIsOver, ": A", new Vector2(ScreenHeight + 270, 400), Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
                spriteBatch.DrawString(WarIsOver, ": D", new Vector2(ScreenHeight + 270, 450), Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);

                spriteBatch.DrawString(WarIsOver, ": CLICK IZQUIERDO", new Vector2(ScreenHeight + 270, 500), Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
                spriteBatch.DrawString(WarIsOver, ": ESC", new Vector2(ScreenHeight + 270, 550), Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
                spriteBatch.DrawString(WarIsOver, "SI SE VA AL MENU, SE REINICIA LA POSICION A LA INICIAL", new Vector2(ScreenHeight + 50, 700), Color.GreenYellow, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                spriteBatch.DrawString(WarIsOver, "UNA VEZ ADENTRO DEL JUEGO", new Vector2(ScreenHeight + 150, 675), Color.GreenYellow, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

                var mouseState = Mouse.GetState();
                spriteBatch.Draw(_cursorTexture, new Vector2(mouseState.X, mouseState.Y), Color.White);
            }
            else
            {
                if (SoundIsOn)
                {
                    spriteBatch.Draw(SoundOnButton, _soundOnButton, Color.Gray);
                }
                else
                {
                    spriteBatch.Draw(SoundOffButton, _soundOffButton, Color.Gray);
                }

                spriteBatch.Draw(PlayButton, _playButton, Color.Gray);
                spriteBatch.Draw(RectangleButton, _controlsButton, Color.Gray);
                spriteBatch.Draw(RectangleButton, _exitButton, Color.Gray);

                spriteBatch.DrawString(WarIsOver, "THUNDERING TANKS", new Vector2(450, 200), Color.SandyBrown);
                spriteBatch.DrawString(WarIsOver, "VOLUMEN = " + MasterSound.ToString("P"), new Vector2(50, ScreenHeight - 200), Color.SaddleBrown);

                Vector2 controlsTextPosition = new Vector2(_controlsButton.X + (_controlsButton.Width - (_font.MeasureString("Controls").X + 220)) / 2, _controlsButton.Y + (_controlsButton.Height - (_font.MeasureString("Controls").Y + 70)) / 2);

                Vector2 exitTextPosition = new Vector2(_exitButton.X + (_exitButton.Width - (_font.MeasureString("SALIR").X + 100)) / 2, _exitButton.Y + (_exitButton.Height - (_font.MeasureString("SALIR").Y + 70)) / 2);

                spriteBatch.DrawString(WarIsOver, "SALIR", exitTextPosition, Color.White, 0f, Vector2.Zero, 3f, SpriteEffects.None, 0f);
                spriteBatch.DrawString(WarIsOver, "CONTROLES", controlsTextPosition, Color.White, 0f, Vector2.Zero, 3f, SpriteEffects.None, 0f);

                var mouseState = Mouse.GetState();
                spriteBatch.Draw(_cursorTexture, new Vector2(mouseState.X, mouseState.Y), Color.White);

            }
        }
    }
}
