namespace RiotDotNET.Tests.ApiScraper;

using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class ApiEndpointMethod
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Path { get; set; }

    public string? Description { get; set; }

    public string? ResponseType { get; set; }

    public int EndpointId { get; set; }
}
