using System;
using System.IO;

namespace SleepData
{
    class Program
    {
        static void Main(string[] args)
        {
            // ask for input
            Console.WriteLine("Enter 1 to create data file.");
            Console.WriteLine("Enter 2 to parse data.");
            Console.WriteLine("Enter anything else to quit.");
            // input response
            string resp = Console.ReadLine();

            if (resp == "1")
            {
                // create data file

                 // ask a question
                Console.WriteLine("How many weeks of data is needed?");
                // input the response (convert to int)
                int weeks = int.Parse(Console.ReadLine());

                 // determine start and end date
                DateTime today = DateTime.Now;
                // we want full weeks sunday - saturday
                DateTime dataEndDate = today.AddDays(-(int)today.DayOfWeek);
                // subtract # of weeks from endDate to get startDate
                DateTime dataDate = dataEndDate.AddDays(-(weeks * 7));
                
                // random number generator
                Random rnd = new Random();

                // create file
                StreamWriter sw = new StreamWriter("data.txt");
                // loop for the desired # of weeks
                while (dataDate < dataEndDate)
                {
                    // 7 days in a week
                    int[] hours = new int[7];
                    for (int i = 0; i < hours.Length; i++)
                    {
                        // generate random number of hours slept between 4-12 (inclusive)
                        hours[i] = rnd.Next(4, 13);
                    }
                    // M/d/yyyy,#|#|#|#|#|#|#
                    //Console.WriteLine($"{dataDate:M/d/yy},{string.Join("|", hours)}");
                    sw.WriteLine($"{dataDate:M/d/yyyy},{string.Join("|", hours)}");
                    // add 1 week to date
                    dataDate = dataDate.AddDays(7);
                }
                sw.Close();
            }
            else if (resp == "2")
            {
                StreamReader reader = new StreamReader("data.txt");
                while(!reader.EndOfStream)
                {
                    string line = reader.ReadLine(); 
                    string[] lineData = line.Split(new char [] {',','|'}, StringSplitOptions.RemoveEmptyEntries);
                    DateTime date = Convert.ToDateTime(lineData[0]);

                    int total = int.Parse(lineData[1])+int.Parse(lineData[2])+int.Parse(lineData[3])
                    +int.Parse(lineData[4])+int.Parse(lineData[5])+int.Parse(lineData[6])+int.Parse(lineData[7]);

                    double average = (double)total/7;
                    string averageFormat = String.Format("{0:0.0}", average);

                    Console.WriteLine("");

                    Console.WriteLine("Week of {0:MMM}, {0:dd}, {0:yyyy}", date);

                    Console.WriteLine($"{"Su",3}{"Mo",3}{"Tu",3}{"We",3}{"Th",3}{"Fr",3}{"Sa",3}{"Tot",4}{"Avg",4}");

                    Console.WriteLine($"{"--",3}{"--",3}{"--",3}{"--",3}{"--",3}{"--",3}{"--",3}{"---",4}{"---",4}");

                    Console.WriteLine($"{lineData[1],3}{lineData[2],3}{lineData[3],3}{lineData[4],3}{lineData[5],3}" +
                    $"{lineData[6],3}{lineData[7],3}{total,4}{averageFormat,4}");

                };
            }
        }
    }
}