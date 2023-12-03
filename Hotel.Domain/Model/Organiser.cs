using Hotel.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Model
{
    public class Organiser
    {
        public int? Id { get; set; }

        public ContactInfo Contact { get; set; }
        private string _name;
        public List<Activity> _activities { get; set; }
        public IReadOnlyList<Activity> GetActivities() { return _activities.AsReadOnly(); }

        public void AddActivity(Activity activity)
        {
            if (!_activities.Contains(activity))
                _activities.Add(activity);
            else
                throw new OrganiserException("AddActivity");
        }

        public void RemoveActivity(Activity activity)
        {
            if (_activities.Contains(activity))
                _activities.Remove(activity);
            else
                throw new OrganiserException("RemoveActivity");
        }
        public string Name
        {
            get { return _name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length > 500)
                    throw new OrganiserException("Invalid name");

                _name = value;
            }
        }


        public Organiser(int? id, string name, List<Activity> activities) 
        {
            Id = id;
            _name = name;
            _activities = activities;
        }


        public Organiser(int? id, string name, List<Activity> activities, ContactInfo contactInfo)
        {
            Id = id;
            _name = name;
            _activities = activities;
            Contact = contactInfo;
        }




    }
}
