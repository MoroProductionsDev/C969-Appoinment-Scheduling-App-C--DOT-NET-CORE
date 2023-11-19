using MySqlX.XDevAPI.Relational;
using Org.BouncyCastle.Utilities;
using Scheduling_Library.Model.Structure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduling_Library.Model.Data
{
    public sealed class ClientScheduleRecord
    {
        public class UserRecord
        {
            public Integers UserId;
            public String UserName;
            public String Password;
            public DateTime CreateDate;
            public String CreatedBy;
            public DateTime LastUpdate;
            public String LastUpdateBy;
        }

        public class AppointmentRecord
        {
            public Integers AppointmentId;
            public Integers CustomerId;
            public Integers UserID;
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
            public Integers CustomerId;
            public String CustomerName;
            public Integers AddressId;
            public Boolean Active;
            public DateTime CreateDate;
            public String CreatedBy;
            public DateTime LastUpdate;
            public String LastUpdateBy;
        }

        public class AddressRecord
        {
            public Integers AddressId;
            public String Address;
            public String Address2;
            public Integers CityId;
            public String PostalCode;
            public String Phone;
            public DateTime CreateDate;
            public String CreatedBy;
            public DateTime LastUpdate;
            public String LastUpdateBy;
        }

        public class CityRecord
        {
            public Integers CityId;
            public String City;
            public Integers CountryId;
            public DateTime CreateDate;
            public String CreatedBy;
            public DateTime LastUpdate;
            public String LastUpdateBy;
        }

        public class CountryRecord
        {
            public Integers CountryId;
            public String Country;
            public DateTime CreateDate;
            public String CreatedBy;
            public DateTime LastUpdate;
            public String LastUpdateBy;
        }
    }
}
