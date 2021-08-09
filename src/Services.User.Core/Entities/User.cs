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
        private static Regex _pseudonymRegex = new Regex(@"^[a-zA-Z]\w*$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(100));
        
        private ISet<Guid> _completedRuns = new HashSet<Guid>();

        public string Email { get; private set; }
        public string Pseudonym { get; private set; }
        public State State { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? LockedAt { get; private set; }
        
        public string LockedReason { get; private set; }

        public IEnumerable<Guid> CompletedRuns
        {
            get => _completedRuns;
            set => _completedRuns = new HashSet<Guid>(value);
        }

        public User(Guid id, string email, DateTime createdAt) : this(id, email, createdAt, string.Empty,
            State.Incomplete, Enumerable.Empty<Guid>())
        {
        }

        public User(Guid id, string email, DateTime createdAt, string pseudonym,
            State state, IEnumerable<Guid> completedRuns = null)
        {
            Id = id;
            Email = email;
            CreatedAt = createdAt;
            Pseudonym = pseudonym;
            CompletedRuns = completedRuns ?? Enumerable.Empty<Guid>();
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

            if (!_pseudonymRegex.Match(pseudonym).Success)
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

        public void Lock(string reason, DateTime date)
        {
            SetState(State.Locked);
            LockedAt = date;
        }


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