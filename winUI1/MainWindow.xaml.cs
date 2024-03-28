using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.VoiceCommands;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Notifications;
using Windows.UI.Popups;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace winUI1
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {

        //file save GUI
        private async void saveButton_Click(object sender, RoutedEventArgs e)
        {
            string contentToSave = userInputTextBox.Text;

            FileSavePicker savePicker = new FileSavePicker();
            savePicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
            savePicker.FileTypeChoices.Add("Text files (*.txt)", new List<string>() { ".txt" });
            savePicker.SuggestedFileName = "Untitled.txt";

            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(this); // schimba cu main window daca face fente
            WinRT.Interop.InitializeWithWindow.Initialize(savePicker, hwnd);
            StorageFile file = await savePicker.PickSaveFileAsync();

            if (file != null)
            {
                
                try
                {
                    await FileIO.WriteTextAsync(file, contentToSave);

                   
                    var dialog = new ContentDialog()
                    {
                        Title = "Save As",
                        Content = "File saved successfully!",
                        PrimaryButtonText = "OK",
                    };
                    await dialog.ShowAsync();
                }
                catch (Exception ex)
                {
                    // Log the error and show a user-friendly message
                    System.Diagnostics.Debug.WriteLine($"Error saving file: {ex.Message}");

                    // Use ContentDialog for error notification
                    var errorDialog = new ContentDialog()
                    {
                        Title = "Error",
                        Content = $"An error occurred: {ex.Message}",
                        PrimaryButtonText = "OK",
                    };
                    await errorDialog.ShowAsync();
                }
            }


        }


        private void ToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            ToggleSwitch toggleSwitch = sender as ToggleSwitch;
            if (toggleSwitch.IsOn)
            {
                ((FrameworkElement)this.Content).RequestedTheme = ElementTheme.Dark;
            }
            else
            {
                ((FrameworkElement)this.Content).RequestedTheme = ElementTheme.Light;
            }
        }

        
        public MainWindow()
        {
            this.InitializeComponent();
            this.Title = "Notes - WIP";


        }


    }



}
