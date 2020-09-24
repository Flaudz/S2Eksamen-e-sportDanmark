using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Text;
using System.Windows;
using static EsportDanmark.Classes.ApiProperties;

namespace EsportDanmark.Classes
{
    public class MiddleClass
    {
        DataBase db = new DataBase();
        List<Player> players = new List<Player>();
        List<Sponsor> sponsors = new List<Sponsor>();
        List<Tournement> tournements = new List<Tournement>();
        List<Employee> employees = new List<Employee>();

        public DataBase Db { get => db; }
        public List<Player> Players { get => players; set => players = value; }
        public List<Sponsor> Sponsors { get => sponsors; set => sponsors = value; }
        public List<Tournement> Tournements { get => tournements; set => tournements = value; }
        public List<Employee> Employees { get => employees; set => employees = value; }

        // Creating a player
        public void CreatePlayer(string name, string summonername, int rank, int phonenumber, string tournementtype)
        {
            ApiCall apiCall = new ApiCall();
            try
            {
                Root playerinfo = apiCall.GetPlayer(name);
                Player player = new Player(name, summonername, rank, phonenumber, tournementtype);
                db.AddNewPlayer(player);
            }
            catch
            {
                MessageBox.Show("Spiller navnet findes ikke tjek for stave fejl og prøv igen");
            }
        }

        // Creating a Tournement
        public void CreateTournement()
        {

        }

        // Creating a Sponsor
        public void CreateSponsor(string companyname, string branche, string playername, int playersaleryinput)
        {
            Sponsor sponsor = new Sponsor(companyname, branche, playername, playersaleryinput);
            db.AddNewSponsor(sponsor);
        }

        // Creating a Tournement
        public void CreateTournement(string tournementname, string playername, string refname)
        {
            Tournement tournement = new Tournement(tournementname, playername, refname);
            db.AddNewTournement(tournement);
        }

        // Creating a Employee
        public void CreateEmployee(string name, int phonenumber, int salary, string jobtype, int judgelevel)
        {
            Employee employee = new Employee(name, phonenumber, salary, jobtype, judgelevel);
            db.AddNewEmployee(employee);
        }

        // Getting player information
        public string GetPlayerInfo()
        {
            string playersInfoString = "";
            players = db.GetPlayers();
            foreach (Player player in players)
            {
                playersInfoString += $"Name: {player.Name} - Summonername: {player.Summonername} - Rank: {player.Rank} - Telefonnummber: {player.Phonenumber} - Turneringer: {player.Tournermenttype}\n";
            }
            return playersInfoString;
        }

        // Getting Sponsors information
        public string GetSponsorInfo()
        {
            string sponsorInfoString = "";
            sponsors = db.GetSponsors();
            foreach (Sponsor sponsor in sponsors)
            {
                sponsorInfoString += $"Name: {sponsor.Companyname} - Branche: {sponsor.Branche} - PlayerId: {sponsor.Playerid} - PlayerName: {sponsor.Playername} - PlayerSalery: {sponsor.Playersalery}\n";
            }
            return sponsorInfoString;
        }

        // Getting Tournement information
        public string GetTournementInfo()
        {
            string tournementInfoString = "";
            tournements = db.GetTournements();
            foreach (Tournement tournement in tournements)
            {
                tournementInfoString += $"TournementName: {tournement.Tournermentname} - PlayerId: {tournement.Playerid} - PlayerName: {tournement.Playername} - PlayerPhoneNumber: {tournement.Playerphonenumber} - RefId: {tournement.Refid} - RefName: {tournement.Refname} - RefPhoneNumber: {tournement.Refphonenumber} - RefLevel: {tournement.Reflevel}\n";
            }
            return tournementInfoString;
        }

        // Getting Employees information
        public string GetEmployeeInfo()
        {
            string employeeInfoString = "";
            employees = db.GetEmployees();
            foreach (Employee employee in employees)
            {
                employeeInfoString += $"Name: {employee.Name} - PhoneNumber: {employee.Phonenumber} - Salary: {employee.Salary} - JobType: {employee.Jobtype} - JudgeLevel: {employee.Judgelevel}\n";
            }
            return employeeInfoString;
        }


        // Deleteing Player information from database
        public void DeletePlayer(int phonenumber)
        {
            db.deletePlayer(phonenumber);
        }

        // Deleteing Sponsor Information from database
        public void deleteSponsor(string companyname)
        {
            db.deleteSponsor(companyname);
        }

        public void deleteTournement(string tournementname)
        {
            db.deleteTournement(tournementname);
        }

        // Deleteing Employee from database
        public void deleteEmployee(string employeeName)
        {
            db.deleteEmployee(employeeName);
        }

        // Update Player information
        public void updatePlayer(string name, string summonername, int rank, int phonenumber, string tournementype, int beforephonenumber)
        {
            db.updatePlayer(name, summonername, rank, phonenumber, tournementype, beforephonenumber);
        }


        // Update Sponsor Information
        public void updateSponsor(string companyname, string branche, string playername, int playersaleryinput, string beforecompanyname)
        {
            Sponsor sponsor = new Sponsor(companyname, branche, playername, playersaleryinput);
            db.updateSponsor(sponsor, beforecompanyname);
        }

        // Update Tournement Information
        public void updateTournement(string tournementname, string playername, string refname, string beforetournement)
        {
            Tournement tournement = new Tournement(tournementname, playername, refname);
            db.updateTournement(tournement, beforetournement);
        }

        public void updateEmployee(string employeename, int phonenumber, int salary, string jobtype, int judgelevel, string beforeemployee)
        {
            Employee employee = new Employee(employeename, phonenumber, salary, jobtype, judgelevel);

            db.UpdateEmployee(employee, beforeemployee);
        }
    }
}
