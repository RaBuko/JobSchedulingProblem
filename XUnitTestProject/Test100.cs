using Solver.Methods;
using Xunit;
using Xunit.Abstractions;

namespace XUnitTestProject
{
    public class Test100 : BaseTest
    {
        public Test100(ITestOutputHelper outputHelper) : base(outputHelper, 100, 20) { }

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
