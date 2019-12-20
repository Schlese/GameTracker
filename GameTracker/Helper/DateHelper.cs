using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameTracker.Helper
{
    public sealed class DateHelper
    {
        private static DateHelper _instance;

        private DateHelper()
        {

        }

        public static DateHelper getInstance()
        {
            if (_instance == null) 
                _instance = new DateHelper();

            return _instance;
        }

        public bool checkBeforeEqualsToday(DateTime checkDate)
        {
            return checkDate.Date <= DateTime.Now.Date;
        }

        public bool checkBeforeToday(DateTime checkDate)
        {
            return checkDate.Date < DateTime.Now.Date;
        }
    }
}
