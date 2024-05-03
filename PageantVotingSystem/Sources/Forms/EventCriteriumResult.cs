
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
    public partial class EventCriteriumResult : Form
    {
        private readonly InformationLayout informationLayout;
        
        private readonly TopSideNavigationLayout topSideNavigationLayout;

        private readonly EventResultItemLayout resultLayout; 

        public EventCriteriumResult()
        {
            InitializeComponent();

            ApplicationFormStyle.SetupFormStyles(this);
            informationLayout = new InformationLayout(informationLayoutControl);
            topSideNavigationLayout = new TopSideNavigationLayout(topSideNavigationLayoutControl);
            resultLayout = new EventResultItemLayout(resultLayoutControl);
        }

        public void Render(int id)
        {
            List<ContestantResultEntity> entities = ApplicationDatabase.ReadManyCriteriumResultEntities(id);
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
                ApplicationFormNavigator.DisplayPreviousForm();
                ResetAllData();
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (sender == goBackButton)
            {
                ApplicationFormNavigator.DisplayPreviousForm();
                ResetAllData();
            }
        }
    }
}
