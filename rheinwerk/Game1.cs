using Microsoft.Xna.Framework;
using rheinwerk.Components;

namespace rheinwerk;

internal class Game1 : Game
{
    public InputComponent Input { get; private set; }
    
    public SimulationComponent Simulation { get; private set; }
    
    public SceneComponent Scene { get; private set; }

    public Game1()
    {
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        
        Input = new(this);
        Input.UpdateOrder = 0;
        Components.Add(Input);
        
        Simulation = new(this);
        Simulation.UpdateOrder = 1;
        Components.Add(Simulation);
        
        Scene = new(this);
        Scene.UpdateOrder = 2;
        Scene.DrawOrder = 0;
        Components.Add(Scene);
    }
}
