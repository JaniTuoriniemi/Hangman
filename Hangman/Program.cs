using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Hangman
{
    class Program
    {
        static void Main(string[] args)
        {                  // The list of words and control parameter ''game'' are created. 
            bool game = true;
            string[] words;
            words = new string[10] { "flower", "cat", "computer", "treasure", "town", "planet", "horse", "luggage", "forest", "island" };
            while (game == true)
            {
                Console.WriteLine("Welcome to hangman. \r\n To play, press 1. To exit, press 0.");
                bool valid = int.TryParse(Console.ReadLine(), out int Choice);

                if (valid == false)
                {
                    Console.WriteLine("Invalid type of input. Press either the numbers 0, or 1");
                }
                else
                    switch (Choice) // The program is on untill it shut down below upon user request. 
                    {
                        case 0:
                            Console.WriteLine("Closes the program");
                            game = false;
                            break;
                        case 1: // The word for the gameround is randomly chosen from the ''words'' array and utility parameters are set.
                            Random random = new Random();
                            int dice = random.Next(1, 11);
                            bool isright = false;
                            int attempts = 10;
                            string Hidden_word = words[dice];
                            int length = Hidden_word.Length;
                            int revealed = 0;
                            StringBuilder Hidden_word_sb = new StringBuilder(Hidden_word, length);// The game manipulates text with stringbuilders
                            StringBuilder Guessed_characters_sb = new StringBuilder(null, length);
                            StringBuilder Displayed_word_sb = new StringBuilder(null, length);// The word with charakters replaced by dashes is build.
                            for (int i = 0; i < length; i++)
                            {
                                Displayed_word_sb.Append('_');
                                Displayed_word_sb.Append(' ');
                            }
                            while (attempts > 0 && isright == false) // The game is on as long as attempts are left, or untill the word is revealed (isright=true).
                            {
                                Console.WriteLine($"You have { attempts } attempts left.");
                                Console.WriteLine(Displayed_word_sb.ToString());
                                if (attempts != 10)
                                {// This message is not shown the first time.
                                    Console.WriteLine("You have alrady guessed the following characters");
                                    Console.WriteLine(Guessed_characters_sb.ToString());
                                }
                                string guess = null;
                                Console.WriteLine("Guess either a letter, or write the whole word");
                                try // Ensures the game does not chrash by invalid user input.
                                {
                                    guess = Console.ReadLine().ToLower();
                                }
                                catch (IOException)
                                {
                                    Console.WriteLine("The program does not recognise your input. Please give a letter.");
                                }
                                catch (OutOfMemoryException)
                                {
                                    Console.WriteLine("Your computer has run out of memory");
                                }
                                catch (ArgumentOutOfRangeException)
                                {
                                    Console.WriteLine("Your input argument is too big");
                                }
                                if (guess != null) // Game proceeds if user input was recognised.
                                {
                                    int guesslength = guess.Length;
                                    if (guesslength > 1)
                                    {
                                        if (guesslength != length) // The guess is repeated if user guesses more than one character, but the string has not the same length as the word.
                                        {
                                            Console.WriteLine("Bad guess, try again!");
                                        }
                                        else if (string.Equals(Hidden_word_sb.ToString(), guess)) // Checks if user guessed whole word is a match. 
                                        {
                                            Console.WriteLine(Hidden_word_sb.ToString());
                                            Console.WriteLine("You guessed correct!");
                                            isright = true;
                                        }
                                        else
                                        {
                                            Console.WriteLine("you guessed wrong!");// Attempts remaining are decreased upon wrong guess.
                                            attempts--;
                                        }
                                    }
                                    else
                                    {   if (Guessed_characters_sb.ToString().Contains(guess))// Checks if letter is already guessed.
                                        {
                                            Console.WriteLine("you have already guessed that letter!");
                                         }
                                        else
                                            {
                                            Guessed_characters_sb.Append(guess + ",");// Displays guessed characters contained i the word.
                                            char guess_letter = Convert.ToChar(guess);
                                            for (int i = 0; i < length; i++)
                                            {
                                                if (Hidden_word_sb[i] == guess_letter)
                                                {
                                                    Displayed_word_sb[i * 2] = guess_letter;
                                                    revealed++;
                                                }
                                            }
                                            if (revealed == length)// If all characters are revealed, the game stops.
                                            {
                                                Console.WriteLine(Hidden_word_sb.ToString());
                                                Console.WriteLine("you have unraveled the word!");
                                                isright = true;
                                            }
                                            else
                                            {
                                                attempts--; // Attempts remaining are decreased i whole word is not yet revealed.
                                            }
                                        }
                                       
                                        
                                    }
                                }
                            }
                            if (attempts == 0)
                                Console.WriteLine("You are hanged!"); // When attempts are spent up, user gets this message.
                            break;
                        default:
                            Console.WriteLine("The program did not understand your input. Press either 0 or 1");
                            break;
                    }
            }
        }
    }
}

        
    

