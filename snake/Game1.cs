using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace snake;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Texture2D _pixels;
    private List<Vector2> _snakeSegments;
    private Vector2 _foodPosition;

    private Rectangle _rectangle;
    private int _cellSize = 30;
    private int _gridX = 20;
    private int _gridY = 15;
    private double _timer = 0.0;
    private bool _snakeAlive;

    private readonly Queue<Keys> _directionQueue;
    private readonly Random _random = new Random();

    public Game1()
    {
        var windowHeight = _gridY * _cellSize;
        var windowWidth = _gridX * _cellSize;

        _graphics = new GraphicsDeviceManager(this);
        _graphics.PreferredBackBufferHeight = windowHeight;
        _graphics.PreferredBackBufferWidth = windowWidth;
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        _directionQueue = new Queue<Keys>();
        
        Reset();
    }

    protected override void Initialize()
    {
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _pixels = new Texture2D(GraphicsDevice, 1, 1);
        _pixels.SetData<Color>(new Color[] { Color.White });
        _rectangle = new Rectangle(0, 0, _gridX * _cellSize, _gridY * _cellSize);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        KeyboardState state = Keyboard.GetState();

        if (state.IsKeyDown(Keys.Right)
            && _directionQueue.Last() != Keys.Right
            && _directionQueue.Last() != Keys.Left)
        {
            _directionQueue.Enqueue(Keys.Right);
        }
        else if (state.IsKeyDown(Keys.Down)
                 && _directionQueue.Last() != Keys.Down
                 && _directionQueue.Last() != Keys.Up)
        {
            _directionQueue.Enqueue(Keys.Down);
        }
        else if (state.IsKeyDown(Keys.Left)
                 && _directionQueue.Last() != Keys.Left
                 && _directionQueue.Last() != Keys.Right)
        {
            _directionQueue.Enqueue(Keys.Left);
        }
        else if (state.IsKeyDown(Keys.Up)
                 && _directionQueue.Last() != Keys.Up
                 && _directionQueue.Last() != Keys.Down)
        {
            _directionQueue.Enqueue(Keys.Up);
        }

        _timer += gameTime.ElapsedGameTime.TotalSeconds;
        // Console.WriteLine(_timer);
        if (_snakeAlive)
        {
            if (_timer >= 0.15)
            {
                if (_directionQueue.Count() >= 2)
                {
                    Keys key;
                    _directionQueue.TryDequeue(out key);
                }

                var nextX = _snakeSegments[0].X;
                var nextY = _snakeSegments[0].Y;

                if (_directionQueue.Peek() == Keys.Right)
                {
                    nextX++;
                    if (nextX >= _gridX)
                        nextX = 0;
                }
                else if (_directionQueue.Peek() == Keys.Left)
                {
                    nextX--;
                    if (nextX < 0)
                        nextX = _gridX - 1;
                }
                else if (_directionQueue.Peek() == Keys.Down)
                {
                    nextY++;
                    if (nextY >= _gridY)
                        nextY = 0;
                }
                else if (_directionQueue.Peek() == Keys.Up)
                {
                    nextY--;
                    if (nextY < 0)
                        nextY = _gridY - 1;
                }

                var nextPos = new Vector2(nextX, nextY);

                var canMove = true;

                foreach (var segmeent in _snakeSegments)
                {
                    if (nextPos.X == segmeent.X && nextPos.Y == segmeent.Y)
                    {
                        canMove = false;
                    }
                }

                if (canMove)
                {
                    _snakeSegments.Insert(0, nextPos);

                    if (_snakeSegments[0].X == _foodPosition.X
                        && _snakeSegments[0].Y == _foodPosition.Y)
                    {
                        MoveFood();
                    }
                    else
                    {
                        _snakeSegments.RemoveAt(_snakeSegments.Count - 1);
                    }
                }
                else
                {
                    _snakeAlive = false;
                }

                _timer = 0;
                // Console.WriteLine("tick");
            }
        }
        else if (_timer > 2)
        {
           Reset(); 
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();
        _spriteBatch.Draw(_pixels, _rectangle, new Color(70, 70, 70));
        var snakeColor = _snakeAlive ? new Color(165, 255, 81) : new Color(140,140,140); 
        foreach (var segment in _snakeSegments)
        {
            DrawCell((int)segment.X, (int)segment.Y, snakeColor);
        }

        DrawCell((int)_foodPosition.X, (int)_foodPosition.Y, new Color(255, 76, 76));
        _spriteBatch.End();

        base.Draw(gameTime);
    }

    private void DrawCell(int x, int y, Color color)
    {
        _spriteBatch.Draw(_pixels, new Rectangle(x * _cellSize, y * _cellSize, _cellSize - 1, _cellSize - 1), color);
    }

    private void MoveFood()
    {
        var possibleFoodPositions = new List<Vector2>();

        for (int i = 0; i < _gridX; i++)
        {
            for (int j = 0; j < _gridY; j++)
            {
                var possible = true;

                foreach (var segment in _snakeSegments)
                {
                    if (i == segment.X && j == segment.Y)
                        possible = false;
                }

                if (possible)
                    possibleFoodPositions.Add(new Vector2(i, j));
            }
        }

        _foodPosition = possibleFoodPositions[_random.Next(possibleFoodPositions.Count - 1)];
    }

    private void Reset()
    {
        _timer = 0;
        _directionQueue.Clear();
        _directionQueue.Enqueue(Keys.Right);
        
        _snakeSegments = new List<Vector2>();
        _snakeSegments.Add(new Vector2(2, 0));
        _snakeSegments.Add(new Vector2(1, 0));
        _snakeSegments.Add(new Vector2(0, 0));
        
        MoveFood();
        _snakeAlive = true;
    }
}