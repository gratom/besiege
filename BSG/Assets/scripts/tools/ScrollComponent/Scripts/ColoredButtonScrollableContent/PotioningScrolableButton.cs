using UnityEngine;
using UnityEngine.UI;

namespace Tools.Components.Universal
{
    [Assert]
    [RequireComponent(typeof(Button))]
    public class PotioningScrolableButton : ScrollableButton
    {
        public Image image1;
        public Image image2;
        public Image image3;

        #region public functions

        public override void Init(IScrollableContainerContent content)
        {
            image1.sprite = PotionsImages.Instance.spritesDictionary[((PotionButtonScrollableContainerContent)content).z1];
            image2.sprite = PotionsImages.Instance.spritesDictionary[((PotionButtonScrollableContainerContent)content).z2];
            image3.sprite = PotionsImages.Instance.spritesDictionary[((PotionButtonScrollableContainerContent)content).z3];
        }

        #endregion public functions
    }
}
