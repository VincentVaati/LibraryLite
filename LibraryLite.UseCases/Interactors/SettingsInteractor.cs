using System.Linq;
using System.Collections.Generic;
using LibraryLite.Core.Entities;
using LibraryLite.UseCases.Interfaces;
using System.Data.Entity;

namespace LibraryLite.UseCases.Interactors
{
    public class SettingsInteractor:ISettingsInteractor
    {
        private readonly IEntityRepository<ApplicationSettings> _settingsEntityRepository;

        public SettingsInteractor() { }
        public SettingsInteractor(IEntityRepository<ApplicationSettings> settingsEntityRepository)
        {
            _settingsEntityRepository = settingsEntityRepository;
        }
        public ApplicationSettings FindSettingsBy(int id)
        {
            return _settingsEntityRepository.GetEntity(id);
        }
        public IList<ApplicationSettings> FindAllSettings()
        {
            return _settingsEntityRepository.GetAll().ToList();
        }
        public void Add(ApplicationSettings entity)
        {
            _settingsEntityRepository.InsertOnCommit(entity);
            _settingsEntityRepository.CommitChangesAsync();
        }
        public void Delete(ApplicationSettings entity)
        {
            _settingsEntityRepository.DeleteOnCommit(entity);
            _settingsEntityRepository.CommitChangesAsync();
        }
        public void SaveChanges(ApplicationSettings appSettings)
        {
            _settingsEntityRepository.Entry(appSettings).State = EntityState.Modified;
            _settingsEntityRepository.CommitChangesAsync();
        }
    }
}
