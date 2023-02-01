using FellowshipOne.Api;
using FellowshipOne.API.Events.Model;
using System;

namespace FellowshipOne.API.Events.Sets
{
    public class Events : ApiSet<Event>
    {
        private readonly string _baseUrl = string.Empty;

        private const string GET_URL = "/events/v1/events/{0}/schedules";

        private const string AUTO_MATCH_URL = "/events/v1/events/{0}/system_automatch";

        protected override string GetUrl
        {
            get
            {
                return "/events/v1/events/{0}/schedules";
            }
        }

        public Events(F1OAuthTicket ticket, string baseUrl, ContentType contentType) : base(ticket, baseUrl, contentType)
        {
            this._baseUrl = baseUrl;
        }

        public void GetSystemAutoMatch(int id)
        {
            string str = string.Format(string.Concat(this._baseUrl, "/events/v1/events/{0}/system_automatch"), id);
            base.GetByUrl(str);
        }
    }
}