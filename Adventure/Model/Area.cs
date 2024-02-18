using System.Collections.Generic;

namespace Adventure.Model;

public class Area
{
    
    public List<Item> Items { get; private set; }

    public int Width { get; private set; }
    
    public int Height { get; private set; }

    public Layer[] Layers { get; private set; }

    public Area(int layers, int width, int height)
    {
        Width = width;
        Height = height;

        this.Layers = new Layer[layers];

        for (int i = 0; i < layers; i++)
        {
            this.Layers[i] = new Layer(width, height);
        }
        
        Items = new List<Item>();
    }

    public bool IsCellBlocked(int x, int y)
    {

        // special case: not within bounds
        if (x < 0 || y < 0 || x > Width - 1 || y > Height - 1)
            return true;
        
        for (int l = 0; l < Layers.Length; l++)
        {
            if (Layers[l].Tiles[x, y].Blocked)
                return true;
        }
        return false;
    }
}