using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System.IO;
using OpenQA.Selenium.Support.UI;
using System.Threading;
using OpenQA.Selenium.Chrome;

namespace SteamBot2
{
    public partial class Form1 : Form
    {
        IWebDriver Browser;

        public Form1()
        {
           

            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Browser = new OpenQA.Selenium.Chrome.ChromeDriver();
            Browser.Navigate().GoToUrl("https://steamcommunity.com/login/home/?goto="); //переход по ссылке
            IWebElement textBox; // создаю веб переменную 
            textBox = Browser.FindElement(By.Name("username")); // имя элемента 
            textBox.SendKeys("Fyri32"); // то что вводим
            textBox = Browser.FindElement(By.Name("password")); // имя элемента 
            textBox.SendKeys("Qazwsd123"); // сам прасак который вводим
            IWebElement click = Browser.FindElement(By.XPath("//*[@class='btn_blue_steamui btn_medium login_btn']/span")); // путь к кнопке ВОЙТИ
            click.Click();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (var line in File.ReadAllLines("LogFull.txt")) // открывает файл с вещами
            {
                Browser.Navigate().GoToUrl("https://steamcommunity.com/market/");// заходим на торговую площадку
                IWebElement click = Browser.FindElement(By.Name("q")); // поле поиска вещей 
                click.Click();
                System.Threading.Thread.Sleep(2000);
                var element = Browser.FindElement(By.Id("findItemsSearchBox")); // присваеваем этому полю значение элемента

                element.SendKeys(line);// береме елемента line с файла Log и вставляем в поле
                IWebElement knop = Browser.FindElement(By.Id("findItemsSearchSubmit")); // кликаем на поиск предмета
                knop.Click();

                string produktS = "";
                string produktS2 = "";
                string Max_prod = textBox1.Text;
                int Maxprod = Convert.ToInt32(Max_prod);
                IWebElement produkt = Browser.FindElement(By.XPath(@"//*[@id='result_0']/div[1]/div[1]/span/span"));

                produktS = produkt.Text;
                produktS2 = produktS.Replace(",", "");
                int produktInt = Convert.ToInt32(produktS2);
                if (Maxprod <= produktInt)
                {
                    string produktNameS = "";
                    IWebElement produktName = Browser.FindElement(By.XPath(@" //*[@id='result_0_name']"));

                    produktNameS = produktName.Text;
                    File.AppendAllText(@"Prod800.txt", produktNameS + "\n");  // запись этой вещи в файл
                    System.Threading.Thread.Sleep(1000);
                }
                


              ///  System.Threading.Thread.Sleep(2000);
               //  click = Browser.FindElement(By.Id("result_0")); // первы в списке товаров
 //click.Click();

            }

            }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (var line in File.ReadAllLines("Prod.txt")) // открывает файл с вещами
            {
                double WalletFF;
                double PricefloatF;
                string DivText3;
                string DivText4;
                Browser.Navigate().GoToUrl("https://steamcommunity.com/market/");// заходим на торговую площадку
                IWebElement click = Browser.FindElement(By.Name("q")); // поле поиска вещей 
                click.Click();
                System.Threading.Thread.Sleep(2000);
                var element = Browser.FindElement(By.Id("findItemsSearchBox")); // присваеваем этому полю значение элемента

                element.SendKeys(line);// береме елемента line с файла Log и вставляем в поле
                IWebElement knop = Browser.FindElement(By.Id("findItemsSearchSubmit")); // кликаем на поиск предмета
                knop.Click();
                System.Threading.Thread.Sleep(2000);
                click = Browser.FindElement(By.Id("result_0")); // первы в списке товаров
                click.Click();
                string Max_prise = textBox2.Text;   // количество с текст 
                double Max_priseD = Single.Parse(Max_prise);
                String Wille = "";

                System.Threading.Thread.Sleep(4000);
                IWebElement WilleWeb = Browser.FindElement(By.XPath(" //*[@id='marketWalletBalanceAmount']")); 
                Wille = WilleWeb.Text;
                string WalletSS = System.Text.RegularExpressions.Regex.Match(Wille, @"[0-9]+\,?[0-9]*").Value; // кошеек 
                float WalletF = Single.Parse(WalletSS);
                WalletFF = (double)Math.Round(WalletF, 2);

                string DivText2 = "";
                IWebElement textfields = Browser.FindElement(By.XPath(@"//*[@id='market_commodity_buyrequests']/span[2]"));
                DivText2 = textfields.Text;
                string priseStr = System.Text.RegularExpressions.Regex.Match(DivText2, @"[0-9]+\,?[0-9]*").Value; 
                float Pricefloat = Single.Parse(priseStr); //  спизженая цена Минимальная ( селва )
                PricefloatF = (double)Math.Round(Pricefloat, 2);

                if (PricefloatF <= Max_priseD)
                {

                    if (WalletFF >= PricefloatF)
                    {
                        string CAENA_PACAS = "";
                        IWebElement CAENA_PACA1 = Browser.FindElement(By.XPath("/html/body/div[1]/div[7]/div[2]/div[1]/div[4]/div[1]/div[3]/div[4]/div[2]/div[2]/div[2]/div[2]/span/span[1]"));
                        CAENA_PACAS = CAENA_PACA1.Text;
                        string CAENA_PACASS = System.Text.RegularExpressions.Regex.Match(CAENA_PACAS, @"[0-9]+\,?[0-9]*").Value;
                        string Sold = "Sold!";

                        while (Sold.StartsWith(CAENA_PACASS))
                        {

                            SendKeys.Send(@"^{r}");
                            System.Threading.Thread.Sleep(4000);

                            string CAENA_PACAS1 = "";
                            IWebElement CAENA_PACA2 = Browser.FindElement(By.XPath("/html/body/div[1]/div[7]/div[2]/div[1]/div[4]/div[1]/div[3]/div[4]/div[2]/div[2]/div[2]/div[2]/span/span[1]"));

                            CAENA_PACAS1 = CAENA_PACA2.Text;
                            string CAENA_PACASS1 = System.Text.RegularExpressions.Regex.Match(CAENA_PACAS1, @"[0-9]+\,?[0-9]*").Value;
                            CAENA_PACASS = CAENA_PACASS1;

                        }
                        float CAENA_PACAF = Single.Parse(CAENA_PACASS);
                        double CAENA_PACAD = (double)Math.Round(CAENA_PACAF, 2);
                        double CAENA_PACAD2 = CAENA_PACAD;
                        double percent = 16.5;


                        CAENA_PACAD = CAENA_PACAD * (100 - percent) / 100;
                        CAENA_PACAD = Math.Round(CAENA_PACAD, 2);

                        double percent2 = 15.5;
                        CAENA_PACAD2 = CAENA_PACAD2 * (100 - percent2) / 100;


                        IWebElement arms = Browser.FindElement(By.XPath(@"//*[@id='largeiteminfo_item_name']")); // пиздим оружие название
                        DivText3 = arms.Text;
                        IWebElement wear = Browser.FindElement(By.XPath(@"//*[@id='largeiteminfo_item_descriptors']/div[1]")); // пиздим износ оружия
                        DivText4 = wear.Text;

                        System.Threading.Thread.Sleep(500);
                        click = Browser.FindElement(By.XPath(@"//*[@id='market_buyorder_info']/div[1]/div[1]/a")); // клик на кнопку заказать
                        click.Click();
                        System.Threading.Thread.Sleep(1000);
                        click = Browser.FindElement(By.XPath(@"//*[@id='market_buyorder_dialog_accept_ssa']"));
                        click.Click();
                        System.Threading.Thread.Sleep(1000);
                        Browser.FindElement(By.XPath(@"//*[@id='market_buy_commodity_input_price']")).Clear(); // окно для ввода цены 
                        System.Threading.Thread.Sleep(1000);
                        Browser.FindElement(By.XPath(@"//*[@id='market_buy_commodity_input_price']")).SendKeys(CAENA_PACAD.ToString()); // окно для ввода цены 
                        System.Threading.Thread.Sleep(700);
                        click = Browser.FindElement(By.XPath(@"//*[@id='market_buyorder_dialog_purchase']")); // имя элемента 
                        click.Click();

                        File.AppendAllText(@"11.txt", DivText3 + " " + DivText4 + "/" + CAENA_PACAD2 + "\n");  // запись этой вещи в файл


                    }
                }
               
            }

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Browser.Navigate().GoToUrl("https://steamcommunity.com/profiles/76561199093571587/inventory/"); //переход в инвентарь
            System.Threading.Thread.Sleep(1000);

            bool FirstSell = true;
            List<IWebElement> Items = Browser.FindElements(By.XPath("/html/body/div[1]/div[7]/div[2]/div[1]/div[2]/div/div[5]/div[9]/div[2]/div[1]/div/div/div[1]/div")).ToList();

            for (int i = 0; i < Items.Count; i++)
            {

                System.Threading.Thread.Sleep(5000);
                Items[i].Click();

                String DivText3 = "";
                String DivText4 = "";
                String rezylt = "";

                IWebElement arms2 = Browser.FindElement(By.XPath(@"//*[@id='iteminfo0_item_name']")); // пиздим оружие название
                System.Threading.Thread.Sleep(500);
                DivText3 = arms2.Text;

                IWebElement wear2 = Browser.FindElement(By.XPath(@"//*[@id='iteminfo0_item_descriptors']/div[1]")); // пиздим износ оружия
                System.Threading.Thread.Sleep(500);
                DivText4 = wear2.Text;

                rezylt = DivText3 + " " + DivText4; // полное значение вещи 
                StreamReader str = new StreamReader("11.txt", Encoding.Default);
                while (!str.EndOfStream)
                {
                    string st = str.ReadLine();

                    int index = st.IndexOf('/');
                    String text2 = "";
                    if (index >= 0)
                        text2 = st.Substring(0, index);

                    if (text2.StartsWith(rezylt))
                    {


                        double Saledo;
                        string result;

                        result = st.Substring((st.IndexOf("/") + 1));
                        Saledo = double.Parse(result);
                        Saledo = Math.Round(Saledo, 2);

                        IWebElement button = null;
                        button = Browser.FindElement(By.CssSelector(".item_market_action_button.item_market_action_button_green"));


                        Actions SellClick = new Actions(Browser);
                        SellClick.MoveToElement(button).Click().Perform();

                        System.Threading.Thread.Sleep(3000);
                        Browser.FindElement(By.XPath(@"//*[@id='market_sell_currency_input']")).Clear(); // окно для ввода цены 
                        System.Threading.Thread.Sleep(1000);
                        Browser.FindElement(By.XPath(@"//*[@id='market_sell_currency_input']")).SendKeys(Saledo.ToString()); // окно для ввода цены 
                        System.Threading.Thread.Sleep(1000);

                        if (FirstSell)
                        {
                            Browser.FindElement(By.XPath(@"//*[@id='market_sell_dialog_accept_ssa']")).Click();
                            FirstSell = false;
                        }

                        System.Threading.Thread.Sleep(1000);
                        IWebElement click1 = Browser.FindElement(By.XPath(@"//*[@id='market_sell_dialog_accept']"));
                        click1.Click();
                        System.Threading.Thread.Sleep(2000);
                        click1 = Browser.FindElement(By.XPath(@"//*[@id='market_sell_dialog_ok']"));
                        click1.Click();
                        System.Threading.Thread.Sleep(2000);

                        File.AppendAllText(@"RezultTraide.txt", rezylt + "\n");
                        System.Threading.Thread.Sleep(5000);

                        SendKeys.Send("{ESC}");
                        System.Threading.Thread.Sleep(2000);
                    }
                }
               }
            }

        private void button5_Click(object sender, EventArgs e)
        {

                string email = "maxwgjdihan1@gmail.com";
                string password = "VKokoneMastak!123742";
                string passwordNew = "passwordNew";
                var options = new ChromeOptions();
               // options.AddExtension("kokon.zip");
                IWebDriver Browser = new ChromeDriver(options);
                Browser.Navigate().GoToUrl("https://www.joinhoney.com/");
                Browser.FindElement(By.XPath("/html/body/div[1]/div/div[2]/div/div/div/div/div/div/div/div[2]/div[2]/div/span")).Click();
            Thread.Sleep(2000);
                Browser.FindElement(By.XPath("/html/body/div[1]/div/div[2]/div/div/div/div/div[2]/div[2]/div/div[2]/div[2]/div/button")).Click();
                Browser.FindElement(By.XPath("/html/body/div[1]/div/div[2]/div/div/div/div/div[2]/div[2]/div/div[2]/div[2]/div/form/div[1]/div[1]/input")).SendKeys("maxwgjdihan1@gmail.com");//eamil
                Browser.FindElement(By.XPath("/html/body/div[1]/div/div[2]/div/div/div/div/div[2]/div[2]/div/div[2]/div[2]/div/form/div[2]/div[1]/input")).SendKeys(password);
                Browser.FindElement(By.XPath(@"//*[@aria-label=""Log in with Email""]")).Click(); // кликаем по кнопке чтобы активировалась капча
                Thread.Sleep(5000);                                                                     //разгадываем капч
            Browser.FindElement(By.XPath("/html/body/div[1]/div/div[2]/div/header/div/div/div[3]/div[1]/div/div[3]/ul/li[3]/a")).Click();
            Browser.FindElement(By.XPath("/html/body/div[1]/div/div[2]/div/div/main/div/div[2]/div/div[15]")).Click();
                // возможна капча


                return ;
            
        }
    }
}
