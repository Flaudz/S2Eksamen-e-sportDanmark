using System;
using System.Collections.Generic;
using System.Text;

namespace EsportDanmark.Classes
{
    public class ApiProperties
    {
        public class Root
        {
            public string id { get; set; }
            public string accountId { get; set; }
            public string puuid { get; set; }
            public string name { get; set; }
            public int profileIconId { get; set; }
            public long revisionDate { get; set; }
            public int summonerLevel { get; set; }
        }

    }
}
