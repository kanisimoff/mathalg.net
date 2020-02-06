using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Defaults;
using LiveCharts.Definitions.Series;
using LiveCharts.Wpf;
using MathAlgNet.Matrix;
using MathAlgNet.Matrix.Methods;
using Microsoft.CodeAnalysis.CSharp.Scripting;

namespace MathAlgNet.Viewer.ViewModel
{
    public class SquareMethodViewModel
    {
        public int NodesCount { get; set; }

        public int PolynomeDegree { get; set; }

        public ChartValues<ObservablePoint> Points { get; set; }

        public SeriesCollection SeriesCollection { get; set; }

        public SquareMethodViewModel()
        {
            PolynomeDegree = 2;

            Points = new ChartValues<ObservablePoint>
            {
                new ObservablePoint() {X = 0.75, Y = 2.5},
                new ObservablePoint() {X = 1.5, Y = 1.2},
                new ObservablePoint() {X = 2.25, Y = 1.12},
                new ObservablePoint() {X = 3.0, Y = 2.25},
                new ObservablePoint() {X = 3.75, Y = 4.28}
            };

            var mapper = Mappers.Xy<ObservablePoint>().X(v => v.X).Y(v => v.Y);
            SeriesCollection = new SeriesCollection(mapper)
            {
                new ScatterSeries()
                {
                    Title = "FAM - сырые данные",
                    Values = Points,
                    PointGeometry = DefaultGeometries.Circle,
                    MaxPointShapeDiameter = 5.0
                }
            };
        }

        public void Evalute()
        {
            var rawMatrix = new Matrix<double>(ArrayHelper.FromVectors(Points.Select(v => new[] {v.X, v.Y}).ToArray())).Transpose();
            int basis = PolynomeDegree + 1;
            var matrix = LeastSquares.MakeSystem(rawMatrix.Value, basis);

            Console.WriteLine("---\nСоставляем систему уравнений:\n");
            for (int i = 0; i < basis; i++)
            {
                for (int j = 0; j < basis; j++)
                {
                    Console.Write((matrix[i, j] > 0 ? "+" : "") +
                                              Math.Round(matrix[i, j], 3) + "*c" + j.ToString() + " ");
                }
                Console.WriteLine(" = " + matrix[i, basis]);
            }

            var result = LeastSquares.Gauss(matrix, basis, basis + 1);
            if (result == null)
            {
                Console.WriteLine("Невозможно найти частное решение составленной системы уравнений\n");
                return;
            }
            Console.WriteLine("Решение системы уравнений:\n");
            for (int i = 0; i < basis; i++)
            {
                Console.WriteLine("C" + i + " = " + Math.Round(result[i], 3));
            }
            Console.WriteLine("Таким образом, среднеквадратичное приближение:");
            var func = new StringBuilder();
            for (int i = 0; i < basis; i++)
            {
                if (Math.Round(result[i], 3) != 0)
                {
                    func.Append(((result[i] > 0) ? "+" : "") +
                                Math.Round(result[i], 3) + ((i > 0) ? "*x^" + i : "") + " ");
                }
            }
            Console.WriteLine($"y = {func.ToString()}");
            Console.WriteLine();

            // Строим график
            var min = Points.Select(p => p.X).Min();
            var max = Points.Select(p => p.X).Max();
            var step = (max - min) / 20;

            var newPlot = new ChartValues<ObservablePoint>();
            var xi = min;
            do
            {
                double y = 0;
                for (int i = 0; i < basis; i++)
                {
                    y += result[i] * Math.Pow(xi, i);
                }
                newPlot.Add(new ObservablePoint(){X = xi, Y = y});
                xi += step;
            } while (xi <= max);

            // Применяем график
            var gr = new LineSeries
            {
                Title = "F(x)",
                Values = newPlot,
                LineSmoothness = 0,
                PointGeometry = DefaultGeometries.Cross,
                PointGeometrySize = 5.0
            };
            SeriesCollection.Add(gr);
        }

        private List<double> getChartValues()
        {
            var data = "2539,25;2530,948;2536,583;2544,302;2551,521;2554,979;2556,823;2555,802;2557,906;2553,438;2558,781;2555,708;2556,771;2554,167;2559,167;2561,344;2558,948;2564,115;2560,177;2562,646;2561,708;2558,594;2568,635;2563,75;2568,813;2566,646;2566,625;2568,698;2570,198;2575,26;2575,542;2589,333;2609,281;2644,604;2704,948;2800,635;2917,531;3037,354;3138,792;3214,177;3267,063;3286,042;3304,354;3316,917;3319,531"
                .Split(";")
                .Select(double.Parse).ToList();

            var res = new List<double>(data);
            return res;
        }
    }
}
