using System.Windows.Forms;

using PageantVotingSystem.Sources.FormStyles;

namespace PageantVotingSystem.Source.Forms
{
    public partial class Structure : Form
    {
        public Structure()
        {
            InitializeComponent();

            ApplicationFormStyle.SetupFormStyles(this);
        }
    }
}
