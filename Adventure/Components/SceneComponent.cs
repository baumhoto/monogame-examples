using Adventure.Model;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Adventure.Components;

internal class SceneComponent : DrawableGameComponent
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private AdventureGame _game;
    private Texture2D _pixel;
    
    public SceneComponent(AdventureGame game) : base(game)
    {
        _graphics = new GraphicsDeviceManager(game);
        _game = game;
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _pixel = new Texture2D(GraphicsDevice, 1, 1);
        _pixel.SetData(new [] { Color.White });
    }

    public override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        Area area = _game.Simulation.World.Areas[0];

        float scaleX = (float)(GraphicsDevice.Viewport.Width - 20) / area.Width;
        float scaleY = (float)(GraphicsDevice.Viewport.Height - 20) / area.Height;
        
        _spriteBatch.Begin();

        for (int x = 0; x < area.Width; x++)
        {
            for (int y = 0; y < area.Height; y++)
            {
                bool blocked = false;
                for (int i = 0; i < area.Layers.Length; i++)
                {

                    blocked |= area.Layers[i].Tiles[x, y].Blocked;
                }

                Color color = Color.DarkGreen;
                if (blocked)
                    color = Color.DarkRed;
                
                
                int offsetX = (int)(x * scaleX) + 10;
                int offsetY = (int)(y * scaleY) + 10;
                
                _spriteBatch.Draw(_pixel, new Rectangle(offsetX, offsetY, (int)scaleX, (int)scaleY), color); 
                _spriteBatch.Draw(_pixel, new Rectangle(offsetX, offsetY, 1, (int)scaleY), Color.Black);
                _spriteBatch.Draw(_pixel, new Rectangle(offsetX, offsetY, (int)scaleX, 1), Color.Black);
            }
            
        }

        foreach (var item in area.Items)
        {
            Color color = Color.Yellow;
            if (item is Player)
                color = Color.Red;

            int posX = (int)((item.Position.X - item.Radius) * scaleX) + 10;
            int posY = (int)((item.Position.Y - item.Radius) * scaleY) + 10;

            int size = (int)((item.Radius * 2) * scaleX);
            _spriteBatch.Draw(_pixel, new Rectangle(posX, posY, size, size), color);            

        }
        
        _spriteBatch.End();

    }
}