using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Global.Managers.Datas
{
    [Serializable]
    public class MainData : BaseStageData
    {
        public override GameData.GameStage Stage => GameData.GameStage.Main;

        public void PostInit()
        {

        }
    }
}
