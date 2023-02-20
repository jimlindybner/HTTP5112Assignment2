using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        /// Calculates the number ways two dice, one with {m} sides and the other with {n} sides, can roll the value of 10.
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
        /// 
        /// GET ../api/J2/Dicegame/-1/11 -> Not physically possible. A die can't have fewer than 2 sides.
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
            string ParameterLessThanTwo = "Not physically possible. A die can't have fewer than 2 sides.";

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
            if (m < 2 || n < 2)
            {
                //Error message if one or both of the parameters are less than 2
                return ParameterLessThanTwo;
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
    }
}
