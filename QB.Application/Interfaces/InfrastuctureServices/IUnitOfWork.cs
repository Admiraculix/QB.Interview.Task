using QB.Application.Interfaces.Repositories;
using System;

namespace QB.Application.Interfaces.InfrastuctureServices
{
    public interface IUnitOfWork : IDisposable
    {
        ICityRepository Cities { get; }
        IStateRepository States { get; }
        ICountryRepository Countries { get; }

        int Commit();
    }
}
