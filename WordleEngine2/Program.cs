using System;
using System.Collections.Generic;

public class Program
{
    /*
         In Wordle, players have six attempts to guess a five-letter word, with feedback given for each guess in the 
         form of coloured tiles indicating when letters match or occupy the correct position.
         
         For example, if the word a player is trying to guess is "space" and they enter "stamp" then:
         > The s is the correct letter and in the correct position so a green tile would be shown in the first position
         > Similarly the a is in the correct position so the 3rd tile would also be green
         > The p is the correct letter, but in the wrong position so a yellow tile would be displayed for the p
         > The t and the m are not in the answer the player is trying to guess so they would be displayed as grey
         
         You need to implement an algorithm that, given the player's guess and the correct answer, outputs a 5 character string with:
         > A "G" for each character in the guess that is in the correct position and should be marked "Green"
         > A "Y" for each character in the guess that is in the answer, but not in the correct position
         > An "X" for each character in the guess that is not in the answer
         
         For the example above (guess = stamp, answer = space) the expected output of the algorithm is "GXGXY"
    */

    public static void Main()
    {
        //Answer = space
        Console.WriteLine("Guess: stamp | Answer: space | Expected: GXGXY | Output: " + WordlePattern("stamp", "space"));
        Console.WriteLine("Guess: space | Answer: space | Expected: GGGGG | Output: " + WordlePattern("space", "space"));
        Console.WriteLine(string.Empty);
        //Answer = bored
        Console.WriteLine("Guess: bloom | Answer: bored | Expected: GXYXX | Output: " + WordlePattern("bloom", "bored"));
        Console.WriteLine(string.Empty);
        //Answer = erase
        Console.WriteLine("Guess: sleep | Answer: erase | Expected: YXYYX | Output: " + WordlePattern("sleep", "erase"));
        Console.WriteLine("Guess: eject | Answer: erase | Expected: GXYXX | Output: " + WordlePattern("eject", "erase"));

        //Answer = steer
        Console.WriteLine("Guess: easee | Answer: steer | Expected: YXYGX | Output: " + WordlePattern("easee", "steer"));

        //Answer = error
        Console.WriteLine("Guess: error | Answer: steer | Expected: YXXXG | Output: " + WordlePattern("error", "steer"));

    }

    public static string WordlePattern(string guess, string answer)
    {
        string response = "";

        Dictionary<char, int> answerDict = new Dictionary<char, int>();
        
        response = ScoreNext(guess, answer, answerDict, response);

        return response;

    }

    public static string ScoreNext(string guess, string answer, Dictionary<char, int> Dict, string response)
    {
        int pos = answer.Length-1;

        if (guess.Length < 1 || answer.Length<1)
            return response;

        if(guess[pos] == answer[pos])
        {
            return response = ScoreNext(guess.Remove(guess.Length - 1, 1), answer.Remove(answer.Length - 1, 1), Dict, response) + "G";
        }

        return response = CheckContains(guess, answer, Dict, response);

    }

    public static string CheckContains(string guess, string answer, Dictionary<char, int> Dict, string response)
    {
        int pos = answer.Length - 1;

        if (Dict.ContainsKey(answer[pos]))
        {
            Dict[answer[pos]]++;
        }
        else
        {
            Dict.Add(answer[pos], 1);
        }        

        response = ScoreNext(guess.Remove(guess.Length - 1, 1), answer.Remove(answer.Length - 1, 1), Dict, response);

        if (Dict.ContainsKey(guess[pos]))
        {
            if(Dict[guess[pos]] >0)
            {
                Dict[guess[pos]]--;
                return response =  response + "Y";
            }
        }

        return response = response +  "X";

    }

}