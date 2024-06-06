namespace TwitterAppPrototype.Api.Configurations.Options;

public class TwitterAppAccessOptions
{
    public string? RedirectUrl { get; set; }
    
    //Oauth2.0
    public string? ClientId {  get; set; }
    public string? ClientSecret {  get; set; }

    //Oauth1.0
    public string? ApiKey { get; set; }
    public string? ApiKeySecret { get; set; }
}