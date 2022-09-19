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
    /// 

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
            public List<List<object>> template { get; set; }
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


            Dictionary<string, string> templateList = new Dictionary<string, string>();

            string[] files = Directory.GetFiles(@"C:/NBT-Launcher/Json", "*.json");
            foreach (var file in files)
            {
                string filename = System.IO.Path.GetFileNameWithoutExtension(file);
                string jsonString = File.ReadAllText($"C:/NBT-Launcher/Json/{filename}.json");
                jsonShit nbtInfo = JsonSerializer.Deserialize<jsonShit>(jsonString);
                if (nbtInfo.NbtType == "Template")
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
                    buttonCopy.MouseEnter += ButtonCopy_MouseEnter;
                    buttonCopy.MouseLeave += ButtonCopy_MouseLeave;
                    Style style = Application.Current.FindResource("ButtonHover") as Style;
                    buttonCopy.Style = style;
                    buttonBorder.Child = buttonCopy;

                    //////////////////////////////////////////////////////// new window /////////////////////////////////////////////////////////

                    // background
                    Border backgroundBorder = new Border();
                    backgroundBorder.Background = new SolidColorBrush(Colors.Black) { Opacity = 0.5 };
                    backgroundBorder.Visibility = Visibility.Hidden;
                    backgroundBorder.CornerRadius = new CornerRadius(10);
                    grid.Children.Add(backgroundBorder);

                    // inputBorder
                    Border inputBorder = new Border();
                    inputBorder.Width = 300;
                    inputBorder.Height = 400;
                    inputBorder.Background = new SolidColorBrush((Color)System.Windows.Media.ColorConverter.ConvertFromString("#1b1b1b"));
                    inputBorder.CornerRadius = new CornerRadius(10);
                    backgroundBorder.Child = inputBorder;

                    // input grid
                    Grid inputGrid = new Grid();
                    inputBorder.Child = inputGrid;

                    // doneButton
                    Border donebuttonBorder = new Border();
                    donebuttonBorder.CornerRadius = new CornerRadius(10);
                    donebuttonBorder.Height = 30;
                    donebuttonBorder.Width = 100;
                    donebuttonBorder.Margin = new Thickness(0, 0, 30, 10);
                    donebuttonBorder.Background = copyButtonBrush;
                    donebuttonBorder.VerticalAlignment = VerticalAlignment.Bottom;
                    donebuttonBorder.HorizontalAlignment = HorizontalAlignment.Right;
                    inputGrid.Children.Add(donebuttonBorder);

                    Button doneButton = new Button();
                    doneButton.Background = Brushes.Transparent;
                    doneButton.BorderBrush = null;
                    doneButton.Content = "Done";
                    doneButton.FontSize = 14;
                    doneButton.Foreground = Brushes.GhostWhite;
                    doneButton.MouseEnter += doneButton_MouseEnter;
                    doneButton.MouseLeave += doneButton_MouseLeave;
                    doneButton.Style = style;
                    donebuttonBorder.Child = doneButton;

                    // Canel Button
                    Border cancelButtonBorder = new Border();
                    cancelButtonBorder.CornerRadius = new CornerRadius(10);
                    cancelButtonBorder.Height = 30;
                    cancelButtonBorder.Width = 100;
                    cancelButtonBorder.Margin = new Thickness(30, 0, 0, 10);
                    cancelButtonBorder.Background = copyButtonBrush;
                    cancelButtonBorder.VerticalAlignment = VerticalAlignment.Bottom;
                    cancelButtonBorder.HorizontalAlignment = HorizontalAlignment.Left;
                    inputGrid.Children.Add(cancelButtonBorder);

                    Button cancelButton = new Button();
                    cancelButton.Background = Brushes.Transparent;
                    cancelButton.BorderBrush = null;
                    cancelButton.Content = "Cancel";
                    cancelButton.FontSize = 14;
                    cancelButton.Foreground = Brushes.GhostWhite;
                    cancelButton.MouseEnter += cancelButton_MouseEnter;
                    cancelButton.MouseLeave += cancelButton_MouseLeave;
                    cancelButton.Click += cancel_Click;
                    cancelButton.Style = style;
                    cancelButtonBorder.Child = cancelButton;

                    // scroll viewer and stack pannel
                    ScrollViewer inputScrollViewer = new ScrollViewer();
                    inputScrollViewer.Margin = new Thickness(5, 5, 5, 60);
                    inputScrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
                    inputGrid.Children.Add(inputScrollViewer);
                    StackPanel inputStackPanel = new StackPanel();
                    inputStackPanel.VerticalAlignment = VerticalAlignment.Top;
                    inputStackPanel.HorizontalAlignment = HorizontalAlignment.Center;
                    inputScrollViewer.Content = inputStackPanel;

                    

                    for (int i = 0; i < nbtInfo.template.Count; i++)
                    {
                        var elementValue0 = (nbtInfo.template[i] == null)
                        ? "<missing>"
                        : nbtInfo.template[i][0].ToString();

                        var elementValue1 = (nbtInfo.template[i] == null)
                        ? "<missing>"
                        : nbtInfo.template[i][1].ToString();

                        var elementValue2 = (nbtInfo.template[i] == null)
                        ? "<missing>"
                        : nbtInfo.template[i][2].ToString();
                        // Text Grid
                        Grid textGrid = new Grid();
                        textGrid.Width = 250;
                        textGrid.Height = 60;
                        inputStackPanel.Children.Add(textGrid);

                        // Text Block
                        TextBlock textBlock = new TextBlock();
                        textBlock.HorizontalAlignment = HorizontalAlignment.Center;
                        textBlock.VerticalAlignment = VerticalAlignment.Top;
                        textBlock.Text = elementValue0;
                        textBlock.Foreground = Brushes.GhostWhite;
                        textBlock.FontSize = 16;
                        textGrid.Children.Add(textBlock);

                        // Text Border
                        Border textBorder = new Border();
                        textBorder.CornerRadius = new CornerRadius(5);
                        textBorder.Background = copyButtonBrush;
                        textBorder.Height = 30;
                        textBorder.Width = 250;
                        textBorder.VerticalAlignment = VerticalAlignment.Bottom;

                        string text2 = elementValue1;

                        //Text Box
                        TextBox testText = new TextBox();
                        testText.Background = Brushes.Transparent;
                        testText.Foreground = Brushes.GhostWhite;
                        testText.BorderThickness = new Thickness(0);
                        testText.VerticalContentAlignment = VerticalAlignment.Center;
                        testText.FontSize = 16;
                        testText.Text = elementValue1;
                        textBorder.Child = testText;
                        testText.TextChanged += TestText_TextChanged;
                        testText.Initialized += TestText_Initialized;
                        doneButton.Click += done_Click;
                        textGrid.Children.Add(textBorder);

                        

                        void TestText_Initialized(object sender, EventArgs e)
                        {
                            text2 = elementValue1;
                            try
                            {
                                templateList.Add(elementValue2, "1234");
                            }
                            catch (ArgumentException)
                            {
                                templateList.Remove(elementValue2);
                                templateList.Add(elementValue2, "1234");
                            }
                        }                     

                        void TestText_TextChanged(object sender, TextChangedEventArgs e)
                        {
                            text2 = testText.Text;

                            if (string.IsNullOrEmpty(text2))
                            {
                                text2 = elementValue1;
                            }
                            
                            try
                            {
                                templateList.Add(elementValue2, text2);
                            }
                            catch (ArgumentException)
                            {
                                templateList.Remove(elementValue2);
                                templateList.Add(elementValue2, text2);
                            }
                        }
                    }
                    


                    buttonCopy.Click += button_Click;

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

                    void doneButton_MouseEnter(object sender, MouseEventArgs e)
                    {
                        doneButton.Background = Brushes.Transparent;
                        doneButton.BorderBrush = null;
                        donebuttonBorder.Background = copyButtonBrushEnter;
                    }

                    void doneButton_MouseLeave(object sender, MouseEventArgs e)
                    {
                        donebuttonBorder.Background = copyButtonBrush;
                    }

                    void cancelButton_MouseEnter(object sender, MouseEventArgs e)
                    {
                        cancelButton.Background = Brushes.Transparent;
                        cancelButton.BorderBrush = null;
                        cancelButtonBorder.Background = copyButtonBrushEnter;
                    }

                    void cancelButton_MouseLeave(object sender, MouseEventArgs e)
                    {
                        cancelButtonBorder.Background = copyButtonBrush;
                    }

                    void button_Click(object sender, RoutedEventArgs e)
                    {
                        backgroundBorder.Visibility = Visibility.Visible;
                        templateList.Clear();
                    }

                    void done_Click(object sender, RoutedEventArgs e)
                    {
                        //string newnbt = System.IO.File.ReadAllText($@"C:\Users\Thomas\Downloads\nuke.txt");
                        string newnbt = System.IO.File.ReadAllText($@"C:/NBT-Launcher/Nbts/{nbtInfo.nbtPath}");
                        foreach (KeyValuePair<string, string> templateText in templateList)
                        {
                            newnbt = newnbt.Replace(templateText.Key, templateText.Value);
                        }

                        
                        Clipboard.SetText(newnbt);
                        backgroundBorder.Visibility = Visibility.Hidden;
                        //File.WriteAllText($@"C:\Users\Thomas\Downloads\nuke2.txt", newnbt);
                    }

                    void cancel_Click(object sender, RoutedEventArgs e)
                    {
                        backgroundBorder.Visibility = Visibility.Hidden;
                        templateList.Clear();
                    }
                }
            }
        }
    }
}
