using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Point origin;  // Original Offset of image
        private Point start;   // Original Position of the mouse
        Image mapImage = new Image();
        public MainWindow()
        {
            InitializeComponent();

            TransformGroup group = new TransformGroup();

            ScaleTransform xform = new ScaleTransform();
            group.Children.Add(xform);

            TranslateTransform tt = new TranslateTransform();
            group.Children.Add(tt);

            grdMaze.RenderTransform = group;



            for (var c = 0; c < 20; c++)
            {
                grdMaze.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(40) });
            }

            for (var l = 0; l < 20; l++)
            {
                grdMaze.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(40) });
            }
            grdMaze.ShowGridLines = true;




            Image mapImage = new Image();

            
            
            BitmapImage newImage = new BitmapImage(new Uri("c:\\testMap.jpg"));
           // mapImage.Source = newImage;
            
            //Grid.SetRow(mapImage, 0);
            //Grid.SetRowSpan(mapImage, 20);
            //Grid.SetColumn(mapImage, 0);
            //Grid.SetColumnSpan(mapImage, 20);

            //Grid.SetRow(border, 0);
            //Grid.SetRowSpan(border, 20);
            //Grid.SetColumn(border, 0);
            //Grid.SetColumnSpan(border, 20);

           
           // grdMaze.Children.Add(mapImage);
            image.Source = newImage;
            Grid.SetRow(image, 0);
            Grid.SetRowSpan(image, 20);
            Grid.SetColumn(image, 0);
            Grid.SetColumnSpan(image, 20);

            Label testLabel = new Label();
            testLabel.Content = "hello";
            testLabel.ToolTip = "hello";

            Grid.SetRow(testLabel, 4);
            Grid.SetColumn(testLabel, 4);

            grdMaze.Children.Add(testLabel);


            grdMaze.MouseWheel += image_MouseWheel;
            grdMaze.MouseLeftButtonDown += image_MouseLeftButtonDown;
            grdMaze.MouseLeftButtonUp += image_MouseLeftButtonUp;
            grdMaze.MouseMove += image_MouseMove;

        }
        private void image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            grdMaze.ReleaseMouseCapture();
        }

        private void image_MouseMove(object sender, MouseEventArgs e)
        {
            if (!grdMaze.IsMouseCaptured) return;

            var tt = (TranslateTransform)((TransformGroup)grdMaze.RenderTransform).Children.First(tr => tr is TranslateTransform);
            Vector v = start - e.GetPosition(border);
            tt.X = origin.X - v.X;
            tt.Y = origin.Y - v.Y;
        }

        private void image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
            var tt = (TranslateTransform)((TransformGroup)grdMaze.RenderTransform).Children.First(tr => tr is TranslateTransform);
            start = e.GetPosition(border);
            origin = new Point(tt.X, tt.Y);
            grdMaze.CaptureMouse();
    
        }

        private void image_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            TransformGroup transformGroup = (TransformGroup)grdMaze.RenderTransform;
            ScaleTransform transform = (ScaleTransform)transformGroup.Children[0];

            double zoom = e.Delta > 0 ? .2 : -.2;
            transform.ScaleX += zoom;
            transform.ScaleY += zoom;
        }

    
    }
}
