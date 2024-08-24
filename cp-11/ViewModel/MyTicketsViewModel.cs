using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cp_11.Logic.model;
using cp_11.ViewModel.Base;

namespace cp_11.ViewModel
{
    public class MyTicketsViewModel : BindableBase
    {
        List<Ticket> _tickets;

        public List<Ticket> Tickets
        {
            get => _tickets;
            set => Set(ref _tickets, value);
        }
    }
}

