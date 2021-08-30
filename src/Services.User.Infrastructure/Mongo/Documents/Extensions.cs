using Services.User.Application.DTO;

namespace Services.User.Infrastructure.Mongo.Documents
{
    public static class Extensions
    {
        public static Core.Entities.User AsEntity(this UserDocument document)
            => new Core.Entities.User(document.Id, document.CreatedAt, document.Pseudonym,
                document.State);

        public static UserDocument AsDocument(this Core.Entities.User entity)
            => new UserDocument
            {
                Id = entity.Id,
                Pseudonym = entity.Pseudonym,
                State = entity.State,
                CreatedAt = entity.CreatedAt,
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
            };
    }
}