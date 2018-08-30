using System.Drawing;

namespace TessaractTemplates
{
    public class OcrWord
    {
        public Rectangle CoOrdinate { get; set; }
        public string Word { get; set; }
        public char[] Characters { get; set; }
        public int[] Confidence { get; set; }
    }
}