using System;
using System.Reactive;
using Perpetual_Calendar.Models;
using ReactiveUI;
using ReactiveUI.FluentValidation;

namespace Perpetual_Calendar.ViewModels
{
    public class AppViewModel : ReactiveValidationObject
    {
        public AppViewModel():base(new AppViewModelValidator())
        {
            CalcGregorian = ReactiveCommand.Create(Gregorian,isValid);
            CalcJulian    = ReactiveCommand.Create(Julian, isValid);
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

        #region DayProperty
        private Int32 _day;
        
        public Int32 Day
        {
            get => _day;
            set
            {
                this.RaiseAndSetIfChanged(ref _day, value);
                RaiseValidation(nameof(Day));
                RaiseValidation(nameof(Month));
                RaiseValidation(nameof(Year));
            }
        }

        #endregion

        #region MonthProperty
        private Int32 _month;
        public Int32 Month
        {
            get => _month;
            set
            {
                this.RaiseAndSetIfChanged(ref _month, value);
                RaiseValidation(nameof(Day));
                RaiseValidation(nameof(Month));
                RaiseValidation(nameof(Year));
            }
        }
        #endregion

        #region YearProperty
        private Int32 _year;
        public Int32 Year
        {
            get => _year;
            set
            {
                this.RaiseAndSetIfChanged(ref _year, value);
                RaiseValidation(nameof(Day));
                RaiseValidation(nameof(Month));
                RaiseValidation(nameof(Year));
            }
        }

        #endregion
        
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