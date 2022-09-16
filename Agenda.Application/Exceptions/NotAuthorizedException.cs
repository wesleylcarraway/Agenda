namespace Agenda.Application.Exceptions
{
    public class NotAuthorizedException : Exception
    {
        public NotAuthorizedException() : base("User without proper permissions.")
        {
        }
    }
}
