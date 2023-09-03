using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueQuests
{
    [CreateAssetMenu(fileName ="Quest", menuName = "DialogueQuests/Quest", order= 0)]
    public class QuestData : ScriptableObject {

        [Tooltip("Important: make sure all quests have a unique ID")]
        public string quest_id;

        public string title;
        public Sprite icon;
        [TextArea(3, 5)]
        public string desc;
        public int sort_order;

        public void Begin(){ NarrativeData.Get().StartQuest(quest_id);}
        public void Complete(){ NarrativeData.Get().CompleteQuest(quest_id);}
        public void Fail(){ NarrativeData.Get().FailQuest(quest_id); }
        public void Cancel(){ NarrativeData.Get().CancelQuest(quest_id); }

        public void AddQuestProgress(string progress, int value){ NarrativeData.Get().AddQuestProgress(quest_id, progress, value); }
        public void SetQuestProgress(string progress, int value){ NarrativeData.Get().SetQuestProgress(quest_id, progress, value); }

        public bool IsStarted() { return NarrativeData.Get().IsQuestStarted(quest_id); }
        public bool IsActive() { return NarrativeData.Get().IsQuestActive(quest_id); }
        public bool IsCompleted() { return NarrativeData.Get().IsQuestCompleted(quest_id); }
        public bool IsFailed() { return NarrativeData.Get().IsQuestFailed(quest_id); }
        public int GetQuestStatus(){ return NarrativeData.Get().GetQuestStatus(quest_id);}
        public int GetQuestProgress(string progress) { return NarrativeData.Get().GetQuestProgress(quest_id, progress); }

        private static List<QuestData> quest_list = new List<QuestData>();

        public string GetTitle()
        {
            string txt = NarrativeTool.Translate(title);
            return NarrativeTool.ReplaceCodes(txt);
        }

        public string GetDesc()
        {
            string txt = NarrativeTool.Translate(desc);
            return NarrativeTool.ReplaceCodes(txt);
        }

        public static void Load(QuestData quest)
        {
            if (!quest_list.Contains(quest))
            {
                quest_list.Add(quest);

                if (quest is QuestAutoData)
                    ((QuestAutoData)quest).OnLoad();
            }
        }

        public static QuestData Get(string quest_id)
        {
            foreach (QuestData quest in GetAll())
            {
                if (quest.quest_id == quest_id)
                    return quest;
            }
            return null;
        }

        public static List<QuestData> GetAllActive()
        {
            List<QuestData> valid_list = new List<QuestData>();
            foreach (QuestData aquest in GetAll())
            {
                if (aquest.IsActive())
                    valid_list.Add(aquest);
            }
            return valid_list;
        }

        public static List<QuestData> GetAllStarted()
        {
            List<QuestData> valid_list = new List<QuestData>();
            foreach (QuestData aquest in GetAll())
            {
                if (aquest.IsStarted())
                    valid_list.Add(aquest);
            }
            return valid_list;
        }

        public static List<QuestData> GetAllActiveOrCompleted()
        {
            List<QuestData> valid_list = new List<QuestData>();
            foreach (QuestData aquest in GetAll())
            {
                if (aquest.IsActive() || aquest.IsCompleted())
                    valid_list.Add(aquest);
            }
            return valid_list;
        }

        public static List<QuestData> GetAllActiveOrFailed()
        {
            List<QuestData> valid_list = new List<QuestData>();
            foreach (QuestData aquest in GetAll())
            {
                if (aquest.IsActive() || aquest.IsFailed())
                    valid_list.Add(aquest);
            }
            return valid_list;
        }

        public static List<QuestData> GetAll()
        {
            return quest_list;
        }
    }
}
