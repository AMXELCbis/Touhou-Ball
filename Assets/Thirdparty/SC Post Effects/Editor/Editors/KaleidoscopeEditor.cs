using UnityEditor;
using UnityEditor.Rendering;

namespace SCPE
{
    [VolumeComponentEditor(typeof(Kaleidoscope))]
    sealed class KaleidoscopeEditor : VolumeComponentEditor
    {
        SerializedDataParameter splits;

        private bool isSetup;

        public override void OnEnable()
        {
            base.OnEnable();

            var o = new PropertyFetcher<Kaleidoscope>(serializedObject);
            isSetup = AutoSetup.ValidEffectSetup<KaleidoscopeRenderer>();

            splits = Unpack(o.Find(x => x.splits));
        }


        public override void OnInspectorGUI()
        {
            SCPE_GUI.DisplayDocumentationButton("kaleidoscope");

            SCPE_GUI.DisplaySetupWarning<KaleidoscopeRenderer>(ref isSetup);

            PropertyField(splits);
        }
    }
}