using System.Collections.Generic;

namespace searchingLibrary
{
    public class cleverSearchClass
    {

        private Dictionary<string, int> analyser(string fileText)
        {
            var specialWords = new Dictionary<string, int>();

            char[] charsToTrim = {' ', '.', ',', '-', '\r','\n', '\"', '(', ')', ':', ';', '[', ']', '\t', '!', '?',
                                                             '0','1','2','3','4','5','6','7','8','9'};
            string[] notUnique = {"я", "мы","он","она","они","оно","ты","вы","нее","у","в","него","ее","его","мне","чтобы","себя", "тебя",
                                      "не", "с","к","все","за","это","от","по","так","то","из","бы","ему","ей","была","будто","быть", "тут", "уж",
                                      "о","и","или","в","на","под","над","а","но","как","что", "сноска", "же", "этот","ним","ваше", "свое", "даже", "всё",
                                      "было","были","был","еще","для","когда","вот","ни","ну","до","нет", "да", "этого", "про", "эти","этих","кто",
                                      "того","вас","тем","себе","ли","вам","чем","которые","во","свою","перед", "сам", "мой", "меня", "чтоб", "если",
                                      "их","только","уже","очень","всех","при","со","чтото","своего","им","без", "ней", "там", "го", "т","г","м", "ж"};

            string[] textWords = fileText.ToLower().Split(' ');
            foreach (string word in textWords)
            {
                string cleanWord = "";

                foreach (char letter in word)
                    cleanWord += letter.ToString().Trim(charsToTrim);
                    
                if (specialWords.ContainsKey(cleanWord))
                    specialWords[cleanWord]++;
                else
                    specialWords[cleanWord] = 1;
                    
            }

            specialWords.Remove("");

            foreach (string cleanWord in notUnique)
                specialWords.Remove(cleanWord);

            return specialWords;
        }
    }
}