namespace RiotDotNET.Tests.ApiScraper;

using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class ApiDataType
{
    public int Id { get; set; }

    public string? Name { get; set; }

    [Ignore]
    public List<ApiDataTypeProperty> Properties { get; set; } = new();
}
