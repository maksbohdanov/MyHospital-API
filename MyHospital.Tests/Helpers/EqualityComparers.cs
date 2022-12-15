using DAL.Entities;
using System.Diagnostics.CodeAnalysis;

namespace MyHospital.Tests.Helpers
{
    internal class PatientEqualityComparer : IEqualityComparer<Patient>
    {
        public bool Equals([AllowNull] Patient x, [AllowNull] Patient y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id == y.Id &&
                x.FullName == y.FullName &&
                x.Phone == y.Phone;
        }

        public int GetHashCode([DisallowNull] Patient obj)
        {
            return obj.GetHashCode();
        }
    }

    internal class DoctorEqualityComparer : IEqualityComparer<Doctor>
    {
        public bool Equals([AllowNull] Doctor x, [AllowNull] Doctor y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id == y.Id &&
                x.FirstName == y.FirstName &&
                x.LastName == y.LastName &&
                x.MiddleName == y.MiddleName &&
                x.Experience == y.Experience &&
                x.SpecializationId == y.SpecializationId;
        }

        public int GetHashCode([DisallowNull] Doctor obj)
        {
            return obj.GetHashCode();
        }
    }
    
    internal class FavorEqualityComparer : IEqualityComparer<Favor>
    {
        public bool Equals([AllowNull] Favor x, [AllowNull] Favor y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id == y.Id && 
                x.Price== y.Price &&
                x.FavorNameId == y.FavorNameId &&
                x.FavorTypeId == y.FavorTypeId &&
                x.SpecializationId == y.SpecializationId;
        }

        public int GetHashCode([DisallowNull] Favor obj)
        {
            return obj.GetHashCode();
        }
    }

    internal class AppointmentEqualityComparer : IEqualityComparer<Appointment>
    {
        public bool Equals([AllowNull] Appointment x, [AllowNull] Appointment y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id == y.Id &&
                x.Date == y.Date &&
                x.DoctorId == y.DoctorId &&
                x.FavorId == y.FavorId &&
                x.PatientId == y.PatientId;
        }

        public int GetHashCode([DisallowNull] Appointment obj)
        {
            return obj.GetHashCode();
        }
    }
}
