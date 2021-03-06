﻿using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace DefectivelyServer.Storage.Configuration
{
    public class Helper
    {
        public static Values GetConfig() {
            try {
                return JsonConvert.DeserializeObject<Values>(File.ReadAllText(Application.StartupPath + "\\config.json"));
            } catch {
                throw new IOException();
            }
        }

        public static bool Exists() {
            return File.Exists(Application.StartupPath + "\\config.json");
        }

        public static void CreateDefault() {
            Values Config = new Values {
                ConsoleRequiresAuthentification = true,
                ConsoleAuthentificationTimeout = true,
                CATimeoutTime = 60,
                ServerPort = "42000",
                ServerLanguage = "de-de",
                ServerBroadcastColor = "#1E90FF",
                ServerShutdownMessage = "Shutdown",
                MetaServerName = "Defectively Server",
                MetaOwnerId = "server",
                MetaWebsiteUrl = "https://festival.ml/",
                MetaRequiresAuthentification = true,
                MetaAcceptsGuests = false,
                MetaAcceptsRegistration = false,
                MetaRequiresInvitation = false,
                MetaAccountsInstantlyActivated = false
            };
            File.WriteAllText(Application.StartupPath + "\\config.json", JsonConvert.SerializeObject(Config, Formatting.Indented));
        }
    }
}
