using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace paint.Tools;

public class LineTool : DrawingTool
{
    private Line? _currentLine;

    public Line? CurrentLine
    {
        get => _currentLine;
        set => _currentLine = value;
    }

    public override void OnMouseDown(Point point, Canvas canvas, Color color, double thickness)
    {
        IsDrawing = true;
        StartPoint = point;

        _currentLine = new Line
        {
            X1 = point.X,
            Y1 = point.Y,
            X2 = point.X,
            Y2 = point.Y,
            Stroke = new SolidColorBrush(color),
            StrokeThickness = thickness,
            StrokeStartLineCap = PenLineCap.Round,
            StrokeEndLineCap = PenLineCap.Round
        };

        canvas.Children.Add(_currentLine);
    }

    public override void OnMouseMove(Point point, Canvas canvas)
    {
        if (IsDrawing && _currentLine != null)
        {
            _currentLine.X2 = point.X;
            _currentLine.Y2 = point.Y;
        }
    }

    public override void OnMouseUp(Point point, Canvas canvas)
    {
        if (IsDrawing && _currentLine != null)
        {
            _currentLine.X2 = point.X;
            _currentLine.Y2 = point.Y;
        }

        IsDrawing = false;
        _currentLine = null;
    }
}
