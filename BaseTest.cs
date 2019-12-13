using Laren.E2ETests.Core.Framework;
using Laren.E2ETests.Core.Framework.DbAccess;
using Laren.E2ETests.Core.Models;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;

namespace Laren.E2ETests
{
    public class BaseTest /*: AllureReport*/
    {
        private IWebDriver _driver;
        private DataBaseConnection _dbConn;
        private IConfiguration _configuration;
        private HelperSetUp _helperSetUp;
        private WebDriverWait _waiter;
        protected WebDriverWait DbWait => _waiter ?? (_waiter = new WebDriverWait(_driver, TimeSpan.FromSeconds(10)));
        protected DataBaseConnection DbConn => _dbConn ?? (_dbConn = new DataBaseConnection(Configuration.GetSection("DBConn").Value));

        public HelperSetUp HelperSet => _helperSetUp ?? (_helperSetUp = new HelperSetUp(_driver, _dbConn, _configuration));
        public IConfiguration Configuration 
        {
            get
            {
                _configuration = new ConfigurationFactory().CreateInstance();
                return _configuration;
            } 
        }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public string ClosingFileNumber { get; set; }
        [Obsolete]
        public IWebDriver Driver
        {
            get
            {
                if (_driver != null)
                {
                    return _driver;
                }
                if (_driver == null)
                {
                    var prop = Configuration.GetSection("DriverName").Value;
                    string driverName = prop != null ? prop : string.Empty;

                    switch (driverName)
                    {
                        case "chrome":

#if !DEBUG
                         var chromeOptions = new ChromeOptions();
                        var path = Directory.GetCurrentDirectory();

                        chromeOptions.AddUserProfilePreference("download.default_directory", path);
                        chromeOptions.AddUserProfilePreference("disable-popup-blocking", "true");
                        chromeOptions.AddAdditionalCapability("enableVNC", true, true);
                        chromeOptions.AddAdditionalCapability("sessionTimeout", "2m", true);

                        _driver = new RemoteWebDriver(new Uri(Configuration.GetSection("HubUrl").Value), chromeOptions/*, TimeSpan.FromSeconds(80)*/);

                        //_driver = new ChromeDriver(Directory.GetCurrentDirectory());
                        var allowFile = _driver as IAllowsFileDetection;
                        if (allowFile != null)
                        {
                            allowFile.FileDetector = new LocalFileDetector();
                        }
                        return _driver;
#endif
#if DEBUG

                            var chromeOptions = new ChromeOptions();
                            var path = Directory.GetCurrentDirectory();

                            chromeOptions.AddUserProfilePreference("download.default_directory", path);
                            chromeOptions.AddUserProfilePreference("disable-popup-blocking", "true");
                            chromeOptions.AddAdditionalCapability("enableVNC", true, true);
                            chromeOptions.AddAdditionalCapability("sessionTimeout", "2m", true);

                            _driver = new RemoteWebDriver(new Uri(_configuration.GetSection("HubUrl").Value), chromeOptions/*, TimeSpan.FromSeconds(80)*/);

                            //_driver = new ChromeDriver(Directory.GetCurrentDirectory());
                            var allowFile = _driver as IAllowsFileDetection;
                            if (allowFile != null)
                            {
                                allowFile.FileDetector = new LocalFileDetector();
                            }
                            return _driver;
#endif

                        case "firefox":
                            _driver = new FirefoxDriver(Directory.GetCurrentDirectory());
                            return _driver;

                        default:
                            throw new Exception("Incorrect driver name.");
                    }
                }
                return _driver;
            }
        }

        [OneTimeSetUp]
        public void BeforeAllTests()
        {
            _dbConn = new DataBaseConnection(Configuration.GetSection("DBConn").Value);
            Driver.Manage().Window.Maximize();
            var tempUser = new User()
            {
                Email = $"email{new Random().Next(200, 500000)}@gmail.com",
                Password = "QWE123!!",
                ClosingFileNumber = $"CF{new Random().Next(200, 500000)}"
            };

            var id = HelperSet.CreateNewUser(tempUser.Email);
            ClosingFileNumber = tempUser.ClosingFileNumber;
            HelperSet.InsertClosingFileToDB(tempUser.Email, tempUser.ClosingFileNumber);
            UserPassword = tempUser.Password;
            UserEmail = tempUser.Email;
            UserId = id;
            HelperSet.SignOut();

        }

        [OneTimeTearDown]
        public void AfterAllTests()
        {
            _driver.Close();
            _driver.Quit();
            HelperSet.DeleteAllTestData(UserEmail);
            _dbConn.CloseConnection();
        }

    }
}


