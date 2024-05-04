namespace TeacherUtilityBelt.Core.Domain;

public class AppSettings
{
    public virtual int CacheTimeout {get; set;}

    public virtual int FoundWordMinCount {get; set;}


    public virtual int MiinimumWordLengthAcceptable {get; set;} = 4;
}
