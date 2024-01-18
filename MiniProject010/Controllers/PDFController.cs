using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.collection;
using Microsoft.AspNetCore.Mvc;
using MiniProject010.Models;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace MiniProject010.Controllers
{
    public class PDFController : Controller
    {
        private readonly Context _context;

        public PDFController(Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();    
        }
        
        [HttpGet]
        public IActionResult HelloWorld()
        {
            return View();
        }

        [HttpPost]
        public IActionResult HelloWorld(string userName)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Document document = new Document(PageSize.A4, 25, 25, 30, 30);
                PdfWriter writer = PdfWriter.GetInstance(document, ms);
                document.Open();
                //var image = iTextSharp.text.Image.GetInstance("wwwroot/image/jakisimg.pgn");
                //image.Alignment = Element.ALIGN_CENTER;
                //document.Add(image);
                Paragraph par1 = new Paragraph("Hello " + userName, new Font(Font.FontFamily.HELVETICA, 20));
                par1.Alignment = Element.ALIGN_CENTER;
                document.Add(par1);
                document.Close();
                writer.Close();
                var constant = ms.ToArray();
                return File(constant, "application/vnd", "hello.pdf");
            }
        }

        [HttpGet]
        public IActionResult TOPNColors()
        {
            return View();
        }

        [HttpPost]
        public IActionResult TOPNColors(string nrColors)
        {
            int howManyColors = 5;
            Int32.TryParse(nrColors, out howManyColors);
            var result = _context.Colors.Take(howManyColors).ToList();
            using (MemoryStream ms = new MemoryStream())
            {
                Document document = new Document(PageSize.A4, 25, 25, 30, 30);
                PdfWriter writer = PdfWriter.GetInstance(document, ms);
                document.Open();
                //var image = iTextSharp.text.Image.GetInstance("wwwroot/image/jakisimg.png");
                //image.Alignment = Element.ALIGN_CENTER;
                //document.Add(image);
                Paragraph par1 = new Paragraph("TOP " + howManyColors.ToString() + "Colors", new Font(Font.FontFamily.HELVETICA, 20));
                par1.Alignment = Element.ALIGN_CENTER;
                document.Add(par1);

                PdfPTable table = new PdfPTable(4);

                PdfPCell cell1 = new PdfPCell(new Phrase("Id", new Font(Font.FontFamily.HELVETICA, 10)));
                cell1.BackgroundColor = BaseColor.LIGHT_GRAY;
                cell1.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                cell1.BorderWidthBottom = 1f;
                cell1.BorderWidthTop = 1f;
                cell1.BorderWidthLeft = 1f;
                cell1.BorderWidthRight = 1f;
                cell1.HorizontalAlignment = Element.ALIGN_CENTER;
                cell1.VerticalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell1);

                PdfPCell cell2 = new PdfPCell(new Phrase("Name", new Font(Font.FontFamily.HELVETICA, 10)));
                cell2.BackgroundColor = BaseColor.LIGHT_GRAY;
                cell2.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                cell2.BorderWidthBottom = 1f;
                cell2.BorderWidthTop = 1f;
                cell2.BorderWidthLeft = 1f;
                cell2.BorderWidthRight = 1f;
                cell2.HorizontalAlignment = Element.ALIGN_CENTER;
                cell2.VerticalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell2);

                PdfPCell cell3 = new PdfPCell(new Phrase("Hex", new Font(Font.FontFamily.HELVETICA, 10)));
                cell3.BackgroundColor = BaseColor.LIGHT_GRAY;
                cell3.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                cell3.BorderWidthBottom = 1f;
                cell3.BorderWidthTop = 1f;
                cell3.BorderWidthLeft = 1f;
                cell3.BorderWidthRight = 1f;
                cell3.HorizontalAlignment = Element.ALIGN_CENTER;
                cell3.VerticalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell3);

                PdfPCell cell4 = new PdfPCell(new Phrase("Decimal", new Font(Font.FontFamily.HELVETICA, 10)));
                cell4.BackgroundColor = BaseColor.LIGHT_GRAY;
                cell4.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                cell4.BorderWidthBottom = 1f;
                cell4.BorderWidthTop = 1f;
                cell4.BorderWidthLeft = 1f;
                cell4.BorderWidthRight = 1f;
                cell4.HorizontalAlignment = Element.ALIGN_CENTER;
                cell4.VerticalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell4);

                foreach (var item in result)
                {
                    PdfPCell cell_1 = new PdfPCell(new Phrase(item.Id.ToString()));
                    PdfPCell cell_2 = new PdfPCell(new Phrase(item.Name));
                    PdfPCell cell_3 = new PdfPCell(new Phrase(item.HexValue));
                    PdfPCell cell_4 = new PdfPCell(new Phrase(item.DecimalValue));

                    cell_1.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell_2.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell_3.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell_4.HorizontalAlignment = Element.ALIGN_CENTER;

                    table.AddCell(cell_1);
                    table.AddCell(cell_2);
                    table.AddCell(cell_3);
                    table.AddCell(cell_4);
                }
                document.Add(table);

                document.Close();
                writer.Close();
                var constant = ms.ToArray();
                return File(constant, "application/vnd", "TopColors.pdf");
            }
        }

        [HttpGet]
        public IActionResult ColorsByName()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ColorsByName(string colorName)
        {
            var result = _context.Colors.Where(x => x.Name.Contains(colorName)).ToList();
            using (MemoryStream ms = new MemoryStream())
            {
                Document document = new Document(PageSize.A4, 25, 25, 30, 30);
                PdfWriter writer = PdfWriter.GetInstance(document, ms);
                document.Open();
                //var image = iTextSharp.text.Image.GetInstance("wwwroot/image/jakisimg.png");
                //image.Alignment = Element.ALIGN_CENTER;
                //document.Add(image);
                Paragraph par1 = new Paragraph("Colors by " + colorName, new Font(Font.FontFamily.HELVETICA, 20));
                par1.Alignment = Element.ALIGN_CENTER;
                document.Add(par1);

                PdfPTable table = new PdfPTable(4);

                PdfPCell cell1 = new PdfPCell(new Phrase("Id", new Font(Font.FontFamily.HELVETICA, 10)));
                cell1.BackgroundColor = BaseColor.LIGHT_GRAY;
                cell1.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                cell1.BorderWidthBottom = 1f;
                cell1.BorderWidthTop = 1f;
                cell1.BorderWidthLeft = 1f;
                cell1.BorderWidthRight = 1f;
                cell1.HorizontalAlignment = Element.ALIGN_CENTER;
                cell1.VerticalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell1);

                PdfPCell cell2 = new PdfPCell(new Phrase("Name", new Font(Font.FontFamily.HELVETICA, 10)));
                cell2.BackgroundColor = BaseColor.LIGHT_GRAY;
                cell2.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                cell2.BorderWidthBottom = 1f;
                cell2.BorderWidthTop = 1f;
                cell2.BorderWidthLeft = 1f;
                cell2.BorderWidthRight = 1f;
                cell2.HorizontalAlignment = Element.ALIGN_CENTER;
                cell2.VerticalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell2);

                PdfPCell cell3 = new PdfPCell(new Phrase("Hex", new Font(Font.FontFamily.HELVETICA, 10)));
                cell3.BackgroundColor = BaseColor.LIGHT_GRAY;
                cell3.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                cell3.BorderWidthBottom = 1f;
                cell3.BorderWidthTop = 1f;
                cell3.BorderWidthLeft = 1f;
                cell3.BorderWidthRight = 1f;
                cell3.HorizontalAlignment = Element.ALIGN_CENTER;
                cell3.VerticalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell3);

                PdfPCell cell4 = new PdfPCell(new Phrase("Decimal", new Font(Font.FontFamily.HELVETICA, 10)));
                cell4.BackgroundColor = BaseColor.LIGHT_GRAY;
                cell4.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                cell4.BorderWidthBottom = 1f;
                cell4.BorderWidthTop = 1f;
                cell4.BorderWidthLeft = 1f;
                cell4.BorderWidthRight = 1f;
                cell4.HorizontalAlignment = Element.ALIGN_CENTER;
                cell4.VerticalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell4);

                foreach (var item in result)
                {
                    PdfPCell cell_1 = new PdfPCell(new Phrase(item.Id.ToString()));
                    PdfPCell cell_2 = new PdfPCell(new Phrase(item.Name));
                    PdfPCell cell_3 = new PdfPCell(new Phrase(item.HexValue));
                    PdfPCell cell_4 = new PdfPCell(new Phrase(item.DecimalValue));

                    cell_1.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell_2.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell_3.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell_4.HorizontalAlignment = Element.ALIGN_CENTER;

                    table.AddCell(cell_1);
                    table.AddCell(cell_2);
                    table.AddCell(cell_3);
                    table.AddCell(cell_4);
                }
                document.Add(table);

                document.Close();
                writer.Close();
                var constant = ms.ToArray();
                return File(constant, "application/vnd", "ColorsByName.pdf");
            }
        }
    }
}
