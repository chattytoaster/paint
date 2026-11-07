using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace paint.Tools;

public class PencilTool : DrawingTool
{
    private Polyline? _currentPolyline;

    public Polyline? CurrentPolyline
    {
        get => _currentPolyline;
        set => _currentPolyline = value;
    }

    public override void OnMouseDown(Point point, Canvas canvas, Color color, double thickness)
    {
        IsDrawing = true;
        StartPoint = point;

        _currentPolyline = new Polyline
        {
            Stroke = new SolidColorBrush(color),
            StrokeThickness = thickness,
            StrokeLineJoin = PenLineJoin.Round,
            StrokeStartLineCap = PenLineCap.Round,
            StrokeEndLineCap = PenLineCap.Round
        };

        _currentPolyline.Points.Add(point);
        canvas.Children.Add(_currentPolyline);
    }

    public override void OnMouseMove(Point point, Canvas canvas)
    {
        if (IsDrawing && _currentPolyline != null)
        {
            _currentPolyline.Points.Add(point);
        }
    }

    public override void OnMouseUp(Point point, Canvas canvas)
    {
        if (IsDrawing && _currentPolyline != null)
        {
            if (_currentPolyline.Points.Count == 1)
            {
                _currentPolyline.Points.Add(new Point(point.X + 0.1, point.Y + 0.1));
            }
        }

        IsDrawing = false;
        _currentPolyline = null;
    }
}
