using System.Text.RegularExpressions;

public class RegionData
{
    public Group Name { get; set; }
    public Group Content { get; set; }
    public int Index { get; set; }
    public int Length { get; set; }
    public RegionData(Group name, Group content, int index, int length)
    {
        Name = name;
        Content = content;
        Index = index;
        Length = length;
    }
}