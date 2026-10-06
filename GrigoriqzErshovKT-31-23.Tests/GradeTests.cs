using GrigoriqzErshovKt_31_23.Models;

namespace GrigoriqzErshovKT_31_23.Tests
{
    public class GradeTests
    {
        [Fact]
        public void IsPassed_Value5_True()
        {
            // Arrange
            var testGrade = new Grade
            {
                Value = 5
            };

            // Act
            var result = testGrade.IsPassed();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsPassed_Value2_False()
        {
            // Arrange
            var testGrade = new Grade
            {
                Value = 2
            };

            // Act
            var result = testGrade.IsPassed();

            // Assert
            Assert.False(result);
        }
    }
}
