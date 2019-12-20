using FluentAssertions;
using GameTracker.Models;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text.Json;
using System.Text;
using Newtonsoft.Json;
using System;
using GameTracker.Helper;
using Xunit.Extensions.Ordering;

namespace GameTracker.IntegrationTests
{
    public class DateHelperTest
    {
        private DateHelper _dateHelper = DateHelper.getInstance();

        [Fact]
        public void CreateTodayDate_THEN_CheckBeforeToday_ResultsInFalse()
        {
            var today = DateTime.Today;
            _dateHelper.checkBeforeToday(today);
        }
        
        [Fact]
        public void CreateOldDate_THEN_CheckBeforeToday_ResultsInTrue()
        {
            var today = DateTime.Today;
            var checkDate = new DateTime(today.Year-1, today.Month, today.Day);
            _dateHelper.checkBeforeToday(today);
        }
        
        [Fact]
        public void CreateTodayDate_THEN_CheckBeforeEqualsToday_ResultsInTrue()
        {
            var today = DateTime.Today;
            _dateHelper.checkBeforeEqualsToday(today);
        }
        
        [Fact]
        public void CreateFutureDate_THEN_CheckBeforeToday_ResultsInFalse()
        {
            var today = DateTime.Today;
            var checkDate = new DateTime(today.Year +1, today.Month, today.Day);
            _dateHelper.checkBeforeEqualsToday(checkDate);
        }
        
        [Fact]
        public void CreateFutureDate_THEN_CheckBeforeEqualsToday_ResultsInFalse()
        {
            var today = DateTime.Today;
            var checkDate = new DateTime(today.Year +1, today.Month, today.Day);
            _dateHelper.checkBeforeEqualsToday(checkDate);
        }
    }
}
