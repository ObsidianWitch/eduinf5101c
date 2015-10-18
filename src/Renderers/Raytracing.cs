using System.Collections.Generic;
using ImageSynthesis.Models;
using ImageSynthesis.Views;
using ImageSynthesis.Lights;

namespace ImageSynthesis.Renderers {

    class Raytracing : Renderer {
        
        private V3 CameraPos;
        private int MaxDepth;
        
        public Raytracing(
            Canvas canvas, Scene scene, V3 cameraPos, int maxDepth
        ) : base(canvas, scene)
        {
            CameraPos = cameraPos;
            MaxDepth = maxDepth;
        }

        /// Renders the scene.
        override public void Render() {
            Canvas.BeginDrawing();
            
            for (int x = 0 ; x < Canvas.Width ; x++) {
                for (int y = 0 ; y < Canvas.Height ; y++) {
                    V3 p = new V3(x, 0, y);
                    
                    Color color = Raytrace(
                        new Ray(
                            origin: CameraPos,
                            direction: p - CameraPos
                        ),
                        MaxDepth
                    );
                    
                    if (color != null) {
                        V3 pScreen = new V3(x, Canvas.Height - y, 0);
                        Canvas.DrawPixel(pScreen, color);
                    }
                }
            }
            
            Canvas.EndDrawing();
        }
        
        /// Casts a ray and checks if it intersects any object. We can then
        /// compute the color of the point corresponding to the closest
        /// intersected object. If the intersected object is transparent or
        /// reflective, new rays are cast from the collision point.
        private Color Raytrace(Ray ray, int depth) {
            if (depth <= 0) { return null; }
            
            Object3D collidedObj = ray.ClosestIntersectedObject(Scene.Objects);
            
            // If the ray previously encountered an object, compute the pixel's
            // color.
            if (collidedObj != null) {
                V3 collisionPoint = ray.CollisionPoint();
                V2 collisionUV = collidedObj.UV(collisionPoint);
                
                List<Light> lights = Occultation(collidedObj, collisionPoint);
                Color directComponent = Scene.IlluModel.Compute(
                    lights, collidedObj, collisionPoint, collisionUV
                );
                
                // reflected light component
                Color reflectionColor = ReflectionColor(
                    -ray.Direction, collisionPoint, collisionUV,
                    collidedObj, depth
                );
                
                // refracted light component
                Color refractionColor = RefractionColor(
                    ray.Direction, collisionPoint, collisionUV,
                    collidedObj, depth
                );
                
                return directComponent + reflectionColor + refractionColor;
            }
            
            return null;
        }
        
        /// Recursivly raytrace to get the color resulting from the reflected
        /// light component for the specified collisionPoint.
        private Color ReflectionColor(
            V3 incidentVec, V3 collisionPoint, V2 collisionUV,
            Object3D collidedObj, int depth
        ) {
            if (!collidedObj.Material.IsReflective()) { return Color.Black; }
            
            V3 normal = collidedObj.Normal(collisionPoint, collisionUV);
            Ray reflectionRay = new Ray(
                origin:       collisionPoint,
                direction:    incidentVec.ReflectedVector(normal),
                originObject: collidedObj
            );
            
            Color reflectionColor = Raytrace(reflectionRay, depth - 1);
            if (reflectionColor == null) { return Color.Black; }
            
            return collidedObj.Material.Reflection * reflectionColor;
        }
        
        /// Recursivly raytrace to get the color resulting from the refracted
        /// light component for the specified collisionPoint.
        private Color RefractionColor(
            V3 incidentVec, V3 collisionPoint, V2 collisionUV,
            Object3D collidedObj, int depth
        ) {
            if (!collidedObj.Material.IsTransparent()) { return Color.Black; }
            
            V3 normal = collidedObj.Normal(collisionPoint, collisionUV);
            
            Ray refractionRay = new Ray(
                origin: collisionPoint,
                direction: incidentVec.RefractedVector(
                    normalVec: normal,
                    n1: Scene.RefractiveIndex,
                    n2: collidedObj.Material.RefractiveIndex
                ),
                originObject: collidedObj
            );
            
            Color refractionColor = Raytrace(refractionRay, depth - 1);
            if (refractionColor == null) { return Color.Black; }
            
            return collidedObj.Material.Transparency * refractionColor;
        }
        
        /// Returns a list of lights from which the currentPoint is visible.
        /// If another object is placed between the currentPoint and a light,
        /// then this point is occulted and the light will not be added to the
        /// list.
        private List<Light> Occultation(Object3D currentObject, V3 currentPoint) {
            List<Light> lights = new List<Light>();
            
            foreach (Light l in Scene.Lights) {
                if (PointLightened(currentObject, currentPoint, l)) {
                    lights.Add(l);
                }
            }
            
            return lights;
        }
        
        /// Checks whether the specified light is able to lightens the
        /// currentPoint (i.e. there is no obstacle between them).
        private bool PointLightened (
            Object3D currentObject, V3 currentPoint, Light light
        ) {
            Ray lightRay;
            
            if (light.GetType().Name == "PointLight") {
                PointLight pl = (PointLight) light;
                lightRay = new Ray(
                    origin:       currentPoint,
                    direction:    pl.Position - currentPoint,
                    originObject: currentObject
                );
            }
            else if (light.GetType().Name == "DirectionalLight") {
                DirectionalLight dl = (DirectionalLight) light;
                lightRay = new Ray(
                    origin:       currentPoint,
                    direction:    -dl.Direction,
                    originObject: currentObject
                );
            }
            else if (light.GetType().Name == "AmbientLight") {
                return true;
            }
            else { return false; }
            
            return !lightRay.IntersectObject(Scene.Objects);
        }
        
    }
}
