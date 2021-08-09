using Services.User.Application.DTO;

namespace Services.User.Infrastructure.Mongo.Documents
{
    public static class Extensions
    {
        public static Core.Entities.User AsEntity(this UserDocument document)
            => new Core.Entities.User(document.Id, document.Email, document.CreatedAt, document.Pseudonym,
                document.State, document.CompletedRuns);

        public static UserDocument AsDocument(this Core.Entities.User entity)
            => new UserDocument
            {
                Id = entity.Id,
                Email = entity.Email,
                Pseudonym = entity.Pseudonym,
                State = entity.State,
                CreatedAt = entity.CreatedAt,
                CompletedRuns = entity.CompletedRuns
            };

        public static UserDto AsDto(this UserDocument document)
            => new UserDto
            {
                Id = document.Id,
                State = document.State.ToString().ToLowerInvariant(),
                CreatedAt = document.CreatedAt,
            };

        public static UserDetailsDto AsDetailsDto(this UserDocument document)
            => new UserDetailsDto
            {
                Id = document.Id,
                Pseudonym = document.Pseudonym,
                State = document.State.ToString().ToLowerInvariant(),
                CreatedAt = document.CreatedAt,
                CompletedRuns = document.CompletedRuns
            };
    }
}