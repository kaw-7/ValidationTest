using System;
using System.IO;

namespace ValidationTest;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
	}

	private void OnCounterClicked(object? sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

		SemanticScreenReader.Announce(CounterBtn.Text);
	}

    // --- NEW CODE FOR FILE SYSTEM VALIDATION ---
    private async void OnFileTestClicked(object? sender, EventArgs e)
    {
        try
        {
            // 1. Define the native path using MAUI's FileSystem helper
            string targetFileName = "test_file.txt";
            string filePath = Path.Combine(FileSystem.Current.AppDataDirectory, targetFileName);

            // 2. SAVE: Write the char 'a' to the native storage
            char dataToSave = 'a';
            dataToSave++;
            await File.WriteAllTextAsync(filePath, dataToSave.ToString());

            // 3. READ: Read it back from the native storage
            if (File.Exists(filePath))
            {
                string readContent = await File.ReadAllTextAsync(filePath);

                // 4. DISPLAY: Update the UI display field
                FileContentLabel.Text = $"File Content: {readContent}";

                // Optional: Show a native alert for extra "Verification" impact
                await DisplayAlertAsync("Native API Success", $"Successfully wrote and read '{readContent}' at: {filePath}", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Error", $"Native File Error: {ex.Message}", "OK");
        }
    }
}
