using Handmades.Models;
using System.ComponentModel.DataAnnotations;

namespace Handmade.Models
{
    public class UniqueAttribute : ValidationAttribute
    {
        private readonly string _propertyName; // اسم الحقل الذي نتحقق منه

        public UniqueAttribute(string propertyName)
        {
            _propertyName = propertyName;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return null; // إذا كانت القيمة فارغة، لا توجد حاجة للتحقق
            }

            // الحصول على كائن قاعدة البيانات
            var context = validationContext.GetService<DataDbContext>();
            // التحقق من الحقل بناءً على الاسم المحدد
            bool recordExists = false;

             if (_propertyName == "Email")
                {
                    recordExists = context.Signups.Any(s => s.Email == value.ToString());
                }

            else if (_propertyName == "Phone")
            {
                recordExists = context.Signups.Any(s => s.Phone == value.ToString());
            } 
            else if (_propertyName == "Password")
            {
                recordExists = context.Signups.Any(s => s.Password == value.ToString());
            }

            if (recordExists)
            {
                return new ValidationResult($"{_propertyName} is already in use!");
            }

            return ValidationResult.Success;
        }
    }
}
