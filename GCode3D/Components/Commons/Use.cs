namespace GCode3D.Components;

public class Use<T>(T? initialValue = default) : StandardComponent
{
    private T? _Current = initialValue;
    private T? Current
    {
        get => _Current;
        set => Set(ref _Current, value);
    }

    // Implicit Conversion to T
    public static implicit operator T?(Use<T> use) => 
        use.Current;

    // Implicit Conversion from T
    public static implicit operator Use<T>(T? value) => 
        new(value);
}