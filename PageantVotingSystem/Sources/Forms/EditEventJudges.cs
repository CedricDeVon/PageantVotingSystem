
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
using PageantVotingSystem.Sources.Entities;

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
            judgeQueryLayout = new SingleValuedItemLayout(judgeQueryLayoutControl);
            judgeQueryLayout.ItemSingleClick += new EventHandler(JudgeQueryLayoutItem_SingleClick);
            selectedJudgesLayout = new OrderedValueItemLayout(selectedJudgeLayoutControl);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            informationLayout.StartLoadingMessageDisplay();

            if (sender == goBackButton)
            {
                ApplicationFormNavigator.DisplayPreviousForm();
                enterJudgeEmailQueryInput.Text = "";
                judgeQueryResultCountLabel.Text = "0";
                selectedJudgesCountLabel.Text = "0";
                judgeQueryLayout.Clear();
                selectedJudgesLayout.Clear();
                selectedJudgesLayout.Unfocus();
            }
            else if (sender == resetButton)
            {
                EditEventCache.JudgeEntities.ClearAllItems();

                enterJudgeEmailQueryInput.Text = "";
                judgeQueryResultCountLabel.Text = "0";
                selectedJudgesCountLabel.Text = "0";
                judgeQueryLayout.Clear();
                selectedJudgesLayout.Clear();
                selectedJudgesLayout.Unfocus();
            }

            informationLayout.StopLoadingMessageDisplay();
        }

        private void JudgeQueryLayoutItem_SingleClick(object sender, EventArgs e)
        {
            if (judgeQueryLayout.SelectedItem != null)
            {
                string judgeEmail = judgeQueryLayout.SelectedItem.Value;
                EditEventCache.JudgeEntities.AddNewItem(judgeEmail);
                judgeQueryLayout.Clear();
                selectedJudgesLayout.Render(judgeEmail);                
                enterJudgeEmailQueryInput.Text = "";
                judgeQueryResultCountLabel.Text = $"{judgeQueryLayout.Items.Count}";
                selectedJudgesCountLabel.Text = $"{EditEventCache.JudgeEntities.ItemCount}";
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
            if (selectedJudgesLayout.SelectedItem != null && e.KeyCode == Keys.NumPad8)
            {
                EditEventCache.JudgeEntities.MoveItemAtIndexUpwards(CalculateSelectedJudgeItemLayoutIndex());
                selectedJudgesLayout.MoveSelectedUpwards();

                e.Handled = true;
            }
            else if (selectedJudgesLayout.SelectedItem != null && e.KeyCode == Keys.NumPad2)
            {
                EditEventCache.JudgeEntities.MoveItemAtIndexDownwards(CalculateSelectedJudgeItemLayoutIndex());
                selectedJudgesLayout.MoveSelectedDownwards();

                e.Handled = true;
            }
            else if (selectedJudgesLayout.SelectedItem != null && e.KeyCode == Keys.Delete)
            {
                EditEventCache.JudgeEntities.RemoveItemAtIndex(CalculateSelectedJudgeItemLayoutIndex());
                selectedJudgesLayout.RemoveSelected();
                selectedJudgesLayout.Unfocus();

                selectedJudgesCountLabel.Text = $"{EditEventCache.JudgeEntities.ItemCount}";
                e.Handled = true;
            }

            if (e.KeyData == Keys.Escape)
            {
                ApplicationFormNavigator.DisplayPreviousForm();
                e.Handled = true;
            }
        }

        public void Render()
        {
            judgeQueryLayout.Clear();
            selectedJudgesLayout.Clear();
            for (int orderdNumber = EditEventCache.JudgeEntities.Items.Count; orderdNumber > 0; orderdNumber--)
            {
                string judgeEntity = EditEventCache.JudgeEntities.Items[EditEventCache.JudgeEntities.Items.Count - orderdNumber];
                selectedJudgesLayout.Render($"{orderdNumber}", judgeEntity, judgeEntity);
            }
            enterJudgeEmailQueryInput.Text = "";
            judgeQueryResultCountLabel.Text = $"0";
            selectedJudgesCountLabel.Text = $"{EditEventCache.JudgeEntities.ItemCount}";
        }

        private int CalculateSelectedJudgeItemLayoutIndex()
        {;
            return EditEventCache.JudgeEntities.ItemCount - Convert.ToInt32(selectedJudgesLayout.SelectedItem.OrderedNumber);
        }

        private List<string> ReadManyUniqueJudgeEmails()
        {
            List<string> keywords = enterJudgeEmailQueryInput.Text.Split(new char[] { ',' }).ToList();
            return ApplicationDatabase.ReadManyUniqueJudgeEmails(keywords, EditEventCache.JudgeEntities.Items.ToHashSet());
        }
    }
}
