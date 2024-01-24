using System;

namespace WpfConverters.Converters
{
    public class BoolToConverterBase<T> : ConverterBase
    {
        /// <summary>
        /// The <see cref="T"/> behavior when binding value is <see langword="true"/>.
        /// </summary>
        public T ForTrue { get; set; }

        /// <summary>
        /// The <see cref="T"/> behavior when binding value is <see langword="false"/>.
        /// </summary>
        public T ForFalse { get; set; }

        internal BoolToConverterBase()
        {
        }

        /// <summary>
        /// Base bool to <see cref="T"/> converter. 
        /// </summary>
        /// <param name="defaultTrue">Default value of <see cref="ForTrue"/> property</param>
        /// <param name="defaultFalse">Default value of <see cref="ForFalse"/> property</param>
        internal BoolToConverterBase(T defaultTrue, T defaultFalse)
        {
            ForTrue = defaultTrue;
            ForFalse = defaultFalse;
        }

        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool input = System.Convert.ToBoolean(value);
            T result = input ? ForTrue : ForFalse;

            return ConvertNextIfNeeded(result);
        }
    }
}
