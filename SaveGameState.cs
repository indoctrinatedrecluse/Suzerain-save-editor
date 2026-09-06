using System;
using System.Collections.Generic;

namespace SuzerainSaveEditor;

public sealed class SaveGameState
{
    public string? LoadedFilePath { get; private set; }
    public string? OriginalContent { get; private set; }
    public Dictionary<string, object>? SaveData { get; private set; }

    public event EventHandler? DataChanged;

    public void SetDocument(string filePath, string content, Dictionary<string, object> data)
    {
        LoadedFilePath = filePath;
        OriginalContent = content;
        SaveData = data;
        DataChanged?.Invoke(this, EventArgs.Empty);
    }

    public void NotifyDataChanged()
    {
        DataChanged?.Invoke(this, EventArgs.Empty);
    }
}
