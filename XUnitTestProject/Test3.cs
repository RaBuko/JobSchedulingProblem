using Solver.Methods;
using Xunit;
using Xunit.Abstractions;

namespace XUnitTestProject
{
    public class Test3 : BaseTest
    {
        public Test3(ITestOutputHelper outputHelper) : base(outputHelper, 3, 10) { }

        [Fact]
        public void BruteForceTest() =>
            TestMethod(new BruteForceMethod(), new BruteForceOptions() { Data = data, });

        [Fact]
        public void DynamicProgrammingTest() =>
            TestMethod(new DynamicProgrammingMethod(), new DynamicProgrammingOptions() { Data = data, });

        [Fact]
        public void GeneticAlgorithmTest() =>
            TestMethod(new GeneticAlgorithmMethod(), new GeneticAlgorithmOptions()
            {
                Data = data,
                ChromosomeCount = 100,
                CrossoverRate = 0.2,
                MutationRate = 0.8,
                NumberOfIterations = 1000,
                SelectionMechanism = Solver.Utils.SelectionMechanismEnum.RouletteWheel,
            });

        [Fact]
        public void SimulatedAnnealingTest() =>
            TestMethod(new SimulatedAnnealingMethod(), new SimulatedAnnealingOptions()
            {
                Data = data,
                IterationCount = 2000,
            });
    }
}
