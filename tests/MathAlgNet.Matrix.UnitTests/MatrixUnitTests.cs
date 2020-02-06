using System;
using Xunit;

namespace MathAlgNet.Matrix.UnitTests
{
    public class MatrixUnitTests
    {
        [Fact]
        public void ThrowException_If_LinesNull()
        {
            // arrange
            string[] lines = null;

            // act
            Action act = () => new Matrix<int>(lines);

            // assert
            Exception ex = Assert.Throws<ArgumentException>(act);
        }

        [Fact]
        public void ThrowException_If_LinesContainLetter()
        {
            // arrange
            var lines = new[] { "1 2", "3 4", "5 a" };

            // act
            Action act = () => new Matrix<int>(lines);

            // assert
            Exception ex = Assert.Throws<FormatException>(act);
        }

        [Fact]
        public void ThrowException_If_Count_Collumns_Different()
        {
            // arrange
            var lines = new[] { "1 2", "3 4", "5 6 7" };

            // act
            Action act = () => new Matrix<int>(lines);

            // assert
            Exception ex = Assert.Throws<ArgumentException>(act);
        }


        [Fact]
        public void GetDimension()
        {
            // arrange
            var lines = new[] { "1 2", "3 4", "5 6" };

            // act
            var matrix = new Matrix<int>(lines);

            // assert
            Assert.Equal(3, matrix.Rows);
            Assert.Equal(2, matrix.Cols);
        }

        [Fact]
        public void CompareRank()
        {
            // arrange
            var matrix1 = new Matrix<int>(new[] { "1 2", "3 4", "5 6" });
            var matrix2 = new Matrix<int>(new[] { "1 2 3", "3 4 3", "5 6 3" });
            var matrix3 = new Matrix<int>(new[] { "2 1", "4 3" });

            // assert
            Assert.False(matrix1.DimensionEqual(matrix2));
            Assert.False(matrix1.DimensionEqual(matrix3));
            Assert.True(matrix1.DimensionEqual(matrix1));
        }

        [Fact]
        public void CanTranspose()
        {
            // arrange
            var matrix = new Matrix<int>(new[] { "1 2", "3 4", "5 6" });

            // act
            var transpose = matrix.Transpose();

            // assert
            Assert.Equal(1, transpose.Value[0, 0]);
            Assert.Equal(3, transpose.Value[0, 1]);
            Assert.Equal(5, transpose.Value[0, 2]);

            Assert.Equal(2, transpose.Rows);
            Assert.Equal(3, transpose.Cols);
        }

        [Fact]
        public void CanRectangleTranspose()
        {
            // arrange
            var matrix = new Matrix<int>(new[] { "1 2 3", "-4 5 6", "7 8 9" });

            // act
            var transpose = matrix.Transpose();

            // assert
            Assert.Equal(1, transpose.Value[0, 0]);
            Assert.Equal(-4, transpose.Value[0, 1]);
            Assert.Equal(7, transpose.Value[0, 2]);
        }

        [Fact]
        public void TestNonMatchedAdd()
        {
            // arrange
            var matrix = new Matrix<int>(new[] { "1 1 1", "1 1 1", "1 1 1" });
            var matrix2 = new Matrix<int>(new[] { "1 2 3 4", "5 6 7 8" });

            // act
            Action act = () => matrix.Add(matrix2);

            // assert
            Exception ex = Assert.Throws<ArgumentException>(act);
        }

        [Fact]
        public void TestAdd()
        {
            // arrange
            var matrix = new Matrix<int>(new[] { "1 2 3", "3 4 6" });
            var matrix2 = new Matrix<int>(new[] { "2 2 2", "1 2 3" });

            // act
            var result = matrix.Add(matrix2);

            // assert
            Assert.Equal(result.Value[0, 0], 1 + 2);
            Assert.Equal(result.Value[0, 1], 2 + 2);
            Assert.Equal(result.Value[0, 2], 3 + 2);

            Assert.Equal(result.Value[1, 0], 3 + 1);
            Assert.Equal(result.Value[1, 1], 4 + 2);
            Assert.Equal(result.Value[1, 2], 6 + 3);
        }

        [Fact]
        public void TestAddOperator()
        {
            // arrange
            var matrix = new Matrix<int>(new[] { "1 2 3", "3 4 6" });
            var matrix2 = new Matrix<int>(new[] { "2 2 2", "1 2 3" });

            // act
            var result = matrix + matrix2;

            // assert
            Assert.Equal(result.Value[0, 0], 1 + 2);
            Assert.Equal(result.Value[0, 1], 2 + 2);
            Assert.Equal(result.Value[0, 2], 3 + 2);

            Assert.Equal(result.Value[1, 0], 3 + 1);
            Assert.Equal(result.Value[1, 1], 4 + 2);
            Assert.Equal(result.Value[1, 2], 6 + 3);
        }

        [Fact]
        public void TestNonMatchedSub()
        {
            // arrange
            var matrix = new Matrix<int>(new[] { "1 1 1", "1 1 1", "1 1 1" });
            var matrix2 = new Matrix<int>(new[] { "1 2 3 4", "5 6 7 8" });

            // act
            Action act = () => matrix.Sub(matrix2);

            // assert
            Exception ex = Assert.Throws<ArgumentException>(act);
        }

        [Fact]
        public void TestSub()
        {
            // arrange
            var matrix = new Matrix<int>(new[] { "9 8 7", "6 5 4" });
            var matrix2 = new Matrix<int>(new[] { "3 2 1", "10 20 30" });

            // act

            var result = matrix.Sub(matrix2);

            // assert
            Assert.Equal(result.Value[0, 0], 9 - 3);
            Assert.Equal(result.Value[0, 1], 8 - 2);
            Assert.Equal(result.Value[0, 2], 7 - 1);

            Assert.Equal(result.Value[1, 0], 6 - 10);
            Assert.Equal(result.Value[1, 1], 5 - 20);
            Assert.Equal(result.Value[1, 2], 4 - 30);
        }

        [Fact]
        public void TestSubOperator()
        {
            // arrange
            var matrix = new Matrix<int>(new[] { "9 8 7", "6 5 4" });
            var matrix2 = new Matrix<int>(new[] { "3 2 1", "10 20 30" });

            // act

            var result = matrix - matrix2;

            // assert
            Assert.Equal(result.Value[0, 0], 9 - 3);
            Assert.Equal(result.Value[0, 1], 8 - 2);
            Assert.Equal(result.Value[0, 2], 7 - 1);

            Assert.Equal(result.Value[1, 0], 6 - 10);
            Assert.Equal(result.Value[1, 1], 5 - 20);
            Assert.Equal(result.Value[1, 2], 4 - 30);
        }

        [Fact]
        public void TestNonMatchedMultiply()
        {
            // arrange
            var matrix = new Matrix<int>(new[] { "1 1 1", "1 1 1", "1 1 1" });
            var matrix2 = new Matrix<int>(new[] { "1 2 3 4", "5 6 7 8" });

            // act
            Action act = () => matrix.Multiple(matrix2);

            // assert
            Exception ex = Assert.Throws<ArgumentException>(act);
        }

        [Fact]
        public void TestMultiply()
        {
            // arrange
            var matrix = new Matrix<int>(new[] { "1 2", "3 4", "5 6" });
            var matrix2 = new Matrix<int>(new[] { "1 2 3 4", "5 6 7 8" });

            // act
            var result = matrix.Multiple(matrix2);

            // assert
            Assert.Equal(result.Value[0, 0], 1 * 1 + 2 * 5);
            Assert.Equal(result.Value[0, 1], 1 * 2 + 2 * 6);
            Assert.Equal(result.Value[0, 2], 1 * 3 + 2 * 7);
            Assert.Equal(result.Value[0, 3], 1 * 4 + 2 * 8);

            Assert.Equal(result.Value[1, 0], 3 * 1 + 4 * 5);
            Assert.Equal(result.Value[1, 1], 3 * 2 + 4 * 6);
            Assert.Equal(result.Value[1, 2], 3 * 3 + 4 * 7);
            Assert.Equal(result.Value[1, 3], 3 * 4 + 4 * 8);

            Assert.Equal(result.Value[2, 0], 5 * 1 + 6 * 5);
            Assert.Equal(result.Value[2, 1], 5 * 2 + 6 * 6);
            Assert.Equal(result.Value[2, 2], 5 * 3 + 6 * 7);
            Assert.Equal(result.Value[2, 3], 5 * 4 + 6 * 8);
        }

        [Fact]
        public void TestMultiplyOperator()
        {
            // arrange
            var matrix = new Matrix<int>(new[] { "1 2", "3 4", "5 6" });
            var matrix2 = new Matrix<int>(new[] { "1 2 3 4", "5 6 7 8" });

            // act
            var result = matrix * matrix2;

            // assert
            Assert.Equal(result.Value[0, 0], 1 * 1 + 2 * 5);
            Assert.Equal(result.Value[0, 1], 1 * 2 + 2 * 6);
            Assert.Equal(result.Value[0, 2], 1 * 3 + 2 * 7);
            Assert.Equal(result.Value[0, 3], 1 * 4 + 2 * 8);

            Assert.Equal(result.Value[1, 0], 3 * 1 + 4 * 5);
            Assert.Equal(result.Value[1, 1], 3 * 2 + 4 * 6);
            Assert.Equal(result.Value[1, 2], 3 * 3 + 4 * 7);
            Assert.Equal(result.Value[1, 3], 3 * 4 + 4 * 8);

            Assert.Equal(result.Value[2, 0], 5 * 1 + 6 * 5);
            Assert.Equal(result.Value[2, 1], 5 * 2 + 6 * 6);
            Assert.Equal(result.Value[2, 2], 5 * 3 + 6 * 7);
            Assert.Equal(result.Value[2, 3], 5 * 4 + 6 * 8);
        }
    }
}
