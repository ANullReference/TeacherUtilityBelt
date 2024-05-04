using Microsoft.Extensions.Options;
using TeacherUtilityBelt.Core.Abstractions;
using TeacherUtilityBelt.Core.Domain;

namespace TeacherUtilityBelt.Infrastructure;

public class GridHelper : IGridHelper
{
    private IWordDictionary _wordDictionary;
    private AppSettings _appSettings;

    public GridHelper(IWordDictionary wordDictionary, IOptions<AppSettings> options)
    {
        _wordDictionary = wordDictionary;
        _appSettings = options.Value; 
    }

    public async Task<string[][]> GenerateRandomGrid( Coordinate coordinate)
    {
        string[][] s = new string[coordinate.Y][];
        Random random = new Random();

        for (short i = 0; i < coordinate.Y; i++)
        {
            s[i] = new string[coordinate.X];

            for (short j = 0; j < coordinate.X; j++)
            {
                Alphabet a = (Alphabet)random.Next(0,25);
                s[i][j] = a.ToString();
            }
        }

        return await Task.FromResult(s);
    }

    /// <summary>
    /// Generate random words from dictionnary
    /// </summary>
    /// <param name="maxWordCount"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception> <summary>
    /// 
    /// </summary>
    /// <param name="maxWordCount"></param>
    /// <returns></returns>
    public async Task<List<string>> GenerateRandomAnswers(int maxWordCount)
    {
        var words = await _wordDictionary.GetWordDictionary("en");

        if (maxWordCount <= 0)
        {
            throw new ArgumentException("Parameter {maxwordCount} equal to or less than 0");
        }

        var randomWords = words
                            .Select(s => s.Key)
                            .Where(w => w.Length >= _appSettings.MiinimumWordLengthAcceptable)
                            .OrderBy(o => Guid.NewGuid())
                            .Take(_appSettings.MiinimumWordLengthAcceptable)
                            .ToList();                                    
                            
                                    
        return await Task.FromResult( randomWords );
    }
}
