using System;

namespace PdfApi
{
    public class HtmlModel
    {
        public string Html { get; set; }
        
        public string HeaderTemplate { get; set; }
        public string FooterTemplate { get; set; }

        public string MarginTop { get; set; }
        public string MarginBottom { get; set; }
        public string MarginLeft { get; set; }
        public string MarginRight { get; set; }
    }
}