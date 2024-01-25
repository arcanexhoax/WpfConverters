namespace Hoax.WpfConverters
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
}
