using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace FantasyGame
{
    class Quest
    {
        private string name;
        private int id;
        private string descritpion;
        private TaskToComplete task;
        private List<Collectable> reward;
        private bool QuestCompleted;

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public List<Collectable> getReward()
        {
            if (QuestCompleted)
                return reward;
            return new List<Collectable>();
        }

        public void CheckTask(List<Collectable> items, List<bool> foundLocations)
        {
            task.checkCompletion(items, foundLocations);
            QuestCompleted = task.GetCompleted();
        }

        public Quest(TaskToComplete task, List<Collectable> reward, string name, string descritpion, int id)
        {
            this.name = name;
            this.id = id;
            this.descritpion = descritpion;
            this.task = task;
            this.reward = reward;
        }
    }

    class TaskToComplete
    {
        Collectable toCollect;
        int amountToCollect;
        List<Enemy> toKill;
        List<bool> toFind;
        bool taskCompleted;
        int killCount;      //Combat not implemented

        public TaskToComplete(Collectable toCollect, int amountToCollect, List<bool> toFind)
        {
            taskCompleted = false;
            this.amountToCollect = amountToCollect;
            this.toCollect = toCollect;
            this.toFind = toFind;
        }

        public bool GetCompleted()
        {
            return taskCompleted;
        }

        public void checkCompletion(List<Collectable> items, List<bool> foundLocations)
        {
            taskCompleted = true;
            int GotItems = 0;
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].name == toCollect.name)
                    GotItems++;
            }
            if (GotItems < amountToCollect)
                taskCompleted = false;

        }
    }
}

