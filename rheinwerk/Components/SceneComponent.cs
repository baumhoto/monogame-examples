using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace rheinwerk.Components;

internal class SceneComponent : DrawableGameComponent
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    // private Texture2D _starGold;
    private Texture2D _pixel;
    private Game1 _game;
    
    public SceneComponent(Game1 game) : base(game)
    {
        _graphics = new GraphicsDeviceManager(game);
        _game = game;
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        // _starGold = Game.Content.Load<Texture2D>("starGold");
        _pixel = new Texture2D(GraphicsDevice, 1, 1);
        _pixel.SetData<Color>(new Color[] { Color.White });
        
        base.LoadContent();
    }

    public override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        int width = GraphicsDevice.Viewport.Width - 20;
        int height = GraphicsDevice.Viewport.Height - 20;
        
        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        // _spriteBatch.Draw(_starGold, _game.Simulation.Position, Color.White);
        _spriteBatch.Draw(_pixel, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, 10), Color.DarkGray);
        _spriteBatch.Draw(_pixel, new Rectangle(0,  GraphicsDevice.Viewport.Height - 10,GraphicsDevice.Viewport.Width, 10), Color.DarkGray);
        _spriteBatch.Draw(_pixel, new Rectangle(GraphicsDevice.Viewport.Width - 10,  0, 10 ,GraphicsDevice.Viewport.Height), Color.DarkGray);
        
        _spriteBatch.Draw(_pixel, new Rectangle((int)(_game.Simulation.BallPosition.X * width) + 10, 
            (int)(_game.Simulation.BallPosition.Y * height) + 10, 10, 10), Color.White);

        int playerRadius = (int)(_game.Simulation.PlayerSize * height) / 2;
        int player = (int)(height * _game.Simulation.PlayerPosition) - playerRadius + 10;
        
        _spriteBatch.Draw(_pixel, new Rectangle(0,  player, 10 ,playerRadius * 2), Color.DarkGray);
        
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}