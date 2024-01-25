namespace Hoax.WpfConverters
{
    /// <summary>
    /// Specifies a behavior is case when given value is null.
    /// </summary>
    public enum NullHanding
    {
        /// <summary>
        /// Return null.
        /// </summary>
        ReturnNull,
        /// <summary>
        /// Return "Null".
        /// </summary>
        ReturnNullAsString,
        /// <summary>
        /// Return "".
        /// </summary>
        ReturnEmptyString
    }
}
