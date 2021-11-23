using System;
using UnityEngine;
using Verse;

namespace Industrail_Revolution
{
    public class Projectile_GaussGrenade : Projectile
    {
		private const int V = 1;
		private int ticksToDetonation;

        public  override void ExposeData()
        {
            base.ExposeData();
            // ISSUE: cast to a reference type
            Scribe_Values.Look<int>( ref this.ticksToDetonation, "ticksToDetonation", 0, false);
        }

        public override void Tick()
        {
            base.Tick();
            if (this.ticksToDetonation <= 0)
                return;
            --this.ticksToDetonation;
            if (this.ticksToDetonation > 0)
                return;
            this.Explode();
        }

        protected virtual void Impact(Thing hitThing)
        {
            if (def.projectile.explosionDelay == null)
            {
                this.Explode();
            }
            else
            {
                this.landed = Convert.ToBoolean(V);
                this.ticksToDetonation = (int)((ProjectileProperties)((ThingDef)((Thing)this).def).projectile).explosionDelay;
            }
        }

        protected virtual void Explode()
        {
            ((Thing)this).Destroy((DestroyMode)0);
			float projectile = (float)Convert.ToDouble(def.projectile);
            for (int index = 0; index < 4; ++index)
            {
                IntVec3 position = ((Thing)this).Position;
                position = ((Thing)this).Position;
            }
        }

        public static void ThrowSmokeBlue(Vector3 loc, Map map, float size)
        {
            if (!GenView.ShouldSpawnMotesAt(loc, map) || ((MoteCounter)map.moteCounter).SaturatedLowPriority)
                return;
            MoteThrown moteThrown = (MoteThrown)ThingMaker.MakeThing(ThingDef.Named("Mote_SmokeBlue"), (ThingDef)null);
            ((Mote)moteThrown).Scale = Rand.Range(1.5f, 2.5f) * size;
            ((Mote)moteThrown).rotationRate = Rand.Range(-30f, 30f);
            ((Mote)moteThrown).exactPosition = loc;
            moteThrown.SetVelocity((float)Rand.Range(30, 40), Rand.Range(0.5f, 0.7f));
            GenSpawn.Spawn((Thing)moteThrown, IntVec3Utility.ToIntVec3(loc), map);
        }

        public static void ThrowMicroSparksBlue(Vector3 loc, Map map)
        {
            if (!GenView.ShouldSpawnMotesAt(loc, map) || ((MoteCounter)map.moteCounter).SaturatedLowPriority)
                return;
            MoteThrown moteThrown = (MoteThrown)ThingMaker.MakeThing(ThingDef.Named("Mote_MicroSparksBlue"), (ThingDef)null);
            ((Mote)moteThrown).Scale = Rand.Range(0.8f, 1.2f);
            ((Mote)moteThrown).rotationRate = Rand.Range(-12f, 12f);
            ((Mote)moteThrown).exactPosition = loc;
            moteThrown.SetVelocity((float)Rand.Range(35, 45), 1.2f);
            GenSpawn.Spawn((Thing)moteThrown, IntVec3Utility.ToIntVec3(loc), map);
        }
    }
}
