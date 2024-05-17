using Tools;
using UnityEngine;
using UnityEngine.UI;

namespace Global.Components.Localization
{
    [Assert]
    [RequireComponent(typeof(Text))]
    public class TextLocalizer : BaseMonoLocalizable
    {
#pragma warning disable
        [SerializeField] private Text text;
#pragma warning restore

        private void OnValidate()
        {
            text = GetComponent<Text>();
        }

        protected override void LanguageChangeAction(string newValue)
        {
            text.text = newValue;
        }
    }
}
