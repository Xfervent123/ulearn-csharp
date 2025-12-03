using System;
using NUnit.Framework;
using static Manipulation.Manipulator;
using NUnit.Framework.Legacy;

namespace Manipulation;

public static class ManipulatorTask
{
    public static double[] MoveManipulatorTo(double x, double y, double alpha)
    {
        var wristX = x - Palm * Math.Cos(alpha);
        var wristY = y - Palm * Math.Sin(alpha);

        var distToWrist = Math.Sqrt(wristX * wristX + wristY * wristY);
		var elbow = Math.PI - TriangleTask.GetABAngle(UpperArm, Forearm, distToWrist);
		var shoulder = Math.Atan2(wristY, wristX) + TriangleTask.GetABAngle(distToWrist, UpperArm, Forearm);
        var wrist = -alpha - shoulder - elbow;

        if (double.IsNaN(wrist))
            return new[] { double.NaN, double.NaN, double.NaN };

        return new[] { shoulder, elbow, wrist };
    }
}

[TestFixture]
public class ManipulatorTask_Tests
{
    [Test]
    public void TestMoveManipulatorTo()
    {
        var rnd = new Random();

        for (int i = 0; i < 52; i++)
        {
            var x = (rnd.NextDouble() - 0.5) * 520;
            var y = (rnd.NextDouble() - 0.5) * 520;
            var alpha = (rnd.NextDouble() - 0.5) * 2 * Math.PI;

            var angles = ManipulatorTask.MoveManipulatorTo(x, y, alpha);

            var expectedWristX = x - Palm * Math.Cos(alpha);
            var expectedWristY = y - Palm * Math.Sin(alpha);
            var distToWrist = Math.Sqrt(expectedWristX * expectedWristX + expectedWristY * expectedWristY);

            var maxArmLen = UpperArm + Forearm;
            var minArmLen = Math.Abs(UpperArm - Forearm);

            var isImpossible = distToWrist > maxArmLen || distToWrist < minArmLen;

            if (double.IsNaN(angles[0]))
            {
                ClassicAssert.IsTrue(isImpossible,
                    $"Метод вернул NaN для решаемой позиции: X={x}, Y={y}, Alpha={alpha} :(");
            }
            else
            {
                var positions = AnglesToCoordinatesTask.GetJointPositions(angles[0], angles[1], angles[2]);
                var PalmEnd = positions[2];

                ClassicAssert.AreEqual(x, PalmEnd.X, 1e-4, "Что-то не то с координатой X конца кисти :(");
                ClassicAssert.AreEqual(y, PalmEnd.Y, 1e-4, "Что-то не то с координатой Y конца кисти :(");
            }
        }
    }
}
