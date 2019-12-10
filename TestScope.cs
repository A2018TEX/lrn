using Laren.E2ETests.Core.Framework;
using Laren.E2ETests.Core.Framework.DbAccess;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Web;

namespace Laren.E2ETests
{
    public class TestScope<T> : IDisposable where T : class
    {
        private static SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(3);
        private Action<TestScope<T>> _beforeTest;
        private Action<TestScope<T>> _afterTest;

        private IWebDriver _driver;
        public string BrowserName { get; set; }
        public DataBaseConnection DbConnection { get; set; }
        public IConfiguration Configuration { get; set; }
        public HelperSetUp HelperSet { get; set; }
        public string UserEmail { get; set; }
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public T Context { get; set; }
        public IWebDriver Driver
        {
            get
            {
                if (_driver != null)
                {
                    return _driver;
                }
                switch (BrowserName)
                {
                    case "chrome":

#if !DEBUG
                        var capabilities = new DesiredCapabilities();
                        capabilities.SetCapability("browserName", "chrome");
                        _driver = new RemoteWebDriver(
                          new Uri(Configuration.GetSection("HubUrl").Value), capabilities);
#endif
#if DEBUG
                        var chromeOptions = new ChromeOptions();
                        string path = AppDomain.CurrentDomain.BaseDirectory;
                        chromeOptions.AddArguments(new List<string>() {
                            "--no-sandbox",/*"--headless", */"--window-size=1920x1080"});
                        chromeOptions.AddUserProfilePreference("download.default_directory", $@"{path}");
                        chromeOptions.AddUserProfilePreference("intl.accept_languages", "nl");
                        chromeOptions.AddUserProfilePreference("disable-popup-blocking", "true");

                        //_driver = new ChromeDriver(Directory.GetCurrentDirectory(), chromeOptions);
                        _driver = new RemoteWebDriver(
                          new Uri(Configuration.GetSection("HubUrl").Value), chromeOptions);

                        IAllowsFileDetection allowFile = _driver as IAllowsFileDetection;
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
        }

        public TestScope(IConfiguration configuration,
            Action<TestScope<T>> beforeEachTest = null,
            Action<TestScope<T>> afterEachTest = null,
            T context = null)
        {
            Thread.Sleep(1000);
            _semaphoreSlim.Wait();
            Context = context is null ? Activator.CreateInstance(typeof(T)) as T : context;
            Configuration = configuration;
            _beforeTest = beforeEachTest;
            _afterTest = afterEachTest;
            BrowserName =
            HttpUtility.ParseQueryString(TestContext.CurrentContext.WorkDirectory.Split("?").LastOrDefault()).Get("browser")
            ?? "chrome";
            DbConnection = new DataBaseConnection(Configuration.GetSection("DBConn").Value);
            HelperSet = new HelperSetUp(Driver, DbConnection, Configuration);
            BeforeTest();
        }

        private void BeforeTest()
        {
            if (_beforeTest == null)
            {
                return;
            }
            Environment.CurrentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            _beforeTest(this);
        }

        private void AfterTest()
        {
            _afterTest?.Invoke(this);
            try
            {
                _driver.Quit();
            }
            finally
            {
                if (_driver != null)
                {
                    _driver.Quit();
                }
                _semaphoreSlim.Release();
            }
        }



        [Obsolete]
        public void Dispose()
        {
            AfterTest();
        }
    }
}
