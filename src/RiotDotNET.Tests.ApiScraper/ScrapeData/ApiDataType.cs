namespace RiotDotNET.Tests.ApiScraper.ScrapeData;

using CsvHelper.Configuration.Attributes;
using System.Collections.Generic;

internal class ApiDataType
{
    public int Id { get; set; }

    public string? Name { get; set; }

    [Ignore]
    public List<ApiDataTypeProperty> Properties { get; set; } = new();
}
