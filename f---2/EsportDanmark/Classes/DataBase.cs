using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Metadata;
using System.Security.Permissions;
using System.Text;
using System.Windows;

namespace EsportDanmark.Classes
{
    public class DataBase
    {
        private string connectionString = "Data Source=CV-BB-5992;Initial Catalog=EsportDanmark;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        // Executes the specified query.
        private DataSet Execute(string query)
        {
            DataSet resultSet = new DataSet();
            using (SqlDataAdapter adapter = new SqlDataAdapter(new SqlCommand(query, new SqlConnection(connectionString))))
            {
                adapter.Fill(resultSet);
            }
            return resultSet;
        }


        // Add methodes

        // Making a mothod where there will be added a new player in the database table Players
        public void AddNewPlayer(Player player)
        {
            string addNewPlayerQuery =
                $"INSERT INTO Players (Name, SummonerName, Rank, PhoneNumber, TournermentType) VALUES ('{player.Name}', '{player.Summonername}', {player.Rank}, {player.Phonenumber}, '{player.Tournermenttype}')";
            try
            {
                Execute(addNewPlayerQuery);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        // Making a mothod where there will be added a new employee in the database table Employees
        public void AddNewEmployee(Employee employee)
        {
            string addNewEmploeeQuery =
                $"INSERT INTO Employees (Name, PhoneNumber, Salary, JobType, JudgeLevel) VALUES ('{employee.Name}', {employee.Phonenumber}, {employee.Salary}, '{employee.Jobtype}', {employee.Judgelevel})";
            try
            {
                Execute(addNewEmploeeQuery);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        // Making a mothod where there will be added a new sponsor in the database table Sponsorers
        public void AddNewSponsor(Sponsor sponsor)
        {
            string allPlayersQuery = $"SELECT Id FROM Players WHERE Name = '{sponsor.Playername}'";

            // Eksikver query og gemmer i en variabel
            DataSet resultSet = Execute(allPlayersQuery);

            // Får første table af data sættet og gemmer i en variabel
            DataTable playerTable = resultSet.Tables[0];



            foreach (DataRow playerRow in playerTable.Rows)
            {
                int id = (int)playerRow["Id"];
                string addNewSponsorQuery =
                    $"INSERT INTO Sponsorers (CompanyName, Branche, PlayerId, PlayerName, PlayerSalery) VALUES ('{sponsor.Companyname}', '{sponsor.Branche}', {id}, '{sponsor.Playername}', {sponsor.Playersalery})";
                try
                {
                    Execute(addNewSponsorQuery);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
            }

        }

        // Making a mothod where there will be added a new tournement in the database table Tournements
        public void AddNewTournement(Tournement tournement)
        {
            string allEmployeesQuery = $"SELECT Id, PhoneNumber, JudgeLevel, JobType FROM Employees WHERE Name = '{tournement.Refname}'";
            string allPlayersQuery = $"SELECT Id, PhoneNumber FROM Players WHERE Name = '{tournement.Playername}'";

            // Eksikver query og gemmer i en variabel
            DataSet resultSet = Execute(allEmployeesQuery);
            DataSet playerResultSet = Execute(allPlayersQuery);

            // Får første table af data sættet og gemmer i en variabel
            DataTable employeeTable = resultSet.Tables[0];
            DataTable playerTable = playerResultSet.Tables[0];

            foreach (DataRow employeeRow in employeeTable.Rows)
            {
                int refId = (int)employeeRow["Id"];
                int refPhonenumber = (int)employeeRow["PhoneNumber"];
                int refLevel = (int)employeeRow["JudgeLevel"];
                string jobtype = (string)employeeRow["JobType"];
                foreach (DataRow playerRow in playerTable.Rows)
                {
                    int playerId = (int)playerRow["Id"];
                    int playerPhonenumber = (int)playerRow["PhoneNumber"];
                    string addNewTournementQuery =
                        $"INSERT INTO Tournements (TournermentName, PlayerId, PlayersName, PlayersPhoneNumber, RefId, RefName, RefPhoneNumber, RefLevel) VALUES ('{tournement.Tournermentname}', {playerId}, '{tournement.Playername}', {playerPhonenumber}, {refId}, '{tournement.Refname}', {refPhonenumber}, {refLevel})";
                    try
                    {
                        if (jobtype == "Ref" || jobtype == "Judge")
                        {
                            Execute(addNewTournementQuery);
                        }
                        else
                        {
                            MessageBox.Show("Denne Ansat er ikke en dommer.");
                        }
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.ToString());
                    }
                }
            }

        }


        // Get information from database

        // Getting all players information from database
        public List<Player> GetPlayers()
        {
            List<Player> playersList = new List<Player>();
            string allPlayersQuery = "SELECT * FROM Players";

            // Eksikver query og gemmer i en variabel
            DataSet resultSet = Execute(allPlayersQuery);

            // Får første table af data sættet og gemmer i en variabel
            DataTable playerTable = resultSet.Tables[0];



            foreach (DataRow playerRow in playerTable.Rows)
            {
                string name = (string)playerRow["Name"];
                string summonername = (string)playerRow["SummonerName"];
                int rank = (int)playerRow["Rank"];
                int phonenumber = (int)playerRow["PhoneNumber"];
                string tournementtype = (string)playerRow["TournermentType"];
                Player player = new Player(name, summonername, rank, phonenumber, tournementtype);
                playersList.Add(player);
            }
            return playersList;
        }

        // Getting Sponsor information from database
        public List<Sponsor> GetSponsors()
        {
            List<Sponsor> sponsorsList = new List<Sponsor>();
            string allSponsorsQuery = "SELECT * FROM Sponsorers";

            // Eksikver query og gemmer i en variabel
            DataSet resultSet = Execute(allSponsorsQuery);

            // Får første table af data sættet og gemmer i en variabel
            DataTable sponsorTabel = resultSet.Tables[0];


            foreach (DataRow sponsorRow in sponsorTabel.Rows)
            {
                string companyname = (string)sponsorRow["CompanyName"];
                string branche = (string)sponsorRow["Branche"];
                int playerid = (int)sponsorRow["PlayerId"];
                string playername = (string)sponsorRow["PlayerName"];
                int playersalery = (int)sponsorRow["PlayerSalery"];
                Sponsor sponsor = new Sponsor(companyname, branche, playerid, playername, playersalery);
                sponsorsList.Add(sponsor);
            }
            return sponsorsList;
        }

        // Getting Tournements information from database
        public List<Tournement> GetTournements()
        {
            List<Tournement> TournementsList = new List<Tournement>();
            string allTournementsQuery = "SELECT * FROM Tournements";

            // Eksikver query og gemmer i en variabel
            DataSet resultSet = Execute(allTournementsQuery);

            // Får første table af data sættet og gemmer i en variabel
            DataTable tournementTable = resultSet.Tables[0];


            foreach (DataRow tournementRow in tournementTable.Rows)
            {
                string tournementname = (string)tournementRow["TournermentName"];
                int playerid = (int)tournementRow["PlayerId"];
                string playername = (string)tournementRow["PlayersName"];
                int playerphonenumber = (int)tournementRow["PlayersPhoneNumber"];
                int refif = (int)tournementRow["RefId"];
                string refname = (string)tournementRow["RefName"];
                int refphonenumber = (int)tournementRow["RefPhoneNumber"];
                int reflevel = (int)tournementRow["RefLevel"];
                Tournement tournement = new Tournement(tournementname, playerid, playername, playerphonenumber, refif, refname, refphonenumber, reflevel);
                TournementsList.Add(tournement);
            }
            return TournementsList;
        }

        // Får ansatte information fra databasen
        public List<Employee> GetEmployees()
        {
            List<Employee> employeesList = new List<Employee>();
            string allEmployeesQuery = "SELECT * FROM Employees";


            // Eksikver query og gemmer i en variabel
            DataSet resultSet = Execute(allEmployeesQuery);

            // Får første table af data sættet og gemmer i en variabel
            DataTable employeeTable = resultSet.Tables[0];


            foreach (DataRow employeeRow in employeeTable.Rows)
            {
                string name = (string)employeeRow["Name"];
                int phonenumber = (int)employeeRow["PhoneNumber"];
                int salary = (int)employeeRow["Salary"];
                string jobtype = (string)employeeRow["JobType"];
                int judgelevel = (int)employeeRow["JudgeLevel"];
                Employee employee = new Employee(name, phonenumber, salary, jobtype, judgelevel);
                employeesList.Add(employee);
            }
            return employeesList;
        }


        // Slette fra DataBasen

        // Slette spiller fra databasen
        public void deletePlayer(int phonenumber)
        {
            string allPlayersQuery = $"SELECT Name FROM Players WHERE PhoneNumber = {phonenumber}";

            // Eksikver query og gemmer i en variabel
            DataSet resultSet = Execute(allPlayersQuery);

            // Får Første table af data sættet og gemmer i en variabel
            DataTable playerTable = resultSet.Tables[0];

            foreach (DataRow person in playerTable.Rows)
            {
                string name = (string)person["Name"];
                string deleteSponsorQuery =
                    $"DELETE FROM Sponsorers WHERE PlayerName = '{name}'";
                Execute(deleteSponsorQuery);
            }

            string deletePlayerQuery =
                $"DELETE FROM Players WHERE PhoneNumber = {phonenumber}";
            Execute(deletePlayerQuery);
        }

        // Slette Sponsor fra databasen
        public void deleteSponsor(string comapanyname)
        {
            string deleteSponsorQuery =
                $"DELETE FROM Sponsorers WHERE CompanyName = '{comapanyname}'";
            Execute(deleteSponsorQuery);
        }

        // Slette Tournement fra databasen
        public void deleteTournement(string tournementname)
        {
            string deleteTournementQuery =
                $"DELETE FROM Tournements WHERE TournermentName = '{tournementname}'";
            Execute(deleteTournementQuery);
        }

        // Slette Employee fra databasen
        public void deleteEmployee(string employeeName)
        {
            string deleteEmployeeQuery =
                $"DELETE FROM Employees WHERE Name = '{employeeName}'";
        }

        // Opdater fra databasen

        // Opdatere player fra databasen
        public void updatePlayer(string name, string summonername, int rank, int phonenumber, string tournementtype, int beforephonenumber)
        {
            string updatePlayerQuery =
                $"UPDATE Players SET Name = '{name}', SummonerName = '{summonername}', Rank = {rank}, PhoneNumber = {phonenumber}, TournermentType = '{tournementtype}' WHERE PhoneNumber = {beforephonenumber}";
            Execute(updatePlayerQuery);
        }


        // Opdater sponsor i databasen
        public void updateSponsor(Sponsor sponsor, string beforecompanyname)
        {
            string updateSponsorQuery =
                $"UPDATE Sponsorers SET CompanyName = '{sponsor.Companyname}', Branche = '{sponsor.Branche}', PlayerId = {sponsor.Playerid}, PlayerName = '{sponsor.Playername}', PlayerSalery = {sponsor.Playersalery} WHERE CompanyName = '{beforecompanyname}'";
            Execute(updateSponsorQuery);
        }


        // Opdater Tournement
        public void updateTournement(Tournement tournement, string beforetournement)
        {
            string allEmployeesQuery = $"SELECT Id, PhoneNumber, JudgeLevel FROM Employees WHERE Name = '{tournement.Refname}'";
            string allPlayersQuery = $"SELECT Id, PhoneNumber FROM Players WHERE Name = '{tournement.Playername}'";

            // Eksikver query og gemmer i en variabel
            DataSet resultSet = Execute(allEmployeesQuery);
            DataSet playerResultSet = Execute(allPlayersQuery);

            // Får første table af data sættet og gemmer i en variabel
            DataTable employeeTable = resultSet.Tables[0];
            DataTable playerTable = playerResultSet.Tables[0];

            foreach (DataRow employeeRow in employeeTable.Rows)
            {
                int refId = (int)employeeRow["Id"];
                int refPhonenumber = (int)employeeRow["PhoneNumber"];
                int refLevel = (int)employeeRow["JudgeLevel"];
                foreach (DataRow playerRow in playerTable.Rows)
                {
                    int playerId = (int)playerRow["Id"];
                    int playerPhonenumber = (int)playerRow["PhoneNumber"];
                    string addupdateTournementQuery =
                        $"UPDATE Tournements SET TournermentName = '{tournement.Tournermentname}', PlayerId = {playerId}, PlayersName = '{tournement.Playername}', PlayersPhoneNumber = {playerPhonenumber}, RefId = {refId}, RefName = '{tournement.Refname}', RefPhoneNumber = {refPhonenumber}, RefLevel = {refLevel} WHERE TournermentName = '{beforetournement}'";
                    try
                    {
                        Execute(addupdateTournementQuery);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.ToString());
                    }
                }
            }
        }

        public void UpdateEmployee(Employee employee, string beforeemployee)
        {
            string updateEmployeeQuery =
                $"UPDATE Employees SET Name = '{employee.Name}', PhoneNumber = {employee.Phonenumber}, Salary = {employee.Salary}, JobType = '{employee.Jobtype}', JudgeLevel = {employee.Judgelevel} WHERE Name = '{beforeemployee}'";
            Execute(updateEmployeeQuery);
        }
    }
}