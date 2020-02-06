using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathAlgNet.Matrix
{
    /// <summary>
    /// Класс расширения для вычисления матриц
    /// </summary>
    public static class MatrixOperations
    {
        /// <summary>
        /// Метод для сложения двух матриц
        /// </summary>
        /// <typeparam name="T">Тип значения матрицы</typeparam>
        /// <param name="leftMatrix">Первая слагаемая матрица</param>
        /// <param name="rightMatrix">Вторая слагаемая матрица</param>
        /// <returns>Результат сложения матриц</returns>
        public static Matrix<T> Add<T>(this Matrix<T> leftMatrix, Matrix<T> rightMatrix) where T : struct
        {
            if (!leftMatrix.DimensionEqual(rightMatrix))
                throw new ArgumentException("Размерность матриц должна быть одинаковая");


            var result = Enumerable.Range(0, leftMatrix.Rows)
                .Select(x => Enumerable.Range(0, leftMatrix.Cols)
                    .Select(y => TypeOperation<T>.Add(leftMatrix.Value[x, y], rightMatrix.Value[x, y]))
                    .ToArray())
                .ToArray();

            return new Matrix<T>(ArrayHelper.FromVectors(result));
        }

        /// <summary>
        /// Метод для вычитания матриц
        /// </summary>
        /// <typeparam name="T">Тип матрицы</typeparam>
        /// <param name="leftMatrix">Уменьшаемая матрица</param>
        /// <param name="rightMatrix">Вычитаемая матрица</param>
        /// <returns>Разность матриц</returns>
        public static Matrix<T> Sub<T>(this Matrix<T> leftMatrix, Matrix<T> rightMatrix) where T : struct
        {
            if (!leftMatrix.DimensionEqual(rightMatrix))
                throw new ArgumentException("Размерность матриц должна быть одинаковая");

            var result = Enumerable.Range(0, leftMatrix.Rows)
                .Select(x => Enumerable.Range(0, leftMatrix.Cols)
                    .Select(y => TypeOperation<T>.Sub(leftMatrix.Value[x, y], rightMatrix.Value[x, y]))
                    .ToArray())
                .ToArray();

            return new Matrix<T>(ArrayHelper.FromVectors(result));
        }

        /// <summary>
        /// Метод для произведения матриц
        /// </summary>
        /// <typeparam name="T">Тип матрицы</typeparam>
        /// <param name="leftMatrix">Матрица для первого множителя</param>
        /// <param name="rightMatrix">Матрица для второго множителя</param>
        /// <returns>Произведение матриц</returns>
        public static Matrix<T> Multiple<T>(this Matrix<T> leftMatrix, Matrix<T> rightMatrix) where T : struct
        {
            if (leftMatrix.Cols != rightMatrix.Rows)
                throw new ArgumentException("Матрицы должны быть согласованными");

            var left = ArrayHelper.ToVectors(leftMatrix.Value);
            var right = ArrayHelper.ToVectors(rightMatrix.Value);

            var result = left.Select((row, rowIndex) =>
                    right[0].Select((val1, columnIndex) =>
                            right.Select(val2 => val2[columnIndex])
                                .Zip(row, TypeOperation<T>.Multiplication)
                                .Aggregate(TypeOperation<T>.Add))
                        .ToArray())
                .ToArray();

            return new Matrix<T>(ArrayHelper.FromVectors(result));
        }

        /// <summary>
        /// Метод для транспонирования матрицы
        /// </summary>
        /// <typeparam name="T">Тип значения матрицы</typeparam>
        /// <param name="matrix">Исходная матрица</param>
        /// <returns>Транспонированая матрица</returns>
        public static Matrix<T> Transpose<T>(this Matrix<T> matrix) where T : struct
        {
            var result = new T[matrix.Cols, matrix.Rows];
            for (var i = 0; i < matrix.Rows; i++)
                for (var j = 0; j < matrix.Cols; j++)
                    result[j, i] = matrix.Value[i, j];

            return new Matrix<T>(result);
        }
    }
}
