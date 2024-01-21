using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace WpfConverters.Converters
{
    /// <summary>
    /// Describes a math operation between specified operands
    /// </summary>
    public enum MathOperation
    {
        Addition,
        Subtraction,
        Multiply,
        Division,
        /// <summary>
        /// Use only first 2 specified operands
        /// </summary>
        Power,
        Mod,
        /// <summary>
        /// Use only first specified operand
        /// </summary>
        Absolute,
        Max,
        Min,
        /// <summary>
        /// Use only first specified operand
        /// </summary>
        Sqrt,
        /// <summary>
        /// Use only first 2 specified operands. The second operand is a number of digits
        /// </summary>
        Round,
        Increment,
        Decrement
    }

    [ValueConversion(typeof(double), typeof(double))]
    public class MathConverter : ConverterBase
    {
        /// <summary>
        /// Second operand for a math operation.
        /// </summary>
        public double? Operand { get; set; }

        /// <summary>
        /// Additional operands for a math operation. Use only if <see cref="Operand"/> isn't specified.
        /// </summary>
        public double[] Operands { get; set; }

        /// <summary>
        /// A math operation between specified operands.
        /// </summary>
        public MathOperation Operation { get; set; }

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double firstOp = System.Convert.ToDouble(value);
            List<double> values;

            if (Operand.HasValue)
                values = [firstOp, Operand.Value];
            else if (Operands is not null && Operands.Length > 0)
                values = [firstOp, ..Operands];
            else
                values = [firstOp];

            double result = Operation switch
            {
                MathOperation.Subtraction => OperateBinary(values, (a, b) => a - b),
                MathOperation.Multiply    => OperateBinary(values, (a, b) => a * b),
                MathOperation.Division    => OperateBinary(values, (a, b) => a / b),
                MathOperation.Mod         => OperateBinary(values, (a, b) => a % b),
                MathOperation.Power       => OperateBinary(values, Math.Pow),
                MathOperation.Round       => Round(values),
                MathOperation.Max         => values.Max(),
                MathOperation.Min         => values.Min(),
                MathOperation.Absolute    => Math.Abs(values[0]),
                MathOperation.Sqrt        => Math.Sqrt(values[0]),
                MathOperation.Increment   => ++values[0],
                MathOperation.Decrement   => --values[0],
                _                         => OperateBinary(values, (a, b) => a + b),
            };

            return ConvertNextIfNeeded(result);
        }

        private double OperateBinary(List<double> values, Func<double, double, double> func)
        {
            if (values.Count < 2)
                throw new ArgumentException($"Given operands: {values.Count}, need: 2.");

            double buffer = values[0];

            for (int i = 1; i < values.Count; i++)
            {
                buffer = func(buffer, values[i]);
            }

            return buffer;
        }

        private double Round(List<double> values)
        {
            if (values.Count < 2)
                throw new ArgumentException($"Given operands: {values.Count}, need: 2.");

            return Math.Round(values[0], (int)values[1]);
        }
    }
}
