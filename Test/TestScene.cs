using System.Numerics;
using Raylib_cs;
using Sparkle.csharp;
using Sparkle.csharp.entity;
using Sparkle.csharp.graphics.util;
using Sparkle.csharp.gui;
using Sparkle.csharp.scene;

namespace Test; 

public class TestScene : Scene {
    
    public TestScene(string name) : base(name) { }

    protected override void Init() {
        Vector3 pos = new Vector3(10.0f, 10.0f, 10.0f);
        Cam3D cam3D = new Cam3D(pos, 70, CameraMode.CAMERA_ORBITAL) {
            Target = new Vector3(0, 0, 0)
        };
        this.AddEntity(cam3D);
        
        //Cam2D cam2D = new Cam2D(new Vector2(10, 10), new Vector2(10, 10), Cam2D.CameraMode.Custom);
        //this.AddEntity(cam2D);
        
        /*
        for (int i = 0; i < 1000; i++) {
            this.AddEntity(new TestEntity(new Vector3(0, i, 0)));
        }*/

        TestEntity entity = new TestEntity(new Vector3(0, 20, 0));
        this.AddEntity(entity);
        
        this.AddEntity(new GroundEntity(Vector3.Zero));
    }

    protected override void Update() {
        base.Update();
        
        if (Input.IsKeyPressed(KeyboardKey.KEY_E)) {
            GuiManager.SetGui(new TestGui("Test"));
        }

        if (Input.IsKeyPressed(KeyboardKey.KEY_R)) {
            GuiManager.SetGui(null);
        }
    }

    protected override void Draw() {
        base.Draw();
        
        // BEGIN 3D
        SceneManager.MainCamera!.BeginMode3D();

        // DRAW GIRD
        ModelHelper.DrawGrid(10, 1);
        
        // DRAW CUBE
        ModelHelper.DrawCube(new Vector3(3, 2, 3), 5, 5, 5, Color.PURPLE);
        
        // DRAW LINE
        ModelHelper.DrawLine3D(new Vector3(10, 3, 4), new Vector3(-10, -3, -4), Color.RED);
        
        // DRAW SECOND LINE
        ModelHelper.DrawLine3D(new Vector3(0, 3, 4), new Vector3(-10, -3, -4), Color.BLUE);
        
        ModelHelper.DrawCube(SceneManager.MainCamera.Target, 2, 2, 2, Color.RED);

        // END 3D
        SceneManager.MainCamera.EndMode3D();
        
        /*
        Cam2D cam2D = (Cam2D) this.GetEntity(1);

        if (Input.IsKeyDown(KeyboardKey.KEY_A)) {
            cam2D.Target.X += 1.0F * Time.Delta;
        }
        
        cam2D.BeginMode2D();
        
        ShapeHelper.DrawRectangle((int) cam2D.Target.X, (int) cam2D.Target.Y, 5, 5, Color.WHITE);
        
        cam2D.EndMode2D();*/
    }
}