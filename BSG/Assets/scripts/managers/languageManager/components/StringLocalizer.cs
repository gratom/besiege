using System;
using UnityEngine;

namespace Global.Components.Localization
{
    [Serializable]
    public class StringLocalizer : BaseLocalizable
    {
        public string Text { get; private set; }

        protected override void LanguageChangeAction(string newValue)
        {
            Text = newValue;
        }

        public static implicit operator string(StringLocalizer s) => s.Text;
    }
}
