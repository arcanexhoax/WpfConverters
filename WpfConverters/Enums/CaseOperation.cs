namespace Hoax.WpfConverters
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
}
