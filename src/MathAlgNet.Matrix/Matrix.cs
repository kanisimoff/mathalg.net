using System;
using System.Linq;

namespace MathAlgNet.Matrix
{
    /// <summary>
    /// Класс определения матрицы
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public struct Matrix<T> where T : struct
    {
        private readonly T[,] _matrix;

        public Matrix(T[,] matrix)
        {
            _matrix = matrix;
        }

        public Matrix(string[] lines)
        {
            if (lines == null || !lines.Any())
                throw new ArgumentException("Массив строк не определен.");

            try
            {
                var vectorArray = lines
                    .Select(x => x.Trim()
                            .Split(' ')
                            .Select(singleVal => (T)Convert.ChangeType(singleVal, typeof(T)))
                            .ToArray()
                    )
                    .ToArray();
                _matrix = ArrayHelper.FromVectors(vectorArray);
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        /// <summary>
        /// Количество строк
        /// </summary>
        public int Rows => _matrix.GetLength(0);

        /// <summary>
        /// Количество столбцов
        /// </summary>
        public int Cols => _matrix.GetLength(1);

        /// <summary>
        /// Представление матрицы в виде массива
        /// </summary>
        public T[,] Value => _matrix;

        /// <summary>
        /// Метод сравнивает размеры матриц
        /// </summary>
        /// <param name="compare">Матрица для сравнения</param>
        /// <returns>Результат сравнения размеров матриц</returns>
        public bool DimensionEqual(Matrix<T> compare)
        {
            return Rows == compare.Rows && Cols == compare.Cols;
        }

        /// <summary>
        /// Сложения матриц
        /// </summary>
        /// <typeparam name="T">Тип значения матрицы</typeparam>
        /// <param name="leftMatrix">Первая слагаемая матрица</param>
        /// <param name="rightMatrix">Вторая слагаемая матрица</param>
        /// <returns>Результат сложения матриц</returns>
        public static Matrix<T> operator +(Matrix<T> leftMatrix, Matrix<T> rightMatrix) => leftMatrix.Add(rightMatrix);

        /// <summary>
        /// Вычитание матриц
        /// </summary>
        /// <typeparam name="T">Тип матрицы</typeparam>
        /// <param name="leftMatrix">Уменьшаемая матрица</param>
        /// <param name="rightMatrix">Вычитаемая матрица</param>
        /// <returns>Разность матриц</returns>
        public static Matrix<T> operator -(Matrix<T> leftMatrix, Matrix<T> rightMatrix) => leftMatrix.Sub(rightMatrix);

        /// <summary>
        /// Метод для произведения матриц
        /// </summary>
        /// <typeparam name="T">Тип матрицы</typeparam>
        /// <param name="leftMatrix">Матрица для первого множителя</param>
        /// <param name="rightMatrix">Матрица для второго множителя</param>
        /// <returns>Произведение матриц</returns>
        public static Matrix<T> operator *(Matrix<T> leftMatrix, Matrix<T> rightMatrix) => leftMatrix.Multiple(rightMatrix);

        public override string ToString()
        {
            var matrix = _matrix;
            return string.Join(Environment.NewLine,
                matrix.OfType<T>()
                    .Select((value, index) => new { value, index })
                    .GroupBy(x => x.index / matrix.GetLength(1))
                    .Select(x => $"{string.Join(" ", x.Select(y => y.value))}"));
        }
    }
}
