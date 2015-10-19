using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeVault.Models.ViewModels
{
    public class PrerequisiteViewModel : IDisposable
    {
        CV2Context db = new CV2Context();

        public void Dispose()
        {
            db.Dispose();
        }

        public PrerequisiteViewModel(Product viewProduct)
        {
            var preInstallDependencies = db.Dependencies.Where(d => d.DependencyType == DependencyType.PreInstall && d.BaseProductId == viewProduct.ProductId);
            var postInstallDependencies = db.Dependencies.Where(d => d.DependencyType == DependencyType.PostInstall && d.BaseProductId == viewProduct.ProductId);
        }
    }

}