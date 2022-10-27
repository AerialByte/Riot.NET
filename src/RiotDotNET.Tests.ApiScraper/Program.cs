namespace RiotDotNET.Tests.ApiScraper;

using CsvHelper;
using CsvHelper.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools;
using OpenQA.Selenium.Interactions;
using System.Collections.ObjectModel;
using System.Globalization;

#pragma warning disable CS8602 // Dereference of a possibly null reference.
class Program
{
    const string apiUrl = "https://developer.riotgames.com/apis";

    static void Main(params string[] args)
    {
        if (args.Length == 1 && Path.GetExtension(args[0])?.ToLower() == ".csv")
        {
            var outDir = Path.GetDirectoryName(args[0]) ?? throw new Exception($"Could not get directory from path: '{args[0]}'");
            var outFileBase = Path.GetFileNameWithoutExtension(args[0]);

            var dataTypes = new[] { typeof(ApiDataType), typeof(ApiDataTypeProperty), typeof(ApiEndpoint), typeof(ApiEndpointMethod) };
            if (dataTypes.Any(t => !File.Exists(GetDataFile(outDir, outFileBase, t))))
            {
                ScrapeData(out var apiEndpoints, out var apiDataTypes);

                SaveData(apiEndpoints, apiDataTypes, outDir, outFileBase);
            }
        }
        else
        {
            Log("First argument must be prefix for csv files- [file].Endpoints.csv, [file].DataTypes.csv, etc.");
            Log("If the files don't exist, they will be generated.");
            Log("Second argument must specify a path to output generated code files.");
        }
    }

    static string GetDataFile(string outDir, string outFileBase, Type type) => Path.Combine(outDir, $"{outFileBase}.{type.Name}.csv");

    static void ScrapeData(out List<ApiEndpoint> apiEndpoints, out List<ApiDataType> apiDataTypes)
    {
        apiEndpoints = new();
        apiDataTypes = new();
        ChromeDriver? driver = null;
        try
        {
            Log("Setting up driver...");
            driver = new ChromeDriver
            {
                Url = apiUrl
            };
            Thread.Sleep(2500);

            Log("Loading endpoints...");
            var apiListItems = driver.FindElements(By.ClassName("list-group-item-api"));
            var validApiLinks = apiListItems.Where(item =>
            {
                var apiType = item.FindElement(By.ClassName("api_desc")).Text;
                return apiType == "League of Legends" || apiType == "Accounts RSO";
            }).ToList();

            int apiId = 1;
            int dataTypeId = 1;

            Log($"Found {validApiLinks.Count} endpoint(s)");
            foreach (var item in validApiLinks)
            {
                var apiName = item.FindElement(By.ClassName("api_option"))?.GetAttribute("api-name");
                Validate.NotNull(apiName, nameof(apiName), $"FAILURE: Could not load api-name attribute in endpoint");

                var apiCleanName = string.Join(string.Empty, apiName.Split('-').Select(x => x[0].ToString().ToUpper() + x[1..]));

                var endpoint = new ApiEndpoint
                {
                    Id = apiId++,
                    Name = apiName,
                    CleanName = apiCleanName,
                };
                apiEndpoints.Add(endpoint);

                Log($"API Name: {apiCleanName}");
                Indent();

                bool clicked = false;
                while (!clicked)
                {
                    try
                    {
                        var actions = new Actions(driver);
                        actions.MoveToElement(item);
                        actions.Build().Perform();
                        item.Click();
                        clicked = true;
                    }
                    catch (Exception)
                    {
                        Thread.Sleep(500);
                    }
                }

                var getOperations = ScrapeDataGetOperations(driver, apiName);

                foreach (var callback in getOperations)
                {
                    var id = callback.GetAttribute("id");
                    var heading = callback.FindElement(By.ClassName("heading"));
                    var content = callback.FindElement(By.ClassName("content"));

                    if (id == null || heading == null || content == null)
                    {
                        continue;
                    }

                    var pathElement = heading?.FindElement(By.ClassName("path"));
                    pathElement?.Click();
                    Thread.Sleep(1500);

                    var path = pathElement
                        ?.FindElement(By.TagName("a"))
                        ?.Text;
                    Validate.NotNull(path, nameof(path), $"FAILURE: Could not find path for callback: '{id}' under api-name '{apiName}'");

                    Log($"Path: {path}");
                    Indent();

                    var description = heading
                        ?.FindElement(By.ClassName("options"))
                        ?.FindElement(By.TagName("a"))
                        ?.Text;
                    Validate.NotNull(description, nameof(description), $"FAILURE: Could not find description for callback: '{id}' under api-name '{apiName}'");

                    Log($"Description: {description}");

                    var apiBlockClasses = content.FindElements(By.ClassName("api_block"));
                    var responseBlock = apiBlockClasses
                        .Where(apiBlock =>
                        {
                            var header = apiBlock.FindElements(By.TagName("h4")).FirstOrDefault();
                            var headerText = header?.Text;
                            if (headerText != null && header.Text.Equals("Response Classes", StringComparison.OrdinalIgnoreCase))
                            {
                                return true;
                            }

                            return false;
                        })
                        .SingleOrDefault();
                    Validate.NotNull(responseBlock, nameof(responseBlock), $"FAILURE: Could not find response block for path '{path}' in callback: '{id}' under api-name '{apiName}'");

                    var responseType = responseBlock.FindElements(By.TagName("h4"))
                        .SingleOrDefault(x => x.Text.StartsWith("return value:", StringComparison.OrdinalIgnoreCase))
                        ?.Text.Replace("return value:", string.Empty, StringComparison.OrdinalIgnoreCase).Trim();
                    if (responseType.EndsWith("dto", StringComparison.OrdinalIgnoreCase))
                    {
                        responseType = responseType[..^3];
                    }
                    responseType += "Dto";
                    Log($"Response: {responseType}");

                    var apiMethod = new ApiEndpointMethod
                    {
                        Name = id,
                        Description = description,
                        Path = path,
                        EndpointId = endpoint.Id,
                        ResponseType = responseType
                    };
                    endpoint.Methods.Add(apiMethod);

                    foreach (var dataTypeBlock in responseBlock.FindElements(By.CssSelector("div.block.response_body")))
                    {
                        var dtHeading = dataTypeBlock.FindElements(By.TagName("h5"))
                            .FirstOrDefault()
                            ?.Text.Replace("dto", string.Empty, StringComparison.OrdinalIgnoreCase);
                        if (dtHeading == null)
                        {
                            continue;
                        }
                        dtHeading += "Dto";

                        Log($"Data Type: {dtHeading}");

                        var dataType = new ApiDataType
                        {
                            Id = dataTypeId++,
                            Name = dtHeading,
                        };

                        Indent();
                        var dtoRows = dataTypeBlock.FindElements(By.CssSelector("tbody tr"));
                        foreach (var dtoField in dtoRows)
                        {
                            var dtoProp = new ApiDataTypeProperty
                            {
                                DataTypeId = dataType.Id
                            };

                            Indent();
                            var fieldMetaData = dtoField.FindElements(By.TagName("td"));
                            if (fieldMetaData.Count > 1)
                            {
                                dtoProp.Name = fieldMetaData[0]?.Text;
                                dtoProp.Type = fieldMetaData[1]?.Text;
                                dataType.Properties.Add(dtoProp);

                                Log($"Field: {dtoProp.Name}");
                                Indent();
                                Log($"Type: {dtoProp.Type}");
                                Unindent();
                            }
                            if (fieldMetaData.Count > 2)
                            {
                                dtoProp.Description = fieldMetaData[2]?.Text;
                                Indent();
                                Log($"Description: {dtoProp.Description}");
                                Unindent();
                            }
                            Unindent();
                        }
                        Unindent();
                    }

                    pathElement?.Click();
                    Thread.Sleep(1500);

                    Unindent();
                }
                Unindent();
            }
        }
        catch (Exception ex)
        {
            Log(ex.Message);
            Environment.Exit(1);
        }
        finally
        {
            driver?.Quit();
        }
    }

    static ReadOnlyCollection<IWebElement> ScrapeDataGetOperations(ChromeDriver driver, string apiName)
    {
        ReadOnlyCollection<IWebElement>? getOperations = null;

        while (getOperations == null)
        {
            var content = driver.FindElements(By.CssSelector(".api_detail.inner_content"))
                        .Where(x => x.GetAttribute("api-name") == apiName)
                        .SingleOrDefault();

            if (content == null)
            {
                Thread.Sleep(500);
                continue;
            }

            getOperations = content.FindElements(By.CssSelector("ul.operations li.get.operation"));
            if (getOperations == null)
            {
                Thread.Sleep(500);
                continue;
            }

            if (!getOperations.Any())
            {
                getOperations = null;
            }
        }

        return getOperations;
    }

    static void SaveData(List<ApiEndpoint> apiEndpoints, List<ApiDataType> apiDataTypes, string outDir, string outFileBase)
    {
        try
        {
            SaveDataFile(outDir, outFileBase, apiEndpoints);
            SaveDataFile(outDir, outFileBase, apiEndpoints.SelectMany(endpoint => endpoint.Methods));
            SaveDataFile(outDir, outFileBase, apiDataTypes);
            SaveDataFile(outDir, outFileBase, apiDataTypes.SelectMany(dto => dto.Properties));
        }
        catch (Exception ex)
        {
            Log("Failed writing output");
            Log(ex.ToString());
        }
    }

    static void SaveDataFile<T>(string outDir, string outFileBase, IEnumerable<T> data)
    {
        string fileName = GetDataFile(outDir, outFileBase, typeof(T));
        Directory.CreateDirectory(Path.GetDirectoryName(fileName)!);

        var config = new CsvConfiguration(CultureInfo.CurrentCulture);
        using TextWriter textWriter = File.CreateText(fileName);
        using var csvWriter = new CsvWriter(textWriter, config);

        Log($"Writing out {typeof(T).Name} file: {fileName}");
        csvWriter.WriteRecords(data);
    }


    static int indentLevel = 0;

    static void Indent(int count = 1) => indentLevel += count;

    static void Unindent(int count = 1) => indentLevel -= count;

    static void Log(string message) => Console.WriteLine("{1}{0}", message, new string('\t', indentLevel));
}