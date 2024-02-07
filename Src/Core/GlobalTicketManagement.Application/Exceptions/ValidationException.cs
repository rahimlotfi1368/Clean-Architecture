using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTicketManagement.Application.Exceptions
{
    public class ValidationException: Exception
    {
        //public List<string> ValdationErrors { get; set; }
        public Dictionary<string, List<string>> ValdationErrors { get; set; }
        public ValidationException(ValidationResult validationResult)
        {
            //ValdationErrors = new List<string>();
            ValdationErrors = new Dictionary<string,List<string>>();

            foreach (var validationError in validationResult.Errors)
            {
                List<string> errorMessage = new List<string>();

                if (ValdationErrors.Keys.Contains(validationError.PropertyName))
                {                  
                    ValdationErrors[validationError.PropertyName].Add(validationError.ErrorMessage);
                }
                else
                {
                    //ValdationErrors.Add(validationError.ErrorMessage);
                    errorMessage.Add(validationError.ErrorMessage);
                    ValdationErrors.Add(validationError.PropertyName, errorMessage);
                }


            }

            //ValdationErrors=validationResult.Errors.Select(e=> new { e.PropertyName, e.ErrorMessage }).ToDictionary(e => e.PropertyName, e => e.ErrorMessage);
        }
    }
}
