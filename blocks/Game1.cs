using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace blocks;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Texture2D _pixels;
    private Color _backGroundColor;

    private int _blockSize = 40;
    private int _blockDrawSize;
    private int _gridX = 10;
    private int _gridY = 18;

    private char[][] _inert;

    private int _blockType = 0;
    private int _blockRotation = 0;
    private int _blockX = 3;
    private int _blockY = 0;

    private double _timer;
    private const double KeyPressCoolDown = 0.125;
    private double _keyPressedCooldownTimer = 0;

    private List<Block> _blocks = new List<Block>();
    private Dictionary<char, Color> _colors = new Dictionary<char, Color>();

    public Game1()
    {
        var windowHeight = _gridY * _blockSize;
        var windowWidth = _gridX * _blockSize;
        _graphics = new GraphicsDeviceManager(this);
        _graphics.PreferredBackBufferHeight = windowHeight;
        _graphics.PreferredBackBufferWidth = windowWidth;
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        _backGroundColor = new Color(255, 255, 255);
        _blockDrawSize = this._blockSize - 1;
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

        _inert = new char[_gridY][];

        for (int y = 0; y < _gridY; y++)
        {
            _inert[y] = new char[_gridX];
            for (int x = 0; x < _gridX; x++)
            {
                _inert[y][x] = ' ';
            }
        }

        _blocks.Add(new Block(Blocks.I));
        _blocks.Add(new Block(Blocks.O));
        _blocks.Add(new Block(Blocks.J));
        _blocks.Add(new Block(Blocks.L));
        _blocks.Add(new Block(Blocks.T));
        _blocks.Add(new Block(Blocks.S));
        _blocks.Add(new Block(Blocks.Z));

        _colors.Add(' ', new Color(222, 222, 222));
        _colors.Add('i', new Color(120, 195, 239));
        _colors.Add('j', new Color(236, 231, 108));
        _colors.Add('l', new Color(124, 218, 193));
        _colors.Add('o', new Color(234, 177, 121));
        _colors.Add('s', new Color(211, 147, 196));
        _colors.Add('t', new Color(248, 147, 196));
        _colors.Add('z', new Color(169, 221, 118));
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        _timer += gameTime.ElapsedGameTime.TotalSeconds;

        if (_timer >= 0.5)
        {
            _timer = 0;
            var testY = _blockY + 1;
            if (CanBlockMove(_blockX, testY, _blockRotation))
                _blockY = testY;
            
        }

        var state = Keyboard.GetState();
        if (_keyPressedCooldownTimer <= 0 && state.GetPressedKeyCount() > 0)
        {
            if (state.IsKeyDown(Keys.E))
            {
                var testRotattion = _blockRotation + 1;
                if (testRotattion > _blocks[_blockType].Rotations - 1)
                    testRotattion = 0;
                
                if (CanBlockMove(_blockX, _blockY, testRotattion))
                    _blockRotation = testRotattion;
            }
            else if (state.IsKeyDown(Keys.Q))
            {
                var testRotattion = _blockRotation - 1;
                if (testRotattion < 0)
                    testRotattion = _blocks[_blockType].Rotations - 1;
                
                if (CanBlockMove(_blockX, _blockY, testRotattion))
                    _blockRotation = testRotattion;
            }
            else if (state.IsKeyDown(Keys.A))
            {
                var testX = _blockX - 1;
                if (CanBlockMove(testX, _blockY, _blockRotation))
                    _blockX = testX;
            }
            else if (state.IsKeyDown(Keys.D))
            {
                var testX = _blockX + 1;
                if (CanBlockMove(testX, _blockY, _blockRotation))
                    _blockX = testX;
            }
            // TODO remove 
            else if (state.IsKeyDown(Keys.S))
            {
                _blockType += 1;
                if (_blockType > _blocks.Count - 1)
                    _blockType = 0;
                _blockRotation = 0;
            }

            _keyPressedCooldownTimer = KeyPressCoolDown;
        }
        else
        {
            _keyPressedCooldownTimer -= gameTime.ElapsedGameTime.TotalSeconds;
        }

        base.Update(gameTime);
    }


    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(_backGroundColor);
        _spriteBatch.Begin();
        for (int y = 0; y < _gridY; y++)
        {
            for (int x = 0; x < _gridX; x++)
            {
                var block = _inert[y][x];
                if (block != ' ')
                    DrawBlock(x, y, _colors[block]);
            }
        }

        for (int y = 0; y < 4; y++)
        {
            for (int x = 0; x < 4; x++)
            {
                var block = _blocks[_blockType][_blockRotation, y, x];
                if (block != ' ')
                    DrawBlock(x + _blockX, y + _blockY, _colors[block]);
            }
        }

        _spriteBatch.End();

        base.Draw(gameTime);
    }

    private void DrawBlock(int x, int y, Color color)
    {
        _spriteBatch.Draw(_pixels,
            new Rectangle(x * _blockSize, y * _blockSize, _blockDrawSize - 1, _blockDrawSize - 1), color);
    }

    private bool CanBlockMove(int test_x, int test_y, int test_rotation)
    {
        for (int y = 0; y < 4; y++)
        {
            for (int x = 0; x < 4; x++)
            {
               if(_blocks[_blockType][test_rotation, y, x] != ' ' && (test_x + x) < 0)
                   return false;
            } 
        }
        
        
        return true;
    }
}