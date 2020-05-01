using CodingPlayground;

public class Program
{
    static float ElapsedTime = 0.0f;

    static int WIDTH = 512;
    static int HEIGHT = 512;

    static Mesh cubeMesh = Mesh.Cube;

    static Matrix4x4 projMatrix = Matrix4x4.zero;

    static Matrix4x4 matRotZ = Matrix4x4.zero;

    static Matrix4x4 matRotX = Matrix4x4.zero;

    static Bitmap bitmap = new Bitmap(WIDTH, HEIGHT);

    static void Main(string[] args)
    {
        var stopwatch = new System.Diagnostics.Stopwatch();
        stopwatch.Start();

        var path = System.IO.Path.Combine(
            @"C:\Users\minim\Desktop\image_output",
            "output.bmp"
        );

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

        bitmap.Fill(Color.black);

        System.Console.WriteLine("Reading input...");
        while (true)
        {
            var keyInfo = System.Console.ReadKey();

            switch (keyInfo.Key)
            {
                case System.ConsoleKey.LeftArrow:
                    ElapsedTime -= 1.0f;
                    Runtime();
                    break;
                case System.ConsoleKey.RightArrow:
                    ElapsedTime += 1.0f;
                    Runtime();
                    break;
                case System.ConsoleKey.Escape:
                    stopwatch.Stop();
                    System.Console.WriteLine($"Time elapsed: {stopwatch.Elapsed}");
                    return;
            }
            bitmap.Save(path);
        }

    }

    public static void Runtime()
    {
        bitmap.Fill(Color.black);
        float theta = 1.0f * ElapsedTime;

        // rot z
        matRotZ[0, 0] = (float)Math.Cos(theta);
        matRotZ[0, 1] = (float)Math.Sin(theta);
        matRotZ[1, 0] = -(float)Math.Sin(theta);
        matRotZ[1, 1] = -(float)Math.Cos(theta);
        matRotZ[2, 2] = 1f;
        matRotZ[3, 3] = 1f;

        // rot x
        matRotX[0, 0] = 1f;
        matRotX[1, 1] = (float)Math.Cos(theta * 0.5f);
        matRotX[1, 2] = (float)Math.Sin(theta * 0.5f);
        matRotX[2, 1] = -(float)Math.Sin(theta * 0.5f);
        matRotX[2, 2] = -(float)Math.Cos(theta * 0.5f);
        matRotX[3, 3] = 1f;

        for (int i = 0; i < cubeMesh.Count; i++)
        {
            var tri = cubeMesh[i];
            var triProjected = Triangle.zero;
            var triTranslated = Triangle.zero;

            var triRotatedZ = Triangle.zero;
            var triRotatedZX = Triangle.zero;

            triRotatedZ[0] = MultiplyMatrixVector(tri[0], matRotZ);
            triRotatedZ[1] = MultiplyMatrixVector(tri[1], matRotZ);
            triRotatedZ[2] = MultiplyMatrixVector(tri[2], matRotZ);

            triRotatedZX[0] = MultiplyMatrixVector(triRotatedZ[0], matRotX);
            triRotatedZX[1] = MultiplyMatrixVector(triRotatedZ[1], matRotX);
            triRotatedZX[2] = MultiplyMatrixVector(triRotatedZ[2], matRotX);

            triTranslated = triRotatedZX;
            triTranslated[0] = triTranslated[0].With(z: triRotatedZX[0].z + 3.0f);
            triTranslated[1] = triTranslated[1].With(z: triRotatedZX[1].z + 3.0f);
            triTranslated[2] = triTranslated[2].With(z: triRotatedZX[2].z + 3.0f);

            triProjected[0] = MultiplyMatrixVector(triTranslated[0], projMatrix);
            triProjected[1] = MultiplyMatrixVector(triTranslated[1], projMatrix);
            triProjected[2] = MultiplyMatrixVector(triTranslated[2], projMatrix);

            // Scale into view
            triProjected[0] = triProjected[0].With(x: triProjected[0].x + 1.0f);
            triProjected[0] = triProjected[0].With(y: triProjected[0].y + 1.0f);
            triProjected[1] = triProjected[1].With(x: triProjected[1].x + 1.0f);
            triProjected[1] = triProjected[1].With(y: triProjected[1].y + 1.0f);
            triProjected[2] = triProjected[2].With(x: triProjected[2].x + 1.0f);
            triProjected[2] = triProjected[2].With(y: triProjected[2].y + 1.0f);

            triProjected[0] = triProjected[0].With(x: triProjected[0].x * 0.5f * (float)(WIDTH));
            triProjected[0] = triProjected[0].With(y: triProjected[0].y * 0.5f * (float)(HEIGHT));
            triProjected[1] = triProjected[1].With(x: triProjected[1].x * 0.5f * (float)(WIDTH));
            triProjected[1] = triProjected[1].With(y: triProjected[1].y * 0.5f * (float)(HEIGHT));
            triProjected[2] = triProjected[2].With(x: triProjected[2].x * 0.5f * (float)(WIDTH));
            triProjected[2] = triProjected[2].With(y: triProjected[2].y * 0.5f * (float)(HEIGHT));

            bitmap.DrawTriangle(
                (int)triProjected[0].x, (int)triProjected[0].y,
                (int)triProjected[1].x, (int)triProjected[1].y,
                (int)triProjected[2].x, (int)triProjected[2].y,
                Color.white
            );
        }
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
