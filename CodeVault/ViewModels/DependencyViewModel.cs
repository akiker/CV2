using System.ComponentModel;

namespace CodeVault.ViewModels
{
    public class DependencyViewModel
    {
        public DependencyViewModel()
        {

        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }

        [DisplayName("Install Order")]
        public int InstallOrder { get; set; }
    }
}