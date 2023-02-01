using FellowshipOne.Api.Model;
using System;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace FellowshipOne.API.Events.Model
{
    [XmlRoot("schedule")]
    public class Event : APIModel
    {
        [XmlElement("name")]
        public string Name
        {
            get;
            set;
        }

        public Event()
        {
        }

        public Event(int eventTypeId, int eventStatusId) : this()
        {
        }
    }
}