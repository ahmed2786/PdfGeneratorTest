using IronPdf;
using Microsoft.AspNetCore.Mvc;

namespace PdfGeneratorDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        [HttpGet(Name = "/api/[controller]/[action]")]
        public IActionResult DownloadChartPdf()
        {
            SavePdf();

            string path = Path.Combine(Environment.CurrentDirectory, @"Pdf\", "js-chart.pdf");

            if (System.IO.File.Exists(path))
            {
                return File(System.IO.File.OpenRead(path), "application/octet-stream", Path.GetFileName(path));
            }
            return NotFound();
        }
        [NonAction]
        public void SavePdf()
        {
            string path = Path.Combine(Environment.CurrentDirectory, @"Html\", "htmlpage.html");
            string htmlWithJs = System.IO.File.ReadAllText(path);

            string json = @"{
                    columns: [
                        ['data1', 30, 200, 100, 400, 150, 250],
                        ['data2', 50, 20, 10, 40, 15, 25]
                    ]
                }";

            htmlWithJs = htmlWithJs.Replace("@JsonData", json);

            // Instantiate Renderer
            var htmlToPdf = new NReco.PdfGenerator.HtmlToPdfConverter();
            var pdf = htmlToPdf.GeneratePdf(htmlWithJs);

            var pdfFilePath = Path.Combine(Environment.CurrentDirectory, @"Pdf\", "js-chart.pdf");
            // Export to a file or Stream
            System.IO.File.WriteAllBytes(pdfFilePath, pdf);
        }
    }
}
