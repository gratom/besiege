using System;
using Global.Components.UserInterface;
using UnityEngine;

namespace Global.Managers.Datas
{
    [Serializable]
    public abstract class BaseStageData
    {
        [SerializeField] protected int version;

        public abstract GameData.GameStage Stage { get; }
        public int Version => version;
    }
}
