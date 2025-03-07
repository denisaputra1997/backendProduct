using Newtonsoft.Json;

namespace mybackend.Models;

public class User
{
    [JsonProperty("OperatorId")]
    public long OperatorId { get; set; }

    [JsonProperty("Nama")]
    public string Nama { get; set; }

    [JsonProperty("Username")]
    public string Username { get; set; }

    [JsonProperty("Password")]
    public string Password { get; set; }

    [JsonProperty("TanggalBergabung")]
    public DateTime TanggalBergabung { get; set; }

    [JsonProperty("CreatedAt")]
    public DateTime CreatedAt { get; set; }

    [JsonProperty("UpdatedAt")]
    public DateTime UpdatedAt { get; set; }

    [JsonProperty("CreatedBy")]
    public long CreatedBy { get; set; }

    [JsonProperty("ModifiedBy")]
    public long ModifiedBy { get; set; }

    [JsonProperty("Status")]
    public string Status { get; set; }

}
