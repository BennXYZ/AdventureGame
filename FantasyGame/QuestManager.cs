using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyGame
{
    public static class QuestManager
    {
        static List<Quest> currentQuests;

        static void removeQuest(string name)
        {
            for(int i = 0; i < currentQuests.Count; i++)
            {
                if (currentQuests[i].Name == name)
                {
                    currentQuests.RemoveAt(i);
                    break;
                }
            }
        }
    }
}
