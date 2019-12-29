using System;
using System.Collections.Generic;
using System.Linq;

namespace AlphaDev.Paging
{
    public class ArgumentsException : ArgumentException
    {
        public ArgumentsException(string message, string parameterName, params string[] parameterNames) : base(message)
        {
            static Func<string, string> GetParameterNamesDelimited(IEnumerable<string> parameterNames)
            {
                return s => string.Join(", ", parameterNames.Select(x => $"{s}{x}{s}"));
            }

            var parameters = new[] { parameterName }.Concat(parameterNames).ToArray();
            var parametersFunc = GetParameterNamesDelimited(parameters);
            var parametersWhole = parametersFunc("'");
            ParamName = parametersFunc(string.Empty);
            ParamNames = parameters;
            Message = $"{message} (Parameters {parametersWhole})";
        }

        public string[] ParamNames { get; }

        public override string Message { get; }

        public override string ParamName { get; }
    }
}