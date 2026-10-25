using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Contracts.JwtOptions
{
    internal class JwtOptionsValidation(IValidator<JwtOptions> validator) : IValidateOptions<JwtOptions>
    {
        private readonly IValidator<JwtOptions> _validator = validator;
        public ValidateOptionsResult Validate(string? name, JwtOptions options) 
        { 
            var result = _validator.Validate(options);
            
            if (result.IsValid) 
                return ValidateOptionsResult.Success;
            
            var errors = result.Errors.Select(error => error.ErrorMessage);
            
            return ValidateOptionsResult.Fail(errors);
        }
    }
}
