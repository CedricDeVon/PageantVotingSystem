
using System;
using System.Windows.Forms;
using System.Collections.Generic;

using PageantVotingSystem.Sources.FormStyles;

namespace PageantVotingSystem.Sources.FeatureCollections
{
    public class AllButtonItemFeatureCollection : ItemFeatureCollection
    {
        private readonly List<Button> itemButtons;

        public AllButtonItemFeatureCollection(
            object formControl,
            Panel parentControl,
            Panel itemControl,
            List<Button> buttons = null) :
            base(formControl,
                parentControl,
                itemControl)
        {
            itemButtons = buttons ?? new List<Button>();
        }

        public void ConnectButtonsToAllEvents(List<Button> buttons)
        {
            ThrowIfListButtonIsNull(buttons);

            foreach (Button button in buttons)
            {
                ConnectButtonToItemClick(button);
                ConnectButtonToMouseEnter(button);
                ConnectButtonToMouseLeave(button);
            }
        }

        public void ConnectButtonsToMouseEnter(List<Button> buttons)
        {
            ThrowIfListButtonIsNull(buttons);

            foreach (Button button in buttons)
            {
                ConnectButtonToMouseEnter(button);
            }
        }

        public void ConnectButtonToMouseEnter(Button button)
        {
            ThrowIfButtonIsNull(button);

            button.MouseEnter += new EventHandler(Button_MouseEnter);
        }

        public void ConnectButtonsToMouseLeave(List<Button> buttons)
        {
            ThrowIfListButtonIsNull(buttons);

            foreach (Button button in buttons)
            {
                ConnectButtonToMouseLeave(button);
            }
        }

        public void ConnectButtonToMouseLeave(Button button)
        {
            button.MouseLeave += new EventHandler(Button_MouseLeave);
        }

        public void ConnectButtonsToKeyPress(KeyPressEventHandler handler)
        {
            foreach (Button itemButton in itemButtons)
            {
                itemButton.KeyPress += handler;
            }
        }

        public void DisconnectButtonsToKeyPress(KeyPressEventHandler handler)
        {
            foreach (Button itemButton in itemButtons)
            {
                itemButton.KeyPress -= handler;
            }
        }

        public new void Toggle()
        {
            if (IsToggled)
            {
                DisableToggle();
            }
            else
            {
                EnableToggle();
            }
        }

        public new void EnableToggle()
        {
            ApplicationFormStyle.ButtonsHighlighted(itemButtons);
            base.EnableToggle();
        }

        public new void DisableToggle()
        {
            ApplicationFormStyle.ButtonsNormal(itemButtons);
            base.DisableToggle();
        }

        public new void EnableInteraction()
        {
            ApplicationFormStyle.ButtonsHighlighted(itemButtons);
            base.EnableInteraction();
        }

        public new void DisableInteraction()
        {
            ApplicationFormStyle.ButtonsDisabled(itemButtons);
            base.DisableInteraction();
        }

        private void Button_MouseEnter(object sender, EventArgs e)
        {
            if (IsDisabled)
            {
                return;
            }

            if (IsToggled && AreFocusAnimationsOn)
            {
                ApplicationFormStyle.ButtonsNormal(itemButtons);
            }
            else
            {
                ApplicationFormStyle.ButtonsHighlighted(itemButtons);
            }
        }

        private void Button_MouseLeave(object sender, EventArgs e)
        {
            if (IsDisabled)
            {
                return;
            }

            if (IsToggled && AreUnfocusAnimationsOn)
            {
                ApplicationFormStyle.ButtonsHighlighted(itemButtons);
            }
            else
            {
                ApplicationFormStyle.ButtonsNormal(itemButtons);
            }
        }
    }
}
