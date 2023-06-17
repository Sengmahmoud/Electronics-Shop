using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common.Exceptions
{
    public class BusinessValidationException:Exception
    {
        public BusinessValidationException()
        {

        }
        public BusinessValidationException(string message):base(message)
        {

        }
        public BusinessValidationException(string message, params object[] args) : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
