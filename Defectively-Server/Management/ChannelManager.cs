using System;
using System.Linq;
using System.Windows.Forms;
using Defectively;
using Defectively.Extension;
using DefectivelyServer.Forms;
using DefectivelyServer.Internal;
using DefectivelyServer.Storage.Database;
using Newtonsoft.Json;

namespace DefectivelyServer.Management
{
    public class ChannelManager
    {
        public static void CreateChannel(Channel channel) {
            Server.Channels.Add(channel);
            ListenerManager.InvokeEvent(Event.ChannelCreated,  channel.Id);
            MoveAccountTo(channel.OwnerId, channel.Id);
        }

        public static void AddChannel(Channel channel) {
            Server.Channels.Add(channel);
            if (channel.Persistent) {
                Server.Database.Channels.Add(channel);
                Storage.Database.Helper.Save();
            }
            ListenerManager.InvokeEvent(Event.ChannelCreated, channel.Id);
        }

        public static void CloseChannel(Channel channel) {
            MoveAllFromTo(channel.Id, "defectively");
            Server.Channels.Remove(channel);
        }

        public static void SendChannelList() {
            var AllChannels = JsonConvert.SerializeObject(Server.Channels);
            var NonHiddenChannels = JsonConvert.SerializeObject(Server.Channels.FindAll(c => !c.Hidden));
            Server.Connections.ForEach(c => {
                if (c.Owner.AccountHasLuvaValue("defectively.canSeeHiddenChannels")) {
                    c.SetStreamContent(string.Join("|", Enumerations.Action.SetChannelList, AllChannels));
                } else {
                    c.SetStreamContent(string.Join("|", Enumerations.Action.SetChannelList, NonHiddenChannels));
                }
            });
        }

        public static void MoveAccountTo(Account account, Channel channel) {
            var Connection = Server.Connections.Find(c => c.Owner == account);

            ExtensionPool.Server.SendMessagePacketToChannelExceptTo(account.Id, Connection.Channel.Id, new MessagePacket {
                Content = $"{account.Name} left the channel.",
                Time = DateTime.Now.ToShortTimeString(),
                Type = Enumerations.MessageType.Center
            });

            Connection.Channel.MemberIds.Remove(account.Id);
            channel.MemberIds.Add(account.Id);

            ExtensionPool.Server.SendMessagePacketToChannelExceptTo(account.Id, channel.Id, new MessagePacket {
                Content = $"{account.Name} entered the channel.",
                Time = DateTime.Now.ToShortTimeString(),
                Type = Enumerations.MessageType.Center
            });

            ListenerManager.InvokeEvent(Event.ClientChannelChanged,  Connection.Owner.Id, channel.Id);
            Connection.Channel = channel;
            Connection.SetStreamContent(Enumerations.Action.ClearConversation.ToString());
            Connection.SetStreamContent(string.Join("|", Enumerations.Action.SetChannel, JsonConvert.SerializeObject(channel)));
            Server.Channels.RemoveAll(c => c.MemberIds.Count == 0 && !c.Persistent);
            SendChannelList();
            Application.OpenForms.OfType<MainWindow>().ToList()[0].RefreshAccountList();
        }

        public static void MoveAccountTo(string accountId, string channelId) {
            MoveAccountTo(Server.Database.Accounts.Find(a => a.Id == accountId), Server.Channels.Find(c => c.Id == channelId));
        }

        public static void MoveAllTo(Channel channel) {
            Server.Connections.ForEach(c => MoveAccountTo(c.Owner, channel));
        }

        public static void MoveAllFromTo(Channel channelFrom, Channel channelTo) {
            Server.Connections.FindAll(c => c.Channel == channelFrom).ForEach(c => MoveAccountTo(c.Owner, channelTo));
        }

        public static void MoveAllFromTo(string channelFromId, string channelToId) {
            Server.Connections.FindAll(c => c.Channel.Id == channelFromId).ForEach(c => MoveAccountTo(c.Owner.Id, channelToId));
        }
    }
}
