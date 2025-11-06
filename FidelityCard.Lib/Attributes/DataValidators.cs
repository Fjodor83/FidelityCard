using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FidelityCard.Lib.Attributes
{
    public class DateRangeAttribute : ValidationAttribute
    {
        private readonly DateTime _min = DateTime.Today.AddYears(-100);
        private readonly DateTime _max = DateTime.Today.AddYears(-6);

        public DateRangeAttribute()
        {

        }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime date)
            {
                if (date < _min || date > _max)
                {
                    return new ValidationResult($"La data deve essere compresa tra {_min:dd/MM/yyyy} e {_max:dd/MM/yyyy}");
                }
            }
            return ValidationResult.Success;
        }
    }

}
