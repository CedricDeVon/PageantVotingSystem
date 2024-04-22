
using System;
using System.Windows.Forms;
using System.Collections.Generic;

using PageantVotingSystem.Sources.Entities;
using PageantVotingSystem.Sources.Databases;
using PageantVotingSystem.Sources.FormStyles;
using PageantVotingSystem.Sources.FormControls;
using PageantVotingSystem.Sources.FormNavigators;

namespace PageantVotingSystem.Sources.Forms
{
    public partial class EventRoundResult : Form
    {
        public InformationLayout InformationLayout { get; private set; }
        
        private readonly TopSideNavigationLayout topSideNavigationLayout;

        private readonly EventResultItemLayout resultLayout;

        public EventRoundResult()
        {
            InitializeComponent();

            ApplicationFormStyle.SetupFormStyles(this);
            InformationLayout = new InformationLayout(informationLayoutControl);
            topSideNavigationLayout = new TopSideNavigationLayout(topSideNavigationLayoutControl);
            topSideNavigationLayout.HideReloadButton();
            resultLayout = new EventResultItemLayout(resultLayoutControl);
        }

        public void Render(int id)
        {
            List<ContestantResultEntity> entities = ApplicationDatabase.ReadManyRoundResults(id);
            resultLayout.Render(entities);
        }

        private void ResetAllData()
        {
            resultLayout.Clear();
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                ApplicationFormNavigator.DisplayPrevious();
                ResetAllData();
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (sender == goBackButton)
            {
                ApplicationFormNavigator.DisplayPrevious();
                ResetAllData();
            }
        }
    }
}
