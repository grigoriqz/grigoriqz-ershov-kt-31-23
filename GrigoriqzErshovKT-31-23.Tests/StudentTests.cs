using GrigoriqzErshovKt_31_23.Models;

namespace GrigoriqzErshovKT_31_23.Tests
{
    public class StudentTests
    {
        [Fact]
        public void GetFio_IvanovIvan_IvanovIvan()
        {
            // Arrange
            var testStudent = new Student
            {
                FirstName = "Иван",
                LastName = "Иванов"
            };

            // Act
            var result = testStudent.GetFio();

            // Assert
            Assert.Equal("Иванов Иван", result);
        }
    }
}
