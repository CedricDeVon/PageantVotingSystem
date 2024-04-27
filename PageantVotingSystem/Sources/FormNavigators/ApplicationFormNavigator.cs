
using System;
using System.Windows.Forms;
using System.Collections.Generic;

using PageantVotingSystem.Sources.Forms;
using PageantVotingSystem.Sources.Caches;
using PageantVotingSystem.Sources.Setups;
using PageantVotingSystem.Sources.Entities;

namespace PageantVotingSystem.Sources.FormNavigators
{
    public class ApplicationFormNavigator : FormNavigator
    {
        public static event EventHandler BeforeKeyPress;

        public static event EventHandler AfterKeyPress;

        private static Form backgroundForm;

        private static Form informationNotFoundForm;

        public static void Setup(
            List<Form> forms,
            Form selectedBackgroundForm = null,
            Form selectedInformationNotFoundForm = null)
        {
            SetupRecorder.ThrowIfAlreadySetup("ApplicationFormNavigator");

            if (selectedBackgroundForm != null &&
                selectedInformationNotFoundForm != null &&
                selectedBackgroundForm == selectedInformationNotFoundForm)
            {
                throw new Exception("'ApplicationFormNavigator' - 'Background' and 'InformationNotFound' forms must be unique");
            }
            foreach (Form form in forms)
            {
                ThrowIfFormIsNull(form);
                if (form == selectedBackgroundForm || form == selectedInformationNotFoundForm)
                {
                    throw new Exception("'ApplicationFormNavigator' - 'Background' and 'InformationNotFound' forms must be unique");
                }

                AddForm(form);
            }

            backgroundForm = selectedBackgroundForm;
            if (selectedBackgroundForm != null)
            {
                AddForm(selectedBackgroundForm);
            }

            informationNotFoundForm = selectedInformationNotFoundForm;
            if (selectedInformationNotFoundForm != null)
            {
                AddForm(selectedInformationNotFoundForm);
            }

            SetupRecorder.Add("ApplicationFormNavigator");
        }

        public static new void BeginDisplay(string foregroundFormName)
        {
            if (backgroundForm != null)
            {
                backgroundForm.Show();
            }

            FormNavigator.BeginDisplay(foregroundFormName);
        }

        public static new void DisplayNext(string formName)
        {
            if (informationNotFoundForm != null &&
                FormNavigator.IsNotFound(formName))
            {
                FormNavigator.DisplayNext(informationNotFoundForm);
            }
            else
            {
                FormNavigator.DisplayNext(formName);
            }
        }

        public static void ListenToFormKeyDownEvent(Form form)
        {
            ThrowIfFormIsNull(form);

            form.KeyDown += new KeyEventHandler(Form_KeyDown);
        }

        public static void DisplayAboutForm()
        {
            DisplayNext("About");
        }

        public static void DisplayBackgroundForm()
        {
            DisplayNext("Background");
        }

        public static void DisplayInformationNotFoundForm()
        {
            DisplayNext("InformationNotFound");
        }

        public static void DisplayStartingMenuForm()
        {
            DisplayNext("StartingMenu");
        }

        public static void DisplayLogInForm()
        {
            DisplayNext("LogIn");
        }

        public static void DisplaySignUpForm()
        {
            DisplayNext("SignUp");
        }

        public static void DisplayManagerDashboardForm()
        {
            DisplayNext("ManagerDashboard");
        }

        public static void DisplayEditEventForm()
        {
            DisplayNext("EditEvent");
        }

        public static void DisplayEditEventProfileForm()
        {
            ((EditEventProfile)GetForm("EditEventProfile")).Render();
            DisplayNext("EditEventProfile");
        }

        public static void DisplayEditEventJudgesForm()
        {
            DisplayNext("EditEventJudges");
        }

        public static void DisplayEditEventContestantsForm()
        {
            DisplayNext("EditEventContestants");
        }

        public static void DisplayEditEventSegmentStructureForm()
        {
            DisplayNext("EditEventSegmentStructure");
        }

        public static void DisplayEditEventRoundStructureForm()
        {
            DisplayNext("EditEventRoundStructure");
        }

        public static void DisplayEditEventCriteriumStructureForm()
        {
            DisplayNext("EditEventCriteriumStructure");
        }

        public static void DisplayEditUserProfileForm()
        {
            ((EditUserProfile)GetForm("EditUserProfile")).Render();
            DisplayNext("EditUserProfile");
        }

        public static void DisplayEventResultsForm()
        {
            DisplayNext("EventResults");
        }
        
        public static void DisplayEventJudgesForm(EventEntity entity)
        {
            ((EventJudges)GetForm("EventJudges")).Render(entity);
            DisplayNext("EventJudges");
        }
        
        public static void DisplayEventContestantsForm(EventEntity entity)
        {
            ((EventContestants)GetForm("EventContestants")).Render(entity);
            DisplayNext("EventContestants");
        }
        
        public static void DisplayEventLayoutForm(EventEntity entity)
        {
            ((EventLayout)GetForm("EventLayout")).Render(entity);
            DisplayNext("EventLayout");
        }

        public static void DisplayEventProfileForm(EventEntity entity)
        {
            ((EventProfile)GetForm("EventProfile")).Render(entity);
            DisplayNext("EventProfile");
        }

        public static void DisplayEventSegmentProfileForm(SegmentEntity entity)
        {
            ((EventSegmentProfile)GetForm("EventSegmentProfile")).Render(entity);
            DisplayNext("EventSegmentProfile");
        }

        public static void DisplayEventRoundProfileForm(RoundEntity entity)
        {
            ((EventRoundProfile)GetForm("EventRoundProfile")).Render(entity);
            DisplayNext("EventRoundProfile");
        }

        public static void DisplayEventCriteriumProfileForm(CriteriumEntity entity)
        {
            ((EventCriteriumProfile)GetForm("EventCriteriumProfile")).Render(entity);
            DisplayNext("EventCriteriumProfile");
        }

        public static void DisplayUserProfileForm(UserEntity userEntity)
        {
            ((UserProfile)GetForm("UserProfile")).Render(userEntity);
            DisplayNext("UserProfile");
        }

        public static void DisplayEventContestantProfileForm(ContestantEntity userEntity)
        {
            ((EventContestantProfile)GetForm("EventContestantProfile")).Render(userEntity);
            DisplayNext("EventContestantProfile");
        }
        
        public static void DisplayEventCriteriumResultForm(int id)
        {
            ((EventCriteriumResult)GetForm("EventCriteriumResult")).Render(id);
            DisplayNext("EventCriteriumResult");
        }

        public static void DisplayEventRoundResultForm(int id)
        {
            ((EventRoundResult)GetForm("EventRoundResult")).Render(id);
            DisplayNext("EventRoundResult");
        }
        
        public static void DisplayEventSegmentResultForm(int id)
        {
            ((EventSegmentResult)GetForm("EventSegmentResult")).Render(id);
            DisplayNext("EventSegmentResult");
        }

        public static void DisplayAdministerEventForm()
        {
            DisplayNext("AdministerEvent");
        }

        public static void DisplayAdministerEventContestantsForm()
        {
            DisplayNext("AdministerEventContestants");
        }

        public static void DisplayAdministerEventJudgesForm()
        {
            DisplayNext("AdministerEventJudges");
        }

        public static void DisplayAdministerEventLayoutForm()
        {
            DisplayNext("AdministerEventLayout");
        }

        public static void DisplayAdministerVotingSessionForm()
        {
            DisplayNext("AdministerVotingSession");
        }

        public static void DisplayJudgeDashboardForm()
        {
            DisplayNext("JudgeDashboard");
        }

        public static void DisplayJudgeContestantDashboardForm(EventEntity eventEntity, ContestantEntity contestantEntity)
        {
            ((JudgeContestantDashboard)GetForm("JudgeContestantDashboard")).Render(eventEntity, contestantEntity);
            DisplayNext("JudgeContestantDashboard");
        }

        public static void DisplayManagerOrJudgeDashboardForm(string userRoleType)
        {
            ThrowInvalidUserRoleType(userRoleType);

            if (userRoleType == "Manager")
            {
                DisplayManagerDashboardForm();
            }
            else if (userRoleType == "Judge")
            {
                DisplayJudgeDashboardForm();
            }
        }

        public static void DisplayEditEventRoundStructureForm(SegmentEntity segmentEntity)
        {
            ((EditEventRoundStructure)GetForm("EditEventRoundStructure")).Render(segmentEntity);
            DisplayEditEventRoundStructureForm();
        }

        public static void DisplayEditEventCriteriumStructureForm(SegmentEntity segmentEntity, RoundEntity roundEntity)
        {
            ((EditEventCriteriumStructure)GetForm("EditEventCriteriumStructure")).Render(segmentEntity, roundEntity);
            DisplayEditEventCriteriumStructureForm();
        }

        public static void LogOut()
        {
            DisplayStartingMenuForm();
        }

        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                BeforeKeyPress?.Invoke(sender, e);
                ApplicationFormNavigator.DisplayPrevious();
                AfterKeyPress?.Invoke(sender, e);

                e.Handled = true;
            }
        }

        private static void ThrowInvalidUserRoleType(string userRoleType)
        {
            if (UserRoleCache.IsNotFound(userRoleType))
            {
                throw new Exception($"'ApplicationFormNavigator' - User role type '{userRoleType}' is invalid");
            }
        }
    }
}
