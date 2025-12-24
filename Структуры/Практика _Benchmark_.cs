using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace StructBenchmarking;
public class Benchmark : IBenchmark
{
    public double MeasureDurationInMs(ITask task, int repetitionCount)
    {
        GC.Collect();
        GC.WaitForPendingFinalizers();

        task.Run();
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        for (int i = 0; i < repetitionCount; i++)
        {
            task.Run();
        }

        stopwatch.Stop();

        return stopwatch.Elapsed.TotalMilliseconds / repetitionCount;
    }

    public class StringBuilderTask : ITask
    {
        public void Run()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < 10000; i++)
            {
                sb.Append('a');
            }
            sb.ToString();
        }
	}

    public class StringConstructor : ITask
    {
        public void Run()
        {
            new string('a', 10000);
        }
    }
}

[TestFixture]
public class RealBenchmarkUsageSample
{
    [Test]
    public void StringConstructorFasterThanStringBuilder()
    {
        var benchmark = new Benchmark();
        var sbTask = new Benchmark.StringBuilderTask();
        var strTask = new Benchmark.StringConstructor();
        var sbDuration = benchmark.MeasureDurationInMs(sbTask, 10000);
        var strDuration = benchmark.MeasureDurationInMs(strTask, 10000);

        ClassicAssert.Less(strDuration, sbDuration);
    }
}
