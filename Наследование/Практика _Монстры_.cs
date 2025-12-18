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
        return conflictedObject is Sack || conflictedObject is Monster;
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

        if (objectBelow == null || (fallCounter > 0 && (objectBelow is Player || objectBelow is Monster)))
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

class Monster : ICreature
{
    public CreatureCommand Act(int x, int y)
    {
        var creatureCommand = new CreatureCommand { DeltaX = 0, DeltaY = 0 };

        var diggerX = -1;
        var diggerY = -1;

        for (int i = 0; i < Game.MapWidth; i++)
        {
            for (int j = 0; j < Game.MapHeight; j++)
            {
                var creature = Game.Map[i, j];
                if (creature is Player)
                {
                    diggerX = i;
                    diggerY = j;
                    break;
                }
            }
        }

        if (diggerX == -1) return creatureCommand;

        var dx = 0;
        var dy = 0;

        if (diggerX > x) dx = 1;
        else if (diggerX < x) dx = -1;

        if (diggerY > y) dy = 1;
        else if (diggerY < y) dy = -1;

        if (dx != 0 && CanMoveTo(x + dx, y))
        {
            creatureCommand.DeltaX = dx;
        }
        else if (dy != 0 && CanMoveTo(x, y + dy))
        {
            creatureCommand.DeltaY = dy;
        }

        return creatureCommand;
    }

    private bool CanMoveTo(int nx, int ny)
    {
        if (nx < 0 || ny < 0 || nx >= Game.MapWidth || ny >= Game.MapHeight)
            return false;

        var obj = Game.Map[nx, ny];

        return !(obj is Terrain || obj is Sack || obj is Monster);
    }

    public bool DeadInConflict(ICreature conflictedObject)
    {
        return conflictedObject is Sack || conflictedObject is Monster;
    }

    public int GetDrawingPriority()
    {
        return 1;
    }

    public string GetImageFileName()
    {
        return "Monster.png";
    }
}
