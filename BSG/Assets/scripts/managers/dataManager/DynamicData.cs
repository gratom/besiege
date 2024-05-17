using System;
using UnityEngine;

namespace Global.Managers.Datas
{
    /// <summary>
    /// Class for saving application states.
    /// The state is, for example, the name of the character,
    /// the amount of money the player has,
    /// the saving of the game, the settings,
    /// all the data that can change during the game and that need to be saved.
    /// </summary>
    [Serializable]
    public class DynamicData
    {
        [SerializeField, Storage(Storage.StoragePathType.applicationFolder, "saves")]
        private GameData gameData;

        [SerializeField, Storage(Storage.StoragePathType.playerPrefs, "Settings")]
        private SettingsData settings = new SettingsData();

        public GameData GameData
        {
            get => gameData;
            set => gameData = value;
        }

        public SettingsData Settings => settings;
    }
}
