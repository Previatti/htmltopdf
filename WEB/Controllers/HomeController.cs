using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WEB.Models;
using IronPdf;
using System.IO;
using System.Net;

namespace WEB.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var m = PdfGenerator();
            return File(m, "application/pdf","teste.pdf");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public Stream PdfGenerator()
        {
            var Renderer = new HtmlToPdf();
            var htmlString = GetEmailTemplate("https://www1.folha.uol.com.br/ultimas-noticias/");
            var PDF = Renderer.RenderHtmlAsPdf(htmlString);
            return PDF.Stream;
        }


        protected string GetEmailTemplate(string url)
        {
            var body = "";
            var webRequest = WebRequest.Create($"{url}");

            using (var response = webRequest.GetResponse())
            {
                using (var content = response.GetResponseStream())
                {

                    using (var reader = new StreamReader(content))
                    {
                        body = reader.ReadToEnd();
                    }
                }
            }
            return body;
        }
    }
}
