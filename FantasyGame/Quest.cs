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

        public List<Collectable> getReward()
        {
            if (QuestCompleted)
                return reward;
            return new List<Collectable>();
        }

        public void CheckTask()
        {
            task.checkCompletion();
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
        public enum questTypes { instawin = 0, collecting = 1, killingMobs = 2, venturing = 3 }
        questTypes task;
        List<Collectable> toCollect;
        List<Enemy> toKill;
        List<bool> toFind;
        bool taskCompleted;
        int killCount;      //Combat not implemented

        public TaskToComplete(questTypes taskType, List<Collectable> toCollect, List<Enemy> toKill, List<bool> toFind)
        {
            taskCompleted = false;
            switch (task)
            {
                case questTypes.instawin:
                    taskCompleted = true;
                    break;
                case questTypes.collecting:
                    this.toCollect = toCollect;
                    break;
                case questTypes.killingMobs:
                    this.toKill = toKill;
                    break;
                case questTypes.venturing:
                    this.toFind = toFind;
                    break;
            }
        }

        public bool GetCompleted()
        {
            return taskCompleted;
        }

        public void checkCompletion()
        {

        }
    }
}
