using System;
using System.Globalization;
using System.Linq;
using WpfConverters.Converters;

namespace WpfConverters
{
    public class StringCaseConverter : ConverterBase
    {
        /// <summary>
        /// Case convert operation. Default is <see cref="CaseOperation.ToUpper"/>.
        /// </summary>
        public CaseOperation Operation { get; set; } = CaseOperation.ToUpper;

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string input = targetType == typeof(string) ? (string)value : value.ToString();

            string result = Operation switch
            {
                CaseOperation.ToUpperFirstLetterAndIgnoreOther  => char.ToUpper(input[0]) + input[1..],
                CaseOperation.ToUpperFirstLetterAndToLowerOther => char.ToUpper(input[0]) + input[1..].ToLower(),
                CaseOperation.ToLower                           => input.ToLower(),
                CaseOperation.Invert                            => string.Concat(input.Select(c => char.IsUpper(c) ? char.ToLower(c) : char.ToUpper(c))),
                _                                               => input.ToUpper(),
            };

            return ConvertNextIfNeeded(result);
        }
    }
}
