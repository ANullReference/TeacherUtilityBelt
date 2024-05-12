
using TeacherUtilityBelt.Core.Domain;

namespace TeacherUtilityBelt.Core.Abstractions;

public interface IRequestManager
{
    Task<GridAnswerResponse> GenerateRandomGrid(Coordinate coordinate);

    Task<Tuple<List<string>, string[][]>>  GenerateRandomWords(int wordCOunt);
}   