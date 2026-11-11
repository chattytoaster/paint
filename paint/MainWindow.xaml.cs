using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using paint.Tools;

namespace paint;

public partial class MainWindow : Window
{
    private ToolManager _toolManager;
    private Saver _saver;
    private Color _selectedColor;
    private double _selectedThickness;
//опопои
    public MainWindow()
    {
        InitializeComponent();

        _toolManager = new ToolManager();
        _saver = new Saver();

        _selectedColor = Colors.Black;
        _selectedThickness = 3;
    }

    private Point ClampPointToCanvas(Point p)
    {
        double x = p.X;
        double y = p.Y;

        if (x < 0) x = 0;
        if (x > PaintCanvas.ActualWidth) x = PaintCanvas.ActualWidth;

        if (y < 0) y = 0;
        if (y > PaintCanvas.ActualHeight) y = PaintCanvas.ActualHeight;

        return new Point(x, y);
    }

    private void PaintCanvas_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
        {
            PaintCanvas.CaptureMouse();
            Point point = ClampPointToCanvas(e.GetPosition(PaintCanvas));
            _toolManager.HandleMouseDown(point, PaintCanvas, _selectedColor, _selectedThickness);
        }
    }

    private void PaintCanvas_MouseMove(object sender, MouseEventArgs e)
    {
        Point point = ClampPointToCanvas(e.GetPosition(PaintCanvas));

        StatusTextCoordinates.Text = $"X: {(int)point.X}, Y: {(int)point.Y}";

        if (e.LeftButton == MouseButtonState.Pressed)
        {
            _toolManager.HandleMouseMove(point, PaintCanvas);
        }
    }

    private void PaintCanvas_MouseUp(object sender, MouseButtonEventArgs e)
    {
        if (PaintCanvas.IsMouseCaptured)
        {
            PaintCanvas.ReleaseMouseCapture();
        }

        Point point = ClampPointToCanvas(e.GetPosition(PaintCanvas));
        _toolManager.HandleMouseUp(point, PaintCanvas);
    }

    private void PaintCanvas_MouseLeave(object sender, MouseEventArgs e)
    {
        if (!PaintCanvas.IsMouseCaptured)
        {
            StatusTextCoordinates.Text = "X: -, Y: -";
        }
    }

    private void ToolRadio_Checked(object sender, RoutedEventArgs e)
    {
        if (_toolManager == null) return;

        if (RadioPencil.IsChecked == true)
        {
            _toolManager.CurrentTool = _toolManager.PencilTool;
            if (StatusTextTool != null) StatusTextTool.Text = "Инструмент: Карандаш";
        }
        else if (RadioLine.IsChecked == true)
        {
            _toolManager.CurrentTool = _toolManager.LineTool;
            if (StatusTextTool != null) StatusTextTool.Text = "Инструмент: Линия";
        }
        else if (RadioRectangle.IsChecked == true)
        {
            _toolManager.CurrentTool = _toolManager.RectangleTool;
            if (StatusTextTool != null) StatusTextTool.Text = "Инструмент: Прямоугольник";
        }
        else if (RadioEllipse.IsChecked == true)
        {
            _toolManager.CurrentTool = _toolManager.EllipseTool;
            if (StatusTextTool != null) StatusTextTool.Text = "Инструмент: Эллипс";
        }
    }

    private void ColorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (ColorComboBox.SelectedItem is ComboBoxItem item && item.Tag != null)
        {
            string colorHex = item.Tag.ToString()!;
            _selectedColor = (Color)ColorConverter.ConvertFromString(colorHex);

            if (ColorPreviewBorder != null)
            {
                ColorPreviewBorder.Background = new SolidColorBrush(_selectedColor);
            }

            if (StatusTextColor != null)
            {
                if (item.Content is StackPanel panel && panel.Children.Count > 1 && panel.Children[1] is TextBlock tb)
                {
                    StatusTextColor.Text = $"Цвет: {tb.Text}";
                }
            }
        }
    }

    private void ThicknessSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        _selectedThickness = ThicknessSlider.Value;

        if (ThicknessText != null)
        {
            ThicknessText.Text = $"{(int)_selectedThickness} px";
        }

        if (StatusTextThickness != null)
        {
            StatusTextThickness.Text = $"Толщина: {(int)_selectedThickness} px";
        }
    }

    private void BtnClear_Click(object sender, RoutedEventArgs e)
    {
        MessageBoxResult result = MessageBox.Show(
            "Вы уверены, что хотите полностью очистить холст?",
            "Очистка холста",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question);

        if (result == MessageBoxResult.Yes)
        {
            PaintCanvas.Children.Clear();
        }
    }

    private void BtnSave_Click(object sender, RoutedEventArgs e)
    {
        _saver.SaveCanvasImage(PaintCanvas);
    }
}
