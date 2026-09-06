using System;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;

namespace SuzerainSaveEditor;

public partial class RawSaveViewModel : BaseViewModel
{
    private readonly ObservableCollection<RawSaveEntry> _allEntries = new();
    private bool _updatingEntries;
    private Dictionary<string, object>? _entriesDocument;

    public RawSaveViewModel(SaveGameState saveGameState) : base(saveGameState)
    {
    }

    public ObservableCollection<RawSaveEntry> FilteredEntries { get; } = new();

    [ObservableProperty]
    private string _searchText = string.Empty;

    partial void OnSearchTextChanged(string value)
    {
        RefreshFilteredEntries();
    }

    protected override void LoadDataToProperties()
    {
        if (_saveData == null)
        {
            return;
        }

        _updatingEntries = true;
        try
        {
            if (!ReferenceEquals(_entriesDocument, _saveData))
            {
                _allEntries.Clear();
                foreach (var pair in _saveData)
                {
                    _allEntries.Add(new RawSaveEntry(pair.Key, FormatValue(pair.Value), OnEntryValueChanged));
                }
                _entriesDocument = _saveData;
            }
            else
            {
                foreach (var entry in _allEntries)
                {
                    if (_saveData.TryGetValue(entry.Key, out var value))
                    {
                        entry.Value = FormatValue(value);
                        entry.IsValid = true;
                    }
                }
            }

            RefreshFilteredEntries();
        }
        finally
        {
            _updatingEntries = false;
        }
    }

    protected override void SavePropertiesToData()
    {
        // Raw entries update the shared dictionary as soon as their text is valid.
    }

    private void OnEntryValueChanged(RawSaveEntry entry, string value)
    {
        if (_updatingEntries || _saveData == null)
        {
            return;
        }

        if (!_saveGameService.TryParsePrimitiveValue(value, out var parsedValue))
        {
            entry.IsValid = false;
            return;
        }

        entry.IsValid = true;
        _saveData[entry.Key] = parsedValue;
        _saveGameState.NotifyDataChanged();
    }

    private void RefreshFilteredEntries()
    {
        var search = SearchText.Trim();
        var entries = string.IsNullOrEmpty(search)
            ? _allEntries
            : _allEntries.Where(entry => entry.Key.Contains(search, StringComparison.OrdinalIgnoreCase));

        FilteredEntries.Clear();
        foreach (var entry in entries)
        {
            FilteredEntries.Add(entry);
        }
    }

    private static string FormatValue(object value)
    {
        return value switch
        {
            bool boolValue => boolValue ? "true" : "false",
            double doubleValue => doubleValue.ToString(System.Globalization.CultureInfo.InvariantCulture),
            _ => value.ToString() ?? string.Empty
        };
    }
}
