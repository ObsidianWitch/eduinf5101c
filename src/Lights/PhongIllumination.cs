using System.Collections.Generic;
using ImageSynthesis.Models;

namespace ImageSynthesis.Lights {

    class PhongIllumination : IlluminationModel {
        
        private V3 CameraPos;
        
        public PhongIllumination(V3 cameraPos) {
            CameraPos = cameraPos;
        }
        
        /// Computes the ambient component of the Phong reflection model for
        /// an ambient light.
        override public Color ComputeAmbientLight(
            AmbientLight aL, Object3D obj, V3 p, V2 uv
        ) {
            return obj.TextureColor(uv) * aL.Intensity * obj.Material.KAmbient;
        }
        
        /// Computes the diffuse and specular components of the Phong
        /// reflection model for a point light.
        override public Color ComputePointLight(
            PointLight pL, Object3D obj, V3 p, V2 uv
        ) {
            V3 normalVec = obj.Normal(p, uv);
            
            V3 incidentVec = pL.Position - p;
            incidentVec.Normalize();
            
            V3 reflectedVec = 2 * (normalVec * incidentVec) *
                              normalVec - incidentVec;
            reflectedVec.Normalize();
            
            V3 viewingVec = CameraPos - p;
            viewingVec.Normalize();
            
            Color diffuseIllu = ComputeDiffuseComponent(
                pL, incidentVec, normalVec, obj, uv
            );
            Color specularIllu = ComputeSpecularComponent(
                pL, reflectedVec, normalVec, obj, uv
            );
            
            return diffuseIllu + specularIllu;
        }
        
        /// Computes the diffuse and specular components of the Phong
        /// reflection model for a directional light.
        override public Color ComputeDirectionalLight(
            DirectionalLight dL, Object3D obj, V3 p, V2 uv
        ) {
            V3 normalVec = obj.Normal(p, uv);
            
            V3 incidentVec = dL.Direction;
            
            V3 reflectedVec = 2 * (normalVec * incidentVec) *
                              normalVec - incidentVec;
            reflectedVec.Normalize();
            
            V3 viewingVec = CameraPos - p;
            viewingVec.Normalize();
            
            Color diffuseIllu = ComputeDiffuseComponent(
                dL, incidentVec, normalVec, obj, uv
            );
            Color specularIllu = ComputeSpecularComponent(
                dL, reflectedVec, normalVec, obj, uv
            );
            
            return diffuseIllu + specularIllu;
        }
        
        private Color ComputeDiffuseComponent(
            Light l, V3 incidentVec, V3 normalVec, Object3D obj, V2 uv
        ) {
            Color diffuseIllu = new Color(0, 0, 0);
            if (incidentVec * normalVec > 0.0f) {
                diffuseIllu = obj.TextureColor(uv) * l.Intensity *
                              obj.Material.KDiffuse * (incidentVec * normalVec);
            }
            
            return diffuseIllu;
        }
        
        private Color ComputeSpecularComponent(
            Light l, V3 reflectedVec, V3 viewingVec, Object3D obj, V2 uv
        ) {
            Color specularIllu = new Color(0, 0, 0);
            if (reflectedVec * viewingVec > 0.0f) {
                specularIllu = l.Intensity * obj.Material.KSpecular *
                     Mathf.Pow(
                        reflectedVec * viewingVec,
                        obj.Material.Shininess
                     );
            }
            
            return specularIllu;
        }
    }

}
