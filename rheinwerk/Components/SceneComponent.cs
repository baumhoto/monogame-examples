using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace rheinwerk.Components;

internal class SceneComponent : DrawableGameComponent
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Texture2D _starGold;
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
        _starGold = Game.Content.Load<Texture2D>("starGold");
        
        base.LoadContent();
    }

    public override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        
        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        _spriteBatch.Draw(_starGold, _game.Simulation.Position, Color.White);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}