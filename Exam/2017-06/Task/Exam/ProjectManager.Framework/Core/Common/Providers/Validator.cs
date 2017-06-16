using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

using ProjectManager.Framework.Core.Common.Contracts;
using ProjectManager.Framework.Core.Common.Exceptions;

namespace ProjectManager.Framework.Core.Common.Providers
{
    public class Validator : IValidator
    {
        public void Validate<T>(T obj) where T : class
        {
            var validationErrors = this.GetValidationErrors(obj);
            var valid = validationErrors.Count() == 0;

            if (!valid)
            {
                this.LogValidationErrors(validationErrors);
            }
        }

        public void LogValidationErrors(IEnumerable<string> validationErrors)
        {
            throw new UserValidationException(validationErrors.First());
        }

        private IEnumerable<string> GetValidationErrors(object obj)
        {
            Type type = obj.GetType();
            PropertyInfo[] properties = type.GetProperties();
            Type attrType = typeof(ValidationAttribute);

            foreach (var propertyInfo in properties)
            {
                object[] customAttributes = propertyInfo.GetCustomAttributes(attrType, inherit: true);

                foreach (var customAttribute in customAttributes)
                {
                    var validationAttribute = (ValidationAttribute)customAttribute;

                    bool valid = validationAttribute.IsValid(propertyInfo.GetValue(obj, BindingFlags.GetProperty, null, null, null));

                    if (!valid)
                    {
                        yield return validationAttribute.ErrorMessage;
                    }
                }
            }
        }
    }
}
