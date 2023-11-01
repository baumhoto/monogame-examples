using Microsoft.Xna.Framework;

namespace rheinwerk.Components;

internal class SimulationComponent : GameComponent
{
    private readonly Game1 _game;
    
    public Vector2 Position
    {
        get;
        private set;
    }
    
    public SimulationComponent(Game1 game) : base(game)
    {
        _game = game;
    }

    public override void Update(GameTime gameTime)
    {
        Position += _game.Input.Direction;
        base.Update(gameTime);
    }
}