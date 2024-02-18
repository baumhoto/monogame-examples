using System;
using System.Linq;
using Adventure.Model;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Adventure.Components;

internal class SimulationComponent : GameComponent
{
    private float gap = 0.00001f;
    
    private readonly AdventureGame _game;

    public World World { get; private set; }

    public Player Player { get; private set; }

    public SimulationComponent(AdventureGame game) : base(game)
    {
        _game = game;
        NewGame();
    }

    public void NewGame()
    {
        World = new World();
        Area area = new Area(2, 30, 20);

        for (int x = 0; x < area.Width; x++)
        {
            for (int y = 0; y < area.Height; y++)
            {
                area.Layers[0].Tiles[x, y] = new Tile();
                area.Layers[1].Tiles[x, y] = new Tile();
                
                if (x == 0 || y == 0 || x == area.Width -1 || y == area.Height -1)
                    area.Layers[0].Tiles[x, y].Blocked = true;
            }
        }
        
        Player = new Player() { Position = new Vector2(15, 10), Radius = 0.25f };
        Diamond diamond = new Diamond() { Position = new Vector2(10, 10), Radius = 0.25f };
        
        area.Items.Add(Player);
        area.Items.Add(diamond);
        
        World.Areas.Add(area);
    }

    public override void Update(GameTime gameTime)
    {
        Player.Velocity = _game.Input.Movement * 10f;

        foreach (var area in World.Areas)
        {
            foreach (var character in area.Items.OfType<Character>())
            {
                // recalculation of character position
                character.move += character.Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

                // Collision with all other items
                foreach (var item in area.Items)
                {
                   if (item == character) continue;

                   Vector2 distance = (item.Position + item.move) - (character.Position + character.move);
                   float overlap = item.Radius + character.Radius - distance.Length();
                   if (overlap > 0f)
                   {
                       Vector2 resolution = distance * (overlap / distance.Length());
                       if (item.Fixed && !character.Fixed)
                       {
                          // Item fixed
                          character.move -= resolution;
                       }
                       else if (!item.Fixed && character.Fixed)
                       {
                           // character fixed
                           item.move += resolution;
                       }
                       else if (!item.Fixed && !character.Fixed)
                       {
                           // none fixed
                           float totalMass = item.Mass + character.Mass;
                           character.move -= resolution * (item.Mass / totalMass);
                           item.move += resolution * (character.Mass / totalMass);
                       }
                   }
                }
            }

            foreach (var item in area.Items)
            {
                bool collision = false;
                int loops = 0;
                do
                {

                    Vector2 position = item.Position + item.move;
                    int minCellX = (int)(position.X - item.Radius);
                    int maxCellX = (int)(position.X + item.Radius);
                    int minCellY = (int)(position.Y - item.Radius);
                    int maxCellY = (int)(position.Y + item.Radius);


                    collision = false;
                    float minImpact = 2f;
                    int minAxis = 0;

                    for (int x = minCellX; x <= maxCellX; x++)
                    {
                        for (int y = minCellY; y <= maxCellY; y++)
                        {
                            if (!area.IsCellBlocked(x, y)) continue;

                            if (position.X - item.Radius > x + 1 ||
                                position.X + item.Radius < x ||
                                position.Y + item.Radius > y + 1 ||
                                position.Y + item.Radius < y)
                                continue;

                            collision = true;
                            
                            float diffX = float.MaxValue;
                            if (item.move.X > 0) diffX = position.X + item.Radius - x + gap;
                            if (item.move.X < 0) diffX = position.X - item.Radius - (x + 1) - gap;
                            float impactX = 1f - (diffX / item.move.X);

                            float diffY = float.MaxValue;
                            if (item.move.Y > 0) diffY = position.Y + item.Radius - y + gap;
                            if (item.move.Y < 0) diffY = position.Y - item.Radius - (y + 1) - gap;
                            float impactY = 1f - (diffY / item.move.Y);

                            int axis = 0;
                            float impact = 0;
                            if (impactX > impactY)
                            {
                                axis = 1;
                                impact = impactX;
                            }
                            else
                            {
                                axis = 2;
                                impact = impactY;
                            }

                            // is this collision earlier
                            if (impact < minImpact)
                            {
                                minImpact = impact;
                                minAxis = axis;
                            }
                        }
                    }

                    if (collision)
                    {
                        if (minAxis == 1) item.move *= new Vector2(minImpact, 1f);
                        if (minAxis == 2) item.move *= new Vector2(1f, minImpact);
                    }

                    loops++;
                } while (collision && loops < 2);

                item.Position += item.move;
                item.move = Vector2.Zero;
                
            }
        }
        
        base.Update(gameTime);
    }
}