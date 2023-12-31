using System;
using Microsoft.Xna.Framework;

namespace rheinwerk.Components;

internal class SimulationComponent : GameComponent
{
    private readonly Game1 _game;

    private Vector2 _ballVelocity = new Vector2(0.3f, 0.2f);
    
    public Vector2 BallPosition
    {
        get;
        private set;
    }

    public float PlayerPosition
    {
        get;
        private set;
    }
    
    public float PlayerSize
    {
        get;
        private set;
    }
    
    public SimulationComponent(Game1 game) : base(game)
    {
        _game = game;
        BallPosition = new Vector2(0.3f, 0.2f);
        PlayerPosition = 0.5f;
        PlayerSize = 0.2f;
    }

    public override void Update(GameTime gameTime)
    {
        BallPosition += _ballVelocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        PlayerPosition += _game.Input.Direction.Y  * (float) gameTime.ElapsedGameTime.TotalSeconds * 0.5f;
        
        if (BallPosition.X < 0f)
        {
            if (BallPosition.Y < PlayerPosition - (PlayerSize / 2)
                || BallPosition.Y > PlayerPosition + (PlayerSize / 2))
            {
                throw new Exception("Player hat verloren");
            }
                
            BallPosition = new Vector2(0f, BallPosition.Y);
            _ballVelocity *= new Vector2(-1f, 1f);
        }
        
        if (BallPosition.Y < 0f)
        {
            BallPosition = new Vector2(BallPosition.X, 0f);
            _ballVelocity *= new Vector2(1f, -1f);
        }

        if (BallPosition.X > 1f)
        {
            BallPosition = new Vector2(1f, BallPosition.Y);
            _ballVelocity *= new Vector2(-1f, 1f);
        }
        if (BallPosition.Y > 1f)
        {
            BallPosition = new Vector2(BallPosition.X, 1f);
            _ballVelocity *= new Vector2(1f, -1f);
        }
        base.Update(gameTime);
    }
}