using FellowshipOne.Api;
using FellowshipOne.API.Events.Sets;
using System;
using System.Runtime.CompilerServices;

namespace FellowshipOne.API.Realms
{
    public class EventRealm
    {
        private FellowshipOne.API.Events.Sets.Events _events;

        private string _baseUrl
        {
            get;
            set;
        }

        private ContentType _contentType
        {
            get;
            set;
        }

        private F1OAuthTicket _ticket
        {
            get;
            set;
        }

        public FellowshipOne.API.Events.Sets.Events Events
        {
            get
            {
                if (this._events == null)
                {
                    this._events = new FellowshipOne.API.Events.Sets.Events(this._ticket, this._baseUrl, this._contentType);
                }
                return this._events;
            }
        }

        public EventRealm(F1OAuthTicket ticket, string baseUrl, ContentType contentType)
        {
            this._ticket = ticket;
            this._baseUrl = baseUrl;
            this._contentType = contentType;
        }
    }
}