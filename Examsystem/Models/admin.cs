//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Examsystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class admin
    {

        [Display(Name = "Admin ID")]
        [Required(ErrorMessage = "Admin Id is required")]
        public string admin_id { get; set; }
        [Display(Name = "Admin Name")]
        [Required(ErrorMessage = "Admin Name is required")]
        public string admin_name { get; set; }
        [Display(Name = "Admin Password")]
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string admin_password { get; set; }
    }
}
