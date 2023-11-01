using System.Runtime.CompilerServices;

namespace blocks;

public class Block
{
    private char[,,] _data;

    public int Rotations => _data.GetLength(0);

    public Block(char[,,] data)
    {
        _data = data;
    }

    public char this[int rotation, int y, int x]
    {
        get => _data[rotation, y, x];
    }
}