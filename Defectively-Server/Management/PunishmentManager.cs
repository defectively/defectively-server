using System;
using System.Linq;
using Defectively;
using Defectively.Command;
using DefectivelyServer.Internal;
using Newtonsoft.Json;

namespace DefectivelyServer.Management
{
    //public class PunishmentManager
    //{
    //    public static void CreateRecord(Punishment punishment) {
    //        var ERecordId = CheckForRecords(punishment.AccountId, punishment.Type);
    //        if (ERecordId != "-1")
    //            DisposeRecord(ERecordId);
    //        Server.Database.Punishments.Add(punishment);
    //        Storage.Database.Helper.Save();

    //        var Connection = Server.Connections.Find(c => c.Owner.Id == punishment.AccountId);
    //        if (!Connection.Owner.Online)
    //            return;
    //        Enumerations.ClientState State;
    //        if (punishment.Type == Enumerations.PunishmentType.Bann || punishment.Type == Enumerations.PunishmentType.BannTemporarily) {
    //            State = Enumerations.ClientState.Banned;
    //        } else {
    //            State = Enumerations.ClientState.Muted;
    //        }
    //        Connection.SetStreamContent(string.Join("|", Enumerations.Action.SetState.ToString(), State.ToString(), JsonConvert.SerializeObject(punishment)));

    //        // Extension Management
    //        ListenerManager.InvokeEvent(Event.PunishmentRecorded, punishment);
    //        //End
    //    }

    //    public static string CheckForRecords(string accountId, params Enumerations.PunishmentType[] types) {
    //        DisposeExceeded();
    //        foreach (var PType in types) {
    //            var Punishment = Server.Database.Punishments.Find(p => p.Type == PType && p.AccountId == accountId);
    //            if (Punishment != null)
    //                return Punishment.Id;
    //        }
    //        return "-1";
    //    }

    //    public static Punishment GetRecord(string id) {
    //        return Server.Database.Punishments.Find(p => p.Id == id);
    //    }

    //    public static void DisposeExceeded() {
    //        var ActiveRecords = Server.Database.Punishments.FindAll(p => p.EndDate.CompareTo(DateTime.Now) > 0 || p.Type == Enumerations.PunishmentType.Bann);
    //        Server.Database.Punishments.Clear();
    //        Server.Database.Punishments.AddRange(ActiveRecords);
    //        Storage.Database.Helper.Save();
    //    }

    //    public static void DisposeRecord(string id) {
    //        var ActiveRecords = Server.Database.Punishments.FindAll(p => p.Id != id);
    //        Server.Database.Punishments.Clear();
    //        Server.Database.Punishments.AddRange(ActiveRecords);
    //        Storage.Database.Helper.Save();
    //    }
    //}

    public static class PunishmentManager
    {
        public static void Register(Punishment punishment) {
            DisposeExceeded();
            Server.Database.Punishments.Add(punishment);
            Storage.Database.Helper.Save();

            var Message = new MessagePacket();

            // Triggers
            if (punishment.Type == Enumerations.PunishmentType.Mute) {
                Message = new MessagePacket {
                    Content = $"@{punishment.CreatorId} muted you until:<br />{punishment.EndDate:U}.<br />Reason: {punishment.Reason}.",
                    RankColor = "#FFA600",
                    SenderId = "server",
                    SenderPrefix = "[Server] Server",
                    Time = DateTime.Now.ToShortTimeString(),
                    Type = Enumerations.MessageType.Left
                };
            } else if (punishment.Type == Enumerations.PunishmentType.Ban) {
                Message = new MessagePacket {
                    Content = $"@{punishment.CreatorId} banned you until:<br />{punishment.EndDate:U}.<br />Reason: {punishment.Reason}.",
                    RankColor = "#FFA600",
                    SenderId = "server",
                    SenderPrefix = "[Server] Server",
                    Time = DateTime.Now.ToShortTimeString(),
                    Type = Enumerations.MessageType.Left
                };
            }

            Server.Connections.Find(c => c.Owner.Id == punishment.AccountId)?.SetStreamContent(string.Join("|", Enumerations.Action.Plain, JsonConvert.SerializeObject(Message)));
        }

        public static bool CheckForAny(string id) {
            DisposeExceeded();
            return Server.Database.Punishments.Any(p => p.AccountId == id);
        }

        public static Punishment GetPunishment(string id) {
            DisposeExceeded();
            return Server.Database.Punishments.Find(p => p.AccountId == id);
        }

        public static void DisposeExceeded() {
            if (Server.Database.Punishments.Any(p => p.IsExceeded)) {
                var Message = new MessagePacket {
                    Content = "Your punishment exceeded.",
                    RankColor = "#FFA600",
                    SenderId = "server",
                    SenderPrefix = "[Server] Server",
                    Time = DateTime.Now.ToShortTimeString(),
                    Type = Enumerations.MessageType.Left
                };
                Server.Database.Punishments.Select(p => Server.Connections.Find(c => c.Owner.Id == p.AccountId)).ToList().ForEach(c => c.SetStreamContent(string.Join("|", Enumerations.Action.Plain, JsonConvert.SerializeObject(Message))));
                Server.Database.Punishments.RemoveAll(p => p.IsExceeded);
                Storage.Database.Helper.Save();
            }
        }

        public static void Dispose(string id) {
            Server.Database.Punishments.RemoveAll(p => p.AccountId == id);
            Storage.Database.Helper.Save();
        }
    }
}