using Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels
{
    public class HomeViewModel
    {
        public Owner Owner{ get; set; }

        public List<ProtfolioItem> Portfolioitems { get; set; }
    }
}
