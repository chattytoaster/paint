using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

namespace paint;

public class Saver
{
    public bool SaveCanvasImage(Canvas canvas)
    {
        if (canvas == null)
        {
            MessageBox.Show("Ошибка: холст не существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            return false;
        }

        int width = (int)canvas.ActualWidth;
        int height = (int)canvas.ActualHeight;

        if (width <= 0 || height <= 0)
        {
            MessageBox.Show("Размер холста слишком мал для сохранения!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }

        SaveFileDialog saveFileDialog = new SaveFileDialog
        {
            Title = "Сохранить изображение",
            Filter = "Изображение PNG (*.png)|*.png|Изображение JPEG (*.jpg)|*.jpg",
            DefaultExt = ".png",
            FileName = "Рисунок"
        };

        if (saveFileDialog.ShowDialog() == true)
        {
            try
            {
                RenderTargetBitmap renderBitmap = new RenderTargetBitmap(
                    width,
                    height,
                    96d,
                    96d,
                    PixelFormats.Pbgra32);

                DrawingVisual visual = new DrawingVisual();
                using (DrawingContext context = visual.RenderOpen())
                {
                    context.DrawRectangle(Brushes.White, null, new Rect(0, 0, width, height));

                    VisualBrush brush = new VisualBrush(canvas);
                    context.DrawRectangle(brush, null, new Rect(0, 0, width, height));
                }

                renderBitmap.Render(visual);

                BitmapEncoder encoder;
                string extension = Path.GetExtension(saveFileDialog.FileName).ToLower();

                if (extension == ".jpg" || extension == ".jpeg")
                {
                    encoder = new JpegBitmapEncoder();
                }
                else
                {
                    encoder = new PngBitmapEncoder();
                }

                encoder.Frames.Add(BitmapFrame.Create(renderBitmap));

                using (FileStream fileStream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                {
                    encoder.Save(fileStream);
                }

                MessageBox.Show("Изображение успешно сохранено в файл:\n" + saveFileDialog.FileName,
                    "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении файла: " + ex.Message,
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        return false;
    }
}
