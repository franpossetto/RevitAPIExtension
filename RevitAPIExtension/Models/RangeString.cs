namespace RevitAPIExtension.Models
{
    public class RangeString
    {
        public int StartIndex { get; set; }
        public int EndIndex { get; set; }
        public RangeString(int start, int end)
        {
            this.StartIndex = start;
            this.EndIndex = end;
        }

    }
}
