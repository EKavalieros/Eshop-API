using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop.Models
{
    public class ProductFilters
    {
        public enum PageSizeEnum
        {
            View6 = 6,
            View12 = 12
        }

        public PageSizeEnum? PageSize { get; set; }
        public Guid? BrandID { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }

        public bool IsPriceValid()
        {
            bool valid = true;

            if (MinPrice != null && MinPrice < 0)
            {
                valid = false;
            }

            if (MaxPrice != null && MaxPrice < MinPrice)
            {
                valid = false;
            }

            return valid;
        }

    }
}
