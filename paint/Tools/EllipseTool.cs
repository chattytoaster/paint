using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace paint.Tools;

public class EllipseTool : DrawingTool
{
    private Ellipse? _currentEllipse;

    public Ellipse? CurrentEllipse
    {
        get => _currentEllipse;
        set => _currentEllipse = value;
    }

    public override void OnMouseDown(Point point, Canvas canvas, Color color, double thickness)
    {
        IsDrawing = true;
        StartPoint = point;

        _currentEllipse = new Ellipse
        {
            Stroke = new SolidColorBrush(color),
            StrokeThickness = thickness
        };

        Canvas.SetLeft(_currentEllipse, point.X);
        Canvas.SetTop(_currentEllipse, point.Y);
        _currentEllipse.Width = 0;
        _currentEllipse.Height = 0;

        canvas.Children.Add(_currentEllipse);
    }

    public override void OnMouseMove(Point point, Canvas canvas)
    {
        if (IsDrawing && _currentEllipse != null)
        {
            double left = Math.Min(StartPoint.X, point.X);
            double top = Math.Min(StartPoint.Y, point.Y);
            double width = Math.Abs(point.X - StartPoint.X);
            double height = Math.Abs(point.Y - StartPoint.Y);

            Canvas.SetLeft(_currentEllipse, left);
            Canvas.SetTop(_currentEllipse, top);
            _currentEllipse.Width = width;
            _currentEllipse.Height = height;
        }
    }

    public override void OnMouseUp(Point point, Canvas canvas)
    {
        if (IsDrawing && _currentEllipse != null)
        {
            if (_currentEllipse.Width == 0 && _currentEllipse.Height == 0)
            {
                canvas.Children.Remove(_currentEllipse);
            }
        }

        IsDrawing = false;
        _currentEllipse = null;
    }
}
