using CommunityToolkit.Maui.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SuzerainSaveEditor
{
    public abstract partial class BaseViewModel : ObservableObject
    {
        protected readonly SaveGameService _saveGameService;
        protected string? _loadedFilePath;
        protected string? _originalContent;
        protected Dictionary<string, object>? _saveData;

        [ObservableProperty]
        private string? _statusText;

        public BaseViewModel()
        {
            _saveGameService = new SaveGameService();
        }

        [RelayCommand]
        protected async Task LoadSaveFileAsync()
        {
            try
            {
                var fileResult = await FilePicker.Default.PickAsync();
                if (fileResult == null) return;

                _loadedFilePath = fileResult.FullPath;
                await RefreshDataAsync();
                
                StatusText = $"Loaded: {Path.GetFileName(_loadedFilePath)}";
            }
            catch (Exception ex)
            {
                StatusText = $"Error: {ex.Message}";
            }
        }

        [RelayCommand]
        protected async Task RefreshDataAsync()
        {
             if (string.IsNullOrEmpty(_loadedFilePath))
            {
                StatusText = "No file loaded to refresh.";
                return;
            }

            try
            {
                _originalContent = await _saveGameService.LoadSaveGameAsync(_loadedFilePath);
                _saveData = _saveGameService.ParseSaveGame(_originalContent);

                LoadDataToProperties();
                StatusText = $"Refreshed from: {Path.GetFileName(_loadedFilePath)}";
            }
            catch (Exception ex)
            {
                 StatusText = $"Refresh Error: {ex.Message}";
            }
        }

        [RelayCommand]
        protected async Task SaveSaveFileAsync()
        {
            await SaveToFileAsync(_loadedFilePath);
        }

        [RelayCommand]
        protected async Task ExportSaveFileAsync()
        {
            if (string.IsNullOrEmpty(_loadedFilePath) || _saveData == null)
            {
                StatusText = "No file loaded to export.";
                return;
            }

            try
            {
                SavePropertiesToData();
                var newContent = _saveGameService.CreateModifiedSaveContent(_originalContent, _saveData);
                
                using var stream = new MemoryStream(Encoding.UTF8.GetBytes(newContent));
                
                var fileSaverResult = await FileSaver.Default.SaveAsync("edited-save.json", stream);
                if (fileSaverResult.IsSuccessful)
                {
                    StatusText = $"Exported to: {Path.GetFileName(fileSaverResult.FilePath)}";
                }
                else
                {
                    StatusText = "Export was cancelled or failed.";
                }
            }
            catch (Exception ex)
            {
                StatusText = $"Export Error: {ex.Message}";
            }
        }

        private async Task SaveToFileAsync(string? filePath)
        {
            if (string.IsNullOrEmpty(filePath) || _saveData == null)
            {
                StatusText = "No file loaded to save.";
                return;
            }

            try
            {
                SavePropertiesToData();
                var newContent = _saveGameService.CreateModifiedSaveContent(_originalContent, _saveData);
                await _saveGameService.WriteSaveGameAsync(filePath, newContent);
                StatusText = $"Saved to: {Path.GetFileName(filePath)}";
            }
            catch (Exception ex)
            {
                StatusText = $"Save Error: {ex.Message}";
            }
        }

        protected abstract void LoadDataToProperties();
        protected abstract void SavePropertiesToData();
    }
}