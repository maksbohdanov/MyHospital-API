namespace BLL.Exceptions
{
    public class HospitalException: Exception
    {
        public HospitalException()
        {
        }

        public HospitalException(string message) : base(message)
        {
        }

        public HospitalException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
