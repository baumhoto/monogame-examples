using Adventure.Components;
using Microsoft.Xna.Framework;

namespace Adventure;

internal class AdventureGame : Game
{
    private  GraphicsDeviceManager _graphics;
    public InputComponent Input { get; private set; }
    public SimulationComponent Simulation { get; private set; }
    public SceneComponent Scene { get; private set; }
    
    public AdventureGame()
    {
        // _graphics = new GraphicsDeviceManager(this);
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