using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace Industrial_Revolution
{
    public class HeadgearFrame : Apparel
    {
        public bool init = false;

        public virtual void DrawWornExtras()
        {
            if (!ThingUtility.DestroyedOrNull((Thing)this.Wearer))
            {
                if (!this.init)
                {
                    ApparelGraphicRecord apparelGraphicRecord = new ApparelGraphicRecord();
                    ApparelGraphicRecordGetter.TryGetGraphicApparel((Apparel)this, (BodyTypeDef)((Pawn_StoryTracker)this.Wearer.story).bodyType, out apparelGraphicRecord);
                    ((List<ApparelGraphicRecord>)((PawnGraphicSet)((PawnRenderer)this.Wearer.Drawer.renderer).graphics).apparelGraphics).Remove(apparelGraphicRecord);
                    PortraitsCache.Clear();
                    this.init = true;
                }
                if (this.init && this.Wearer == null)
                    this.init = false;
                if (PawnUtility.GetPosture(this.Wearer) == 0)
                {
                    Material material = GraphicDatabase.Get<Graphic_Multi>((string)((GraphicData)((ThingDef)((Thing)this).def).graphicData).texPath, (Shader)ShaderDatabase.Cutout, Vector2.one, ((Thing)this).DrawColor).MatAt(((Thing)this.Wearer).Rotation, (Thing)null);
                    Vector3 vector3_1;
                    // ISSUE: explicit constructor call
                    Matrix4x4 matrix4x4 = new Matrix4x4();
                    Vector3 vector3_2;
                    // ISSUE: explicit constructor call
                    
                    if (RestUtility.Awake(this.Wearer) || !this.Wearer.Downed)
                    {
                        Graphics.DrawMesh(((GraphicMeshSet)MeshPool.humanlikeHeadSet).MeshAt(((Thing)this.Wearer).Rotation), matrix4x4, material, 0);
                    }
                }
            }
            if (Find.TickManager.TicksGame % 120 == 0)
                this.init = false;
            base.DrawWornExtras();
        }

        public virtual void Tick()
        {
            ((ThingWithComps)this).Tick();
            if (!ThingUtility.DestroyedOrNull((Thing)this.Wearer) || !this.init)
                return;
            this.init = false;
        }
    }
}
