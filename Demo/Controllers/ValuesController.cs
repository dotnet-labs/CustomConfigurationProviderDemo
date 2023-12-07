namespace Demo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ValuesController(IConfiguration configuration) : ControllerBase
{
    private readonly Dictionary<string, string> _configs = Keys.ToDictionary(k => k, k => configuration[k] ?? string.Empty);
    private static readonly IEnumerable<string> Keys = new List<string> { "MyKey1", "MyKey2" };

    [HttpGet("")]
    public IEnumerable<string> Get()
    {
        return _configs.Values;
    }

    [HttpGet("{key}")]
    public string GetValueByKey(string key)
    {
        _configs.TryGetValue(key, out var value);
        return value ?? string.Empty;
    }
}