using BethysPieShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethysPieShop.ViewModels
{
    /* In this class we will list all the data needed inside out view.
     * We will use this class inside HomeController.
     */
    public class HomeViewModel
    {
        public string Title { get; set; }
        public List<Pie> Pies { get; set; }

    }
}
