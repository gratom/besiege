﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Global.Managers.Game
{
    using Datas;

    public class GameManager : BaseManager
    {
        public override Type ManagerType => typeof(GameManager);

        public GameData CurrentGame { get; private set; }

#pragma warning disable

        protected override async Task<bool> OnInit()
        {
            return true;
        }

#pragma warning restore

        #region public functions

        public void StartGame(GameData data)
        {
            CurrentGame = data;
            GameSetter.SetGameFrom(CurrentGame);
        }

        public void ExitGame()
        {
            Services.GetManager<DataManager>().DynamicData.GameData = CurrentGame;
        }

        public void GotoStage(GameData.GameStage stage)
        {
            CurrentGame.currentStageData = stage;
            GameSetter.SetGameFrom(CurrentGame);
        }

        #endregion
    }
}
