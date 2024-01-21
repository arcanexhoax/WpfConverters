using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfConverters.Converters
{
    /// <summary>
    /// Case convert operation.
    /// </summary>
    public enum CaseOperation
    {
        /// <summary>
        /// Convert the whole string to the upper case ("string" => "STRING").
        /// </summary>
        ToUpper,
        /// <summary>
        /// Convert only first letter to the upper case and ignore other ("string" => "String").
        /// </summary>
        ToUpperFirstLetterAndIgnoreOther,
        /// <summary>
        /// Convert first letter to the upper case and other to the lower case ("sTrInG" => "String").
        /// </summary>
        ToUpperFirstLetterAndToLowerOther,
        /// <summary>
        /// Convert the whole string to the lower case ("STRING" => "string").
        /// </summary>
        ToLower,
        /// <summary>
        /// Convert every character of the string to the opposite case ("sTrInG" => "StRiNg").
        /// </summary>
        Invert,
    }

    public class StringCaseConverter : ConverterBase
    {
        /// <summary>
        /// Case convert operation.
        /// </summary>
        public CaseOperation Operation { get; set; }

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
