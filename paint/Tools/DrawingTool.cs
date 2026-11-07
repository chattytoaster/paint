using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace paint.Tools;

public abstract class DrawingTool
{
    private Point _startPoint;
    private bool _isDrawing;

    public Point StartPoint
    {
        get => _startPoint;
        set => _startPoint = value;
    }

    public bool IsDrawing
    {
        get => _isDrawing;
        set => _isDrawing = value;
    }

    public abstract void OnMouseDown(Point point, Canvas canvas, Color color, double thickness);
    public abstract void OnMouseMove(Point point, Canvas canvas);
    public abstract void OnMouseUp(Point point, Canvas canvas);
}
