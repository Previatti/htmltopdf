using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using IronPdf;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<FileStream> Get(int id)
        {
            var m = PdfGenerator();
            return File(m, "application/pdf", "teste.pdf");
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
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
