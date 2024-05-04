using TeacherUtilityBelt.Core.Domain;

namespace TeacherUtilityBelt.Core.Abstractions;

public interface IGridHelper
{
    public Task<string[][]> GenerateRandomGrid(Coordinate coordinate);

    public Task<List<string>> GenerateRandomAnswers(int maxWordCount);
}
