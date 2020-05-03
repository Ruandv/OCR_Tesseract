namespace OCR_API.Models
{
    public class Region
    {
        public int Index { get; set; }
        public string RegionType { get; set; }
        public string Description { get; set; }
        public Point TopLeft { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
    public class Point
    {
        public decimal X { get; set; }
        public decimal Y { get; set; }
    }
}
