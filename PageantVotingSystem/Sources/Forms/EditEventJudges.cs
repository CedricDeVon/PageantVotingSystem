
using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;

using PageantVotingSystem.Sources.Caches;
using PageantVotingSystem.Sources.Results;
using PageantVotingSystem.Sources.Security;
using PageantVotingSystem.Sources.Databases;
using PageantVotingSystem.Sources.FormStyles;
using PageantVotingSystem.Sources.FormControls;
using PageantVotingSystem.Sources.FormNavigators;

namespace PageantVotingSystem.Sources.Forms
{
    public partial class EditEventJudges : Form
    {
        private readonly InformationLayout informationLayout;

        private readonly TopSideNavigationLayout topSideNavigationLayout;

        private readonly SingleValuedItemLayout judgeQueryLayout;

        private readonly OrderedValueItemLayout selectedJudgesLayout;

        public EditEventJudges()
        {
            InitializeComponent();

            ApplicationFormStyle.SetupFormStyles(this);
            informationLayout = new InformationLayout(informationLayoutControl);
            topSideNavigationLayout = new TopSideNavigationLayout(topSideNavigationLayoutControl);
            topSideNavigationLayout.HideReloadButton();
            judgeQueryLayout = new SingleValuedItemLayout(judgeQueryLayoutControl);
            judgeQueryLayout.ItemSingleClick += new EventHandler(JudgeQueryLayoutItem_SingleClick);
            selectedJudgesLayout = new OrderedValueItemLayout(selectedJudgeLayoutControl);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            informationLayout.StartLoadingMessageDisplay();

            if (sender == saveButton)
            {
                Result securityResult = ApplicationSecurity.AuthenticateNewEventJudges(EditEventCache.Judges);
                if (!securityResult.IsSuccessful)
                {
                    informationLayout.DisplayErrorMessage(securityResult.Message);
                    return;
                }

                ApplicationFormNavigator.DisplayPrevious();
                selectedJudgesLayout.Unfocus();
            }
            else if (sender == resetButton)
            {
                EditEventCache.Judges.ClearAllItems();

                judgeQueryLayout.Clear();
                selectedJudgesLayout.Clear();
                selectedJudgesLayout.Unfocus();

                enterJudgeEmailQueryInput.Text = "";
                judgeQueryResultCountLabel.Text = "0";
                selectedJudgesCountLabel.Text = "0";
            }

            informationLayout.StopLoadingMessageDisplay();
        }

        private void JudgeQueryLayoutItem_SingleClick(object sender, EventArgs e)
        {
            if (judgeQueryLayout.SelectedItem != null)
            {
                string judgeEmail = judgeQueryLayout.SelectedItem.Value;
                judgeQueryLayout.Clear();

                EditEventCache.Judges.AddNewItem(judgeEmail);
                selectedJudgesLayout.RenderOrdered(judgeEmail);
                
                enterJudgeEmailQueryInput.Text = "";
                judgeQueryResultCountLabel.Text = $"{judgeQueryLayout.Items.Count}";
                selectedJudgesCountLabel.Text = $"{EditEventCache.Judges.ItemCount}";
            }
        }

        private void Control_KeyDown(object sender, KeyEventArgs e)
        {
            if (sender != enterJudgeEmailQueryInput)
            {
                return;
            }

            if (e.KeyCode == Keys.Enter)
            {
                judgeQueryLayout.Clear();

                List<string> judgeEmails = ReadManyUniqueJudgeEmails();
                judgeQueryLayout.Render(judgeEmails);

                judgeQueryResultCountLabel.Text = $"{judgeEmails.Count}";
                
                e.Handled = true;
            }
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (selectedJudgesLayout.SelectedItem == null)
            {
                return;
            }

            if (e.KeyCode == Keys.NumPad8)
            {
                EditEventCache.Judges.MoveItemAtIndexUpwards(CalculateSelectedJudgeItemLayoutIndex());
                selectedJudgesLayout.MoveSelectedUpwards();

                e.Handled = true;
            }
            else if (e.KeyCode == Keys.NumPad2)
            {
                EditEventCache.Judges.MoveItemAtIndexDownwards(CalculateSelectedJudgeItemLayoutIndex());
                selectedJudgesLayout.MoveSelectedDownwards();

                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Delete)
            {
                EditEventCache.Judges.RemoveItemAtIndex(CalculateSelectedJudgeItemLayoutIndex());
                selectedJudgesLayout.RemoveSelected();
                selectedJudgesLayout.Unfocus();

                selectedJudgesCountLabel.Text = $"{EditEventCache.Judges.ItemCount}";
                
                e.Handled = true;
            }
        }

        private int CalculateSelectedJudgeItemLayoutIndex()
        {;
            return EditEventCache.Judges.ItemCount - Convert.ToInt32(selectedJudgesLayout.SelectedItem.OrderedNumber);
        }

        private List<string> ReadManyUniqueJudgeEmails()
        {
            List<string> keywords = enterJudgeEmailQueryInput.Text.Split(new char[] { ',' }).ToList();
            return ApplicationDatabase.ReadManyUniqueJudgeEmails(keywords, EditEventCache.Judges.Items.ToHashSet());
        }
    }
}
