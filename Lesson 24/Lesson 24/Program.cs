using System;

namespace Coding.Exercise
{
    public static class NumericTypesDescriber
    {
        public static string Describe(object someObject)
        {
            if (someObject is int) 
            {
                var x = ($"Int of value {someObject.ToString()}");
                return x.ToString();

                
             }else if(someObject is decimal)
            {
                var x = ($"Decimal of value {someObject.ToString()}");
                return x.ToString();

            }
            else if(someObject is double)
            {
                var x = ($"Double of value {someObject.ToString()}");
                return x.ToString();

            }

            else {
                return null;
            }
            

            //your code goes here
        }
    }
}
