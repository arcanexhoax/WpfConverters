namespace Hoax.WpfConverters.Utils
{
    internal static class ObjectExtensions
    {
        public static bool IsNumber(this object value)
        {
            return value is int
                || value is double
                || value is float
                || value is long
                || value is byte
                || value is uint
                || value is ulong
                || value is short
                || value is decimal
                || value is sbyte
                || value is ushort;
        }
    }
}
