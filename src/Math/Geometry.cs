using System;

namespace ImageSynthesis {

    class Geometry {

        public static void InvertSphericalCoord(
            V3 P, float r, out float u, out float v
        ) {
            P /= r;
            
            if (P.Z >= 1) {
                u = Mathf.PI / 2 ;
                v = 0;
            }
            else if (P.Z <= -1) {
                u = -Mathf.PI / 2 ;
                v = 0;
            }
            else {
                v = Mathf.Asin(P.Z);
                float t = P.X / Mathf.Cos(v);
                
                if (t <= -1) {
                    u = Mathf.PI;
                }
                else if (t >= 1) {
                    u = 0;
                }
                else {
                    if (P.Y < 0) {
                        u = 2 * Mathf.PI - Mathf.Acos(t);
                    }
                    else {
                        u = Mathf.Acos(t);
                    }
                }
            }
        }
	}
}
