using System.Collections.Generic;

namespace searchingLibrary
{
    public class cleverSearchClass
    {
        public string[] notUnique = {"я", "мы","он","она","они","оно","ты","вы","нее","у","в","него","ее","его","мне","чтобы","себя", "тебя",
                                      "не", "с","к","все","за","это","от","по","так","то","из","бы","ему","ей","была","будто","быть", "тут", "уж",
                                      "о","и","или","в","на","под","над","а","но","как","что", "сноска", "же", "этот","ним","ваше", "свое", "даже", "всё",
                                      "было","были","был","еще","для","когда","вот","ни","ну","до","нет", "да", "этого", "про", "эти","этих","кто",
                                      "того","вас","тем","себе","ли","вам","чем","которые","во","свою","перед", "сам", "мой", "меня", "чтоб", "если",
                                      "их","только","уже","очень","всех","при","со","чтото","своего","им","без", "ней", "там", "го", "т","г","м", "ж"};

        private Dictionary<string, int> analyser(string fileText)
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
    }
}
