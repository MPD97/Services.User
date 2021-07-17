using Services.User.Application;

namespace Services.User.Infrastructure
{
    public interface IAppContextFactory
    {
        IAppContext Create();
    }
}