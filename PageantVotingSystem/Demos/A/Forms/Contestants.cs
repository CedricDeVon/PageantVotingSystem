using System.Windows.Forms;

using PageantVotingSystem.Source.FormStyles;

namespace PageantVotingSystem.Source.Forms
{
    public partial class Contestants : Form
    {
        public Contestants()
        {
            InitializeComponent();

            ApplicationFormStyle.Setup(this);
        }
    }
}
