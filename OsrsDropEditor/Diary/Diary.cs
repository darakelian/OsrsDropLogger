using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OsrsDropEditor.Diary
{
    /// <summary>
    /// Used to store data about an achievement diary
    /// </summary>
    class Diary
    {
        public string Name { get; }
        public List<DiaryTask> Tasks { get; }

        public Diary(string name)
        {
            Name = name;
            Tasks = new List<DiaryTask>();
            LoadTasks();
        }

        private void LoadTasks()
        {
            string json = Utility.ReadFileToEnd(Name + ".json", filePath: ".", offlineJson: false);
            List<DiaryTask> loadedTasks = JsonConvert.DeserializeObject<List<DiaryTask>>(json);
        }
    }
}
