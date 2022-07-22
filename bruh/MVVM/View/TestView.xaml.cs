using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.Text.Json;
using System.IO;

namespace bruh.MVVM.View
{
    /// <summary>
    /// Interaction logic for TestView.xaml
    /// </summary>
    public partial class TestView : UserControl
    {
        public class jsonShit
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public string NbtType { get; set; }
            public string Color1 { get; set; }
            public string Color2 { get; set; }
            public string imagePath { get; set; }
            public string nbtPath { get; set; }
        }

        public TestView()
        {
            InitializeComponent();

            

            // make stack panel and scroll viewer
            Grid grid = new Grid();
            this.testwindow.Children.Add(grid);
            ScrollViewer scrollViewer = new ScrollViewer();
            scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            grid.Children.Add(scrollViewer);
            StackPanel stackPanel = new StackPanel();
            stackPanel.VerticalAlignment = VerticalAlignment.Top;
            stackPanel.HorizontalAlignment = HorizontalAlignment.Left;
            scrollViewer.Content = stackPanel;        

            LinearGradientBrush copyButtonBrush =
            new LinearGradientBrush();
            copyButtonBrush.StartPoint = new Point(0, 0);
            copyButtonBrush.EndPoint = new Point(1, 1);
            copyButtonBrush.GradientStops.Add(
                new GradientStop((Color)System.Windows.Media.ColorConverter.ConvertFromString("#242424"), 0.0));
            copyButtonBrush.GradientStops.Add(
                new GradientStop((Color)System.Windows.Media.ColorConverter.ConvertFromString("#2b2b2b"), 1.0));

            LinearGradientBrush copyButtonBrushEnter =
            new LinearGradientBrush();
            copyButtonBrushEnter.StartPoint = new Point(0, 0);
            copyButtonBrushEnter.EndPoint = new Point(1, 1);
            copyButtonBrushEnter.GradientStops.Add(
                new GradientStop((Color)System.Windows.Media.ColorConverter.ConvertFromString("#323232"), 0.0));
            copyButtonBrushEnter.GradientStops.Add(
                new GradientStop((Color)System.Windows.Media.ColorConverter.ConvertFromString("#393939"), 1.0));


            string[] files = Directory.GetFiles(@"C:/NBT-Launcher/Json", "*.json");
            foreach (var file in files)
            {
                string filename = System.IO.Path.GetFileNameWithoutExtension(file);
                string jsonString = File.ReadAllText($"C:/NBT-Launcher/Json/{filename}.json");
                jsonShit nbtInfo = JsonSerializer.Deserialize<jsonShit>(jsonString);
                if (nbtInfo.NbtType == "Test")
                {

                    LinearGradientBrush backgroundBrush =
                    new LinearGradientBrush();
                    backgroundBrush.StartPoint = new Point(0, 0);
                    backgroundBrush.EndPoint = new Point(1, 1.2);
                    backgroundBrush.GradientStops.Add(
                        new GradientStop((Color)System.Windows.Media.ColorConverter.ConvertFromString(nbtInfo.Color1), 0.0));
                    backgroundBrush.GradientStops.Add(
                        new GradientStop((Color)System.Windows.Media.ColorConverter.ConvertFromString(nbtInfo.Color2), 1.0));

                    // border
                    Border border = new Border() { };
                    border.Width = 711;
                    border.Height = 200;
                    border.Margin = new Thickness(0, 10, 0, 10);
                    border.Background = backgroundBrush;
                    stackPanel.Children.Add(border);

                    // grid
                    Grid content = new Grid();
                    border.Child = content;

                    // border clip
                    RectangleGeometry clipRect = new RectangleGeometry();
                    clipRect.Rect = new Rect(0, 0, 650, 200);
                    clipRect.RadiusX = 10;
                    clipRect.RadiusY = 10;
                    border.Clip = clipRect;

                    // Create Image and set its width and height  
                    Image image = new Image();
                    image.Width = 280;
                    image.Height = 180;
                    image.Margin = new Thickness(10, 0, 0, 0);
                    image.HorizontalAlignment = HorizontalAlignment.Left;
                    image.VerticalAlignment = VerticalAlignment.Center;

                    // Create a BitmapSource  
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri($@"C:/NBT-Launcher/Images/{nbtInfo.imagePath}");
                    bitmap.EndInit();

                    // Set Image.Source  
                    image.Source = bitmap;

                    // Add Image to Window  
                    content.Children.Add(image);

                    // text stack pannel
                    StackPanel text = new StackPanel();
                    text.Width = 650;
                    text.Height = 200;
                    text.Margin = new Thickness(310, 0, 0, 0);
                    content.Children.Add(text);

                    // title 
                    TextBlock title = new TextBlock();
                    title.Text = nbtInfo.Title;
                    title.HorizontalAlignment = HorizontalAlignment.Left;
                    title.Margin = new Thickness(0, 10, 0, 0);
                    title.FontSize = 32;
                    title.Foreground = Brushes.White;
                    text.Children.Add(title);

                    // description
                    TextBlock description = new TextBlock();
                    description.Text = nbtInfo.Description;
                    description.HorizontalAlignment = HorizontalAlignment.Left;
                    description.Margin = new Thickness(0, 0, 0, 10);
                    description.FontSize = 18;
                    description.Foreground = Brushes.White;
                    text.Children.Add(description);

                    // button
                    Border buttonBorder = new Border();
                    buttonBorder.CornerRadius = new CornerRadius(10);
                    buttonBorder.Margin = new Thickness(346, 140, 162.667, 10);
                    buttonBorder.Background = copyButtonBrush;

                    content.Children.Add(buttonBorder);
                    Button buttonCopy = new Button();
                    buttonCopy.Background = Brushes.Transparent;
                    buttonCopy.BorderBrush = null;
                    buttonCopy.Content = "Copy to Clipboard";
                    buttonCopy.FontSize = 16;
                    buttonCopy.Foreground = Brushes.GhostWhite;
                    buttonCopy.Click += button_Click;
                    buttonCopy.MouseEnter += ButtonCopy_MouseEnter;
                    buttonCopy.MouseLeave += ButtonCopy_MouseLeave;
                    Style style = Application.Current.FindResource("ButtonHover") as Style;
                    buttonCopy.Style = style;
                    buttonBorder.Child = buttonCopy;

                    void ButtonCopy_MouseEnter(object sender, MouseEventArgs e)
                    {
                        buttonCopy.Background = Brushes.Transparent;
                        buttonCopy.BorderBrush = null;
                        buttonBorder.Background = copyButtonBrushEnter;
                    }

                    void ButtonCopy_MouseLeave(object sender, MouseEventArgs e)
                    {
                        buttonBorder.Background = copyButtonBrush;
                    }

                    void button_Click(object sender, RoutedEventArgs e)
                    {
                        string nbt = System.IO.File.ReadAllText(nbtInfo.nbtPath);
                        Clipboard.SetText(nbt);
                    }
                }
            }
        }
    }
}
