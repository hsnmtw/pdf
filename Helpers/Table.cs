using SkiaSharp;

namespace pdf.Helpers;

//TODO: detect when the canvas is not going to fit the table and split it across multiple pages
public static class Table {
    public static void Draw(SKCanvas canvas, SKFont font, SKRect rect, string[] data, float[] columnWidths) {
        float x = rect.Left;
        float y = rect.Top;
        float rowHeight = font.Size + 2f;
        float tableWidth = rect.Right - rect.Left;
        float columnWidthsSum = 0;
        foreach (var w in columnWidths) {
            columnWidthsSum += w;
        }
        for (int i = 0; i < columnWidths.Length; i++) {
            columnWidths[i] = columnWidths[i] / columnWidthsSum * tableWidth;
        }

        var paint = new SKPaint {
            Color = SKColors.Black,
            IsStroke = true,
            StrokeWidth = 0.75f
        };
        var textPaint = new SKPaint {
            Color = SKColors.Black,
            IsStroke = false
        };
        // Draw table

        for (int i = 0; i < data.Length; i++) {
            float colWidth = columnWidths[i % columnWidths.Length];

            // Draw cell border
            canvas.DrawRect(x, y, colWidth, rowHeight, paint);

            // Draw cell text      
            if (data[i].Any(c => c >= 0x0600 && c <= 0x06FF)) {
                // Arabic text detected, align right
                data[i] = Pdf.HarfBuzz.AraibcPdf.Transform(data[i]);
            }
            canvas.DrawText(data[i], x + 2.5f, y + font.Size, SKTextAlign.Left, font, textPaint);

            // Move to next column
            x += colWidth;

            // Move to next row after filling all columns
            if ((i + 1) % columnWidths.Length == 0) {
                x = rect.Left;
                y += rowHeight;
            }
        }
    } 
}
