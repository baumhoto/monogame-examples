namespace Adventure.Model;

public interface ICollidable
{
    public float Mass { get; set; }
    public bool Fixed { get; set; }    
}