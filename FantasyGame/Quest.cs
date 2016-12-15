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
        public string name;
        private int id;
        private string descritpion;
        private TaskToComplete task;
        private List<Collectable> reward;
        private bool QuestCompleted;

        /// <summary>
        /// used to get the items once Task completed
        /// </summary>
        /// <returns>Returns a List of collectables to collect</returns>
        public List<Collectable> getReward()
        {
            if (QuestCompleted)
            {
                return reward;
            }
            return new List<Collectable>(0);
        }

        /// <summary>
        /// Used to determine, how many of which items have to be removed from the Inventory
        /// </summary>
        /// <returns>Returns a List of Collectable-Types</returns>
        public List<Type> giveCost()
        {
            List<Type> cost = new List<Type>();
            if (QuestCompleted)
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

        /// <summary>
        /// Creates a Quest That checks if a specific amount of items have been collected 
        /// </summary>
        /// <param name="task">the task that has to be completed in order to finish the Quest</param>
        /// <param name="reward">List of Collectables the player gets once completed</param>
        /// <param name="name">Name of the Quest</param>
        /// <param name="descritpion">Used to tell the Player what to do</param>
        public Quest(TaskToComplete task, List<Collectable> reward, string name, string descritpion, int id)
        {
            this.name = name;
            this.id = id;
            this.descritpion = descritpion;
            this.task = task;
            this.reward = reward;
        }

        public void Draw(RenderWindow window, View view, Inventory inventory, int questNumber)
        {
            for (int i = 0; i < ContentManager.spriteMaps.Count; i++)
            {
                if (ContentManager.spriteMaps[i].name == "questBG")
                {
                    ContentManager.spriteMaps[i].Sprites[1].Position = new Vector2f(view.Center.X + view.Size.X / 2 - 399,view.Center.Y - view.Size.Y / 2 + questNumber * 202);
                    window.Draw(ContentManager.spriteMaps[i].Sprites[1]);

                    break;
                }
            }
            Text aName = new Text(name, ContentManager.arial);
            aName.Position = new Vector2f(view.Center.X + view.Size.X / 2 - 399 + 20, view.Center.Y - view.Size.Y / 2 + questNumber * 202 + 10);
            Text aText = new Text(descritpion, ContentManager.arial);
            aText.Position = new Vector2f(view.Center.X + view.Size.X / 2 - 399 + 20, view.Center.Y - view.Size.Y / 2 + questNumber * 202 + 60);
            Text condition = new Text(inventory.AmountOf(task.toCollect).ToString() + " / " + task.amountToCollect.ToString(), ContentManager.arial);
            condition.Position = new Vector2f(view.Center.X + view.Size.X / 2 - 399 + 20, view.Center.Y - view.Size.Y / 2 + questNumber * 202 + 120);

            window.Draw(condition);
            window.Draw(aName);
            window.Draw(aText);
        }
    }

    /// <summary>
    /// Used by Quest-Class. This Class checks if the player has finished the Quest
    /// </summary>
    class TaskToComplete
    {
        public Type toCollect;
        public int amountToCollect;
        bool taskCompleted;

        /// <summary>
        /// Sets what Task the player has to do
        /// </summary>
        /// <param name="toCollect">Collectable that needs to be collected</param>
        /// <param name="amountToCollect">How many of the wanted Items the player has to collect</param>
        /// <param name="toFind"></param>
        public TaskToComplete(Type toCollect, int amountToCollect)
        {
            taskCompleted = false;
            this.amountToCollect = amountToCollect;
            this.toCollect = toCollect;
        }

        /// <summary>
        /// Used by Quest to check if Quest has been completed
        /// </summary>
        /// <returns>Returns bool, if quest has been completed</returns>
        public bool GetCompleted()
        {
            return taskCompleted;
        }

        /// <summary>
        /// Checks if the Player has the needed Items to finish the Quest
        /// </summary>
        /// <param name="inventory">Takes Inventory to check items within it</param>
        /// <returns>Returns bool weather the Task has been completed</returns>
        public bool checkCompletion(Inventory inventory)
        {
            taskCompleted = true;
            if (inventory.AmountOf(toCollect) < amountToCollect)
                taskCompleted = false;
            return taskCompleted;
        }
    }
}

