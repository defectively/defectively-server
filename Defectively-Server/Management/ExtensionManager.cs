using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using Defectively.Command;
using Defectively.Extension;
using Newtonsoft.Json;

namespace DefectivelyServer.Management
{
    public class ExtensionManager
    {
        public static List<IExtension> Extensions = new List<IExtension>();

        public static void LoadExtension(string path) {
            if (File.Exists(path) && new FileInfo(path).Extension == ".dll") {
                Assembly.LoadFile(path);
                var ExtensionType = typeof(IExtension);
                var AssemblyTypes = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(assembly => assembly.GetTypes())
                    .Where(type => ExtensionType.IsAssignableFrom(type) && type.IsClass)
                    .ToArray();
                foreach (var Type in AssemblyTypes) {
                    var Extension = (IExtension) Activator.CreateInstance(Type);
                    Extension.Path = path;
                    if (Extensions.Any(e => e.Namespace == Extension.Namespace)) {
                        continue;
                    }

                    // DEMO

                    try {
                        var Assembly = System.Reflection.Assembly.GetAssembly(Type);
                        using (var Stream = Assembly.GetManifestResourceStream($"{Assembly.GetName().Name}.meta.json")) {
                            using (var Reader = new StreamReader(Stream)) {
                                var Content = Reader.ReadToEnd();
                                var ExtensionMeta = JsonConvert.DeserializeObject<JsonExtension>(Content);
                                ExtensionMeta.Commands.ForEach(j => Extension.Commands.Add(j.Name, CommandFactory.CreateCommandFromJson(j)));
                            }
                        }
                    } catch {
                        Debug.Print($"{Extension.Namespace} didn't provide a valid meta.json!");
                    }

                    // DEMO END

                    Extensions.Add(Extension);
                }
            }
        }
    }
}
