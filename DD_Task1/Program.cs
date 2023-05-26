// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Concurrent;
using System.IO;
using searchingLibrary;
using System.Reflection;

public static class Task_1
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter path to book");
        string pathToBook = Console.ReadLine();
        string pathToUniqueWords = @".\uniqueWords.txt";

        try
        {
            cleverSearchClass search = new cleverSearchClass();

            StreamReader fileText = new StreamReader(pathToBook);

            var type = typeof(cleverSearchClass);
            MethodInfo methodInfo = type.GetMethod("analyser", BindingFlags.Instance | BindingFlags.NonPublic);

            object reflectionReturn = methodInfo.Invoke(search, new object[] {fileText.ReadToEnd()});
            Dictionary<string, int> totalDictionary = (Dictionary<string, int>)reflectionReturn;

            StreamWriter uniqeWords = new StreamWriter(pathToUniqueWords);
            foreach (var word in totalDictionary.OrderByDescending(x => x.Value))
                uniqeWords.WriteLine($"{word.Key}\t {word.Value}");

            uniqeWords.Close();
        
        }

        catch { Console.WriteLine("Возникло исключение!"); }
    }
}
