using CodingPlayground;

public class Program
{
    static float ElapsedTime = 0.0f;
    static float theta = 0.0f;

    static Mesh cubeMesh = Mesh.Cube;

    static Matrix4x4 projMatrix = Matrix4x4.zero;

    static Matrix4x4 rotationMatZ = Matrix4x4.zero;

    static Matrix4x4 rotationMatX = Matrix4x4.zero;

    static Vector3 vCamera = Vector3.zero;

    static int WIDTH = 400;
    static int HEIGHT = 400;

    static Bitmap bitmap = new Bitmap(WIDTH, HEIGHT);

    static void Main(string[] args)
    {
        var path = System.IO.Path.Combine(
            $@"{System.AppDomain.CurrentDomain.BaseDirectory}",
            "output.bmp"
        );

        // bitmap.DrawTriangle(5, 5, 5, 95, 95, 5, Color.black);
        // bitmap.FillTriangle(5, 5, 5, 95, 95, 5, Color.black);

        // bitmap.Save(path);
        // return;

        // Projection Matrix
        float near = 0.1f;
        float far = 1000f;
        float fov = 90f;
        float aspectRatio = (float)HEIGHT / (float)WIDTH;
        float fovRad = 1.0f / (float)Math.Tan(fov * 0.5f / 180f * Math.PI);

        projMatrix[0, 0] = aspectRatio * fovRad;
        projMatrix[1, 1] = fovRad;
        projMatrix[2, 2] = far / (far - near);
        projMatrix[3, 2] = (-far * near) / (far - near);
        projMatrix[2, 3] = 1.0f;
        projMatrix[3, 3] = 0.0f;

        System.Console.WriteLine("Reading input...");
        while (true)
        {
            var keyInfo = System.Console.ReadKey();

            switch (keyInfo.Key)
            {
                case System.ConsoleKey.LeftArrow:
                    ElapsedTime -= 0.1f;
                    Runtime();
                    break;
                case System.ConsoleKey.RightArrow:
                    ElapsedTime += 0.1f;
                    Runtime();
                    break;
                case System.ConsoleKey.Escape:
                    return;
            }
            bitmap.Save(path);
        }
    }

    public static void Runtime()
    {
        var stopwatch = new System.Diagnostics.Stopwatch();
        stopwatch.Start();

        bitmap.Fill(0, 0, WIDTH, HEIGHT, Color.black);
        theta = 1f * ElapsedTime;

        // rot z
        rotationMatZ[0, 0] = (float)Math.Cos(theta);
        rotationMatZ[0, 1] = (float)Math.Sin(theta);
        rotationMatZ[1, 0] = -(float)Math.Sin(theta);
        rotationMatZ[1, 1] = (float)Math.Cos(theta);
        rotationMatZ[2, 2] = 1.0f;
        rotationMatZ[3, 3] = 1.0f;

        // rot x
        rotationMatX[0, 0] = 1.0f;
        rotationMatX[1, 1] = (float)Math.Cos(theta * 0.5f);
        rotationMatX[1, 2] = (float)Math.Sin(theta * 0.5f);
        rotationMatX[2, 1] = -(float)Math.Sin(theta * 0.5f);
        rotationMatX[2, 2] = (float)Math.Cos(theta * 0.5f);
        rotationMatX[3, 3] = 1.0f;

        for (int i = 0; i < cubeMesh.Count; i++)
        {
            var tri = cubeMesh[i];
            var triProjected = Triangle.zero;
            var triTranslated = Triangle.zero;

            var rotatedTriZ = Triangle.zero;
            var rotatedZX = Triangle.zero;

            rotatedTriZ.p0 = MultiplyMatrixVector(tri.p0, rotationMatZ);
            rotatedTriZ.p1 = MultiplyMatrixVector(tri.p1, rotationMatZ);
            rotatedTriZ.p2 = MultiplyMatrixVector(tri.p2, rotationMatZ);

            // Rotate in X-Axis
            rotatedZX.p0 = MultiplyMatrixVector(rotatedTriZ.p0, rotationMatX);
            rotatedZX.p1 = MultiplyMatrixVector(rotatedTriZ.p1, rotationMatX);
            rotatedZX.p2 = MultiplyMatrixVector(rotatedTriZ.p2, rotationMatX);

            // Rotate into the Screen
            triTranslated = rotatedZX;
            triTranslated.p0.Select(z: rotatedZX.p0.z + 2.0f);
            triTranslated.p1.Select(z: rotatedZX.p1.z + 2.0f);
            triTranslated.p2.Select(z: rotatedZX.p2.z + 2.0f);

            Vector3 normal = Vector3.zero;
            Vector3 line1 = Vector3.zero;
            Vector3 line2 = Vector3.zero;

            line1.x = triTranslated.p1.x - triTranslated.p0.x;
            line1.y = triTranslated.p1.y - triTranslated.p0.y;
            line1.z = triTranslated.p1.z - triTranslated.p0.z;

            line2.x = triTranslated.p2.x - triTranslated.p0.x;
            line2.y = triTranslated.p2.y - triTranslated.p0.y;
            line2.z = triTranslated.p2.z - triTranslated.p0.z;

            normal.x = line1.y * line2.z - line1.z * line2.y;
            normal.y = line1.z * line2.x - line1.x * line2.z;
            normal.z = line1.x * line2.y - line1.y * line2.x;

            var l = (float)System.Math.Sqrt(normal.x * normal.x + normal.y * normal.y + normal.z * normal.z);
            normal.x /= l;
            normal.y /= l;
            normal.z /= l;

            if (normal.x * (triTranslated.p0.x - vCamera.x) +
                normal.y * (triTranslated.p0.y - vCamera.y) +
                normal.z * (triTranslated.p0.z - vCamera.z) < 0)
            {

                Vector3 lightDir = new Vector3(0, 0, -1.0f);
                var nl = (float)System.Math.Sqrt(lightDir.x * lightDir.x + lightDir.y * lightDir.y + lightDir.z * lightDir.z);
                lightDir.x /= l;
                lightDir.y /= l;
                lightDir.z /= l;

                var dp = normal.x * lightDir.x + normal.y * lightDir.y + normal.z * lightDir.z;
                System.Console.WriteLine(dp);

                // Project lines from 3D --> 2D
                triProjected.p0 = MultiplyMatrixVector(triTranslated.p0, projMatrix);
                triProjected.p1 = MultiplyMatrixVector(triTranslated.p1, projMatrix);
                triProjected.p2 = MultiplyMatrixVector(triTranslated.p2, projMatrix);

                // Scale into view
                triProjected.p0.Select(x: triProjected.p0.x + 1.0f);
                triProjected.p1.Select(x: triProjected.p1.x + 1.0f);
                triProjected.p2.Select(x: triProjected.p2.x + 1.0f);

                triProjected.p0.Select(y: triProjected.p0.y + 1.0f);
                triProjected.p1.Select(y: triProjected.p1.y + 1.0f);
                triProjected.p2.Select(y: triProjected.p2.y + 1.0f);

                triProjected.p0.Select(x: triProjected.p0.x * 0.5f * (float)(WIDTH));
                triProjected.p1.Select(x: triProjected.p1.x * 0.5f * (float)(WIDTH));
                triProjected.p2.Select(x: triProjected.p2.x * 0.5f * (float)(WIDTH));

                triProjected.p0.Select(y: triProjected.p0.y * 0.5f * (float)(HEIGHT));
                triProjected.p1.Select(y: triProjected.p1.y * 0.5f * (float)(HEIGHT));
                triProjected.p2.Select(y: triProjected.p2.y * 0.5f * (float)(HEIGHT));

                bitmap.FillTriangle(
                    (int)triProjected.p0.x, (int)triProjected.p0.y,
                    (int)triProjected.p1.x, (int)triProjected.p1.y,
                    (int)triProjected.p2.x, (int)triProjected.p2.y,
                    Color.white * dp
                );

                // bitmap.DrawTriangle(
                //     (int)triProjected.p0.x, (int)triProjected.p0.y,
                //     (int)triProjected.p1.x, (int)triProjected.p1.y,
                //     (int)triProjected.p2.x, (int)triProjected.p2.y,
                //     Color.black
                // );
            }
        }

        stopwatch.Stop();
        System.Console.WriteLine($"\nTime elapsed: {stopwatch.Elapsed}");
    }

    public static Vector3 MultiplyMatrixVector(Vector3 input, Matrix4x4 matrix)
    {
        var output = Vector3.zero;
        output.x = input.x * matrix[0, 0] + input.y * matrix[1, 0] + input.z * matrix[2, 0] + matrix[3, 0];
        output.y = input.x * matrix[0, 1] + input.y * matrix[1, 1] + input.z * matrix[2, 1] + matrix[3, 1];
        output.z = input.x * matrix[0, 2] + input.y * matrix[1, 2] + input.z * matrix[2, 2] + matrix[3, 2];
        float w = input.x * matrix[0, 3] + input.y * matrix[1, 3] + input.z * matrix[2, 3] + matrix[3, 3];

        if (w != 0.0f)
        {
            output.x /= w;
            output.y /= w;
            output.z /= w;
        }

        return output;
    }


}
