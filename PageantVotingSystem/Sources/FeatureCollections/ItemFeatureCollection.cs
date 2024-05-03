
using System;
using System.Windows.Forms;
using System.Collections.Generic;

using PageantVotingSystem.Sources.Generics;

namespace PageantVotingSystem.Sources.FeatureCollections
{
    public abstract class ItemFeatureCollection
    {
        public bool IsClicked { get; set; }

        public bool IsToggled { get; set; }

        public bool IsDisabled { get; set; }

        public bool AreFocusAnimationsOn { get; set; }

        public bool AreUnfocusAnimationsOn { get; set; }

        public object FormControl { get; private set; }

        public Panel ParentControl { get; private set; }

        public Panel ItemControl { get; private set; }
        
        public GenericDoublyLinkedListItem GenericItemReference { get; private set; }

        public event EventHandler SingleClick;

        public event EventHandler DoubleClick;

        public event EventHandler MouseEnter;

        public event EventHandler MouseExit;

        public event EventHandler KeyUp;

        public event EventHandler KeyPressed;

        public event EventHandler KeyDown;

        protected Timer doubleClickDelayTimer;

        public ItemFeatureCollection(
            object formControl,
            Panel parentControl,
            Panel itemControl)
        {
            ThrowIfFormControlIsNull(formControl);
            ThrowIfParentControlIsNull(parentControl);
            ThrowIfItemControlIsNull(itemControl);

            FormControl = formControl;
            ParentControl = parentControl;
            ItemControl = itemControl;
            GenericItemReference = new GenericDoublyLinkedListItem(formControl);
            doubleClickDelayTimer = new Timer { Interval = 200 };
            doubleClickDelayTimer.Tick += new EventHandler(Timer_Tick);
            ParentControl.Controls.Add(itemControl);
            AreFocusAnimationsOn = true;
            AreUnfocusAnimationsOn = true;
        }

        public void Toggle()
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

        public void Dispose()
        {
            ParentControl.Controls.Remove(ItemControl);
            ((UserControl)FormControl).Dispose();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            IsClicked = false;
            doubleClickDelayTimer.Stop();
        }

        public void EnableToggle()
        {
            IsToggled = true;
        }

        public void DisableToggle()
        {
            IsToggled = false;
        }

        public void EnableInteraction()
        {
            IsDisabled = false;
        }

        public void DisableInteraction()
        {
            IsDisabled = true;
        }

        public void ConnectButtonsToItemClick(List<Button> buttons)
        {
            ThrowIfListButtonIsNull(buttons);

            foreach (Button button in buttons)
            {
                ConnectButtonToItemClick(button);
            }
        }

        public void ConnectButtonToItemClick(Button button)
        {
            ThrowIfButtonIsNull(button);

            button.Click += new EventHandler(Item_Click);
        }

        protected void Item_Click(object sender, EventArgs e)
        {
            if (IsDisabled)
            {
                return;
            }

            SingleClick?.Invoke(FormControl, e);
            if (IsClicked)
            {
                DoubleClick?.Invoke(FormControl, e);
                IsClicked = false;
                doubleClickDelayTimer.Stop();
            }
            else
            {
                IsClicked = true;
                doubleClickDelayTimer.Start();
            }
        }

        protected void ThrowIfFormControlIsNull(object formControl)
        {
            if (formControl == null)
            {
                throw new Exception("'ItemFeatureCollection' - 'formControl' cannot be null");
            }
        }

        protected void ThrowIfParentControlIsNull(Panel parentControl)
        {
            if (parentControl == null)
            {
                throw new Exception("'ItemFeatureCollection' - 'parentControl' cannot be null");
            }
        }

        protected void ThrowIfItemControlIsNull(Panel itemControl)
        {
            if (itemControl == null)
            {
                throw new Exception("'ItemFeatureCollection' - 'itemControl' cannot be null");
            }
        }

        protected void ThrowIfListButtonIsNull(List<Button> buttons)
        {
            if (buttons == null)
            {
                throw new Exception("'ItemFeatureCollection' - 'buttons' cannot be null");
            }
        }

        protected void ThrowIfButtonIsNull(Button button)
        {
            if (button == null)
            {
                throw new Exception("'ItemFeatureCollection' - 'button' cannot be null");
            }
        }
    }
}
