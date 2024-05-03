
using System;
using System.Windows.Forms;
using System.Collections.Generic;

using PageantVotingSystem.Sources.Forms;
using PageantVotingSystem.Sources.Caches;
using PageantVotingSystem.Sources.Setups;
using PageantVotingSystem.Sources.Loggers;
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
            ApplicationLogger.LogInformationMessage("'ApplicationFormNavigator' setup began");
            
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
                ApplicationLogger.LogInformationMessage($"'{form.Name}' setup for navigation");
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
            ApplicationLogger.LogInformationMessage("'ApplicationFormNavigator' setup complete");
        }

        public static new void BeginDisplayingForm(string foregroundFormName)
        {
            if (backgroundForm != null)
            {
                backgroundForm.Show();
            }

            ApplicationLogger.LogInformationMessage($"'ApplicationFormNavigator' began displaying at '{foregroundFormName}'");
            FormNavigator.BeginDisplayingForm(foregroundFormName);
        }

        public static new void DisplayNextForm(string formName)
        {
            if (informationNotFoundForm != null &&
                FormNavigator.IsFormNotFound(formName))
            {
                FormNavigator.DisplayNextForm(informationNotFoundForm);
            }
            else
            {
                FormNavigator.DisplayNextForm(formName);
            }
            ApplicationLogger.LogInformationMessage($"'ApplicationFormNavigator' displaying next form '{formName}'");
        }

        public static new void DisplayPreviousForm()
        {
            FormNavigator.DisplayPreviousForm();
            ApplicationLogger.LogInformationMessage($"'ApplicationFormNavigator' displaying previous form '{history.Peek().Name}'");
        }

        public static new void StopDisplay()
        {
            ApplicationLogger.LogInformationMessage($"'ApplicationFormNavigator' stoped displaying from '{history.Peek().Name}'");
            FormNavigator.StopDisplay();
        }

        public static void ListenToFormKeyDownEvent(Form form)
        {
            ThrowIfFormIsNull(form);

            form.KeyDown += new KeyEventHandler(Form_KeyDown);
        }

        public static void DisplayAboutForm()
        {
            DisplayNextForm("About");
        }

        public static void DisplayBackgroundForm()
        {
            DisplayNextForm("Background");
        }

        public static void DisplayInformationNotFoundForm()
        {
            DisplayNextForm("InformationNotFound");
        }

        public static void DisplayStartingMenuForm()
        {
            DisplayNextForm("StartingMenu");
        }

        public static void DisplayLogInForm()
        {
            DisplayNextForm("LogIn");
        }

        public static void DisplaySignUpForm()
        {
            DisplayNextForm("SignUp");
        }

        public static void LogOut()
        {
            DisplayStartingMenuForm();
        }

        public static void DisplayManagerDashboardForm()
        {
            DisplayNextForm("ManagerDashboard");
        }

        public static void DisplayJudgeDashboardForm()
        {
            DisplayNextForm("JudgeDashboard");
        }

        public static void DisplayJudgeContestantDashboardForm(
            EventLayoutSequenceEntity eventLayoutSequenceEntity,
            ContestantEntity contestantEntity)
        {
            ((JudgeContestantDashboard)GetForm("JudgeContestantDashboard")).Render(eventLayoutSequenceEntity, contestantEntity);
            DisplayNextForm("JudgeContestantDashboard");
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

        public static void DisplayEditUserProfileForm()
        {
            ((EditUserProfile)GetForm("EditUserProfile")).Render();
            DisplayNextForm("EditUserProfile");
        }

        public static void DisplayEventResultsForm()
        {
            DisplayNextForm("EventResults");
        }

        public static void DisplayEventJudgesForm(EventEntity eventEntity)
        {
            ((EventJudges)GetForm("EventJudges")).Render(eventEntity);
            DisplayNextForm("EventJudges");
        }

        public static void DisplayEventContestantsForm(EventEntity eventEntity)
        {
            ((EventContestants)GetForm("EventContestants")).Render(eventEntity);
            DisplayNextForm("EventContestants");
        }

        public static void DisplayEventLayoutForm(EventEntity eventEntity)
        {
            ((EventLayout)GetForm("EventLayout")).Render(eventEntity);
            DisplayNextForm("EventLayout");
        }

        public static void DisplayEventProfileForm(int eventEntityId)
        {
            ((EventProfile)GetForm("EventProfile")).Render(eventEntityId);
            DisplayNextForm("EventProfile");
        }

        public static void DisplayEventSegmentProfileForm(int segmentEntityId)
        {
            ((EventSegmentProfile)GetForm("EventSegmentProfile")).Render(segmentEntityId);
            DisplayNextForm("EventSegmentProfile");
        }

        public static void DisplayEventRoundProfileForm(int roundEntityId)
        {
            ((EventRoundProfile)GetForm("EventRoundProfile")).Render(roundEntityId);
            DisplayNextForm("EventRoundProfile");
        }

        public static void DisplayEventCriteriumProfileForm(int criteriumEntityId)
        {
            ((EventCriteriumProfile)GetForm("EventCriteriumProfile")).Render(criteriumEntityId);
            DisplayNextForm("EventCriteriumProfile");
        }

        public static void DisplayUserProfileForm(string userEntityEmail)
        {
            ((UserProfile)GetForm("UserProfile")).Render(userEntityEmail);
            DisplayNextForm("UserProfile");
        }

        public static void DisplayEventContestantProfileForm(int contestantEntityId)
        {
            ((EventContestantProfile)GetForm("EventContestantProfile")).Render(contestantEntityId);
            DisplayNextForm("EventContestantProfile");
        }

        public static void DisplayEventCriteriumResultForm(int criteriumEntityId)
        {
            ((EventCriteriumResult)GetForm("EventCriteriumResult")).Render(criteriumEntityId);
            DisplayNextForm("EventCriteriumResult");
        }

        public static void DisplayEventRoundResultForm(int roundEntityId)
        {
            ((EventRoundResult)GetForm("EventRoundResult")).Render(roundEntityId);
            DisplayNextForm("EventRoundResult");
        }

        public static void DisplayEventSegmentResultForm(int segmentEntityId)
        {
            ((EventSegmentResult)GetForm("EventSegmentResult")).Render(segmentEntityId);
            DisplayNextForm("EventSegmentResult");
        }

        public static void DisplayAdministerEventForm()
        {
            ((AdministerEvent)GetForm("AdministerEvent")).Render();
            DisplayNextForm("AdministerEvent");
        }

        public static void DisplayAdministerEventQueryForm()
        {
            DisplayNextForm("AdministerEventQuery");
        }

        public static void DisplayAdministerEventContestantsForm()
        {
            ((AdministerEventContestants)GetForm("AdministerEventContestants")).Render();
            DisplayNextForm("AdministerEventContestants");
        }

        public static void DisplayAdministerEventContestantReviewForm()
        {
            ((AdministerEventContestantReview)GetForm("AdministerEventContestantReview")).Render();
            DisplayNextForm("AdministerEventContestantReview");
        }

        public static void DisplayAdministerVotingSessionForm()
        {
            ((AdministerVotingSession)GetForm("AdministerVotingSession")).Render();
            DisplayNextForm("AdministerVotingSession");
        }

        public static void DisplayEditEventForm()
        {
            DisplayNextForm("EditEvent");
        }

        public static void DisplayEditEventProfileForm()
        {
            ((EditEventProfile)GetForm("EditEventProfile")).Render();
            DisplayNextForm("EditEventProfile");
        }

        public static void DisplayEditEventJudgesForm()
        {
            ((EditEventJudges)GetForm("EditEventJudges")).Render();
            DisplayNextForm("EditEventJudges");
        }

        public static void DisplayEditEventContestantsForm()
        {
            ((EditEventContestants)GetForm("EditEventContestants")).Render();
            DisplayNextForm("EditEventContestants");
        }

        public static void DisplayEditEventSegmentStructureForm()
        {
            ((EditEventSegmentStructure)GetForm("EditEventSegmentStructure")).Render();
            DisplayNextForm("EditEventSegmentStructure");
        }

        public static void DisplayEditEventRoundStructureForm()
        {
            DisplayNextForm("EditEventRoundStructure");
        }

        public static void DisplayEditEventCriteriumStructureForm()
        {
            DisplayNextForm("EditEventCriteriumStructure");
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

        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                BeforeKeyPress?.Invoke(sender, e);
                ApplicationFormNavigator.DisplayPreviousForm();
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
