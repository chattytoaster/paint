using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace paint.Tools;

public class ToolManager
{
    private DrawingTool _currentTool;

    private readonly DrawingTool _pencilTool;
    private readonly DrawingTool _lineTool;
    private readonly DrawingTool _rectangleTool;
    private readonly DrawingTool _ellipseTool;

    public DrawingTool CurrentTool
    {
        get => _currentTool;
        set => _currentTool = value;
    }

    public DrawingTool PencilTool => _pencilTool;
    public DrawingTool LineTool => _lineTool;
    public DrawingTool RectangleTool => _rectangleTool;
    public DrawingTool EllipseTool => _ellipseTool;

    public ToolManager()
    {
        _pencilTool = new PencilTool();
        _lineTool = new LineTool();
        _rectangleTool = new RectangleTool();
        _ellipseTool = new EllipseTool();

        _currentTool = _pencilTool;
    }

    public void HandleMouseDown(Point point, Canvas canvas, Color color, double thickness)
    {
        if (_currentTool != null)
        {
            _currentTool.OnMouseDown(point, canvas, color, thickness);
        }
    }

    public void HandleMouseMove(Point point, Canvas canvas)
    {
        if (_currentTool != null)
        {
            _currentTool.OnMouseMove(point, canvas);
        }
    }

    public void HandleMouseUp(Point point, Canvas canvas)
    {
        if (_currentTool != null)
        {
            _currentTool.OnMouseUp(point, canvas);
        }
    }
}
