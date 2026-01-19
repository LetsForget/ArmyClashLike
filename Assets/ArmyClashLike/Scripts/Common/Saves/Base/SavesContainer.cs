using System;
using System.Collections.Generic;
using Logging;

namespace Common.Saves
{
    public class SavesContainer<T> where T : Enum
    {
        private readonly ILogsWriter logsWriter;
        
        public Dictionary<T, SaveCategoryData> SaveCategories { get; }

        public SavesContainer(ILogsWriter logsWriter)
        {
            this.logsWriter = logsWriter;
            
            SaveCategories = new Dictionary<T, SaveCategoryData>();
        }

        public void SetCategory(T category, Dictionary<string, string> saves)
        {
            if (SaveCategories.ContainsKey(category))
            {
                logsWriter.LogError($"Category already exists: {category}");
                return;
            }
            
            SaveCategories.Add(category, new SaveCategoryData(saves));
        }
        
        public void SetSave(ISavable<T> save)
        {
            var category = save.SaveCategory;

            if (!SaveCategories.ContainsKey(category))
            {
                SetCategory(category, new Dictionary<string, string>());
            }
            
            var saves = SaveCategories[category].Saves;
            saves[save.SaveKey] = save.Save();
        }

        public string GetSave(ISavable<T> save)
        {
            var category = save.SaveCategory;
            
            if (!SaveCategories.TryGetValue(category, out var saveCategory))
            {
                return null;
            }
            
            saveCategory.Saves.TryGetValue(save.SaveKey, out var saveData);
            return saveData;
        }
        
        public void Remove(ISavable<T> savable)
        {
            var category = savable.SaveCategory;

            if (!SaveCategories.TryGetValue(category, out var saveCategories))
            {
                return;
            }

            saveCategories.Saves.Remove(savable.SaveKey);
        }

        public bool HasSave(ISavable<T> savableKey)
        {
            var category = savableKey.SaveCategory;
            
            return SaveCategories.TryGetValue(category, out var saveCategories) && saveCategories.Saves.ContainsKey(savableKey.SaveKey);
        }
        
        public bool HasSave(string savableKey)
        {
            foreach (var saveCategory in SaveCategories.Values)
            {
                var saves = saveCategory.Saves;
                
                if (saves.ContainsKey(savableKey))
                {
                    return true;
                }
            }
            
            return false;
        }
    }

    public struct SaveCategoryData
    {
        public readonly Dictionary<string, string> Saves;

        public SaveCategoryData(Dictionary<string, string> saves)
        {
            Saves = saves;
        }
    }
}