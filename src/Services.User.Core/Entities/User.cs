using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Services.User.Core.Events;
using Services.User.Core.Exceptions;

namespace Services.User.Core.Entities
{
    public class User : AggregateRoot
    {
        private static readonly Regex PseudonymRegex = new Regex(@"^[a-zA-Z]\w*$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(100));
        
        public string Pseudonym { get; private set; }
        public State State { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? LockedAt { get; private set; }
        public Guid? LockedBy { get; private set; }
        public string LockedReason { get; private set; }

        public User(Guid id, string email, DateTime createdAt) : this(id, createdAt, string.Empty,
            State.Incomplete)
        {
        }

        public User(Guid id, DateTime createdAt, string pseudonym,
            State state)
        {
            Id = id;
            CreatedAt = createdAt;
            Pseudonym = pseudonym;
            State = state;
        }

        public void CompleteRegistration(string pseudonym)
        {
            if (string.IsNullOrWhiteSpace(pseudonym))
            {
                throw new InvalidUserPseudonymException(Id, pseudonym);
            }

            const int minLength = 5;
            const int maxLength = 15;
            if (pseudonym.Length is < minLength or > maxLength)
            {
                throw new InvalidUserPseudonymLengthException(Id, pseudonym.Length, minLength, maxLength);
            }

            if (!PseudonymRegex.Match(pseudonym).Success)
            {
                throw new InvalidUserPseudonymException(Id, pseudonym);
            }

            if (State != State.Incomplete)
            {
                throw new CannotChangeUserStateException(Id, State);
            }

            Pseudonym = pseudonym;
            State = State.Valid;
            AddEvent(new UserRegistrationCompleted(this));
        }

        public void SetValid() => SetState(State.Valid);
        
        public void SetIncomplete() => SetState(State.Incomplete);

        public void Lock(Guid lockBy, string reason, DateTime date)
        {
            const int minLength = 10;
            const int maxLength = 1024;
            if (string.IsNullOrWhiteSpace(reason))
                throw new InvalidLockReasonLengthException(Id, 0, minLength, maxLength);   
            
            if(reason.Length is < minLength or > maxLength)
                throw new InvalidLockReasonLengthException(Id, reason.Length, minLength, maxLength);   

            SetState(State.Locked);
            LockedReason = reason;
            LockedAt = date;
            LockedBy = lockBy;
        }
        private void SetState(State state)
        {
            var previousState = State;
            State = state;
            AddEvent(new UserStateChanged(this, previousState));
        }
    }
}