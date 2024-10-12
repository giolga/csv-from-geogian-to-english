using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaveFun_
{
    internal class Program
    {

        static string Translate(string word, Dictionary<char, string> georgianToEnglish)
        {
            string englishText = string.Empty;

            foreach (char letter in word)
            {
                if (georgianToEnglish.ContainsKey(letter))
                {
                    englishText += georgianToEnglish[letter];
                }
                else
                {
                    englishText += letter;
                }
            }

            return englishText;
        }

        static void Main(string[] args)
        {
            Dictionary<char, string> georgianToEnglish = new Dictionary<char, string>()
            {
                {'ა', "a"}, {'ბ', "b"}, {'გ', "g"}, {'დ', "d"}, {'ე', "e"},
                {'ვ', "v"}, {'ზ', "z"}, {'თ', "t"}, {'ი', "i"}, {'კ', "k"},
                {'ლ', "l"}, {'მ', "m"}, {'ნ', "n"}, {'ო', "o"}, {'პ', "p"},
                {'ჟ', "zh"}, {'რ', "r"}, {'ს', "s"}, {'ტ', "t"}, {'უ', "u"},
                {'ფ', "ph"}, {'ქ', "k"}, {'ღ', "gh"}, {'ყ', "q"}, {'შ', "sh"},
                {'ჩ', "ch"}, {'ც', "ts"}, {'ძ', "dz"}, {'წ', "ts"}, {'ჭ', "ch"},
                {'ხ', "kh"}, {'ჯ', "j"}, {'ჰ', "h"}
            };

            using (var streamReader = new StreamReader(@"C:\Users\GioLGA\Desktop\HaveFun)\HaveFun)\csv\fun.csv"))
            {
                using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                {
                    csvReader.Context.RegisterClassMap<FighterLaunchClassMap>();
                    var records = csvReader.GetRecords<FighterLaunch>().ToList();

                    foreach (var record in records)
                    {
                        record.Name = Translate(record.Name, georgianToEnglish);
                        record.Surname = Translate(record.Surname, georgianToEnglish);
                    }

                    using (var streamWriter = new StreamWriter(@"C:\Users\GioLGA\Desktop\HaveFun)\HaveFun)\csv\fun_english.csv"))
                    {
                        using (var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
                        {
                            csvWriter.Context.RegisterClassMap<FighterLaunchClassMap>();
                            csvWriter.WriteRecords(records);
                        }
                    }
                }
            }

            Console.WriteLine("Translation completed successfully");
            Console.ReadKey();
        }
    }

    public class FighterLaunchClassMap : ClassMap<FighterLaunch>
    {
        public FighterLaunchClassMap()
        {
            Map(m => m.Id).Name("Id");
            Map(m => m.Name).Name("Name");
            Map(m => m.Surname).Name("Surname");
            Map(m => m.Age).Name("Age");
        }

    }

    public class FighterLaunch
    {
        //[Name("Id")] // Commented because we created ClassMap above this class
        public int Id { get; set; }
        //[Name("Name")] //column names in CSV
        public string Name { get; set; }
        //[Name("Surname")]
        public string Surname { get; set; }
        //[Name("Age")]
        public int Age { get; set; }
    }
}
