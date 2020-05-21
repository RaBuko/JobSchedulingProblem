using Solver.Methods;
using Xunit;
using Xunit.Abstractions;

namespace XUnitTestProject
{
    public class Test010 : BaseTest
    {
        public Test010(ITestOutputHelper outputHelper) : base(outputHelper, 10, 20) { }

        [Fact]
        public void BruteForceTest() =>
            TestMethod(new BruteForceMethod(), new BruteForceOptions() { Data = data, });


        [Fact]
        public void DynamicProgrammingTest()  =>
            TestMethod(new DynamicProgrammingMethod(), new DynamicProgrammingOptions() { Data = data, });

        [Fact]
        public void GeneticAlgorithmTest() =>
            TestMethod(new GeneticAlgorithmMethod(), new GeneticAlgorithmOptions()
            {
                Data = data,
                PopulationSize = 60,
                CrossoverRate = 0.2f,
                MutationRate = 0.8,
                IterationCount = 1000,
                SelectionMechanism = Solver.Utils.SelectionMechanismEnum.RouletteWheel,
            });

        [Fact]
        public void SimulatedAnnealingTest() =>
            TestMethod(new SimulatedAnnealingMethod(), new SimulatedAnnealingOptions()
            {
                Data = data,
                IterationCount = 2000,
            });

        [Fact]
        public void AlphaDominantGeneticTest() =>
           TestMethod(new AlphaDominantGeneticMethod(), new AlphaDominantGeneticOptions()
           {
               Data = data,
               IterationCount = 1000,
               MutationChance = 0.8f,
               PopulationSize = 60,
               CrossoverRate = 0.2f,
           });
    }
}
