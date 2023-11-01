using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace rheinwerk.Components;

internal class InputComponent : GameComponent
{
    private readonly Game1 _game;
    
    public Vector2 Direction { get; private set;  }
    
    public InputComponent(Game1 game) : base(game)
    {
        _game = game;
    }

    public override void Update(GameTime gameTime)
    {
        var gamepadState = GamePad.GetState(PlayerIndex.One);
        Direction += gamepadState.ThumbSticks.Left * new Vector2(1, -1);
        
        base.Update(gameTime);
    }
}