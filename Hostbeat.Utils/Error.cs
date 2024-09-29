namespace Hostbeat.Utils;

public sealed class Error(string code, string description)
{
    public string Code { get; set; } = code;
    public string Description { get; set; } = description;
    
    public static readonly Error None =
        new(string.Empty, string.Empty);

    public static readonly Error Unauthorized =
        new("Generic.Unauthorized", "Auth token not supplied or expired");
    
    public static readonly Error Unknown 
        = new("Generic.UnknownError", "Not controlled error happened");

    public static readonly Error Connectivity 
        = new("Generic.ConnectivityError", "Error connecting to the server");
}