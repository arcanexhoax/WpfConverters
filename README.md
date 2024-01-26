## WPF Converters
![NET](https://img.shields.io/badge/.NET%206-%23512BD4)
[![Version](https://img.shields.io/nuget/vpre/Hoax.WpfConverters.svg?label=NuGet)](https://www.nuget.org/packages/Hoax.WpfConverters)
[![License](https://img.shields.io/github/license/arcanexhoax/WpfConverters.svg?color=00b542&label=License)](https://raw.githubusercontent.com/arcanexhoax/WpfConverters/master/LICENSE)

The ultimate converters pack for WPF, providing enough simple and flexible converters for all occasions.

## Available converters

### Boolean

- **BoolConverter** - Returns the result of the specified operation between 2 *bool*.
- **BoolToObjectConverter** - Returns the specified *object* according to the *bool* value.
- **BoolToStringConverter** - Returns the specified *string* according to the *bool* value.
- **BoolToDoubleConverter** - Returns the specified *double* value according to the *bool* value.
- **BoolToOpacityConverter** - Returns the specified *Opacity* value according to the *bool* value.
- **BoolToVisibilityConverter** - Returns the specified *Visibility* value according to the *bool* value.
- **BoolToThicknessConverter** - Returns the specified *Thickness* according to the *bool* value.
- **BoolToBrushConverter** - Returns the specified *Brush* according to the *bool* value.

### Numbers

- **MathConverter** - Performs the specified math operation between 2 *double* numbers.
- **NumberComparisonConverter** - Compares 2 *double* numbers using the specified operation.

### String

- **StringCaseConverter** - Converts given *string* to a new one with the specified case.
- **StringComparisonConverter** - Compares 2 *strings* using the specified operation.
- **StringFormatConverter** - Formats given *string* with the specified argument(-s).

### Object

- **ObjectToStringConverter** - Returns a *string* of the specified *object*.
- **ObjectComparisonConverter** - Compares 2 *objects* using the specified operation.
- **TypeConverter** - Changes a type of the given *object* to the specified one.

### Collection

- **CollectionToStringConverter** - Converts given *Collection* to the *string* using specified separator.
- **CollectionToCountConverter** - Returns the count of the given *Collection*.
- **CollectionToItemConverter** - Returns an item of the given *Collection* according to the specified its index.

### UI

- **BrushToColorConverter** - Returns a *Color* of the specified *SolidColorBrush*.
- **ColorToBrushConverter** - Converts the given *Color* to the *SolidColorBrush*.
- **VisibilityToBoolConverter** - Returns the specified *bool* value according to the *Visibility* value.
- **UrlToImage** - Converts the given url to the *BitmapImage*. 

## How to use

### Installation

First of all install package using *.NET CLI*:
```batchfile
dotnet add package Hoax.WpfConverters --version 0.9.0
```
or add the following string to the *.csproj* file:
```xml
<PackageReference Include="Hoax.WpfConverters" Version="0.9.0" />
```

### Adding namespace

Add the namespace to your XAML header:
```xml
xmlns:c="clr-namespace:Hoax.WpfConverters;assembly=Hoax.WpfConverters"
```

### Simple example

Using `BoolToVisibilityConverter` with custom behavior. If the binding value is `true`, the border will be `Hidden`, otherwise `Visible`. 
```xml
<Border Visibility="{Binding BoolValue, 
                     Converter={c:BoolToVisibilityConverter ForTrue=Hidden, ForFalse=Visible}}" />
```
or you can add a converter to resources which is a more effiecient way to use the converter.
```xml
<Window.Resources>
    <c:BoolToVisibilityConverter x:Key="BoolToVis" ForTrue="Hidden" ForFalse="Visible" />
</Window.Resources>

<Border Visibility="{Binding BoolValue, Converter={StaticResource BoolToVis}}" />
```

### Converters chain

Each converter has a `Then` property that takes an `IValueConverter` value. The next converter will use the result of the previous one. You can build as long a chain of converters as you like.
<br>This example shows a chain of 3 operations. 
1. `true` => `false`. The `IsEnabled` property of the button is `true`, so it becomes `false` after the `NOT` operation. 
2. `false` => `False is the value after the NOT operation`. Formats `false` with given pattern. 
**Tip:** a string value of `false` is `"False"`. If the pattern starts with `{0}` you should add `{}` at the beginning of the string. But if the pattern binds from a C# code, you don't need to add that.
3. `False is the value after the NOT operation` => `fALSE IS THE VALUE AFTER THE not OPERATION`. Inverts the case of the given string and set it to the Text property.

<br>**Note**. You can use your own converters as a next converter, but if that converter doesn't extend the `Hoax.WpfConveters.Base.ConverterBase` class, it won't be able to continue the chain and will be the last link.
```xml
<Button x:Name="btn" 
        IsEnabled="True" />

<TextBlock Text="{Binding IsEnabled, ElementName=btn, 
                  Converter={c:BoolConverter Operation=Not, 
                  Then={c:StringFormatConverter Format='{}{0} is the value after the NOT operation',
                  Then={c:StringCaseConverter Operation=Invert}}}}" />
```