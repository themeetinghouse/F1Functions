using FellowshipOne.Api;
using FellowshipOne.API.Events.Sets;
using System;
using System.Runtime.CompilerServices;

namespace FellowshipOne.API.Realms
{
    public class TimezoneRealm
    {
        private FellowshipOne.API.Events.Sets.Timezone _timezone;

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

        public FellowshipOne.API.Events.Sets.Timezone Timezone
        {
            get
            {
                if (this._timezone == null)
                {
                    this._timezone = new FellowshipOne.API.Events.Sets.Timezone(this._ticket, this._baseUrl, this._contentType);
                }
                return this._timezone;
            }
        }

        public TimezoneRealm(F1OAuthTicket ticket, string baseUrl, ContentType contentType)
        {
            this._ticket = ticket;
            this._baseUrl = baseUrl;
            this._contentType = contentType;
        }
    }
}