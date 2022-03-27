using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;

namespace Selenium
{
    class Program
    {
        static void Main(string[] args)
        {
            //string agent = "--user-agent=Mozilla/5.0 (iPhone; CPU iPhone OS 10_3 like Mac OS X) AppleWebKit/602.1.50 (KHTML, like Gecko) CriOS/56.0.2924.75 Mobile/14E5239e Safari/602.1";

            var arg = new ChromeOptions();
            arg.EnableMobileEmulation("iPhone SE");

            var agent = new ChromeDriver(@"D:\Code\C#\Selenium\Selenium\src",arg);

            agent.Navigate().GoToUrl("https://m.2gis.ru/vladivostok/search/Поесть");

            //open slider

            //var openElem = agent.FindElement(By.XPath("//div[@class='searchResults__content searchResults___noScroll']"));
            //openElem.Click();/*

            var js = (IJavaScriptExecutor)agent;
            js.ExecuteScript("document.getElementsByClassName('searchResults__content searchResults___noScroll")[0].click()");

            var elem = agent.FindElement(By.XPath("//div[@class='preloader__container']"));
            



            Thread.Sleep(3000);

            OpenQA.Selenium.Interactions.Actions act = new OpenQA.Selenium.Interactions.Actions(agent);
            act.MoveToElement(elem);
            act.Perform();

            var js = "window.scrollTo(0,1000)";
            agent.ExecuteScript("scroll(0,1300)");

            Thread.Sleep(3000);
            elem = agent.FindElement(By.XPath("//div[@class='preloader__container']"));
            act.MoveToElement(elem);
            act.Perform();
            Thread.Sleep(3000);




            //var js = "window.scrollTo(0,1000)";
            //agent.ExecuteScript("scroll(0,1300)");

            Console.WriteLine(agent.PageSource);

            //agent.GetAttribute("innerHTML");

            

            agent.Quit();
            */

        }
    }
}
