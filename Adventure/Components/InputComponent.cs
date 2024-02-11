using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Adventure.Components;

internal class InputComponent : GameComponent
{
    private readonly AdventureGame _game;
    
    public Vector2 Movement { get; private set;  }
    
    public InputComponent(AdventureGame game) : base(game)
    {
        _game = game;
    }

    public override void Update(GameTime gameTime)
    {
        var movement = Vector2.Zero;

        // Gamepad
        var gamepadState = GamePad.GetState(PlayerIndex.One);
        movement += gamepadState.ThumbSticks.Left * new Vector2(1f, -1f);
        
        // Keyboard
        KeyboardState keyboard = Keyboard.GetState();
        if (keyboard.IsKeyDown(Keys.A)) 
            movement += new Vector2(-1f, 0f);
        if (keyboard.IsKeyDown(Keys.D))
            movement += new Vector2(1f, 0f);
        if (keyboard.IsKeyDown(Keys.W))
            movement += new Vector2(0f, -1f);
        if (keyboard.IsKeyDown(Keys.S))
            movement += new Vector2(0f, 1f);
            
        if (movement.Length() > 1f)
            movement.Normalize();

        Movement = movement;
        
        base.Update(gameTime);
    }
}