using System;
using System.Collections.Generic;

namespace Common.Parser
{
    public abstract class Parser<T, P> where P : IParserByType<T>
    {
        protected Dictionary<string, P> parsers;

        public T Parse(string input)
        {
            var parameters = input.Split(';');

            if (!parsers.TryGetValue(parameters[0], out var parser))
            {
                throw new Exception("Unknown parser: " + parameters[0]);
            }

            return parser.Parse(parameters[1]); 
        }
    }
}