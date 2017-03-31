﻿using System;
using System.Collections.Generic;
using Defectively;
using Defectively.Extension;
using DefectivelyServer.Internal;

namespace DefectivelyServer.Management
{
    public class ListenerManager
    {
        public static List<Listener> Listeners = new List<Listener>();

        public static void RegisterListener(Listener listener) {
            Listeners.Add(listener);
        }

        public static void InvokeEvent(Event e, params object[] args) {
            try {
                Listeners.FindAll(l => l.Event == e).ForEach(l => l.Delegate.DynamicInvoke(args));
            } catch (Exception Ex) {
                ExtensionPool.Server.PrintToConsole($"Exception:  {Ex.InnerException?.Message}\nStacktrace: {Ex.InnerException?.StackTrace}\n", Server.Error);
            }
        }

        public static void InvokeSpecialEvent(DynamicEvent e) {
            try {
                Listeners.FindAll(l => l.Event == Event.Dynamic).ForEach(l => l.Delegate.DynamicInvoke(e));
            } catch (Exception Ex) {
                ExtensionPool.Server.PrintToConsole($"Exception:  {Ex.InnerException?.Message}\nStacktrace: {Ex.InnerException?.StackTrace}\n", Server.Error);
            }
        }
    }
}
