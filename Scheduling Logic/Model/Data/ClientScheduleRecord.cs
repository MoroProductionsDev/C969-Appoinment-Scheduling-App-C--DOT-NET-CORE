using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduling_Logic.Model.Data
{
    public sealed class ClientScheduleRecord
    {
        public class UserRecord
        {
            public Int32 UserId;
            public String UserName;
            public String Password;
            public DateTime CreateDate;
            public String CreatedBy;
            public DateTime LastUpdate;
            public String LastUpdateBy;
        }

        public class AppointmentRecord
        {
            public Int32 AppointmentId;
            public Int32 CustomerId;
            public Int32 UserID;
            public String Title;
            public String Description;
            public String Location;
            public String Contact;
            public String Type;
            public String Url;
            public DateTime Start;
            public DateTime End;
            public DateTime CreateDate;
            public String CreatedBy;
            public DateTime LastUpdate;
            public String LastUpdateBy;
        }

        public class CustomerRecord
        {
            public Int32 CustomerId;
            public String CustomerName;
            public Int32 AddressId;
            public Boolean Active;
            public DateTime CreateDate;
            public String CreatedBy;
            public DateTime LastUpdate;
            public String LastUpdateBy;
        }

        public class AddressRecord
        {
            public Int32 AddressId;
            public String Address;
            public String Address2;
            public Int32 CityId;
            public String PostalCode;
            public String Phone;
            public DateTime CreateDate;
            public String CreatedBy;
            public DateTime LastUpdate;
            public String LastUpdateBy;
        }

        public class CityRecord
        {
            public Int32 CityId;
            public String City;
            public Int32 CountryId;
            public DateTime CreateDate;
            public String CreatedBy;
            public DateTime LastUpdate;
            public String LastUpdateBy;
        }

        public class CountryRecord
        {
            public Int32 CountryId;
            public String Country;
            public DateTime CreateDate;
            public String CreatedBy;
            public DateTime LastUpdate;
            public String LastUpdateBy;
        }
    }
}
