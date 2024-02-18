using Microsoft.Xna.Framework;

namespace Adventure.Model;

public class Item : ICollidable
{
    internal Vector2 move = Vector2.Zero;
    
    public Vector2 Position { get; set; }
    public float Radius { get; set; }
    
    public float Mass { get; set; }
    public bool Fixed { get; set; }
    

    public Item()
    {
        Fixed = false;
        Mass = 1;
    }
}