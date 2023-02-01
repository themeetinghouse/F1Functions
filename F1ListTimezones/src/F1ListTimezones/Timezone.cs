using FellowshipOne.Api.Model;
using System;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace FellowshipOne.API.Events.Model
{
    [XmlRoot("timezone")]
    public class Timezone : APIModel
    {
        [XmlElement("name")]
        public string Name
        {
            get;
            set;
        }

        public Timezone()
        {
        }

        public Timezone(int timezoneTypeId, int timezoneStatusId) : this()
        {
        }
    }
}