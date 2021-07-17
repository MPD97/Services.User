using System;
using System.Collections.Generic;
using System.Linq;
using Services.User.Core.Events;
using Services.User.Core.Exceptions;

namespace Services.User.Core.Entities
{
    public class User : AggregateRoot
    {
        private ISet<Guid> _completedRuns = new HashSet<Guid>();

        public string Email { get; private set; }
        public string FullName { get; private set; }
        public string Address { get; private set; }
        public State State { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public IEnumerable<Guid> CompletedRuns
        {
            get => _completedRuns;
            set => _completedRuns = new HashSet<Guid>(value);
        }

        public User(Guid id, string email, DateTime createdAt) : this(id, email, createdAt, string.Empty,
            string.Empty, State.Incomplete, Enumerable.Empty<Guid>())
        {
        }

        public User(Guid id, string email, DateTime createdAt, string fullName, string address, 
            State state, IEnumerable<Guid> completedRuns = null)
        {
            Id = id;
            Email = email;
            CreatedAt = createdAt;
            FullName = fullName;
            Address = address;
            CompletedRuns = completedRuns ?? Enumerable.Empty<Guid>();
            State = state;
        }

        public void CompleteRegistration(string fullName, string address)
        {
            if (string.IsNullOrWhiteSpace(fullName))
            {
                throw new InvalidUserFullNameException(Id, fullName);
            }

            if (string.IsNullOrWhiteSpace(address))
            {
                throw new InvalidUserAddressException(Id, address);
            }

            if (State != State.Incomplete)
            {
                throw new CannotChangeUserStateException(Id, State);
            }

            FullName = fullName;
            Address = address;
            State = State.Valid;
            AddEvent(new UserRegistrationCompleted(this));
        }

        public void SetValid() => SetState(State.Valid);
        
        public void SetIncomplete() => SetState(State.Incomplete);

        public void Lock() => SetState(State.Locked);

        public void MarkAsSuspicious() => SetState(State.Suspicious);

        private void SetState(State state)
        {
            var previousState = State;
            State = state;
            AddEvent(new UserStateChanged(this, previousState));
        }

        public void AddCompletedRun(Guid runId)
        {
            if (runId == Guid.Empty)
            {
                return;
            }

            _completedRuns.Add(runId);
        }
    }
}