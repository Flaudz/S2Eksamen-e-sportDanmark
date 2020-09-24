using System;
using System.Collections.Generic;
using System.Text;

namespace EsportDanmark.Classes
{
    public class Tournement : Sponsor
    {
        private string tournermentname;
        private int playerphonenumber;
        private int refid;
        private string refname;
        private int refphonenumber;
        private int reflevel;


        public string Tournermentname { get => tournermentname; set => tournermentname = value; }
        public int Playerphonenumber { get => playerphonenumber; set => playerphonenumber = value; }
        public int Refid { get => refid; set => refid = value; }
        public string Refname { get => refname; set => refname = value; }
        public int Refphonenumber { get => refphonenumber; set => refphonenumber = value; }
        public int Reflevel { get => reflevel; set => reflevel = value; }

        public Tournement(string tournermentname, int playerid, string playername, int playerphonenumber, int refid, string refname, int refphonenumber, int reflevel)
        {
            Tournermentname = tournermentname;
            Playerid = playerid;
            Playername = playername;
            Playerphonenumber = playerphonenumber;
            Refid = refid;
            Refname = refname;
            Refphonenumber = refphonenumber;
            Reflevel = reflevel;
        }

        public Tournement(string tournermentname, string playername, string refname)
        {
            Tournermentname = tournermentname;
            Playername = playername;
            Refname = refname;
        }
    }
}
