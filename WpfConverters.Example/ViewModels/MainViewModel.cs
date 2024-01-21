using Prism.Mvvm;

namespace WpfConverters.Example.ViewModels
{
    internal class MainViewModel : BindableBase
    {
        private double _width;
        private bool _boolValue;

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

        public MainViewModel()
        {
            Width = 100;
            BoolValue = true;
        }
    }
}
