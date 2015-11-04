using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using CodeVault.Models;

namespace CodeVault.ViewModels
{
    public class PermissionDetailViewModel
    {
        public PermissionDetailViewModel()
        {
                
        }

        public int? Id { get; set; }

        public string Permission { get; set; }

        public string Type { get; set; }
        public string GroupUser { get; set; }
        public string Location { get; set; }
    }
}