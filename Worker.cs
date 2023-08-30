
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace WorkerService1
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IWebDriver _driver;


        public Worker(ILogger<Worker> logger, ChromeDriver webDriver)
        {
            _logger = logger;
            _driver = webDriver;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                //insert code here
                _driver.Navigate().GoToUrl("https://citas.sre.gob.mx/");
                await Task.Delay(5000);

                try
                {
                    //click to dismiss popup
                    _driver.FindElement(By.XPath("/html/body/div[2]/div/main/div/div/div/div/div[1]/div/div/div/div/div/div/div/div[3]/div/div/div/button[2]")).Click();
                    await Task.Delay(5000);

                    //fill data in the input fields
                    var username = _driver.FindElement(By.XPath("/html/body/div[2]/div/main/div/div/div/div/div[4]/div[2]/div[2]/div/div/div/div[2]/form/div[1]/div/input"));
                    username.SendKeys("singhh.shivani98@gmail.com");

                    var password = _driver.FindElement(By.XPath("/html/body/div[2]/div/main/div/div/div/div/div[4]/div[2]/div[2]/div/div/div/div[2]/form/div[2]/div/input"));
                    password.SendKeys("SpaceProbe7!@#");

                    //click on checkbox
                    _driver.FindElement(By.XPath("/html/body/div[2]/div/main/div/div/div/div/div[4]/div[2]/div[2]/div/div/div/div[2]/form/div[4]/div/div/label/input")).Click();
                    await Task.Delay(500);

                    //dismiss the popup
                    _driver.FindElement(By.ClassName("bi-x-circle")).Click();
                    await Task.Delay(500);

                    //click on login button
                    _driver.FindElement(By.XPath("/html/body/div[2]/div/main/div/div/div/div/div[4]/div[2]/div[3]/div/div/div/div[2]/form/div[5]/div/div/div[2]/button")).Click();
                    await Task.Delay(2000);

                    //click on schedule button
                    _driver.FindElement(By.XPath("/html/body/div[2]/div[3]/div[3]/div[4]/a")).Click(); await Task.Delay(2000);
                    _driver.FindElement(By.ClassName("bi-x-circle")).Click();//dismiss the popup 
                    await Task.Delay(500);

                    //click on offices dropdown
                    _driver.FindElement(By.XPath("/html/body/div[2]/div[3]/div[3]/div/div/div/div[2]/div[1]/form/div[1]/div[3]/div[1]/div/div/div/div[2]")).Click();
                    await (Task.Delay(5000));

                    //click on City dropdown and get count
                    //_driver.FindElement(By.XPath("/html/body/div[2]/div[3]/div[3]/div/div/div/div[2]/div[1]/form/div[1]/div[2]/div[1]/div/div/div/div[2]")).Click(); await Task.Delay(500);

                    //count the items in dropdown
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.StackTrace);
                }

                //end

                await Task.Delay(10*60*1000, stoppingToken); //delays for 10 mins
            }
        }
    }
}