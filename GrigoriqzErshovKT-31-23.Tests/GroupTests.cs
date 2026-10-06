using GrigoriqzErshovKt_31_23.Models;

namespace GrigoriqzErshovKT_31_23.Tests
{
    public class GroupTests
    {
        [Fact]
        public void IsValidGroupName_KT3123_True()
        {
            // Arrange
            var testGroup = new Group
            {
                Name = "KT-31-23"
            };

            // Act
            var result = testGroup.IsValidGroupName();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsValidGroupName_gr1_False()
        {
            // Arrange
            var testGroup = new Group
            {
                Name = "гр1"
            };

            // Act
            var result = testGroup.IsValidGroupName();

            // Assert
            Assert.False(result);
        }
    }
}
