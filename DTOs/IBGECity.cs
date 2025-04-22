using System.Text.Json.Serialization;

public class IBGECity
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("nome")]
    public string Nome { get; set; }
}
