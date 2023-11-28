namespace Domain;

public class SurveyPublication: Publication
{
    public string Survey { get; set; }
    private List<string> Answers { get; set; }
}