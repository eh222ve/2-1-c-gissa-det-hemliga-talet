using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1DV402.S2.L1C
{
    public class SecretNumber
    {
        private GuessedNumber[] _guessedNumbers;
        private int? _number;
        public const int MaxNumberOfGuesses = 7;

        //Kan användaren fortsätta gissa? Kollar mot Max-gissningar
        public bool CanMakeGuess
        {
            get
            {
                if (Count >= MaxNumberOfGuesses || Outcome == Outcome.Right)
                {
                    return false;
                }
                return true;
            }
        }

        //Räknar antalet gissningar
        public int Count
        {
            get;
            private set;
        }

        //Senaste gissningen
        public int? Guess
        {
            get;
            private set;
        }

        //Returnerar kopia av _guessedNumbers
        public GuessedNumber[] GuessedNumbers
        {
            get
            {
                return (GuessedNumber[]) _guessedNumbers.Clone();
            }
        }

        //Det hemliga talet, går det att fortsätta gissa returneras null
        public int? Number
        {
            get
            {
                if (CanMakeGuess)
                {
                    return null;
                }
                else
                {
                    return _number;
                }
            }
            private set { _number = value; }
        }

        //Utfallet av den senaste gissningen
        public Outcome Outcome
        {
            get;
            private set;
        }

        //Initierar/nollställer till default-värden. Slumpar hemligt tal.
        public void Initialize() { 

            for (int i = 0; i < _guessedNumbers.Length; i++)
            {
                _guessedNumbers[i].Number = null;
                _guessedNumbers[i].Outcome = Outcome.Indefinite;
            }

            Random myRandom = new Random();
            Number = myRandom.Next(1, 100);

            Count = 0;

            Guess = null;

            Outcome = Outcome.Indefinite;
        
        }

        //Gör en gissning av hemligt tal
        public Outcome MakeGuess(int guess)
        {
            if (CanMakeGuess)
            {
                if (guess < 1 || guess > 100)
                {
                    throw new ArgumentOutOfRangeException();
                }

                for (int i = 0; i < GuessedNumbers.Length; i++)
                {
                    if (GuessedNumbers[i].Number == guess)
                    {
                        Outcome = Outcome.OldGuess;
                        return Outcome;
                    }
                }

                Guess = guess;

                if (Guess > _number)
                {
                    Outcome = Outcome.High;
                }
                else if (Guess < _number)
                {
                    Outcome = Outcome.Low;
                }
                else if (Guess == _number)
                {
                    Outcome = Outcome.Right;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Värdet är inte giltigt");
                }
                _guessedNumbers[Count].Number = Guess;
                _guessedNumbers[Count].Outcome = Outcome;

                Count++;
            }
            else
            {
                Outcome = Outcome.NoMoreGuesses;
            }
            return Outcome;
        }

        //Construct, skapar array med gissningar och anropar Initialize()
        public SecretNumber()
        {
            _guessedNumbers = new GuessedNumber[MaxNumberOfGuesses];
            Initialize();
        }
        
    }
   
}
