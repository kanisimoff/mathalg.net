using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathAlgNet.Matrix
{
    /// <summary>
    /// Вспомогательный класс для работы с массивом
    /// </summary>
    public static class ArrayHelper
    {
        /// <summary>
        /// Преобразовать массив векторов в 2-х мерный массив
        /// </summary>
        /// <typeparam name="T">Тип вектора</typeparam>
        /// <param name="arrays">Массив векторов</param>
        /// <returns>2-х мерный массив</returns>
        public static T[,] FromVectors<T>(T[][] arrays)
        {
            var minorLength = arrays[0].Length;
            var result = new T[arrays.Length, minorLength];

            for (var i = 0; i < arrays.Length; i++)
            {
                var array = arrays[i];
                if (array.Length != minorLength)
                {
                    throw new ArgumentException("Строки матрицы должны быть одинаковой длины.");
                }
                for (int j = 0; j < minorLength; j++)
                {
                    result[i, j] = array[j];
                }
            }
            return result;
        }

        /// <summary>
        /// Преобразовать 2-х мерный массив в массив векторов
        /// </summary>
        /// <typeparam name="T">Тип 2-х мерного массива</typeparam>
        /// <param name="arrays">2-х массив</param>
        /// <returns>Массив векторов</returns>
        public static T[][] ToVectors<T>(T[,] arrays)
        {
            var rowCount = arrays.GetLength(0);
            var columnCount = arrays.GetLength(1);

            return Enumerable.Range(0, rowCount)
                .Select(x => Enumerable.Range(0, columnCount)
                    .Select(y => arrays[x, y])
                    .ToArray())
                .ToArray();
        }
    }
}
