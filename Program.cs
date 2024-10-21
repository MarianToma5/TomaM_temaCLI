using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;

class Program : GameWindow
{
    private float objX = 0.0f, objY = 0.0f;
    private float speed = 0.05f;

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        // Setare fundal roz
        GL.ClearColor(1.0f, 0.75f, 0.8f, 1.0f);
        GL.MatrixMode(MatrixMode.Projection);
        GL.LoadIdentity();
        GL.Ortho(-1.0, 1.0, -1.0, 1.0, -1.0, 1.0);
    }


    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);
        GL.Viewport(0, 0, Width, Height);
    }

    // Funcție pentru a desena steaua galbenă
    private void DrawStar(float x, float y, float size)
    {
        GL.Begin(PrimitiveType.TriangleFan);
        GL.Color3(1.0f, 1.0f, 0.0f);  // Culoare galbenă

        // Centrul stelei
        GL.Vertex2(x, y);

        // Coordonatele stelei (10 puncte)
        for (int i = 0; i <= 10; i++)
        {
            double angle = i * Math.PI / 5;  // Se alternează între colțuri și punctele intermediare
            double radius = (i % 2 == 0) ? size : size / 2.5;
            double xPos = x + Math.Cos(angle) * radius;
            double yPos = y + Math.Sin(angle) * radius;
            GL.Vertex2(xPos, yPos);
        }

        GL.End();
    }

    protected override void OnRenderFrame(FrameEventArgs e)
    {
        base.OnRenderFrame(e);
        GL.Clear(ClearBufferMask.ColorBufferBit);

        // Desenează steaua galbenă la poziția (objX, objY)
        DrawStar(objX, objY, 0.2f);

        SwapBuffers();
    }

    protected override void OnUpdateFrame(FrameEventArgs e)
    {
        base.OnUpdateFrame(e);

        // Control cu tastele WASD
        KeyboardState input = Keyboard.GetState();
        if (input.IsKeyDown(Key.W))
        {
            objY += speed;
        }
        if (input.IsKeyDown(Key.S))
        {
            objY -= speed;
        }
        if (input.IsKeyDown(Key.A))
        {
            objX -= speed;
        }
        if (input.IsKeyDown(Key.D))
        {
            objX += speed;
        }
    }

    protected override void OnMouseMove(MouseMoveEventArgs e)
    {
        base.OnMouseMove(e);

        // Normalizează poziția mouse-ului
        float normalizedX = (e.X / (float)Width) * 2.0f - 1.0f;
        float normalizedY = 1.0f - (e.Y / (float)Height) * 2.0f;

        objX = normalizedX;
        objY = normalizedY;
    }

    static void Main(string[] args)
    {
        using (Program program = new Program())
        {
            program.Run(60.0);
        }
    }
}