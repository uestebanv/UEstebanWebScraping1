using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace BL
{
    public class WebScraping
    {
        public static ML.Result Metodo()
        {
            ML.Result result = new ML.Result();

            IWebDriver driver = new ChromeDriver();
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
                        var imagen = Item.FindElement(By.CssSelector("img"));

                        //if (imagen == null)
                        //{
                        //    imagen = Item.FindElement(By.CssSelector(".ui-search-variations-picker__variation-image"));
                        //}

                        var nombre = Item.FindElement(By.CssSelector(".ui-search-item__title.shops__item-title")).Text;
                        var precio = Item.FindElement(By.CssSelector(".price-tag-amount")).Text;

                        var vendedor = Item.FindElement(By.CssSelector(".ui-search-official-store-label.ui-search-item__group__element.shops__items-group-details.ui-search-color--GRAY")).Text;

                        ML.Producto producto = new ML.Producto();

                        producto.Imagen = imagen.GetAttribute("src");
                        producto.Nombre = nombre;
                        producto.Precio = precio;
                        producto.Vendedor = vendedor;

                        result.Objects.Add(producto);
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                    result.Correct = true;
                }
            }
            else
            {
                result.Correct = false;

            }
            driver.Quit();
            return result;
        }
    }
}
