using System;
using System.Windows.Forms;

using PageantVotingSystem.Source.Forms;

namespace PageantVotingSystem.Source.FormNavigators
{
    public class ApplicationFormNavigator : FormNavigator
    {
        private static bool isLoaded = false;

        public static void Setup()
        {
            if (isLoaded)
            {
                throw new Exception("'ApplicationFormNavigator' is already loaded");
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Add(new Background());
            Add(new StartingMenu());
            Add(new About());
            Add(new SignUp());
            Add(new LogIn());
            Add(new UserInformation());
            Add(new ManagerDashboard());
            Add(new EditEvent());
            Add(new BaseInformation());
            Add(new Structure());
            Add(new Judges());
            Add(new Contestants());
            Add(new JudgeDashboard());
            Add(new Features());

            Start("Features", "Background");

            isLoaded = true;
        }

        public static void ManagerOrJudge(string userRoleType)
        {
            if (userRoleType == "Manager")
            {
                Next("ManagerDashboard");
            }
            else if (userRoleType == "Judge")
            {
                Next("JudgeDashboard");
            }
        }

        public static void UpdateUserInformationFormData()
        {
            ((UserInformation)Get("UserInformation")).UpdateData();
        }
    }
}
