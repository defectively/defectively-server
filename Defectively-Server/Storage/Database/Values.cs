using System.Collections.Generic;
using Defectively;

namespace DefectivelyServer.Storage.Database
{
    public class Values
    {
        public List<Account> Accounts { get; set; } = new List<Account>();
        public List<Rank> Ranks { get; set; } = new List<Rank>();
        public List<Channel> Channels { get; set; } = new List<Channel>();
        public List<Punishment> Punishments { get; set; } = new List<Punishment>();
    }
}
