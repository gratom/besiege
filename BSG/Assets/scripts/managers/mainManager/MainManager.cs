using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Global.Managers
{
    using Datas;
    using Game;

    public class MainManager : BaseManager
    {
        public override Type ManagerType => typeof(MainManager);

#pragma warning disable

        protected override async Task<bool> OnInit()
        {
            return true;
        }

#pragma warning restore

        public void EntryPoint()
        {
            Services.GetManager<DataManager>().StaticData.Balance.Init();
            ContinueGame();
        }

        public void StartNewGame()
        {
            //ContinueGame();
        }

        public void ContinueGame()
        {
            //creating new data (full, from zero)
            if (!Services.GetManager<DataManager>().DynamicData.GameData.isInit)
            {
                Debug.Log("data is null. create new");
                Services.GetManager<DataManager>().DynamicData.GameData = Services.GetManager<DataManager>().StaticData.DefaultGameData.GetCopyOfData();
            }

            CheckUpdate();

            Services.GetManager<DataManager>().DynamicData.GameData.PostInitData();
            Services.GetManager<GameManager>().StartGame(Services.GetManager<DataManager>().DynamicData.GameData);
        }

        private void CheckUpdate()
        {
            List<BaseStageData> datasListCurrent = Services.GetManager<DataManager>().DynamicData.GameData.GetAllDataAsList();
            List<BaseStageData> datasListDefault = Services.GetManager<DataManager>().StaticData.DefaultGameData.GetCopyOfData().GetAllDataAsList();

            for (int i = 0; i < datasListCurrent.Count; i++)
            {
                BaseStageData stageData = datasListCurrent[i];
                BaseStageData defaultData = datasListDefault.FirstOrDefault(x => x.Stage == stageData.Stage);
                if (defaultData != null)
                {
                    if (defaultData.Version > stageData.Version)
                    {
                        Services.GetManager<DataManager>().DynamicData.GameData.SetData(defaultData);
                    }
                }
            }
        }
    }
}
