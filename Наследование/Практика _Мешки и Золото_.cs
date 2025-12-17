using Avalonia.Controls.Documents;
using Avalonia.Input;
using Digger.Architecture;
using System;

namespace Digger;

class Terrain : ICreature
{
    public CreatureCommand Act(int x, int y)
    {
        return new CreatureCommand { DeltaX = 0, DeltaY = 0 };
    }

    public bool DeadInConflict(ICreature conflictedObject)
    {
        return conflictedObject is Player;
    }

    public int GetDrawingPriority()
    {
        return 3;
    }

    public string GetImageFileName()
    {
        return "Terrain.png";
    }
}

class Player : ICreature
{
    public CreatureCommand Act(int x, int y)
    {
        var creatureCommand = new CreatureCommand() { DeltaX = 0, DeltaY = 0 };

        switch (Game.KeyPressed)
        {
            case Key.Left:
                if (x - 1 >= 0)
                {
                    creatureCommand.DeltaX = -1;
                }
                break;
            case Key.Right:
                if (x + 1 < Game.MapWidth)
                {
                    creatureCommand.DeltaX = 1;
                }
                break;
            case Key.Up:
                if (y - 1 >= 0)
                {
                    creatureCommand.DeltaY = -1;
                }
                break;
            case Key.Down:
                if (y + 1 < Game.MapHeight)
                {
                    creatureCommand.DeltaY = 1;
                }
                break;
        }

        if (Game.Map[x + creatureCommand.DeltaX, y + creatureCommand.DeltaY] is Sack)
        {
            creatureCommand.DeltaX = 0;
            creatureCommand.DeltaY = 0;
        }
        return creatureCommand;
    }

    public bool DeadInConflict(ICreature conflictedObject)
    {
        return conflictedObject is Sack;
    }

    public int GetDrawingPriority()
    {
        return 2;
    }

    public string GetImageFileName()
    {
        return "Digger.png";
    }
}

class Sack : ICreature
{
    int fallCounter = 0;
    public CreatureCommand Act(int x, int y)
    {
        var creatureCommand = new CreatureCommand { DeltaX = 0, DeltaY = 0 };

        if (y + 1 >= Game.MapHeight)
        {
            if (fallCounter > 1)
            {
                creatureCommand.TransformTo = new Gold();
            }
            fallCounter = 0;
            return creatureCommand;
        }

        var objectBelow = Game.Map[x, y + 1];

        if (objectBelow == null || (fallCounter > 0 && objectBelow is Player))
        {
            fallCounter++;
            creatureCommand.DeltaY = 1;
            return creatureCommand;
        }

        if (fallCounter > 1)
        {
            creatureCommand.TransformTo = new Gold();
        }

        fallCounter = 0;
        return creatureCommand;
    }

    public bool DeadInConflict(ICreature conflictedObject)
    {
        return false;
    }

    public int GetDrawingPriority()
    {
        return 0;
    }

    public string GetImageFileName()
    {
        return "Sack.png";
    }
}

class Gold : ICreature
{
    public CreatureCommand Act(int x, int y)
    {
        return new CreatureCommand { DeltaX = 0, DeltaY = 0 };
    }

    public bool DeadInConflict(ICreature conflictedObject)
    {
        Game.Scores += 10;
        return true;
    }

    public int GetDrawingPriority()
    {
        return 4;
    }

    public string GetImageFileName()
    {
        return "Gold.png";
    }
}
