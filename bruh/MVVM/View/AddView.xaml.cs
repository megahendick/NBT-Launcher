using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace bruh.MVVM.View
{
    /// <summary>
    /// Interaction logic for AddView.xaml
    /// </summary>
    public class jsonWrite
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string NbtType { get; set; }
        public string Color1 { get; set; }
        public string Color2 { get; set; }
        public string imagePath { get; set; }
        public string nbtPath { get; set; }
    }
    public partial class AddView : UserControl
    {
        string RadioText;
        string nbtpath;
        string imagepath;
        string imagename;
        string nbtname;
        string zipPath;

        public AddView()
        {
            InitializeComponent();            
        }

        private void RadioButton1_Click(object sender, RoutedEventArgs e)
        {
            RadioText = "Grief";
        }
        private void RadioButton2_Click(object sender, RoutedEventArgs e)
        {
            RadioText = "Build";
        }
        private void RadioButton3_Click(object sender, RoutedEventArgs e)
        {
            RadioText = "Kit";
        }
        private void RadioButton4_Click(object sender, RoutedEventArgs e)
        {
            RadioText = "NPC";
        }
        private void RadioButton5_Click(object sender, RoutedEventArgs e)
        {
            RadioText = "Crash";
        }
        private void RadioButton6_Click(object sender, RoutedEventArgs e)
        {
            RadioText = "Funny";
        }
        private void RadioButton7_Click(object sender, RoutedEventArgs e)
        {
            RadioText = "Misc";
        }

        public void FindImageButton_Click(object sender, RoutedEventArgs e)
        {
            // Configure open file dialog box
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "Images"; // Default file name
            dialog.DefaultExt = ".png"; // Default file extension
            dialog.Filter = "Images (.png)|*.png"; // Filter files by extension

            // Show open file dialog box
            bool? result = dialog.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                imagepath = dialog.FileName;
            }
            imagename = System.IO.Path.GetFileNameWithoutExtension(imagepath);
            FindImageButton.Content = imagename;
        }

        public void FindFileButton_Click(object sender, RoutedEventArgs e)
        {
            // Configure open file dialog box
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "Document"; // Default file name
            dialog.DefaultExt = ".txt"; // Default file extension
            dialog.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

            // Show open file dialog box
            bool? result = dialog.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                nbtpath = dialog.FileName;
            }
            nbtname = System.IO.Path.GetFileNameWithoutExtension(nbtpath);
            FindFileButton.Content = nbtname;
        }

        public void AddNBTButton_Click(object sender, RoutedEventArgs e)
        {
            File.Move(nbtpath, $"C:/NBT-Launcher/Nbts/{nbtname}.txt");

            File.Move(imagepath, $"C:/NBT-Launcher/Images/{imagename}.png");

            var json = new jsonWrite
            {
                Title = title.Text,
                Description = description.Text,
                NbtType = RadioText,
                Color1 = color1.Text,
	            Color2 = color2.Text,
	            imagePath = $"{imagename}.png",
	            nbtPath = $"{nbtname}.txt"
            };
            
            string jsonString = JsonSerializer.Serialize(json);

            using (StreamWriter sw = File.CreateText($"C:/NBT-Launcher/Json/{nbtname}.json"))
            {
                sw.WriteLine(jsonString);
            }
        }

        public void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            if (Directory.Exists("C:/temp/NBT-Launcher"))
            {
                Directory.Delete($@"C:\temp\NBT-Launcher", true);
            }
            string home = "C:" + Environment.GetEnvironmentVariable(RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "HOMEPATH" : "HOME");
            // Configure open file dialog box
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "Compressed Folders"; // Default file name
            dialog.DefaultExt = ".zip"; // Default file extension
            dialog.Filter = "Compressed Folders (.zip)|*.zip"; // Filter files by extension

            // Show open file dialog box
            bool? result = dialog.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                zipPath = dialog.FileName;

                string zipName = System.IO.Path.GetFileNameWithoutExtension(zipPath);
                ZipFile.ExtractToDirectory($@"{zipPath}", @"C:\temp\NBT-Launcher");
                string rootPath = @"C:\temp\NBT-Launcher";
                string[] dirs = Directory.GetDirectories(rootPath, "*", System.IO.SearchOption.TopDirectoryOnly);
                string dir2 = dirs[0].Remove(0, 21);
                string[] nbtFiles = Directory.GetFiles($@"C:\temp\NBT-Launcher\{dir2}\Nbts");
                foreach (string file in nbtFiles)
                {
                    string nbtName = System.IO.Path.GetFileName(file);
                    File.Copy(file, $@"C:\NBT-Launcher\Nbts\{nbtName}", true);
                }
                string[] imageFiles = Directory.GetFiles($@"C:\temp\NBT-Launcher\{dir2}\Images");
                foreach (string file in imageFiles)
                {
                    string imageName = System.IO.Path.GetFileName(file);
                    File.Copy(file, $@"C:\NBT-Launcher\Images\{imageName}", true);
                }
                string[] jsonFiles = Directory.GetFiles($@"C:\temp\NBT-Launcher\{dir2}\Json");
                foreach (string file in jsonFiles)
                {
                    string jsonName = System.IO.Path.GetFileName(file);
                    File.Copy(file, $@"C:\NBT-Launcher\Json\{jsonName}", true);
                }
                Directory.Delete($@"C:\temp\NBT-Launcher", true);
            }
        }
    }
}
