using Avalonia;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;
using static Manipulation.Manipulator;

namespace Manipulation;

public static class AnglesToCoordinatesTask
{
	public static Point[] GetJointPositions(double shoulder, double elbow, double wrist)
	{
        var elbowX = UpperArm * Math.Cos(shoulder);
        var elbowY = UpperArm * Math.Sin(shoulder);
        var elbowPos = new Point(elbowX, elbowY);

        var angleForearm = shoulder + Math.PI + elbow;

        var wristX = elbowPos.X + Forearm * Math.Cos(angleForearm);
        var wristY = elbowPos.Y + Forearm * Math.Sin(angleForearm);
        var wristPos = new Point(wristX, wristY);

        var anglePalm = angleForearm + Math.PI + wrist;

        var palmX = wristPos.X + Palm * Math.Cos(anglePalm);
        var palmY = wristPos.Y + Palm * Math.Sin(anglePalm);
        var palmEndPos = new Point(palmX, palmY);

        return new[]
		{
			elbowPos,
			wristPos,
			palmEndPos
		};
	}
}

[TestFixture]
public class AnglesToCoordinatesTask_Tests
{
    [TestCase(Math.PI / 2, Math.PI / 2, Math.PI, Forearm + Palm, UpperArm)]
    [TestCase(Math.PI / 2, Math.PI / 2, Math.PI / 2, Forearm, UpperArm - Palm)]
    [TestCase(Math.PI / 2, Math.PI, Math.PI, 0, UpperArm + Forearm + Palm)]
    [TestCase(Math.PI / 2, Math.PI / 2, Math.PI, Forearm + Palm, UpperArm)]
	public void TestGetJointPositions(double shoulder, double elbow, double wrist, double palmEndX, double palmEndY)
	{
		var joints = AnglesToCoordinatesTask.GetJointPositions(shoulder, elbow, wrist);
        ClassicAssert.AreEqual(palmEndX, joints[2].X, 1e-5, "palm endX");
        ClassicAssert.AreEqual(palmEndY, joints[2].Y, 1e-5, "palm endY");
        ClassicAssert.AreEqual(UpperArm, GetDistance(new Point(0, 0), joints[0]), 1e-5, "Что-то не то с длиной UpperArm :(");
        ClassicAssert.AreEqual(Forearm, GetDistance(joints[0], joints[1]), 1e-5, "Что-то не то с длиной Forearm :(");
        ClassicAssert.AreEqual(Palm, GetDistance(joints[1], joints[2]), 1e-5, "Что-то не то с длиной Palm :(");
    }

    public double GetDistance(Point p1, Point p2)
    {
        var dx = p1.X - p2.X;
        var dy = p1.Y - p2.Y;
        return Math.Sqrt(dx * dx + dy * dy);
    }
}
