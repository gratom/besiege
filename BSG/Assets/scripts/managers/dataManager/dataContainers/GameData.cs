﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.Managers.Datas
{
    [Serializable]
    public class GameData
    {
        public bool isInit = false;

        public MainData mainData = new MainData();

        public enum GameStage
        {
            Main
        }

        public GameStage currentStageData = GameStage.Main;

        public void PostInitData()
        {
            mainData.PostInit();
        }

        public List<BaseStageData> GetAllDataAsList()
        {
            return new List<BaseStageData>()
            {
                mainData
            };
        }

        public void SetData(BaseStageData defaultData)
        {
            switch (defaultData.Stage)
            {
                case GameStage.Main:
                    mainData = (MainData)defaultData;
                    break;
            }
        }
    }

}
