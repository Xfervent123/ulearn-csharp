using System;

namespace Mazes;

public static class DiagonalMazeTask
{
    public static void MoveOut(Robot robot, int width, int height)
    {
        var firstDirection = width > height ? Direction.Right : Direction.Down;
        var secondDirection = width > height ? Direction.Down : Direction.Right;
        var directionStepCount = (Math.Max(width, height) - 2) / (Math.Min(width, height) - 2);
        while (!robot.Finished)
        {
            Move(robot, firstDirection, directionStepCount);
            if (robot.Finished) break;
            Move(robot, secondDirection, 1);
        }
    }

    public static void Move(Robot robot, Direction direction, int steps)
    {
        for (var i = 0; i < steps; i++)
            robot.MoveTo(direction);
    }
}
