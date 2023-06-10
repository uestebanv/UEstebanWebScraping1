using ML;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.DevTools.V112.Network;
using OpenQA.Selenium.Chrome;
using AngleSharp.Dom;

namespace PLConsole
{
    public class WebScraping
    {
        public static ML.Result Metodo()
        {
            ML.Result result = new ML.Result();

            //IWebDriver driver = new FirefoxDriver(@"C:/Users/digis/Documents/Ulises Esteban Valdes/UEstebanWebScraping1/Drive/geckodriver.exe");
            IWebDriver driver = new ChromeDriver(@"C:\Users\digis\Documents\Ulises Esteban Valdes\UEstebanWebScraping1\Drive\chromedriver.exe");
            driver.Navigate().GoToUrl("https://www.mercadolibre.com.mx/");

            var busqueda = driver.FindElement(By.Name("as_word"));

            busqueda.SendKeys("Maleta Jony");
            busqueda.Submit();


            var registro = driver.FindElements(By.CssSelector(".ui-search-layout__item"));


            if (registro.Count > 0)
            {
                result.Objects = new List<object>();

                foreach (var Item in registro)
                {
                    try
                    {
                        //ui - search - result - image__element shops__image - element
                        //var imagen = Item.FindElement(By.XPath("//img[@class='ui-search-result-image__element shops__image-element']"));
                        var opcion1 = driver.FindElement(By.CssSelector(".slick-slide.slick-active"));
                        var opcion2 = opcion1.FindElement(By.TagName("img"));
                        var imagen = opcion1.GetDomAttribute("src");

                        //var imagen = Item.FindElement(By.CssSelector("async")).Text;
                        //if (imagen == null)
                        //{
                        //    imagen = Item.FindElement(By.CssSelector(".ui-search-variations-picker__variation-image"));
                        //}

                        var nombre = Item.FindElement(By.CssSelector(".ui-search-item__title.shops__item-title")).Text;
                        var precio = Item.FindElement(By.CssSelector(".price-tag-amount")).Text;

                        //var vendedor = Item.FindElement(By.CssSelector(".ui-search-official-store-label.ui-search-item__group__element.shops__items-group-details.ui-search-color--GRAY p")).Text == null ? "Mercado libre" : Item.FindElement(By.CssSelector(".ui-search-official-store-label.ui-search-item__group__element.shops__items-group-details.ui-search-color--GRAY p")).Text;
                        var vendedor = Item.FindElement(By.CssSelector(".ui-search-official-store-label.ui-search-item__group__element.shops__items-group-details.ui-search-color--GRAY")).Text == null ? "Mercado libre" : "";


                        ML.Producto producto = new ML.Producto();

                        //producto.Imagen = imagen.GetAttribute("src");
                        producto.Nombre = nombre;
                        producto.Precio = precio;
                        producto.Vendedor = vendedor;

                        result.Objects.Add(producto);

                        Console.WriteLine(producto.Imagen);
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
            }
            else
            {
                Console.WriteLine("No se encontro ningun regsitro");
            }
            driver.Quit();

            Console.ReadKey();

            return result;
        }
    }
}
