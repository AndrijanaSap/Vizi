using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vizi.Models
{
    public class RequiredCollection : RequiredAttribute
    {
        public override bool IsValid(object value)
        {
            bool isValid = base.IsValid(value);

            if (isValid)
            {
                ICollection collection = value as ICollection;
                if (collection != null)
                {
                    isValid = collection.Count != 0;
                }
            }
            return isValid;
        }
    }

    public class Restaurant
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [RegularExpression(@"^(07)[1,5,7,0]\d{6}$",ErrorMessage ="Please enter a valid number(ex. 071234567)")]
        public string Telephone { get; set; }
        public string ManagerId { get; set; }
        [Required]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$",ErrorMessage ="Not a valid E-mail Address")]
        public string Email { get; set; }
        [Display(Name ="Cover Photo")]
        public byte[] Picture { get; set; }
        [Display(Name = "Slide Photo")]
        public byte[] SidePicture1 { get; set; }
        [Display(Name = "Slide Photo")]
        public byte[] SidePicture2 { get; set; }
        [Display(Name = "Slide Photo")]
        public byte[] SidePicture3 { get; set; }
        [Display(Name = "Slide Photo")]
        public byte[] SidePicture4 { get; set; }
    }
}