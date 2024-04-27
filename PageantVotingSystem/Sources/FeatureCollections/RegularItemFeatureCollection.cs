
using System.Windows.Forms;

namespace PageantVotingSystem.Sources.FeatureCollections
{
    public class RegularItemFeatureCollection : ItemFeatureCollection
    {
        public RegularItemFeatureCollection(
            object formControl, Panel parentControl, Panel itemControl) :
            base(formControl, parentControl, itemControl)
        {
            AreFocusAnimationsOn = false;
            AreUnfocusAnimationsOn = true;
        }
    }
}
