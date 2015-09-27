using System;

namespace ImageSynthesis {

    class Geometry {

        public static void InvertSphericalCoord(
            V3 P, float r, out V2 uv
        ) {
            uv = new V2(0, 0);
            P /= r;
            
            if (P.Z >= 1) {
                uv.U = Mathf.PI / 2 ;
                uv.V = 0;
            }
            else if (P.Z <= -1) {
                uv.U = -Mathf.PI / 2 ;
                uv.V = 0;
            }
            else {
                uv.V = Mathf.Asin(P.Z);
                float t = P.X / Mathf.Cos(uv.V);
                
                if (t <= -1) {
                    uv.U = Mathf.PI;
                }
                else if (t >= 1) {
                    uv.U = 0;
                }
                else {
                    if (P.Y < 0) {
                        uv.U = 2 * Mathf.PI - Mathf.Acos(t);
                    }
                    else {
                        uv.U = Mathf.Acos(t);
                    }
                }
            }
        }
	}
}
