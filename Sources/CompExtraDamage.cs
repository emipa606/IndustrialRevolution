using Verse;

namespace Industrial_Revolution
{
    public class CompExtraDamage : ThingComp
    {
        public CompProperties compProp;
        public int count;
        public string damageDef;
        public int damageAmount = 10;
        public float chanceToProc = 0.1f;

        public virtual void Initialize(CompProperties vprops)
        {
            base.Initialize(vprops);
            this.compProp = vprops;
        }

        public virtual void PostExposeData()
        {
            base.PostExposeData();
            // ISSUE: cast to a reference type
            Scribe_Values.Look<int>(ref this.count, "count", 1, false);
        }
    }
}
