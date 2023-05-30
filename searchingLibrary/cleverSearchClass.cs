using System.Collections.Generic;
using System.Threading.Tasks;
using System;


namespace searchingLibrary
{
    public class cleverSearchClass
    {
        public Dictionary<string, int> specialWords = new Dictionary<string, int>();
        public string[] notUnique = {"я", "мы","он","она","они","оно","ты","вы","нее","у","в","него","ее","его","мне","чтобы","себя", "тебя",
                                      "не", "с","к","все","за","это","от","по","так","то","из","бы","ему","ей","была","будто","быть", "тут", "уж",
                                      "о","и","или","в","на","под","над","а","но","как","что", "сноска", "же", "этот","ним","ваше", "свое", "даже", "всё",
                                      "было","были","был","еще","для","когда","вот","ни","ну","до","нет", "да", "этого", "про", "эти","этих","кто",
                                      "того","вас","тем","себе","ли","вам","чем","которые","во","свою","перед", "сам", "мой", "меня", "чтоб", "если",
                                      "их","только","уже","очень","всех","при","со","чтото","своего","им","без", "ней", "там", "го", "т","г","м", "ж"};

        private Dictionary<string, int> privateAnalyser(string fileText)
        {
            var specialWords = new Dictionary<string, int>();
            string tempStr = new string((from c in fileText
                                     where (char.IsLetter(c) || char.IsWhiteSpace(c)) && c!='\r' && c!='\n'
                                     select c).ToArray());
                        
            string[] textWords = tempStr.ToLower().Split(' ');
            foreach (string word in textWords)
            {
                if (specialWords.ContainsKey(word))
                    specialWords[word]++;
                else
                    specialWords[word] = 1;                  
            }

            specialWords.Remove("");

            foreach (string word in notUnique)
                specialWords.Remove(word);

            return specialWords;
        }

        public Dictionary<string, int> PublicAnalyser(string fileText)
        {
            int NUMBER_OF_PARTS = 4;

            var specialWords = new Dictionary<string, int>();
            string tempStr = new string((from c in fileText
                                         where (char.IsLetter(c) || char.IsWhiteSpace(c)) && c != '\r' && c != '\n'
                                         select c).ToArray());

            string[] textWords = tempStr.ToLower().Split(' ');

            int chunkSize = textWords.Length / NUMBER_OF_PARTS;
            string[][] chunks = new string[NUMBER_OF_PARTS][];

            for (int i = 0; i < NUMBER_OF_PARTS; i++)
            {
                chunks[i] = new string[chunkSize];
                Array.Copy(textWords, i * chunkSize, chunks[i], 0, chunkSize);
            }
          
            List<Task<Dictionary<string, int>>> tasks = new List<Task<Dictionary<string, int>>>();

            for (int i = 0; i < NUMBER_OF_PARTS; i++)
            {
                int taskNumber = i; 
                tasks.Add(Task.Factory.StartNew(() => tinyAnalyzer(chunks[taskNumber], taskNumber)));

            }

            Task.WaitAll(tasks.ToArray());

            Dictionary<string, int> result = new Dictionary<string, int>();

            foreach (var task in tasks)
            {
                foreach (var kvp in task.Result)
                {
                    if (result.ContainsKey(kvp.Key))
                    {
                        result[kvp.Key] += kvp.Value;
                    }
                    else
                    {
                        result.Add(kvp.Key, kvp.Value);
                    }
                }
            }

            return result;
        }

        public Dictionary<string, int> tinyAnalyzer(string[] textWords, int taskNumber)
        {

            Console.WriteLine($"Task {taskNumber} выполняется.");

            var specialWords = new Dictionary<string, int>();
            foreach (string word in textWords)
            {
                if (specialWords.ContainsKey(word))
                    specialWords[word]++;
                else
                {
                    specialWords[word] = 1;
                }
            }

            specialWords.Remove("");

            foreach (string word in notUnique)
                specialWords.Remove(word);

            return specialWords;
        }

    }
}