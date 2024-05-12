using TeacherUtilityBelt.Core.Abstractions;
using TeacherUtilityBelt.Core.Domain;
using TeacherUtilityBelt.Infrastructure;
using Moq;

using Microsoft.Extensions.Options;

namespace TeacherUtilityBelt.Test;

public class UnitTestGridHelper
{
    private Mock<IWordDictionary> wordDictionary;
    private Mock<IOptions<AppSettings>> options;


    public UnitTestGridHelper()
    {
        wordDictionary = new Mock<IWordDictionary>();
        options = new Mock<IOptions<AppSettings>>();

    }


    private GridHelper BuildSystemUnderTest()
    {
        return new GridHelper(wordDictionary.Object,  options.Object);
    }


    [Theory]
    [InlineData(0)]
    [InlineData(int.MinValue)]
    public async Task GenerateRandomWordsTest_GenerateRandomWord_ArgumentException(int maxWordCount)
    {
        IDictionary<string, string> d = new Dictionary<string, string>
        {
            { "words", "Characters stuck together" },
            { "words1", "Characters stuck together" },
            { "words2", "Characters stuck together" },
            { "words3", "Characters stuck together" }
        };

        wordDictionary.Setup(s => s.GetWordDictionary(It.IsAny<string>())).Returns(Task.FromResult(d));


        var sut = BuildSystemUnderTest();   
        await Assert.ThrowsAsync<ArgumentException>(() => sut.GenerateRandomAnswers(maxWordCount)) ;
    }

    [Theory]
    [InlineData(0)]
    [InlineData(int.MinValue)]
    public async Task Test_GenerateRandomWord_ArgumentExceptionMessage(int maxWordCount)
    {
        IDictionary<string, string> d = new Dictionary<string, string>
        {
            { "words", "Characters stuck together" },
            { "words1", "Characters stuck together" },
            { "words2", "Characters stuck together" },    
            { "words3", "Characters stuck together" }    
        };

            wordDictionary.Setup(s => s.GetWordDictionary(It.IsAny<string>())).Returns(Task.FromResult(d));


        var sut = BuildSystemUnderTest();   
        string errorMessage = "";

        try
        {
            var response = await sut.GenerateRandomAnswers(maxWordCount);
        }   
        catch(Exception e)
        {
            errorMessage = e.Message;
        } 
     
        Assert.True(errorMessage.Equals( "Parameter {maxwordCount} equal to or less than 0"));
    }

    [Theory]
    [InlineData(2)]
    public async Task Test_GenerateRandomWord_Ok(int maxWordCount)
    {

        options.Setup(s => s.Value.MiinimumWordLengthAcceptable).Returns(maxWordCount);        

        IDictionary<string, string> d = new Dictionary<string, string>
        {
            { "words", "Characters stuck together" },
            { "words1", "Characters stuck together" },
            { "words2", "Characters stuck together" },    
            { "words3", "Characters stuck together" }    
        };

        wordDictionary.Setup(s => s.GetWordDictionary(It.IsAny<string>())).Returns(Task.FromResult(d));

        var sut = BuildSystemUnderTest();   
        string errorMessage = "";

        var response = await sut.GenerateRandomAnswers(maxWordCount);
     
        Assert.Equal(maxWordCount, response.Count );
    }
}