using Hotel.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Presentation.Activities.Model
{
    public class OrganiserUI 
    {
        public int Id { get; set; }
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
            }
        }
        public OrganiserUI(int id , string name)
        {
            Id = id;
            Name = name;
        }

        public override string? ToString()
        {
            return _name;
        }
    }

}
