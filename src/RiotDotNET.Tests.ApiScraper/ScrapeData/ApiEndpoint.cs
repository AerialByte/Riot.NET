namespace RiotDotNET.Tests.ApiScraper.ScrapeData;

using CsvHelper.Configuration.Attributes;

internal class ApiEndpoint
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? CleanName { get; set; }

    [Ignore]
    public List<ApiEndpointMethod> Methods { get; set; } = new();
}