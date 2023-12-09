using Hotel.Presentation.Activities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Presentation.Registration.Model
{
    public class RegistrationUI
    {
        public int Id { get; set; }
        public ActivityUI Activity { get; set; }
        public List<MemberUI> Members { get; set; }
    }
}
