namespace Services.User.Core.Events
{
    public class UserRegistrationCompleted : IDomainEvent
    {
        public Entities.User User { get; }

        public UserRegistrationCompleted(Entities.User user)
        {
            User = user;
        }
    }
}