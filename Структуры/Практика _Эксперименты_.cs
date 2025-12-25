using System.Collections.Generic;

namespace StructBenchmarking;

public class Experiments
{
    public interface IFactory
    {
        ITask CreateClass(int size);
        ITask CreateStruct(int size);
    }

    public class Arrays : IFactory
    {
        public ITask CreateClass(int size)
        {
            return new ClassArrayCreationTask(size);
        }

        public ITask CreateStruct(int size)
        {
            return new StructArrayCreationTask(size);
        }
    }

    public class Methods : IFactory
    {
        public ITask CreateClass(int size)
        {
            return new MethodCallWithClassArgumentTask(size);
        }

        public ITask CreateStruct(int size)
        {
            return new MethodCallWithStructArgumentTask(size);
        }
    }

    public static ChartData BuildChartDataForArrayCreation(IBenchmark benchmark, int reps)
    {
        return Run(benchmark, reps, new Arrays(), "Create array");
    }

    public static ChartData BuildChartDataForMethodCall(IBenchmark benchmark, int reps)
    {
        return Run(benchmark, reps, new Methods(), "Call method with argument");
    }

    public static ChartData Run(IBenchmark benchmark, int reps, IFactory factory, string title)
    {
        var classResults = new List<ExperimentResult>();
        var structResults = new List<ExperimentResult>();

        foreach (var size in Constants.FieldCounts)
        {
            var task1 = factory.CreateClass(size);
            var time1 = benchmark.MeasureDurationInMs(task1, reps);
            classResults.Add(new ExperimentResult(size, time1));

            var task2 = factory.CreateStruct(size);
            var time2 = benchmark.MeasureDurationInMs(task2, reps);
            structResults.Add(new ExperimentResult(size, time2));
        }

        return new ChartData
        {
            Title = title,
            ClassPoints = classResults,
            StructPoints = structResults,
        };
    }
}
