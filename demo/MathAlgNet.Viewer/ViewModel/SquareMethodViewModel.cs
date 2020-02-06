using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Defaults;
using LiveCharts.Definitions.Series;
using LiveCharts.Wpf;
using MathAlgNet.Matrix;
using MathAlgNet.Matrix.Methods;

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
            var matrix = new Matrix<double>(ArrayHelper.FromVectors(Points.Select(v => new[] {v.X, v.Y}).ToArray())).Transpose();
            var t= LeastSquares.MakeSystem(matrix.Value, PolynomeDegree+1);
        }

        private ChartValues<double> getChartValues()
        {
            return new ChartValues<double>((IEnumerable<double>)((IEnumerable<string>)"2539,25;2530,948;2536,583;2544,302;2551,521;2554,979;2556,823;2555,802;2557,906;2553,438;2558,781;2555,708;2556,771;2554,167;2559,167;2561,344;2558,948;2564,115;2560,177;2562,646;2561,708;2558,594;2568,635;2563,75;2568,813;2566,646;2566,625;2568,698;2570,198;2575,26;2575,542;2589,333;2609,281;2644,604;2704,948;2800,635;2917,531;3037,354;3138,792;3214,177;3267,063;3286,042;3304,354;3316,917;3319,531".Split(";", StringSplitOptions.None)).Select<string, double>(new Func<string, double>(double.Parse)).ToList<double>());
        }
    }
}
