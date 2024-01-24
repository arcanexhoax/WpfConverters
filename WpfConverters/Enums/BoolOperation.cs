namespace WpfConverters
{
    /// <summary>
    /// Boolean operation
    /// </summary>
    public enum BoolOperation
    {
        /// <summary>
        /// Unary bool operation: !
        /// </summary>
        Not,
        /// <summary>
        /// Binary bool operation: &&
        /// </summary>
        And,
        /// <summary>
        /// Binary bool operation: ||
        /// </summary>
        Or,
        /// <summary>
        /// Binary bool operation: ^
        /// </summary>
        Xor,
        /// <summary>
        /// Binary bool operation: ==
        /// </summary>
        Equals,
        /// <summary>
        /// Binary bool operation: !=
        /// </summary>
        NotEquals
    }
}
