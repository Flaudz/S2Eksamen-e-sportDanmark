using System;
using System.Collections.Generic;
using System.Text;

namespace EsportDanmark.Classes
{
    public class Player
    {
        //Fields
        private int id;
        private string name;
        private string summonername;
        private int rank;
        private int phonenumber;
        private string tournermenttype;

        public int Id { get => id;}
        public string Name { get => name; set => name = value; }
        public string Summonername { get => summonername; set => summonername = value; }
        public int Rank { get => rank; set => rank = value; }
        public int Phonenumber { get => phonenumber; set => phonenumber = value; }
        public string Tournermenttype { get => tournermenttype; set => tournermenttype = value; }

        // Constructors

        public Player(string name, string summonername, int rank, int phonenumber, string tournermenttype)
        {
            Name = name;
            Summonername = summonername;
            Rank = rank;
            Phonenumber = phonenumber;
            Tournermenttype = tournermenttype;
        }
    }
}
