using System.Collections.Generic;
using ImageSynthesis;
using ImageSynthesis.Models;

namespace ImageSynthesis.Renderers {
    
    class Ray {
        
        public V3 Origin { get; private set; }
        public V3 Direction { get; private set; }
        public float Distance { get; private set; }
        
        private Object3D OriginObject;
        
        public Ray(V3 origin, V3 direction, Object3D originObject = null) {
            Origin = origin;
            
            Direction = direction;
            Direction.Normalize();
            
            OriginObject = originObject;
            
            Distance = float.MaxValue;
        }
        
        public V3 CollisionPoint() {
            return Origin + (Direction * Distance);
        }
        
        /// Retrieves the closest intersected object by this ray, and sets the
        /// distance to this object (Distance attribute).
        /// If no object is intersected, returns null.
        public Object3D ClosestIntersectedObject(List<Object3D> objects) {
            Object3D closestObject = null;
            Distance = float.MaxValue;
            
            foreach (Object3D o in objects) {
                float newDistance;
                bool intersect = o.Intersect(this, out newDistance);
                
                if (o != OriginObject && intersect && newDistance < Distance) {
                    closestObject = o;
                    Distance = newDistance;
                }
            }
            
            return closestObject;
        }
        
    }
}
