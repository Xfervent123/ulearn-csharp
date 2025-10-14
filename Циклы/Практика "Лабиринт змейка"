namespace Mazes;

public static class SnakeMazeTask
{
    public static void MoveOut(Robot robot, int width, int height)
    {
        while (!robot.Finished)
        {
            Move(robot, Direction.Right, width - 3);
			Move(robot, Direction.Down, 2);
			Move(robot, Direction.Left, width - 3);
			if (robot.Finished) break;
            Move(robot, Direction.Down, 2);
        }
	}

    public static void Move(Robot robot, Direction direction, int steps)
    {
        for (int i = 0; i < steps; i++)
            robot.MoveTo(direction);
    }
}
