using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.IO;

namespace iiDailyWorkout
{
    public partial class MainPage : ContentPage
    {
        // Overall workout minutes
        int workoutMinutes = 0;
        // Create empty storedFile string
        string storedFile = "";
        public MainPage()
        {
            InitializeComponent();

            // Display current date
            string curDate = DateTime.Now.ToShortDateString();
            LblDate.Text = $"Date: {curDate}";

            string fileDate = curDate.Replace('/', '_');

            // Load the saved file, pass date in as parameter
            LoadSavedFile(fileDate);

            LblResults.Text = workoutMinutes.ToString();
        }

        /// <summary>
        ///     Load a saved file (should be cross-platform)
        /// </summary>
        /// <param name="fileDate"></param>
        private void LoadSavedFile(string fileDate)
        {
            // Get and store the path
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            // Store the file name by concatenating the name with the date
            string fileName = $"{fileDate}wom.txt";

            // Get the stored file path
            storedFile = Path.Combine(docPath, fileName);

            // Check out if the file exists

            if (File.Exists(storedFile))
            {
                // Parse the file text as an integer and store it
                workoutMinutes = int.Parse(File.ReadAllText(storedFile).ToString());
            }
            else
            {
                // if the file doesn't exist, create it
                File.WriteAllText(storedFile, workoutMinutes.ToString());
            }
        }

        /// <summary>
        ///     Add minutes to file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAddMinutes_Clicked(object sender, EventArgs e)
        {
            // Make sure something is selected and display an alert if nothing is selected
            if (PckMinutes.SelectedIndex > -1)
            {
                // Increase workout minutes
                workoutMinutes += int.Parse(PckMinutes.SelectedItem.ToString());

                // Write to the file
                File.WriteAllText(storedFile, workoutMinutes.ToString());

                // Update the GUI
                LblResults.Text = workoutMinutes.ToString();
            }
            else
            {
                DisplayAlert("Invalid Input", "Please select minutes", "Close");
            }
        }
    }
}
