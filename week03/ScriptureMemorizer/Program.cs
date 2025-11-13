using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ScriptureMemorizer
{
    // Represents a scripture reference like "John 3:16" or "Proverbs 3:5-6"
    public class Reference
    {
        public string Book { get; private set; }
        public int Chapter { get; private set; }
        public int VerseStart { get; private set; }
        public int? VerseEnd { get; private set; } // null if single verse

        // Constructor for single verse: e.g. "John", 3, 16
        public Reference(string book, int chapter, int verse)
        {
            Book = book;
            Chapter = chapter;
            VerseStart = verse;
            VerseEnd = null;
        }

        // Constructor for verse range: e.g. "Proverbs", 3, 5, 6
        public Reference(string book, int chapter, int verseStart, int verseEnd)
        {
            Book = book;
            Chapter = chapter;
            VerseStart = verseStart;
            VerseEnd = verseEnd;
        }

        public override string ToString()
        {
            if (VerseEnd.HasValue)
                return $"{Book} {Chapter}:{VerseStart}-{VerseEnd.Value}";
            else
                return $"{Book} {Chapter}:{VerseStart}";
        }
    }

    // Represents a single word/token in the scripture text.
    // It preserves punctuation and whitespace when displaying, but tracks whether letters are hidden.
    public class Word
    {
        private string _original;   // original token, e.g. "world," or "faith."
        private bool _hidden;

        public Word(string token)
        {
            _original = token;
            _hidden = false;
        }

        // Whether the token contains at least one letter (so punctuation-only tokens are not considered hideable)
        public bool HasLetter()
        {
            foreach (char c in _original)
                if (char.IsLetter(c))
                    return true;
            return false;
        }

        public bool IsHidden => _hidden;

        // Hide this word (letters replaced by underscores)
        public void Hide()
        {
            _hidden = true;
        }

        // Reveal (not used by default but provided for completeness / testing)
        public void Reveal()
        {
            _hidden = false;
        }

        // Returns the display form of the word; if hidden then letters become underscores but punctuation remains.
        public string GetDisplay()
        {
            if (!_hidden)
                return _original;

            // Replace letters with underscores, preserve non-letter characters
            char[] displayed = new char[_original.Length];
            for (int i = 0; i < _original.Length; i++)
            {
                char c = _original[i];
                if (char.IsLetter(c))
                    displayed[i] = '_';
                else
                    displayed[i] = c;
            }
            return new string(displayed);
        }

        // For debugging / testing
        public override string ToString()
        {
            return _original;
        }
    }

    // Represents the scripture: holds a Reference and a list of Word tokens that make up the text.
    public class Scripture
    {
        private Reference _reference;
        private List<Word> _words;
        private static readonly Regex _splitRegex = new Regex(@"\s+"); // split on whitespace

        private Random _random = new Random();

        // Constructor takes a Reference and the full text (possibly containing multiple verses)
        public Scripture(Reference reference, string fullText)
        {
            _reference = reference;
            // split by whitespace to preserve punctuation inside tokens
            var tokens = _splitRegex.Split(fullText.Trim());
            _words = tokens.Select(t => new Word(t)).ToList();
        }

        // Display the scripture (reference + the current visible/hidden words)
        public void Display()
        {
            Console.WriteLine(_reference.ToString());
            Console.WriteLine(); // blank line
            Console.WriteLine(GetTextForDisplay());
            Console.WriteLine(); // extra spacing for prompts
        }

        // Return the full line built from words' displays
        private string GetTextForDisplay()
        {
            // join tokens with single space
            return string.Join(" ", _words.Select(w => w.GetDisplay()));
        }

        // Hides up to 'count' random words that contain letters and are not already hidden.
        // If fewer than 'count' non-hidden words remain, hides all remaining.
        public void HideRandomWords(int count)
        {
            var candidates = _words
                .Select((w, idx) => new { Word = w, Index = idx })
                .Where(x => x.Word.HasLetter() && !x.Word.IsHidden)
                .ToList();

            if (candidates.Count == 0)
                return;

            int toHide = Math.Min(count, candidates.Count);

            // Select unique random indices from candidates
            for (int i = 0; i < toHide; i++)
            {
                int pick = _random.Next(candidates.Count);
                var chosen = candidates[pick];
                chosen.Word.Hide();
                candidates.RemoveAt(pick); // ensure we don't pick same again
            }
        }

        // Returns true when all tokens that include letters are hidden.
        public bool AllHidden()
        {
            return _words
                .Where(w => w.HasLetter())
                .All(w => w.IsHidden);
        }
    }

    class Program
    {
        // Configuration: how many words to hide per Enter press (tweak to change difficulty)
        private const int WordsToHidePerStep = 3;

        static void Main(string[] args)
        {
            // Built-in library of scriptures (to exceed requirements by offering multiple scriptures).
            // You could extend this to load from a file as another stretch improvement.
            var library = new List<Scripture>()
            {
                new Scripture(new Reference("John", 3, 16),
                    "For God so loved the world, that he gave his only begotten Son, that whosoever believeth in him should not perish but have everlasting life."),
                new Scripture(new Reference("Proverbs", 3, 5, 6),
                    "Trust in the Lord with all thine heart; and lean not unto thine own understanding. In all thy ways acknowledge him, and he shall direct thy paths."),
                new Scripture(new Reference("Psalm", 23, 1),
                    "The Lord is my shepherd; I shall not want.")
            };

            Random rnd = new Random();
            // Pick one scripture at random from library
            Scripture scripture = library[rnd.Next(library.Count)];

            // Initial display
            Console.Clear();
            scripture.Display();

            while (true)
            {
                Console.WriteLine("Press Enter to hide words, or type \"quit\" then Enter to exit.");
                string input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input) && input.Trim().ToLower() == "quit")
                {
                    Console.WriteLine("Goodbye!");
                    return;
                }

                // Hide a few words
                scripture.HideRandomWords(WordsToHidePerStep);

                // Clear and display again
                Console.Clear();
                scripture.Display();

                // If all words hidden, show final and then end
                if (scripture.AllHidden())
                {
                    Console.WriteLine("All words are now hidden. Press Enter to close.");
                    Console.ReadLine();
                    return;
                }
            }
        }
    }
}



