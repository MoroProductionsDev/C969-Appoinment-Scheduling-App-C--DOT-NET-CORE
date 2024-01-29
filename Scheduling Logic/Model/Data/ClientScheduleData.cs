namespace Scheduling_Logic.Model.Data
{
    // Class that have the types of each client's schedule database record type on the system
    public sealed class ClientScheduleData
    {
        public class UserRecord
        {
            public Int32 UserId;
            public String UserName = String.Empty;
            public String Password = String.Empty;
            public SByte Active;
            public DateTime CreateDate;
            public String CreatedBy = String.Empty;
            public DateTime LastUpdate;
            public String LastUpdateBy = String.Empty;
        }

        public class AppointmentRecord
        {
            public Int32 AppointmentId;
            public Int32 CustomerId;
            public Int32 UserID;
            public String Title = String.Empty;
            public String Description = String.Empty;
            public String Location = String.Empty;
            public String Contact = String.Empty;
            public String Type = String.Empty;
            public String Url = String.Empty;
            public DateTime Start;
            public DateTime End;
            public DateTime CreateDate;
            public String CreatedBy = String.Empty;
            public DateTime LastUpdate;
            public String LastUpdateBy = String.Empty;
        }

        public class CustomerRecord
        {
            public Int32 CustomerId;
            public String CustomerName = String.Empty;
            public Int32 AddressId;
            public Boolean Active;
            public DateTime CreateDate;
            public String CreatedBy = String.Empty;
            public DateTime LastUpdate;
            public String LastUpdateBy = String.Empty;
        }

        public class AddressRecord
        {
            public Int32 AddressId;
            public String Address = String.Empty;
            public String Address2 = String.Empty;
            public Int32 CityId;
            public String PostalCode = String.Empty;
            public String Phone = String.Empty;
            public DateTime CreateDate;
            public String CreatedBy = String.Empty;
            public DateTime LastUpdate;
            public String LastUpdateBy = String.Empty;
        }

        public class CityRecord
        {
            public Int32 CityId;
            public String City = String.Empty;
            public Int32 CountryId;
            public DateTime CreateDate;
            public String CreatedBy = String.Empty;
            public DateTime LastUpdate;
            public String LastUpdateBy = String.Empty;
        }

        public class CountryRecord
        {
            public Int32 CountryId;
            public String Country = String.Empty;
            public DateTime CreateDate;
            public String CreatedBy = String.Empty;
            public DateTime LastUpdate;
            public String LastUpdateBy = String.Empty;
        }
    }
}
