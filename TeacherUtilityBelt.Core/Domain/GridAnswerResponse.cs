namespace TeacherUtilityBelt.Core.Domain;


public class GridAnswerResponse
{
    /// <summary>
    /// 
    /// </summary>
    public string[][] Grid { get; set;}
    /// <summary>
    /// 
    /// </summary>
    public Dictionary<string, List<Coordinate>> GridAnswer  { get; set;}


    public List<string> FindWordsWithinGrid(string[][] grid)
    {
        return null;
    }

    public string[][] GenerateCrossWordGrid(int wordCount)
    {
        return null;       
    }
}