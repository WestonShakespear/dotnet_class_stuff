using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

class Program
{
  static void Main(string[] args)
  {
    // instantiate a driver instance to control
    // Chrome in headless mode
    // ChromeOptions chromeOptions = new ChromeOptions();
    // chromeOptions.AddArguments("--headless=new"); // comment out for testing
    // chromeOptions.AddArgument("--disable-blink-features=AutomationControlled");
    // chromeOptions.AddExcludedArgument("enable-automation");
    // chromeOptions.PageLoadStrategy = PageLoadStrategy.None;
    FirefoxOptions firefoxOptions= new FirefoxOptions();

    FirefoxDriver driver = new FirefoxDriver(firefoxOptions);
    // ChromeDriver driver = new ChromeDriver(chromeOptions);

    // driver.ExecuteScript("Object.defineProperty(navigator, 'webdriver', {get: () => undefined})");

    // scraping logic...
    

    // driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(4);

    // try
    // {
    driver.Navigate().GoToUrl("https://www.nasdaq.com/market-activity/stocks/f");
  
    // // driver.ExecuteScript("window.stop();");
    // driver.Navigate().Refresh();
    // Thread.Sleep(4000);

    // }
    // catch
    // {
    //     Console.WriteLine("Stopping page");
    //     driver.ExecuteScript("window.stop();");
    // }

    string html = driver.PageSource;

    // var shadowHost = driver.FindElement(By.CssSelector("#shadow-root"));
    // var shadowRoot = shadowHost.GetShadowRoot();
    // var shadowContent = shadowRoot.FindElement(By.CssSelector("#shadow_content"));



    var table = driver.FindElements(By.CssSelector("div.table-row"));
    Console.WriteLine(table.Count);

 

    string docPath = @"C:\Users\Initec\github-repos\dotnet_class_stuff\SeleniumStuff\SeleniumTest";

    using (StreamWriter outFile = new StreamWriter(Path.Combine(docPath, "test.html")))
    {
        outFile.WriteLine(html);
    }





    // Console.WriteLine("Enter to quit");
    Console.ReadLine();
    // close the browser and release its resources
    driver.Quit();
  }
}