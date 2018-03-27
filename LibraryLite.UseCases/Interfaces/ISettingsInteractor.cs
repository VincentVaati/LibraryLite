using System.Collections.Generic;
using LibraryLite.Core.Entities;

namespace LibraryLite.UseCases.Interfaces
{
    public interface ISettingsInteractor
    {
        ApplicationSettings FindSettingsBy(int id);
        IList<ApplicationSettings> FindAllSettings();
        void Add(ApplicationSettings entity);
        void SaveChanges(ApplicationSettings entity);
        void Delete(ApplicationSettings entity);
    }
}
