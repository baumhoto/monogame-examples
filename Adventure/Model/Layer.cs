using System;

namespace Adventure.Model;

public class Layer
{
    public int Width { get; private set; }
    
    public int Height { get; private set; }

    public Tile[,] Tiles { get; private set; }

    public Layer(int width, int height)
    {
        if (width < 1)
            throw new ArgumentException("GameArea width smaller than 1");
        if (height < 1)
            throw new ArgumentException("Gamearea height smaller than 1");
            
        Tiles = new Tile[width, height];
    }
}