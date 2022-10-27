namespace RiotDotNET.Tests.ApiScraper;

using CsvHelper.Configuration.Attributes;

internal class ApiEndpoint
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? CleanName { get; set; }

    [Ignore]
    public List<ApiEndpointMethod> Methods { get; set; } = new();
}