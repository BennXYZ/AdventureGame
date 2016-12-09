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
            {
                return reward;
            }
            return new List<Collectable>(0);
        }

        public List<Type> giveCost()
        {
            List<Type> cost = new List<Type>();
            if(QuestCompleted)
            {
                for (int i = 0; i < task.amountToCollect; i++)
                    cost.Add(task.toCollect);
                return cost;
            }
            return new List<Type>();
        }

        public bool CheckTask(Inventory inventory)
        {
            task.checkCompletion(inventory);
            QuestCompleted = task.GetCompleted();
            return QuestCompleted;
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
        public Type toCollect;
        public int amountToCollect;
        List<Enemy> toKill;
        List<bool> toFind;
        bool taskCompleted;
        int killCount;      //Combat not implemented

        public TaskToComplete(Type toCollect, int amountToCollect, List<bool> toFind)
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

        public bool checkCompletion(Inventory inventory)
        {
            taskCompleted = true;
            if (inventory.AmountOf(toCollect) < amountToCollect)
                taskCompleted = false;
            return taskCompleted;
        }
    }
}

