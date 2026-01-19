using System;
using System.Collections.Generic;
using System.Text;
using Common.JsonConverting;
using Common.Storage;
using Cysharp.Threading.Tasks;
using Logging;
using UnityEngine;

namespace Common.Saves
{
    public class LocalSaveSystem<T> : ISaveSystem<T> where T : Enum
    {
        private readonly IStorageProvider storageProvider;
        private readonly IJsonConverter jsonConverter;
        private readonly ILogsWriter logsWriter;
        
        private readonly SavesContainer<T> savesContainer;
        private readonly StringBuilder stringBuilder;

        private readonly List<ISavable<T>> savables;
        private readonly LocalSaveSettings settings;
        
        private string SavePath => Application.persistentDataPath; 
        
        public LocalSaveSystem(IStorageProvider storageProvider, IJsonConverter jsonConverter, ILogsWriter logsWriter, LocalSaveSettings settings)
        {
            this.storageProvider = storageProvider;
            this.jsonConverter = jsonConverter;
            this.logsWriter = logsWriter;
            this.settings = settings;
            
            savesContainer = new SavesContainer<T>(logsWriter);
            stringBuilder = new StringBuilder(SavePath.Length + 20);
            
            savables = new List<ISavable<T>>(settings.SaveStartCapacity);
        }
        
        public async UniTask LoadSaves()
        {
            foreach (T category in Enum.GetValues(typeof(T)))
            {
                var path = GetCategoryPath(category.ToString());
                var saveRaw = await storageProvider.Load(path);

                if (string.IsNullOrEmpty(saveRaw))
                {
                    continue;
                }
                
                var saves = await jsonConverter.Deserialize<Dictionary<string, string>>(saveRaw);
                savesContainer.SetCategory(category, saves);
            }
            
            logsWriter.Log("Successfully loaded");
        }

        public UniTask ApplySaves()
        {
            foreach (var save in savables)
            {
                var saveData = savesContainer.GetSave(save);

                if (string.IsNullOrEmpty(saveData))
                {
                    logsWriter.LogWarning("Loaded empty save");
                    continue;
                }
                
                save.Load(saveData);
            }
            
            logsWriter.Log("Successfully applied");
            return UniTask.CompletedTask;
        }
        
        public async UniTask Save()
        {
            foreach (var save in savables)
            {
                savesContainer.SetSave(save);
            }
            
            foreach (var saveCategoryData in savesContainer.SaveCategories)
            {
                var path = GetCategoryPath(saveCategoryData.Key.ToString());
                var saveRaw = await jsonConverter.Serialize(saveCategoryData.Value.Saves);
                
                await storageProvider.Write(saveRaw, path);
            }
            
            logsWriter.Log("Successfully saved");
        }

        public void Add(ISavable<T> savable)
        {
            if (!savables.Contains(savable))
            {
                savables.Add(savable);
            }

            savesContainer.SetSave(savable);
        }

        public void Remove(ISavable<T> savable)
        {
            savables.Remove(savable);
            savesContainer.Remove(savable);
        }

        public bool HasSaved(ISavable<T> savable)
        {
            return savesContainer.HasSave(savable);
        }

        public bool HasSaved(string savableKey)
        {
            return savesContainer.HasSave(savableKey);
        }

        private string GetCategoryPath(string category)
        {
            stringBuilder.Append(SavePath);
            stringBuilder.Append("\\");
            stringBuilder.Append(category);
            stringBuilder.Append(".gSave");
            
            var result = stringBuilder.ToString();
            stringBuilder.Clear();
            
            return result;
        }
    }
}