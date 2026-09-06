using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace SuzerainSaveEditor;

public partial class RawSaveEntry : ObservableObject
{
    private readonly Action<RawSaveEntry, string> _valueChanged;

    public RawSaveEntry(string key, string value, Action<RawSaveEntry, string> valueChanged)
    {
        Key = key;
        _value = value;
        _valueChanged = valueChanged;
    }

    public string Key { get; }

    [ObservableProperty]
    private string _value;

    [ObservableProperty]
    private bool _isValid = true;

    partial void OnValueChanged(string value)
    {
        _valueChanged(this, value);
    }
}
