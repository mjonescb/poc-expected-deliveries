namespace Process.Aspects.Validation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using FluentValidation.Results;

    public class PipelineValidationException : Exception
    {
        public IEnumerable<ValidationFailure> Failures { get; }

        public PipelineValidationException(
            IEnumerable<ValidationFailure> failures)
        {
            Failures = failures;
        }

        public HttpStatusCode GetMostCommonError(
            HttpStatusCode defaultCode = HttpStatusCode.BadRequest)
        {
            return Failures
                .Select(x =>
                {
                    if(!int.TryParse(x.ErrorMessage, out int codeResult))
                    {
                        return defaultCode;
                    }

                    return (HttpStatusCode) codeResult;
                })
                .GroupBy(x => x)
                .Select(x => new { StatusCode = x.Key, Count = x.Count() })
                .OrderByDescending(x => x.Count)
                .First()
                .StatusCode;
        }
    }
}
