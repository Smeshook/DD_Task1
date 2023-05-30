// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Concurrent;
using System.IO;
using searchingLibrary;
using System.Reflection;
using System.Diagnostics;

public static class Task_1
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter path to book");
        string pathToBook = Console.ReadLine();
        string pathToUniqueWordsPublic = @".\uniqueWordsPublic.txt";
        string pathToUniqueWordsPrivate = @".\uniqueWordsPrivate.txt";

        Stopwatch watchPrivate = new Stopwatch();
        Stopwatch watchPublic = new Stopwatch();

        try
        {
            cleverSearchClass search = new cleverSearchClass();

            StreamReader fileText = new StreamReader(pathToBook);
            string fileString = fileText.ReadToEnd();

            var type = typeof(cleverSearchClass);
            MethodInfo methodInfo = type.GetMethod("privateAnalyser", BindingFlags.Instance | BindingFlags.NonPublic);

            object reflectionReturn = methodInfo.Invoke(search, new object[] {fileString});

            watchPrivate.Start();
            Dictionary<string, int> totalDictionaryPrivate = (Dictionary<string, int>)reflectionReturn;
            watchPrivate.Stop();

            watchPublic.Start();
            Dictionary<string, int> totalDictionaryPublic = search.PublicAnalyser(fileString);
            watchPublic.Stop();

            StreamWriter uniqeWordsPrivate = new StreamWriter(pathToUniqueWordsPrivate);
            foreach (var word in totalDictionaryPrivate.OrderByDescending(x => x.Value))
                uniqeWordsPrivate.WriteLine($"{word.Key}\t {word.Value}");

            uniqeWordsPrivate.Close();

            StreamWriter uniqeWordsPublic = new StreamWriter(pathToUniqueWordsPublic);
            foreach (var word in totalDictionaryPublic.OrderByDescending(x => x.Value))
                uniqeWordsPublic.WriteLine($"{word.Key}\t {word.Value}");

            uniqeWordsPublic.Close();

            TimeSpan tsPublic = watchPublic.Elapsed;
            TimeSpan tsPrivate = watchPrivate.Elapsed;

            string elapsedTimePublic = String.Format("{0:00}:{1:00}:{2:000}",
                                                    tsPublic.Seconds,
                                                    tsPublic.Milliseconds, tsPublic.Microseconds);
            string elapsedTimePrivate = String.Format("{0:00}:{1:00}:{2:000}",
                                                    tsPrivate.Seconds,
                                                    tsPrivate.Milliseconds, tsPrivate.Microseconds);

            Console.WriteLine("RunTime Public: " + elapsedTimePublic
                                +"\n"+ "RunTime Private: " + elapsedTimePrivate);

        }

        catch { Console.WriteLine("Возникло исключение!"); }
    }
}
