
public class PersonaCommandResponse
{
    public bool IsValid => !Errors.Any();
    public IDictionary<string, string> Errors { get; set; } = new Dictionary<string, string>();
}

public class PersonaCommandResponse<TResult> : PersonaCommandResponse
{
    public PersonaCommandResponse() { }

    public PersonaCommandResponse(TResult result)
    {
        Result = result;
    }

    public TResult? Result { get; set; }
}