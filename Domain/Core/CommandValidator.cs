using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace GuyHarwood.DieThrow.Domain.Core
{
    public class CommandValidator<TCommand,TReturn> : IHandler<TCommand, TReturn> where TCommand : Command where TReturn : new()
    {
        private readonly IHandler<TCommand, TReturn> _decoratedHandler;
        private readonly IServiceProvider _serviceProvider;

        public CommandValidator(IHandler<TCommand,TReturn> decoratedHandler, IServiceProvider serviceProvider)
        {
            _decoratedHandler = decoratedHandler;
            _serviceProvider = serviceProvider;
        }

        public TReturn Handle(TCommand command)
        {
            var context = new ValidationContext(command, _serviceProvider, null);

            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(command, context, validationResults, true);

            if (validationResults.Count > 0)
            {
                throw new ValidationException(JsonConvert.SerializeObject(validationResults));
            }

            return _decoratedHandler.Handle(command);
        }
    }
}