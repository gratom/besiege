using System;
using UnityEngine.UI;

namespace Tools
{
    public class SelectableExpansion : Selectable
    {
        public enum SelectableState
        {
            Normal,

            /// <summary>
            /// The UI object is highlighted.
            /// </summary>
            Highlighted,

            /// <summary>
            /// The UI object is pressed.
            /// </summary>
            Pressed,

            /// <summary>
            /// The UI object is selected
            /// </summary>
            Selected,

            /// <summary>
            /// The UI object cannot be selected.
            /// </summary>
            Disabled
        }

        public event Action<SelectableState> ONStateChange;

        protected override void DoStateTransition(SelectionState state, bool instant)
        {
            base.DoStateTransition(state, instant);
            ONStateChange?.Invoke((SelectableState)state);
        }
    }
}
