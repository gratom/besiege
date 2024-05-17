using System.Collections.Generic;
using UnityEngine;

namespace Tools.Components.Universal.Testing
{
    public class TestSetterContent : MonoBehaviour
    {
#pragma warning disable
        [SerializeField] private ScrollableComponent scrollableComponent;
#pragma warning restore

        private void Start()
        {
            List<IScrollableContainerContent> testList = new List<IScrollableContainerContent>();
            for (int i = 0; i < 10000; i++)
            {
                testList.Add(new PotionButtonScrollableContainerContent() { z1 = Random.Range(0, 3), z2 = Random.Range(0, 3), z3 = Random.Range(0, 3) });
            }

            scrollableComponent.SetContent(testList);
        }
    }
}
