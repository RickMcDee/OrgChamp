using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace OrgChamp.ViewModels
{
    public class AddressViewModel
    {
        public AddressViewModel()
        {

        }

        public AddressViewModel(AddressViewModel original)
        {
            Street = original.Street;
            Housenumber = original.Housenumber;
            City = original.City;
            Zipcode = original.Zipcode;
            Country = original.Country;
        }

        public string? Street { get; set; }

        [Range(1, 1000)]
        public int? Housenumber { get; set; }

        public string? City { get; set; }

        [StringLength(5, ErrorMessage = "German zip codes always consist of 5 digits")]
        public string? Zipcode { get; set; }

        public string? Country { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj is AddressViewModel address)
            {
                return Street == address.Street
                    && Housenumber == address.Housenumber
                    && City == address.City
                    && Zipcode == address.Zipcode
                    && Country == address.Country;
            }

            return false;
        }


        public override int GetHashCode()
        {
            // https://stackoverflow.com/a/263416
            unchecked // Overflow is fine, just wrap
            {
                int hash = 17;
                foreach (PropertyInfo prop in this.GetType().GetProperties())
                {
                    var value = prop.GetValue(this, null);
                    if (value is not null)
                    {
                        hash = hash * 23 + value.GetHashCode();
                    }
                }

                return hash;
            }
        }
    }
}
