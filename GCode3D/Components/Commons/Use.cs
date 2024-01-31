using System.Collections;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace GCode3D.Components
{
    public class Use<T> : ObservableObject
    {
        private T? _value;
        public T? Value
        {
            get => _value;
            set => SetProperty(ref _value, value);
        }

        public static (T?, Action<Func<T?, T?>>) New(T? initialValue) =>
            typeof(T) is IEnumerable ?
                UseCollection<T>.New(initialValue) :
                UseSingle<T>.New(initialValue);
    }

    public class UseSingle<T> : ObservableObject
    {
        private T? _value;
        public T? Value
        {
            get => _value;
            set => SetProperty(ref _value, value);
        }

        public static (T?, Action<Func<T?, T?>>) New(T? initialValue)
        {
            var _instance =
                new UseSingle<T?>()
                {
                    _value = initialValue
                };
            var setter = new RelayCommand<T?>(x => _instance.Value = x);

            return (
                _instance.Value, 
                (x) => setter.Execute(x(_instance.Value))
            );
        }
    }

    public class UseCollection<T> : ObservableObject
    {
        private T? _value;
        public T? Value
        {
            get => _value;
            set => SetProperty(ref _value, value);
        }

        public static (T?, Action<Func<T?, T?>>) New(T? initialValue)
        {
            var _instance =
                new UseCollection<T?>()
                {
                    _value = initialValue
                };
            var setter = new RelayCommand<T?>(x => _instance.Value = x);

            return (
                _instance.Value, 
                (x) => setter.Execute(x(_instance.Value))
            );
        }
    }
}
