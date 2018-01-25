using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleLINQ.Model;

namespace ConsoleLINQ
{
    class Program
    {
        static MCS db = new MCS();
        static void Main(string[] args)
        {
            //Example1(); -- создание xml документа
            //Example2(); -- считывание xml документа
            //Example3(); -- сохранение  xml документа
            //Example4(); -- cчитывает с db и записывает в файл
            //Example6(); -- добавление html код в xml
            //Example7();
            //Example8();
            //Example9();
        }

        //создание 
        public static void Example1()
        {
            XElement xbook = new XElement("BookPraticipants",
                new XElement("BookPraticipant",
                new XAttribute("type", "Author"),

                new XElement("FirstName", "AbdulFattah"),
                new XElement("LastName", "Frinkets")),

                new XElement("BookPraticipant",
                new XAttribute("type", "Author"),

                new XElement("FirstName", "AbdulFattahNewUpdateVersion"),
                new XElement("LastName", "FrinketsNewUpdateVersion")

                ));
            Console.WriteLine(xbook.ToString());
        }
        //загрузка
        public static void Example2()
        {
            //  из Документа
            XDocument doc =
                XDocument.Load(@"\\dc\Студенты\ПКО\SDP-162\ADO.NET\PBook.xml");

            //  Console.WriteLine(doc.ToString());


            //из строки
            string str = doc.ToString();

            XDocument docFromStr = XDocument.Parse(str);

            Console.WriteLine(docFromStr.ToString());



        }
        //сохранение в файл
        public static void Example3()
        {
            XDocument doc =
                XDocument.Load(@"\\dc\Студенты\ПКО\SDP-162\ADO.NET\PBook.xml");

            doc.Save("TextSave.xml");

            //doc.Save("TextSave.xml", SaveOptions.DisableFormatting);-------( Если хочешь насолить Сбей Формат :) )
        }

        public static void Example4()
        {
            XElement serviceHistory =
                new XElement("ServiceHistory",
                from service in db.TrackServiceHistory.ToList()
                select
                new XElement("ServiceHestory",
                new XAttribute("ServiceHistoryId", service.intServiceHistoryId),
                new XElement("dRepairDate", service.dRepairDate),
                new XElement("strDescriptionProblem", service.strDescriptionProblem)
               )
                );
            serviceHistory.Save("TrackServiseHistory.xml");
        }

        public static void Example5()
        {
            XElement serviceHistory =
                new XElement("ServiceHistory",
                from service in db.TrackServiceHistory.Take(10).ToList()
                select
                new XElement("ServiceHestory",

                new XAttribute("ServiceHistoryId", service.intServiceHistoryId),
                new XElement("Equipment",
                    new XElement("StopReason", service.intStopReason),
                     new XElement("SMCSJob", service.strSMCSJob)),

                new XElement("dRepairDate", service.dRepairDate),

                new XElement("strDescriptionProblem", service.strDescriptionProblem)
               )
            );
            foreach (XElement item in serviceHistory.Elements())
            {
                Console.WriteLine("-->" + item.Name + " - " + item.Value + "\t");
            }


            IEnumerable<string> findSMCSJob1 =
                from doc in serviceHistory.Elements()
                where doc.Elements().Any(a => a.Value == "Плановое ТО-500")
                select doc.Value;

            foreach (string item in findSMCSJob1)
            {
                Console.WriteLine("-->" + item);
            }


            IEnumerable<string> findSMCSJob2 =
                from doc in serviceHistory.Elements()
                where doc.Elements().Any(a => a.Value == "Плановое ТО-500")
                select doc.Value;





            //serviceHistory.Save("TrackServiseHistory1.xml");
        }

        public static void Example6()
        {
            XElement f = new XElement("Text", "001");

            XNamespace ns = "https://mystat.itstep.org/ru/homeworks";

            XDocument xDoc = new XDocument(
                new XDeclaration("1.0", "UTF-8", "yes"),
                new XElement(ns + "rootElement",
                new XCData("<H1>HEllo</H1>")));

            xDoc.Save("HTML.xml");
        }

        public static void Example7()
        {
            XElement xbook = new XElement("BookPraticipants",
              new XElement("BookPraticipant",
              new XAttribute("type", "Author"),

              new XElement("FirstName", "AbdulFattah"),
              new XElement("LastName", "Frinkets"))
              );

            xbook.Element("BookPraticipant").Add(
                new XElement("BookPraticipant",
              new XAttribute("type", "Author"),

              new XElement("FirstName", "AbdulFattah1"),
              new XElement("LastName", "Frinkets1")));

            xbook.Element("BookPraticipant").AddFirst(
               new XElement("BookPraticipant",
             new XAttribute("type", "Author"),

             new XElement("FirstName", "FirstAbdulFattah1"),
             new XElement("LastName", "FirstFrinkets1")));

            //xbook.Element("BookPraticipants")
            //    .Elements("BookPraticipant")
            //    .Where(w=>((string)w.Element("FirstName"))=="Absdifj")
            //    .Single<XElement>()
            //    .AddAfterSelf(
            //    new XElement("BookPraticipant",
            //    new XAttribute("type", "Author"),

            //    new XElement("FirstName", "AbdulFattah1"),
            //    new XElement("LastName", "Frinkets1")));


            Console.WriteLine(xbook);
        }

        public static void Example8()
        {
            //XDocument xDoc = new XDocument("BookPraticipant", 
            //    new XElement bookPraticipants =

            //new XElement("BookPraticipant",
            //            new XAttribute("type", "Author"),

            //      new XElement("FirstName", "AbdulFattah1"),
            //      new XElement("LastName", "Frinkets1")),

            //new XElement("BookPraticipant",
            //            new XAttribute("type", "Author"),

            //      new XElement("FirstName", "AbdulFattah2"),
            //      new XElement("LastName", "Frinkets2"))
            //);
        }

        public static void Example9()
        {
             XDocument xDoc = new XDocument("BookPraticipant",

            new XElement("BookPraticipant",
                        new XAttribute("type", "Author"),

                  new XElement("FirstName", "AbdulFattah1"),
                  new XElement("LastName", "Frinkets1")),

            new XElement("BookPraticipant",
                        new XAttribute("type", "Author"),

                  new XElement("FirstName", "AbdulFattah2"),
                  new XElement("LastName", "Frinkets2"))
            );

            xDoc.Nodes().OfType<XElement>()
                .Where(w => w.Name == "FirstName")
                .Single(w => w.Value == "Frinkets2").Value = "Hello";

           var element =  xDoc.Nodes().OfType<XElement>()
             .Where(w => w.Name == "FirstName")
             .Single(w => w.Value == "Frinkets2");

            element.SetValue("5555");

        }
    }
}
