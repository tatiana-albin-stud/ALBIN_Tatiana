﻿
using System;
using System.Drawing;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace ALBIN_Tatiana
{
    /**
     * Realizat de: ALBIN Tatiana, 3133A
     * 
     */
    class ImmediateMode : GameWindow
    {

        private const int XYZ_SIZE = 75;

        Color colorVertex1 ;
        Color colorVertex2 ;
        Color colorVertex3 ;
        public ImmediateMode() : base(800, 600, new GraphicsMode(32, 24, 0, 8))
        {
            VSync = VSyncMode.On;

            Console.WriteLine("OpenGl versiunea: " + GL.GetString(StringName.Version));
            Title = "OpenGl versiunea: " + GL.GetString(StringName.Version) + " (mod imediat)";

        }
        Color GetRandomColor()
        {
            Random random = new Random();
            int randR = random.Next(0, 256);
            int randG = random.Next(0, 256);
            int randB = random.Next(0, 256);

            Color color = Color.FromArgb(randR, randG, randB);
            return color;
        }
        /**Setare mediu OpenGL și încarcarea resurselor (dacă e necesar) - de exemplu culoarea de
           fundal a ferestrei 3D.
           Atenție! Acest cod se execută înainte de desenarea efectivă a scenei 3D. */
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.ClearColor(Color.White);
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);
            GL.Hint(HintTarget.PolygonSmoothHint, HintMode.Nicest);
        }

        /**Inițierea afișării și setarea viewport-ului grafic. Metoda este invocată la redimensionarea
           ferestrei. Va fi invocată o dată și imediat după metoda ONLOAD()!
           Viewport-ul va fi dimensionat conform mărimii ferestrei active (cele 2 obiecte pot avea și mărimi 
           diferite). */
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, Width, Height);

            double aspect_ratio = Width / (double)Height;

            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver2, (float)aspect_ratio, 1, 600);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspective);

            Matrix4 lookat = Matrix4.LookAt( 45, 45, 0, 0, 0, 0, 0, 1, 0);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookat);


        }

        /** Secțiunea pentru "game logic"/"business logic". Tot ce se execută în această secțiune va fi randat
            automat pe ecran în pasul următor - control utilizator, actualizarea poziției obiectelor, etc. */
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
           

            KeyboardState keyboard = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();

            //apasarea a doua taste
             if (keyboard[Key.Left] && keyboard[Key.A])
             {
                 Matrix4 lookat = Matrix4.LookAt(15, 30, 30, 0, 0, 0, 0, 1, 0);
                 GL.MatrixMode(MatrixMode.Modelview);
                 GL.LoadMatrix(ref lookat);
             }
             if (keyboard[Key.Right] && keyboard[Key.B])
             {
                 Matrix4 lookat = Matrix4.LookAt(45, 30, 30, 0, 0, 0, 0, 1, 0);
                 GL.MatrixMode(MatrixMode.Modelview);
                 GL.LoadMatrix(ref lookat);
             }
             if (keyboard[Key.K])
            {
                colorVertex1 = GetRandomColor();
                colorVertex2 = GetRandomColor();
                colorVertex3 = GetRandomColor();
                
                Console.WriteLine(colorVertex1 + " " + colorVertex2 + " " + colorVertex3);
            }
            //8.mouse-ul
             Matrix4 lookat1 = Matrix4.LookAt(mouse.X, mouse.Y, 30, 0, 0, 0, 0, 1, 0);
             GL.MatrixMode(MatrixMode.Modelview);
             GL.LoadMatrix(ref lookat1);
            


            if (keyboard[Key.Escape])
            {
                Exit();
            }
        }

        private void ImmediateMode_MouseMove(object sender, MouseMoveEventArgs e)
        {
            throw new NotImplementedException();
        }

        /** Secțiunea pentru randarea scenei 3D. Controlată de modulul logic din metoda ONUPDATEFRAME().
            Parametrul de intrare "e" conține informatii de timing pentru randare. */
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Clear(ClearBufferMask.DepthBufferBit);




            DrawAxes();

            DrawObjects();



            // Se lucrează în modul DOUBLE BUFFERED - câtă vreme se afișează o imagine randată, o alta se randează în background apoi cele 2 sunt schimbate...
            SwapBuffers();
        }

        private void DrawAxes()
        {

            //GL.LineWidth(3.0f);

            // Desenează axa Ox (cu roșu).
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Red);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(XYZ_SIZE, 0, 0);
            
           

            // Desenează axa Oy (cu galben).
            GL.Color3(Color.Yellow);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, XYZ_SIZE, 0); ;

            // Desenează axa Oz (cu verde).
            
            GL.Color3(Color.Green);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 0, XYZ_SIZE);
            GL.End();

            
        }

        private void DrawObjects()
        {
            GL.PointSize(8);

            //GL.Begin(PrimitiveType.Points);
            //GL.Color3(1, 0, 0);
            //GL.Vertex3(3, 5, 8);

            // GL.End();
            //  
            //1.Triangle
            /*
             * 
             GL.Begin(PrimitiveType.Triangles);
             GL.Color3(Color.Red);
             GL.Vertex3(22, 13, 16);
             GL.Color3(Color.ForestGreen);
             GL.Vertex3(15, 16, 28);
             GL.Color3(Color.Aqua);
             GL.Vertex3(3, 8, 12);
             GL.End();
            */
            //3.
            /*
            * GL.Color3(Color.Brown);

              GL.LineWidth((float)86);
              GL.Begin(PrimitiveType.Lines);
              GL.Vertex3(3, 8, 12);
              GL.Vertex3(16, 15, 25);
              GL.End();
              GL.LineWidth((float)1);

              GL.Color3(Color.CadetBlue);
              GL.PointSize((float)15.2);
              GL.Begin(PrimitiveType.Points);
              GL.Vertex3(5, 8, 10);
              GL.End();
            */
            //4.LineLoop
            /*
            GL.Color3(Color.Gold);
            GL.LineWidth(6);
            GL.Begin(PrimitiveType.LineLoop);
            GL.Vertex3(13, 15, 25);
            GL.Vertex3(2, 6, 12);
            GL.Vertex3(24, 32, 16);
            GL.End();
            */
            //4.LineStrip
            /*
            GL.Color3(Color.Khaki);
            GL.LineWidth(6);
            GL.Begin(PrimitiveType.LineStrip);
            GL.Vertex3(13, 15, 15);
            GL.Vertex3(12, 16, 12);
            GL.Vertex3(14, 12, 16);
            GL.Vertex3(15, 16, 18);
            GL.End();
            */
            //4. TriangleStrip
            /* 
             GL.Color3(Color.Black);
             GL.Begin(PrimitiveType.TriangleStrip);
             GL.Vertex3(13, 15, 15);
             GL.Vertex3(12, 16, 12);
             GL.Vertex3(14, 12, 16);
             GL.Color3(Color.Yellow);
             GL.Vertex3(23, 25, 25);
             GL.Vertex3(22, 26, 22);
             GL.Vertex3(24, 22, 26);
             GL.End();
            */
            //4. TriangleFan
            /* 
             GL.Color3(Color.Black);
             GL.Begin(PrimitiveType.TriangleFan);
             GL.Vertex3(13, 15, 15);
             GL.Vertex3(12, 16, 12);
             GL.Vertex3(14, 12, 16);
             GL.Color3(Color.Yellow);
             GL.Vertex3(13, 15, 15);
             GL.Vertex3(22, 26, 22);
             GL.Vertex3(24, 22, 26);
             GL.Color3(Color.Green);
             GL.Vertex3(13, 15, 15);
             GL.Vertex3(32, 26, 22);
             GL.Vertex3(38, 22, 26);
             GL.End();
            */
            //9.Triangles with random
            GL.Color4(colorVertex1);
            GL.Begin(PrimitiveType.Triangles);
            GL.Vertex3(12, 23, 30);
            GL.Color4(colorVertex2);
            GL.Vertex3(13, 25, 35);
            GL.Color4(colorVertex3);
            GL.Vertex3(25, 35, 40);
            GL.End();

        }


        [STAThread]
        static void Main(string[] args)
        {

            /**Utilizarea cuvântului-cheie "using" va permite dealocarea memoriei o dată ce obiectul nu mai este
               în uz (vezi metoda "Dispose()").
               Metoda "Run()" specifică cerința noastră de a avea 30 de evenimente de tip UpdateFrame per secundă
               și un număr nelimitat de evenimente de tip randare 3D per secundă (maximul suportat de subsistemul
               grafic). Asta nu înseamnă că vor primi garantat respectivele valori!!!
               Ideal ar fi ca după fiecare UpdateFrame să avem si un RenderFrame astfel încât toate obiectele generate
               în scena 3D să fie actualizate fără pierderi (desincronizări între logica aplicației și imaginea randată
               în final pe ecran). */
            using (ImmediateMode example = new ImmediateMode())
            {
                example.Run(30.0, 0.0);
            }
        }
    }

}
