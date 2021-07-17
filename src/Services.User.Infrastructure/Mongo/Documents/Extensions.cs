using Services.User.Application.DTO;

namespace Services.User.Infrastructure.Mongo.Documents
{
    public static class Extensions
    {
        public static Core.Entities.User AsEntity(this UserDocument document)
            => new Core.Entities.User(document.Id, document.Email, document.CreatedAt, document.FullName, document.Address,
                document.State, document.CompletedRuns);

        public static UserDocument AsDocument(this Core.Entities.User entity)
            => new UserDocument
            {
                Id = entity.Id,
                Email = entity.Email,
                FullName = entity.FullName,
                Address = entity.Address,
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
                Email = document.Email,
                FullName = document.FullName,
                Address = document.Address,
                State = document.State.ToString().ToLowerInvariant(),
                CreatedAt = document.CreatedAt,
                CompletedRuns = document.CompletedRuns
            };
    }
}