using McGurkin.Collections;

namespace McGurkin.Test.Collections
{
    [TestClass]
    public class CollectionTests
    {
        [TestMethod]
        public void Permute_ShouldReturnCorrectPermutations()
        {
            // Arrange
            var list = new List<int> { 1, 2, 3 };
            var expectedPermutations = new List<List<int>>
            {
                new List<int> { 1, 2 },
                new List<int> { 1, 3 },
                new List<int> { 2, 1 },
                new List<int> { 2, 3 },
                new List<int> { 3, 1 },
                new List<int> { 3, 2 }
            };

            // Act
            var permutations = PermuteUtilities.Permute(list, 2).ToList();

            // Assert
            Assert.AreEqual(expectedPermutations.Count, permutations.Count);
            foreach (var permutation in expectedPermutations)
            {
                Assert.IsTrue(permutations.Any(p => p.SequenceEqual(permutation)));
            }
        }

        [TestMethod]
        public void Concat_ShouldReturnConcatenatedList()
        {
            // Arrange
            var listA = new List<int> { 1, 2, 3 };
            var listB = new List<int> { 4, 5, 6 };
            var expectedConcatenation = new List<int> { 1, 2, 3, 4, 5, 6 };

            // Act
            var concatenatedList = PermuteUtilities.Concat(listA, listB).ToList();

            // Assert
            Assert.AreEqual(expectedConcatenation.Count, concatenatedList.Count);
            Assert.IsTrue(expectedConcatenation.SequenceEqual(concatenatedList));
        }

        [TestMethod]
        public void AllExcept_ShouldReturnListWithoutSpecifiedIndex()
        {
            // Arrange
            var list = new List<int> { 1, 2, 3, 4, 5 };
            var indexToSkip = 2;
            var expectedList = new List<int> { 1, 2, 4, 5 };

            // Act
            var resultList = PermuteUtilities.AllExcept(list, indexToSkip).ToList();

            // Assert
            Assert.AreEqual(expectedList.Count, resultList.Count);
            Assert.IsTrue(expectedList.SequenceEqual(resultList));
        }
    }
}
