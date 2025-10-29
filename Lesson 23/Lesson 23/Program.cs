using System;

namespace Coding.Exercise
{
    public class Exercise
    {
        static void Main(string[] args)
        {
            var exercise = new Exercise();

            var words = new List<string> { "bobcat", "wolverine", "grizzly" };
            var result = exercise.ProcessAll(words);

            Console.WriteLine("Processed words:");
            foreach (var word in result)
            {
                Console.WriteLine(word);
            }
        }

        public List<string> ProcessAll(List<string> words)
        {
            var stringsProcessors = new List<StringsProcessor>
                {
                    new StringsTrimmingProcessor(),
                    new StringsUppercaseProcessor()
                };

            List<string> result = words;
            foreach (var stringsProcessor in stringsProcessors)
            {
                result = stringsProcessor.Process(result);
            }
            return result;
        }
    }
    public abstract class StringsProcessor
    {
        public abstract List<string> Process(List<string> words); 
        

    }

    public class StringsTrimmingProcessor : StringsProcessor
    {
        public override List<string> Process(List<string> words) {
            var results = new List<string>();
            foreach (var word in words) {
                int half_lenght = word.Length / 2;
                results.Add(word.Substring(0, word.Length / 2));//Keep the first half

            }

            return results;
        }
    }
    public class StringsUppercaseProcessor: StringsProcessor
    {
        public override List<string> Process(List<string> words)
        {
            var results = new List<string>();
            foreach (var word in words)
            {
                var word_upper = word.ToUpper();
                results.Add(word_upper);
            }

            return results;
            
                
            
        }
    }

    //your code goes here
}
