using Services.User.Core.Entities;

namespace Services.User.Core.Events
{
    public class UserStateChanged : IDomainEvent
    {
        public Entities.User User { get; }
        public State PreviousState { get; }

        public UserStateChanged(Entities.User user, State previousState)
        {
            User = user;
            PreviousState = previousState;
        }
    }
}