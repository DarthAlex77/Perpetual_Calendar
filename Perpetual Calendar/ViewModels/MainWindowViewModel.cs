using System;
using System.Reactive;
using System.Reactive.Linq;
using Perpetual_Calendar.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using ReactiveUI.Validation.Abstractions;
using ReactiveUI.Validation.Extensions;
using ReactiveUI.Validation.Helpers;

namespace Perpetual_Calendar.ViewModels
{
    public class MainWindowViewModel : ReactiveValidationObject<MainWindowViewModel>
    {
        public MainWindowViewModel()
        {
            
            CalcGregorian = ReactiveCommand.Create(Gregorian, this.IsValid());
            CalcJulian    = ReactiveCommand.Create(Julian, this.IsValid());
            this.ValidationRule(vm => vm.Day, day => day >= 1 && day <= 31, "День должен быть от 1 до 31");
            this.ValidationRule(vm => vm.Month, month => month >= 1 && month <= 31, "Месяц должен быть от 1 до 12");
            this.ValidationRule(vm => vm.Year, year => year >= 1 && year <= 9999, "Программное ограничение года от 1 до 9999");
        }

        #region Commands

        public ReactiveCommand<Unit, Unit> CalcGregorian { get; }
        public ReactiveCommand<Unit, Unit> CalcJulian    { get; }

        #endregion

        #region DayOfWeek Property

        private String _dayOfWeek;
        public String DayOfWeek
        {
            get => _dayOfWeek;
            set => this.RaiseAndSetIfChanged(ref _dayOfWeek, value);
        }

        #endregion

        #region Properties

        [Reactive]
        public Int32 Day { get; set; }
        [Reactive]
        public Int32 Month { get; set; }
        [Reactive]
        public Int32 Year { get; set; }

        #endregion

        #region Methods

        public void Julian()
        {
            Int32      a          = (14 - Month) / 12;
            Int32      year       = Year + 4800 - a;
            Int32      month      = Month + 12 * a - 3;
            Int32      result     = (Day + (153 * month + 2) / 5 + 365 * year + year / 4 - 32083) % 7;
            JulianDays julianDays = (JulianDays) result;
            DayOfWeek = julianDays.ToString();
        }

        public void Gregorian()
        {
            Int32         a             = (14 - Month) / 12;
            Int32         year          = Year - a;
            Int32         month         = Month + 12 * a - 2;
            Int32         result        = (Day + 31 * month / 12 + year + year / 4 - year / 100 + year / 400) % 7;
            GregorianDays gregorianDays = (GregorianDays) result;
            DayOfWeek = gregorianDays.ToString();
        }

        #endregion
    }
}