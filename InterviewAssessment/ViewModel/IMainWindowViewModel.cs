using DomainModelEditor.UserControl;
using System.Collections.Generic;

namespace DomainModelEditor.ViewModel
{
    public interface IMainWindowViewModel : IViewModel
    {
        void AddEntity(string name, int x, int y);

        List<IEntityViewModel> Entities { get; }
    }
}
