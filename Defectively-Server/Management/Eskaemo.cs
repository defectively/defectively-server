﻿using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Defectively.Compatibility;
using DefectivelyServer.Internal;

namespace DefectivelyServer.Management
{
    public class Eskaemo
    {
        private static readonly string EskaemoPath = Path.Combine(Application.StartupPath, "Eskaemo");
        private static readonly string TracesPath = Path.Combine(EskaemoPath, "Traces");
        private static DateTime SessionStart;

        public static void Trace(string content, string prefix) {
            if (Server.Config.TracingEnabled) {
                var TraceFile = DateTime.Today.ToShortDateString().Replace(".", "-") + ".eskm";
                if (!File.Exists(Path.Combine(TracesPath, TraceFile))) {
                    File.Create(Path.Combine(TracesPath, TraceFile));
                }
                var Trace = File.ReadAllLines(Path.Combine(TracesPath, TraceFile)).ToList();

                Trace.Add($"{DateTime.Now.ToLongTimeString()} [{prefix}] >> {content}");

                File.WriteAllLines(Path.Combine(TracesPath, TraceFile), Trace);
            }
        }

        public static void TraceIndented(string content) {
            if (Server.Config.TracingEnabled) {
                var TraceFile = DateTime.Today.ToShortDateString().Replace(".", "-") + ".eskm";
                if (!File.Exists(Path.Combine(TracesPath, TraceFile))) {
                    File.Create(Path.Combine(TracesPath, TraceFile));
                }
                var Trace = File.ReadAllLines(Path.Combine(TracesPath, TraceFile)).ToList();
                
                Trace.Add($"                   {content}");

                File.WriteAllLines(Path.Combine(TracesPath, TraceFile), Trace);
            }
        }

        public static void BeginSession() {
            if (Server.Config.TracingEnabled) {
                SessionStart = DateTime.Now;

                var TraceFile = DateTime.Today.ToShortDateString().Replace(".", "-") + ".eskm";
                if (!File.Exists(Path.Combine(TracesPath, TraceFile))) {
                    File.WriteAllText(Path.Combine(TracesPath, TraceFile), "");
                }

                var Trace = File.ReadAllLines(Path.Combine(TracesPath, TraceFile)).ToList();

                Trace.Add("=========== Session Begin ===========");
                Trace.Add($"Defectively Server Version {VersionHelper.GetFullStringFromAssembly(Assembly.GetExecutingAssembly())}");
                Trace.Add($"based on Defectively Version {VersionHelper.GetFullStringFromCore()}");
                Trace.Add($"Start: {SessionStart.ToLongTimeString()}");
                Trace.Add("=====================================");
                Trace.Add("");

                File.WriteAllLines(Path.Combine(TracesPath, TraceFile), Trace);
            }
        }

        public static void EndSession() {
            if (Server.Config.TracingEnabled) {
                var SessionEnd = DateTime.Now;

                var TraceFile = DateTime.Today.ToShortDateString().Replace(".", "-") + ".eskm";
                if (!File.Exists(Path.Combine(TracesPath, TraceFile))) {
                    File.Create(Path.Combine(TracesPath, TraceFile));
                }
                var Trace = File.ReadAllLines(Path.Combine(TracesPath, TraceFile)).ToList();

                Trace.Add("");
                Trace.Add("============ Session End ============");
                Trace.Add($"Duration: {SessionEnd.Subtract(SessionStart)}");
                Trace.Add("=====================================");
                Trace.Add("");

                File.WriteAllLines(Path.Combine(TracesPath, TraceFile), Trace);
            }
        }
    }
}
