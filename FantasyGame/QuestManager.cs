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
    static class QuestManager
    {
        public static List<Quest> currentQuests;  //List of currently Active Quests

        public static void removeQuest(string name)
        {
            for(int i = 0; i < currentQuests.Count; i++)
            {
                if (currentQuests[i].name == name)
                {
                    currentQuests.RemoveAt(i);
                    break;
                }
            }
        }

        public static void CheckQuest(string name, Inventory inventory)
        {
            if (SearchForQuest(name).CheckTask(inventory))
            {
                inventory.Add(SearchForQuest(name).getReward()[0],SearchForQuest(name).getReward().Count);
                inventory.Remove(SearchForQuest(name).giveCost()[0], SearchForQuest(name).giveCost().Count);
                removeQuest(name);
            }
        }

        /// <summary>
        /// Used to get a Quest with a specific name
        /// </summary>
        /// <param name="name">Name of the quest you are looking for</param>
        /// <returns>Returns a Quest</returns>
        public static Quest SearchForQuest(string name)
        {
            for (int i = 0; i < currentQuests.Count; i++)
            {
                if (currentQuests[i].name == name)
                {
                    return currentQuests[i];
                }
            }
            throw new ArgumentOutOfRangeException("Quest doesn't exist");  //No Problems by now
        }

        /// <summary>
        /// Draws the Quest in the upper right corner
        /// </summary>
        /// <param name="window">necesarry to Draw something</param>
        /// <param name="view">necessary to pin the Quest-Window to the upper right corner of the View</param>
        /// <param name="inventory">necessary to draw the progress of the Quest</param>
        public static void DrawQuests(RenderWindow window, View view, Inventory inventory)
        {
            for (int i = 0; i < currentQuests.Count; i++)
                currentQuests[i].Draw(window, view, inventory,i);
        }

        /// <summary>
        /// Checks if a Quest exists within the List of Active Quests
        /// </summary>
        /// <param name="name">name of the Quest you are checking</param>
        /// <returns></returns>
        public static bool QuestInList(string name)
        {
            for (int i = 0; i < currentQuests.Count; i++)
                if (currentQuests[i].name == name)
                    return true;
            return false;
        }
    }
}
