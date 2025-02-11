using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Rainfall_API.Drivers
{
    public class Driver
    {
        // Public property to hold the ChromeDriver instance.
        public IWebDriver WebDriver { get; }

        // Constructor initializes the ChromeDriver.
        public Driver()
        {
            WebDriver = new ChromeDriver();
        }

        // A simple method to close the driver when you're finished.
        public void Close()
        {
            WebDriver.Quit();
        }
    }
}