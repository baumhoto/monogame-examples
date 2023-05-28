using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace rectangles;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    
    private Texture2D _pixel;
    private Rectangle _bigRectangle;
    private Rectangle _smallRectangle;
    private Color _smallRectangleColor;
    private bool smallrectangleMovesLeft;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        _pixel = new Texture2D(GraphicsDevice, 1, 1);
        _pixel.SetData<Color>( new Color[] { Color.White });

        _bigRectangle = new Rectangle(200, 100, 300, 150);
        _smallRectangle = new Rectangle(600, 150, 50, 50);

        _smallRectangleColor = Color.White; 
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        
        _spriteBatch.Begin();
        _spriteBatch.Draw(_pixel, _bigRectangle, Color.Black);
        _spriteBatch.Draw(_pixel, _smallRectangle, _smallRectangleColor);
        _spriteBatch.End();

        // TODO: Add your drawing code here

        base.Draw(gameTime);
    }
}
