using System;
using FluentValidation;
using FluentValidation.Validators;

namespace Perpetual_Calendar.ViewModels
{
    public class AppViewModelValidator : AbstractValidator<AppViewModel>
    {
        public AppViewModelValidator()
        {
            //RuleFor(vm => vm.Day).InclusiveBetween(1, 31);
            RuleFor(vm => vm.Month).InclusiveBetween(1, 12);
            RuleFor(vm => vm.Year).InclusiveBetween(1, 9999);
            RuleFor(vm => vm.Day).Must(LeapDecember);
        }

        private static Boolean LeapDecember(AppViewModel vm, Int32 day,PropertyValidatorContext context)
        {
            if (vm.Month != 2)
            {
                context.Rule.MessageBuilder = s => "День должен быть от 1 до 31";
                return day >= 1 && day <= 31;
            }
            if (DateTime.IsLeapYear(vm.Year))
            {
                context.Rule.MessageBuilder = s => "Високосный год. День должен быть от 1 до 29";
                return day >= 1 && day <= 29;
            }
            context.Rule.MessageBuilder = s => "Февраль. День должен быть от 1 до 28";
            return day >= 1 && day <= 28;
        }
    }
}