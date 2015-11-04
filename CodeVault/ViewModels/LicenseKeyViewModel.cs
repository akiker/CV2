using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CodeVault.ViewModels
{
    public class LicenseKeyViewModel
    {
        public int ? Id { get; set; }

        [DisplayName("Key Data")]
        public string KeyData { get; set; }

        [DisplayName("Owner Name")]
        public string OwnerName { get; set; }

        [DisplayName("Owner Email")]
        public string OwnerEmail { get; set; }

        [DisplayName("Owner Phone Number")]
        public string LicenseKeyOwnerPhoneNumber { get; set; }

    }
}