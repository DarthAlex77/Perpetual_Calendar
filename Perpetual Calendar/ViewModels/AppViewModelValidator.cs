using System;
using System.Runtime.Intrinsics.X86;
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
            RuleFor(vm => vm.Day).Must(DayValidator);
        }

        private static Boolean DayValidator(AppViewModel vm, Int32 day,PropertyValidatorContext context)
        {
            switch (vm.Month)
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12:
                    context.Rule.MessageBuilder = s => "День должен быть от 1 до 31";
                    return day >= 1 && day <= 31;
                case 2 when DateTime.IsLeapYear(vm.Year):
                    context.Rule.MessageBuilder = s => "Високосный год. День должен быть от 1 до 29";
                    return day >= 1 && day <= 29;
                case 2:
                    context.Rule.MessageBuilder = s => "День должен быть от 1 до 28";
                    return day >= 1 && day <= 28;
                default:
                    context.Rule.MessageBuilder = s => "День должен быть от 1 до 30";
                    return day >= 1 && day <= 30;
            }
        }
    }
}