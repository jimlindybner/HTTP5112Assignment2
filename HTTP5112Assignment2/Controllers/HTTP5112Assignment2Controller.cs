using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Web.Http;

namespace HTTP5112Assignment2.Controllers
{
    public class HTTP5112Assignment2Controller : ApiController
    {
        /// <summary>
        /// Computes total calories based on user choice of burger, drink, side and dessert
        /// </summary>
        /// <param name="burger"></param>
        /// <param name="drink"></param>
        /// <param name="side"></param>
        /// <param name="dessert"></param>
        /// <returns>A number indicating total calorie count in chosen meal</returns>
        /// <example>
        /// GET ../api/J1/Menu/4/4/4/4 -> 0
        /// GET ../api/J1/Menu/1/2/3/4 -> 691
        /// </example>

        //GET:/api/J1/Menu/{burger}/{drink}/{side}/{dessert}
        [HttpGet]
        [Route("api/J1/Menu/{burger}/{drink}/{side}/{dessert}")]
        public int Menu(int burger, int drink, int side, int dessert)
        {
            int[] BurgerCal = { 461, 431, 420, 0 };
            int[] DrinkCal = { 130, 160, 118, 0 };
            int[] SideCal = { 100, 57, 70, 0 };
            int[] DessertCal = { 167, 266, 75, 0 };

            return BurgerCal[burger - 1] + DrinkCal[drink - 1] + SideCal[side - 1] + DessertCal[dessert - 1];
        }

        /// <summary>
        /// Calculates the number ways two dice, one with {m} (1 <= m <= 1000) sides and the other with {n} (1 <= n <= 1000) sides, can roll the value of 10.
        /// </summary>
        /// <param name="m"></param>
        /// <param name="n"></param>
        /// <returns>
        /// A string indicating number of ways the two dice can roll the value of 10.
        /// </returns>
        /// <example>
        /// GET ../api/J2/DiceGame/6/8 -> There are 5 ways to get the sum 10.
        /// GET ../api/J2/DiceGame/12/4 -> There are 4 ways to get the sum 10.
        /// GET ../api/J2/DiceGame/3/3 -> There are 0 ways to get the sum 10.
        /// GET ../api/J2/Dicegame/5/5 -> There is 1 way to get the sum 10.
        /// Error message example below for inputs outside of permitted range:
        /// GET ../api/J2/Dicegame/-1/11 -> Please enter a number between 1 and 1000 (inclusively) for the inputs.
        /// </example>

        //GET:/api/J2/DiceGame/{m}/{n}
        [HttpGet]
        [Route("api/J2/DiceGame/{m}/{n}")]
        public string DiceGame(int m, int n)
        {
            //variables: final string output
            string SingularOutput = "There is 1 way to get the sum 10.";
            string PluralOpening = "There are ";
            string PluralClosing = " ways to get the sum 10.";
            string ErrorMessage = "Please enter a number between 1 and 1000 (inclusively) for the inputs.";

            //variable: number of ways to get the sum 10 - starting at 0 by default
            int Solution = 0;

            //lists dice sides based on {m} and {n}
            List<int> DieOne = new List<int>();
            List<int> DieTwo = new List<int>();

            //adding values to DieOne based on {m}
            for (int i = 1; i <= m; i++)
            {
                DieOne.Add(i);
            }
            //adding values to DieTwo based on {n}
            for (int i = 1; i <= n; i++)
            {
                DieTwo.Add(i);
            }

            //caculating solution
            if (m + n == 10)
            {
                //if m + n = 10, there is only one solution
                Solution = 1;
            } else if (m + n > 10)
            {
                //if m + n > 10, list possible solutions
                for (int i = 0; i < m; i++)
                {
                    for (int j = n - 1; j >= 0; j--)
                    {
                         if (DieOne[i] + DieTwo[j] == 10)
                        {
                            Solution++;
                        }
                    }
                }
            }

            //final output
            if (m < 1 || m > 1000 || n < 1 || n > 1000)
            {
                //Error message if NOT 1 <= m <= 1000 or 1 <= n <= 1000
                return ErrorMessage;
            }
            if (Solution == 1)
            {
                //singular string output
                return SingularOutput;
            } else
            {
                //plural string output
                return PluralOpening + Solution + PluralClosing;
            }
        }

        /// <summary>
        /// A program that translates English words (lower case input) into Rovarspraket.
        /// </summary>
        /// <param name="Word"></param>
        /// <returns>
        /// Every consonant in the input word is replaced with three letters:
        /// 1) the consonant itself
        /// 2) the vowel closest to it in the alphabet
        /// 3) the next consonant in the alphabet
        /// </returns>
        /// <example>
        /// GET ../api/J3/Rovarspraket/joy -> jikoyuz
        /// GET ../api/J3/Rovarspraket/ham -> hijamon
        /// </example>

        //GET: /api/J3/Rovarspraket/{Word}
        [HttpGet]
        [Route("api/J3/Rovarspraket/{Word}")]
        public string Rovarspraket(string Word)
        {
            string[] Alphabet = {
                "a","b","c",
                "d","e","f","g",
                "h","i","j","k","l",
                "m","n","o","p","q","r",
                "s","t","u","v","w","x","y","z"
            };
            string[] Vowels = { "a", "e", "i", "o", "u" };
            string[] Consonants = {"b","c","d","f","g","h","j","k","l","m","n","p","q","r","s","t","v","w","x","y","z"};
            string Letter;
            string Consonant;
            string AddVowel; //add closest vowel
            string ConsFin; //add next consonant
            //string Rovarspraket; //final word

            //gather letters in {Word} in an array
            char[] Letters = Word.ToCharArray();

            //loop for extracting individual letters
            for (int i = 0; i < Letters.Length; i++)
            {
                //loop looking for consonants
                for (int j = 0; j < Consonants.Length; j++)
                {
                    //extract individual letters
                    Letter = Letters[i].ToString();
                    //identify the consonants and modify
                    if (Letter.Contains(Consonants[j]))
                    {
                        //identify consonants
                        Consonant = Letter;
                        //add closest vowel to consonant
                        if (Consonant == "b" || Consonant == "c")
                        {
                            AddVowel = Consonant + "a";
                        } else if (Consonant == "d" || Consonant == "f" || Consonant == "g")
                        {
                            AddVowel = Consonant + "e";
                        } else if (Consonant == "h" || Consonant == "j" || Consonant == "k" || Consonant == "l")
                        {
                            AddVowel = Consonant + "i";
                        } else if (Consonant == "m" || Consonant == "n" || Consonant == "p" || Consonant == "q" || Consonant == "r")
                        {
                            AddVowel = Consonant + "o";
                        } else
                        {
                            AddVowel = Consonant + "u";
                        }
                        //add next consonant
                        ConsFin = AddVowel + Consonants[j + 1];
                        //add original vowel back
                        //Rovarspraket = ConsFin + Letters[i + 1].ToString();
                    }
                }
            }
        }
    }
}
