using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace paint.Tools;

public class RectangleTool : DrawingTool
{
    private Rectangle? _currentRectangle;

    public Rectangle? CurrentRectangle
    {
        get => _currentRectangle;
        set => _currentRectangle = value;
    }

    public override void OnMouseDown(Point point, Canvas canvas, Color color, double thickness)
    {
        IsDrawing = true;
        StartPoint = point;

        _currentRectangle = new Rectangle
        {
            Stroke = new SolidColorBrush(color),
            StrokeThickness = thickness
        };

        Canvas.SetLeft(_currentRectangle, point.X);
        Canvas.SetTop(_currentRectangle, point.Y);
        _currentRectangle.Width = 0;
        _currentRectangle.Height = 0;

        canvas.Children.Add(_currentRectangle);
    }

    public override void OnMouseMove(Point point, Canvas canvas)
    {
        if (IsDrawing && _currentRectangle != null)
        {
            double left = Math.Min(StartPoint.X, point.X);
            double top = Math.Min(StartPoint.Y, point.Y);
            double width = Math.Abs(point.X - StartPoint.X);
            double height = Math.Abs(point.Y - StartPoint.Y);

            Canvas.SetLeft(_currentRectangle, left);
            Canvas.SetTop(_currentRectangle, top);
            _currentRectangle.Width = width;
            _currentRectangle.Height = height;
        }
    }

    public override void OnMouseUp(Point point, Canvas canvas)
    {
        if (IsDrawing && _currentRectangle != null)
        {
            if (_currentRectangle.Width == 0 && _currentRectangle.Height == 0)
            {
                canvas.Children.Remove(_currentRectangle);
            }
        }

        IsDrawing = false;
        _currentRectangle = null;
    }
}
