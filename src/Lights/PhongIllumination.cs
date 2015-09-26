using ImageSynthesis.Models;

namespace ImageSynthesis.Lights {

    class PhongIllumination {

        public PhongIllumination() {}

        // FIXME Object3D instead of Sphere
        // TODO doc
        public Color ComputeAmbientLight(AmbientLight aL, Sphere obj, V3 p) {
            return obj.Color * aL.Intensity * obj.Material.KAmbient;
        }
        
        // FIXME rename Id, Is ?
        // TODO check equations
        // TODO doc
        public Color ComputePointLight(PointLight pL, Sphere obj, V3 p) {
            V3 normalVec = obj.Normal(p);
            
            V3 incidentVec = pL.Direction(p);
            
            V3 reflectedVec = 2 * (normalVec * incidentVec) *
                              normalVec - incidentVec;
            reflectedVec.Normalize();
            
            // FIXME define cameraPosition outside
            V3 viewingVec = new V3(0, 0, 0) - p;
            viewingVec.Normalize();
            
            // Diffuse reflection
            Color Id = new Color(0, 0, 0);
            if (incidentVec * normalVec > 0.0f) {
                Id = obj.Color * pL.Intensity * obj.Material.KDiffuse *
                     (incidentVec * normalVec);
            }
            
            // Specular reflection
            Color Is = new Color(0, 0, 0);
            if (reflectedVec * viewingVec > 0.0f) {
                Is = pL.Intensity * obj.Material.KSpecular *
                     Mathf.Pow(
                        reflectedVec * viewingVec,
                        obj.Material.Shininess
                     );
            }
            
            return Id + Is;
        }
    }

}
