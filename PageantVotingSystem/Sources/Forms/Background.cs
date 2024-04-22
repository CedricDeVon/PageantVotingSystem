
using System.Windows.Forms;

using PageantVotingSystem.Sources.FormStyles;

namespace PageantVotingSystem.Sources.Forms
{
    public partial class Background : Form
    {
        public Background()
        {
            InitializeComponent();

            ApplicationFormStyle.SetupFormStyles(this);
        }
    }
}
