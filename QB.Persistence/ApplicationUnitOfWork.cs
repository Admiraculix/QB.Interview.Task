using QB.Application.Interfaces.InfrastuctureServices;
using QB.Application.Interfaces.Repositories;
using QB.Persistence.Sqlite;
using System;

namespace QB.Persistence
{
    public class ApplicationUnitOfWork : IUnitOfWork
    {
        private readonly SqliteDbContext _context;

        public ApplicationUnitOfWork(
            SqliteDbContext applicationDbContext,
            ICountryRepository countryRepository,
            IStateRepository stateRepository,
            ICityRepository cityRepository)
        {
            _context = applicationDbContext;

            Countries = countryRepository;
            States = stateRepository;
            Cities = cityRepository;
        }

        public ICityRepository Cities { get; }
        public IStateRepository States { get; }
        public ICountryRepository Countries { get; }

        public int Commit()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}
