using FellowshipOne.Api;
using FellowshipOne.API.Events.Model;
using System;

namespace FellowshipOne.API.Events.Sets
{
    public class Timezone : ApiSet<Event>
    {
        private readonly string _baseUrl = string.Empty;

        private const string GET_URL = "/groups/v1/groups/{0}";

        private const string AUTO_MATCH_URL = "/groups/v1/groups/{0}/system_automatch";

        protected override string GetUrl
        {
            get
            {
                return "/groups/v1/groups/{0}";
            }
        }

        public Timezone(F1OAuthTicket ticket, string baseUrl, ContentType contentType) : base(ticket, baseUrl, contentType)
        {
            this._baseUrl = baseUrl;
        }

        public void GetSystemAutoMatch(int id)
        {
            string str = string.Format(string.Concat(this._baseUrl, "/groups/v1/groups/{0}/system_automatch"), id);
            base.GetByUrl(str);
        }
    }
}