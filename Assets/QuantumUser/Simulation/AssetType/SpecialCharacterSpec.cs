using Photon.Deterministic;

namespace Quantum
{
    public unsafe partial class SpecialCharacterSpec : CharacterSpec
    {
        public FP SpecailPower = 10;

        public override void UpdateStats(Frame f, ref CharacterSystem.Filter filter)
        {
            base.UpdateStats(f, ref filter);
        }



    }
}
