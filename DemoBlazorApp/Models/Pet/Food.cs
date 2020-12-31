using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoBlazorApp.Models.Pet
{
    public class Food : GenericBase<IEntity>
    {
        public string BrandName { get; set; }
    }
}
