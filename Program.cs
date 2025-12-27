
using SkiaSharp;

var doc = SKDocument.CreatePdf("table.pdf");
var canvas = doc.BeginPage(595, 842); // A4 size in points
var rect = new SKRect(50, 50, 545/2, 792); // Margins of 50 points
string[] data = {   "Header 1", "Header 2", "Header 3",
                    "Row 1 Col 1", "Row 1 Col 2", "Row 1 Col 3",
                    "Row 2 Col 1", "Row 2 Col 2", "Row 2 Col 3",
                    "Row 3 Col 1", "Row 3 Col 2", "Row 3 Col 3",
                    "دعم", "للغة", "العربية" };
float[] columnWidths = { 1, 1, 1 }; // Relative widths
var font = new SKFont(SKTypeface.FromFile(@"C:\Windows\Fonts\Arial.ttf"), 9);
pdf.Helpers.Table.Draw(canvas, font, rect, data, columnWidths);
doc.EndPage();
doc.Close();
