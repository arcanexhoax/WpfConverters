using Prism.Mvvm;

namespace WpfConverters.Example.ViewModels
{
    internal class MainViewModel : BindableBase
    {
        private double _width;

        public double Width
        {
            get => _width;
            set => SetProperty(ref _width, value);
        }

        public MainViewModel()
        {
            Width = 100;
        }
    }
}
