
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

using PageantVotingSystem.Sources.Setups;
using PageantVotingSystem.Sources.Loggers;

namespace PageantVotingSystem.Sources.FormStyles
{
    public class ApplicationFormStyle
    {
        public static Color NormalColor { get; private set; }

        public static Color HighlightColor { get; private set; }

        public static Color DisabledColor { get; private set; }
        
        public static Color ErrorColor { get; private set; }

        public static Color SuccessColor { get; private set; }

        public static void Setup()
        {
            SetupRecorder.ThrowIfAlreadySetup("ApplicationFormStyle");
            ApplicationLogger.LogInformationMessage("'ApplicationFormStyle' setup began");
            
            NormalColor = Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(187)))), ((int)(((byte)(14)))));
            HighlightColor = Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(0)))), ((int)(((byte)(241)))));
            DisabledColor = Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(79)))), ((int)(((byte)(9)))));
            ErrorColor = Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(14)))), ((int)(((byte)(14)))));
            SuccessColor = Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(255)))), ((int)(((byte)(14)))));

            SetupRecorder.Add("ApplicationFormStyle");
            ApplicationLogger.LogInformationMessage("'ApplicationFormStyle' setup complete");
        }

        public static void SetupFormStyles(Form form)
        {
            form.FormBorderStyle = FormBorderStyle.None;
            form.KeyPreview = true;
        }

        public static void UpdateImage(Button target, Bitmap imageResource)
        {
            target.Image = imageResource;
        }

        public static void ButtonsNormal(List<Button> buttons)
        {
            foreach (Button button in buttons)
            {
                ButtonNormal(button);
            }
        }

        public static void ButtonNormal(Button button)
        {
            button.BackColor = NormalColor;
            button.FlatAppearance.BorderColor = NormalColor;
            button.FlatAppearance.MouseDownBackColor = NormalColor;
            button.FlatAppearance.MouseOverBackColor = NormalColor;
        }

        public static void ButtonsHighlighted(List<Button> buttons)
        {
            foreach (Button button in buttons)
            {
                ButtonHighlighted(button);
            }
        }

        public static void ButtonHighlighted(Button button)
        {
            button.BackColor = HighlightColor;
            button.FlatAppearance.BorderColor = HighlightColor;
            button.FlatAppearance.MouseDownBackColor = HighlightColor;
            button.FlatAppearance.MouseOverBackColor = HighlightColor;
        }

        public static void ButtonsDisabled(List<Button> buttons)
        {
            foreach (Button button in buttons)
            {
                ButtonDisabled(button);
            }
        }

        public static void ButtonDisabled(Button button)
        {
            button.BackColor = DisabledColor;
            button.FlatAppearance.BorderColor = DisabledColor;
            button.FlatAppearance.MouseDownBackColor = DisabledColor;
            button.FlatAppearance.MouseOverBackColor = DisabledColor;
        }

        public static void ButtonsHidden(List<Button> buttons)
        {
            foreach (Button button in buttons)
            {
                ButtonHidden(button);
            }
        }

        public static void ButtonHidden(Button button)
        {
            button.BackColor = Color.Transparent;
            button.FlatAppearance.BorderColor = Color.Transparent;
            button.FlatAppearance.MouseDownBackColor = Color.Transparent;
            button.FlatAppearance.MouseOverBackColor = Color.Transparent;
        }

        public static void ButtonsError(List<Button> buttons)
        {
            foreach (Button button in buttons)
            {
                ButtonError(button);
            }
        }

        public static void ButtonError(Button button)
        {
            button.BackColor = ErrorColor;
            button.FlatAppearance.BorderColor = ErrorColor;
            button.FlatAppearance.MouseDownBackColor = ErrorColor;
            button.FlatAppearance.MouseOverBackColor = ErrorColor;
        }

        public static void ButtonsSuccess(List<Button> buttons)
        {
            foreach (Button button in buttons)
            {
                ButtonSuccess(button);
            }
        }

        public static void ButtonSuccess(Button button)
        {
            button.BackColor = SuccessColor;
            button.FlatAppearance.BorderColor = SuccessColor;
            button.FlatAppearance.MouseDownBackColor = SuccessColor;
            button.FlatAppearance.MouseOverBackColor = SuccessColor;
        }
    }
}
