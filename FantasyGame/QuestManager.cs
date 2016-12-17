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
        public static List<Quest> currentQuests;

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

        public static Quest SearchForQuest(string name)
        {
            for (int i = 0; i < currentQuests.Count; i++)
            {
                if (currentQuests[i].name == name)
                {
                    return currentQuests[i];
                }
            }
            throw new ArgumentOutOfRangeException("Quest doesn't exist");
        }

        public static void DrawQuests(RenderWindow window, View view, Inventory inventory)
        {
            for (int i = 0; i < currentQuests.Count; i++)
                currentQuests[i].Draw(window, view, inventory,i);
        }

        public static bool QuestInList(string name)
        {
            for (int i = 0; i < currentQuests.Count; i++)
                if (currentQuests[i].name == name)
                    return true;
            return false;
        }
    }
}
