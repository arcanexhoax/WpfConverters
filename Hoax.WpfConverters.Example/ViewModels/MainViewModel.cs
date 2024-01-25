using Prism.Mvvm;
using System.Collections.ObjectModel;

namespace WpfConverters.Example.ViewModels
{
    internal class MainViewModel : BindableBase
    {
        private double _width;
        private bool _boolValue;
        private Collection<int>? _numbers;

        public double Width
        {
            get => _width;
            set => SetProperty(ref _width, value);
        }

        public bool BoolValue
        {
            get => _boolValue;
            set => SetProperty(ref _boolValue, value);
        }

        public Collection<int>? Numbers
        {
            get => _numbers;
            set => SetProperty(ref _numbers, value);
        }

        public MainViewModel()
        {
            Width = 100;
            BoolValue = true;
            Numbers = [10, 5, 33, 58];
        }
    }
}
