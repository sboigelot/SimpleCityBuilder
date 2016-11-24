using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Localization
{
    public class Localizer : Singleton<Localizer>
    {
        private Dictionary<string, Dictionary<string, string>> locals;

        private string currentCulture;

        public Localizer()
        {
            localizableInterfaces = new List<ILocalizableInterface>();
            Load(Path.Combine(Application.streamingAssetsPath, "Local"));
        }

        public string CurrentCulture
        {
            get
            {
                return currentCulture;
            }

            set
            {
                if (currentCulture != value)
                {
                    currentCulture = value;
                    UpdateRegisteredInterfaces();
                }
            }
        }

        private readonly List<ILocalizableInterface> localizableInterfaces;

        public void RegisterInterface(ILocalizableInterface screen)
        {
            if (!localizableInterfaces.Contains(screen))
            {
                localizableInterfaces.Add(screen);
            }

            screen.OnLocalChanged();
        }

        private void UpdateRegisteredInterfaces()
        {
            foreach (var localizableInterface in localizableInterfaces)
            {
                localizableInterface.OnLocalChanged();
            }
        }

        public static string Get(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return string.Empty;
            }

            if (!Instance.locals.ContainsKey(Instance.CurrentCulture) ||
                !Instance.locals[Instance.CurrentCulture].ContainsKey(key))
            {
                Instance.AddLocalization(key);
                return string.Format("[{0}][{1}]", Instance.CurrentCulture, key);
            }

            return Instance.locals[Instance.CurrentCulture][key];
        }

        private void AddLocalization(string key)
        {
            string folder = Path.Combine(Application.streamingAssetsPath, "Local");
            string filePath = Path.Combine(folder, CurrentCulture + ".local");
            using (var file = File.Open(filePath, FileMode.Create, FileAccess.Write))
            {
                using (var sw = new StreamWriter(file))
                {
                    locals[CurrentCulture].Add(key, key);
                    foreach (var k in locals[CurrentCulture].Keys.OrderBy(k => k))
                    {
                        sw.WriteLine("{0}={1}", k, locals[CurrentCulture][k]);
                    }
                }
            }
        }

        public void EnsureAllLocalKeyExist()
        {
            var keys = new List<string>();
           
            // TODO priority:medium implement a way to loop the prototype and fetch all keys
            keys = keys.Distinct().ToList();
            foreach (var key in keys.ToList())
            {
                if (locals[CurrentCulture].ContainsKey(key))
                {
                    keys.Remove(key);
                }
            }

            foreach (var key in keys)
            {
                locals[CurrentCulture].Add(key, key);
            }

            string folder = Path.Combine(Application.streamingAssetsPath, "Local");
            string filePath = Path.Combine(folder, CurrentCulture + ".local");
            using (var file = File.Open(filePath, FileMode.Create, FileAccess.Write))
            {
                using (var sw = new StreamWriter(file))
                {
                    foreach (var k in locals[CurrentCulture].Keys.OrderBy(k => k))
                    {
                        sw.WriteLine("{0}={1}", k, locals[CurrentCulture][k]);
                    }
                }
            }
        }

        public void Load(string folderPath)
        {
            locals = new Dictionary<string, Dictionary<string, string>>();
            CurrentCulture = "en_US";

            foreach (var file in Directory.GetFiles(folderPath, "*.local"))
            {
                LoadFile(file);
            }
        }

        private void LoadFile(string file)
        {
            string culture = new FileInfo(file).Name.Replace(".local", string.Empty);

            if (locals.ContainsKey(culture))
            {
                locals.Remove(culture);
            }

            locals.Add(culture, new Dictionary<string, string>());
            var local = locals[culture];

            var rows = File.ReadAllLines(file);
            foreach (var row in rows)
            {
                var args = row.Split(new[] { '=' }, 2);
                local.Add(args[0], args[1]);
            }
        }
    }
}