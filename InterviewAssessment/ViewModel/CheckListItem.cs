
using DatabaseSchemaEngine.Lookup;
using GalaSoft.MvvmLight;

namespace DomainModelEditor.ViewModel
{
    public class CheckListItem : ViewModelBase
    {
        public ILookup Lookup { get; set; }

        public bool Checked { get; set; }
    }
}
