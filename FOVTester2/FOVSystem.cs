using System;
using System.Collections.Generic;
using System.Text;
using Xenko.Engine;
using Xenko.UI.Controls;
using Xenko.UI;
using Xenko.Core.Mathematics;
using Xenko.VirtualReality;

namespace FOVTester2 {
    public class FOVSystem : SyncScript {

        public TransformComponent CubeTransform;
        public UIPage TextPage;
        public TextBlock FOVResults;

        public override void Start() {
            FOVResults = TextPage.RootElement.GatherUIDictionary<TextBlock>()["AngleText"];
        }

        public static float FastAngleBetweenXZ(Vector3 vector, Vector3 otherVector) {
            return (float)Math.Atan2(vector.Z - otherVector.Z, vector.X - otherVector.X) * 180f / 3.14159265f;
        }

        public static float AngleOfVectorXZ(Vector3 vector) {
            return (float)Math.Atan2(vector.Z, vector.X) * 180f / 3.14159265f;
        }

        public static float FastAngleBetweenYX(Vector3 vector, Vector3 otherVector) {
            return (float)Math.Atan2(vector.Y - otherVector.Y, vector.Z - otherVector.Z) * 180f / 3.14159265f;
        }

        public static float AngleOfVectorYX(Vector3 vector) {
            return (float)Math.Atan2(vector.Y, vector.Z) * 180f / 3.14159265f;
        }

        public static float GetRelativeAngleXZ(Vector3 sourcePos, Vector3 sourceLook, Vector3 target) {
            float xz = FastAngleBetweenXZ(target, sourcePos) - AngleOfVectorXZ(sourceLook);
            return Math.Abs(180f + xz);
        }

        public static float GetRelativeAngleYX(Vector3 sourcePos, Vector3 sourceLook, Vector3 target) {
            float yx = FastAngleBetweenYX(target, sourcePos) - AngleOfVectorYX(sourceLook);
            return Math.Abs(yx - 180f);
        }

        public override void Update() {
            // update FOV number
            if (VRDeviceSystem.VRActive) {
                Vector3 p = VRDeviceSystem.GetSystem.Device.HeadPosition;
                Quaternion q = VRDeviceSystem.GetSystem.Device.HeadRotation;
                Vector3 lookVector = Vector3.Transform(Vector3.UnitZ, q);
                int xz = (int)Math.Round(GetRelativeAngleXZ(p, lookVector, CubeTransform.Position));
                int yx = (int)Math.Round(GetRelativeAngleYX(p, lookVector, CubeTransform.Position));
                FOVResults.Text = "Deg X: " + xz + "\n" + "Deg Y: " + yx;
            }
        }
    }
}
