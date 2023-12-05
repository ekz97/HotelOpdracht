using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Presentation.Activities.Model
{
    public class DescriptionUI
    {
        public DescriptionUI(int duration, string explanation, string location, string name)
        {
            Duration = duration;
            Explanation = explanation;
            Location = location;
            Name = name;
        }

        public int Duration { get; set; }
        public string Explanation { get; set; }
        public string Location { get; set; }
        public string Name { get; set; }

    }
}
