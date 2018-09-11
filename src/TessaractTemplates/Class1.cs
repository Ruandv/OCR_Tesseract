using System.Collections.Generic;
using System.Drawing;

namespace TessaractTemplates
{
       //-----------------------------------------------------------------------------------------
    public class TessaractTemplate
    {
        public string Name { get;set;}
        public Rectangle[] CoOrdinates { get; set; }
        public List<OcrWord> Results { get; set; }
    }
}
