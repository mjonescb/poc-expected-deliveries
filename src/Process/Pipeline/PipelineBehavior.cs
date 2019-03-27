namespace Process.Pipeline
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Aspects.Validation;
    using FluentValidation;
    using FluentValidation.Results;
    using MediatR;
    using Serilog;

    public class PipelineBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        readonly IEnumerable<IValidator<TRequest>> validators;
        readonly ILogger logger;

        public PipelineBehavior(
            IEnumerable<IValidator<TRequest>> validators,
            ILogger logger)
        {
            this.validators = validators;
            this.logger = logger;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            ValidationContext context = new ValidationContext(request);
            IEnumerable<ValidationFailure> failures = validators
                .Select(x => x.Validate(context))
                .SelectMany(x => x.Errors)
                .ToList();

            if(failures.Any())
            {
                throw new PipelineValidationException(failures);
            }

            try
            {
                TResponse result = await next();
                return result;
            }
            catch(Exception exception)
            {
                logger.Error(exception,
                    "Error executing {requestType}",
                    request.GetType().FullName);
                
                throw;
            }
        }
    }
}
