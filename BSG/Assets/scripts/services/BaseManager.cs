using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Global.Managers
{
    public abstract class BaseManager : MonoBehaviour
    {
        public abstract Type ManagerType { get; }

        public bool isInit { get; private set; } = false;

        public async Task Init()
        {
            if (!isInit)
            {
                isInit = await OnInit();
                if (!isInit)
                {
                    throw new Exception($"manager type of {ManagerType} is not initialized correctly!");
                }
            }
        }

        protected abstract Task<bool> OnInit();

        protected virtual void OnValidate()
        {
            gameObject.name = GetType().Name;
        }

    }
}
