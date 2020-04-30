using CodingPlayground;

public class Program
{
    static void Main(string[] args)
    {
        var stopwatch = new System.Diagnostics.Stopwatch();
        stopwatch.Start();

        int WIDTH = 512;
        int HEIGHT = 512;

        var path = System.IO.Path.Combine(
            @"C:\Users\minim\Desktop\image_output",
            "output.bmp"
        );

        Bitmap bitmap = new Bitmap(WIDTH, HEIGHT);

        bitmap.Fill(Color.black);

        var meshCube = new Mesh(12);
        meshCube.tris.Add(
            // SOUTH
            new Triangle((0, 0, 0), (0, 1, 0), (1, 1, 0)),
            new Triangle((0, 0, 0), (1, 1, 0), (1, 0, 0)),

            // EAST
            new Triangle((1, 0, 0), (1, 1, 0), (1, 1, 1)),
            new Triangle((1, 0, 0), (1, 1, 1), (1, 0, 1)),

            // NORTH
            new Triangle((1, 0, 1), (1, 1, 1), (0, 1, 1)),
            new Triangle((1, 0, 1), (0, 1, 1), (0, 0, 1)),

            // WEST
            new Triangle((0, 0, 1), (0, 1, 1), (0, 1, 0)),
            new Triangle((0, 0, 1), (0, 1, 0), (0, 0, 0)),

            // TOP
            new Triangle((0, 1, 0), (0, 1, 1), (1, 1, 1)),
            new Triangle((0, 1, 0), (1, 1, 1), (1, 1, 0)),

            // BOTTOM
            new Triangle((1, 0, 1), (0, 0, 1), (0, 0, 0)),
            new Triangle((1, 0, 1), (0, 0, 0), (1, 0, 0))
        );

        // Projection Matrix
        float near = 0.1f;
        float far = 1000f;
        float fov = 90f;
        float aspectRatio = (float)HEIGHT / (float)WIDTH;
        float fovRad = 1.0f / (float)Math.Tan(fov * 0.5f / 180f * Math.PI);

        Matrix4x4 matProj = new Matrix4x4();
        matProj.m[0][0] = aspectRatio * fovRad;
        matProj.m[1][1] = fovRad;
        matProj.m[2][2] = far / (far - near);
        matProj.m[3][2] = (-far * near) / (far - near);
        matProj.m[2][3] = 1.0f;
        matProj.m[3][3] = 0.0f;

        float elapsedTime = 500.0f;
        Matrix4x4 matRotZ = new Matrix4x4();
        Matrix4x4 matRotX = new Matrix4x4();
        float fTheta = 1.0f * elapsedTime;

        // rot z
        matRotZ.m[0][0] = (float)Math.Cos(fTheta);
        matRotZ.m[0][1] = (float)Math.Sin(fTheta);
        matRotZ.m[1][0] = -(float)Math.Sin(fTheta);
        matRotZ.m[1][1] = -(float)Math.Cos(fTheta);
        matRotZ.m[2][2] = 1f;
        matRotZ.m[3][3] = 1f;

        // rot x
        matRotX.m[0][0] = 1f;
        matRotX.m[1][1] = (float)Math.Cos(fTheta * 0.5f);
        matRotX.m[1][2] = (float)Math.Sin(fTheta * 0.5f);
        matRotX.m[2][1] = -(float)Math.Sin(fTheta * 0.5f);
        matRotX.m[2][2] = -(float)Math.Cos(fTheta * 0.5f);
        matRotX.m[3][3] = 1f;


        for (int i = 0; i < meshCube.tris.Count; i++)
        {
            Triangle tri = meshCube.tris[i];
            Triangle triProjected = new Triangle();
            Triangle triTranslated = new Triangle();

            Triangle triRotatedZ = new Triangle();
            Triangle triRotatedZX = new Triangle();

            MultiplyMatrixVector(ref tri.p[0], ref triRotatedZ.p[0], matRotZ);
            MultiplyMatrixVector(ref tri.p[1], ref triRotatedZ.p[1], matRotZ);
            MultiplyMatrixVector(ref tri.p[2], ref triRotatedZ.p[2], matRotZ);

            MultiplyMatrixVector(ref triRotatedZ.p[0], ref triRotatedZX.p[0], matRotX);
            MultiplyMatrixVector(ref triRotatedZ.p[1], ref triRotatedZX.p[1], matRotX);
            MultiplyMatrixVector(ref triRotatedZ.p[2], ref triRotatedZX.p[2], matRotX);

            triTranslated = triRotatedZX;
            triTranslated.p[0].z = triRotatedZX.p[0].z + 3.0f;
            triTranslated.p[1].z = triRotatedZX.p[1].z + 3.0f;
            triTranslated.p[2].z = triRotatedZX.p[2].z + 3.0f;

            MultiplyMatrixVector(ref triTranslated.p[0], ref triProjected.p[0], matProj);
            MultiplyMatrixVector(ref triTranslated.p[1], ref triProjected.p[1], matProj);
            MultiplyMatrixVector(ref triTranslated.p[2], ref triProjected.p[2], matProj);

            // Scale into view
            triProjected.p[0].x += 1f; triProjected.p[0].y += 1f;
            triProjected.p[1].x += 1f; triProjected.p[1].y += 1f;
            triProjected.p[2].x += 1f; triProjected.p[2].y += 1f;

            triProjected.p[0].x *= 0.5f * (float)WIDTH;
            triProjected.p[0].y *= 0.5f * (float)HEIGHT;
            triProjected.p[1].x *= 0.5f * (float)WIDTH;
            triProjected.p[1].y *= 0.5f * (float)HEIGHT;
            triProjected.p[2].x *= 0.5f * (float)WIDTH;
            triProjected.p[2].y *= 0.5f * (float)HEIGHT;

            bitmap.DrawTriangle(
                (int)triProjected.p[0].x, (int)triProjected.p[0].y,
                (int)triProjected.p[1].x, (int)triProjected.p[1].y,
                (int)triProjected.p[2].x, (int)triProjected.p[2].y,
                Color.white
            );
        }

        bitmap.Save(path);

        stopwatch.Stop();
        System.Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);
    }

    static void MultiplyMatrixVector(ref Vector3 i, ref Vector3 o, Matrix4x4 m)
    {
        o.x = i.x * m.m[0][0] + i.y * m.m[1][0] + i.z * m.m[2][0] + m.m[3][0];
        o.y = i.x * m.m[0][1] + i.y * m.m[1][1] + i.z * m.m[2][1] + m.m[3][1];
        o.z = i.x * m.m[0][2] + i.y * m.m[1][2] + i.z * m.m[2][2] + m.m[3][2];
        float w = i.x * m.m[0][3] + i.y * m.m[1][3] + i.z * m.m[2][3] + m.m[3][3];

        if (w != 0.0f)
        {
            o.x /= w;
            o.y /= w;
            o.z /= w;
        }
    }


}
