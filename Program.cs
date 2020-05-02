using System;
using System.Diagnostics;

// TASKS:
// Console Window
// FPS
// Loop
// Loop Events

namespace Console3D
{
    public class Program
    {
        private static int WIDTH = 256;
        private static int HEIGHT = 256;

        private static Stopwatch runtimeWatch;

        private static Console3D c3D;
        private static ConsoleWindow cWin;


        static void Main(string[] args)
        {
            try
            {
                cWin = new ConsoleWindow(WIDTH, HEIGHT);
                c3D = new Console3D(cWin);
                Debug.Log("Start");
                Run();
                runtimeWatch.Stop();
            }
            catch (System.Exception e)
            {
                Debug.Log(e);
            }
            finally
            {
                Console.Read();
                Debug.Log("End");
            }
        }

        static void Run()
        {
            int loops = 0;
            runtimeWatch = new Stopwatch();
            runtimeWatch.Start();


            Mesh cubeMesh = Mesh.Cube;
            float zoom = 3.0f;
            var fileLoaded = true;
            // fileLoaded = cubeMesh.LoadFromFile(@"C:\Users\minim\Desktop\image_output\cone.obj");
            if (!fileLoaded) throw new System.IO.FileLoadException();


            #region Current Projection

            Matrix4x4 projMatrix = Matrix4x4.zero;
            Matrix4x4 rotationMatZ = Matrix4x4.zero;
            Matrix4x4 rotationMatX = Matrix4x4.zero;
            Vector3 vCamera = Vector3.zero;

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

            float theta = 0.0f;

            #endregion

            bool IsPaused = false;

            while (true)
            {
                // TODO: perform correct stepper
                if (Input.GetKeyDown(ConsoleKey.P))
                {
                    IsPaused = !IsPaused;
                    Debug.Log($"IsPaused: {IsPaused}");
                    continue;
                }

                if (Input.GetKeyDown(ConsoleKey.R))
                {
                    Debug.Log($"Render");
                    var bm = new Bitmap(WIDTH, HEIGHT);
                    bm.Render(c3D);
                    continue;
                }

                if (Input.GetKeyDown(ConsoleKey.RightArrow))
                {
                    if (IsPaused)
                    {
                        loops++;
                        runtimeWatch.Elapsed.Add(new TimeSpan(1));
                        Debug.Log($"Step {runtimeWatch.ElapsedTicks}");
                        goto Step;
                    }
                }

                if (IsPaused)
                {
                    // runtimeWatch.Stop();
                    continue;
                }
                else
                {
                    if (!runtimeWatch.IsRunning)
                    {
                        runtimeWatch.Start();
                    }
                }

            Step:

                // udpate theta (used for rotation)
                theta = 2.0f * (float)runtimeWatch.Elapsed.TotalSeconds;

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
                    var triRotatedZX = Triangle.zero;

                    rotatedTriZ.p0 = MultiplyMatrixVector(tri.p0, rotationMatZ);
                    rotatedTriZ.p1 = MultiplyMatrixVector(tri.p1, rotationMatZ);
                    rotatedTriZ.p2 = MultiplyMatrixVector(tri.p2, rotationMatZ);

                    // Rotate in X-Axis
                    triRotatedZX.p0 = MultiplyMatrixVector(rotatedTriZ.p0, rotationMatX);
                    triRotatedZX.p1 = MultiplyMatrixVector(rotatedTriZ.p1, rotationMatX);
                    triRotatedZX.p2 = MultiplyMatrixVector(rotatedTriZ.p2, rotationMatX);

                    triTranslated = triRotatedZX;
                    triTranslated.p0.z = triRotatedZX.p0.z + zoom;
                    triTranslated.p0.z = triRotatedZX.p0.z + zoom;
                    triTranslated.p1.z = triRotatedZX.p1.z + zoom;
                    triTranslated.p2.z = triRotatedZX.p2.z + zoom;

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

                        // Project lines from 3D --> 2D
                        triProjected.p0 = MultiplyMatrixVector(triTranslated.p0, projMatrix);
                        triProjected.p1 = MultiplyMatrixVector(triTranslated.p1, projMatrix);
                        triProjected.p2 = MultiplyMatrixVector(triTranslated.p2, projMatrix);

                        // Scale into view
                        triProjected.p0.x = triProjected.p0.x + 1.0f;
                        triProjected.p1.x = triProjected.p1.x + 1.0f;
                        triProjected.p2.x = triProjected.p2.x + 1.0f;

                        triProjected.p0.y = triProjected.p0.y + 1.0f;
                        triProjected.p1.y = triProjected.p1.y + 1.0f;
                        triProjected.p2.y = triProjected.p2.y + 1.0f;

                        triProjected.p0.x = triProjected.p0.x * 0.5f * (float)(WIDTH);
                        triProjected.p1.x = triProjected.p1.x * 0.5f * (float)(WIDTH);
                        triProjected.p2.x = triProjected.p2.x * 0.5f * (float)(WIDTH);

                        triProjected.p0.y = triProjected.p0.y * 0.5f * (float)(HEIGHT);
                        triProjected.p1.y = triProjected.p1.y * 0.5f * (float)(HEIGHT);
                        triProjected.p2.y = triProjected.p2.y * 0.5f * (float)(HEIGHT);

                        if (loops % 2 != 0)
                        {
                            c3D.FillTriangle(
                                triProjected.p2,
                                triProjected.p1,
                                triProjected.p0,
                                Color.white * dp
                            );

                            c3D.DrawTriangle(
                                triProjected.p1,
                                triProjected.p0,
                                triProjected.p2,
                                Color.black
                            );
                        }
                    }
                }

                if (loops % 2 == 0)
                {
                    c3D.ClearBuffer();
                }
                else
                {
                    c3D.Blit();
                }

                if (++loops % 10 == 0)
                {
                    cWin.UpdateTitle((float)(loops / runtimeWatch.Elapsed.TotalSeconds));
                }
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
}