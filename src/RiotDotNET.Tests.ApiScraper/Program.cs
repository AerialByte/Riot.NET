namespace RiotDotNET.Tests.ApiScraper;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools;
using OpenQA.Selenium.Interactions;
using System.Collections.ObjectModel;

class Program
{
    const string apiUrl = "https://developer.riotgames.com/apis";

    static void Main()
    {
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

            Log($"Found {validApiLinks.Count} endpoint(s)");
            foreach (var item in validApiLinks)
            {

                var apiName = item.FindElement(By.ClassName("api_option"))?.GetAttribute("api-name");
                if (apiName == null)
                {
                    throw new Exception($"FAILURE: Could not load api-name attribute in endpoint");
                }

                var apiCleanName = string.Join(string.Empty, apiName.Split('-').Select(x => x[0].ToString().ToUpper() + x[1..]));

                Log($"API Name: {apiCleanName}");
                Indent();

                bool clicked = false;
                while (!clicked)
                {
                    try
                    {
                        item.Click();
                        clicked = true;
                    }
                    catch
                    {
                        var actions = new Actions(driver);
                        actions.MoveToElement(item);
                        actions.Perform();
                        Thread.Sleep(500);
                    }
                }

                var getOperations = LoadGetOperations(driver, apiName);

                foreach (var callback in getOperations)
                {
                    var id = callback.GetAttribute("id");
                    if (id == null)
                    {
                        continue;
                    }

                    var path = callback.FindElement(By.ClassName("heading"))
                        ?.FindElement(By.ClassName("path"))
                        ?.FindElement(By.TagName("a"))
                        ?.Text;

                    if (path == null)
                    {
                        throw new Exception($"FAILURE: Could not find path for callback: '{id}' under api-name '{apiName}'");
                    }

                    Log($"Endpoint: {path}");
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
            driver?.Close();
            driver?.Quit();
            driver?.Dispose();
        }
    }

    static ReadOnlyCollection<IWebElement> LoadGetOperations(ChromeDriver driver, string apiName)
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

    static int indentLevel = 0;

    static void Indent(int count = 1) => indentLevel += count;

    static void Unindent(int count = 1) => indentLevel -= count;

    static void Log(string message) => Console.WriteLine("{1}{0}", message, new string('\t', indentLevel));
}