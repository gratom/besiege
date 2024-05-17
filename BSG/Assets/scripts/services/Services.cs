using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Global
{
    using Managers;

    public static class Services
    {
        private static bool isInit = false;

        private static Dictionary<Type, BaseManager> managersDictionary;

        public static async Task InitAppWith(List<BaseManager> managers)
        {
            if (!isInit)
            {
                managersDictionary = new Dictionary<Type, BaseManager>();
                foreach (BaseManager manager in managers)
                {
                    managersDictionary.Add(manager.ManagerType, manager);
                }

                List<Task> initialization = new List<Task>(managers.Count);
                foreach (BaseManager manager in managers)
                {
                    initialization.Add(manager.Init());
                }

                await Task.WhenAll(initialization);

                isInit = true;
            }
#if UNITY_EDITOR
            else
            {
                Debug.LogError("Services already initiated. Your list of managers is not added to services.");
            }
#endif
        }

        public static T GetManager<T>() where T : BaseManager
        {
            return (T)managersDictionary[typeof(T)];
        }
    }
}
