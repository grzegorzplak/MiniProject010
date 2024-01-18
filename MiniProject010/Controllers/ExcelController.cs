using iTextSharp.text.pdf;
using iTextSharp.text;
using Microsoft.AspNetCore.Mvc;
using MiniProject010.Models;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;

namespace MiniProject010.Controllers
{
    public class ExcelController : Controller
    {
        private readonly Context _context;

        public ExcelController(Context context)
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
            System.IO.Stream spreadsheetStream = new System.IO.MemoryStream();
            XLWorkbook wb = new ClosedXML.Excel.XLWorkbook();
            IXLWorksheet ws = wb.AddWorksheet();
            ws.FirstCell().SetValue("Hello " + userName);
            wb.SaveAs(spreadsheetStream);
            spreadsheetStream.Position = 0;
            return new FileStreamResult(spreadsheetStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet") { FileDownloadName = "hello.xlsx" };
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

            System.IO.Stream spreadsheetStream = new System.IO.MemoryStream();
            XLWorkbook wb = new ClosedXML.Excel.XLWorkbook();
            IXLWorksheet ws = wb.AddWorksheet();
            ws.FirstCell().SetValue("TOP " + howManyColors.ToString() + " colors");
            ws.Cell(2, 1).SetValue("Id");
            ws.Cell(2, 2).SetValue("Color name");
            ws.Cell(2, 3).SetValue("Hex value");
            ws.Cell(2, 4).SetValue("Decimal value");
            int x = 3;
            foreach (var item in result)
            {
                ws.Cell(x, 1).SetValue(item.Id);
                ws.Cell(x, 2).SetValue(item.Name);
                ws.Cell(x, 3).SetValue(item.HexValue);
                ws.Cell(x, 4).SetValue(item.DecimalValue);
                x++;
            }
            wb.SaveAs(spreadsheetStream);
            spreadsheetStream.Position = 0;
            return new FileStreamResult(spreadsheetStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet") { FileDownloadName = "topNcolors.xlsx" };
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

            System.IO.Stream spreadsheetStream = new System.IO.MemoryStream();
            XLWorkbook wb = new ClosedXML.Excel.XLWorkbook();
            IXLWorksheet ws = wb.AddWorksheet();
            ws.FirstCell().SetValue("Colors by " + colorName);
            ws.Cell(2, 1).SetValue("Id");
            ws.Cell(2, 2).SetValue("Color name");
            ws.Cell(2, 3).SetValue("Hex value");
            ws.Cell(2, 4).SetValue("Decimal value");
            int x = 3;
            foreach (var item in result)
            {
                ws.Cell(x, 1).SetValue(item.Id);
                ws.Cell(x, 2).SetValue(item.Name);
                ws.Cell(x, 3).SetValue(item.HexValue);
                ws.Cell(x, 4).SetValue(item.DecimalValue);
                x++;
            }
            wb.SaveAs(spreadsheetStream);
            spreadsheetStream.Position = 0;
            return new FileStreamResult(spreadsheetStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet") { FileDownloadName = "ColorsByName.xlsx" };
        }
    }
}
